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
Imports System.Runtime.InteropServices
Public Class ExportForm
    Implements IThemeChangeable, ILocalizable
    Private _artworkList As List(Of Artwork)
    Private _currentArtwork As Artwork
#Region "初始化"
    ''' <summary>
    ''' 构造函数
    ''' </summary>
    ''' <param name="artworks">要被导出的稿件</param>
    Public Sub New(artworks As List(Of Artwork))
        InitializeComponent()
        _artworkList = artworks
        _currentArtwork = artworks(0)
        PreviewPicturebox.SizeMode = PictureBoxSizeMode.Zoom
        PreviewPicturebox.Image = _currentArtwork.Thumbnail
        TxtName.Text = _currentArtwork.Title & " - " & _currentArtwork.Author
        If _artworkList.Count = 1 Then
            CbOperation.Enabled = False
            CbOperation.Text = "为后续所有稿件执行相同操作"
            Text = "导出稿件"
        Else
            CbOperation.Enabled = True
            CbOperation.Text = String.Format("为后续{0}个稿件执行相同操作", _artworkList.Count - 1)
            Text = String.Format("批量导出稿件 - 剩余{0}个待处理", _artworkList.Count)
        End If
    End Sub
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
            Icon = CreateRoundedRectangleIcon(True, My.Resources.Icons.MenuFileOutputDark)
        Else
            bgColor = BgColorLight
            frColor = FrColorLight
            Icon = CreateRoundedRectangleIcon(False, My.Resources.Icons.MenuFileOutputLight)
        End If
        For Each control In controlList
            control.ForeColor = frColor
            control.BackColor = bgColor
        Next
        ForeColor = frColor
        BackColor = bgColor
        'WinAPI
        DwmSetWindowAttribute(Handle, DwmWindowAttribute.UseImmersiveDarkMode, IsDarkMode(), Marshal.SizeOf(Of Integer))
        SetPreferredAppMode(If(IsDarkMode(), PreferredAppMode.AllowDark, PreferredAppMode.ForceLight))
        FlushMenuThemes()
    End Sub
    Private Sub LanguageChange() Implements ILocalizable.LanguageChange

    End Sub
    Private Sub ExportForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SystemThemeChange()
        Dim MnuHandle = GetSystemMenu(Handle, False) '获取菜单句柄
        RemoveMenu(MnuHandle, SC_RESTORE, MF_BYCOMMAND) '去除还原菜单
        RemoveMenu(MnuHandle, SC_MAXIMIZE, MF_BYCOMMAND) '去除最大化菜单
        RemoveMenu(MnuHandle, SC_SIZE, MF_BYCOMMAND) '去除大小菜单
        RemoveMenu(MnuHandle, SC_MINIMIZE, MF_BYCOMMAND) '去除最小化菜单
        LanguageChange()
    End Sub
#End Region
End Class