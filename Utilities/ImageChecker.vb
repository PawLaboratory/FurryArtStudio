Imports System.IO

Public Class ImageChecker
    ' 考虑更通用的做法
    Private Declare Auto Function memcmp Lib "msvcrt.dll" (b1 As Byte(), b2 As Byte(), count As IntPtr) As Integer
    Private Shared ReadOnly Signatures As New Dictionary(Of String, Byte()) From {
        {"image/jpeg", New Byte() {&HFF, &HD8, &HFF}},
        {"image/png", New Byte() {&H89, &H50, &H4E, &H47}},
        {"image/gif", New Byte() {&H47, &H49, &H46, &H38}},
        {"image/bmp", New Byte() {&H42, &H4D}},
        {"image/tiff", New Byte() {&H49, &H49, &H2A, &H0}},
        {"image/tiff_be", New Byte() {&H4D, &H4D, &H0, &H2A}},
        {"image/x-icon", New Byte() {&H0, &H0, &H1, &H0}},
        {"image/wmf", New Byte() {&HD7, &HCD, &HC6, &H9A}},
        {"image/emf", New Byte() {&H1, &H0, &H0, &H0}}
    }

    Private Sub New()
    End Sub

    Public Shared Function IsImageByMIMEType(filePath As String) As Boolean
        ' 校验给定的路径
        If String.IsNullOrEmpty(filePath) Then Return False
        If Not File.Exists(filePath) Then Return False
        Try
            Using fs As New IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                If fs.Length < 2 Then Return False ' 最小魔数为2
                Dim buffer(7) As Byte
                fs.Read(buffer, 0, buffer.Length)
                For Each kvp In Signatures
                    Dim sign = kvp.Value
                    Dim reanOnlySlice As New ArraySegment(Of Byte)(buffer, 0, sign.Length)
                    If memcmp(reanOnlySlice.Array, sign, New IntPtr(sign.Length)) = 0 Then
                        Return True
                    End If
                Next
            End Using
        Catch ex As Exception
            ' 不应当返回False，应当当抛出异常，调用者可以根据需要处理
            Throw New IOException($"Can not read file at {filePath}", ex)
        End Try
    End Function

End Class
