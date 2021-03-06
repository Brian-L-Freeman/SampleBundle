﻿Imports System.ComponentModel
Public Class WeekChooser
    Inherits DockPanel
    Implements INotifyPropertyChanged

#Region "Properties"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public SystemChange As Boolean
    Public PeriodChange As Boolean
    Public Property MinWeek As Byte
    Public Property MaxWeek As Byte
    Public Property HeldWeek As Byte
    Public Property ChooserParent As Object
    Public Property SelectedCount As Byte
    Public Property DisableSelectAllWeeks As Boolean
    Public Property DisableHideWeeks As Boolean
    Private _currentweek As Byte
    Public Property CurrentWeek As Byte
        Get
            Return _currentweek
        End Get
        Set(value As Byte)
            HeldWeek = _currentweek
            _currentweek = value
            If value > 0 Then
                SelectedCount = 0
                For Each b As Border In Children
                    If b.Tag <> "Label" Then
                        Dim tb As TextBlock = b.Child
                        If FormatNumber(tb.Text, 0) <> value Then
                            tb.Foreground = Brushes.LightGray
                            tb.FontSize = 16
                            tb.FontWeight = FontWeights.Normal
                        Else
                            tb.FontWeight = FontWeights.SemiBold
                            tb.Foreground = Brushes.Black
                            tb.FontSize = 24
                            If b.IsEnabled = True Then SelectedCount += 1
                        End If
                    End If
                Next
            End If

            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(“Week”))
        End Set
    End Property

#End Region

#Region "Constructor"
    Public Sub New(MinW As Byte, MaxW As Byte, CurW As Byte)
        Dim ct As Byte
        MinWeek = MinW
        MaxWeek = MaxW
        '// Create chooser label
        Dim BorderLabel As New Border With {.BorderBrush = Brushes.Black, .VerticalAlignment = VerticalAlignment.Center,
            .Name = "brdLabel", .Tag = "Label"}
        Dim TextLabel As New TextBlock With {.Text = "  Week: ", .TextAlignment = TextAlignment.Center,
        .HorizontalAlignment = HorizontalAlignment.Center, .FontSize = 16, .Name = "tbLabel", .Tag = "Label"}
        BorderLabel.Child = TextLabel
        Children.Add(BorderLabel)
        For ct = 1 To 5
            Dim brdWeek As New Border With {.BorderBrush = Brushes.Black, .Width = 32, .VerticalAlignment = VerticalAlignment.Center,
            .Name = "brdW" & ct}
            Dim tbWeek As New TextBlock With {.Text = ct, .TextAlignment = TextAlignment.Center, .HorizontalAlignment = HorizontalAlignment.Center,
                .FontSize = 16, .Tag = ct, .Name = "tbW" & ct}
            AddHandler brdWeek.MouseEnter, AddressOf HoverOverWeek
            AddHandler tbWeek.MouseEnter, AddressOf HoverOverWeek
            AddHandler brdWeek.MouseLeave, AddressOf LeaveWeek
            AddHandler tbWeek.MouseLeave, AddressOf LeaveWeek
            AddHandler tbWeek.PreviewMouseDown, AddressOf ChooseWeek
            brdWeek.Child = tbWeek
            Children.Add(brdWeek)
        Next
        CurrentWeek = CurW
        EnableWeeks()
    End Sub

#End Region

#Region "Public Methods"
    Public Sub EnableWeeks()
        For Each brd As Border In Children
            If brd.Tag <> "Label" Then
                Dim tb As TextBlock = brd.Child
                If DisableHideWeeks = False And ((FormatNumber(tb.Tag, 0) < MinWeek) Or (FormatNumber(tb.Tag, 0) > MaxWeek)) Then
                    brd.IsEnabled = False
                    brd.Visibility = Visibility.Hidden
                Else
                    brd.IsEnabled = True
                    brd.Visibility = Visibility.Visible
                End If
            End If
        Next
    End Sub

    Public Sub Reset()
        For Each brd As Border In Children
            Dim tb As TextBlock = brd.Child
            If brd.Tag <> "Label" Then
                tb.Foreground = Brushes.Black
                tb.FontSize = 16
                tb.FontWeight = FontWeights.SemiBold
                If brd.IsEnabled = True Then SelectedCount += 1
            End If
        Next
        CurrentWeek = 0
    End Sub

#End Region

#Region "Private Methods"
    Private Sub HoverOverWeek(sender As Object, e As MouseEventArgs)
        Dim tb As TextBlock
        If TypeOf (sender) Is TextBlock Then
            tb = sender
        Else
            Dim brd As Border = sender
            tb = brd.Child
        End If
        tb.FontSize = 30
    End Sub

    Private Sub LeaveWeek(sender As Object, e As MouseEventArgs)
        Dim tb As TextBlock
        If TypeOf (sender) Is TextBlock Then
            tb = sender
        Else
            Dim brd As Border = sender
            tb = brd.Child
        End If
        If FormatNumber(tb.Tag, 0) <> CurrentWeek Then
            tb.FontSize = 16
        Else
            tb.FontSize = 24
        End If
    End Sub

    Private Sub ChooseWeek(sender As Object, e As MouseEventArgs)
        SelectedCount = 0
        Dim tb As TextBlock
        If TypeOf (sender) Is TextBlock Then
            tb = sender
        Else
            Dim brd As Border = sender
            tb = brd.Child
        End If
        If FormatNumber(tb.Tag, 0) <> CurrentWeek Then
            CurrentWeek = FormatNumber(tb.Tag, 0)
        Else
            If CurrentWeek <> 0 And DisableSelectAllWeeks = False Then Reset()
        End If
    End Sub

#End Region

End Class
