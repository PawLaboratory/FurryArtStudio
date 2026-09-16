<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ViewForm
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
        Me.PictureBoxMain = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBoxMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxMain
        '
        Me.PictureBoxMain.Location = New System.Drawing.Point(1, 2)
        Me.PictureBoxMain.Name = "PictureBoxMain"
        Me.PictureBoxMain.Size = New System.Drawing.Size(624, 430)
        Me.PictureBoxMain.TabIndex = 0
        Me.PictureBoxMain.TabStop = False
        '
        'ViewForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 433)
        Me.Controls.Add(Me.PictureBoxMain)
        Me.DoubleBuffered = True
        Me.Name = "ViewForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ViewForm"
        CType(Me.PictureBoxMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBoxMain As PictureBox
End Class
