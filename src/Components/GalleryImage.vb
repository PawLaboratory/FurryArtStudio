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

''' <summary>
''' <seealso cref="ImageGallery"/>的图片类型
''' </summary>
Public Class GalleryImage
    Public Property ID As Integer
    Public Property Title As String
    Public Property Thumbnail As Image
    Public Property UUID As String
    Public Property Count As Integer
End Class
''' <summary>
''' 图片墙外观设置枚举
''' </summary>
Public Enum GalleryDisplayMode
    Normal '正常
    Dark '深色模式
    HighContrast '高对比度
End Enum

''' <summary>
''' 处理稿件选中委托事件, 避免幽灵代码的出现
''' </summary>
Public Class SelectionChangedEventArgs
    Inherits EventArgs
    Public Property SelectedImages As IReadOnlyList(Of GalleryImage)
    Public Sub New(images As IReadOnlyList(Of GalleryImage))
        Me.SelectedImages = images
    End Sub
End Class