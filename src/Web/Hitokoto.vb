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
''' 一言实例
''' </summary>
Public Class Hitokoto
    Public Property ID As Integer
    Public Property UUID As Guid
    Public Property Content As String
    Public Property Type As HitokotoType
    Public Property From As String
    Public Property FromWho As String
    Public Property Creator As String
    Public Property CreatorUID As Integer
    Public Property Reviewer As Integer
    Public Property CommitForm As String
    Public Property CreatedAt As DateTime
End Class
''' <summary>
''' 一言类别
''' </summary>
Public Enum HitokotoType
    Anime
    Comic
    Game
    Literature
    Myself
    Internet
    Other
    Video
    Poem
    NCM
    Philosophy
    Funny
End Enum
