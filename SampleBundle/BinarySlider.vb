Imports System.Windows.Threading
Public Class BinarySlider
    Inherits Grid

#Region "Properties"
    Public Enum SliderSize
        Small
        Medium
        Standard
        Large
        Huge
    End Enum

    Public Enum ColorSchemes
        GreenRed
        White
        WhiteYellow
    End Enum

    Public TimerActive As Boolean
    Private _firsttextchoice As TextBlock
    Private _secondtextchoice As TextBlock
    Private _rbgpanel As RadialGradientBrush
    Private _brdpanel As Border
    Private _verttextpad As Integer
    Private _internalfontsize As Integer
    Private _internalheight As Integer
    Private _internalwidth As Integer
    Private _firstcolorchoice As Brush = Brushes.LightGreen
    Private _secondcolorchoice As Brush = Brushes.PaleVioletRed
    Private _firstpanelcolor As Color = Colors.DarkBlue
    Private _secondpanelcolor As Color = Colors.White
    Private _choiceval As Boolean
    Private _slidertimer As DispatcherTimer
    Private _panelxpos As Integer

    Public Property ChoiceVal As Boolean
        Get
            Return _choiceval
        End Get
        Set(value As Boolean)
            If TimerActive = True Then Exit Property
            _choiceval = value
            _slidertimer = New DispatcherTimer()
            AddHandler _slidertimer.Tick, AddressOf SlidePanel
            _slidertimer.Interval = New TimeSpan(0, 0, 0, 0, 0.25)
            TimerActive = True
            _slidertimer.Start()
        End Set
    End Property

#End Region

#Region "Constructor"
    Public Sub New(InstanceSize As SliderSize, Option1 As String, Option2 As String, Optional Extend As Byte = 1, Optional ShadowOn As Boolean = False, Optional ColorScheme As ColorSchemes = ColorSchemes.GreenRed)

        Select Case InstanceSize
            Case SliderSize.Small
                _internalheight = 20
                _internalwidth = 40 * Extend
                _internalfontsize = 8
                _verttextpad = 4
            Case SliderSize.Medium
                _internalheight = 30
                _internalwidth = 60 * Extend
                _internalfontsize = 12
                _verttextpad = 6
            Case SliderSize.Standard
                _internalheight = 40
                _internalwidth = 80 * Extend
                _internalfontsize = 18
                _verttextpad = 8
            Case SliderSize.Large
                _internalheight = 60
                _internalwidth = 120 * Extend
                _internalfontsize = 18
                _verttextpad = 16
            Case SliderSize.Huge
                _internalheight = 80
                _internalwidth = 160 * Extend
                _internalfontsize = 24
                _verttextpad = 20

        End Select

        Select Case ColorScheme
            Case ColorSchemes.GreenRed
                _firstcolorchoice = New BrushConverter().ConvertFrom("#FFA0FF97")
                _secondcolorchoice = New BrushConverter().ConvertFrom("#FFFF5959")
            Case ColorSchemes.White
                _firstcolorchoice = Brushes.White
                _secondcolorchoice = Brushes.White
            Case ColorSchemes.WhiteYellow
                _firstcolorchoice = Brushes.White
                _secondcolorchoice = Brushes.Yellow
        End Select
        Height = _internalheight
        Width = _internalwidth
        VerticalAlignment = VerticalAlignment.Top
        HorizontalAlignment = HorizontalAlignment.Left
        _firsttextchoice = New TextBlock With {.Text = Option1, .FontSize = _internalfontsize, .TextAlignment = TextAlignment.Center, .HorizontalAlignment = HorizontalAlignment.Left,
            .Width = _internalwidth / 2, .Background = _firstcolorchoice, .Height = _internalheight, .Padding = New Thickness(0, _verttextpad, 0, 0), .Margin = New Thickness(0, 0, 0, 0)}
        _secondtextchoice = New TextBlock With {.Text = Option2, .FontSize = _internalfontsize, .TextAlignment = TextAlignment.Center, .HorizontalAlignment = HorizontalAlignment.Left,
            .Width = _internalwidth / 2, .Background = _secondcolorchoice, .Height = _internalheight, .Padding = New Thickness(0, _verttextpad, 0, 0), .Margin = New Thickness((_internalwidth / 2), 0, 0, 0)}


        _rbgpanel = New RadialGradientBrush
        Dim gs1 As New GradientStop With {.Color = _firstpanelcolor, .Offset = 3}
        Dim gs2 As New GradientStop With {.Color = _secondpanelcolor}
        With _rbgpanel.GradientStops
            .Add(gs1)
            .Add(gs2)
        End With

        _brdpanel = New Border With {.HorizontalAlignment = HorizontalAlignment.Left, .Width = _internalwidth / 2, .BorderBrush = Brushes.Gray, .BorderThickness = New Thickness(1, 1, 1, 1),
            .Background = _rbgpanel}
        AddHandler _brdpanel.MouseLeftButtonDown, AddressOf PanelClick

        With Children
            .Add(_firsttextchoice)
            .Add(_secondtextchoice)
            .Add(_brdpanel)
        End With

        If ShadowOn = True Then Effect = New Effects.DropShadowEffect With {.BlurRadius = 10, .ShadowDepth = 5}
    End Sub

#End Region

#Region "Public Methods"

#End Region

#Region "Private Methods"
    Private Sub PanelClick(sender As Border, e As MouseEventArgs)
        If TimerActive = True Or IsEnabled = False Then Exit Sub
        ChoiceVal = Not ChoiceVal
    End Sub

    Private Sub SlidePanel()
        If ChoiceVal = True Then
            _panelxpos += 1
            If _panelxpos > (_internalwidth / 2) Then
                _slidertimer.Stop()
                _slidertimer = Nothing
                TimerActive = False
            Else
                _brdpanel.Margin = New Thickness(_panelxpos, 0, 0, 0)
            End If
        Else
            _panelxpos -= 1
            If _panelxpos < 0 Then
                _slidertimer.Stop()
                _slidertimer = Nothing
                TimerActive = False
            Else
                _brdpanel.Margin = New Thickness(_panelxpos, 0, 0, 0)
            End If
        End If

    End Sub

    Private Sub EnabledHandler(sender As Object, e As DependencyPropertyChangedEventArgs) Handles Me.IsEnabledChanged
        If IsEnabled = True Then
            Opacity = 1
        Else
            Opacity = 0.6
        End If
    End Sub

#End Region

End Class
