' Furry Art Studio - 本地稿件管理工具
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
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.InteropServices
Imports Krypton.Toolkit

Public Class EditDialogForm
    Implements IThemeChangeable, ILocalizable
    '存储传入的稿件对象
    Private _artwork As Artwork
    Private _libraryPath As String
    '文件事务管理类
    Private _transaction As FileTransaction
#Region "窗体相关"
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
            Icon = CreateRoundedRectangleIcon(True, My.Resources.Icons.MenuEditDark)
        Else
            bgColor = BgColorLight
            frColor = FrColorLight
            Icon = CreateRoundedRectangleIcon(False, My.Resources.Icons.MenuEditLight)
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
    Private Sub LanguageChange() Implements ILocalizable.LanguageChange
        If _artwork.ID = 0 Then
            Text = My.Resources.Edit_NewTitle
        Else
            Text = My.Resources.Edit_EditTitle
        End If
        LblTitle.Text = My.Resources.Edit_LblTitle
        LblAuthor.Text = My.Resources.Edit_LblAuthor
        LblCharacters.Text = My.Resources.Edit_LblCharacters
        LblTags.Text = My.Resources.Edit_LblTags
        LblNotes.Text = My.Resources.Edit_LblNotes
        LblCreateTime.Text = My.Resources.Edit_LblCreate
        BtnAdd.Text = My.Resources.Edit_BtnAddItem
        BtnDel.Text = My.Resources.Edit_BtnDel
        BtnSetPreview.Text = My.Resources.Edit_BtnSetPreview
        BtnCancel.Text = My.Resources.Edit_BtnCancel
        BtnModify.Text = My.Resources.About_BtnOK
    End Sub
    Private Sub EditDialogForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SystemThemeChange()
        Dim MnuHandle = GetSystemMenu(Handle, False) '获取菜单句柄
        RemoveMenu(MnuHandle, SC_RESTORE, MF_BYCOMMAND) '去除还原菜单
        RemoveMenu(MnuHandle, SC_MAXIMIZE, MF_BYCOMMAND) '去除最大化菜单
        RemoveMenu(MnuHandle, SC_SIZE, MF_BYCOMMAND) '去除大小菜单
        RemoveMenu(MnuHandle, SC_MINIMIZE, MF_BYCOMMAND) '去除最小化菜单
        LstBox.DisplayMember = "FileName"
        LstBox.ValueMember = "FullPath" '初始化
        If LstBox.SelectedItems.Count = 0 Then
            BtnSetPreview.Enabled = False
            BtnDel.Enabled = False
        End If
        LanguageChange()
    End Sub
    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        '解决管理员权限下程序无法接受拖拽数据的问题
        '但是似乎不能用, 可能是微软限制, 不管了
        ChangeWindowMessageFilterEx(Me.Handle, WM_DROPFILES, MSGFLT_ALLOW, IntPtr.Zero)
        ChangeWindowMessageFilterEx(Me.Handle, WM_COPYDATA, MSGFLT_ALLOW, IntPtr.Zero)
    End Sub
    Private Sub EditDialogForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        _transaction.Dispose()
    End Sub

    ''' <summary>
    ''' 构造函数
    ''' </summary>
    ''' <param name="artwork">要编辑的稿件对象</param>
    ''' <param name="libraryPath">稿件库路径</param>
    Public Sub New(artwork As Artwork, libraryPath As String)
        InitializeComponent()
        _libraryPath = libraryPath
        If artwork.ID = 0 Then '用户拖入图片添加
            Me.Text = My.Resources.Edit_NewTitle
            _artwork = New Artwork With {
                        .ID = 0,
                        .UUID = Guid.NewGuid(),
                        .Title = artwork.Title,'已知
                        .Author = String.Empty,
                        .Characters = Array.Empty(Of String)(),
                        .CreateTime = artwork.CreateTime,'已知
                        .ImportTime = DateTime.Now,
                        .UpdateTime = DateTime.Now,
                        .IsDeleted = 0,
                        .Tags = Array.Empty(Of String)(),
                        .Notes = String.Empty,
                        .FilePaths = Array.Empty(Of String)()
                    }
            _transaction = New FileTransaction() '新建文件事务处理类
        Else '编辑已有稿件
            Me.Text = My.Resources.Edit_EditTitle
            _artwork = artwork
            _transaction = New FileTransaction(Path.Combine(_libraryPath, _artwork.UUID.ToString))
            RefreshFileList() '将文件添加到列表
        End If
        InitializeForm()
    End Sub

    ''' <summary>
    ''' 初始化窗体显示
    ''' </summary>
    Private Sub InitializeForm()
        TxtboxTitle.Text = _artwork.Title '标题
        TxtboxAuthor.Text = _artwork.Author '作者
        TxtboxCharacters.Text = If(_artwork.Characters IsNot Nothing, String.Join(" ", _artwork.Characters), "") '角色
        TxtboxTags.Text = If(_artwork.Tags IsNot Nothing, String.Join(" ", _artwork.Tags), "") '标签
        TxtboxCreateTime.Text = _artwork.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") '创作时间
        LblImportTime.Text = String.Format(My.Resources.Edit_LblImport,
                                           _artwork.ImportTime.ToString("yyyy-MM-dd HH:mm:ss")) '导入时间
        LblUpdateTime.Text = String.Format(My.Resources.Edit_LblUpdate,
                                           _artwork.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")) '更新时间
        LblUUID.Text = _artwork.UUID.ToString() 'UUID
        TxtboxNotes.Text = _artwork.Notes '备注
        TxtboxCreateTime.ForeColor = Color.Gray '设置创建时间文本框的提示
        '加载预览图
        PreviewPicturebox.SizeMode = PictureBoxSizeMode.Zoom
        LoadPreviewImage()
    End Sub

    ''' <summary>
    ''' 加载预览图片
    ''' </summary>
    Private Sub LoadPreviewImage()
        Try
            '如果稿件文件夹存在, 尝试查找预览图
            Dim artworkFolder As String = Path.Combine(_libraryPath, _artwork.UUID.ToString())
            If Directory.Exists(artworkFolder) Then
                '查找预览图文件
                Dim previewFiles = Directory.GetFiles(artworkFolder, ".preview.*")
                If previewFiles.Length > 0 Then
                    '加载第一个预览图
                    Using stream As New FileStream(previewFiles(0), FileMode.Open, FileAccess.Read)
                        Using tempImage As Image = Image.FromStream(stream) '创建独立副本, 解除对流的依赖
                            PreviewPicturebox.Image = New Bitmap(tempImage)
                        End Using
                    End Using
                    Return
                End If
                '如果没有预览图, 尝试加载第一个图片文件
                Dim imageFiles = Directory.GetFiles(artworkFolder, "*.*") _
                    .Where(Function(f)
                               Dim ext = Path.GetExtension(f).ToLower()
                               Return ext = ".jpg" OrElse ext = ".jpeg" OrElse
                                      ext = ".png" OrElse ext = ".bmp" OrElse
                                      ext = ".gif"
                           End Function) _
                    .ToArray()
                If imageFiles.Length > 0 Then
                    Using stream As New FileStream(imageFiles(0), FileMode.Open, FileAccess.Read)
                        PreviewPicturebox.Image = Image.FromStream(stream)
                    End Using
                End If
            End If
        Catch ex As Exception
            '加载图片失败
            PreviewPicturebox.Image = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' 获取编辑后的稿件对象
    ''' </summary>
    Public ReadOnly Property EditedArtwork As Artwork
        Get
            Return _artwork
        End Get
    End Property
    Private Sub EditDialogForm_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Try '支持拖放操作
            Dim paths() As String = e.Data.GetData(DataFormats.FileDrop) '拖拽的路径数组
            Dim imageFiles As New List(Of String) '图像文件列表
            Dim imageExtensions As String() = {"*.jpg", "*.jpeg", "*.png", "*.gif", "*.bmp"} '扩展名
            For Each path In paths
                If Directory.Exists(path) Then
                    '提取所有的图片文件
                    For Each pattern In imageExtensions
                        imageFiles.AddRange(Directory.GetFiles(path, pattern, SearchOption.AllDirectories))
                    Next
                ElseIf File.Exists(path) Then
                    Dim extension As String = IO.Path.GetExtension(path).ToLower()
                    If imageExtensions.Select(Function(ext) ext.Replace("*", "")).Contains(extension) Then
                        imageFiles.Add(path)
                    End If
                End If
            Next
            imageFiles = imageFiles.Distinct().ToList() '去重
            If imageFiles.Count > 0 Then
                _transaction.AddFiles(imageFiles) '添加文件到处理类
                PreviewPicturebox.Image = LoadImageFromFile(imageFiles(0)) '添加一个文件作为缩略图
                RefreshFileList()
            End If
        Catch ex As Exception
            '忽略错误
        End Try
    End Sub
    Private Sub EditDialogForm_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.None
            End If
        Catch ex As Exception
            '忽略错误
        End Try
    End Sub
#End Region

#Region "列表编辑"
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "图片文件(*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif|所有文件(*.*)|*.*"
            openFileDialog.Title = "选择要添加的图片"
            openFileDialog.Multiselect = True
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    Dim canAddedFiles As New List(Of String)
                    'Dim cannotAddedFiles As New List(Of String) ' 考虑为用户选中的不是图片的文件提供反馈
                    ' 检查给定的文件是不是图片文件
                    For Each file In openFileDialog.FileNames
                        If ImageChecker.IsImageByMIMEType(file) Then
                            canAddedFiles.Add(file)
                        Else
                            'cannotAddedFiles.Add(file)
                        End If
                    Next
                    If canAddedFiles.Count > 0 Then
                        _transaction.AddFiles(canAddedFiles) '添加文件到处理类
                        PreviewPicturebox.Image = LoadImageFromFile(canAddedFiles(0)) '添加一个文件作为缩略图
                        PreviewPicturebox.Refresh()
                        RefreshFileList()
                    End If
                Catch ex As Exception
                    '不处理
                End Try
            End If
        End Using
    End Sub
    Private Sub BtnDel_Click(sender As Object, e As EventArgs) Handles BtnDel.Click
        If LstBox.SelectedItem Is Nothing Then
            MessageBox.Show("请先选择要删除的文件", "Furry Art Studio",
                          MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim selectedItem = CType(LstBox.SelectedItem, FileItemInfo)
        Dim result = MessageBox.Show($"确定要删除文件 '{selectedItem.FileName}' 吗？",
                                    "确认删除", MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            If _transaction.DeleteFile(selectedItem.FileName) Then
                RefreshFileList() '更新文件列表
                MessageBox.Show("文件已标记为删除", "Furry Art Studio",
                              MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("删除失败，文件不存在", "Furry Art Studio",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub
    Private Sub BtnSetPreview_Click(sender As Object, e As EventArgs) Handles BtnSetPreview.Click
        Dim selectedItem = CType(LstBox.SelectedItem, FileItemInfo)
        PreviewPicturebox.Image = LoadImageFromFile(selectedItem.FullPath)
    End Sub
    Private Sub RefreshFileList()
        If _transaction Is Nothing Then
            LstBox.DataSource = Nothing
            Return
        End If
        Dim files = _transaction.GetFileList()
        LstBox.DataSource = files
        '设置显示格式
        LstBox.DrawMode = DrawMode.OwnerDrawFixed
        LstBox.ItemHeight = 20
    End Sub
    ''' <summary>
    ''' 自定义绘制ListBox项
    ''' </summary>
    Private Sub LstBox_DrawItem(sender As Object, e As DrawItemEventArgs) Handles LstBox.DrawItem
        If e.Index < 0 Then Return
        Dim lb = CType(sender, ListBox)
        Dim item = CType(lb.Items(e.Index), FileItemInfo)
        '绘制背景
        e.DrawBackground()
        '根据状态设置字体样式
        Dim fontStyle As FontStyle = FontStyle.Regular
        Select Case item.Status
            Case FileStatus.Added
                fontStyle = FontStyle.Bold
            Case FileStatus.Deleted
                fontStyle = FontStyle.Italic
            Case FileStatus.Modified
                fontStyle = FontStyle.Bold Or FontStyle.Italic
        End Select
        Using font As New Font(e.Font, fontStyle)
            '绘制文件名
            e.Graphics.DrawString(item.FileName, font, If(IsDarkMode(), Brushes.White, Brushes.Black), e.Bounds)
        End Using
        '绘制焦点框
        e.DrawFocusRectangle()
    End Sub
    Private Sub LstBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstBox.SelectedIndexChanged
        If LstBox.SelectedItems.Count = 0 Then
            BtnSetPreview.Enabled = False
            BtnDel.Enabled = False
        Else
            Dim selectedItem = CType(LstBox.SelectedItem, FileItemInfo)
            Dim filePath As String = selectedItem.FullPath
            SelectPictureBox.SizeMode = PictureBoxSizeMode.Zoom
            SelectPictureBox.Image = Nothing
            If File.Exists(filePath) Then
                Using stream As New FileStream(filePath, FileMode.Open, FileAccess.Read)
                    SelectPictureBox.Image = Image.FromStream(stream)
                End Using
            End If
            If _artwork.ID <> 0 Then
                BtnSetPreview.Enabled = True
            End If
            BtnDel.Enabled = True
        End If
    End Sub
#End Region

#Region "其他功能"
    ''' <summary>
    ''' 创建稿件文件夹
    ''' </summary>
    Private Function CreateArtworkFolder() As Boolean
        Try
            Dim folderPath As String = Path.Combine(_libraryPath, _artwork.UUID.ToString())
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
                _transaction.SetTargetPath(folderPath) '设置目录
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show($"创建稿件文件夹时出错：{ex.Message}", "Furry Art Studio",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Private Sub BtnModify_Click(sender As Object, e As EventArgs) Handles BtnModify.Click
        If TxtboxTitle.Text = "" Then
            _artwork.Title = "无题"
        Else
            _artwork.Title = TxtboxTitle.Text
        End If '标题
        If TxtboxAuthor.Text = "" Then
            _artwork.Author = "无名"
        Else
            _artwork.Author = TxtboxAuthor.Text
        End If '作者
        If TxtboxCharacters.Text = "" Then
            _artwork.Characters = Array.Empty(Of String)()
        Else
            _artwork.Characters = TxtboxCharacters.Text.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        End If '角色数组
        If TxtboxTags.Text = "" Then
            _artwork.Tags = Array.Empty(Of String)()
        Else
            _artwork.Tags = TxtboxTags.Text.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        End If '标签数组
        _artwork.Notes = TxtboxNotes.Text '备注
        Dim createTime As DateTime
        If TxtboxCharacters.Text = "" Then createTime = DateTime.Parse("1970-01-01 00:00:00")
        '验证创建时间格式
        If Not DateTime.TryParse(CleanInvisibleCharacters(TxtboxCreateTime.Text), createTime) Then
            MessageBox.Show("创作时间格式不正确！请使用 yyyy-MM-dd HH:mm:ss 格式", "Furry Art Studio",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtboxCreateTime.Focus()
            Return
        End If
        '验证创建时间不能晚于当前时间
        If createTime > DateTime.Now Then
            MessageBox.Show("创作时间不能晚于当前时间！", "Furry Art Studio",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtboxCreateTime.Focus()
            Return
        End If
        _artwork.CreateTime = createTime
        _artwork.UpdateTime = Now
        _artwork.ImportTime = Now
        If Not CreateArtworkFolder() Then Return '创建/更新文件夹
        _transaction.Commit() '提交数据
        Dim previewPath As String = Path.Combine(_libraryPath, _artwork.UUID.ToString(), ".preview.jpg")
        PreviewPicturebox.Image?.Save(previewPath, ImageFormat.Jpeg) '图片不为空的时候, 保存图片
        '设置DialogResult为OK, 关闭窗体
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
    ''' <summary>
    ''' 清理字符串中的不可见字符
    ''' </summary>
    Private Function CleanInvisibleCharacters(input As String) As String
        If String.IsNullOrEmpty(input) Then Return input
        '移除各种不可见字符
        Dim result As New Text.StringBuilder()
        For Each c As Char In input
            '判断是否是可打印字符
            If Not Char.IsControl(c) AndAlso
               c <> ChrW(&H200B) AndAlso ' 零宽空格
               c <> ChrW(&H200C) AndAlso ' 零宽非连接符
               c <> ChrW(&H200D) AndAlso ' 零宽连接符
               c <> ChrW(&H200E) AndAlso ' 左到右标记
               c <> ChrW(&H200F) AndAlso ' 右到左标记
               c <> ChrW(&H202A) AndAlso ' 从左到右嵌入
               c <> ChrW(&H202B) AndAlso ' 从右到左嵌入
               c <> ChrW(&H202C) AndAlso ' 弹出方向格式
               c <> ChrW(&H202D) AndAlso ' 从左到右覆盖
               c <> ChrW(&H202E) AndAlso ' 从右到左覆盖
               c <> ChrW(&H2060) AndAlso ' 单词连接符
               c <> ChrW(&H2066) AndAlso ' 从左到右隔离
               c <> ChrW(&H2067) AndAlso ' 从右到左隔离
               c <> ChrW(&H2068) AndAlso ' 第一强隔离
               c <> ChrW(&H2069) Then   ' 弹出隔离
                result.Append(c)
            End If
        Next
        Return result.ToString().Trim()
    End Function
#End Region

End Class