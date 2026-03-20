Imports System.Runtime.InteropServices
Imports System.Text
Public Class ColorDialogForm
    Implements IThemeChangeable
    Dim colorLabels() As Label
    Private Sub ColorDialogForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        colorLabels = {L1, L2, L3, L4, L5, L6, L7, L8, L9, L10}
        For Each label In colorLabels '为每个标签添加一个点击复制的事件
            AddHandler label.Click, AddressOf Label_Click
        Next
        Dim MnuHandle = GetSystemMenu(Handle, False) '获取菜单句柄
        RemoveMenu(MnuHandle, SC_RESTORE, MF_BYCOMMAND) '去除还原菜单
        RemoveMenu(MnuHandle, SC_MAXIMIZE, MF_BYCOMMAND) '去除最大化菜单
        RemoveMenu(MnuHandle, SC_SIZE, MF_BYCOMMAND) '去除大小菜单
        RemoveMenu(MnuHandle, SC_MINIMIZE, MF_BYCOMMAND) '去除最小化菜单
        SystemThemeChange()
    End Sub
    Private Sub Label_Click(sender As Object, e As EventArgs)
        Dim lbl As Label = DirectCast(sender, Label)
        If Not String.IsNullOrEmpty(lbl.Text) Then
            Clipboard.SetText(lbl.Text)
        End If
    End Sub
    Private Sub ColorDialogForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control And e.KeyCode = Keys.C Then '当按下 Ctrl+C 时, 复制到剪贴板
            Dim sb As New StringBuilder
            For Each label In colorLabels
                sb.Append(label.Text & vbCrLf)
            Next
            Clipboard.SetText(sb.ToString())
        End If
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub
    Private Sub SystemThemeChange() Implements IThemeChangeable.SystemThemeChange
        If IsDarkMode() Then BackColor = BgColorDark Else BackColor = BgColorLight
        DwmSetWindowAttribute(Handle, DwmWindowAttribute.UseImmersiveDarkMode, IsDarkMode(), Marshal.SizeOf(Of Integer))
        SetPreferredAppMode(If(IsDarkMode(), PreferredAppMode.AllowDark, PreferredAppMode.ForceLight))
        FlushMenuThemes()
    End Sub
End Class