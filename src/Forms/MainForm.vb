' FurryArtStudio - 本地稿件管理工具
' Copyright 2026 xionglongztz
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
Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports Krypton.Toolkit
Imports SysThreading = System.Threading
Imports System.Globalization

Public Class MainForm
    Implements IThemeChangeable, ILocalizable
#Region "变量与常量"
    '库
    Private _libraryManager As LibraryManager '当前稿件库管理器实例
    '打印
    Private _currentPrintImages As List(Of String) = Nothing '当前要打印的图片文件路径
    Private _currentPrintIndex As Integer = 0 '当前要打印的图片序号
    Private _artworkCount As Integer = 0 '当前实例稿件总数
    '图片
    Private _imageList As New List(Of Artwork) '图片列表
    Public Event LibraryClosed As EventHandler '定义库关闭事件
    Private _openViewForms As New List(Of ViewForm) '跟踪打开的图片窗口
    '菜单
    Private Const SC_ALWAYSONTOP = 1 '置顶
    Private Const SC_NEWMANUSCRIPT = 2 '新建稿件
    Private Const SC_REFRESH = 3 '刷新
    Private Const SC_PLAY = 4 '幻灯片放映
    Private Const SC_SETTINGS = 5 '选项
    Private Const SC_STATISTICS = 6 '统计信息
    Private Const SC_ABOUT = 7 '关于
    '用于主题消息变更消抖
    Private WithEvents _themeDebounceTimer As New Timer With {
    .Interval = 300
    }
#End Region

#Region "窗体事件处理"

    ''' <summary>
    ''' 程序启动时调用
    ''' </summary>
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StatusLabel.Text = My.Resources.Stat_Init '正在初始化
        _libraryManager = LibraryManager.Instance '启动稿件库管理器实例
        MenuInit() '初始化菜单
        ResizeControl() '调整组件尺寸
        SysMenuInit() '设置系统菜单
        Dim titleFont As New Font(LblTitle.Font, FontStyle.Bold)
        LblTitle.Font = titleFont
        If IsAdmin() Then MnuRunAsElevated.Enabled = False
#If DEBUG Then
        MnuDevTools.Visible = True '显示并启用开发者工具选项
        MnuDevTools.Enabled = True
        MnuRunAsElevated.Visible = True
#Else
        MnuDevTools.Enabled = False
#End If
        Dim autoChangeLang As Boolean = IsFirstRun() '判断程序是否首次启动, 并存储状态
        Dim settings = AppSettings.Load() '读取设置项
        If autoChangeLang Then
            Dim systemCulture = CultureInfo.InstalledUICulture '检测当前系统语言, 并自动设置
            Select Case systemCulture.Name '下次重构时, 考虑改为单独的函数
                Case "zh-CN", "zh-SG"
                    settings.Appearance.Language = AppSettings.LanguageOption.ChineseSimplified
                Case "zh-TW", "zh-HK", "zh-MO"
                    settings.Appearance.Language = AppSettings.LanguageOption.ChineseTraditional
                Case Else
                    settings.Appearance.Language = AppSettings.LanguageOption.English
            End Select
        End If
        Select Case settings.Appearance.Language
            Case AppSettings.LanguageOption.English
                SysThreading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
            Case AppSettings.LanguageOption.ChineseSimplified
                SysThreading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("zh-Hans")
            Case AppSettings.LanguageOption.ChineseTraditional
                SysThreading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("zh-Hant")
            Case Else '回退
                SysThreading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        End Select
        UpdateFormLang() '更新语言
        SystemThemeChange() '设置主题
        SetTitleBarColor(Handle, IconColorLight) '修改标题栏颜色(win11生效)
        Icon = Icon.FromHandle(My.Resources.Icons.FurryArtStudio.GetHicon)
        settings.Save() '保存默认设置
        StatusLabel.Text = My.Resources.Stat_Ready '就绪
    End Sub

    ''' <summary>
    ''' 关闭时释放资源
    ''' </summary>
    Private Sub MainForm_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
        CloseLibrary()
        LibraryManager.CloseAllLibrary() '关闭时释放资源
    End Sub

    ''' <summary>
    ''' 窗体消息处理
    ''' </summary>
    ''' <param name="m">Windows消息</param>
    Protected Overrides Sub WndProc(ByRef m As Message) '窗体消息处理函数
        If m.Msg = WM_SYSCOMMAND Then '窗体响应菜单
            Dim hMenu = GetSystemMenu(Handle, False)
            Select Case m.WParam.ToInt32'对应菜单标号
                Case SC_ALWAYSONTOP '窗口置顶
                    If TopMost = False Then
                        TopMost = True
                        CheckMenuItem(hMenu, SC_ALWAYSONTOP, MF_CHECKED) '窗口置顶
                        MnuOnTop.Checked = True
                    Else
                        TopMost = False
                        CheckMenuItem(hMenu, SC_ALWAYSONTOP, MF_UNCHECKED) '取消置顶
                        MnuOnTop.Checked = False
                    End If
                Case SC_NEWMANUSCRIPT
                    NewManuscript()
                Case SC_REFRESH
                    RefreshLib()
                Case SC_PLAY
                    '待开发
                Case SC_SETTINGS
                    StatusLabel.Text = My.Resources.Stat_OpenProp
                    PropertiesForm.ShowDialog()
                    StatusLabel.Text = My.Resources.Stat_Ready
                Case SC_STATISTICS
                    ShowLibStatistics()
                Case SC_ABOUT
                    AboutForm.ShowDialog()
            End Select
        End If
        If m.Msg = WM_DWMCOLORIZATIONCOLORCHANGED Then '主题发生改变时
            Dim settings = AppSettings.Load()
            If settings.Appearance.Theme = AppSettings.ThemeMode.FollowSystem Then
                _themeDebounceTimer.Stop()
                _themeDebounceTimer.Start() '消抖
            End If
        End If
        MyBase.WndProc(m) '循环监听消息
    End Sub
    Private Sub _themeDebounceTimer_Tick() Handles _themeDebounceTimer.Tick
        _themeDebounceTimer.Stop()
        BeginInvoke(New MethodInvoker(Sub()
                                          Dim settings = AppSettings.Load()
                                          If settings.Appearance.Theme = AppSettings.ThemeMode.FollowSystem Then
                                              UpdateFormTheme() '当主题设置为跟随系统时,主题变更通过接口发送给全部窗体
                                          End If
                                      End Sub))
    End Sub

    ''' <summary>
    ''' 分割线调整时触发
    ''' </summary>
    Private Sub ArtworkListSplitContainer_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles ArtworkListSplitContainer.SplitterMoved
        ResizeControl()
    End Sub

    ''' <summary>
    ''' 调整组件的大小
    ''' </summary>
    Private Sub ResizeControl()
        Dim p2Width = ArtworkListSplitContainer.Panel2.Width - 10
        PiChkThumb.Height = PiChkThumb.Width '保持为方形
    End Sub

#End Region

