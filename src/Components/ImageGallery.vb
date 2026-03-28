' FurryArtStudio - 本地稿件管理工具
' Copyright 2026 xionglongztz/PawLaboratory
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

Imports System.ComponentModel
Public Class ImageGallery
#Region "基本"
    Inherits ScrollableControl
    '内部变量与常量
    Private _allImages As New List(Of GalleryImage)
    Private _currentPageImages As New List(Of GalleryImage)
    Private _selectedImages As New List(Of GalleryImage)

    Private _layoutItems As New List(Of LayoutItem)

    Private _minItemSize As Integer = 120
    Private _maxItemSize As Integer = 240

    Private _pageSize As Integer = 100
    Private _currentPage As Integer = 1 '当前页码
    Private _totalPages As Integer = 1 '总页码

    Private _displayMode As GalleryDisplayMode = GalleryDisplayMode.Normal

    Private _pageRects As New Dictionary(Of Integer, Rectangle)

    Private _selectionAccentColor As Color = Color.Blue
    Private _badgeColor As Color = Color.Red

    Private Const ItemPadding As Integer = 8

    Private _focusIndex As Integer = -1 '当前键盘焦点项的索引
    Private _anchorIndex As Integer = -1 '范围选择的锚点索引
    '事件
    ''' <summary>
    ''' 当已选的稿件变化时调用
    ''' </summary>
    Public Event SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
    'Public Event SelectionChanged(selectedImages As IReadOnlyList(Of GalleryImage))
    ''' <summary>
    ''' 稿件双击时调用
    ''' </summary>
    ''' <param name="image">被双击的单个稿件</param>
    Public Event ImageDoubleClicked(image As GalleryImage)
    ''' <summary>
    ''' 稿件右键时调用
    ''' </summary>
    ''' <param name="image">被右键的稿件</param>
    Public Event ImageRightClicked(image As GalleryImage)
    ''' <summary>
    ''' 切换页面时调用
    ''' </summary>
    ''' <param name="page">切换后的页面</param>
    Public Event PageChanged(page As Integer)

    ''' <summary>
    ''' 构造函数
    ''' </summary>
    Public Sub New()
        Me.AutoScroll = True
        Me.DoubleBuffered = True
        Me.BackColor = Color.White
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
    End Sub
#End Region

#Region "公共属性"
    ''' <summary>
    ''' 隐藏继承的Text属性
    ''' </summary>
    <Browsable(False)>
    <EditorBrowsable(EditorBrowsableState.Never)>
    Public Shadows Property Text As String
        Get
            Return String.Empty
        End Get
        Set(value As String)
            '什么都不做
        End Set
    End Property
    ''' <summary>
    ''' 获取或设置每个图像的最小尺寸(单位:像素)
    ''' </summary>
    <Browsable(True)>
    <Description("获取或设置每个图像的最小尺寸(单位:像素)")>
    Public Property MinItemSize As Integer
        Get
            Return _minItemSize
        End Get
        Set(value As Integer)
            _minItemSize = value
            RecalculateLayout()
        End Set
    End Property
    ''' <summary>
    ''' 获取或设置每个图像的最大尺寸(单位:像素)
    ''' </summary>
    ''' <returns></returns>
    <Browsable(True)>
    <Description("获取或设置每个图像的最大尺寸(单位:像素)")>
    Public Property MaxItemSize As Integer
        Get
            Return _maxItemSize
        End Get
        Set(value As Integer)
            _maxItemSize = value
            RecalculateLayout()
        End Set
    End Property
    ''' <summary>
    ''' 获取或设置每一页的元素数量最大值
    ''' </summary>
    <Browsable(True)>
    <Description("获取或设置每一页的元素数量最大值")>
    Public Property PageSize As Integer
        Get
            Return _pageSize
        End Get
        Set(value As Integer)
            _pageSize = Math.Max(1, value)
            SetPage(1)
        End Set
    End Property
    ''' <summary>
    ''' 获取当前已选择的稿件
    ''' </summary>
    Public ReadOnly Property SelectedImages As List(Of GalleryImage)
        Get
            Return _selectedImages
        End Get
    End Property
    ''' <summary>
    ''' 获取或设置当前图片墙的外观显示模式
    ''' </summary>
    ''' <returns></returns>
    Public Property DisplayMode As GalleryDisplayMode
        Get
            Return _displayMode
        End Get
        Set(value As GalleryDisplayMode)
            _displayMode = value
            Invalidate()
        End Set
    End Property
    ''' <summary>
    ''' 获得总图片数量
    ''' </summary>
    Public ReadOnly Property TotalImageCount As Integer
        Get
            Return _allImages.Count
        End Get
    End Property
    ''' <summary>
    ''' 获得总页数
    ''' </summary>
    Public ReadOnly Property TotalPages As Integer
        Get
            Return _totalPages
        End Get
    End Property
    ''' <summary>
    ''' 获得当前页
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Page As Integer
        Get
            Return _currentPage
        End Get
    End Property
    ''' <summary>
    ''' 获取或设置选中项的颜色
    ''' </summary>
    <Browsable(True)>
    <Description("获取或设置选中项的颜色")>
    Public Property SelectionAccentColor As Color
        Get
            Return _selectionAccentColor
        End Get
        Set(value As Color)
            _selectionAccentColor = value
        End Set
    End Property
    ''' <summary>
    ''' 获取或设置角标的颜色
    ''' </summary>
    <Browsable(True)>
    <Description("获取或设置角标的颜色")>
    Public Property BadgeColor As Color
        Get
            Return _badgeColor
        End Get
        Set(value As Color)
            _badgeColor = value
        End Set
    End Property
