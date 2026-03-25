<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.MnuStrip = New System.Windows.Forms.MenuStrip()
        Me.MnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuLibrary = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuLibCopyPath = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuManuscript = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuMsCopyPath = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuViews = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuSelectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSelectReverse = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuAdvancedSearch = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuHelpDonate = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLoadSponsors = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuFeedback = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSuggestions = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMyFeedback = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuHelpLicense = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuHelpPrivacy = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuTerms = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchTextBox = New System.Windows.Forms.TextBox()
        Me.ArtworkListSplitContainer = New System.Windows.Forms.SplitContainer()
        Me.ImageGalleryMain = New FurryArtStudio.ImageGallery()
        Me.LblNotes = New System.Windows.Forms.Label()
        Me.LblTags = New System.Windows.Forms.Label()
        Me.LblCharacters = New System.Windows.Forms.Label()
        Me.LblAuthor = New System.Windows.Forms.Label()
        Me.LblTitle = New System.Windows.Forms.Label()
        Me.StaStrip = New System.Windows.Forms.StatusStrip()
        Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSSep1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SearchStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSspace = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PageStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSSep2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ArtworkStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSSep3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SelectStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSSep4 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StorageStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSSep5 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.KryptonMgrMain = New Krypton.Toolkit.KryptonManager(Me.components)
        Me.ConMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ConMnuMsView = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConMnuMsEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConMnuMsExport = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConMnuMsPrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConMnuMsDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.ConMnuMsOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConMnuMsCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConMnuMsCopyPath = New System.Windows.Forms.ToolStripMenuItem()
        Me.PiChkThumb = New System.Windows.Forms.PictureBox()
        Me.MnuOnTop = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDevTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuRunAsElevated = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuRunTerminal = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCreateShortcut = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuProperties = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLibList = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLibRefresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLibNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLibImport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLibExport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLibExportCSV = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLibClone = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLibOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLibCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLibClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLibRename = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLibDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLibStatistics = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMsNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMsImport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMsView = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMsEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMsExport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMsPrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMsDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMsOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMsCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuViewPlay = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSearch = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPageUp = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPageDown = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuHelpTutorial = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuHelpWebsite = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuOpenAfdian = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBugReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuHelpGithub = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCheckUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuHelpWhatsNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuHelpAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuStrip.SuspendLayout()
        CType(Me.ArtworkListSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ArtworkListSplitContainer.Panel1.SuspendLayout()
        Me.ArtworkListSplitContainer.Panel2.SuspendLayout()
        Me.ArtworkListSplitContainer.SuspendLayout()
        Me.StaStrip.SuspendLayout()
        Me.ConMenu.SuspendLayout()
        CType(Me.PiChkThumb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MnuStrip
        '
        Me.MnuStrip.Font = New System.Drawing.Font("Microsoft YaHei UI", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        Me.MnuStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuFile, Me.MnuLibrary, Me.MnuManuscript, Me.MnuViews, Me.MnuHelp})
        Me.MnuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MnuStrip.Name = "MnuStrip"
        Me.MnuStrip.Size = New System.Drawing.Size(962, 28)
        Me.MnuStrip.TabIndex = 4
        Me.MnuStrip.Text = "MenuStrip1"
        '
        'MnuFile
        '
        Me.MnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuOnTop, Me.ToolStripSeparator9, Me.MnuDevTools, Me.MnuRunAsElevated, Me.MnuRunTerminal, Me.MnuCreateShortcut, Me.MnuProperties, Me.ToolStripSeparator1, Me.MnuExit})
        Me.MnuFile.Name = "MnuFile"
        Me.MnuFile.Size = New System.Drawing.Size(71, 24)
        Me.MnuFile.Text = "文件(&F)"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(232, 6)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(232, 6)
        '
        'MnuLibrary
        '
        Me.MnuLibrary.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuLibList, Me.MnuLibRefresh, Me.ToolStripSeparator11, Me.MnuLibNew, Me.MnuLibImport, Me.MnuLibExport, Me.MnuLibExportCSV, Me.ToolStripSeparator5, Me.MnuLibClone, Me.MnuLibOpenFolder, Me.MnuLibCopy, Me.MnuLibCopyPath, Me.ToolStripSeparator6, Me.MnuLibClose, Me.MnuLibRename, Me.MnuLibDelete, Me.ToolStripSeparator7, Me.MnuLibStatistics})
        Me.MnuLibrary.Name = "MnuLibrary"
        Me.MnuLibrary.Size = New System.Drawing.Size(86, 24)
        Me.MnuLibrary.Text = "稿件库(&L)"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(255, 6)
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(255, 6)
        '
        'MnuLibCopyPath
        '
        Me.MnuLibCopyPath.Name = "MnuLibCopyPath"
        Me.MnuLibCopyPath.Size = New System.Drawing.Size(258, 26)
        Me.MnuLibCopyPath.Text = "复制目录路径(&T)"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(255, 6)
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(255, 6)
        '
        'MnuManuscript
        '
        Me.MnuManuscript.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuMsNew, Me.MnuMsImport, Me.ToolStripSeparator3, Me.MnuMsView, Me.MnuMsEdit, Me.MnuMsExport, Me.MnuMsPrint, Me.MnuMsDelete, Me.ToolStripSeparator4, Me.MnuMsOpenFolder, Me.MnuMsCopy, Me.MnuMsCopyPath})
        Me.MnuManuscript.Name = "MnuManuscript"
        Me.MnuManuscript.Size = New System.Drawing.Size(78, 24)
        Me.MnuManuscript.Text = "稿件(&M)"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(240, 6)
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(240, 6)
        '
        'MnuMsCopyPath
        '
        Me.MnuMsCopyPath.Name = "MnuMsCopyPath"
        Me.MnuMsCopyPath.Size = New System.Drawing.Size(243, 26)
        Me.MnuMsCopyPath.Text = "复制目录路径(&T)"
        '
        'MnuViews
        '
        Me.MnuViews.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuViewPlay, Me.ToolStripSeparator12, Me.MnuSelectAll, Me.MnuSelectReverse, Me.ToolStripSeparator13, Me.MnuSearch, Me.MnuAdvancedSearch, Me.ToolStripSeparator2, Me.MnuPageUp, Me.MnuPageDown})
        Me.MnuViews.Name = "MnuViews"
        Me.MnuViews.Size = New System.Drawing.Size(73, 24)
        Me.MnuViews.Text = "视图(&V)"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(280, 6)
        '
        'MnuSelectAll
        '
        Me.MnuSelectAll.Name = "MnuSelectAll"
        Me.MnuSelectAll.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.MnuSelectAll.Size = New System.Drawing.Size(283, 26)
        Me.MnuSelectAll.Text = "全选(&A)"
        '
        'MnuSelectReverse
        '
        Me.MnuSelectReverse.Name = "MnuSelectReverse"
        Me.MnuSelectReverse.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.MnuSelectReverse.Size = New System.Drawing.Size(283, 26)
        Me.MnuSelectReverse.Text = "反选(&R)"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(280, 6)
        '
        'MnuAdvancedSearch
        '
        Me.MnuAdvancedSearch.Name = "MnuAdvancedSearch"
        Me.MnuAdvancedSearch.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.MnuAdvancedSearch.Size = New System.Drawing.Size(283, 26)
        Me.MnuAdvancedSearch.Text = "高级搜索(&D)..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(280, 6)
        '
        'MnuHelp
        '
        Me.MnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuHelpTutorial, Me.ToolStripSeparator8, Me.MnuHelpWebsite, Me.MnuHelpDonate, Me.MnuHelpGithub, Me.MnuFeedback, Me.MnuCheckUpdate, Me.MnuHelpWhatsNew, Me.ToolStripSeparator10, Me.MnuHelpLicense, Me.MnuHelpPrivacy, Me.MnuTerms, Me.MnuHelpAbout})
        Me.MnuHelp.Name = "MnuHelp"
        Me.MnuHelp.Size = New System.Drawing.Size(75, 24)
        Me.MnuHelp.Text = "帮助(&H)"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(221, 6)
        '
        'MnuHelpDonate
        '
        Me.MnuHelpDonate.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuOpenAfdian, Me.MnuLoadSponsors})
        Me.MnuHelpDonate.Name = "MnuHelpDonate"
        Me.MnuHelpDonate.Size = New System.Drawing.Size(224, 26)
        Me.MnuHelpDonate.Text = "捐赠(&D)"
        '
        'MnuLoadSponsors
        '
        Me.MnuLoadSponsors.Name = "MnuLoadSponsors"
        Me.MnuLoadSponsors.Size = New System.Drawing.Size(224, 26)
        Me.MnuLoadSponsors.Text = "加载赞助者名单(&L)"
        '
        'MnuFeedback
        '
        Me.MnuFeedback.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuBugReport, Me.MnuSuggestions, Me.MnuMyFeedback})
        Me.MnuFeedback.Name = "MnuFeedback"
        Me.MnuFeedback.Size = New System.Drawing.Size(224, 26)
        Me.MnuFeedback.Text = "反馈(&F)"
        '
        'MnuSuggestions
        '
        Me.MnuSuggestions.Image = CType(resources.GetObject("MnuSuggestions.Image"), System.Drawing.Image)
        Me.MnuSuggestions.Name = "MnuSuggestions"
        Me.MnuSuggestions.Size = New System.Drawing.Size(224, 26)
        Me.MnuSuggestions.Text = "建议功能(&S)"
        '
        'MnuMyFeedback
        '
        Me.MnuMyFeedback.Name = "MnuMyFeedback"
        Me.MnuMyFeedback.Size = New System.Drawing.Size(224, 26)
        Me.MnuMyFeedback.Text = "我的反馈(&M)"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(221, 6)
        '
        'MnuHelpLicense
        '
        Me.MnuHelpLicense.Name = "MnuHelpLicense"
        Me.MnuHelpLicense.Size = New System.Drawing.Size(224, 26)
        Me.MnuHelpLicense.Text = "许可证(&L)..."
        '
        'MnuHelpPrivacy
        '
        Me.MnuHelpPrivacy.Name = "MnuHelpPrivacy"
        Me.MnuHelpPrivacy.Size = New System.Drawing.Size(224, 26)
        Me.MnuHelpPrivacy.Text = "隐私政策(&P)..."
        '
        'MnuTerms
        '
        Me.MnuTerms.Name = "MnuTerms"
        Me.MnuTerms.Size = New System.Drawing.Size(224, 26)
        Me.MnuTerms.Text = "用户协议(&T)..."
        '
        'SearchTextBox
        '
        Me.SearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SearchTextBox.Dock = System.Windows.Forms.DockStyle.Top
        Me.SearchTextBox.Location = New System.Drawing.Point(3, 0)
        Me.SearchTextBox.Name = "SearchTextBox"
        Me.SearchTextBox.Size = New System.Drawing.Size(727, 25)
        Me.SearchTextBox.TabIndex = 1
        '
        'ArtworkListSplitContainer
        '
        Me.ArtworkListSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ArtworkListSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.ArtworkListSplitContainer.Location = New System.Drawing.Point(0, 28)
        Me.ArtworkListSplitContainer.Name = "ArtworkListSplitContainer"
        '
        'ArtworkListSplitContainer.Panel1
        '
        Me.ArtworkListSplitContainer.Panel1.Controls.Add(Me.ImageGalleryMain)
        Me.ArtworkListSplitContainer.Panel1.Controls.Add(Me.SearchTextBox)
        Me.ArtworkListSplitContainer.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.ArtworkListSplitContainer.Panel1MinSize = 500
        '
        'ArtworkListSplitContainer.Panel2
        '
        Me.ArtworkListSplitContainer.Panel2.Controls.Add(Me.LblNotes)
        Me.ArtworkListSplitContainer.Panel2.Controls.Add(Me.LblTags)
        Me.ArtworkListSplitContainer.Panel2.Controls.Add(Me.LblCharacters)
        Me.ArtworkListSplitContainer.Panel2.Controls.Add(Me.LblAuthor)
        Me.ArtworkListSplitContainer.Panel2.Controls.Add(Me.LblTitle)
        Me.ArtworkListSplitContainer.Panel2.Controls.Add(Me.PiChkThumb)
        Me.ArtworkListSplitContainer.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.ArtworkListSplitContainer.Panel2MinSize = 220
        Me.ArtworkListSplitContainer.Size = New System.Drawing.Size(962, 539)
        Me.ArtworkListSplitContainer.SplitterDistance = 730
        Me.ArtworkListSplitContainer.TabIndex = 3
        '
        'ImageGalleryMain
        '
        Me.ImageGalleryMain.AutoScroll = True
        Me.ImageGalleryMain.BackColor = System.Drawing.Color.White
        Me.ImageGalleryMain.BadgeColor = System.Drawing.Color.Red
        Me.ImageGalleryMain.DisplayMode = FurryArtStudio.GalleryDisplayMode.Normal
        Me.ImageGalleryMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ImageGalleryMain.Location = New System.Drawing.Point(3, 25)
        Me.ImageGalleryMain.MaxItemSize = 240
        Me.ImageGalleryMain.MinItemSize = 120
        Me.ImageGalleryMain.Name = "ImageGalleryMain"
        Me.ImageGalleryMain.PageSize = 100
        Me.ImageGalleryMain.SelectionAccentColor = System.Drawing.Color.Blue
        Me.ImageGalleryMain.Size = New System.Drawing.Size(727, 514)
        Me.ImageGalleryMain.TabIndex = 3
        '
        'LblNotes
        '
        Me.LblNotes.Dock = System.Windows.Forms.DockStyle.Top
        Me.LblNotes.Location = New System.Drawing.Point(0, 305)
        Me.LblNotes.Name = "LblNotes"
        Me.LblNotes.Size = New System.Drawing.Size(225, 120)
        Me.LblNotes.TabIndex = 7
        Me.LblNotes.Text = "备注"
        '
        'LblTags
        '
        Me.LblTags.Dock = System.Windows.Forms.DockStyle.Top
        Me.LblTags.Location = New System.Drawing.Point(0, 285)
        Me.LblTags.Name = "LblTags"
        Me.LblTags.Size = New System.Drawing.Size(225, 20)
        Me.LblTags.TabIndex = 4
        Me.LblTags.Text = "标签"
        Me.LblTags.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCharacters
        '
        Me.LblCharacters.Dock = System.Windows.Forms.DockStyle.Top
        Me.LblCharacters.Location = New System.Drawing.Point(0, 265)
        Me.LblCharacters.Name = "LblCharacters"
        Me.LblCharacters.Size = New System.Drawing.Size(225, 20)
        Me.LblCharacters.TabIndex = 3
        Me.LblCharacters.Text = "角色"
        Me.LblCharacters.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblAuthor
        '
        Me.LblAuthor.Dock = System.Windows.Forms.DockStyle.Top
        Me.LblAuthor.Location = New System.Drawing.Point(0, 245)
        Me.LblAuthor.Name = "LblAuthor"
        Me.LblAuthor.Size = New System.Drawing.Size(225, 20)
        Me.LblAuthor.TabIndex = 2
        Me.LblAuthor.Text = "作者"
        Me.LblAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTitle
        '
        Me.LblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.LblTitle.Location = New System.Drawing.Point(0, 220)
        Me.LblTitle.Name = "LblTitle"
        Me.LblTitle.Size = New System.Drawing.Size(225, 25)
        Me.LblTitle.TabIndex = 1
        Me.LblTitle.Text = "标题"
        Me.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StaStrip
        '
        Me.StaStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StaStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabel, Me.TSSep1, Me.SearchStatusLabel, Me.TSspace, Me.PageStatusLabel, Me.TSSep2, Me.ArtworkStatusLabel, Me.TSSep3, Me.SelectStatusLabel, Me.TSSep4, Me.StorageStatusLabel, Me.TSSep5})
        Me.StaStrip.Location = New System.Drawing.Point(0, 567)
        Me.StaStrip.Name = "StaStrip"
        Me.StaStrip.Size = New System.Drawing.Size(962, 26)
        Me.StaStrip.TabIndex = 8
        Me.StaStrip.Text = "StatusStrip1"
        '
        'StatusLabel
        '
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(39, 20)
        Me.StatusLabel.Text = "就绪"
        '
        'TSSep1
        '
        Me.TSSep1.Name = "TSSep1"
        Me.TSSep1.Size = New System.Drawing.Size(13, 20)
        Me.TSSep1.Text = "|"
        '
        'SearchStatusLabel
        '
        Me.SearchStatusLabel.Name = "SearchStatusLabel"
        Me.SearchStatusLabel.Size = New System.Drawing.Size(108, 20)
        Me.SearchStatusLabel.Text = "已搜到0个稿件"
        '
        'TSspace
        '
        Me.TSspace.Name = "TSspace"
        Me.TSspace.Size = New System.Drawing.Size(349, 20)
        Me.TSspace.Spring = True
        Me.TSspace.Text = " "
        '
        'PageStatusLabel
        '
        Me.PageStatusLabel.Name = "PageStatusLabel"
        Me.PageStatusLabel.Size = New System.Drawing.Size(71, 20)
        Me.PageStatusLabel.Text = "页码: 0/0"
        '
        'TSSep2
        '
        Me.TSSep2.Name = "TSSep2"
        Me.TSSep2.Size = New System.Drawing.Size(13, 20)
        Me.TSSep2.Text = "|"
        '
        'ArtworkStatusLabel
        '
        Me.ArtworkStatusLabel.Name = "ArtworkStatusLabel"
        Me.ArtworkStatusLabel.Size = New System.Drawing.Size(91, 20)
        Me.ArtworkStatusLabel.Text = "<无稿件库>"
        '
        'TSSep3
        '
        Me.TSSep3.Name = "TSSep3"
        Me.TSSep3.Size = New System.Drawing.Size(13, 20)
        Me.TSSep3.Text = "|"
        '
        'SelectStatusLabel
        '
        Me.SelectStatusLabel.Name = "SelectStatusLabel"
        Me.SelectStatusLabel.Size = New System.Drawing.Size(91, 20)
        Me.SelectStatusLabel.Text = "<没有稿件>"
        '
        'TSSep4
        '
        Me.TSSep4.Name = "TSSep4"
        Me.TSSep4.Size = New System.Drawing.Size(13, 20)
        Me.TSSep4.Text = "|"
        '
        'StorageStatusLabel
        '
        Me.StorageStatusLabel.Name = "StorageStatusLabel"
        Me.StorageStatusLabel.Size = New System.Drawing.Size(133, 20)
        Me.StorageStatusLabel.Text = "存储: 0B (0个文件)"
        '
        'TSSep5
        '
        Me.TSSep5.Name = "TSSep5"
        Me.TSSep5.Size = New System.Drawing.Size(13, 20)
        Me.TSSep5.Text = " "
        '
        'KryptonMgrMain
        '
        Me.KryptonMgrMain.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.KryptonMgrMain.ToolkitStrings.MessageBoxStrings.LessDetails = "L&ess Details..."
        Me.KryptonMgrMain.ToolkitStrings.MessageBoxStrings.MoreDetails = "&More Details..."
        '
        'ConMenu
        '
        Me.ConMenu.Font = New System.Drawing.Font("Microsoft YaHei UI", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        Me.ConMenu.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ConMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConMnuMsView, Me.ConMnuMsEdit, Me.ConMnuMsExport, Me.ConMnuMsPrint, Me.ConMnuMsDelete, Me.ToolStripSeparator14, Me.ConMnuMsOpenFolder, Me.ConMnuMsCopy, Me.ConMnuMsCopyPath})
        Me.ConMenu.Name = "ConMenu"
        Me.ConMenu.Size = New System.Drawing.Size(230, 202)
        '
        'ConMnuMsView
        '
        Me.ConMnuMsView.Name = "ConMnuMsView"
        Me.ConMnuMsView.ShortcutKeyDisplayString = "Enter"
        Me.ConMnuMsView.Size = New System.Drawing.Size(229, 24)
        Me.ConMnuMsView.Text = "预览(&V)"
        '
        'ConMnuMsEdit
        '
        Me.ConMnuMsEdit.Name = "ConMnuMsEdit"
        Me.ConMnuMsEdit.ShortcutKeyDisplayString = "F2"
        Me.ConMnuMsEdit.Size = New System.Drawing.Size(229, 24)
        Me.ConMnuMsEdit.Text = "编辑(&E)..."
        '
        'ConMnuMsExport
        '
        Me.ConMnuMsExport.Name = "ConMnuMsExport"
        Me.ConMnuMsExport.ShortcutKeyDisplayString = "Ctrl+E"
        Me.ConMnuMsExport.Size = New System.Drawing.Size(229, 24)
        Me.ConMnuMsExport.Text = "导出(&X)..."
        '
        'ConMnuMsPrint
        '
        Me.ConMnuMsPrint.Name = "ConMnuMsPrint"
        Me.ConMnuMsPrint.ShortcutKeyDisplayString = "Ctrl+P"
        Me.ConMnuMsPrint.Size = New System.Drawing.Size(229, 24)
        Me.ConMnuMsPrint.Text = "打印(&P)..."
        '
        'ConMnuMsDelete
        '
        Me.ConMnuMsDelete.Name = "ConMnuMsDelete"
        Me.ConMnuMsDelete.ShortcutKeyDisplayString = "Delete"
        Me.ConMnuMsDelete.Size = New System.Drawing.Size(229, 24)
        Me.ConMnuMsDelete.Text = "删除(&D)..."
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(226, 6)
        '
        'ConMnuMsOpenFolder
        '
        Me.ConMnuMsOpenFolder.Name = "ConMnuMsOpenFolder"
        Me.ConMnuMsOpenFolder.ShortcutKeyDisplayString = "Ctrl+T"
        Me.ConMnuMsOpenFolder.Size = New System.Drawing.Size(229, 24)
        Me.ConMnuMsOpenFolder.Text = "打开目录(&A)"
        '
        'ConMnuMsCopy
        '
        Me.ConMnuMsCopy.Name = "ConMnuMsCopy"
        Me.ConMnuMsCopy.ShortcutKeyDisplayString = "Ctrl+C"
        Me.ConMnuMsCopy.Size = New System.Drawing.Size(229, 24)
        Me.ConMnuMsCopy.Text = "复制文件夹(&C)"
        '
        'ConMnuMsCopyPath
        '
        Me.ConMnuMsCopyPath.Name = "ConMnuMsCopyPath"
        Me.ConMnuMsCopyPath.Size = New System.Drawing.Size(229, 24)
        Me.ConMnuMsCopyPath.Text = "复制目录路径(&T)"
        '
        'PiChkThumb
        '
        Me.PiChkThumb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PiChkThumb.Dock = System.Windows.Forms.DockStyle.Top
        Me.PiChkThumb.Location = New System.Drawing.Point(0, 0)
        Me.PiChkThumb.Name = "PiChkThumb"
        Me.PiChkThumb.Size = New System.Drawing.Size(225, 220)
        Me.PiChkThumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PiChkThumb.TabIndex = 0
        Me.PiChkThumb.TabStop = False
        '
        'MnuOnTop
        '
        Me.MnuOnTop.Image = CType(resources.GetObject("MnuOnTop.Image"), System.Drawing.Image)
        Me.MnuOnTop.Name = "MnuOnTop"
        Me.MnuOnTop.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.MnuOnTop.Size = New System.Drawing.Size(235, 26)
        Me.MnuOnTop.Text = "窗口置顶(&T)"
        '
        'MnuDevTools
        '
        Me.MnuDevTools.Image = CType(resources.GetObject("MnuDevTools.Image"), System.Drawing.Image)
        Me.MnuDevTools.Name = "MnuDevTools"
        Me.MnuDevTools.ShortcutKeys = System.Windows.Forms.Keys.F12
        Me.MnuDevTools.Size = New System.Drawing.Size(235, 26)
        Me.MnuDevTools.Text = "开发者工具(&D)..."
        Me.MnuDevTools.Visible = False
        '
        'MnuRunAsElevated
        '
        Me.MnuRunAsElevated.Image = CType(resources.GetObject("MnuRunAsElevated.Image"), System.Drawing.Image)
        Me.MnuRunAsElevated.Name = "MnuRunAsElevated"
        Me.MnuRunAsElevated.Size = New System.Drawing.Size(235, 26)
        Me.MnuRunAsElevated.Text = "以管理员权限重启(&R)"
        Me.MnuRunAsElevated.Visible = False
        '
        'MnuRunTerminal
        '
        Me.MnuRunTerminal.Image = CType(resources.GetObject("MnuRunTerminal.Image"), System.Drawing.Image)
        Me.MnuRunTerminal.Name = "MnuRunTerminal"
        Me.MnuRunTerminal.Size = New System.Drawing.Size(235, 26)
        Me.MnuRunTerminal.Text = "在此处打开终端(&T)"
        '
        'MnuCreateShortcut
        '
        Me.MnuCreateShortcut.Image = CType(resources.GetObject("MnuCreateShortcut.Image"), System.Drawing.Image)
        Me.MnuCreateShortcut.Name = "MnuCreateShortcut"
        Me.MnuCreateShortcut.Size = New System.Drawing.Size(235, 26)
        Me.MnuCreateShortcut.Text = "创建桌面快捷方式(&S)"
        '
        'MnuProperties
        '
        Me.MnuProperties.Image = CType(resources.GetObject("MnuProperties.Image"), System.Drawing.Image)
        Me.MnuProperties.Name = "MnuProperties"
        Me.MnuProperties.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.K), System.Windows.Forms.Keys)
        Me.MnuProperties.Size = New System.Drawing.Size(235, 26)
        Me.MnuProperties.Text = "选项(&O)..."
        '
        'MnuExit
        '
        Me.MnuExit.Image = CType(resources.GetObject("MnuExit.Image"), System.Drawing.Image)
        Me.MnuExit.Name = "MnuExit"
        Me.MnuExit.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.MnuExit.Size = New System.Drawing.Size(235, 26)
        Me.MnuExit.Text = "退出(&X)"
        '
        'MnuLibList
        '
        Me.MnuLibList.Image = CType(resources.GetObject("MnuLibList.Image"), System.Drawing.Image)
        Me.MnuLibList.Name = "MnuLibList"
        Me.MnuLibList.Size = New System.Drawing.Size(258, 26)
        Me.MnuLibList.Text = "当前库(&U)"
        '
        'MnuLibRefresh
        '
        Me.MnuLibRefresh.Image = CType(resources.GetObject("MnuLibRefresh.Image"), System.Drawing.Image)
        Me.MnuLibRefresh.Name = "MnuLibRefresh"
        Me.MnuLibRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.MnuLibRefresh.Size = New System.Drawing.Size(258, 26)
        Me.MnuLibRefresh.Text = "刷新(&R)"
        '
        'MnuLibNew
        '
        Me.MnuLibNew.Image = CType(resources.GetObject("MnuLibNew.Image"), System.Drawing.Image)
        Me.MnuLibNew.Name = "MnuLibNew"
        Me.MnuLibNew.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.MnuLibNew.Size = New System.Drawing.Size(258, 26)
        Me.MnuLibNew.Text = "新建(&N)..."
        '
        'MnuLibImport
        '
        Me.MnuLibImport.Image = CType(resources.GetObject("MnuLibImport.Image"), System.Drawing.Image)
        Me.MnuLibImport.Name = "MnuLibImport"
        Me.MnuLibImport.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.MnuLibImport.Size = New System.Drawing.Size(258, 26)
        Me.MnuLibImport.Text = "导入(&I)..."
        '
        'MnuLibExport
        '
        Me.MnuLibExport.Image = CType(resources.GetObject("MnuLibExport.Image"), System.Drawing.Image)
        Me.MnuLibExport.Name = "MnuLibExport"
        Me.MnuLibExport.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.MnuLibExport.Size = New System.Drawing.Size(258, 26)
        Me.MnuLibExport.Text = "导出(&E)..."
        '
        'MnuLibExportCSV
        '
        Me.MnuLibExportCSV.Image = CType(resources.GetObject("MnuLibExportCSV.Image"), System.Drawing.Image)
        Me.MnuLibExportCSV.Name = "MnuLibExportCSV"
        Me.MnuLibExportCSV.Size = New System.Drawing.Size(258, 26)
        Me.MnuLibExportCSV.Text = "导出为 CSV 文件(&V)..."
        '
        'MnuLibClone
        '
        Me.MnuLibClone.Image = CType(resources.GetObject("MnuLibClone.Image"), System.Drawing.Image)
        Me.MnuLibClone.Name = "MnuLibClone"
        Me.MnuLibClone.Size = New System.Drawing.Size(258, 26)
        Me.MnuLibClone.Text = "克隆(&L)..."
        '
        'MnuLibOpenFolder
        '
        Me.MnuLibOpenFolder.Image = CType(resources.GetObject("MnuLibOpenFolder.Image"), System.Drawing.Image)
        Me.MnuLibOpenFolder.Name = "MnuLibOpenFolder"
        Me.MnuLibOpenFolder.Size = New System.Drawing.Size(258, 26)
        Me.MnuLibOpenFolder.Text = "打开目录(&O)"
        '
        'MnuLibCopy
        '
        Me.MnuLibCopy.Image = CType(resources.GetObject("MnuLibCopy.Image"), System.Drawing.Image)
        Me.MnuLibCopy.Name = "MnuLibCopy"
        Me.MnuLibCopy.Size = New System.Drawing.Size(258, 26)
        Me.MnuLibCopy.Text = "复制文件夹(&C)"
        '
        'MnuLibClose
        '
        Me.MnuLibClose.Image = CType(resources.GetObject("MnuLibClose.Image"), System.Drawing.Image)
        Me.MnuLibClose.Name = "MnuLibClose"
        Me.MnuLibClose.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.MnuLibClose.Size = New System.Drawing.Size(258, 26)
        Me.MnuLibClose.Text = "关闭(&C)"
        '
        'MnuLibRename
        '
        Me.MnuLibRename.Image = CType(resources.GetObject("MnuLibRename.Image"), System.Drawing.Image)
        Me.MnuLibRename.Name = "MnuLibRename"
        Me.MnuLibRename.ShortcutKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F2), System.Windows.Forms.Keys)
        Me.MnuLibRename.Size = New System.Drawing.Size(258, 26)
        Me.MnuLibRename.Text = "重命名(&M)..."
        '
        'MnuLibDelete
        '
        Me.MnuLibDelete.Image = CType(resources.GetObject("MnuLibDelete.Image"), System.Drawing.Image)
        Me.MnuLibDelete.Name = "MnuLibDelete"
        Me.MnuLibDelete.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Delete), System.Windows.Forms.Keys)
        Me.MnuLibDelete.Size = New System.Drawing.Size(258, 26)
        Me.MnuLibDelete.Text = "删除(&D)..."
        '
        'MnuLibStatistics
        '
        Me.MnuLibStatistics.Image = CType(resources.GetObject("MnuLibStatistics.Image"), System.Drawing.Image)
        Me.MnuLibStatistics.Name = "MnuLibStatistics"
        Me.MnuLibStatistics.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.MnuLibStatistics.Size = New System.Drawing.Size(258, 26)
        Me.MnuLibStatistics.Text = "统计信息(&S)..."
        '
        'MnuMsNew
        '
        Me.MnuMsNew.Image = CType(resources.GetObject("MnuMsNew.Image"), System.Drawing.Image)
        Me.MnuMsNew.Name = "MnuMsNew"
        Me.MnuMsNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.MnuMsNew.Size = New System.Drawing.Size(243, 26)
        Me.MnuMsNew.Text = "新建(&N)..."
        '
        'MnuMsImport
        '
        Me.MnuMsImport.Image = CType(resources.GetObject("MnuMsImport.Image"), System.Drawing.Image)
        Me.MnuMsImport.Name = "MnuMsImport"
        Me.MnuMsImport.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.MnuMsImport.Size = New System.Drawing.Size(243, 26)
        Me.MnuMsImport.Text = "导入(&I)..."
        '
        'MnuMsView
        '
        Me.MnuMsView.Image = CType(resources.GetObject("MnuMsView.Image"), System.Drawing.Image)
        Me.MnuMsView.Name = "MnuMsView"
        Me.MnuMsView.ShortcutKeyDisplayString = "Enter"
        Me.MnuMsView.Size = New System.Drawing.Size(243, 26)
        Me.MnuMsView.Text = "预览(&V)"
        '
        'MnuMsEdit
        '
        Me.MnuMsEdit.Image = CType(resources.GetObject("MnuMsEdit.Image"), System.Drawing.Image)
        Me.MnuMsEdit.Name = "MnuMsEdit"
        Me.MnuMsEdit.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.MnuMsEdit.Size = New System.Drawing.Size(243, 26)
        Me.MnuMsEdit.Text = "编辑(&E)..."
        '
        'MnuMsExport
        '
        Me.MnuMsExport.Image = CType(resources.GetObject("MnuMsExport.Image"), System.Drawing.Image)
        Me.MnuMsExport.Name = "MnuMsExport"
        Me.MnuMsExport.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.MnuMsExport.Size = New System.Drawing.Size(243, 26)
        Me.MnuMsExport.Text = "导出(&X)..."
        '
        'MnuMsPrint
        '
        Me.MnuMsPrint.Image = CType(resources.GetObject("MnuMsPrint.Image"), System.Drawing.Image)
        Me.MnuMsPrint.Name = "MnuMsPrint"
        Me.MnuMsPrint.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.MnuMsPrint.Size = New System.Drawing.Size(243, 26)
        Me.MnuMsPrint.Text = "打印(&P)..."
        '
        'MnuMsDelete
        '
        Me.MnuMsDelete.Image = CType(resources.GetObject("MnuMsDelete.Image"), System.Drawing.Image)
        Me.MnuMsDelete.Name = "MnuMsDelete"
        Me.MnuMsDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.MnuMsDelete.Size = New System.Drawing.Size(243, 26)
        Me.MnuMsDelete.Text = "删除(&D)..."
        '
        'MnuMsOpenFolder
        '
        Me.MnuMsOpenFolder.Image = CType(resources.GetObject("MnuMsOpenFolder.Image"), System.Drawing.Image)
        Me.MnuMsOpenFolder.Name = "MnuMsOpenFolder"
        Me.MnuMsOpenFolder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.MnuMsOpenFolder.Size = New System.Drawing.Size(243, 26)
        Me.MnuMsOpenFolder.Text = "打开目录(&O)"
        '
        'MnuMsCopy
        '
        Me.MnuMsCopy.Image = CType(resources.GetObject("MnuMsCopy.Image"), System.Drawing.Image)
        Me.MnuMsCopy.Name = "MnuMsCopy"
        Me.MnuMsCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.MnuMsCopy.Size = New System.Drawing.Size(243, 26)
        Me.MnuMsCopy.Text = "复制文件夹(&C)"
        '
        'MnuViewPlay
        '
        Me.MnuViewPlay.Image = CType(resources.GetObject("MnuViewPlay.Image"), System.Drawing.Image)
        Me.MnuViewPlay.Name = "MnuViewPlay"
        Me.MnuViewPlay.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F5), System.Windows.Forms.Keys)
        Me.MnuViewPlay.Size = New System.Drawing.Size(283, 26)
        Me.MnuViewPlay.Text = "幻灯片放映(&P)"
        '
        'MnuSearch
        '
        Me.MnuSearch.Image = CType(resources.GetObject("MnuSearch.Image"), System.Drawing.Image)
        Me.MnuSearch.Name = "MnuSearch"
        Me.MnuSearch.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.MnuSearch.Size = New System.Drawing.Size(283, 26)
        Me.MnuSearch.Text = "搜索(&F)"
        '
        'MnuPageUp
        '
        Me.MnuPageUp.Image = CType(resources.GetObject("MnuPageUp.Image"), System.Drawing.Image)
        Me.MnuPageUp.Name = "MnuPageUp"
        Me.MnuPageUp.ShortcutKeyDisplayString = "PageUp"
        Me.MnuPageUp.Size = New System.Drawing.Size(283, 26)
        Me.MnuPageUp.Text = "上一页(&E)"
        '
        'MnuPageDown
        '
        Me.MnuPageDown.Image = CType(resources.GetObject("MnuPageDown.Image"), System.Drawing.Image)
        Me.MnuPageDown.Name = "MnuPageDown"
        Me.MnuPageDown.ShortcutKeyDisplayString = "PageDown"
        Me.MnuPageDown.Size = New System.Drawing.Size(283, 26)
        Me.MnuPageDown.Text = "下一页(&N)"
        '
        'MnuHelpTutorial
        '
        Me.MnuHelpTutorial.Image = CType(resources.GetObject("MnuHelpTutorial.Image"), System.Drawing.Image)
        Me.MnuHelpTutorial.Name = "MnuHelpTutorial"
        Me.MnuHelpTutorial.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.MnuHelpTutorial.Size = New System.Drawing.Size(224, 26)
        Me.MnuHelpTutorial.Text = "教程(&T)"
        '
        'MnuHelpWebsite
        '
        Me.MnuHelpWebsite.Image = CType(resources.GetObject("MnuHelpWebsite.Image"), System.Drawing.Image)
        Me.MnuHelpWebsite.Name = "MnuHelpWebsite"
        Me.MnuHelpWebsite.Size = New System.Drawing.Size(224, 26)
        Me.MnuHelpWebsite.Text = "官网(&W)"
        '
        'MnuOpenAfdian
        '
        Me.MnuOpenAfdian.Image = CType(resources.GetObject("MnuOpenAfdian.Image"), System.Drawing.Image)
        Me.MnuOpenAfdian.Name = "MnuOpenAfdian"
        Me.MnuOpenAfdian.Size = New System.Drawing.Size(224, 26)
        Me.MnuOpenAfdian.Text = "打开爱发电(&A)"
        '
        'MnuBugReport
        '
        Me.MnuBugReport.Image = CType(resources.GetObject("MnuBugReport.Image"), System.Drawing.Image)
        Me.MnuBugReport.Name = "MnuBugReport"
        Me.MnuBugReport.Size = New System.Drawing.Size(224, 26)
        Me.MnuBugReport.Text = "报告问题(&B)"
        '
        'MnuHelpGithub
        '
        Me.MnuHelpGithub.Image = CType(resources.GetObject("MnuHelpGithub.Image"), System.Drawing.Image)
        Me.MnuHelpGithub.Name = "MnuHelpGithub"
        Me.MnuHelpGithub.Size = New System.Drawing.Size(224, 26)
        Me.MnuHelpGithub.Text = "&GitHub"
        '
        'MnuCheckUpdate
        '
        Me.MnuCheckUpdate.Image = CType(resources.GetObject("MnuCheckUpdate.Image"), System.Drawing.Image)
        Me.MnuCheckUpdate.Name = "MnuCheckUpdate"
        Me.MnuCheckUpdate.Size = New System.Drawing.Size(224, 26)
        Me.MnuCheckUpdate.Text = "检查更新(&U)"
        '
        'MnuHelpWhatsNew
        '
        Me.MnuHelpWhatsNew.Image = CType(resources.GetObject("MnuHelpWhatsNew.Image"), System.Drawing.Image)
        Me.MnuHelpWhatsNew.Name = "MnuHelpWhatsNew"
        Me.MnuHelpWhatsNew.Size = New System.Drawing.Size(224, 26)
        Me.MnuHelpWhatsNew.Text = "新增功能(&H)"
        '
        'MnuHelpAbout
        '
        Me.MnuHelpAbout.Image = CType(resources.GetObject("MnuHelpAbout.Image"), System.Drawing.Image)
        Me.MnuHelpAbout.Name = "MnuHelpAbout"
        Me.MnuHelpAbout.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F1), System.Windows.Forms.Keys)
        Me.MnuHelpAbout.Size = New System.Drawing.Size(224, 26)
        Me.MnuHelpAbout.Text = "关于(&A)..."
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(962, 593)
        Me.Controls.Add(Me.ArtworkListSplitContainer)
        Me.Controls.Add(Me.MnuStrip)
        Me.Controls.Add(Me.StaStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MnuStrip
        Me.MinimumSize = New System.Drawing.Size(400, 250)
        Me.Name = "MainForm"
        Me.Text = "FurryArtStudio"
        Me.MnuStrip.ResumeLayout(False)
        Me.MnuStrip.PerformLayout()
        Me.ArtworkListSplitContainer.Panel1.ResumeLayout(False)
        Me.ArtworkListSplitContainer.Panel1.PerformLayout()
        Me.ArtworkListSplitContainer.Panel2.ResumeLayout(False)
        CType(Me.ArtworkListSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ArtworkListSplitContainer.ResumeLayout(False)
        Me.StaStrip.ResumeLayout(False)
        Me.StaStrip.PerformLayout()
        Me.ConMenu.ResumeLayout(False)
        CType(Me.PiChkThumb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MnuStrip As MenuStrip
    Friend WithEvents MnuLibrary As ToolStripMenuItem
    Friend WithEvents MnuManuscript As ToolStripMenuItem
    Friend WithEvents MnuMsView As ToolStripMenuItem
    Friend WithEvents MnuMsEdit As ToolStripMenuItem
    Friend WithEvents MnuMsDelete As ToolStripMenuItem
    Friend WithEvents MnuHelp As ToolStripMenuItem
    Friend WithEvents MnuLibNew As ToolStripMenuItem
    Friend WithEvents MnuLibImport As ToolStripMenuItem
    Friend WithEvents MnuLibExport As ToolStripMenuItem
    Friend WithEvents MnuFile As ToolStripMenuItem
    Friend WithEvents MnuProperties As ToolStripMenuItem
    Friend WithEvents MnuExit As ToolStripMenuItem
    Friend WithEvents MnuMsNew As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents MnuLibClone As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents MnuMsOpenFolder As ToolStripMenuItem
    Friend WithEvents MnuLibOpenFolder As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents MnuLibDelete As ToolStripMenuItem
    Friend WithEvents MnuLibRename As ToolStripMenuItem
    Friend WithEvents MnuLibRefresh As ToolStripMenuItem
    Friend WithEvents MnuLibCopy As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents MnuHelpAbout As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents MnuHelpWebsite As ToolStripMenuItem
    Friend WithEvents MnuHelpGithub As ToolStripMenuItem
    Friend WithEvents MnuLibList As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents SearchTextBox As TextBox
    Friend WithEvents ArtworkListSplitContainer As SplitContainer
    Friend WithEvents MnuHelpTutorial As ToolStripMenuItem
    Friend WithEvents MnuMsExport As ToolStripMenuItem
    Friend WithEvents MnuMsPrint As ToolStripMenuItem
    Friend WithEvents MnuLibCopyPath As ToolStripMenuItem
    Friend WithEvents MnuMsCopy As ToolStripMenuItem
    Friend WithEvents MnuMsCopyPath As ToolStripMenuItem
    Friend WithEvents MnuMsImport As ToolStripMenuItem
    Friend WithEvents MnuLibStatistics As ToolStripMenuItem
    Friend WithEvents MnuLibClose As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents StaStrip As StatusStrip
    Friend WithEvents StatusLabel As ToolStripStatusLabel
    Friend WithEvents TSspace As ToolStripStatusLabel
    Friend WithEvents ArtworkStatusLabel As ToolStripStatusLabel
    Friend WithEvents SelectStatusLabel As ToolStripStatusLabel
    Friend WithEvents StorageStatusLabel As ToolStripStatusLabel
    Friend WithEvents TSSep2 As ToolStripStatusLabel
    Friend WithEvents TSSep3 As ToolStripStatusLabel
    Friend WithEvents TSSep4 As ToolStripStatusLabel
    Friend WithEvents ImageGalleryMain As ImageGallery
    Friend WithEvents PiChkThumb As PictureBox
    Friend WithEvents PageStatusLabel As ToolStripStatusLabel
    Friend WithEvents TSSep1 As ToolStripStatusLabel
    Friend WithEvents LblTitle As Label
    Friend WithEvents LblAuthor As Label
    Friend WithEvents LblNotes As Label
    Friend WithEvents LblTags As Label
    Friend WithEvents LblCharacters As Label
    Friend WithEvents MnuViews As ToolStripMenuItem
    Friend WithEvents MnuSelectAll As ToolStripMenuItem
    Friend WithEvents MnuSelectReverse As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As ToolStripSeparator
    Friend WithEvents MnuSearch As ToolStripMenuItem
    Friend WithEvents MnuAdvancedSearch As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As ToolStripSeparator
    Friend WithEvents MnuViewPlay As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents MnuPageUp As ToolStripMenuItem
    Friend WithEvents MnuPageDown As ToolStripMenuItem
    Friend WithEvents MnuHelpPrivacy As ToolStripMenuItem
    Friend WithEvents MnuHelpLicense As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As ToolStripSeparator
    Friend WithEvents KryptonMgrMain As Krypton.Toolkit.KryptonManager
    Friend WithEvents TSSep5 As ToolStripStatusLabel
    Friend WithEvents SearchStatusLabel As ToolStripStatusLabel
    Friend WithEvents MnuLibExportCSV As ToolStripMenuItem
    Friend WithEvents MnuDevTools As ToolStripMenuItem
    Friend WithEvents MnuCheckUpdate As ToolStripMenuItem
    Friend WithEvents MnuOnTop As ToolStripMenuItem
    Friend WithEvents MnuHelpWhatsNew As ToolStripMenuItem
    Friend WithEvents MnuRunAsElevated As ToolStripMenuItem
    Friend WithEvents MnuRunTerminal As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents MnuTerms As ToolStripMenuItem
    Friend WithEvents ConMenu As ContextMenuStrip
    Friend WithEvents ConMnuMsView As ToolStripMenuItem
    Friend WithEvents ConMnuMsEdit As ToolStripMenuItem
    Friend WithEvents ConMnuMsExport As ToolStripMenuItem
    Friend WithEvents ConMnuMsPrint As ToolStripMenuItem
    Friend WithEvents ConMnuMsDelete As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator14 As ToolStripSeparator
    Friend WithEvents ConMnuMsOpenFolder As ToolStripMenuItem
    Friend WithEvents ConMnuMsCopy As ToolStripMenuItem
    Friend WithEvents ConMnuMsCopyPath As ToolStripMenuItem
    Friend WithEvents MnuHelpDonate As ToolStripMenuItem
    Friend WithEvents MnuOpenAfdian As ToolStripMenuItem
    Friend WithEvents MnuLoadSponsors As ToolStripMenuItem
    Friend WithEvents MnuCreateShortcut As ToolStripMenuItem
    Friend WithEvents MnuFeedback As ToolStripMenuItem
    Friend WithEvents MnuBugReport As ToolStripMenuItem
    Friend WithEvents MnuMyFeedback As ToolStripMenuItem
    Friend WithEvents MnuSuggestions As ToolStripMenuItem
End Class