#Region "辅助方法"
    ''' <summary>
    ''' 语言变更
    ''' </summary>
    Private Sub LanguageChange() Implements ILocalizable.LanguageChange
        '文件
        MnuFile.Text = My.Resources.Mnu_File
        MnuOnTop.Text = My.Resources.Mnu_AlwaysOnTop
        MnuDevTools.Text = My.Resources.Mnu_DevTools
        MnuRunAsElevated.Text = My.Resources.Mnu_RunAsElevated
        MnuRunTerminal.Text = My.Resources.Mnu_OpenTerminal
        MnuProperties.Text = My.Resources.Mnu_Options
        MnuExit.Text = My.Resources.Mnu_Exit
        '稿件库
        MnuLibrary.Text = My.Resources.Mnu_Lib
        MnuLibList.Text = My.Resources.Mnu_CurrentLib
        MnuLibRefresh.Text = My.Resources.Mnu_Refresh
        MnuLibNew.Text = My.Resources.Mnu_New
        MnuLibImport.Text = My.Resources.Mnu_Import
        MnuLibExport.Text = My.Resources.Mnu_Export
        MnuLibExportCSV.Text = My.Resources.Mnu_ExportCSV
        MnuLibClone.Text = My.Resources.Mnu_Clone
        MnuLibOpenFolder.Text = My.Resources.Mnu_OpenPath
        MnuLibCopy.Text = My.Resources.Mnu_Copy
        MnuLibCopyPath.Text = My.Resources.Mnu_CopyPath
        MnuLibClose.Text = My.Resources.Mnu_Close
        MnuLibRename.Text = My.Resources.Mnu_Rename
        MnuLibDelete.Text = My.Resources.Mnu_Delete
        MnuLibStatistics.Text = My.Resources.Mnu_Properties
        '稿件
        MnuManuscript.Text = My.Resources.Mnu_Ms
        MnuMsNew.Text = My.Resources.Mnu_New
        MnuMsImport.Text = My.Resources.Mnu_Import
        MnuMsView.Text = My.Resources.Mnu_View
        MnuMsEdit.Text = My.Resources.Mnu_Edit
        MnuMsExport.Text = My.Resources.Mnu_ExportMs
        MnuMsPrint.Text = My.Resources.Mnu_Print
        MnuMsDelete.Text = My.Resources.Mnu_Delete
        MnuMsOpenFolder.Text = My.Resources.Mnu_OpenPath
        MnuMsCopy.Text = My.Resources.Mnu_Copy
        MnuMsCopyPath.Text = My.Resources.Mnu_CopyPath
        '视图
        MnuViews.Text = My.Resources.Mnu_Views
        MnuViewPlay.Text = My.Resources.Mnu_Play
        MnuSelectAll.Text = My.Resources.Mnu_SelectAll
        MnuSelectReverse.Text = My.Resources.Mnu_SelectReverse
        MnuSearch.Text = My.Resources.Mnu_Search
        MnuAdvancedSearch.Text = My.Resources.Mnu_AdvancedSearch
        MnuPageUp.Text = My.Resources.Mnu_Prev
        MnuPageDown.Text = My.Resources.Mnu_Next
        '帮助
        MnuHelp.Text = My.Resources.Mnu_Help
        MnuHelpTutorial.Text = My.Resources.Mnu_Tutorial
        MnuHelpWebsite.Text = My.Resources.Mnu_Website
        MnuHelpGithub.Text = My.Resources.Mnu_GitHub
        MnuHelpWhatsNew.Text = My.Resources.Mnu_WhatsNew
        MnuCheckUpdate.Text = My.Resources.Mnu_CheckUpdate
        MnuHelpLicense.Text = My.Resources.Mnu_License
        MnuHelpPrivacy.Text = My.Resources.Mnu_Privacy
        MnuTerms.Text = My.Resources.Mnu_Terms
        MnuHelpAbout.Text = My.Resources.Mnu_About
        '窗体
        UpdateMenuItem()
        Dim settings = AppSettings.Load()
        If settings.Appearance.MenuUppercase Then
            MnuFile.Text = MnuFile.Text.ToUpper
            MnuLibrary.Text = MnuLibrary.Text.ToUpper
            MnuManuscript.Text = MnuManuscript.Text.ToUpper
            MnuViews.Text = MnuViews.Text.ToUpper
            MnuHelp.Text = MnuHelp.Text.ToUpper
        End If
    End Sub

    ''' <summary>
    ''' 系统主题发生变化时调用以更新
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
            ImageGalleryMain.DisplayMode = GalleryDisplayMode.Dark '设置图片墙主题
            KryptonMgrMain.GlobalPaletteMode = PaletteMode.MaterialDark '设置菜单栏主题
            InitializeMenuImages(True) '设置菜单图标主题
        Else
            bgColor = BgColorLight
            frColor = FrColorLight
            ImageGalleryMain.DisplayMode = GalleryDisplayMode.Normal
            KryptonMgrMain.GlobalPaletteMode = PaletteMode.MaterialLight
            InitializeMenuImages()
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

    ''' <summary>
    ''' 初始化菜单图标
    ''' </summary>
    ''' <param name="isDarkMode">(可选)设置深色主题, 默认为浅色</param>
    Private Sub InitializeMenuImages(Optional isDarkMode As Boolean = False)
        Dim menuIcons As New List(Of (MenuItem As ToolStripMenuItem, BaseName As String)) From
    {
        (MnuOnTop, "MenuPin"),
        (MnuDevTools, "MenuDevTools"),
        (MnuRunAsElevated, "MenuShield"),
        (MnuRunTerminal, "MenuTerminal"),
        (MnuProperties, "MenuSettings"),
        (MnuExit, "MenuClose"),
        (MnuLibList, "MenuFolders"),
        (MnuLibRefresh, "MenuRefresh"),
        (MnuLibNew, "MenuFolderNew"),
        (MnuLibImport, "MenuFolderInput"),
        (MnuLibExport, "MenuFolderOutput"),
        (MnuLibExportCSV, "MenuExportCsv"),
        (MnuLibClone, "MenuClone"),
        (MnuLibOpenFolder, "MenuFolderOpen"),
        (MnuLibCopy, "MenuCopy"),
        (MnuLibClose, "MenuFolderClose"),
        (MnuLibRename, "MenuFolderEdit"),
        (MnuLibDelete, "MenuFolderDel"),
        (MnuLibStatistics, "MenuProperties"),
        (MnuMsNew, "MenuFileNew"),
        (MnuMsImport, "MenuFileInput"),
        (MnuMsView, "MenuView"),
        (MnuMsEdit, "MenuEdit"),
        (MnuMsExport, "MenuFileOutput"),
        (MnuMsPrint, "MenuPrint"),
        (MnuMsDelete, "MenuDelete"),
        (MnuMsOpenFolder, "MenuFolderOpen"),
        (MnuMsCopy, "MenuCopy"),
        (MnuViewPlay, "MenuImagePlay"),
        (MnuSearch, "MenuSearch"),
        (MnuPageUp, "MenuPrevious"),
        (MnuPageDown, "MenuNext"),
        (MnuHelpTutorial, "MenuTutorial"),
        (MnuHelpGithub, "MenuGithub"),
        (MnuHelpAbout, "MenuInfo"),
        (MnuHelpWhatsNew, "MenuStar"),
        (ConMnuMsView, "MenuView"),
        (ConMnuMsEdit, "MenuEdit"),
        (ConMnuMsExport, "MenuFileOutput"),
        (ConMnuMsPrint, "MenuPrint"),
        (ConMnuMsDelete, "MenuDelete"),
        (ConMnuMsOpenFolder, "MenuFolderOpen"),
        (ConMnuMsCopy, "MenuCopy")
    }
        For Each setting In menuIcons
            Dim resourceName = setting.BaseName & If(isDarkMode, "Dark", "Light")
            Using img As Image = DirectCast(My.Resources.Icons.ResourceManager.GetObject(resourceName), Image)
                Dim size As Integer = 32 '图标尺寸
                Using thumb As New Bitmap(size, size)
                    Using g = Graphics.FromImage(thumb)
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic
                        g.SmoothingMode = SmoothingMode.HighQuality
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality
                        g.CompositingQuality = CompositingQuality.HighQuality
                        g.DrawImage(img, 0, 0, size, size)
                    End Using
                    setting.MenuItem.Image = thumb.Clone '使用高质量缩略图
                End Using

            End Using
        Next
        Dim menuHandle = GetSystemMenu(Handle, False) '设置窗体菜单
        If isDarkMode Then
            ApplyMenuIcon(menuHandle, SC_ALWAYSONTOP, My.Resources.Icons.MenuPinDark, True)
            ApplyMenuIcon(menuHandle, SC_NEWMANUSCRIPT, My.Resources.Icons.MenuFileNewDark, True)
            ApplyMenuIcon(menuHandle, SC_REFRESH, My.Resources.Icons.MenuRefreshDark, True)
            ApplyMenuIcon(menuHandle, SC_PLAY, My.Resources.Icons.MenuImagePlayDark, True)
            ApplyMenuIcon(menuHandle, SC_SETTINGS, My.Resources.Icons.MenuSettingsDark, True)
            ApplyMenuIcon(menuHandle, SC_STATISTICS, My.Resources.Icons.MenuPropertiesDark, True)
            ApplyMenuIcon(menuHandle, SC_ABOUT, My.Resources.Icons.MenuInfoDark, True)
        Else
            ApplyMenuIcon(menuHandle, SC_ALWAYSONTOP, My.Resources.Icons.MenuPinLight)
            ApplyMenuIcon(menuHandle, SC_NEWMANUSCRIPT, My.Resources.Icons.MenuFileNewLight)
            ApplyMenuIcon(menuHandle, SC_REFRESH, My.Resources.Icons.MenuRefreshLight)
            ApplyMenuIcon(menuHandle, SC_PLAY, My.Resources.Icons.MenuImagePlayLight)
            ApplyMenuIcon(menuHandle, SC_SETTINGS, My.Resources.Icons.MenuSettingsLight)
            ApplyMenuIcon(menuHandle, SC_STATISTICS, My.Resources.Icons.MenuPropertiesLight)
            ApplyMenuIcon(menuHandle, SC_ABOUT, My.Resources.Icons.MenuInfoLight)
        End If
    End Sub

    ''' <summary>
    ''' 初始化菜单项
    ''' </summary>
    Private Sub MenuInit()
        Text = "FurryArtStudio"
        MnuLibOpenFolder.Enabled = False
        MnuLibCopy.Enabled = False
        MnuLibCopyPath.Enabled = False
        MnuLibClone.Enabled = False
        MnuLibExport.Enabled = False
        MnuLibClose.Enabled = False
        MnuLibRename.Enabled = False
        MnuLibDelete.Enabled = False
        MnuLibStatistics.Enabled = False
        MnuMsNew.Enabled = False
        MnuMsImport.Enabled = False
        MnuMsView.Enabled = False
        MnuMsEdit.Enabled = False
        MnuMsDelete.Enabled = False
        MnuMsExport.Enabled = False
        MnuMsPrint.Enabled = False
        MnuMsOpenFolder.Enabled = False
        MnuMsCopy.Enabled = False
        MnuMsCopyPath.Enabled = False
        MnuAdvancedSearch.Enabled = False
        MnuSelectAll.Enabled = False
        MnuSelectReverse.Enabled = False
        MnuViewPlay.Enabled = False
        MnuPageDown.Enabled = False
        MnuPageUp.Enabled = False
        MnuSearch.Enabled = False
        ConMnuMsView.Enabled = False
        ConMnuMsEdit.Enabled = False
        ConMnuMsDelete.Enabled = False
        ConMnuMsExport.Enabled = False
        ConMnuMsPrint.Enabled = False
        ConMnuMsOpenFolder.Enabled = False
        ConMnuMsCopy.Enabled = False
        ConMnuMsCopyPath.Enabled = False
        RefreshLibListMenu()
        ImageGalleryMain.ClearImages() '清空所有图片
        If _imageList.Count <> 0 Then _imageList.Clear()
        PiChkThumb.Image = Nothing
        LblTitle.Text = ""
        LblAuthor.Text = ""
        LblTags.Text = ""
        LblCharacters.Text = ""
        LblNotes.Text = ""
        SearchTextBox.Enabled = False
        SearchTextBox.Text = ""
        StatusLabel.Text = My.Resources.Stat_Ready
        ArtworkStatusLabel.Text = My.Resources.Main_LblNoLib
        _artworkCount = 0
        SelectStatusLabel.Text = My.Resources.Main_LblNoMs
        StorageStatusLabel.Text = My.Resources.Main_LblNoStorage
        PageStatusLabel.Text = My.Resources.Main_LblNoPage
        PageStatusLabel.Visible = False
        TSSep2.Visible = False
        TSSep1.Visible = False
        SearchStatusLabel.Visible = False
        MnuLibExportCSV.Enabled = False
        Dim menuHandle = GetSystemMenu(Handle, False) '获取菜单句柄
        EnableMenuItem(menuHandle, SC_NEWMANUSCRIPT, MF_GRAYED)
        EnableMenuItem(menuHandle, SC_PLAY, MF_GRAYED)
        EnableMenuItem(menuHandle, SC_STATISTICS, MF_GRAYED)
        GC.Collect()
    End Sub

    ''' <summary>
    ''' 初始化系统菜单
    ''' </summary>
    Private Sub SysMenuInit()
        Dim menuHandle = GetSystemMenu(Handle, False) '获取菜单句柄
        '添加新菜单
        InsertMenu(menuHandle, 0, MF_BYPOSITION Or MF_STRING, SC_ALWAYSONTOP, My.Resources.Mnu_AlwaysOnTop)
        InsertMenu(menuHandle, 1, MF_BYPOSITION Or MF_SEPARATOR, 0, Nothing)
        InsertMenu(menuHandle, 8, MF_BYPOSITION Or MF_STRING, SC_NEWMANUSCRIPT, My.Resources.Mnu_NewMs)
        InsertMenu(menuHandle, 9, MF_BYPOSITION Or MF_STRING, SC_REFRESH, My.Resources.Mnu_Refresh)
        InsertMenu(menuHandle, 10, MF_BYPOSITION Or MF_STRING, SC_PLAY, My.Resources.Mnu_Play)
        InsertMenu(menuHandle, 11, MF_BYPOSITION Or MF_SEPARATOR, 0, Nothing)
        InsertMenu(menuHandle, 12, MF_BYPOSITION Or MF_STRING, SC_SETTINGS, My.Resources.Mnu_Options)
        InsertMenu(menuHandle, 13, MF_BYPOSITION Or MF_STRING, SC_STATISTICS, My.Resources.Mnu_Properties)
        InsertMenu(menuHandle, 14, MF_BYPOSITION Or MF_STRING, SC_ABOUT, My.Resources.Mnu_About)
        InsertMenu(menuHandle, 15, MF_BYPOSITION Or MF_SEPARATOR, 0, Nothing)
        '设置菜单快捷键
        UpdateMenuItem()
        '设置菜单可用状态
        EnableMenuItem(menuHandle, SC_NEWMANUSCRIPT, MF_GRAYED)
        EnableMenuItem(menuHandle, SC_PLAY, MF_GRAYED)
        EnableMenuItem(menuHandle, SC_STATISTICS, MF_GRAYED)
    End Sub

    ''' <summary>
    ''' 更新窗体菜单项
    ''' </summary>
    Private Sub UpdateMenuItem()
        Dim menuHandle = GetSystemMenu(Handle, False) '获取菜单句柄
        SetMenuItemWithShortcut(menuHandle, 0, SC_ALWAYSONTOP, My.Resources.Mnu_AlwaysOnTop, "Alt+T")
        SetMenuItemWithShortcut(menuHandle, 8, SC_NEWMANUSCRIPT, My.Resources.Mnu_NewMs, "Ctrl+N")
        SetMenuItemWithShortcut(menuHandle, 9, SC_REFRESH, My.Resources.Mnu_Refresh, "F5")
        SetMenuItemWithShortcut(menuHandle, 10, SC_PLAY, My.Resources.Mnu_Play, "Ctrl+F5")
        SetMenuItemWithShortcut(menuHandle, 12, SC_SETTINGS, My.Resources.Mnu_Options, "Ctrl+K")
        SetMenuItemWithShortcut(menuHandle, 13, SC_STATISTICS, My.Resources.Mnu_Properties, "Alt+I")
        SetMenuItemWithShortcut(menuHandle, 14, SC_ABOUT, My.Resources.Mnu_About, "Ctrl+F1")
    End Sub

    ''' <summary>
    ''' 载入数据并设置图片墙
    ''' </summary>
    Private Sub LoadArtworks()
        Text = $"{_libraryManager.GetCurrentLibrary.LibraryName} - FurryArtStudio"
        StatusLabel.Text = My.Resources.Stat_Loading
        ArtworkListSplitContainer.UseWaitCursor = True
        '设置菜单
        MnuLibOpenFolder.Enabled = True
        MnuLibCopy.Enabled = True
        MnuLibCopyPath.Enabled = True
        MnuLibClone.Enabled = True
        MnuLibExport.Enabled = True
        MnuLibClose.Enabled = True
        MnuLibRename.Enabled = True
        MnuLibDelete.Enabled = True
        MnuLibStatistics.Enabled = True
        MnuMsNew.Enabled = True
        MnuMsImport.Enabled = True
        MnuAdvancedSearch.Enabled = True
        MnuSelectAll.Enabled = True
        MnuSelectReverse.Enabled = True
        MnuViewPlay.Enabled = True
        PageStatusLabel.Visible = True
        TSSep2.Visible = True
        MnuLibExportCSV.Enabled = True
        MnuSearch.Enabled = True
        Dim menuHandle = GetSystemMenu(Handle, False) '获取菜单句柄
        EnableMenuItem(menuHandle, SC_NEWMANUSCRIPT, MF_ENABLED)
        EnableMenuItem(menuHandle, SC_PLAY, MF_ENABLED)
        EnableMenuItem(menuHandle, SC_STATISTICS, MF_ENABLED)
        '设置UI
        LblTitle.Text = My.Resources.Main_LblNoSelect
        LblAuthor.Text = ""
        LblTags.Text = ""
        LblCharacters.Text = ""
        LblNotes.Text = ""
        PiChkThumb.Image = Nothing
        SearchTextBox.Enabled = True
        '设置图片墙
        ImageGalleryMain.ClearImages() '清空所有图片
        ArtworkStatusLabel.Text = String.Format(My.Resources.Main_LblLibName,
                                                _libraryManager.GetCurrentLibrary.LibraryName) '稿件库名称
        Dim artworks = _libraryManager.GetCurrentLibrary.GetAllArtworksComplete '获取当前库全部数据
        SelectStatusLabel.Text = String.Format(My.Resources.Main_LblMs, artworks.Count) '稿件数量
        '遍历所有稿件
        Dim libraryPath = _libraryManager.GetCurrentLibrary.LibraryPath
        SetGallery(artworks, libraryPath) '载入稿件
        _artworkCount = artworks.Count
        _imageList = artworks '用作预览器参数
        _imageList.Reverse() '反转顺序
        '设置状态栏
        Dim result = GetFolderInfo(libraryPath)
        StorageStatusLabel.Text = String.Format(My.Resources.Main_LblStorage,
                                                result.sizeString, result.fileCount)
        Dim page As Integer = Math.Ceiling(_artworkCount / ImageGalleryMain.PageSize)
        MnuPageDown.Enabled = page > 1
        PageStatusLabel.Text = String.Format(My.Resources.Main_LblPage1, page) '在初始化阶段暂时获得不到准确的页码
        ArtworkListSplitContainer.UseWaitCursor = False
        StatusLabel.Text = My.Resources.Stat_Ready
    End Sub

    ''' <summary>
    ''' 设置图片墙显示内容
    ''' </summary>
    Private Sub SetGallery(artworks As List(Of Artwork), libraryPath As String)
        For Each artwork In artworks
            Dim artworkPath As String = Path.Combine(libraryPath, artwork.UUID.ToString())
            If Not Directory.Exists(artworkPath) Then '保证文件夹存在
                Directory.CreateDirectory(artworkPath)
            End If
            Dim previewPath As String = Path.Combine(artworkPath, ".preview.jpg")
            If Not File.Exists(previewPath) And Directory.GetFiles(artworkPath).Count > 0 Then '保证缩略图存在
                Using img As Image = LoadImageFromFile(Directory.GetFiles(artworkPath)(0))
                    If IsNothing(img) Then
                        ' 跳过，不处理
                        Continue For
                    End If
                    img.Save(previewPath, ImageFormat.Jpeg)
                End Using
            End If
            Dim fileCount As Integer = artwork.FilePaths.Length
            If File.Exists(previewPath) Then '去除缩略图
                fileCount -= 1
            End If
            Dim gi As New GalleryImage With {
                .Title = artwork.Title,
                .UUID = artwork.UUID.ToString,
                .ID = artwork.ID,
                .Count = fileCount
            }
            If File.Exists(previewPath) Then
                Try
                    Using sourceImage As Image = Image.FromFile(previewPath)
                        '创建独立于文件的新位图
                        gi.Thumbnail = New Bitmap(sourceImage.Width, sourceImage.Height, sourceImage.PixelFormat)
                        Using g As Graphics = Graphics.FromImage(gi.Thumbnail)
                            g.DrawImage(sourceImage, 0, 0, sourceImage.Width, sourceImage.Height)
                        End Using
                    End Using
                Catch ex As Exception
                    '忽略无法加载的缩略图
                End Try
            End If
            ImageGalleryMain.AddImage(gi)
        Next
    End Sub

    ''' <summary>
    ''' 查看属性信息
    ''' </summary>
    Private Sub StorageStatusLabel_Click(sender As Object, e As EventArgs) Handles StorageStatusLabel.Click
        MnuLibStatistics.PerformClick()
    End Sub

    ''' <summary>
    ''' 搜索内容时触发
    ''' </summary>
    Private Sub SearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles SearchTextBox.TextChanged
        SearchArtwork()
    End Sub

    ''' <summary>
    ''' 搜索稿件
    ''' </summary>
    Private Sub SearchArtwork()
        StatusLabel.Text = My.Resources.Stat_Searching
        ClearSelect()
        If SearchTextBox.Text = "" Then
            RefreshLib()
            TSSep1.Visible = False
            SearchStatusLabel.Visible = False
            If _libraryManager.GetCurrentLibrary IsNot Nothing Then Text = $"{_libraryManager.GetCurrentLibrary.LibraryName} - FurryArtStudio"
        Else
            ImageGalleryMain.ClearImages()
            Dim resultArtwork As List(Of Artwork) = _libraryManager.GetCurrentLibrary.SearchArtworks(SearchTextBox.Text)
            Dim libraryPath = _libraryManager.GetCurrentLibrary.LibraryPath
            SetGallery(resultArtwork, libraryPath) '载入稿件
            TSSep1.Visible = True
            SearchStatusLabel.Visible = True
            SearchStatusLabel.Text = String.Format(My.Resources.Main_LblSearchedMs, ImageGalleryMain.TotalImageCount) '已搜到的图片数量
            Dim page As Integer = Math.Ceiling(ImageGalleryMain.TotalImageCount / ImageGalleryMain.PageSize)
            MnuPageDown.Enabled = page > 1
            PageStatusLabel.Text = String.Format(My.Resources.Main_LblPage1, page) '在初始化阶段暂时获得不到准确的页码
            SelectStatusLabel.Text = String.Format(My.Resources.Main_LblMs, _artworkCount) '稿件总数量
            Text = $"{ImageGalleryMain.TotalImageCount}个稿件 - {_libraryManager.GetCurrentLibrary.LibraryName} - FurryArtStudio"
        End If
        StatusLabel.Text = My.Resources.Stat_Ready
    End Sub
#End Region

#Region "菜单项"

#Region "文件菜单项"
    Private Sub MnuOnTop_Click(sender As Object, e As EventArgs) Handles MnuOnTop.Click
        Dim hMenu = GetSystemMenu(Handle, False)

        If MnuOnTop.Checked = False Then
            MnuOnTop.Checked = True
            TopMost = True
            CheckMenuItem(hMenu, 1, MF_CHECKED) '窗口置顶
        Else
            MnuOnTop.Checked = False
            TopMost = False
            CheckMenuItem(hMenu, 1, MF_UNCHECKED) '取消置顶
        End If
    End Sub
    Private Sub MnuDevTools_Click(sender As Object, e As EventArgs) Handles MnuDevTools.Click
        DevToolsForm.Show()
    End Sub
    Private Sub MnuRunAsElevated_Click(sender As Object, e As EventArgs) Handles MnuRunAsElevated.Click
        Dim startInfo As New ProcessStartInfo With {
            .UseShellExecute = True, '必须设置为True才能使用Verb
            .Verb = "runas", '请求管理员权限
            .FileName = Application.ExecutablePath
        }
        Try
            Process.Start(startInfo)
            Me.Close()
        Catch ex As Win32Exception
            MessageBox.Show(String.Format(My.Resources.Msg_ElevatedFailed, ex.Message), My.Resources.FurryArtStudio, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Private Sub MnuRunTerminal_Click(sender As Object, e As EventArgs) Handles MnuRunTerminal.Click
        Dim startPath As String
        Try
            If _libraryManager.GetCurrentLibrary IsNot Nothing Then
                startPath = _libraryManager.GetCurrentLibrary.LibraryPath
            Else
                startPath = AppDomain.CurrentDomain.BaseDirectory
            End If
            Dim psi As New ProcessStartInfo With {
                .FileName = "cmd.exe",
                .Arguments = $"/K cd /D {startPath}",
                .UseShellExecute = False,
                .CreateNoWindow = False
            }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show(String.Format(My.Resources.Msg_TerminalFailed, ex.Message), My.Resources.FurryArtStudio, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Private Sub MnuProperties_Click(sender As Object, e As EventArgs) Handles MnuProperties.Click
        StatusLabel.Text = My.Resources.Stat_OpenProp
        PropertiesForm.ShowDialog()
        StatusLabel.Text = My.Resources.Stat_Ready
    End Sub
    Private Sub MnuExit_Click(sender As Object, e As EventArgs) Handles MnuExit.Click
        Me.Close()
    End Sub
#End Region

#Region "稿件库菜单项"
    ''' <summary>
    ''' 刷新当前库菜单列表
    ''' </summary>
    Private Sub RefreshLibListMenu()
        MnuLibList.DropDownItems.Clear()
        Dim artworksPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Artworks")
        If Not Directory.Exists(artworksPath) Then Directory.CreateDirectory(artworksPath) '检查 Artworks 文件夹是否存在
        Dim libraryFolders = Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Artworks"))
        If libraryFolders.Length = 0 Then
            Dim artworkMenuItem As New ToolStripMenuItem() With {
                .Text = My.Resources.Main_LblNoLib,
                .Enabled = False
            }
            MnuLibList.DropDownItems.Add(artworkMenuItem)
            Return
        End If
        Dim menuCount As Integer = 0
        For Each foldername In libraryFolders '将稿件库文件夹添加到'当前库'菜单中
            menuCount += 1
            Dim artworkMenuItem As New ToolStripMenuItem With {
                .Tag = Path.GetFileName(foldername)
            }
            artworkMenuItem.Text = $"&{menuCount}. {artworkMenuItem.Tag}"
            If menuCount < 9 Then
                artworkMenuItem.ShortcutKeys = Keys.Alt + Keys.D0 + menuCount
            End If
            MnuLibList.DropDownItems.Add(artworkMenuItem)
            AddHandler artworkMenuItem.Click, AddressOf OnArtworkMenuClick
        Next
    End Sub
    ''' <summary>
    ''' 处理新增的稿件库菜单项
    ''' </summary>
    Private Sub OnArtworkMenuClick(sender As Object, e As EventArgs)
        Dim menuItem As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
        If menuItem IsNot Nothing Then
            Dim foldername As String = TryCast(menuItem.Tag, String)
            If Not String.IsNullOrEmpty(foldername) Then
                _libraryManager.AddLibrary(foldername) '新增并切换到稿件库
                _libraryManager.SwitchLibrary(foldername)
                LoadArtworks() '重新载入稿件库
            End If
        End If
    End Sub
    Private Sub MnuLibNew_Click(sender As Object, e As EventArgs) Handles MnuLibNew.Click
        Using inputForm As New InputDialogForm With {
            .Text = My.Resources.Input_NewLibTitle
        }
            inputForm.InputTxtbox.Text = My.Resources.Input_TxtNewLib
            StatusLabel.Text = My.Resources.Stat_CreatingLib
            If inputForm.ShowDialog() = DialogResult.OK Then '显示对话框并获取结果
                Dim newLib As String = inputForm.InputValue
                _libraryManager.AddLibrary(newLib)
                _libraryManager.SwitchLibrary(newLib)
                RefreshLib() '刷新
            Else
            End If
            StatusLabel.Text = My.Resources.Stat_Ready
        End Using
    End Sub
    Private Sub MnuLibImport_Click(sender As Object, e As EventArgs) Handles MnuLibImport.Click
        Dim pawFileDlg As New OpenFileDialog()
    End Sub
    Private Sub MnuLibExport_Click(sender As Object, e As EventArgs) Handles MnuLibExport.Click

    End Sub
    Private Sub MnuLibExportCSV_Click(sender As Object, e As EventArgs) Handles MnuLibExportCSV.Click
        StatusLabel.Text = My.Resources.Stat_ExportCSV
        Using saveFileDialog As New SaveFileDialog() With {
            .Filter = My.Resources.Main_FileFilterCSV,
            .FileName = $"{_libraryManager.GetCurrentLibrary.LibraryName}.csv"
            }
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                _libraryManager.GetCurrentLibrary.ExportTableToCSV(saveFileDialog.FileName)
            End If
        End Using
        StatusLabel.Text = My.Resources.Stat_Ready
    End Sub
    Private Sub MnuLibRefresh_Click(sender As Object, e As EventArgs) Handles MnuLibRefresh.Click
        RefreshLib()
    End Sub
    ''' <summary>
    ''' 刷新菜单列表, 并重新载入数据
    ''' </summary>
    Private Sub RefreshLib()
        CloseLibrary() '为了避免编辑稿件后图片浏览器出现问题,所以必须关闭库重载
        RefreshLibListMenu()
        If _libraryManager.GetCurrentLibrary IsNot Nothing Then LoadArtworks()
        If SearchTextBox.Text <> "" Then SearchArtwork()
    End Sub
    Private Sub MnuLibClone_Click(sender As Object, e As EventArgs) Handles MnuLibClone.Click
        Using inputForm As New InputDialogForm With {
            .Text = My.Resources.Input_CloneLibTitle
        }
            inputForm.InputTxtbox.Text = String.Format(My.Resources.Input_TxtClone, _libraryManager.GetCurrentLibrary.LibraryName)
            StatusLabel.Text = My.Resources.Stat_Cloning
            If inputForm.ShowDialog() = DialogResult.OK Then '显示对话框并获取结果
                Dim newLib As String = inputForm.InputValue
                Dim newPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Artworks", newLib)
                Try
                    FileIO.FileSystem.CopyDirectory(_libraryManager.GetCurrentLibrary.LibraryPath, newPath, FileIO.UIOption.AllDialogs)
                Catch ex As OperationCanceledException
                    Return '操作被取消, 忽略即可
                End Try
                _libraryManager.AddLibrary(newLib)
                _libraryManager.SwitchLibrary(newLib)
                RefreshLib() '刷新
            Else
            End If
            StatusLabel.Text = My.Resources.Stat_Ready
        End Using
    End Sub
    Private Sub MnuLibOpenFolder_Click(sender As Object, e As EventArgs) Handles MnuLibOpenFolder.Click
        Shell($"explorer /select,{_libraryManager.GetCurrentLibrary.LibraryPath}", 1)
    End Sub
    Private Sub MnuLibCopy_Click(sender As Object, e As EventArgs) Handles MnuLibCopy.Click
        CopyDirectoryToClipboard(_libraryManager.GetCurrentLibrary.LibraryPath)
    End Sub
    Private Sub MnuLibCopyPath_Click(sender As Object, e As EventArgs) Handles MnuLibCopyPath.Click
        Clipboard.SetDataObject(_libraryManager.GetCurrentLibrary.LibraryPath)
    End Sub
    Private Sub MnuLibClose_Click(sender As Object, e As EventArgs) Handles MnuLibClose.Click
        StatusLabel.Text = My.Resources.Stat_Closing
        CloseLibrary()
        _libraryManager.CloseLibrary(_libraryManager.GetCurrentLibrary.LibraryName)
        MenuInit()
    End Sub
    Private Sub MnuLibRename_Click(sender As Object, e As EventArgs) Handles MnuLibRename.Click
        Dim oldLib As String = _libraryManager.GetCurrentLibrary.LibraryName
        Dim oldPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Artworks", oldLib)
        Using inputForm As New InputDialogForm With {
            .Text = My.Resources.Input_RenameLibTitle
        }
            inputForm.InputTxtbox.Text = _libraryManager.GetCurrentLibrary.LibraryName
            StatusLabel.Text = My.Resources.Stat_Renaming
            If inputForm.ShowDialog() = DialogResult.OK Then '显示对话框并获取结果
                Try
                    Dim newLib As String = inputForm.InputValue
                    _libraryManager.CloseLibrary(_libraryManager.GetCurrentLibrary.LibraryName) '先释放数据库资源
                    Dim newPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Artworks", newLib)
                    Directory.Move(oldPath, newPath) '重命名
                    _libraryManager.AddLibrary(newLib) '载入新稿件库
                    _libraryManager.SwitchLibrary(newLib)
                Catch ex As Exception
                    MessageBox.Show(String.Format(My.Resources.Msg_CreateFailed, ex.Message))
                End Try
                RefreshLib() '更名后重新载入数据库
            Else
            End If
            StatusLabel.Text = My.Resources.Stat_Ready
        End Using
    End Sub
    Private Sub MnuLibDelete_Click(sender As Object, e As EventArgs) Handles MnuLibDelete.Click
        Dim isShiftPressed As Boolean = My.Computer.Keyboard.ShiftKeyDown
        Dim nowPath As String = _libraryManager.GetCurrentLibrary.LibraryPath
        Dim nowLib As String = _libraryManager.GetCurrentLibrary.LibraryName
        _libraryManager.CloseLibrary(nowLib) '先释放数据库资源, 再尝试删除文件
        Try
            StatusLabel.Text = My.Resources.Stat_DeletingLib
            If isShiftPressed Then
                Dim result = MessageBox.Show(String.Format(My.Resources.Msg_DeleteLibPermanently, nowLib),
                    My.Resources.FurryArtStudio,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning)
                If result = DialogResult.Yes Then
                    Directory.Delete(nowPath, True)
                Else
                    Throw New OperationCanceledException(My.Resources.Main_StrOperationCancelled)
                End If
            Else
                Dim result = MessageBox.Show(String.Format(My.Resources.Msg_DeleteLib, nowLib),
                    My.Resources.FurryArtStudio,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    FileIO.FileSystem.DeleteDirectory(nowPath,
                    FileIO.UIOption.AllDialogs,
                    FileIO.RecycleOption.SendToRecycleBin)
                Else
                    Throw New OperationCanceledException(My.Resources.Main_StrOperationCancelled)
                End If
            End If
            MenuInit()
            CloseLibrary()
        Catch ex As OperationCanceledException '操作被取消, 回滚操作
            _libraryManager.AddLibrary(nowLib)
            _libraryManager.SwitchLibrary(nowLib)
        Catch ex As Exception
            MessageBox.Show(String.Format(My.Resources.Msg_LibDeleteFailed, ex.Message), My.Resources.FurryArtStudio, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        StatusLabel.Text = My.Resources.Stat_Ready
    End Sub
    Private Sub MnuLibStatistics_Click(sender As Object, e As EventArgs) Handles MnuLibStatistics.Click
        ShowLibStatistics()
    End Sub
    Private Sub ShowLibStatistics()
        Dim sb As New StringBuilder
        Dim library = _libraryManager.GetCurrentLibrary
        sb.Append(String.Format(My.Resources.Main_StrPropLib, library.LibraryName) & vbCrLf)
        sb.Append(String.Format(My.Resources.Main_StrPropLibPath, library.LibraryPath) & vbCrLf)
        sb.Append(String.Format(My.Resources.Main_StrPropMsCount, library.GetAllArtworksComplete.Count) & vbCrLf)
        Dim result = GetFolderInfo(library.LibraryPath)
        sb.Append(String.Format(My.Resources.Main_StrPropStorage, result.sizeString, result.fileCount) & vbCrLf)
        sb.Append(String.Format(My.Resources.Main_StrPropNowTime, Now))
        MessageBox.Show(sb.ToString, My.Resources.FurryArtStudio, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
#End Region

#Region "稿件菜单项"
    Private Sub MnuMsNew_Click(sender As Object, e As EventArgs) Handles MnuMsNew.Click
        NewManuscript()
    End Sub
    Private Sub NewManuscript()
        StatusLabel.Text = My.Resources.Stat_NewMs
        Dim initArtwork As New Artwork With {
            .CreateTime = Now
        }
        Using editForm As New EditDialogForm(initArtwork, _libraryManager.GetCurrentLibrary.LibraryPath)
            If editForm.ShowDialog() = DialogResult.OK Then
                Dim newArtwork As Artwork = editForm.EditedArtwork '获得新建的稿件对象
                _libraryManager.GetCurrentLibrary.AddArtwork(newArtwork)
                RefreshLib()
            End If
        End Using
        StatusLabel.Text = My.Resources.Stat_Ready
    End Sub
    Private Sub MnuMsImport_Click(sender As Object, e As EventArgs) Handles MnuMsImport.Click

    End Sub
    Private Sub MnuMsView_Click(sender As Object, e As EventArgs) Handles MnuMsView.Click
        ViewImage(Guid.Parse(ImageGalleryMain.SelectedImages(0).UUID))
    End Sub
    Private Sub MnuMsEdit_Click(sender As Object, e As EventArgs) Handles MnuMsEdit.Click
        StatusLabel.Text = My.Resources.Stat_EditMs
        Dim nowArtwork As Artwork = _libraryManager.GetCurrentLibrary.GetArtworkByUUID(Guid.Parse(ImageGalleryMain.SelectedImages(0).UUID))
        Using editForm As New EditDialogForm(nowArtwork, _libraryManager.GetCurrentLibrary.LibraryPath)
            If editForm.ShowDialog() = DialogResult.OK Then
                Dim newArtwork As Artwork = editForm.EditedArtwork '获得新建的稿件对象
                _libraryManager.GetCurrentLibrary.UpdateArtwork(newArtwork)
                RefreshLib()
            End If
        End Using
        StatusLabel.Text = My.Resources.Stat_Ready
    End Sub
    Private Sub MnuMsDelete_Click(sender As Object, e As EventArgs) Handles MnuMsDelete.Click
        Dim isShiftPressed As Boolean = My.Computer.Keyboard.ShiftKeyDown
        StatusLabel.Text = My.Resources.Stat_DeletingMs
        Dim imgList As List(Of GalleryImage) = ImageGalleryMain.SelectedImages()
        Dim uuidList As List(Of Guid) = imgList.Select(Function(c) Guid.Parse(c.UUID)).ToList()
        Dim titleList As List(Of String) = imgList.Select(Function(c) c.Title).ToList()
        Dim nowPath As String = _libraryManager.GetCurrentLibrary.LibraryPath
        Try
            If isShiftPressed Then
                Dim result = MessageBox.Show(String.Format(My.Resources.Msg_DeleteMsPermanently, titleList.Count) & vbCrLf &
                                             String.Join(vbCrLf, titleList.Take(5)) &
                                             If(titleList.Count > 5, vbCrLf & String.Format(My.Resources.Msg_DeleteMsCount, titleList.Count), ""),
                    My.Resources.FurryArtStudio,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning)
                If result <> DialogResult.Yes Then
                    StatusLabel.Text = My.Resources.Stat_Ready
                    Return
                End If
                For Each uuid In uuidList
                    Directory.Delete(Path.Combine(nowPath, uuid.ToString), True) '永久删除数据
                    _libraryManager.GetCurrentLibrary.SoftDeleteArtwork(uuid) '标记为软删除
                Next
            Else
                Dim result = MessageBox.Show(String.Format(My.Resources.Msg_DeleteMs, titleList.Count) & vbCrLf &
                                             String.Join(vbCrLf, titleList.Take(5)) &
                                             If(titleList.Count > 5, vbCrLf & String.Format(My.Resources.Msg_DeleteMsCount, titleList.Count), ""),
                    My.Resources.FurryArtStudio,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                If result <> DialogResult.Yes Then
                    StatusLabel.Text = My.Resources.Stat_Ready
                    Return
                End If
                For Each uuid In uuidList
                    FileIO.FileSystem.DeleteDirectory(Path.Combine(nowPath, uuid.ToString),
                                                      FileIO.UIOption.OnlyErrorDialogs,
                                                      FileIO.RecycleOption.SendToRecycleBin) '移动到回收站
                    _libraryManager.GetCurrentLibrary.SoftDeleteArtwork(uuid) '标记为软删除
                Next
            End If
            RefreshLib()
        Catch ex As OperationCanceledException
            '忽略
        End Try
        StatusLabel.Text = My.Resources.Stat_Ready
    End Sub
    Private Sub MnuMsExport_Click(sender As Object, e As EventArgs) Handles MnuMsExport.Click
        '待开发
    End Sub
    Private Sub MnuMsPrint_Click(sender As Object, e As EventArgs) Handles MnuMsPrint.Click
        StatusLabel.Text = My.Resources.Stat_PreparePrint
        '获取选中的图片列表
        Dim selectedImages As New List(Of String)()
        Dim selectedItem As List(Of GalleryImage) = ImageGalleryMain.SelectedImages
        '获取图片目录
        Dim artworkDir = Path.Combine(_libraryManager.GetCurrentLibrary.LibraryPath, selectedItem(0).UUID)
        '获取目录下所有图片文件
        If Directory.Exists(artworkDir) Then
            '支持的图片格式
            Dim imageExtensions() As String = {".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".webp"}
            Dim files = Directory.GetFiles(artworkDir, "*.*")
            For Each file In files
                '排除 .preview.jpg 缩略图文件
                Dim fileName As String = Path.GetFileName(file)
                If fileName.EndsWith(".preview.jpg", StringComparison.OrdinalIgnoreCase) Then
                    Continue For
                End If
                If imageExtensions.Contains(Path.GetExtension(file).ToLower()) Then
                    selectedImages.Add(file)
                End If
            Next
        End If
        '检查是否有图片
        If selectedImages.Count = 0 Then
            MessageBox.Show(My.Resources.Msg_NoPrintFile, My.Resources.FurryArtStudio,
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            StatusLabel.Text = My.Resources.Stat_Ready
            Return
        End If
        Dim isShiftPressed As Boolean = My.Computer.Keyboard.ShiftKeyDown
        Dim nowArtwork As Artwork = _libraryManager.GetCurrentLibrary.GetArtworkByUUID(Guid.Parse(selectedItem(0).UUID))
        Dim docName As String = $"{nowArtwork.Title} - {nowArtwork.Author}"
        If isShiftPressed Then
            Dim pd As New PrintDocument With {
                .DocumentName = docName,'标题 - 作者
                .DefaultPageSettings = New PageSettings With {
                .Landscape = True '默认横向
            }
        }
            _currentPrintImages = selectedImages
            AddHandler pd.PrintPage, AddressOf Pd_Printpage
            AddHandler pd.EndPrint, AddressOf Pd_EndPrint
            Dim printDialog As New PrintDialog With {
                .Document = pd,
                .AllowPrintToFile = True,
                .AllowSomePages = False,
                .AllowSelection = False,
                .UseEXDialog = True,
                .PrinterSettings = New PrinterSettings With {
                .PrintFileName = docName
            }
        } '设置打印对话框
            Dim pageSetupDialog As New PageSetupDialog With {
                .Document = pd,
                .EnableMetric = True
            } '设置页面设置对话框
            '先显示页面设置
            If pageSetupDialog.ShowDialog() = DialogResult.OK Then
                '再显示打印对话框
                If printDialog.ShowDialog() = DialogResult.OK Then
                    StatusLabel.Text = My.Resources.Stat_Printing
                    '开始打印
                    pd.Print()
                End If
            End If
        Else
            '使用新的自定义打印对话框
            Using printForm As New PrintForm(selectedImages, nowArtwork)
                If printForm.ShowDialog() = DialogResult.OK AndAlso printForm.PrintDocumentInstance IsNot Nothing Then
                    Try
                        '开始打印
                        StatusLabel.Text = My.Resources.Stat_Printing
                        printForm.PrintDocumentInstance.Print()
                    Catch ex As Exception
                        MessageBox.Show(String.Format(My.Resources.Msg_PrintFailed, ex.Message),
                                  My.Resources.FurryArtStudio,
                                  MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                ElseIf printForm.UserCancelled Then
                End If
            End Using
        End If
        StatusLabel.Text = My.Resources.Stat_Ready
    End Sub
    Private Sub Pd_Printpage(sender As Object, e As PrintPageEventArgs)
        If _currentPrintImages Is Nothing OrElse _currentPrintIndex >= _currentPrintImages.Count Then
            e.HasMorePages = False
            Return
        End If
        Try
            Dim currentImagePath = _currentPrintImages(_currentPrintIndex) '加载当前图片
            Using img As Image = Image.FromFile(currentImagePath) '使用Using确保释放资源
                Dim marginBounds = e.MarginBounds '获取页面边距区域
                Dim destRect As Rectangle '保持图片比例
                If img.Width / img.Height > marginBounds.Width / marginBounds.Height Then
                    '图片比页面区域宽
                    Dim height = CInt(img.Height * marginBounds.Width / img.Width)
                    destRect = New Rectangle(marginBounds.X,
                                         marginBounds.Y + (marginBounds.Height - height) \ 2,
                                         marginBounds.Width,
                                         height)
                Else
                    '图片比页面区域高
                    Dim width = CInt(img.Width * marginBounds.Height / img.Height)
                    destRect = New Rectangle(marginBounds.X + (marginBounds.Width - width) \ 2,
                                         marginBounds.Y,
                                         width,
                                         marginBounds.Height)
                End If
                '绘制图片
                e.Graphics.DrawImage(img, destRect)
            End Using
            '移动到下一张图片
            _currentPrintIndex += 1
            '检查是否还有更多页面
            e.HasMorePages = (_currentPrintIndex < _currentPrintImages.Count)
        Catch ex As Exception
            '处理图片加载失败的情况
            MessageBox.Show(String.Format(My.Resources.Msg_ImageLoadFailed, ex.Message), My.Resources.FurryArtStudio,
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            '跳过这张图片，继续下一张
            _currentPrintIndex += 1
            e.HasMorePages = (_currentPrintIndex < _currentPrintImages.Count)
        End Try
    End Sub
    Private Sub Pd_EndPrint(sender As Object, e As PrintEventArgs)
        If _currentPrintImages IsNot Nothing Then '打印完成后清理资源
            _currentPrintImages.Clear()
            _currentPrintImages = Nothing
        End If
        _currentPrintIndex = 0
    End Sub
    ''' <summary>
    ''' 获得已选择的稿件UUID目录
    ''' </summary>
    ''' <returns>一个包含已选择稿件UUID的列表</returns>
    Private Function GetSelectedArtworkList() As List(Of String)
        Dim artworkPaths As New List(Of String)()
        For Each selectedItem As GalleryImage In ImageGalleryMain.SelectedImages
            Dim artworkPath = Path.Combine(_libraryManager.GetCurrentLibrary.LibraryPath, selectedItem.UUID)
            artworkPaths.Add(artworkPath)
        Next
        Return artworkPaths
    End Function
    Private Sub MnuMsOpenFolder_Click(sender As Object, e As EventArgs) Handles MnuMsOpenFolder.Click
        Dim artworkPaths = GetSelectedArtworkList()
        If artworkPaths.Count > 5 Then '当用户打开超过5个稿件时, 进行确认
            Dim result = MessageBox.Show(
                            String.Format(My.Resources.Msg_MultiFolderOpen, artworkPaths.Count),
                            My.Resources.FurryArtStudio, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.No Then Return
        End If
        For Each artworkPath In artworkPaths
            Shell($"explorer {artworkPath}", 1)
        Next
    End Sub
    Private Sub MnuMsCopy_Click(sender As Object, e As EventArgs) Handles MnuMsCopy.Click
        Dim artworkPaths = GetSelectedArtworkList() '获取所有选中的项目的目录路径
        If My.Computer.Keyboard.ShiftKeyDown Then
            Dim artworkObject As New DataObject()
            Dim artworkInfo As New StringBuilder
            artworkInfo.Append(My.Resources.Msg_StrMsInfo & vbCrLf)
            artworkInfo.Append($"{SeparatorDash}{vbCrLf}")
            Dim count As Integer = 0
            For Each selectedItem As GalleryImage In ImageGalleryMain.SelectedImages
                count += 1
                Dim artwork As Artwork = _libraryManager.GetCurrentLibrary.GetArtworkByUUID(Guid.Parse(selectedItem.UUID))
                If count > 1 Then artworkInfo.Append($"{SeparatorDash}{vbCrLf}") '当多个稿件时添加分割线
                artworkInfo.Append(String.Format(My.Resources.Msg_StrMsTitle, artwork.Title, artwork.Author) & vbCrLf)
                artworkInfo.Append(String.Format(My.Resources.Msg_StrMsUUID, selectedItem.UUID) & vbCrLf)
                artworkInfo.Append(String.Format(My.Resources.Msg_StrMsUpdate, artwork.UpdateTime) & vbCrLf)
                artworkInfo.Append(String.Format(My.Resources.Msg_StrMsImport, artwork.ImportTime) & vbCrLf)
                artworkInfo.Append(String.Format(My.Resources.Msg_StrMsCreate, artwork.CreateTime) & vbCrLf)
                artworkInfo.Append(String.Format(My.Resources.Msg_StrMsTags, FormatArrayWithEllipsis(artwork.Tags)) & vbCrLf)
                artworkInfo.Append(String.Format(My.Resources.Msg_StrMsCharacters, FormatArrayWithEllipsis(artwork.Characters)) & vbCrLf)
                artworkInfo.Append(String.Format(My.Resources.Msg_StrMsNotes, artwork.Notes) & vbCrLf)
            Next
            artworkInfo.Append($"{SeparatorDash}{vbCrLf}")
            artworkInfo.Append(String.Format(My.Resources.Msg_StrMsTotal, count) & vbCrLf) '总计
            artworkInfo.Append(String.Format(My.Resources.Msg_StrMsNow, Now) & vbCrLf) '当前时间
            artworkInfo.Append(My.Resources.Msg_StrMsSupportByFAS)
            artworkObject.SetData(DataFormats.Text, artworkInfo) '复制信息文本
            CopyDirectoryToClipboard(artworkPaths.ToArray(), artworkObject) '同时复制稿件
        Else
            CopyDirectoryToClipboard(artworkPaths.ToArray())
        End If
    End Sub
    Private Sub MnuMsCopyPath_Click(sender As Object, e As EventArgs) Handles MnuMsCopyPath.Click
        Dim artworkPaths = GetSelectedArtworkList()
        Dim sb As New StringBuilder()
        For i As Integer = 0 To artworkPaths.Count - 1
            sb.Append(artworkPaths(i))
            If i < artworkPaths.Count - 1 Then
                sb.AppendLine()
            End If
        Next
        Clipboard.SetDataObject(sb.ToString())
    End Sub
#End Region

#Region "视图菜单项"
    Private Sub MnuViewPlay_Click(sender As Object, e As EventArgs) Handles MnuViewPlay.Click
        '待开发
    End Sub
    Private Sub MnuAdvancedSearch_Click(sender As Object, e As EventArgs) Handles MnuAdvancedSearch.Click
        '待开发
    End Sub
    Private Sub MnuSearch_Click(sender As Object, e As EventArgs) Handles MnuSearch.Click
        SearchTextBox.Focus()
    End Sub
    Private Sub MnuSelectAll_Click(sender As Object, e As EventArgs) Handles MnuSelectAll.Click
        ImageGalleryMain.SelectAll()
    End Sub
    Private Sub MnuSelectReverse_Click(sender As Object, e As EventArgs) Handles MnuSelectReverse.Click
        ImageGalleryMain.SelectReverse()
    End Sub
    Private Sub MnuPageUp_Click(sender As Object, e As EventArgs) Handles MnuPageUp.Click
        ImageGalleryMain.SetPage(ImageGalleryMain.Page - 1)
        UpdatePageMenu()
    End Sub
    Private Sub MnuPageDown_Click(sender As Object, e As EventArgs) Handles MnuPageDown.Click
        ImageGalleryMain.SetPage(ImageGalleryMain.Page + 1)
        UpdatePageMenu()
    End Sub
    Private Sub UpdatePageMenu()
        If ImageGalleryMain.Page = 1 Then
            MnuPageUp.Enabled = False
        Else
            MnuPageUp.Enabled = True
        End If
        If ImageGalleryMain.Page = ImageGalleryMain.TotalPages Then
            MnuPageDown.Enabled = False
        Else
            MnuPageDown.Enabled = True
        End If
    End Sub
#End Region

#Region "关于菜单项"
    Private Sub MnuHelpAbout_Click(sender As Object, e As EventArgs) Handles MnuHelpAbout.Click
        AboutForm.ShowDialog()
    End Sub
    Private Sub MnuHelpGithub_Click(sender As Object, e As EventArgs) Handles MnuHelpGithub.Click
        Process.Start("https://github.com/xionglongztz/FurryArtStudio")
    End Sub
    Private Sub MnuHelpWebsite_Click(sender As Object, e As EventArgs) Handles MnuHelpWebsite.Click

    End Sub
    Private Sub MnuHelpLicense_Click(sender As Object, e As EventArgs) Handles MnuHelpLicense.Click
        Dim txt As New TextBoxForm(My.Resources.Licenses.LicenseText, My.Resources.Main_StrLicense)
        txt.Show()
    End Sub
    Private Sub MnuHelpPrivacy_Click(sender As Object, e As EventArgs) Handles MnuHelpPrivacy.Click
        Dim txt As New TextBoxForm(My.Resources.Licenses.PrivacyText, My.Resources.Main_StrPrivacy)
        txt.Show()
    End Sub
    Private Sub MnuHelpTutorial_Click(sender As Object, e As EventArgs) Handles MnuHelpTutorial.Click

    End Sub
    Private Async Sub MnuCheckUpdate_Click(sender As Object, e As EventArgs) Handles MnuCheckUpdate.Click
        Await CheckForUpdate()
    End Sub
    Private Async Function CheckForUpdate() As Task
        StatusLabel.Text = My.Resources.Msg_CheckingUpdate
        Dim updateInfo = Await CheckForUpdateAsync()
        If updateInfo.HasError Then '检查更新失败
            MessageBox.Show(String.Format(My.Resources.Msg_CheckUpdateFailed, updateInfo.ErrorMessage), My.Resources.FurryArtStudio, MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf updateInfo.IsUpdateAvailable Then '有新版本可用
            Dim msg = updateInfo.ReleaseNotes & vbCrLf & My.Resources.Msg_DownloadNow
            Dim result = MessageBox.Show(msg, String.Format(My.Resources.Msg_NewVerFound, updateInfo.LatestVersion), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                Process.Start(updateInfo.DownloadUrl) '打开下载链接
            End If
        Else '已是最新版本
            MessageBox.Show(String.Format(My.Resources.Msg_UptoDate, updateInfo.LatestVersion), My.Resources.FurryArtStudio, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        StatusLabel.Text = My.Resources.Stat_Ready
    End Function
    Private Sub MnuHelpWhatsNew_Click(sender As Object, e As EventArgs) Handles MnuHelpWhatsNew.Click
        Dim txt As New TextBoxForm(My.Resources.Licenses.WhatsNewText, My.Resources.Main_StrWhatsNew)
        txt.Show()
    End Sub
    Private Sub MnuTerms_Click(sender As Object, e As EventArgs) Handles MnuTerms.Click
        Dim txt As New TextBoxForm(My.Resources.Licenses.TermsText, My.Resources.Main_StrTerms)
        txt.Show()
    End Sub
#End Region

#Region "弹出菜单"
    Private Sub ConMnuMsView_Click(sender As Object, e As EventArgs) Handles ConMnuMsView.Click
        MnuMsView.PerformClick()
    End Sub
    Private Sub ConMnuMsEdit_Click(sender As Object, e As EventArgs) Handles ConMnuMsEdit.Click
        MnuMsEdit.PerformClick()
    End Sub
    Private Sub ConMnuMsExport_Click(sender As Object, e As EventArgs) Handles ConMnuMsExport.Click
        MnuMsExport.PerformClick()
    End Sub
    Private Sub ConMnuMsPrint_Click(sender As Object, e As EventArgs) Handles ConMnuMsPrint.Click
        MnuMsPrint.PerformClick()
    End Sub
    Private Sub ConMnuMsDelete_Click(sender As Object, e As EventArgs) Handles ConMnuMsDelete.Click
        MnuMsDelete.PerformClick()
    End Sub
    Private Sub ConMnuMsOpenFolder_Click(sender As Object, e As EventArgs) Handles ConMnuMsOpenFolder.Click
        MnuMsOpenFolder.PerformClick()
    End Sub
    Private Sub ConMnuMsCopy_Click(sender As Object, e As EventArgs) Handles ConMnuMsCopy.Click
        MnuMsCopy.PerformClick()
    End Sub
    Private Sub ConMnuMsCopyPath_Click(sender As Object, e As EventArgs) Handles ConMnuMsCopyPath.Click
        MnuMsCopyPath.PerformClick()
    End Sub
#End Region

#End Region

#Region "图片墙功能"
    Private Sub ClearSelect()
        MnuMsView.Enabled = False
        MnuMsEdit.Enabled = False
        MnuMsDelete.Enabled = False
        MnuMsExport.Enabled = False
        MnuMsPrint.Enabled = False
        MnuMsOpenFolder.Enabled = False
        MnuMsCopy.Enabled = False
        MnuMsCopyPath.Enabled = False
        ConMnuMsView.Enabled = False
        ConMnuMsEdit.Enabled = False
        ConMnuMsDelete.Enabled = False
        ConMnuMsExport.Enabled = False
        ConMnuMsPrint.Enabled = False
        ConMnuMsOpenFolder.Enabled = False
        ConMnuMsCopy.Enabled = False
        ConMnuMsCopyPath.Enabled = False
        SelectStatusLabel.Text = String.Format(My.Resources.Main_LblMs, _artworkCount)
        LblTitle.Text = My.Resources.Main_LblNoSelect
        LblAuthor.Text = ""
        LblTags.Text = ""
        LblCharacters.Text = ""
        LblNotes.Text = ""
        PiChkThumb.Image = Nothing
    End Sub
    Private Sub ImageGalleryMain_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ImageGalleryMain.SelectionChanged
        Dim selectedImages = e.SelectedImages
        Dim selectedCount As Integer = selectedImages.Count
        If selectedImages.Count = 0 Then
            ClearSelect()
        ElseIf selectedImages.Count = 1 Then
            Dim selectedImage = selectedImages(0)
            Dim selectedArtwork = _libraryManager.GetCurrentLibrary.GetArtworkByUUID(Guid.Parse(selectedImage.UUID))
            MnuMsView.Enabled = True
            MnuMsEdit.Enabled = True
            MnuMsExport.Enabled = True
            MnuMsPrint.Enabled = True
            MnuMsOpenFolder.Enabled = True
            MnuMsCopy.Enabled = True
            MnuMsCopyPath.Enabled = True
            MnuMsDelete.Enabled = True
            ConMnuMsView.Enabled = True
            ConMnuMsEdit.Enabled = True
            ConMnuMsDelete.Enabled = True
            ConMnuMsExport.Enabled = True
            ConMnuMsPrint.Enabled = True
            ConMnuMsOpenFolder.Enabled = True
            ConMnuMsCopy.Enabled = True
            ConMnuMsCopyPath.Enabled = True
            SelectStatusLabel.Text = String.Format(My.Resources.Main_LblSelectMs1, _artworkCount)
            PiChkThumb.Image = selectedImage.Thumbnail
            LblTitle.Text = $"{selectedArtwork.Title}"
            LblAuthor.Text = $"{selectedArtwork.Author}"
            LblCharacters.Text = String.Format(My.Resources.Main_LblCharacter, FormatArrayWithEllipsis(selectedArtwork.Characters))
            LblTags.Text = String.Format(My.Resources.Main_LblTags, FormatArrayWithEllipsis(selectedArtwork.Tags))
            LblNotes.Text = String.Format(My.Resources.Main_LblNotes, selectedArtwork.Notes)
        ElseIf selectedCount > 1 Then '选择多个时
            MnuMsView.Enabled = False
            MnuMsEdit.Enabled = False
            MnuMsDelete.Enabled = True
            MnuMsExport.Enabled = True
            MnuMsPrint.Enabled = False
            MnuMsOpenFolder.Enabled = True
            MnuMsCopy.Enabled = True
            MnuMsCopyPath.Enabled = True
            ConMnuMsView.Enabled = False
            ConMnuMsEdit.Enabled = False
            ConMnuMsDelete.Enabled = True
            ConMnuMsExport.Enabled = True
            ConMnuMsPrint.Enabled = False
            ConMnuMsOpenFolder.Enabled = True
            ConMnuMsCopy.Enabled = True
            ConMnuMsCopyPath.Enabled = True
            SelectStatusLabel.Text = String.Format(My.Resources.Main_LblSelectMs, selectedCount, _artworkCount)
            LblTitle.Text = String.Format(My.Resources.Main_LblTitleSelect, selectedCount)
            LblAuthor.Text = ""
            LblCharacters.Text = ""
            LblTags.Text = ""
            LblNotes.Text = ""
            PiChkThumb.Image = Nothing
        End If
    End Sub
    Private Sub ImageGalleryMain_PageChanged(page As Integer) Handles ImageGalleryMain.PageChanged
        PageStatusLabel.Text = String.Format(My.Resources.Main_LblPage, page, ImageGalleryMain.TotalPages)
        UpdatePageMenu()
    End Sub
    Private Sub ImageGalleryMain_ImageDoubleClicked(image As GalleryImage) Handles ImageGalleryMain.ImageDoubleClicked
        ViewImage(Guid.Parse(image.UUID))
    End Sub
    Private Sub ImageGalleryMain_ImageRightClicked(image As GalleryImage) Handles ImageGalleryMain.ImageRightClicked
        ConMenu.Show(Control.MousePosition)
    End Sub
#End Region

#Region "图片浏览器"
    Private Sub ViewImage(uuid As Guid)
        Try
            Dim currentArtwork As Artwork = _libraryManager.GetCurrentLibrary.GetArtworkByUUID(uuid) '获取当前选中的稿件
            Dim allArtworks As List(Of Artwork) = _imageList '获取所有稿件列表
            If currentArtwork.FilePaths Is Nothing OrElse'检查当前稿件是否有图片
            currentArtwork.FilePaths.Length = 0 OrElse
            Not currentArtwork.FilePaths.Any(Function(p) IsImageFile(p)) Then
                MessageBox.Show(My.Resources.Msg_NoImg, My.Resources.FurryArtStudio,
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            Dim viewForm As New ViewForm(currentArtwork, allArtworks)
            _openViewForms.Add(viewForm)
            AddHandler viewForm.FormClosed, Sub(sender, e)
                                                _openViewForms.Remove(viewForm)
                                            End Sub '订阅窗口关闭事件
            viewForm.Show() '创建并显示图片窗口
        Catch ex As Exception
            MessageBox.Show(String.Format(My.Resources.Msg_ImageLoadFailed, ex.Message), My.Resources.FurryArtStudio,
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub CloseLibrary()
        RaiseEvent LibraryClosed(Me, EventArgs.Empty) '触发库关闭事件, 通知所有图片窗口
        _openViewForms.Clear()
    End Sub
#End Region

End Class