<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogForm
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
        Me.LblContent = New System.Windows.Forms.Label()
        Me.Btn1 = New System.Windows.Forms.Button()
        Me.Btn2 = New System.Windows.Forms.Button()
        Me.PicBox = New System.Windows.Forms.PictureBox()
        Me.Btn3 = New System.Windows.Forms.Button()
        Me.Btn4 = New System.Windows.Forms.Button()
        Me.LblMainInstruction = New System.Windows.Forms.Label()
        CType(Me.PicBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblContent
        '
        Me.LblContent.Location = New System.Drawing.Point(90, 57)
        Me.LblContent.Name = "LblContent"
        Me.LblContent.Size = New System.Drawing.Size(340, 75)
        Me.LblContent.TabIndex = 0
        Me.LblContent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Btn1
        '
        Me.Btn1.AutoSize = True
        Me.Btn1.Location = New System.Drawing.Point(340, 135)
        Me.Btn1.Name = "Btn1"
        Me.Btn1.Size = New System.Drawing.Size(90, 30)
        Me.Btn1.TabIndex = 4
        Me.Btn1.Text = "Button1"
        Me.Btn1.UseVisualStyleBackColor = True
        '
        'Btn2
        '
        Me.Btn2.AutoSize = True
        Me.Btn2.Location = New System.Drawing.Point(244, 135)
        Me.Btn2.Name = "Btn2"
        Me.Btn2.Size = New System.Drawing.Size(90, 30)
        Me.Btn2.TabIndex = 3
        Me.Btn2.Text = "Button2"
        Me.Btn2.UseVisualStyleBackColor = True
        '
        'PicBox
        '
        Me.PicBox.Location = New System.Drawing.Point(9, 9)
        Me.PicBox.Name = "PicBox"
        Me.PicBox.Size = New System.Drawing.Size(75, 75)
        Me.PicBox.TabIndex = 3
        Me.PicBox.TabStop = False
        '
        'Btn3
        '
        Me.Btn3.AutoSize = True
        Me.Btn3.Location = New System.Drawing.Point(148, 135)
        Me.Btn3.Name = "Btn3"
        Me.Btn3.Size = New System.Drawing.Size(90, 30)
        Me.Btn3.TabIndex = 2
        Me.Btn3.Text = "Button3"
        Me.Btn3.UseVisualStyleBackColor = True
        '
        'Btn4
        '
        Me.Btn4.AutoSize = True
        Me.Btn4.Location = New System.Drawing.Point(52, 135)
        Me.Btn4.Name = "Btn4"
        Me.Btn4.Size = New System.Drawing.Size(90, 30)
        Me.Btn4.TabIndex = 1
        Me.Btn4.Text = "Button4"
        Me.Btn4.UseVisualStyleBackColor = True
        '
        'LblMainInstruction
        '
        Me.LblMainInstruction.Location = New System.Drawing.Point(90, 10)
        Me.LblMainInstruction.Name = "LblMainInstruction"
        Me.LblMainInstruction.Size = New System.Drawing.Size(340, 39)
        Me.LblMainInstruction.TabIndex = 6
        Me.LblMainInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DialogForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(442, 173)
        Me.Controls.Add(Me.LblMainInstruction)
        Me.Controls.Add(Me.Btn4)
        Me.Controls.Add(Me.Btn3)
        Me.Controls.Add(Me.PicBox)
        Me.Controls.Add(Me.Btn2)
        Me.Controls.Add(Me.Btn1)
        Me.Controls.Add(Me.LblContent)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DialogForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Dialog"
        CType(Me.PicBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LblContent As Label
    Friend WithEvents Btn1 As Button
    Friend WithEvents Btn2 As Button
    Friend WithEvents PicBox As PictureBox
    Friend WithEvents Btn3 As Button
    Friend WithEvents Btn4 As Button
    Friend WithEvents LblMainInstruction As Label
End Class
