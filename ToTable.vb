''''''''''''''''''''''''' 
'                       '
'<Danny Hilario Suarez/>'
'                       '
'''''''''''''''''''''''''


Imports System.Reflection
Imports System.Runtime.CompilerServices

Public Module ToTable

    <Extension()>
    Public Function ToTable(Of T)(obj As IQueryable(Of T)) As DataTable

        Dim name As String = String.Empty
        Dim table As New DataTable()
        Dim myType As Type = GetType(T)
        Dim pro As PropertyInfo() = myType.GetProperties()

        If obj.Any Then
            For Each col In pro
                Dim c = If(Nullable.GetUnderlyingType(col.PropertyType), col.PropertyType)
                table.Columns.Add(col.Name, c)
            Next

            For Each item In obj
                Dim row As DataRow = table.NewRow()
                For Each prop In pro
                    row(prop.Name) = prop.GetValue(item)
                Next
                table.Rows.Add(row)
            Next
        End If
        Return table
    End Function

End Module
