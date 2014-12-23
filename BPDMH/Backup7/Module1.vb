Module Module1

    Sub Main()
        Dim fileName = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Names.xls")
        Dim book = New LinqToExcel.ExcelQueryFactory(fileName)
        Dim users = From x In book.Worksheet(Of User)() _
                    Where x.FirstName = "Paul" _
                    Select x

        For Each u In users
            Console.WriteLine(u.FirstName + " " + u.LastName)
        Next
        Console.ReadKey()
    End Sub

End Module