#End Region

#Region "公共方法"
    ''' <summary>
    ''' 添加图片
    ''' </summary>
    ''' <param name="image">要被添加的图片</param>
    ''' <param name="autoNavigate">(可选)是否自动跳转到该页, 默认为False</param>
    Public Sub AddImage(image As GalleryImage, Optional autoNavigate As Boolean = False)
        If image Is Nothing Then Return

        '加入总列表并保持排序
        _allImages.Add(image)
        _allImages = _allImages.OrderBy(Function(i) i.ID).ToList()

        '计算该图片所在的页码
        Dim index = _allImages.FindIndex(Function(i) i Is image)
        If index < 0 Then Return

        Dim targetPage As Integer = (index \ _pageSize) + 1

        '是否自动跳转到该页
        If autoNavigate Then
            SetPage(targetPage)
        ElseIf targetPage = _currentPage Then
            '在当前页，直接刷新
            LoadCurrentPage()
        End If
    End Sub
    ''' <summary>
    ''' 清空所有图片
    ''' </summary>
    Public Sub ClearImages()
        _allImages.Clear()
        _currentPageImages.Clear()
        _layoutItems.Clear()
        _selectedImages.Clear()
        _currentPage = 1
        _totalPages = 1

        For Each img In _allImages
            If img.Thumbnail IsNot Nothing Then
                img.Thumbnail.Dispose()
                img.Thumbnail = Nothing
            End If
        Next '释放全部缓存资源

        Me.AutoScrollMinSize = Size.Empty
        Me.AutoScrollPosition = Point.Empty
        Invalidate()
    End Sub
    ''' <summary>
    ''' 切换到指定页码
    ''' </summary>
    ''' <param name="page">页码</param>
    Public Sub SetPage(page As Integer)
        UpdatePages()
        Dim totalPages = _totalPages
        page = Math.Max(1, Math.Min(totalPages, page))

        If page <> _currentPage Then
            _currentPage = page
            LoadCurrentPage()
            RaiseEvent PageChanged(_currentPage)
        End If
    End Sub
    ''' <summary>
    ''' 根据ID搜索稿件
    ''' </summary>
    ''' <param name="id">ID</param>
    Public Sub SelectImageByID(id As Integer)
        Dim img = _allImages.FirstOrDefault(Function(i) i.ID = id)
        If img IsNot Nothing Then
            _selectedImages.Clear()
            _selectedImages.Add(img)
            Invalidate()
        End If
    End Sub
    ''' <summary>
    ''' 根据UUID搜索稿件
    ''' </summary>
    ''' <param name="uuid">UUID</param>
    Public Sub SelectImageByUUID(uuid As String)
        Dim img = _allImages.FirstOrDefault(Function(i) i.UUID = uuid)
        If img IsNot Nothing Then
            _selectedImages.Clear()
            _selectedImages.Add(img)
            Invalidate()
        End If
    End Sub
    ''' <summary>
    ''' 选择所有稿件
    ''' </summary>
    Public Sub SelectAll()
        _selectedImages.Clear()
        For Each gimg In _currentPageImages
            _selectedImages.Add(gimg)
        Next
        Invalidate()
        'RaiseEvent SelectionChanged(_selectedImages.AsReadOnly()) '选中稿件
        RaiseEvent SelectionChanged(Me, New SelectionChangedEventArgs(_selectedImages.AsReadOnly()))
    End Sub
    ''' <summary>
    ''' 反选所有稿件
    ''' </summary>
    Public Sub SelectReverse()
        For Each gimg In _currentPageImages
            If _selectedImages.Contains(gimg) Then
                _selectedImages.Remove(gimg)
            Else
                _selectedImages.Add(gimg)
            End If
        Next
        Invalidate()
        'RaiseEvent SelectionChanged(_selectedImages.AsReadOnly()) '选中稿件
        RaiseEvent SelectionChanged(Me, New SelectionChangedEventArgs(_selectedImages.AsReadOnly()))
    End Sub
    ''' <summary>
    ''' 取消选择所有稿件
    ''' </summary>
    Public Sub CancelSelect()
        For Each gimg In _currentPageImages
            If _selectedImages.Contains(gimg) Then
                _selectedImages.Remove(gimg)
            End If
        Next
        Invalidate()
        RaiseEvent SelectionChanged(Me, New SelectionChangedEventArgs(_selectedImages.AsReadOnly()))
    End Sub
#End Region

#Region "布局分页"
    Private Sub LoadCurrentPage()
        _currentPageImages.Clear()
        _layoutItems.Clear()

        Dim skip = (_currentPage - 1) * _pageSize
        _currentPageImages = _allImages.Skip(skip).Take(_pageSize).ToList()

        RecalculateLayout()
    End Sub
    Private Sub RecalculateLayout()
        _layoutItems.Clear()

        If _currentPageImages.Count = 0 Then
            Invalidate()
            Return
        End If

        Dim usableWidth = ClientSize.Width - ItemPadding
        Dim y As Integer = ItemPadding

        Dim itemSize = Math.Min(_maxItemSize,
            Math.Max(_minItemSize, usableWidth \ Math.Max(1, usableWidth \ _minItemSize)))

        Dim columns = Math.Max(1, usableWidth \ itemSize)
        Dim realSize = usableWidth \ columns

        Dim index As Integer = 0

        While index < _currentPageImages.Count
            For col = 0 To columns - 1
                If index >= _currentPageImages.Count Then Exit For

                Dim x = ItemPadding + col * realSize
                Dim rect As New Rectangle(x, y, realSize - ItemPadding, realSize - ItemPadding)

                _layoutItems.Add(New LayoutItem With {
                    .Image = _currentPageImages(index),
                    .Bounds = rect
                })

                index += 1
            Next
            y += realSize
        End While

        If _layoutItems.Count > 0 Then
            Dim last = _layoutItems.Last().Bounds
            Me.AutoScrollMinSize = New Size(0, last.Bottom + ItemPadding)
        Else
            Me.AutoScrollMinSize = Size.Empty
        End If

        Invalidate()
        '修改布局后焦点与锚点失效
        _focusIndex = -1
        _anchorIndex = -1
    End Sub
    ''' <summary>
    ''' 更新页数
    ''' </summary>
    Private Sub UpdatePages()
        If _pageSize <= 0 Then
            _totalPages = 1
        Else
            _totalPages = Math.Max(1, CInt(Math.Ceiling(_allImages.Count / _pageSize)))
        End If
    End Sub

