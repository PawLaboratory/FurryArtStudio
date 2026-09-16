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
Imports System.Media
Imports System.Text
Imports PawTheme = PawLab.WindowsTheme.ThemeService

Public Class DialogForm
    Implements IThemeChangeable

    Private _dialogType As DialogType
    ''' <summary>
    ''' 构造函数 - 新建一个对话框
    ''' </summary>
    ''' <param name="content">对话框正文内容</param>
    ''' <param name="title">(可选)对话框标题, 若留空则显示为程序集信息</param>
    ''' <param name="mainInstruction">(可选)对话框主操作内容</param>
    ''' <param name="dialogType">(可选)对话框类型, 默认为Info</param>
    ''' <param name="defaultButtonIndex">(可选)默认键, 默认为位号1</param>
    ''' <param name="cancelButtonIndex">(可选)取消键, 默认为位号1</param>
    ''' <param name="button1Text">(可选)按键1文本内容, 默认为OK</param>
    ''' <param name="button2Text">(可选)按键2文本内容</param>
    ''' <param name="button3Text">(可选)按键3文本内容</param>
    ''' <param name="button4Text">(可选)按键4文本内容</param>
    Public Sub New(content As String,
                   Optional title As String = "",
                   Optional mainInstruction As String = "",
                   Optional dialogType As DialogType = DialogType.Info,
                   Optional defaultButtonIndex As Integer = 1,
                   Optional cancelButtonIndex As Integer = 1,
                   Optional button1Text As String = "OK",
                   Optional button2Text As String = "",
                   Optional button3Text As String = "",
                   Optional button4Text As String = "")
        InitializeComponent()
        LblContent.AutoSize = True
        LblMainInstruction.AutoSize = True
        '对话框内容
        LblContent.Text = content
        '对话框标题
        If title = "" Then
            title = Reflection.Assembly.GetExecutingAssembly().GetName().Name
        End If
        Text = title
        '主操作内容
        If mainInstruction = "" Then
            LblMainInstruction.Hide()
            LblContent.Top = 10
            Height = Math.Max(220, LblContent.Height + 145)
        Else
            LblMainInstruction.Text = mainInstruction

            Height = Math.Max(220, LblContent.Height + LblMainInstruction.Height)
        End If
        Width = Math.Max(460, LblContent.Width + 120)
        Width = Math.Max(Width, LblMainInstruction.Width + 240)
        '对话框默认按键
        Select Case defaultButtonIndex
            Case 1
                AcceptButton = Btn1
                Btn1.Select()
            Case 2
                AcceptButton = Btn2
                Btn2.Select()
            Case 3
                AcceptButton = Btn3
                Btn3.Select()
            Case 4
                AcceptButton = Btn4
                Btn4.Select()
            Case Else
                AcceptButton = Btn1
        End Select
        '对话框取消按键
        Select Case cancelButtonIndex
            Case 1
                CancelButton = Btn1
            Case 2
                CancelButton = Btn2
            Case 3
                CancelButton = Btn3
            Case 4
                CancelButton = Btn4
            Case Else
                CancelButton = Btn1
        End Select
        '按钮文本与间距
        Dim btnWidth As Integer = 60

        If button4Text = "" Then
            Btn4.Hide()
            Btn4.Left = 10
        Else
            Btn4.Text = $" {button4Text} "
            btnWidth += Btn4.Width
            Btn4.Left = 100
        End If

        If button3Text = "" Then
            Btn3.Hide()
            Btn3.Left = 25
        Else
            Btn3.Text = $" {button3Text} "
            btnWidth += Btn3.Width
            Btn3.Left = Btn4.Left + Btn4.Width + 15
        End If

        If button2Text = "" Then
            Btn2.Hide()
            Btn2.Left = 40
        Else
            Btn2.Text = $" {button2Text} "
            btnWidth += Btn2.Width
            Btn2.Left = Btn3.Left + Btn3.Width + 15
        End If

        If button1Text = "" Then
            Btn1.Hide()
        Else
            Btn1.Text = $" {button1Text} "
            btnWidth += Btn1.Width
            Btn1.Left = Btn2.Left + Btn2.Width + 15
        End If
        Dim measureX As Integer = btnWidth + 120 - Width
        If measureX < 0 Then
            Btn1.Left -= measureX
            Btn2.Left -= measureX
            Btn3.Left -= measureX
            Btn4.Left -= measureX
        End If
        Width = Math.Max(btnWidth + 120, Width)
        '对话框级别
        _dialogType = dialogType
    End Sub

#Region "基本"
    Public Property ButtonIndex As Integer = 0
    ''' <summary>
    ''' 实现深色主题
    ''' </summary>
    Private Sub SystemThemeChange() Implements IThemeChangeable.SystemThemeChange
        '颜色常量
        Dim bgColor As Color
        Dim frColor As Color
        '获取控件集合
        Dim controlList As List(Of Control) = GetAllControls(Me)
        '判断颜色
        If IsDarkMode() Then
            bgColor = BgColorDark
            frColor = FrColorDark
        Else
            bgColor = BgColorLight
            frColor = FrColorLight
        End If
        For Each control In controlList
            control.ForeColor = frColor
            control.BackColor = bgColor
        Next
        ForeColor = frColor
        BackColor = bgColor
        If IsDarkMode() Then
            LblMainInstruction.ForeColor = Color.FromArgb(100, 120, 255)
        Else
            LblMainInstruction.ForeColor = Color.FromArgb(0, 51, 153)
        End If

        PawTheme.SetWindowTheme(Handle, IsDarkMode) 'PawLab.WindowsTheme
    End Sub
    Private Sub Dialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SystemThemeChange()
        Dim MnuHandle = GetSystemMenu(Handle, False) '获取菜单句柄
        RemoveMenu(MnuHandle, SC_RESTORE, MF_BYCOMMAND) '去除还原菜单
        RemoveMenu(MnuHandle, SC_MAXIMIZE, MF_BYCOMMAND) '去除最大化菜单
        RemoveMenu(MnuHandle, SC_SIZE, MF_BYCOMMAND) '去除大小菜单
        RemoveMenu(MnuHandle, SC_MINIMIZE, MF_BYCOMMAND) '去除最小化菜单
        Me.KeyPreview = True
        '播放系统消息音和设置logo
        PicBox.SizeMode = PictureBoxSizeMode.Zoom
        Select Case _dialogType
            Case DialogType.Info
                SystemSounds.Asterisk.Play()
                PicBox.Image = My.Resources.Icons.DialogInfo
            Case DialogType.Warn
                SystemSounds.Exclamation.Play()
                PicBox.Image = My.Resources.Icons.DialogWarn
            Case DialogType.Error
                SystemSounds.Hand.Play()
                PicBox.Image = My.Resources.Icons.DialogError
            Case Else
                SystemSounds.Asterisk.Play()
                PicBox.Image = My.Resources.Icons.DialogInfo
        End Select
        '设置字体
        LblContent.Font = SystemFonts.MessageBoxFont
        Btn1.Font = SystemFonts.MessageBoxFont
        Btn2.Font = SystemFonts.MessageBoxFont
        Btn3.Font = SystemFonts.MessageBoxFont
        Btn4.Font = SystemFonts.MessageBoxFont
        Dim tf As New Font(SystemFonts.MessageBoxFont.Name, 12)
        LblMainInstruction.Font = tf
        '计算高度
        Dim measureY As Integer = 220 - LblContent.Height + LblMainInstruction.Height
        If measureY < 0 Then
            Height -= (measureY - 140)
        End If
        Btn1.Top = Height - 85
        Btn2.Top = Btn1.Top
        Btn3.Top = Btn1.Top
        Btn4.Top = Btn1.Top
    End Sub
    ''' <summary>
    ''' 对话框类型
    ''' </summary>
    Public Enum DialogType
        ''' <summary>
        ''' 信息
        ''' </summary>
        Info
        ''' <summary>
        ''' 警告
        ''' </summary>
        Warn
        ''' <summary>
        ''' 错误
        ''' </summary>
        [Error]
    End Enum
    ''' <summary>
    ''' 实现快捷键复制对话框内容
    ''' </summary>
    Private Sub DialogForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control And e.KeyCode = Keys.C Then
            Dim sb As New StringBuilder
            sb.Append($"[Window Title]{vbCrLf}{Text}{vbCrLf}{vbCrLf}")
            If LblMainInstruction.Text <> "" Then
                sb.Append($"[Main Instruction]{vbCrLf}{LblMainInstruction.Text}{vbCrLf}{vbCrLf}")
            End If
            sb.Append($"[Content]{vbCrLf}{LblContent.Text}{vbCrLf}{vbCrLf}")
            If Btn4.Visible Then
                sb.Append($"[{Btn4.Text}] ")
            End If
            If Btn3.Visible Then
                sb.Append($"[{Btn3.Text}] ")
            End If
            If Btn2.Visible Then
                sb.Append($"[{Btn2.Text}] ")
            End If
            If Btn1.Visible Then
                sb.Append($"[{Btn1.Text}]")
            End If
            Clipboard.SetDataObject(sb.ToString)
        End If
    End Sub
#End Region

#Region "按钮操作"
    Private Sub Btn1_Click(sender As Object, e As EventArgs) Handles Btn1.Click
        ButtonIndex = 1
        Me.Close()
    End Sub
    Private Sub Btn2_Click(sender As Object, e As EventArgs) Handles Btn2.Click
        _buttonIndex = 2
        Me.Close()
    End Sub
    Private Sub Btn3_Click(sender As Object, e As EventArgs) Handles Btn3.Click
        _buttonIndex = 3
        Me.Close()
    End Sub
    Private Sub Btn4_Click(sender As Object, e As EventArgs) Handles Btn4.Click
        _buttonIndex = 4
        Me.Close()
    End Sub
#End Region

End Class