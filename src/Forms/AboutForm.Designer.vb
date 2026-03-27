<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AboutForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutForm))
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.LblVersion = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LlblWebSite = New System.Windows.Forms.LinkLabel()
        Me.LlblGitHub = New System.Windows.Forms.LinkLabel()
        Me.LlblLicense = New System.Windows.Forms.LinkLabel()
        Me.TxtBox = New System.Windows.Forms.RichTextBox()
        Me.LlblPrivacy = New System.Windows.Forms.LinkLabel()
        Me.LlblUserAgreement = New System.Windows.Forms.LinkLabel()
        Me.PicBoxLogo = New System.Windows.Forms.PictureBox()
        CType(Me.PicBoxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnOK
        '
        Me.BtnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnOK.Location = New System.Drawing.Point(484, 220)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(126, 41)
        Me.BtnOK.TabIndex = 7
        Me.BtnOK.Text = "确定(&O)"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'LblVersion
        '
        Me.LblVersion.Location = New System.Drawing.Point(65, 32)
        Me.LblVersion.Name = "LblVersion"
        Me.LblVersion.Size = New System.Drawing.Size(150, 23)
        Me.LblVersion.TabIndex = 1
        Me.LblVersion.Text = "Version x.x.x"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(65, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(150, 23)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "FurryArtStudio"
        '
        'LlblWebSite
        '
        Me.LlblWebSite.Location = New System.Drawing.Point(481, 9)
        Me.LlblWebSite.Name = "LlblWebSite"
        Me.LlblWebSite.Size = New System.Drawing.Size(129, 23)
        Me.LlblWebSite.TabIndex = 1
        Me.LlblWebSite.TabStop = True
        Me.LlblWebSite.Text = "官网"
        Me.LlblWebSite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LlblGitHub
        '
        Me.LlblGitHub.Location = New System.Drawing.Point(481, 37)
        Me.LlblGitHub.Name = "LlblGitHub"
        Me.LlblGitHub.Size = New System.Drawing.Size(129, 23)
        Me.LlblGitHub.TabIndex = 2
        Me.LlblGitHub.TabStop = True
        Me.LlblGitHub.Text = "GitHub"
        Me.LlblGitHub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LlblLicense
        '
        Me.LlblLicense.Location = New System.Drawing.Point(481, 65)
        Me.LlblLicense.Name = "LlblLicense"
        Me.LlblLicense.Size = New System.Drawing.Size(129, 23)
        Me.LlblLicense.TabIndex = 3
        Me.LlblLicense.TabStop = True
        Me.LlblLicense.Text = "许可证"
        Me.LlblLicense.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtBox
        '
        Me.TxtBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBox.Location = New System.Drawing.Point(12, 58)
        Me.TxtBox.Name = "TxtBox"
        Me.TxtBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.TxtBox.Size = New System.Drawing.Size(455, 203)
        Me.TxtBox.TabIndex = 6
        Me.TxtBox.Text = ""
        '
        'LlblPrivacy
        '
        Me.LlblPrivacy.Location = New System.Drawing.Point(481, 93)
        Me.LlblPrivacy.Name = "LlblPrivacy"
        Me.LlblPrivacy.Size = New System.Drawing.Size(129, 23)
        Me.LlblPrivacy.TabIndex = 4
        Me.LlblPrivacy.TabStop = True
        Me.LlblPrivacy.Text = "隐私政策"
        Me.LlblPrivacy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LlblUserAgreement
        '
        Me.LlblUserAgreement.Location = New System.Drawing.Point(481, 121)
        Me.LlblUserAgreement.Name = "LlblUserAgreement"
        Me.LlblUserAgreement.Size = New System.Drawing.Size(129, 23)
        Me.LlblUserAgreement.TabIndex = 5
        Me.LlblUserAgreement.TabStop = True
        Me.LlblUserAgreement.Text = "用户协议"
        Me.LlblUserAgreement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PicBoxLogo
        '
        Me.PicBoxLogo.Image = CType(resources.GetObject("PicBoxLogo.Image"), System.Drawing.Image)
        Me.PicBoxLogo.Location = New System.Drawing.Point(12, 7)
        Me.PicBoxLogo.Name = "PicBoxLogo"
        Me.PicBoxLogo.Size = New System.Drawing.Size(45, 45)
        Me.PicBoxLogo.TabIndex = 9
        Me.PicBoxLogo.TabStop = False
        '
        'AboutForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnOK
        Me.ClientSize = New System.Drawing.Size(622, 273)
        Me.Controls.Add(Me.PicBoxLogo)
        Me.Controls.Add(Me.LlblUserAgreement)
        Me.Controls.Add(Me.LlblPrivacy)
        Me.Controls.Add(Me.TxtBox)
        Me.Controls.Add(Me.LlblLicense)
        Me.Controls.Add(Me.LlblGitHub)
        Me.Controls.Add(Me.LlblWebSite)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblVersion)
        Me.Controls.Add(Me.BtnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "关于"
        CType(Me.PicBoxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnOK As Button
    Friend WithEvents LblVersion As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents LlblWebSite As LinkLabel
    Friend WithEvents LlblGitHub As LinkLabel
    Friend WithEvents LlblLicense As LinkLabel
    Friend WithEvents TxtBox As RichTextBox
    Friend WithEvents LlblPrivacy As LinkLabel
    Friend WithEvents LlblUserAgreement As LinkLabel
    Friend WithEvents PicBoxLogo As PictureBox
End Class
