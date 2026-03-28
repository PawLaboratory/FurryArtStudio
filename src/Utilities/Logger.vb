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
Imports System.IO
Imports System.Text

#Region "日志级别枚举"
''' <summary>
''' 日志级别
''' </summary>
Public Enum LogLevel
    ''' <summary>
    ''' 日志级别: 调试
    ''' </summary>
    DEBUG
    ''' <summary>
    ''' 日志级别: 信息
    ''' </summary>
    INFO
    ''' <summary>
    ''' 日志级别: 警告
    ''' </summary>
    WARN
    ''' <summary>
    ''' 日志级别: 错误
    ''' </summary>
    [ERROR]
    ''' <summary>
    ''' 日志级别: 严重
    ''' </summary>
    FATAL
End Enum '日志级别枚举
#End Region

''' <summary>
''' 全局日志记录器实例, 此类无法被继承
''' </summary>
Public NotInheritable Class Logger
#Region "初始化"
    '单例实例
    Private Shared _instance As Logger
    Private Shared ReadOnly _lockObj As New Object()
    '基本属性
    Private _logPath As String
    Private _minLogLevel As LogLevel
    Private _autoFlush As Boolean
    Private _encoding As Encoding
    Private _dateFormat As String
    Private _logFormat As String
    Private Shared ReadOnly _defaultConfig As New LoggerConfig() '默认配置
    Private Shared _isInitialized As Boolean = False
    '私有构造函数
    Private Sub New(config As LoggerConfig)
        If config Is Nothing Then config = _defaultConfig '如果配置参数不存在, 则使用默认配置
        _logPath = config.LogPath
        _minLogLevel = config.MinLogLevel
        _autoFlush = config.AutoFlush
        _encoding = config.Encoding
        _dateFormat = config.DateFormat
        _logFormat = config.LogFormat
    End Sub
    ''' <summary>
    ''' 初始化 Logger 实例
    ''' </summary>
    Public Shared Sub Initialize(config As LoggerConfig)
        If config Is Nothing Then Throw New ArgumentNullException(NameOf(config))
        SyncLock _lockObj '保证原子性
            If _isInitialized Then
                Throw New InvalidOperationException("Logger 已初始化")
            End If
            _instance = New Logger(config)
            _isInitialized = True
        End SyncLock
    End Sub
#End Region

#Region "实例相关方法"
    ''' <summary>
    ''' 获取单例实例
    ''' </summary>
    Public Shared ReadOnly Property Instance As Logger
        Get
            If Not _isInitialized Then
                SyncLock _lockObj
                    If Not _isInitialized Then
                        Throw New InvalidOperationException("请先对 Logger 进行初始化")
                    End If
                End SyncLock
            End If
            Return _instance
        End Get
    End Property
    Private Sub Log(message As String, level As LogLevel, Optional ex As Exception = Nothing)
        If level < _minLogLevel Then Return '过滤掉低于特定等级的消息
        Dim logEntry As New StringBuilder(_logFormat)
        logEntry.Replace("{timestamp}", $"§8{Now.ToString(_dateFormat)}§r") '替换时间戳
        Select Case level
            Case LogLevel.DEBUG
                logEntry.Replace("{level}", "§r[§bDEBUG§r]")
            Case LogLevel.INFO
                logEntry.Replace("{level}", "§r[§aINFO§r]")
            Case LogLevel.WARN
                logEntry.Replace("{level}", "§r[§eWARN§r]")
            Case LogLevel.ERROR
                logEntry.Replace("{level}", "§r[§cERROR§r]")
            Case LogLevel.FATAL
                logEntry.Replace("{level}", "§r[§cFATAL§r]")
        End Select '替换日志级别
        logEntry.Replace("{message}", message) '替换消息文本
        If ex IsNot Nothing Then '如果有异常, 添加异常信息
            logEntry.AppendLine()
            logEntry.AppendLine($"异常类型: {ex.GetType}")
            logEntry.AppendLine($"{ex.StackTrace}")
        End If
        Dim logMessage As String = RemoveColorCodes(logEntry.ToString()) '将颜色字符过滤以便写入文件
        ConsoleWriteLineWithColor(logEntry.ToString()) '将日志输出到控制台
        Try
            Using writer As New StreamWriter(Path.Combine(_logPath, "Latest.log"), True, _encoding) '将过滤后的字符写入文件
                writer.WriteLine(logMessage)
                If _autoFlush Then writer.Flush() '刷新缓冲区
            End Using
        Catch exIO As IOException
            '如果文件写入失败, 尝试输出到控制台
            Log($"无法写入日志文件: {exIO.Message}", LogLevel.ERROR)
        End Try
    End Sub
#End Region

#Region "外部日志方法"
    ''' <summary>
    ''' 输出一条“调试”日志
    ''' </summary>
    Public Shared Sub Debug(message As String)
        Instance.Log(message, LogLevel.DEBUG)
    End Sub
    ''' <summary>
    ''' 输出一条“信息”日志
    ''' </summary>
    Public Shared Sub Info(message As String)
        Instance.Log(message, LogLevel.INFO)
    End Sub
    ''' <summary>
    ''' 输出一条“警告”日志
    ''' </summary>
    Public Shared Sub Warning(message As String)
        Instance.Log(message, LogLevel.WARN)
    End Sub
    ''' <summary>
    ''' 输出一条“错误”日志
    ''' </summary>
    Public Shared Sub [Error](message As String, Optional ex As Exception = Nothing)
        Instance.Log(message, LogLevel.ERROR, ex)
    End Sub
    ''' <summary>
    ''' 输出一条“严重”日志
    ''' </summary>
    Public Shared Sub Fatal(message As String, Optional ex As Exception = Nothing)
        Instance.Log(message, LogLevel.FATAL, ex)
    End Sub
