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
Imports System.Runtime.InteropServices
Imports System.Text
Imports PawTheme = PawLab.WindowsTheme.ThemeService

Public Class DialogForm
    Implements IThemeChangeable

    Private _buttonIndex As Integer
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
        '对话框内容
        LblContent.Text = content
        '对话框标题
        If title = "" Then
            title = Reflection.Assembly.GetExecutingAssembly().GetName().Name
        End If
        Text = title
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
        '按钮文本
        If button1Text = "" Then
            Btn1.Hide()
        Else
            Btn1.Text = button1Text
        End If
        If button2Text = "" Then
            Btn2.Hide()
        Else
            Btn2.Text = button2Text
        End If
        If button3Text = "" Then
            Btn3.Hide()
        Else
            Btn3.Text = button3Text
        End If
        If button4Text = "" Then
            Btn4.Hide()
        Else
            Btn4.Text = button4Text
        End If
        '主操作内容
        If mainInstruction = "" Then
            LblMainInstruction.Hide()
            LblContent.Top = 10
        Else
            LblMainInstruction.Text = mainInstruction
        End If
        '对话框级别
        _dialogType = dialogType
    End Sub

#Region "基本"
    Public ReadOnly Property ButtonIndex As Integer
        Get
            Return _buttonIndex
        End Get
    End Property
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
        _buttonIndex = 1
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
    Private Sub DialogForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If CancelButton Is Btn1 Then
            _buttonIndex = 1
        ElseIf CancelButton Is Btn2 Then
            _buttonIndex = 2
        ElseIf CancelButton Is Btn3 Then
            _buttonIndex = 3
        ElseIf CancelButton Is Btn4 Then
            _buttonIndex = 4
        End If
    End Sub
#End Region

End Class