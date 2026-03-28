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

Module ImageChecker
    '考虑更通用的做法
    Private Declare Auto Function memcmp Lib "msvcrt.dll" (b1 As Byte(), b2 As Byte(), count As IntPtr) As Integer
    Private ReadOnly Signatures As New Dictionary(Of String, Byte()) From {
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
    Public Function IsImageByMIMEType(filePath As String) As Boolean
        '校验给定的路径
        If String.IsNullOrEmpty(filePath) Then Return False
        If Not File.Exists(filePath) Then Return False
        Try
            Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)
                If fs.Length < 2 Then Return False '最小魔数为2
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
            Return False ' 循环未命中，类型不支持或不是图片
        Catch ex As Exception
            '不应当返回False, 应当当抛出异常, 调用者可以根据需要处理
            Throw New IOException($"Can not read file at {filePath}", ex)
        End Try
    End Function
End Module
