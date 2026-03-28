' FurryArtStudio - 本地稿件管理工具
' Copyright 2026 xionglongztz/PawLaboratory
'
' Licensed under the Apache License, Version 2.0 (the "License");
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http://www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.
Imports System.Data.SQLite
Imports System.IO

''' <summary>
''' 稿件库实例管理器
''' </summary>
Public Class LibraryManager
    Private Shared _instance As LibraryManager
    Private ReadOnly _libraries As New Dictionary(Of String, ArtworkLibrary)
    Private Shared ReadOnly _activeConnections As New Dictionary(Of String, SQLiteConnection)
    Private _currentLibrary As ArtworkLibrary

    ''' <summary>
    ''' 获取或设置单例
    ''' </summary>
    Public Shared ReadOnly Property Instance As LibraryManager
        Get
            If _instance Is Nothing Then
                _instance = New LibraryManager()
            End If
            Return _instance
        End Get
    End Property

    ''' <summary>
    ''' 初始化<see cref="LibraryManager"/>实例
    ''' </summary>
    Private Sub New()
        '单例模式
    End Sub

    ''' <summary>
    ''' 将所有稿件库加入到稿件库实例管理器
    ''' </summary>
    Public Sub LoadAllLibraries()
        Dim libraryFolders = Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Artworks"))
        For Each folder In libraryFolders
            Dim name = Path.GetFileName(folder)
            AddLibrary(name) '按照文件夹名称添加到稿件库列表
        Next
    End Sub

    ''' <summary>
    ''' 新增或新建稿件库
    ''' </summary>
    ''' <param name="name">稿件库名称</param>
    Public Sub AddLibrary(name As String)
        If Not _libraries.ContainsKey(name) Then
            Dim library As New ArtworkLibrary(name)
            _libraries.Add(name, library)
            _activeConnections.Add(name, library.DbConnection)
        End If
    End Sub

    ''' <summary>
    ''' 切换当前稿件库
    ''' </summary>
    ''' <param name="name">稿件库名称</param>
    ''' <returns>是否成功切换</returns>
    Public Function SwitchLibrary(name As String) As Boolean
        If _libraries.ContainsKey(name) Then
            _currentLibrary = _libraries(name)
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 获得当前稿件库
    ''' </summary>
    ''' <returns>当前稿件库, 类型为<see cref="ArtworkLibrary"></see></returns>
    Public Function GetCurrentLibrary() As ArtworkLibrary
        Return _currentLibrary
    End Function

    ''' <summary>
    ''' 获得全部已加载的稿件库
    ''' </summary>
    ''' <returns>全部稿件库</returns>
    Public Function GetAllLibraries() As Dictionary(Of String, ArtworkLibrary)
        Return _libraries
    End Function

    ''' <summary>
    ''' 关闭稿件库
    ''' </summary>
    ''' <param name="name">要关闭的稿件库名称</param>
    ''' <returns>是否关闭成功</returns>
    Public Function CloseLibrary(name As String) As Boolean
        If _libraries.ContainsKey(name) Then
            Dim library As ArtworkLibrary = _libraries(name)
            If library.DbConnection.State <> ConnectionState.Closed Then
                library.DbConnection.Close()
                GC.Collect() '确保数据库资源已释放
            End If
            library.DbConnection.Dispose()
            _libraries.Remove(name)
            _activeConnections.Remove(name)
            If name = _currentLibrary.LibraryName Then _currentLibrary = Nothing
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 关闭全部稿件库
    ''' </summary>
    Public Shared Sub CloseAllLibrary()
        For Each connection In _activeConnections.Values
            If connection.State <> ConnectionState.Closed Then
                connection.Close()
            End If
        Next
        _activeConnections.Clear()
    End Sub

    ''' <summary>
    ''' 将稿件从一个稿件库复制到另一个稿件库, 同时赋予新的UUID
    ''' </summary>
    ''' <param name="sourceLibraryName">原稿件库名称</param>
    ''' <param name="targetLibraryName">目标稿件库名称</param>
    ''' <param name="artworkUUID">原稿件库稿件UUID</param>
    ''' <returns>若复制成功, 则返回新稿件的UUID, 否则返回 <seealso cref="Guid.Empty"/></returns>
    Public Function CopyArtwork(sourceLibraryName As String,
                                targetLibraryName As String,
                                artworkUUID As Guid) As Guid
        If _libraries.ContainsKey(sourceLibraryName) AndAlso
           _libraries.ContainsKey(targetLibraryName) Then
            Dim sourceLib = _libraries(sourceLibraryName)
            Dim targetLib = _libraries(targetLibraryName)
            '从源库获取稿件数据
            Dim artwork = sourceLib.GetArtworkByUUID(artworkUUID)
            If artwork Is Nothing Then Return Guid.Empty
            '构造路径
            Dim sourceFolder = Path.Combine(sourceLib.LibraryPath, artwork.UUID.ToString())
            Dim targetFolder = Path.Combine(targetLib.LibraryPath, Guid.NewGuid().ToString())
            '新建目标文件夹
            Directory.CreateDirectory(targetFolder)
            For Each filePath In artwork.FilePaths
                Dim sourceFile = filePath
                Dim targetFile = Path.Combine(targetFolder, Path.GetFileName(filePath))
                File.Copy(sourceFile, targetFile, True)
            Next
            '添加到目标库数据库
            artwork.UUID = Guid.NewGuid() '新UUID
            targetLib.AddArtwork(artwork)
            Return artwork.UUID
        End If
        Return Guid.Empty
    End Function
End Class