#End Region

#Region "彩色字符方法"
    Private ReadOnly colorCodes As New Dictionary(Of Char, ConsoleColor) From {
    {"0", ConsoleColor.Black},
    {"1", ConsoleColor.DarkBlue},
    {"2", ConsoleColor.DarkGreen},
    {"3", ConsoleColor.DarkCyan},
    {"4", ConsoleColor.DarkRed},
    {"5", ConsoleColor.DarkMagenta},
    {"6", ConsoleColor.DarkYellow},
    {"7", ConsoleColor.Gray},
    {"8", ConsoleColor.DarkGray},
    {"9", ConsoleColor.Blue},
    {"a", ConsoleColor.Green},
    {"b", ConsoleColor.Cyan},
    {"c", ConsoleColor.Red},
    {"d", ConsoleColor.Magenta},
    {"e", ConsoleColor.Yellow},
    {"f", ConsoleColor.White},
    {"r", ConsoleColor.Gray} '重置颜色(默认为灰色)
} '颜色常量表
    ''' <summary>
    ''' 过滤颜色字符
    ''' </summary>
    ''' <param name="Input">文字内容</param>
    Private Function RemoveColorCodes(Input As String) As String
        Dim result As New StringBuilder()
        Dim i As Integer = 0
        While i < Input.Length
            If Input(i) = "§"c AndAlso i + 1 < Input.Length Then
                '跳过颜色代码
                i += 2
            Else
                result.Append(Input(i))
                i += 1
            End If
        End While
        Return result.ToString()
    End Function
    ''' <summary>
    ''' 输出带颜色的日志
    ''' </summary>
    ''' <param name="message">文字内容</param>
    Private Sub ConsoleWriteLineWithColor(message As String)
        Dim buffer As New StringBuilder()
        Dim currentColor As ConsoleColor = Console.ForegroundColor
        For i As Integer = 0 To message.Length - 1
            If message(i) = "§"c AndAlso i + 1 < message.Length Then
                '输出缓冲内容(应用当前颜色)
                If buffer.Length > 0 Then
                    Console.Write(buffer.ToString())
                    buffer.Clear()
                End If
                '处理颜色代码
                Dim code As Char = message(i + 1)
                If colorCodes.ContainsKey(code) Then
                    Console.ForegroundColor = colorCodes(code)
                End If
                i += 1 '跳过颜色代码
            Else
                buffer.Append(message(i))
            End If
        Next
        If buffer.Length > 0 Then '输出剩余内容
            Console.Write(buffer.ToString().TrimEnd())
        End If
        '重置颜色
        Console.ForegroundColor = currentColor
        Console.WriteLine()
    End Sub
#End Region

#Region "日志系统相关属性"
    ''' <summary>
    ''' 获得当前日志系统最低日志级别
    ''' </summary>
    Public ReadOnly Property MinLogLevel As LogLevel
        Get
            Return _minLogLevel
        End Get
    End Property
    ''' <summary>
    ''' 获得当前日志文件路径
    ''' </summary>
    Public ReadOnly Property LogFilePath As String
        Get
            Return Path.Combine(_logPath, "Latest.log")
        End Get
    End Property
    ''' <summary>
    ''' 获得当前日志目录
    ''' </summary>
    Public ReadOnly Property LogPath As String
        Get
            Return _logPath
        End Get
    End Property
#End Region
End Class
''' <summary>
''' 日志配置类
''' </summary>
Public Class LoggerConfig
    ''' <summary>
    ''' 日志目录路径
    ''' </summary>
    Public Property LogPath As String = Path.Combine(AppContext.BaseDirectory, "Logs")
    ''' <summary>
    ''' 最低日志级别
    ''' </summary>
    Public Property MinLogLevel As LogLevel = LogLevel.INFO
    ''' <summary>
    ''' 是否自动刷新缓冲区
    ''' </summary>
    Public Property AutoFlush As Boolean = False
    ''' <summary>
    ''' 文件编码
    ''' </summary>
    Public Property Encoding As Encoding = Encoding.UTF8
    ''' <summary>
    ''' 日期时间格式
    ''' </summary>
    Public Property DateFormat As String = "HH:mm:ss.fff"
    ''' <summary>
    ''' 日志格式(支持颜色代码)
    ''' </summary>
    Public Property LogFormat As String = "{timestamp} {level} {message}"
End Class