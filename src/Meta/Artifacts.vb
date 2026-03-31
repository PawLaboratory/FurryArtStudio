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
Imports Ookii.Dialogs.WinForms
''' <summary>
''' 圣遗物
''' 存储那些 Legacy Code
''' </summary>
Public Class Artifacts
    ''' <summary>
    ''' 2026 愚人节代码
    ''' </summary>
    Public Shared Sub AprilFools2026()
        If Now.Month = 4 And Now.Day = 1 Then
            Dim giWishContent As String = "
亲爱的旅行者，「码力全开」活动祈愿现已开启，「系统构筑者·雄龙ztz(逻辑)」概率UP！
活动期间，旅行者可以在活动祈愿中获得更多开发组成员与协作资源，组建强大的研发队伍！
〓祈愿时间〓
2026年4月1日 00:00:00 — 2026年4月1日 23:59:59
〓祈愿介绍〓
● 活动期间，限定5星角色「系统构筑者·雄龙ztz(逻辑)」的祈愿获取概率将大幅提升！
● 活动期间，4星角色「特性塑形者·ra1nyxin(功能)」「潜伏协助者·element115mc(文档)」「补丁编织者·狐小九Little_Jiu(修复)」的祈愿获取概率将大幅提升！
※ 以上角色中，限定角色不会进入「奔行世间」常驻祈愿。
※ 本祈愿属于「角色活动祈愿」，「角色活动祈愿」和「角色活动祈愿-2」的祈愿次数保底完全共享，会一直共同累计在「角色活动祈愿」和「角色活动祈愿-2」中，与其他祈愿的祈愿次数保底相互独立计算，互不影响。
※ 祈愿开启期间，还将开启相应的「码力全开」角色试用活动，旅行者可以使用包含试用角色的固定阵容进入指定的'测试环境'关卡进行体验，挑战成功后即可获得对应奖励（包含'Pull Requests'、'Issues'与'Releases'等）！
※ 更多祈愿信息可点击祈愿界面左下角【详情】按钮进行查询。
"
            Dim buttonInfo As New TaskDialogButton("详情")
            Dim buttonGi As New TaskDialogButton("原神？启动！")
            Using dlg As New TaskDialog With {
            .WindowTitle = My.Resources.FurryArtStudio,
            .MainInstruction = "更新公告",
            .Content = giWishContent,
            .CustomMainIcon = Icon.FromHandle(My.Resources.Icons.RickRollQRCode.GetHicon)
            }
                dlg.Buttons.Add(buttonInfo)
                dlg.Buttons.Add(buttonGi)
                dlg.Buttons.Add(New TaskDialogButton(ButtonType.Ok))
                Dim result As TaskDialogButton = dlg.ShowDialog()
                If result Is buttonInfo Then
                    Process.Start("https://github.com/PawLaboratory/FurryArtStudio")
                End If
                If result Is buttonGi Then
                    Process.Start("https://ys.mihoyo.com/")
                End If
            End Using
        End If
        '藏着么深应该没人注意到吧, 嘻嘻
        '这段代码写的有点乱, 有空重构好了
        '测试了下二维码应该是可以扫描的, 如果扫不了得换更高DPI的显示器
        '应该没人注意到是什么
    End Sub

End Class
