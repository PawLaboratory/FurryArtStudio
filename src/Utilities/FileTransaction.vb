' FurryArtStudio - 本地稿件管理工具
' Copyright 2026 xionglongztz
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
Imports System.IO

Public Class FileTransaction

#Region "定义"
    '私有字段
    Private _targetPath As String '目标文件夹路径
    Private _workingPath As String '临时工作文件夹路径
    Private _isDisposed As Boolean '是否已释放
    Private _transactionType As TransactionType '当前模式类型
#End Region

#Region "属性"
    ''' <summary>
    ''' 获得目标目录路径
    ''' </summary>
    Public ReadOnly Property TargetPath As String
        Get
            Return _targetPath
        End Get
    End Property
    ''' <summary>
    ''' 获得工作区路径
    ''' </summary>
    Public ReadOnly Property WorkingPath As String
        Get
            Return _workingPath
        End Get
    End Property
    ''' <summary>
    ''' 获得当前事务类型
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property GetTransactionType As TransactionType
        Get
            Return _transactionType
        End Get
    End Property
#End Region

#Region "初始化"
    ''' <summary>
    ''' 构造函数 - 编辑
    ''' </summary>
    ''' <param name="targetPath">目标文件夹路径</param>
    Public Sub New(targetPath As String)
        _transactionType = TransactionType.Edit
        '验证目标文件夹
        If Not Directory.Exists(targetPath) Then
            Throw New DirectoryNotFoundException($"目标文件夹不存在: {targetPath}")
        End If
        _targetPath = targetPath
        InitializeWorkspace()
    End Sub
    ''' <summary>
    ''' 构造函数 - 新建
    ''' </summary>
    Public Sub New()
        _transactionType = TransactionType.New
        InitializeWorkspace()
    End Sub
    ''' <summary>
    ''' 初始化工作区
    ''' </summary>
    Private Sub InitializeWorkspace()
        ' 修改临时工作区到程序运行目录
        _workingPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $".FAS_{Guid.NewGuid():N}") '工作区路径, 确保全局唯一
        Directory.CreateDirectory(_workingPath)
        If _transactionType = TransactionType.Edit Then '编辑模式下将文件复制到工作区
            SyncTargetToWorkspace()
        End If
    End Sub
    ''' <summary>
    ''' 从目标文件夹同步到工作区
    ''' </summary>
    Private Sub SyncTargetToWorkspace()
        For Each file In Directory.GetFiles(_targetPath)
            If Path.GetFileName(file).ToLower() = ".preview.jpg" Then
                Continue For '跳过预览图文件
            End If
            IO.File.Copy(file, Path.Combine(_workingPath, Path.GetFileName(file)))
        Next
    End Sub
    ''' <summary>
    ''' 设置目标路径(新建模式下使用)
    ''' </summary>
    ''' <param name="targetPath">目标路径</param>
    Public Sub SetTargetPath(targetPath As String)
        If _transactionType <> TransactionType.New Then
            Throw New InvalidOperationException("只能在新建模式下设置目标路径")
        End If
        If String.IsNullOrWhiteSpace(targetPath) Then
            Throw New ArgumentNullException(NameOf(targetPath), "目标路径无效")
        End If
        _targetPath = targetPath
    End Sub
    ''' <summary>
    ''' 获取工作区所有文件
    ''' </summary>
    Public Function GetFileList() As List(Of FileItemInfo)
        Dim result As New List(Of FileItemInfo)

        For Each file In Directory.GetFiles(_workingPath)
            Dim info As New FileItemInfo With {
                .FileName = Path.GetFileName(file),
                .FullPath = file,
                .Status = FileStatus.Normal
            }
            result.Add(info)
        Next
        Return result
    End Function
#End Region

