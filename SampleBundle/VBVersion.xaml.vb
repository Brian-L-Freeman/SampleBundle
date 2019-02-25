Public Class VBVersion
    Private GetRad As RadialPosition
    Private WkChoose As WeekChooser
    Private MonChoose As MonthChooser
    Private YrChoose As YearChooser

    Private Sub WindowLoaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim sld1 As New BinarySlider(BinarySlider.SliderSize.Huge, "Yes", "No",, True, BinarySlider.ColorSchemes.GreenRed)
        Dim sld2 As New BinarySlider(BinarySlider.SliderSize.Large, "Da", "Nyet",, True, BinarySlider.ColorSchemes.GreenRed) With {.Margin = New Thickness(10, 0, 0, 0)}
        Dim sld3 As New BinarySlider(BinarySlider.SliderSize.Medium, "Left", "Right",, True, BinarySlider.ColorSchemes.White) With {.Margin = New Thickness(10, 0, 0, 0)}
        Dim sld4 As New BinarySlider(BinarySlider.SliderSize.Small, "1", "2",, False, BinarySlider.ColorSchemes.WhiteYellow) With {.Margin = New Thickness(10, 0, 0, 0)}
        Dim curr As New CurrencyBox(80, True, BaseInput.FontSz.Standard)
        WkChoose = New WeekChooser(1, 5, 1)
        MonChoose = New MonthChooser(WkChoose, 1, 12, Month(Now)) With {.Width = 530}
        YrChoose = New YearChooser(MonChoose, 2018, 2020, 2019) With {.Width = 180}
        MonChoose.RelatedYearObject = YrChoose
        GetRad = New RadialPosition
        wrpBinarySliders.Children.Add(sld1)
        wrpBinarySliders.Children.Add(sld2)
        wrpBinarySliders.Children.Add(sld3)
        wrpBinarySliders.Children.Add(sld4)
        grdCurrency.Children.Add(curr)
        curr.UserFocus()
        grdYear.Children.Add(YrChoose)
        grdMonth.Children.Add(MonChoose)
    End Sub

    Private Sub CreateRadialButton(sender As Button, e As RoutedEventArgs) Handles btnOne.Click, btnTwo.Click, btnThree.Click, btnFour.Click, btnFive.Click, btnSix.Click, btnSeven.Click, btnEight.Click
        '// Create button (image element) object and add event handlers
        Dim img As New Image With {.Source = New BitmapImage(New Uri("Resources/" & sender.Content & ".png", UriKind.Relative)), .Name = "btnRadial" & sender.Content,
        .Height = 30, .Width = 30, .Stretch = Stretch.UniformToFill}
        Dim pos = GetRad.GetRadialPosition(FormatNumber(sender.Content, 0), 8, 100)
        pos.x1 -= img.Width / 2 : pos.y1 -= img.Height / 2
        tbReturnVals.Text = "X: " & pos.x1 & "  Y:" & pos.y1
        img.Margin = New Thickness(pos.x1, pos.y1, 0, 0)
        cnvInternal.Children.Add(img)
    End Sub

End Class
