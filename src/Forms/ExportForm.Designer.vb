<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExportForm
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
        Me.PreviewPicturebox = New System.Windows.Forms.PictureBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnExport = New System.Windows.Forms.Button()
        Me.RadFolder = New System.Windows.Forms.RadioButton()
        Me.RadZip = New System.Windows.Forms.RadioButton()
        Me.LblName = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtPath = New System.Windows.Forms.TextBox()
        Me.LblPath = New System.Windows.Forms.Label()
        Me.BtnSelect = New System.Windows.Forms.Button()
        Me.CbOperation = New System.Windows.Forms.CheckBox()
        Me.LblZip = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
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
        'TxtName
        '
        Me.TxtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtName.Location = New System.Drawing.Point(321, 37)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(184, 25)
        Me.TxtName.TabIndex = 7
        '
        'BtnCancel
        '
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Location = New System.Drawing.Point(441, 169)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(109, 49)
        Me.BtnCancel.TabIndex = 14
        Me.BtnCancel.Text = "取消(&C)"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnExport
        '
        Me.BtnExport.Location = New System.Drawing.Point(326, 169)
        Me.BtnExport.Name = "BtnExport"
        Me.BtnExport.Size = New System.Drawing.Size(109, 49)
        Me.BtnExport.TabIndex = 13
        Me.BtnExport.Text = "导出(&E)"
        Me.BtnExport.UseVisualStyleBackColor = True
        '
        'RadFolder
        '
        Me.RadFolder.AutoSize = True
        Me.RadFolder.Location = New System.Drawing.Point(221, 12)
        Me.RadFolder.Name = "RadFolder"
        Me.RadFolder.Size = New System.Drawing.Size(148, 19)
        Me.RadFolder.TabIndex = 15
        Me.RadFolder.Text = "导出为独立文件夹"
        Me.RadFolder.UseVisualStyleBackColor = True
        '
        'RadZip
        '
        Me.RadZip.AutoSize = True
        Me.RadZip.Checked = True
        Me.RadZip.Location = New System.Drawing.Point(423, 12)
        Me.RadZip.Name = "RadZip"
        Me.RadZip.Size = New System.Drawing.Size(127, 19)
        Me.RadZip.TabIndex = 16
        Me.RadZip.TabStop = True
        Me.RadZip.Text = "压缩为ZIP文件"
        Me.RadZip.UseVisualStyleBackColor = True
        '
        'LblName
        '
        Me.LblName.AutoSize = True
        Me.LblName.Location = New System.Drawing.Point(218, 42)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(97, 15)
        Me.LblName.TabIndex = 18
        Me.LblName.Text = "压缩文件名："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(218, 102)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(127, 15)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "当前稿件文件数："
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(218, 123)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 15)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "当前稿件大小："
        '
        'TxtPath
        '
        Me.TxtPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPath.Location = New System.Drawing.Point(321, 68)
        Me.TxtPath.Name = "TxtPath"
        Me.TxtPath.ReadOnly = True
        Me.TxtPath.Size = New System.Drawing.Size(184, 25)
        Me.TxtPath.TabIndex = 21
        '
        'LblPath
        '
        Me.LblPath.AutoSize = True
        Me.LblPath.Location = New System.Drawing.Point(218, 72)
        Me.LblPath.Name = "LblPath"
        Me.LblPath.Size = New System.Drawing.Size(82, 15)
        Me.LblPath.TabIndex = 22
        Me.LblPath.Text = "输出目录："
        '
        'BtnSelect
        '
        Me.BtnSelect.Location = New System.Drawing.Point(510, 68)
        Me.BtnSelect.Name = "BtnSelect"
        Me.BtnSelect.Size = New System.Drawing.Size(40, 25)
        Me.BtnSelect.TabIndex = 23
        Me.BtnSelect.Text = "..."
        Me.BtnSelect.UseVisualStyleBackColor = True
        '
        'CbOperation
        '
        Me.CbOperation.AutoSize = True
        Me.CbOperation.Location = New System.Drawing.Point(221, 144)
        Me.CbOperation.Name = "CbOperation"
        Me.CbOperation.Size = New System.Drawing.Size(217, 19)
        Me.CbOperation.TabIndex = 25
        Me.CbOperation.Text = "为后续2个稿件执行相同操作"
        Me.CbOperation.UseVisualStyleBackColor = True
        '
        'LblZip
        '
        Me.LblZip.AutoSize = True
        Me.LblZip.Location = New System.Drawing.Point(511, 42)
        Me.LblZip.Name = "LblZip"
        Me.LblZip.Size = New System.Drawing.Size(39, 15)
        Me.LblZip.TabIndex = 26
        Me.LblZip.Text = ".zip"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(418, 123)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 15)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "剩余大小："
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(418, 102)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(97, 15)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "剩余文件数："
        '
        'ExportForm
        '
        Me.AcceptButton = Me.BtnExport
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(562, 223)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.LblZip)
        Me.Controls.Add(Me.CbOperation)
        Me.Controls.Add(Me.BtnSelect)
        Me.Controls.Add(Me.LblPath)
        Me.Controls.Add(Me.TxtPath)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LblName)
        Me.Controls.Add(Me.RadZip)
        Me.Controls.Add(Me.RadFolder)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnExport)
        Me.Controls.Add(Me.TxtName)
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
    Friend WithEvents TxtName As TextBox
    Friend WithEvents BtnCancel As Button
    Friend WithEvents BtnExport As Button
    Friend WithEvents RadFolder As RadioButton
    Friend WithEvents RadZip As RadioButton
    Friend WithEvents LblName As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtPath As TextBox
    Friend WithEvents LblPath As Label
    Friend WithEvents BtnSelect As Button
    Friend WithEvents CbOperation As CheckBox
    Friend WithEvents LblZip As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label7 As Label
End Class
