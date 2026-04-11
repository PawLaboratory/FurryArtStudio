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
Imports System.Text.Json.Serialization

Public Class HitokotoHandler
    Private Shared ReadOnly _httpClient As New HttpClient()
    Sub New()
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "FAS-Hitokoto/1.0")
    End Sub
    ''' <summary>
    ''' Hitokoto DTO类型
    ''' </summary>
    Private Class RawHitokoto
        <JsonPropertyName("id")>
        Public Property ID As Integer
        <JsonPropertyName("uuid")>
        Public Property UUID As String
        <JsonPropertyName("hitokoto")>
        Public Property Content As String
        <JsonPropertyName("type")>
        Public Property Type As String
        <JsonPropertyName("from")>
        Public Property From As String
        <JsonPropertyName("from_who")>
        Public Property FromWho As String
        <JsonPropertyName("creator")>
        Public Property Creator As String
        <JsonPropertyName("creator_uid")>
        Public Property CreatorUID As Integer
        <JsonPropertyName("reviewer")>
        Public Property Reviewer As Integer
        <JsonPropertyName("commit_form")>
        Public Property CommitForm As String
        <JsonPropertyName("created_at")>
        Public Property CreatedAt As String
    End Class
    Public Shared Async Function GetHitokoto() As Task(Of Hitokoto)
        Dim apiUrl = "https://v1.hitokoto.cn"
        Dim raw As New RawHitokoto
        Try
            Dim jsonResponse = Await _httpClient.GetStringAsync(apiUrl)
            raw = JsonSerializer.Deserialize(Of RawHitokoto)(jsonResponse)
        Catch ex As Exception
            With raw
                .ID = 0
                .UUID = Guid.Empty.ToString
                .Content = ""
                .Type = "a"
                .From = "无"
                .FromWho = ""
                .Creator = ""
                .CreatorUID = 0
                .Reviewer = 0
                .CommitForm = ""
                .CreatedAt = 0
            End With
        End Try
        Return ConvertHitokoto(raw)
    End Function
    Private Shared Function ConvertHitokoto(raw As RawHitokoto) As Hitokoto
        Dim result As New Hitokoto With {
            .ID = raw.ID,
            .Content = raw.Content,
            .From = raw.From,
            .FromWho = raw.FromWho,
            .Creator = raw.Creator,
            .CreatorUID = raw.CreatorUID,
            .CommitForm = raw.CommitForm,
            .Reviewer = raw.Reviewer
        }
        Select Case raw.Type
            Case "a"
                result.Type = HitokotoType.Anime
            Case "b"
                result.Type = HitokotoType.Comic
            Case "c"
                result.Type = HitokotoType.Game
            Case "d"
                result.Type = HitokotoType.Literature
            Case "e"
                result.Type = HitokotoType.Myself
            Case "f"
                result.Type = HitokotoType.Internet
            Case "g"
                result.Type = HitokotoType.Other
            Case "h"
                result.Type = HitokotoType.Video
            Case "i"
                result.Type = HitokotoType.Poem
            Case "j"
                result.Type = HitokotoType.NCM
            Case "k"
                result.Type = HitokotoType.Philosophy
            Case "l"
                result.Type = HitokotoType.Funny
            Case Else
                result.Type = HitokotoType.Anime
        End Select
        result.UUID = Guid.Parse(raw.UUID)
        result.CreatedAt = UnixTimestampToDateTime(raw.CreatedAt)
        Return result
    End Function
End Class