Imports System.Net.Http
Imports System.Text

Public Class Webhook
    ''' <summary>
    ''' 发送一个Webhook请求
    ''' </summary>
    ''' <param name="WhUrl">Webhook地址</param>
    ''' <param name="Payload">要传输的数据格式</param>
    ''' <returns>若成功, 则返回True, 否则返回False</returns>
    Public Async Function SendWhAsync(WhUrl As String, Payload As String) As Task(Of Boolean)
        Try
            Using client As New HttpClient()
                '设置请求头
                client.DefaultRequestHeaders.Add("User-Agent", "FurryArtStudio")
                client.DefaultRequestHeaders.Add("Accept", "application/json")
                '构造JSON内容
                Dim content As New StringContent(Payload, Encoding.UTF8, "application/json")
                '发送POST请求
                Dim response As HttpResponseMessage = Await client.PostAsync(WhUrl, content)
                '读取响应
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()
                Return response.IsSuccessStatusCode
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
