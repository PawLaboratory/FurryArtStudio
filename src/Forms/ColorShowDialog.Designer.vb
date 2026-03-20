<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ColorDialogForm
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
        Me.L10 = New System.Windows.Forms.Label()
        Me.L9 = New System.Windows.Forms.Label()
        Me.L8 = New System.Windows.Forms.Label()
        Me.L7 = New System.Windows.Forms.Label()
        Me.L6 = New System.Windows.Forms.Label()
        Me.L5 = New System.Windows.Forms.Label()
        Me.L4 = New System.Windows.Forms.Label()
        Me.L3 = New System.Windows.Forms.Label()
        Me.L2 = New System.Windows.Forms.Label()
        Me.L1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'L10
        '
        Me.L10.Dock = System.Windows.Forms.DockStyle.Top
        Me.L10.Location = New System.Drawing.Point(0, 234)
        Me.L10.Name = "L10"
        Me.L10.Size = New System.Drawing.Size(382, 26)
        Me.L10.TabIndex = 19
        Me.L10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'L9
        '
        Me.L9.Dock = System.Windows.Forms.DockStyle.Top
        Me.L9.Location = New System.Drawing.Point(0, 208)
        Me.L9.Name = "L9"
        Me.L9.Size = New System.Drawing.Size(382, 26)
        Me.L9.TabIndex = 18
        Me.L9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'L8
        '
        Me.L8.Dock = System.Windows.Forms.DockStyle.Top
        Me.L8.Location = New System.Drawing.Point(0, 182)
        Me.L8.Name = "L8"
        Me.L8.Size = New System.Drawing.Size(382, 26)
        Me.L8.TabIndex = 17
        Me.L8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'L7
        '
        Me.L7.Dock = System.Windows.Forms.DockStyle.Top
        Me.L7.Location = New System.Drawing.Point(0, 156)
        Me.L7.Name = "L7"
        Me.L7.Size = New System.Drawing.Size(382, 26)
        Me.L7.TabIndex = 16
        Me.L7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'L6
        '
        Me.L6.Dock = System.Windows.Forms.DockStyle.Top
        Me.L6.Location = New System.Drawing.Point(0, 130)
        Me.L6.Name = "L6"
        Me.L6.Size = New System.Drawing.Size(382, 26)
        Me.L6.TabIndex = 15
        Me.L6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'L5
        '
        Me.L5.Dock = System.Windows.Forms.DockStyle.Top
        Me.L5.Location = New System.Drawing.Point(0, 104)
        Me.L5.Name = "L5"
        Me.L5.Size = New System.Drawing.Size(382, 26)
        Me.L5.TabIndex = 14
        Me.L5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'L4
        '
        Me.L4.Dock = System.Windows.Forms.DockStyle.Top
        Me.L4.Location = New System.Drawing.Point(0, 78)
        Me.L4.Name = "L4"
        Me.L4.Size = New System.Drawing.Size(382, 26)
        Me.L4.TabIndex = 13
        Me.L4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'L3
        '
        Me.L3.Dock = System.Windows.Forms.DockStyle.Top
        Me.L3.Location = New System.Drawing.Point(0, 52)
        Me.L3.Name = "L3"
        Me.L3.Size = New System.Drawing.Size(382, 26)
        Me.L3.TabIndex = 12
        Me.L3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'L2
        '
        Me.L2.Dock = System.Windows.Forms.DockStyle.Top
        Me.L2.Location = New System.Drawing.Point(0, 26)
        Me.L2.Name = "L2"
        Me.L2.Size = New System.Drawing.Size(382, 26)
        Me.L2.TabIndex = 11
        Me.L2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'L1
        '
        Me.L1.Dock = System.Windows.Forms.DockStyle.Top
        Me.L1.Location = New System.Drawing.Point(0, 0)
        Me.L1.Name = "L1"
        Me.L1.Size = New System.Drawing.Size(382, 26)
        Me.L1.TabIndex = 10
        Me.L1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ColorDialogForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(382, 258)
        Me.Controls.Add(Me.L10)
        Me.Controls.Add(Me.L9)
        Me.Controls.Add(Me.L8)
        Me.Controls.Add(Me.L7)
        Me.Controls.Add(Me.L6)
        Me.Controls.Add(Me.L5)
        Me.Controls.Add(Me.L4)
        Me.Controls.Add(Me.L3)
        Me.Controls.Add(Me.L2)
        Me.Controls.Add(Me.L1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ColorDialogForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Extract result"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents L10 As Label
    Friend WithEvents L9 As Label
    Friend WithEvents L8 As Label
    Friend WithEvents L7 As Label
    Friend WithEvents L6 As Label
    Friend WithEvents L5 As Label
    Friend WithEvents L4 As Label
    Friend WithEvents L3 As Label
    Friend WithEvents L2 As Label
    Friend WithEvents L1 As Label
End Class
