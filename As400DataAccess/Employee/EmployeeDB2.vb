Imports IBM.Data.DB2.iSeries
Imports ShareModels.Models

Public Class EmployeeDB2
    Dim cn = DB2ConnectionS.as400
    Dim As400_lib = DB2ConnectionS.As400_lib

    Async Function CountEmployees() As Task(Of Integer)

        Dim total As Integer = 0
        Try
            Using connection As New iDB2Connection(cn)
                Dim cmd As New iDB2Command()
                connection.Open()
                Dim rs As iDB2DataReader
                cmd.CommandText = "SELECT  count(*)
	                                  FROM ""QS36F"".""" & As400_lib & ".EMPE"" E                               
                                WHERE  DATD03 = 0 and DATB03 <> 0 and ACTV03 = 'A'"
                cmd.Connection = connection
                cmd.CommandTimeout = 0
                cmd.DeriveParameters()
                rs = Await cmd.ExecuteReaderAsync
                If rs.Read Then
                    total = rs(0)
                End If
                cmd.Dispose()
                rs.Close()
            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try





        Return total
    End Function

End Class
