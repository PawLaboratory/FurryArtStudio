<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PropertiesForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BtnExport = New System.Windows.Forms.Button()
        Me.BtnImport = New System.Windows.Forms.Button()
        Me.BtnReset = New System.Windows.Forms.Button()
        Me.GrpAppearance = New System.Windows.Forms.GroupBox()
        Me.ChkMenuUpper = New System.Windows.Forms.CheckBox()
        Me.ChkShowThemeColor = New System.Windows.Forms.CheckBox()
        Me.LblLanguage = New System.Windows.Forms.Label()
        Me.CboLang = New System.Windows.Forms.ComboBox()
        Me.LblCorColor = New System.Windows.Forms.Label()
        Me.LblSelColor = New System.Windows.Forms.Label()
        Me.BtnCornerColor = New System.Windows.Forms.Button()
        Me.BtnSelectedColor = New System.Windows.Forms.Button()
        Me.LblCornerColor = New System.Windows.Forms.Label()
        Me.LblSelectedColor = New System.Windows.Forms.Label()
        Me.LblThemeColor = New System.Windows.Forms.Label()
        Me.BtnThemeColor = New System.Windows.Forms.Button()
        Me.ChkShowInChild = New System.Windows.Forms.CheckBox()
        Me.RadSystem = New System.Windows.Forms.RadioButton()
        Me.RadDark = New System.Windows.Forms.RadioButton()
        Me.RadLight = New System.Windows.Forms.RadioButton()
        Me.LblTheme = New System.Windows.Forms.Label()
        Me.GrpStartup = New System.Windows.Forms.GroupBox()
        Me.ChkRestore = New System.Windows.Forms.CheckBox()
        Me.CboCheckUpdate = New System.Windows.Forms.ComboBox()
        Me.ChkAutoCheckUpdate = New System.Windows.Forms.CheckBox()
        Me.ChkAutoPlay = New System.Windows.Forms.CheckBox()
        Me.ChkAutoStart = New System.Windows.Forms.CheckBox()
        Me.GrpAdvanced = New System.Windows.Forms.GroupBox()
        Me.ChkLoop = New System.Windows.Forms.CheckBox()
        Me.ChkDevTools = New System.Windows.Forms.CheckBox()
        Me.ChkPlayNext = New System.Windows.Forms.CheckBox()
        Me.TxtAutoNext = New System.Windows.Forms.TextBox()
        Me.LblAutoNext = New System.Windows.Forms.Label()
        Me.ChkFileAssociation = New System.Windows.Forms.CheckBox()
        Me.ChkKeepScale = New System.Windows.Forms.CheckBox()
        Me.TxtMaxSize = New System.Windows.Forms.TextBox()
        Me.LblThumbMax = New System.Windows.Forms.Label()
        Me.TxtMinSize = New System.Windows.Forms.TextBox()
        Me.LblThumbMin = New System.Windows.Forms.Label()
        Me.ColorDlg = New System.Windows.Forms.ColorDialog()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.ChkShowTool = New System.Windows.Forms.CheckBox()
        Me.GrpAppearance.SuspendLayout()
        Me.GrpStartup.SuspendLayout()
        Me.GrpAdvanced.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnExport
        '
        Me.BtnExport.Location = New System.Drawing.Point(136, 404)
        Me.BtnExport.Name = "BtnExport"
        Me.BtnExport.Size = New System.Drawing.Size(119, 41)
        Me.BtnExport.TabIndex = 53
        Me.BtnExport.Text = "导出(&E)..."
        Me.BtnExport.UseVisualStyleBackColor = True
        '
        'BtnImport
        '
        Me.BtnImport.Location = New System.Drawing.Point(12, 404)
        Me.BtnImport.Name = "BtnImport"
        Me.BtnImport.Size = New System.Drawing.Size(119, 41)
        Me.BtnImport.TabIndex = 52
        Me.BtnImport.Text = "导入(&I)..."
        Me.BtnImport.UseVisualStyleBackColor = True
        '
        'BtnReset
        '
        Me.BtnReset.Location = New System.Drawing.Point(562, 404)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(119, 41)
        Me.BtnReset.TabIndex = 51
        Me.BtnReset.Text = "重置(&R)"
        Me.BtnReset.UseVisualStyleBackColor = True
        '
        'GrpAppearance
        '
        Me.GrpAppearance.Controls.Add(Me.ChkShowTool)
        Me.GrpAppearance.Controls.Add(Me.ChkMenuUpper)
        Me.GrpAppearance.Controls.Add(Me.ChkShowThemeColor)
        Me.GrpAppearance.Controls.Add(Me.LblLanguage)
        Me.GrpAppearance.Controls.Add(Me.CboLang)
        Me.GrpAppearance.Controls.Add(Me.LblCorColor)
        Me.GrpAppearance.Controls.Add(Me.LblSelColor)
        Me.GrpAppearance.Controls.Add(Me.BtnCornerColor)
        Me.GrpAppearance.Controls.Add(Me.BtnSelectedColor)
        Me.GrpAppearance.Controls.Add(Me.LblCornerColor)
        Me.GrpAppearance.Controls.Add(Me.LblSelectedColor)
        Me.GrpAppearance.Controls.Add(Me.LblThemeColor)
        Me.GrpAppearance.Controls.Add(Me.BtnThemeColor)
        Me.GrpAppearance.Controls.Add(Me.ChkShowInChild)
        Me.GrpAppearance.Controls.Add(Me.RadSystem)
        Me.GrpAppearance.Controls.Add(Me.RadDark)
        Me.GrpAppearance.Controls.Add(Me.RadLight)
        Me.GrpAppearance.Controls.Add(Me.LblTheme)
        Me.GrpAppearance.Location = New System.Drawing.Point(12, 12)
        Me.GrpAppearance.Name = "GrpAppearance"
        Me.GrpAppearance.Size = New System.Drawing.Size(380, 246)
        Me.GrpAppearance.TabIndex = 55
        Me.GrpAppearance.TabStop = False
        Me.GrpAppearance.Text = "外观"
        '
        'ChkMenuUpper
        '
        Me.ChkMenuUpper.AutoSize = True
        Me.ChkMenuUpper.Location = New System.Drawing.Point(20, 217)
        Me.ChkMenuUpper.Name = "ChkMenuUpper"
        Me.ChkMenuUpper.Size = New System.Drawing.Size(119, 19)
        Me.ChkMenuUpper.TabIndex = 80
        Me.ChkMenuUpper.Text = "菜单栏全大写"
        Me.ChkMenuUpper.UseVisualStyleBackColor = True
        '
        'ChkShowThemeColor
        '
        Me.ChkShowThemeColor.AutoSize = True
        Me.ChkShowThemeColor.Location = New System.Drawing.Point(20, 58)
        Me.ChkShowThemeColor.Name = "ChkShowThemeColor"
        Me.ChkShowThemeColor.Size = New System.Drawing.Size(104, 19)
        Me.ChkShowThemeColor.TabIndex = 79
        Me.ChkShowThemeColor.Text = "显示主题色"
        Me.ChkShowThemeColor.UseVisualStyleBackColor = True
        '
        'LblLanguage
        '
        Me.LblLanguage.AutoSize = True
        Me.LblLanguage.Location = New System.Drawing.Point(17, 109)
        Me.LblLanguage.Name = "LblLanguage"
        Me.LblLanguage.Size = New System.Drawing.Size(52, 15)
        Me.LblLanguage.TabIndex = 74
        Me.LblLanguage.Text = "语言："
        '
        'CboLang
        '
        Me.CboLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboLang.FormattingEnabled = True
        Me.CboLang.Location = New System.Drawing.Point(179, 106)
        Me.CboLang.Name = "CboLang"
        Me.CboLang.Size = New System.Drawing.Size(189, 23)
        Me.CboLang.TabIndex = 73
        '
        'LblCorColor
        '
        Me.LblCorColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblCorColor.Location = New System.Drawing.Point(225, 179)
        Me.LblCorColor.Name = "LblCorColor"
        Me.LblCorColor.Size = New System.Drawing.Size(35, 35)
        Me.LblCorColor.TabIndex = 69
        '
        'LblSelColor
        '
        Me.LblSelColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSelColor.Location = New System.Drawing.Point(225, 138)
        Me.LblSelColor.Name = "LblSelColor"
        Me.LblSelColor.Size = New System.Drawing.Size(35, 35)
        Me.LblSelColor.TabIndex = 68
        '
        'BtnCornerColor
        '
        Me.BtnCornerColor.Location = New System.Drawing.Point(260, 177)
        Me.BtnCornerColor.Name = "BtnCornerColor"
        Me.BtnCornerColor.Size = New System.Drawing.Size(108, 41)
        Me.BtnCornerColor.TabIndex = 67
        Me.BtnCornerColor.Text = "颜色..."
        Me.BtnCornerColor.UseVisualStyleBackColor = True
        '
        'BtnSelectedColor
        '
        Me.BtnSelectedColor.Location = New System.Drawing.Point(260, 135)
        Me.BtnSelectedColor.Name = "BtnSelectedColor"
        Me.BtnSelectedColor.Size = New System.Drawing.Size(108, 41)
        Me.BtnSelectedColor.TabIndex = 66
        Me.BtnSelectedColor.Text = "颜色..."
        Me.BtnSelectedColor.UseVisualStyleBackColor = True
        '
        'LblCornerColor
        '
        Me.LblCornerColor.AutoSize = True
        Me.LblCornerColor.Location = New System.Drawing.Point(17, 190)
        Me.LblCornerColor.Name = "LblCornerColor"
        Me.LblCornerColor.Size = New System.Drawing.Size(97, 15)
        Me.LblCornerColor.TabIndex = 65
        Me.LblCornerColor.Text = "角标背景色："
        '
        'LblSelectedColor
        '
        Me.LblSelectedColor.AutoSize = True
        Me.LblSelectedColor.Location = New System.Drawing.Point(17, 148)
        Me.LblSelectedColor.Name = "LblSelectedColor"
        Me.LblSelectedColor.Size = New System.Drawing.Size(112, 15)
        Me.LblSelectedColor.TabIndex = 64
        Me.LblSelectedColor.Text = "选中项强调色："
        '
        'LblThemeColor
        '
        Me.LblThemeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblThemeColor.Location = New System.Drawing.Point(225, 49)
        Me.LblThemeColor.Name = "LblThemeColor"
        Me.LblThemeColor.Size = New System.Drawing.Size(35, 35)
        Me.LblThemeColor.TabIndex = 63
        '
        'BtnThemeColor
        '
        Me.BtnThemeColor.Location = New System.Drawing.Point(260, 46)
        Me.BtnThemeColor.Name = "BtnThemeColor"
        Me.BtnThemeColor.Size = New System.Drawing.Size(108, 41)
        Me.BtnThemeColor.TabIndex = 62
        Me.BtnThemeColor.Text = "颜色..."
        Me.BtnThemeColor.UseVisualStyleBackColor = True
        '
        'ChkShowInChild
        '
        Me.ChkShowInChild.AutoSize = True
        Me.ChkShowInChild.Location = New System.Drawing.Point(36, 84)
        Me.ChkShowInChild.Name = "ChkShowInChild"
        Me.ChkShowInChild.Size = New System.Drawing.Size(149, 19)
        Me.ChkShowInChild.TabIndex = 60
        Me.ChkShowInChild.Text = "子窗口使用主题色"
        Me.ChkShowInChild.UseVisualStyleBackColor = True
        '
        'RadSystem
        '
        Me.RadSystem.AutoSize = True
        Me.RadSystem.Location = New System.Drawing.Point(280, 23)
        Me.RadSystem.Name = "RadSystem"
        Me.RadSystem.Size = New System.Drawing.Size(88, 19)
        Me.RadSystem.TabIndex = 26
        Me.RadSystem.Text = "跟随系统"
        Me.RadSystem.UseVisualStyleBackColor = True
        '
        'RadDark
        '
        Me.RadDark.AutoSize = True
        Me.RadDark.Location = New System.Drawing.Point(179, 23)
        Me.RadDark.Name = "RadDark"
        Me.RadDark.Size = New System.Drawing.Size(88, 19)
        Me.RadDark.TabIndex = 25
        Me.RadDark.Text = "深色模式"
        Me.RadDark.UseVisualStyleBackColor = True
        '
        'RadLight
        '
        Me.RadLight.AutoSize = True
        Me.RadLight.Location = New System.Drawing.Point(78, 23)
        Me.RadLight.Name = "RadLight"
        Me.RadLight.Size = New System.Drawing.Size(88, 19)
        Me.RadLight.TabIndex = 24
        Me.RadLight.Text = "浅色模式"
        Me.RadLight.UseVisualStyleBackColor = True
        '
        'LblTheme
        '
        Me.LblTheme.AutoSize = True
        Me.LblTheme.Location = New System.Drawing.Point(16, 23)
        Me.LblTheme.Name = "LblTheme"
        Me.LblTheme.Size = New System.Drawing.Size(52, 15)
        Me.LblTheme.TabIndex = 23
        Me.LblTheme.Text = "主题："
        '
        'GrpStartup
        '
        Me.GrpStartup.Controls.Add(Me.ChkRestore)
        Me.GrpStartup.Controls.Add(Me.CboCheckUpdate)
        Me.GrpStartup.Controls.Add(Me.ChkAutoCheckUpdate)
        Me.GrpStartup.Controls.Add(Me.ChkAutoPlay)
        Me.GrpStartup.Controls.Add(Me.ChkAutoStart)
        Me.GrpStartup.Location = New System.Drawing.Point(12, 262)
        Me.GrpStartup.Name = "GrpStartup"
        Me.GrpStartup.Size = New System.Drawing.Size(380, 133)
        Me.GrpStartup.TabIndex = 56
        Me.GrpStartup.TabStop = False
        Me.GrpStartup.Text = "启动"
        '
        'ChkRestore
        '
        Me.ChkRestore.AutoSize = True
        Me.ChkRestore.Location = New System.Drawing.Point(10, 49)
        Me.ChkRestore.Name = "ChkRestore"
        Me.ChkRestore.Size = New System.Drawing.Size(194, 19)
        Me.ChkRestore.TabIndex = 59
        Me.ChkRestore.Text = "启动时恢复上次关闭的库"
        Me.ChkRestore.UseVisualStyleBackColor = True
        '
        'CboCheckUpdate
        '
        Me.CboCheckUpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboCheckUpdate.FormattingEnabled = True
        Me.CboCheckUpdate.Location = New System.Drawing.Point(187, 99)
        Me.CboCheckUpdate.Name = "CboCheckUpdate"
        Me.CboCheckUpdate.Size = New System.Drawing.Size(181, 23)
        Me.CboCheckUpdate.TabIndex = 58
        '
        'ChkAutoCheckUpdate
        '
        Me.ChkAutoCheckUpdate.AutoSize = True
        Me.ChkAutoCheckUpdate.Location = New System.Drawing.Point(10, 101)
        Me.ChkAutoCheckUpdate.Name = "ChkAutoCheckUpdate"
        Me.ChkAutoCheckUpdate.Size = New System.Drawing.Size(119, 19)
        Me.ChkAutoCheckUpdate.TabIndex = 57
        Me.ChkAutoCheckUpdate.Text = "自动检查更新"
        Me.ChkAutoCheckUpdate.UseVisualStyleBackColor = True
        '
        'ChkAutoPlay
        '
        Me.ChkAutoPlay.AutoSize = True
        Me.ChkAutoPlay.Location = New System.Drawing.Point(25, 74)
        Me.ChkAutoPlay.Name = "ChkAutoPlay"
        Me.ChkAutoPlay.Size = New System.Drawing.Size(179, 19)
        Me.ChkAutoPlay.TabIndex = 56
        Me.ChkAutoPlay.Text = "启动时自动播放幻灯片"
        Me.ChkAutoPlay.UseVisualStyleBackColor = True
        '
        'ChkAutoStart
        '
        Me.ChkAutoStart.AutoSize = True
        Me.ChkAutoStart.Location = New System.Drawing.Point(10, 24)
        Me.ChkAutoStart.Name = "ChkAutoStart"
        Me.ChkAutoStart.Size = New System.Drawing.Size(254, 19)
        Me.ChkAutoStart.TabIndex = 55
        Me.ChkAutoStart.Text = "开机时自动启动 FurryArtStudio"
        Me.ChkAutoStart.UseVisualStyleBackColor = True
        '
        'GrpAdvanced
        '
        Me.GrpAdvanced.Controls.Add(Me.ChkLoop)
        Me.GrpAdvanced.Controls.Add(Me.ChkDevTools)
        Me.GrpAdvanced.Controls.Add(Me.ChkPlayNext)
        Me.GrpAdvanced.Controls.Add(Me.TxtAutoNext)
        Me.GrpAdvanced.Controls.Add(Me.LblAutoNext)
        Me.GrpAdvanced.Controls.Add(Me.ChkFileAssociation)
        Me.GrpAdvanced.Controls.Add(Me.ChkKeepScale)
        Me.GrpAdvanced.Controls.Add(Me.TxtMaxSize)
        Me.GrpAdvanced.Controls.Add(Me.LblThumbMax)
        Me.GrpAdvanced.Controls.Add(Me.TxtMinSize)
        Me.GrpAdvanced.Controls.Add(Me.LblThumbMin)
        Me.GrpAdvanced.Location = New System.Drawing.Point(398, 12)
        Me.GrpAdvanced.Name = "GrpAdvanced"
        Me.GrpAdvanced.Size = New System.Drawing.Size(408, 225)
        Me.GrpAdvanced.TabIndex = 57
        Me.GrpAdvanced.TabStop = False
        Me.GrpAdvanced.Text = "高级"
        '
        'ChkLoop
        '
        Me.ChkLoop.AutoSize = True
        Me.ChkLoop.Location = New System.Drawing.Point(10, 146)
        Me.ChkLoop.Name = "ChkLoop"
        Me.ChkLoop.Size = New System.Drawing.Size(89, 19)
        Me.ChkLoop.TabIndex = 88
        Me.ChkLoop.Text = "循环播放"
        Me.ChkLoop.UseVisualStyleBackColor = True
        '
        'ChkDevTools
        '
        Me.ChkDevTools.AutoSize = True
        Me.ChkDevTools.Location = New System.Drawing.Point(10, 196)
        Me.ChkDevTools.Name = "ChkDevTools"
        Me.ChkDevTools.Size = New System.Drawing.Size(134, 19)
        Me.ChkDevTools.TabIndex = 87
        Me.ChkDevTools.Text = "显示开发者选项"
        Me.ChkDevTools.UseVisualStyleBackColor = True
        '
        'ChkPlayNext
        '
        Me.ChkPlayNext.AutoSize = True
        Me.ChkPlayNext.Location = New System.Drawing.Point(25, 171)
        Me.ChkPlayNext.Name = "ChkPlayNext"
        Me.ChkPlayNext.Size = New System.Drawing.Size(179, 19)
        Me.ChkPlayNext.TabIndex = 86
        Me.ChkPlayNext.Text = "播放后切换到下一个库"
        Me.ChkPlayNext.UseVisualStyleBackColor = True
        '
        'TxtAutoNext
        '
        Me.TxtAutoNext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAutoNext.Location = New System.Drawing.Point(212, 111)
        Me.TxtAutoNext.Name = "TxtAutoNext"
        Me.TxtAutoNext.Size = New System.Drawing.Size(179, 25)
        Me.TxtAutoNext.TabIndex = 83
        '
        'LblAutoNext
        '
        Me.LblAutoNext.AutoSize = True
        Me.LblAutoNext.Location = New System.Drawing.Point(8, 117)
        Me.LblAutoNext.Name = "LblAutoNext"
        Me.LblAutoNext.Size = New System.Drawing.Size(181, 15)
        Me.LblAutoNext.TabIndex = 82
        Me.LblAutoNext.Text = "自动切换时间(单位:秒)："
        '
        'ChkFileAssociation
        '
        Me.ChkFileAssociation.AutoSize = True
        Me.ChkFileAssociation.Location = New System.Drawing.Point(10, 88)
        Me.ChkFileAssociation.Name = "ChkFileAssociation"
        Me.ChkFileAssociation.Size = New System.Drawing.Size(220, 19)
        Me.ChkFileAssociation.TabIndex = 81
        Me.ChkFileAssociation.Text = "关联稿件库备份文件(*.paw)"
        Me.ChkFileAssociation.UseVisualStyleBackColor = True
        '
        'ChkKeepScale
        '
        Me.ChkKeepScale.AutoSize = True
        Me.ChkKeepScale.Location = New System.Drawing.Point(10, 63)
        Me.ChkKeepScale.Name = "ChkKeepScale"
        Me.ChkKeepScale.Size = New System.Drawing.Size(194, 19)
        Me.ChkKeepScale.TabIndex = 80
        Me.ChkKeepScale.Text = "图片查看器保持比例放大"
        Me.ChkKeepScale.UseVisualStyleBackColor = True
        '
        'TxtMaxSize
        '
        Me.TxtMaxSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtMaxSize.Location = New System.Drawing.Point(338, 27)
        Me.TxtMaxSize.Name = "TxtMaxSize"
        Me.TxtMaxSize.Size = New System.Drawing.Size(63, 25)
        Me.TxtMaxSize.TabIndex = 79
        Me.TxtMaxSize.Text = "240"
        '
        'LblThumbMax
        '
        Me.LblThumbMax.AutoSize = True
        Me.LblThumbMax.Location = New System.Drawing.Point(209, 32)
        Me.LblThumbMax.Name = "LblThumbMax"
        Me.LblThumbMax.Size = New System.Drawing.Size(127, 15)
        Me.LblThumbMax.TabIndex = 78
        Me.LblThumbMax.Text = "缩略图最大尺寸："
        '
        'TxtMinSize
        '
        Me.TxtMinSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtMinSize.Location = New System.Drawing.Point(136, 27)
        Me.TxtMinSize.Name = "TxtMinSize"
        Me.TxtMinSize.Size = New System.Drawing.Size(63, 25)
        Me.TxtMinSize.TabIndex = 77
        Me.TxtMinSize.Text = "120"
        '
        'LblThumbMin
        '
        Me.LblThumbMin.AutoSize = True
        Me.LblThumbMin.Location = New System.Drawing.Point(7, 32)
        Me.LblThumbMin.Name = "LblThumbMin"
        Me.LblThumbMin.Size = New System.Drawing.Size(127, 15)
        Me.LblThumbMin.TabIndex = 76
        Me.LblThumbMin.Text = "缩略图最小尺寸："
        '
        'BtnClose
        '
        Me.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnClose.Location = New System.Drawing.Point(687, 404)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(119, 41)
        Me.BtnClose.TabIndex = 58
        Me.BtnClose.Text = "关闭(&C)"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'ChkShowTool
        '
        Me.ChkShowTool.AutoSize = True
        Me.ChkShowTool.Location = New System.Drawing.Point(205, 217)
        Me.ChkShowTool.Name = "ChkShowTool"
        Me.ChkShowTool.Size = New System.Drawing.Size(104, 19)
        Me.ChkShowTool.TabIndex = 81
        Me.ChkShowTool.Text = "显示工具栏"
        Me.ChkShowTool.UseVisualStyleBackColor = True
        '
        'PropertiesForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnClose
        Me.ClientSize = New System.Drawing.Size(812, 453)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.GrpAdvanced)
        Me.Controls.Add(Me.GrpStartup)
        Me.Controls.Add(Me.GrpAppearance)
        Me.Controls.Add(Me.BtnExport)
        Me.Controls.Add(Me.BtnImport)
        Me.Controls.Add(Me.BtnReset)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PropertiesForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "选项"
        Me.GrpAppearance.ResumeLayout(False)
        Me.GrpAppearance.PerformLayout()
        Me.GrpStartup.ResumeLayout(False)
        Me.GrpStartup.PerformLayout()
        Me.GrpAdvanced.ResumeLayout(False)
        Me.GrpAdvanced.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnExport As Button
    Friend WithEvents BtnImport As Button
    Friend WithEvents BtnReset As Button
    Friend WithEvents GrpAppearance As GroupBox
    Friend WithEvents LblLanguage As Label
    Friend WithEvents CboLang As ComboBox
    Friend WithEvents LblCorColor As Label
    Friend WithEvents LblSelColor As Label
    Friend WithEvents BtnCornerColor As Button
    Friend WithEvents BtnSelectedColor As Button
    Friend WithEvents LblCornerColor As Label
    Friend WithEvents LblSelectedColor As Label
    Friend WithEvents LblThemeColor As Label
    Friend WithEvents BtnThemeColor As Button
    Friend WithEvents ChkShowInChild As CheckBox
    Friend WithEvents RadSystem As RadioButton
    Friend WithEvents RadDark As RadioButton
    Friend WithEvents RadLight As RadioButton
    Friend WithEvents LblTheme As Label
    Friend WithEvents GrpStartup As GroupBox
    Friend WithEvents ChkRestore As CheckBox
    Friend WithEvents CboCheckUpdate As ComboBox
    Friend WithEvents ChkAutoCheckUpdate As CheckBox
    Friend WithEvents ChkAutoPlay As CheckBox
    Friend WithEvents ChkAutoStart As CheckBox
    Friend WithEvents GrpAdvanced As GroupBox
    Friend WithEvents ChkKeepScale As CheckBox
    Friend WithEvents TxtMaxSize As TextBox
    Friend WithEvents LblThumbMax As Label
    Friend WithEvents TxtMinSize As TextBox
    Friend WithEvents LblThumbMin As Label
    Friend WithEvents ChkDevTools As CheckBox
    Friend WithEvents ChkPlayNext As CheckBox
    Friend WithEvents TxtAutoNext As TextBox
    Friend WithEvents LblAutoNext As Label
    Friend WithEvents ChkFileAssociation As CheckBox
    Friend WithEvents ChkShowThemeColor As CheckBox
    Friend WithEvents ColorDlg As ColorDialog
    Friend WithEvents BtnClose As Button
    Friend WithEvents ChkMenuUpper As CheckBox
    Friend WithEvents ChkLoop As CheckBox
    Friend WithEvents ChkShowTool As CheckBox
End Class
