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
''' 稿件属性
''' </summary>
Public Class Artwork
    ''' <summary>
    ''' 数据库自增主键
    ''' </summary>
    Public Property ID As Integer
    ''' <summary>
    ''' 唯一通用标识符(UUID)
    ''' </summary>
    Public Property UUID As Guid
    ''' <summary>
    ''' 标题
    ''' </summary>
    Public Property Title As String
    ''' <summary>
    ''' 作者
    ''' </summary>
    Public Property Author As String
    ''' <summary>
    ''' 角色名数组
    ''' </summary>
    Public Property Characters As String()
    ''' <summary>
    ''' 文件创建时间
    ''' </summary>
    Public Property CreateTime As DateTime
    ''' <summary>
    ''' 导入数据库的时间
    ''' </summary>
    Public Property ImportTime As DateTime
    ''' <summary>
    ''' 数据更新时间
    ''' </summary>
    Public Property UpdateTime As DateTime
    ''' <summary>
    ''' 稿件状态(正常/已删除)
    ''' </summary>
    Public Property IsDeleted As Integer
    ''' <summary>
    ''' 稿件标签
    ''' </summary>
    Public Property Tags As String()
    ''' <summary>
    ''' 稿件备注
    ''' </summary>
    Public Property Notes As String
    ''' <summary>
    ''' 稿件对应的文件路径
    ''' </summary>
    Public Property FilePaths As String()
End Class