#Region "文件操作"
    ''' <summary>
    ''' 添加文件
    ''' </summary>
    Public Function AddFiles(filePaths As IEnumerable(Of String)) As List(Of AddFileResult)
        Dim results As New List(Of AddFileResult)

        For Each sourcePath In filePaths
            Dim result As New AddFileResult With {.SourcePath = sourcePath}
            Try
                If Not File.Exists(sourcePath) Then
                    result.Success = False
                    result.ErrorMessage = "源文件不存在"
                    results.Add(result)
                    Continue For
                End If
                Dim fileName = Path.GetFileName(sourcePath)
                Dim destPath = Path.Combine(_workingPath, fileName)
                If File.Exists(destPath) Then
                    result.Success = False
                    result.ErrorMessage = "目标文件已存在"
                    result.ExistsInWorkspace = True
                Else
                    ' 使用硬连接方式添加文件
                    If Not CreateHardLink(destPath, sourcePath, IntPtr.Zero) Then
                        ' 降级处理
                        File.Copy(sourcePath, destPath)
                    End If
                    result.Success = True
                    result.WorkingPath = destPath
                End If
            Catch ex As Exception
                result.Success = False
                result.ErrorMessage = ex.Message
            End Try
            results.Add(result)
        Next
        Return results
    End Function
    ''' <summary>
    ''' 删除文件
    ''' </summary>
    Public Function DeleteFile(fileName As String) As Boolean
        Dim filePath = Path.Combine(_workingPath, fileName)
        If File.Exists(filePath) Then
            File.Delete(filePath)
            Return True
        End If
        Return False
    End Function
#End Region

#Region "事务处理"
    ''' <summary>
    ''' 提交所有更改
    ''' </summary>
    Public Function Commit() As CommitResult
        Dim result As New CommitResult()
        Try
            '删除工作区全部文件
            For Each file In Directory.GetFiles(_targetPath)
                If Path.GetFileName(file).ToLower() = ".preview.jpg" Then
                    Continue For '跳过预览图文件
                End If
                IO.File.Delete(file)
            Next
            '复制工作区所有文件到目标文件夹
            For Each file In Directory.GetFiles(_workingPath)
                Dim destPath = Path.Combine(_targetPath, Path.GetFileName(file))
                If Not CreateHardLink(destPath, file, IntPtr.Zero) Then
                    IO.File.Copy(file, destPath)
                End If
                result.CopiedFiles.Add(Path.GetFileName(file))
            Next
            result.Success = True
            Return result
        Catch ex As Exception
            result.Success = False
            result.ErrorMessage = ex.Message
            Return result
        End Try
    End Function
    ''' <summary>
    ''' 回滚所有更改
    ''' </summary>
    Public Sub Rollback()
        ' 直接清理工作区，不做任何同步
        CleanupWorkspace()
        ' 重新初始化
        InitializeWorkspace()
    End Sub
    ''' <summary>
    ''' 清理工作区
    ''' </summary>
    Private Sub CleanupWorkspace()
        If Directory.Exists(_workingPath) Then
            Try
                Directory.Delete(_workingPath, True)
            Catch
                '忽略清理错误
            End Try
        End If
    End Sub
    ''' <summary>
    ''' 实现IDisposable接口, 支持Using语句
    ''' </summary>
    Public Sub Dispose()
        If Not _isDisposed Then
            CleanupWorkspace()
            _isDisposed = True
        End If
    End Sub
#End Region

End Class

#Region "工具类"
''' <summary>
''' 文件事务类型枚举
''' </summary>
Public Enum TransactionType
    [New] '新建模式
    Edit '编辑模式
End Enum
''' <summary>
''' 文件项信息类
''' </summary>
Public Class FileItemInfo
    Public Property FileName As String
    Public Property FullPath As String
    Public Property Status As FileStatus
End Class
''' <summary>
''' 文件状态枚举
''' </summary>
Public Enum FileStatus
    Normal '无修改
    Added '新增
    Deleted '删除
    Modified '已编辑
End Enum
''' <summary>
''' 添加文件结果类
''' </summary>
Public Class AddFileResult
    Public Property SourcePath As String
    Public Property WorkingPath As String
    Public Property Success As Boolean
    Public Property ErrorMessage As String
    Public Property ExistsInWorkspace As Boolean
End Class
''' <summary>
''' 提交结果类
''' </summary>
Public Class CommitResult
    Public Property Success As Boolean
    Public Property ErrorMessage As String
    Public Property CopiedFiles As New List(Of String)
End Class
#End Region