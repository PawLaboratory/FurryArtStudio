<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ExportForm
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
        Me.PreviewPicturebox = New System.Windows.Forms.PictureBox()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnExport = New System.Windows.Forms.Button()
        Me.TxtPath = New System.Windows.Forms.TextBox()
        Me.LblPath = New System.Windows.Forms.Label()
        Me.BtnSelect = New System.Windows.Forms.Button()
        Me.RadAuto = New System.Windows.Forms.RadioButton()
        Me.RadCreate = New System.Windows.Forms.RadioButton()
        Me.RadKeep = New System.Windows.Forms.RadioButton()
        Me.LblClassify = New System.Windows.Forms.Label()
        Me.LblArchive = New System.Windows.Forms.Label()
        Me.ChkExportZip = New System.Windows.Forms.CheckBox()
        Me.CboCompressLevel = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.PreviewPicturebox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PreviewPicturebox
        '
        Me.PreviewPicturebox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PreviewPicturebox.Location = New System.Drawing.Point(12, 12)
        Me.PreviewPicturebox.Name = "PreviewPicturebox"
        Me.PreviewPicturebox.Size = New System.Drawing.Size(200, 200)
        Me.PreviewPicturebox.TabIndex = 6
        Me.PreviewPicturebox.TabStop = False
        '
        'BtnCancel
        '
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Location = New System.Drawing.Point(441, 214)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(109, 49)
        Me.BtnCancel.TabIndex = 14
        Me.BtnCancel.Text = "取消(&C)"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnExport
        '
        Me.BtnExport.Location = New System.Drawing.Point(326, 214)
        Me.BtnExport.Name = "BtnExport"
        Me.BtnExport.Size = New System.Drawing.Size(109, 49)
        Me.BtnExport.TabIndex = 13
        Me.BtnExport.Text = "导出(&E)"
        Me.BtnExport.UseVisualStyleBackColor = True
        '
        'TxtPath
        '
        Me.TxtPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPath.Location = New System.Drawing.Point(305, 183)
        Me.TxtPath.Name = "TxtPath"
        Me.TxtPath.ReadOnly = True
        Me.TxtPath.Size = New System.Drawing.Size(200, 25)
        Me.TxtPath.TabIndex = 21
        '
        'LblPath
        '
        Me.LblPath.AutoSize = True
        Me.LblPath.Location = New System.Drawing.Point(218, 187)
        Me.LblPath.Name = "LblPath"
        Me.LblPath.Size = New System.Drawing.Size(82, 15)
        Me.LblPath.TabIndex = 22
        Me.LblPath.Text = "输出目录："
        '
        'BtnSelect
        '
        Me.BtnSelect.Location = New System.Drawing.Point(510, 183)
        Me.BtnSelect.Name = "BtnSelect"
        Me.BtnSelect.Size = New System.Drawing.Size(40, 25)
        Me.BtnSelect.TabIndex = 23
        Me.BtnSelect.Text = "..."
        Me.BtnSelect.UseVisualStyleBackColor = True
        '
        'RadAuto
        '
        Me.RadAuto.AutoSize = True
        Me.RadAuto.Checked = True
        Me.RadAuto.Location = New System.Drawing.Point(221, 34)
        Me.RadAuto.Name = "RadAuto"
        Me.RadAuto.Size = New System.Drawing.Size(133, 19)
        Me.RadAuto.TabIndex = 29
        Me.RadAuto.TabStop = True
        Me.RadAuto.Text = "智能创建文件夹"
        Me.RadAuto.UseVisualStyleBackColor = True
        '
        'RadCreate
        '
        Me.RadCreate.AutoSize = True
        Me.RadCreate.Location = New System.Drawing.Point(221, 59)
        Me.RadCreate.Name = "RadCreate"
        Me.RadCreate.Size = New System.Drawing.Size(223, 19)
        Me.RadCreate.TabIndex = 30
        Me.RadCreate.TabStop = True
        Me.RadCreate.Text = "强制为每个稿件创建子文件夹"
        Me.RadCreate.UseVisualStyleBackColor = True
        '
        'RadKeep
        '
        Me.RadKeep.AutoSize = True
        Me.RadKeep.Location = New System.Drawing.Point(221, 84)
        Me.RadKeep.Name = "RadKeep"
        Me.RadKeep.Size = New System.Drawing.Size(133, 19)
        Me.RadKeep.TabIndex = 31
        Me.RadKeep.TabStop = True
        Me.RadKeep.Text = "不创建子文件夹"
        Me.RadKeep.UseVisualStyleBackColor = True
        '
        'LblClassify
        '
        Me.LblClassify.AutoSize = True
        Me.LblClassify.Location = New System.Drawing.Point(216, 14)
        Me.LblClassify.Name = "LblClassify"
        Me.LblClassify.Size = New System.Drawing.Size(37, 15)
        Me.LblClassify.TabIndex = 32
        Me.LblClassify.Text = "分类"
        '
        'LblArchive
        '
        Me.LblArchive.AutoSize = True
        Me.LblArchive.Location = New System.Drawing.Point(216, 109)
        Me.LblArchive.Name = "LblArchive"
        Me.LblArchive.Size = New System.Drawing.Size(37, 15)
        Me.LblArchive.TabIndex = 33
        Me.LblArchive.Text = "归档"
        '
        'ChkExportZip
        '
        Me.ChkExportZip.AutoSize = True
        Me.ChkExportZip.Location = New System.Drawing.Point(221, 129)
        Me.ChkExportZip.Name = "ChkExportZip"
        Me.ChkExportZip.Size = New System.Drawing.Size(173, 19)
        Me.ChkExportZip.TabIndex = 34
        Me.ChkExportZip.Text = "导出后压缩为ZIP文件"
        Me.ChkExportZip.UseVisualStyleBackColor = True
        '
        'CboCompressLevel
        '
        Me.CboCompressLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboCompressLevel.FormattingEnabled = True
        Me.CboCompressLevel.Location = New System.Drawing.Point(305, 154)
        Me.CboCompressLevel.Name = "CboCompressLevel"
        Me.CboCompressLevel.Size = New System.Drawing.Size(150, 23)
        Me.CboCompressLevel.TabIndex = 35
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(218, 157)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 15)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "压缩级别："
        '
        'ExportForm
        '
        Me.AcceptButton = Me.BtnExport
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(562, 273)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CboCompressLevel)
        Me.Controls.Add(Me.ChkExportZip)
        Me.Controls.Add(Me.LblArchive)
        Me.Controls.Add(Me.LblClassify)
        Me.Controls.Add(Me.RadKeep)
        Me.Controls.Add(Me.RadCreate)
        Me.Controls.Add(Me.RadAuto)
        Me.Controls.Add(Me.BtnSelect)
        Me.Controls.Add(Me.LblPath)
        Me.Controls.Add(Me.TxtPath)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnExport)
        Me.Controls.Add(Me.PreviewPicturebox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ExportForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "导出"
        CType(Me.PreviewPicturebox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PreviewPicturebox As PictureBox
    Friend WithEvents BtnCancel As Button
    Friend WithEvents BtnExport As Button
    Friend WithEvents TxtPath As TextBox
    Friend WithEvents LblPath As Label
    Friend WithEvents BtnSelect As Button
    Friend WithEvents RadAuto As RadioButton
    Friend WithEvents RadCreate As RadioButton
    Friend WithEvents RadKeep As RadioButton
    Friend WithEvents LblClassify As Label
    Friend WithEvents LblArchive As Label
    Friend WithEvents ChkExportZip As CheckBox
    Friend WithEvents CboCompressLevel As ComboBox
    Friend WithEvents Label1 As Label
End Class
