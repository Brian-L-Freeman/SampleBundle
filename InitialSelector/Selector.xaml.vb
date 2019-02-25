Class Selector
    Private Sub VBOption(sender As Object, e As RoutedEventArgs) Handles imgVB.MouseLeftButtonDown
        Dim vbw As New SampleBundle.VBVersion
        vbw.ShowDialog()


    End Sub

    Private Sub CSOption(sender As Object, e As RoutedEventArgs) Handles imgCSharp.MouseLeftButtonDown
        Dim csw As New SampleCSharp.CSharpWindow
        csw.ShowDialog()
    End Sub
End Class