#End Region

#Region "绘图"
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        e.Graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y)
        DrawBackground(e.Graphics)
        DrawImages(e.Graphics)
    End Sub
    Private Sub DrawBackground(g As Graphics)
        Select Case _displayMode
            Case GalleryDisplayMode.Dark
                g.Clear(Color.FromArgb(32, 32, 32))
            Case GalleryDisplayMode.HighContrast
                g.Clear(Color.Black)
            Case Else
                g.Clear(BackColor)
        End Select
    End Sub
    Private Sub DrawImages(g As Graphics)
        For Each item In _layoutItems
            '绘制图片
            If item.Image.Thumbnail IsNot Nothing Then
                g.DrawImage(item.Image.Thumbnail, item.Bounds)
            Else
                Using b As New SolidBrush(Color.Gray)
                    g.FillRectangle(b, item.Bounds)
                End Using
            End If
            '绘制角标
            '绘制右上角角标（显示Count数量）
            If item.Image.Count > 1 Then
                '角标尺寸为边长的20分之一
                Dim badgeSize As Integer = Math.Max(15, item.Bounds.Width \ 20)
                '计算角标位置（右上角，留一些边距）
                Dim badgeX As Integer = item.Bounds.Right - badgeSize - 2
                Dim badgeY As Integer = item.Bounds.Top + 2
                Dim badgeRect As New Rectangle(badgeX, badgeY, badgeSize, badgeSize)
                Dim badgeFontSize As Integer = badgeSize * 0.6
                '绘制方形背景
                Using badgeBrush As New SolidBrush(_badgeColor)
                    g.FillRectangle(badgeBrush, badgeRect)
                End Using
                '绘制数字
                Using badgeFont As New Font("Arial", badgeFontSize, FontStyle.Bold)
                    Using textBrush As New SolidBrush(GetForeColor(_badgeColor)) '根据颜色选择前景色
                        Dim countText As String = item.Image.Count.ToString()
                        Dim textSize As SizeF = g.MeasureString(countText, badgeFont)
                        '计算文字居中位置
                        Dim textX As Single = badgeX + (badgeSize - textSize.Width) / 2
                        Dim textY As Single = badgeY + (badgeSize - textSize.Height) / 2

                        g.DrawString(countText, badgeFont, textBrush, textX, textY)
                    End Using
                End Using
            End If
            '绘制选中边框
            If _selectedImages.Contains(item.Image) Then
                Using p As New Pen(_selectionAccentColor, 5)
                    g.DrawRectangle(p, item.Bounds)
                End Using
            End If
        Next
    End Sub
