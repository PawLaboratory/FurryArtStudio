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
Imports System.Net.Http
Imports System.Text.Json
Imports System.Text.Json.Serialization

''' <summary>
''' 爱发电赞助计划
''' </summary>
Public Class AfdianSponsorPlan
    ''' <summary>
    ''' 赞助ID
    ''' </summary>
    Public Property PlanID As String
    ''' <summary>
    ''' 赞助名称
    ''' </summary>
    Public Property Name As String
    ''' <summary>
    ''' 赞助描述
    ''' </summary>
    Public Property Description As String
    ''' <summary>
    ''' 赞助价格
    ''' </summary>
    Public Property Price As String
    ''' <summary>
    ''' 更新时间
    ''' </summary>
    Public Property UpdateTime As Date
End Class

''' <summary>
''' 爱发电赞助者信息
''' </summary>
Public Class AfdianSponsor
    ''' <summary>
    ''' 赞助者赞助过的计划
    ''' </summary>
    Public Property SponsorPlans As List(Of AfdianSponsorPlan)
    ''' <summary>
    ''' 赞助者总赞助金额
    ''' </summary>
    ''' <returns></returns>
    Public Property AllSumAmount As String
    ''' <summary>
    ''' 首次赞助时间
    ''' </summary>
    Public Property FirstPayTime As Date
    ''' <summary>
    ''' 最后赞助时间
    ''' </summary>
    Public Property LastPayTime As Date
    ''' <summary>
    ''' 赞助者姓名
    ''' </summary>
    Public Property UserName As String
    ''' <summary>
    ''' 赞助者ID
    ''' </summary>
    Public Property UserID As String
    ''' <summary>
    ''' 赞助者头像
    ''' </summary>
    Public Property UserAvatar As Image
    ''' <summary>
    ''' 赞助者个人ID
    ''' </summary>
    Public Property UserPrivateId As String
End Class

Public Class AfdianHandler
    Private Shared ReadOnly _httpClient As New HttpClient()
    Public Shared Async Function CheckSponsorsAsync() As Task(Of List(Of AfdianSponsor))
        Dim allSponsors As New List(Of AfdianSponsor)()
        Dim page As Integer = 1
        Const perPage As Integer = 100   ' 每页最多100条，减少请求次数
        While True
            'POST 请求体
            Dim requestData As New Dictionary(Of String, Object) From {
                {"page", page},
                {"per_page", perPage}
            }
            Dim jsonContent As String = JsonSerializer.Serialize(requestData)
            Dim content As New StringContent(jsonContent, Text.Encoding.UTF8, "application/json")
            '发送请求
            Dim response As HttpResponseMessage = Await _httpClient.PostAsync("http://112.90.76.180:10465/sponsors", content)
            response.EnsureSuccessStatusCode()
            Dim jsonResponse As String = Await response.Content.ReadAsStringAsync()
            '反序列化 JSON
            Dim options As New JsonSerializerOptions() With {
                .PropertyNameCaseInsensitive = True,
                .NumberHandling = JsonNumberHandling.AllowReadingFromString  '允许字符串形式的数字
            }
            Dim apiResponse As SponsorData = JsonSerializer.Deserialize(Of SponsorData)(jsonResponse, options)
            For Each item In apiResponse.list
                '赞助计划列表
                Dim sponsor = New AfdianSponsor With {
                    .SponsorPlans = New List(Of AfdianSponsorPlan)()
                }
                If item.sponsor_plans IsNot Nothing Then
                    For Each plan In item.sponsor_plans
                        Dim sponsorPlan = New AfdianSponsorPlan() With {
                            .PlanID = plan.PlanID,
                            .Name = plan.Name,
                            .Description = plan.Description,
                            .Price = plan.Price,
                            .UpdateTime = UnixTimestampToDateTime(plan.UpdateTime)
                        }
                        sponsor.SponsorPlans.Add(sponsorPlan)
                    Next
                End If
                '总赞助金额
                sponsor.AllSumAmount = item.all_sum_amount
                '首次赞助时间
                Dim firstTime As Long? = item.first_pay_time
                If Not firstTime.HasValue Then
                    firstTime = item.create_time
                End If
                If firstTime.HasValue Then
                    sponsor.FirstPayTime = UnixTimestampToDateTime(firstTime.Value)
                End If
                '最后赞助时间
                If item.last_pay_time.HasValue Then
                    sponsor.LastPayTime = UnixTimestampToDateTime(item.last_pay_time.Value)
                End If
                '用户信息
                sponsor.UserName = item.user.Name
                sponsor.UserID = item.user.UserID
                '异步下载头像
                If Not String.IsNullOrEmpty(item.user.Avatar) Then
                    sponsor.UserAvatar = Await DownloadImageAsync(item.user.Avatar)
                End If
                '私有ID
                sponsor.UserPrivateId = item.user.PrivateID
                allSponsors.Add(sponsor)
            Next
            '判断是否还有下一页
            If page >= apiResponse.total_page Then
                Exit While
            End If
            page += 1
        End While
        Return allSponsors
    End Function

    ''' <summary>
    ''' 异步下载图片
    ''' </summary>
    Private Shared Async Function DownloadImageAsync(url As String) As Task(Of Image)
        Try
            Dim imageBytes As Byte() = Await _httpClient.GetByteArrayAsync(url)
            Using ms As New MemoryStream(imageBytes)
                Return Image.FromStream(ms)
            End Using
        Catch ex As Exception
            '下载失败时返回 Nothing
            Return Nothing
        End Try
    End Function
    Private Class SponsorData
        <JsonPropertyName("total_count")>
        Public Property total_count As Integer
        <JsonPropertyName("total_page")>
        Public Property total_page As Integer
        <JsonPropertyName("list")>
        Public Property list As List(Of SponsorItem)
    End Class
    Private Class SponsorItem
        <JsonPropertyName("sponsor_plans")>
        Public Property sponsor_plans As List(Of SponsorPlanItem)
        <JsonPropertyName("current_plan")>
        Public Property current_plan As CurrentPlanItem
        <JsonPropertyName("all_sum_amount")>
        Public Property all_sum_amount As String
        <JsonPropertyName("first_pay_time")>
        Public Property first_pay_time As Long?
        <JsonPropertyName("create_time")>
        Public Property create_time As Long?
        <JsonPropertyName("last_pay_time")>
        Public Property last_pay_time As Long?
        <JsonPropertyName("user")>
        Public Property user As UserItem
    End Class
    Private Class SponsorPlanItem
        <JsonPropertyName("plan_id")>
        Public Property PlanID As String
        <JsonPropertyName("name")>
        Public Property Name As String
        <JsonPropertyName("pic")>
        Public Property Img As String
        <JsonPropertyName("desc")>
        Public Property Description As String
        <JsonPropertyName("price")>
        Public Property Price As String
        <JsonPropertyName("update_time")>
        Public Property UpdateTime As Long
        ' 其他字段按需添加
    End Class
    Private Class CurrentPlanItem
        <JsonPropertyName("name")>
        Public Property Name As String
        ' 其他字段可选
    End Class
    Private Class UserItem
        <JsonPropertyName("user_id")>
        Public Property UserID As String
        <JsonPropertyName("name")>
        Public Property Name As String
        <JsonPropertyName("avatar")>
        Public Property Avatar As String
        <JsonPropertyName("user_private_id")>
        Public Property PrivateID As String
    End Class
End Class
