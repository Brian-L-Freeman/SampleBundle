Imports System.ComponentModel

Public Class CurrencyBox
    Inherits BaseInput
    Implements INotifyPropertyChanged

#Region "Properties"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Highlight As Boolean
    Public tb As TextBox
    Private _debitonly As Boolean
    Private _creditonly As Boolean
    Private _systemchange As Boolean
    Private _heldvalue As String
    Private _credit As Boolean
    Public Property Credit As Boolean
        Get
            Return _credit
        End Get
        Set(value As Boolean)
            _credit = value
        End Set
    End Property

    Private _debit As Boolean
    Public Property Debit As Boolean
        Get
            Return _debit
        End Get
        Set(value As Boolean)
            _debit = value
        End Set
    End Property

    Private _setamount As Double
    Public Property SetAmount As Double
        Get
            Return _setamount
        End Get
        Set(value As Double)
            _setamount = value
            tb.Text = FormatCurrency(_setamount, 2)
        End Set
    End Property

    Private _fieldineditmode As Boolean
    Public Property FieldInEditMode As Boolean
        Get
            Return _fieldineditmode
        End Get
        Set(value As Boolean)
            _fieldineditmode = value
        End Set
    End Property


#End Region

#Region "Constructor"
    Public Sub New(FieldWidth As Integer, SelectAllUponEnteringField As Boolean, FontSize As FontSz, Optional ByVal DefaultText As String = "$0.00", Optional ForceCredit As Boolean = False, Optional ForceDebit As Boolean = False)
        MyBase.New(FieldWidth, VerticalAlignment.Top, HorizontalAlignment.Left, FontSize, TextAlignment.Right, DefaultText, TextWrapping.NoWrap)
        Credit = Not ForceDebit
        Debit = Not ForceCredit
        _debitonly = ForceDebit
        _creditonly = ForceCredit
        Highlight = SelectAllUponEnteringField
        tb = Children(1)
        AddHandler tb.KeyDown, AddressOf EnterKeyIsTab
        AddHandler tb.GotFocus, AddressOf EnterField
        AddHandler tb.LostFocus, AddressOf ExitField
        AddHandler tb.TextChanged, AddressOf ValidateText
        ' Field width for 8pt = 80
        '             for 12pt= 112
        '             for 16pt= 140
        '             for 18pt= 160
        '             for 24pt= 204
    End Sub
#End Region

#Region "Private Methods"
    Private Sub EnterKeyIsTab(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Enter Then
            Dim dr As New TraversalRequest(FocusNavigationDirection.Next)
            tb.MoveFocus(dr)
            e.Handled = True
        End If
    End Sub

    Private Sub ValidateText(sender As Object, e As EventArgs)
        Dim t As TextBox = sender
        If _systemchange = True Then Exit Sub
        Try
            Dim cval As Double = FormatCurrency(t.Text, 2)
        Catch ex As Exception
            Flare = True
            Exit Sub
        End Try
        Flare = False
    End Sub

    Private Sub EnterField(sender As Object, e As EventArgs)
        FieldInEditMode = True
        Dim t As TextBox = sender
        _heldvalue = t.Text
        _systemchange = True
        '// Remove currency symbol, if present  
        t.Text = t.Text.Replace("$", "")

        '// Remove negative parentheses, if present
        If InStr(1, t.Text, "(") > 0 Then
            t.Text = t.Text.Replace("(", "")
            t.Text = t.Text.Replace(")", "")
            t.Text = "-" & t.Text
        End If

        If Highlight = True Then
            t.SelectAll()
        Else
            t.CaretIndex = t.Text.Length
        End If
        _systemchange = False

    End Sub

    Private Sub ExitField(sender As Object, e As EventArgs)
        FieldInEditMode = False
        Dim t As TextBox = sender

        Try
            Dim cval As Double = FormatNumber(t.Text, 2)
            If (Debit = False And cval > 0) Or (Credit = False And cval < 0) Then
                _systemchange = True
                Flare = True
            Else
                Flare = False
            End If
            t.Text = FormatCurrency(cval, 2)
            SetAmount = FormatNumber(cval, 2)
            _systemchange = False
        Catch ex As Exception
            Flare = True
            Me.Focus()
        End Try

        Try
            Dim cval As Double = FormatNumber(t.Text, 2)
            If (_debitonly = True And cval < 0) Or (_creditonly = True And cval > 0) Then
                _systemchange = True
                Flare = False
                t.Text = FormatCurrency(-cval, 2)
                SetAmount = FormatNumber(-cval, 2)
                _systemchange = False
            End If
        Catch ex As Exception
            Flare = True
            Me.Focus()
        End Try
        If t.Text = _heldvalue Then Exit Sub
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(“Amountchanged”))
    End Sub

#End Region

End Class
