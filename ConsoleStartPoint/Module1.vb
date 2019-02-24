Module Module1

    Sub Main()
        Dim rk As ConsoleKey, acceptablevalue As Boolean
        Console.WriteLine("1) Run samples in Visual Basic")
        Console.WriteLine("2) Run sample in C#")
        Do Until acceptablevalue = True
            rk = Console.ReadKey().Key
            If rk = ConsoleKey.D1 Or rk = ConsoleKey.D2 Then acceptablevalue = True
        Loop
        Select Case rk
            Case ConsoleKey.D1
                Dim scs As SampleCSharp.MainWindow = New SampleCSharp.MainWindow





            Case ConsoleKey.D2

        End Select
    End Sub

End Module