#End Region

#Region "输入处理"
    ''' <summary>
    ''' 尺寸变更时触发
    ''' </summary>
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        If _currentPageImages IsNot Nothing AndAlso _currentPageImages.Count > 0 Then
            RecalculateLayout()
        Else
            Invalidate()
        End If
    End Sub

    ''' <summary>
    ''' 鼠标按下时触发
    ''' </summary>
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        Me.Focus() '设置自身为焦点

        Dim scrollOffset As Point = Me.AutoScrollPosition
        Dim logicalPoint As New Point(
            e.X - scrollOffset.X,
            e.Y - scrollOffset.Y
        ) '修正坐标偏移

        Dim isShiftPressed As Boolean = My.Computer.Keyboard.ShiftKeyDown
        Dim isCtrlPressed As Boolean = My.Computer.Keyboard.CtrlKeyDown

        If e.Button = MouseButtons.Right Then '处理右键逻辑
            Dim index As Integer = 0
            For Each item In _layoutItems
                If item.Bounds.Contains(logicalPoint) Then
                    RaiseEvent ImageRightClicked(item.Image)
                    _focusIndex = index '记录焦点位置
                    Invalidate()
                    Return
                End If
                index += 1
            Next
            Return
        End If

        If e.Button = MouseButtons.Left Then
            Dim hitIndex As Integer = -1
            Dim index As Integer = 0
            For Each item In _layoutItems
                If item.Bounds.Contains(logicalPoint) Then
                    hitIndex = index
                    Exit For
                End If
                index += 1
            Next
            If hitIndex >= 0 Then
                If isShiftPressed AndAlso _focusIndex >= 0 Then
                    'Shift+左键: 范围选择
                    Dim startIdx As Integer = Math.Min(_focusIndex, hitIndex)
                    Dim endIdx As Integer = Math.Max(_focusIndex, hitIndex)
                    If Not isCtrlPressed Then
                        '无Ctrl时清除原有选择
                        _selectedImages.Clear()
                    End If
                    '添加范围内的所有项
                    For i As Integer = startIdx To endIdx
                        Dim img = _layoutItems(i).Image
                        If Not _selectedImages.Contains(img) Then
                            _selectedImages.Add(img)
                        End If
                    Next
                    _anchorIndex = _focusIndex '保留锚点
                ElseIf isCtrlPressed Then
                    'Ctrl+左键: 切换当前项的选择状态
                    Dim clickedImg = _layoutItems(hitIndex).Image
                    If _selectedImages.Contains(clickedImg) Then
                        _selectedImages.Remove(clickedImg)
                    Else
                        _selectedImages.Add(clickedImg)
                    End If
                    'Ctrl点击时不改变其他项的选择状态
                    _anchorIndex = -1 '清除锚点
                Else
                    '普通左键: 单选
                    _selectedImages.Clear()
                    _selectedImages.Add(_layoutItems(hitIndex).Image)
                    _anchorIndex = -1 '清除锚点
                End If

                _focusIndex = hitIndex '更新焦点索引
                Invalidate()
                RaiseEvent SelectionChanged(Me, New SelectionChangedEventArgs(_selectedImages.AsReadOnly()))
                Return
            End If

            ' 点击空白区域：取消选择
            If Not isCtrlPressed AndAlso Not isShiftPressed Then
                _selectedImages.Clear()
                _focusIndex = -1
                _anchorIndex = -1
                Invalidate()
                'RaiseEvent SelectionChanged(_selectedImages.AsReadOnly()) '选中稿件
                RaiseEvent SelectionChanged(Me, New SelectionChangedEventArgs(_selectedImages.AsReadOnly()))
            End If
        End If

        For Each kv In _pageRects
            If kv.Value.Contains(logicalPoint) Then
                SetPage(kv.Key)
                Return
            End If
        Next

    End Sub

    ''' <summary>
    ''' 鼠标双击时触发
    ''' </summary>
    Protected Overrides Sub OnMouseDoubleClick(e As MouseEventArgs)
        MyBase.OnMouseDoubleClick(e)
        Dim scrollOffset As Point = Me.AutoScrollPosition
        Dim logicalPoint As New Point(
            e.X - scrollOffset.X,
            e.Y - scrollOffset.Y
        ) '修正坐标偏移
        For Each item In _layoutItems
            If item.Bounds.Contains(logicalPoint) Then
                RaiseEvent ImageDoubleClicked(item.Image)
                Return
            End If
        Next
    End Sub

    ''' <summary>
    ''' 按下键盘时触发
    ''' </summary>
    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        Me.Focus() '设置自身为焦点

        If _currentPageImages.Count = 0 Then Return
        Select Case e.KeyCode
            Case Keys.Left, Keys.Right, Keys.Up, Keys.Down
                If _layoutItems.Count = 0 Then Return
                '确保有焦点项(默认选第一个)
                If _focusIndex < 0 Then _focusIndex = 0
                Dim currentFocus As Integer = _focusIndex
                Dim newIndex As Integer = currentFocus
                '计算移动后的索引(网格边界内)
                Dim keyCode = e.KeyCode And Keys.KeyCode
                '上和左是上一张, 下和右是下一张
                If keyCode = Keys.Up OrElse keyCode = Keys.Left Then
                    newIndex = Math.Max(0, currentFocus - 1)
                Else
                    newIndex = Math.Min(_layoutItems.Count - 1, currentFocus + 1)
                End If
                If newIndex = currentFocus Then Return '无法移动
                If e.Shift Then
                    'Shift+方向键: 连续范围选择
                    If _anchorIndex < 0 Then _anchorIndex = currentFocus

                    Dim startIdx As Integer = Math.Min(_anchorIndex, newIndex)
                    Dim endIdx As Integer = Math.Max(_anchorIndex, newIndex)

                    _selectedImages.Clear()
                    For i As Integer = startIdx To endIdx
                        _selectedImages.Add(_layoutItems(i).Image)
                    Next
                    _focusIndex = newIndex
                Else
                    '无Shift: 移动到新项并单选
                    _selectedImages.Clear()
                    _selectedImages.Add(_layoutItems(newIndex).Image)
                    _focusIndex = newIndex
                    _anchorIndex = -1 '清除锚点
                End If
                '滚动到新项可见
                EnsureVisible(newIndex)
                Invalidate()
                RaiseEvent SelectionChanged(Me, New SelectionChangedEventArgs(_selectedImages.AsReadOnly()))
                e.Handled = True
            Case Keys.PageUp
                CancelSelect()
                SetPage(_currentPage - 1)
                e.Handled = True
            Case Keys.PageDown
                CancelSelect()
                SetPage(_currentPage + 1)
                e.Handled = True
            Case Keys.Enter
                '双击事件: 优先焦点项, 其次第一个选中项, 最后第一个项
                Dim target As GalleryImage = Nothing
                If _focusIndex >= 0 AndAlso _focusIndex < _layoutItems.Count Then
                    target = _layoutItems(_focusIndex).Image
                ElseIf _selectedImages.Count > 0 Then
                    target = _selectedImages(0)
                ElseIf _layoutItems.Count > 0 Then
                    target = _layoutItems(0).Image
                End If
                If target IsNot Nothing Then
                    RaiseEvent ImageDoubleClicked(target)
                    e.Handled = True
                End If
            Case Keys.Apps
                '右键菜单事件
                Dim target As GalleryImage = Nothing
                If _focusIndex >= 0 AndAlso _focusIndex < _layoutItems.Count Then
                    target = _layoutItems(_focusIndex).Image
                ElseIf _selectedImages.Count > 0 Then
                    target = _selectedImages(0)
                ElseIf _layoutItems.Count > 0 Then
                    target = _layoutItems(0).Image
                End If
                If target IsNot Nothing Then
                    RaiseEvent ImageRightClicked(target)
                    e.Handled = True
                End If
            Case Keys.Home
                If _layoutItems.Count > 0 Then
                    If e.Shift Then
                        'Shift+Home: 从锚点选择到第一个
                        If _anchorIndex < 0 Then _anchorIndex = _focusIndex
                        Dim startIdx As Integer = 0
                        Dim endIdx As Integer = Math.Max(_anchorIndex, 0)

                        _selectedImages.Clear()
                        For i As Integer = startIdx To endIdx
                            _selectedImages.Add(_layoutItems(i).Image)
                        Next
                        _focusIndex = 0
                    Else
                        'Home: 跳到第一个并单选
                        _selectedImages.Clear()
                        _selectedImages.Add(_layoutItems(0).Image)
                        _focusIndex = 0
                        _anchorIndex = -1
                    End If
                    EnsureVisible(0)
                    Invalidate()
                    RaiseEvent SelectionChanged(Me, New SelectionChangedEventArgs(_selectedImages.AsReadOnly()))
                    e.Handled = True
                End If
            Case Keys.End
                If _layoutItems.Count > 0 Then
                    Dim lastIndex = _layoutItems.Count - 1
                    If e.Shift Then
                        'Shift+End: 从锚点选择到最后一个
                        If _anchorIndex < 0 Then _anchorIndex = _focusIndex
                        Dim startIdx As Integer = Math.Min(_anchorIndex, lastIndex)
                        Dim endIdx As Integer = lastIndex

                        _selectedImages.Clear()
                        For i As Integer = startIdx To endIdx
                            _selectedImages.Add(_layoutItems(i).Image)
                        Next
                        _focusIndex = lastIndex
                    Else
                        'End: 跳到最后一个并单选
                        _selectedImages.Clear()
                        _selectedImages.Add(_layoutItems(lastIndex).Image)
                        _focusIndex = lastIndex
                        _anchorIndex = -1
                    End If
                    EnsureVisible(lastIndex)
                    Invalidate()
                    RaiseEvent SelectionChanged(Me, New SelectionChangedEventArgs(_selectedImages.AsReadOnly()))
                    e.Handled = True
                End If
        End Select
    End Sub
    Private Sub EnsureVisible(index As Integer)
        If index < 0 OrElse index >= _layoutItems.Count Then Return
        Dim rect As Rectangle = _layoutItems(index).Bounds
        Dim currentX As Integer = -Me.AutoScrollPosition.X
        Dim currentY As Integer = -Me.AutoScrollPosition.Y
        Dim clientH As Integer = Me.ClientSize.Height

        If rect.Top < currentY Then
            ' 需要向上滚动
            Me.AutoScrollPosition = New Point(currentX, rect.Top)
        ElseIf rect.Bottom > currentY + clientH Then
            ' 需要向下滚动
            Me.AutoScrollPosition = New Point(currentX, rect.Bottom - clientH)
        End If
    End Sub

#End Region

#Region "内部类型"
    Private Class LayoutItem
        Public Property Image As GalleryImage
        Public Property Bounds As Rectangle
    End Class
#End Region

End Class
