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
Imports System.IO
Imports System.Text.Json
Imports System.Text.Json.Serialization

Public Class AppSettings

    Private Shared ReadOnly SettingsPath As String =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppSettings.json")

    Private Shared ReadOnly JsonOptions As JsonSerializerOptions = CreateOptions()
    Private Shared Function CreateOptions() As JsonSerializerOptions
        Dim opt As New JsonSerializerOptions With {
        .WriteIndented = True,
        .PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    }
        opt.Converters.Add(New JsonStringEnumConverter())
        Return opt
    End Function

#Region "枚举"

    Public Enum ThemeMode
        FollowSystem
        Light
        Dark
    End Enum

    Public Enum LanguageOption
        English
        ChineseSimplified
        ChineseTraditional
    End Enum

    Public Enum UpdateIntervalOption
        Daily
        Weekly
    End Enum

#End Region

#Region "分区设置类"

    Public Class AppearanceSettings
        Public Property Theme As ThemeMode = ThemeMode.FollowSystem
        Public Property ShowThemeColor As Boolean = True
        Public Property ThemeColorArgb As Integer = Color.FromArgb(58, 162, 143).ToArgb()
        Public Property ViewWindowThemeColor As Boolean = False

        Public Property Language As LanguageOption = LanguageOption.English

        Public Property SelectionAccentColorArgb As Integer = Color.Blue.ToArgb()
        Public Property BadgeColorArgb As Integer = Color.Red.ToArgb()

        Public Property MenuUppercase As Boolean = False
        Public Property ShowToolBar As Boolean = True
    End Class
    Public Class StartupSettings
        Public Property RestoreLastLibrary As Boolean = True
        Public Property LastLibraryName As String = "Tutorial" '隐藏

        Public Property AutoPlaySlideshow As Boolean = False

        Public Property AutoCheckUpdate As Boolean = True
        Public Property UpdateInterval As UpdateIntervalOption = UpdateIntervalOption.Daily

        Public Property LastCheckUpdateTime As DateTime = DateTime.MinValue '隐藏

        Public Property ShowHitokoto As Boolean = False
    End Class
    Public Class AdvancedSettings
        Public Property ThumbnailMinSize As Integer = 120
        Public Property ThumbnailMaxSize As Integer = 240

        Public Property ViewerKeepAspectRatio As Boolean = True

        Public Property AssociateBackupFiles As Boolean = False

        Public Property AutoSwitchSeconds As Integer = 5
        Public Property LoopPlayback As Boolean = True
        Public Property SwitchToNextLibraryAfterFinish As Boolean = False

        Public Property ShowDeveloperOptions As Boolean = False
    End Class

#End Region

#Region "主属性"

    Public Property Appearance As New AppearanceSettings()
    Public Property Startup As New StartupSettings()
    Public Property Advanced As New AdvancedSettings()

#End Region

#Region "核心方法"

    Public Shared Function Load() As AppSettings
        Try
            If Not File.Exists(SettingsPath) Then
                Dim def = CreateDefault()
                def.Save()
                Return def
            End If

            Dim json = File.ReadAllText(SettingsPath)
            Dim settings = JsonSerializer.Deserialize(Of AppSettings)(json, JsonOptions)

            If settings Is Nothing Then
                Return CreateDefault()
            End If

            Return settings

        Catch
            '读取失败自动恢复默认
            Return CreateDefault()
        End Try
    End Function

    Public Sub Save()
        Dim json = JsonSerializer.Serialize(Me, JsonOptions)
        File.WriteAllText(SettingsPath, json)
    End Sub

    Public Shared Sub Reset()
        Dim def = CreateDefault()
        def.Save()
    End Sub

    Public Shared Sub Export(filePath As String)
        If File.Exists(SettingsPath) Then
            File.Copy(SettingsPath, filePath, True)
        End If
    End Sub

    Public Shared Sub Import(filePath As String)
        If Not File.Exists(filePath) Then Return

        Dim json = File.ReadAllText(filePath)
        Dim imported = JsonSerializer.Deserialize(Of AppSettings)(json, JsonOptions)

        If imported IsNot Nothing Then
            Dim newJson = JsonSerializer.Serialize(imported, JsonOptions)
            File.WriteAllText(SettingsPath, newJson)
        End If
    End Sub

#End Region

#Region "默认创建"

    Public Shared Function CreateDefault() As AppSettings
        Return New AppSettings()
    End Function

#End Region

End Class