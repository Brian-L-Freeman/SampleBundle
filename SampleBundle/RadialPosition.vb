
Public Class RadialPosition

    Public Function GetRadialPosition(ItemNumber As Byte, ItemCount As Byte, canvasheight As Integer) As (x1 As Integer, y1 As Integer)
        '// Determine position of the button on the dial radius
        Dim ang As Double = (ItemNumber * ((2 * Math.PI) / ItemCount))
        Dim rad As Integer = canvasheight / 2
        Dim x As Integer = ((Math.Cos(ang) * rad) + rad)
        Dim y As Integer = (rad - (Math.Sin(ang) * rad))
        Return (x, y)
    End Function

End Class
