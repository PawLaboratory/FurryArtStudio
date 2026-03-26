<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScriptForm
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
        Me.LblDialog = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LblDialog
        '
        Me.LblDialog.Location = New System.Drawing.Point(1, 120)
        Me.LblDialog.Name = "LblDialog"
        Me.LblDialog.Size = New System.Drawing.Size(531, 131)
        Me.LblDialog.TabIndex = 0
        Me.LblDialog.Text = "Label1"
        '
        'ScriptForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(532, 253)
        Me.Controls.Add(Me.LblDialog)
        Me.Name = "ScriptForm"
        Me.Text = "ScriptForm"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LblDialog As Label
End Class
