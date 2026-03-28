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
Imports System.Net.Http
Imports System.Text.Json

''' <summary>
''' 更新检查结果
''' </summary>
Public Class UpdateInfo
    Public Property HasError As Boolean = False
    Public Property ErrorMessage As String = ""
    Public Property IsUpdateAvailable As Boolean = False
    Public Property LatestVersion As String = ""
    Public Property DownloadUrl As String = ""
    Public Property ReleaseNotes As String = ""
End Class

''' <summary>
''' GitHub 更新检查器
''' </summary>
Public Class UpdateChecker
    Private Shared ReadOnly _httpClient As New HttpClient()
    Sub New()
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "FAS-UpdateChecker/1.0")
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json")
    End Sub
    ''' <summary>
    ''' 异步检查 GitHub 是否有新版本
    ''' </summary>
    Public Shared Async Function CheckForUpdateAsync() As Task(Of UpdateInfo)
        Dim result As New UpdateInfo()
        Try
            '获得版本
            Dim currentVersion As String = GetCurrentVersion()
            If String.IsNullOrEmpty(currentVersion) Then
                Throw New Exception("无法获取当前版本号")
            End If
            '调用 GitHub API
            'Dim apiUrl = "https://api.github.com/repos/PawLaboratory/FurryArtStudio/releases/latest"
            Dim apiUrl = "http://112.90.76.180:10465/release" '@rainyxin 的服务器
            Dim jsonResponse = Await _httpClient.GetStringAsync(apiUrl)
            '解析 JSON
            Using jsonDoc = JsonDocument.Parse(jsonResponse)
                Dim root = jsonDoc.RootElement
                '获取 tag_name
                Dim tagProp As JsonElement
                If Not root.TryGetProperty("tag_name", tagProp) Then
                    Throw New Exception("响应中缺少 tag_name 字段")
                End If
                Dim latestTag = tagProp.GetString()
                '获取 body
                Dim releaseNotes As String = ""
                Dim bodyProp As JsonElement
                If root.TryGetProperty("body", bodyProp) Then
                    releaseNotes = bodyProp.GetString()?.Trim()
                End If
                '获取 assets 数组
                Dim assetsProp As JsonElement
                If Not root.TryGetProperty("assets", assetsProp) Then
                    Throw New Exception("响应中缺少 assets 字段")
                End If
                '查找第一个 zip 文件
                Dim downloadUrl As String = Nothing
                For Each asset In assetsProp.EnumerateArray()
                    Dim name As String = Nothing
                    Dim nameProp As JsonElement
                    If asset.TryGetProperty("name", nameProp) Then
                        name = nameProp.GetString()
                    End If
                    If Not String.IsNullOrEmpty(name) AndAlso name.ToLower().EndsWith(".zip") Then
                        Dim urlProp As JsonElement
                        If asset.TryGetProperty("browser_download_url", urlProp) Then
                            downloadUrl = urlProp.GetString()
                        End If
                        Exit For
                    End If
                Next
                If String.IsNullOrEmpty(downloadUrl) Then
                    Throw New Exception("未在 release 中找到 .zip 文件")
                End If
                '比较版本
                If String.IsNullOrEmpty(latestTag) Then
                    Throw New Exception("无法解析最新版本号")
                End If
                If IsNewerVersion(currentVersion, latestTag) Then
                    result.IsUpdateAvailable = True
                    result.LatestVersion = latestTag
                    result.DownloadUrl = downloadUrl
                    result.ReleaseNotes = releaseNotes
                Else
                    result.IsUpdateAvailable = False
                    result.LatestVersion = latestTag
                    result.ReleaseNotes = releaseNotes
                End If
            End Using
        Catch ex As HttpRequestException
            result.HasError = True
            result.ErrorMessage = "网络连接失败: " & ex.Message
        Catch ex As JsonException
            result.HasError = True
            result.ErrorMessage = "解析 GitHub 响应失败: " & ex.Message
        Catch ex As Exception
            result.HasError = True
            result.ErrorMessage = ex.Message
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 比较版本号
    ''' </summary>
    Private Shared Function IsNewerVersion(currentVer As String, latestVer As String) As Boolean
        Dim cleanCurrent = currentVer.TrimStart("v"c)
        Dim cleanLatest = latestVer.TrimStart("v"c)
        Try
            Dim cur = New Version(cleanCurrent)
            Dim lat = New Version(cleanLatest)
            Return lat > cur
        Catch
            ' 如果解析失败，采用简单字符串比较（按语义化版本规则降级）
            Return String.Compare(cleanLatest, cleanCurrent, StringComparison.OrdinalIgnoreCase) > 0
        End Try
    End Function
End Class