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



    Async Function InsertEmployees() As Task(Of Integer)

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

    Private Function GenerarNIS() As Integer
        Dim NISRegister As Integer = 0
        Try
            Using connection As New iDB2Connection(cn)
                Dim cmd As New iDB2Command()

                connection.Open()
                Dim rs As iDB2DataReader
                cmd.CommandText = "SELECT * FROM ""QS36F"".""" & As400_lib & ".CONS""                                
                                WHERE CKEY01 LIKE 'Z  005' "
                cmd.Connection = connection
                cmd.CommandTimeout = 0
                cmd.DeriveParameters()
                rs = cmd.ExecuteReader
                If rs.Read Then
                    Dim NISNUMBER As String
                    NISNUMBER = Checkdigitcalc(Convert.ToInt32(rs("DATA01").ToString.Substring(30, 9))).ToString
                    NISRegister = CInt(NISNUMBER)

                    'UPDATE CONS FILE
                    Dim cmdupd As New iDB2Command With {
                        .CommandText = "UPDATE ""QS36F"".""" & As400_lib & ".CONS""                                
                                SET DATA01 = @DATA WHERE CKEY01 LIKE @CKEY ",
                        .Connection = connection,
                        .CommandTimeout = 0
                    }
                    cmdupd.DeriveParameters()

                    Dim strdata As String
                    Dim newnis As Integer = Convert.ToInt32(NISNUMBER) + 10
                    Dim newnisstr As String = Convert.ToString(newnis)
                    While newnisstr.Length < 9
                        newnisstr = "0" + newnisstr
                    End While
                    strdata = Mid(rs("DATA01"), 1, 30) + newnisstr

                    cmdupd.Parameters("@DATA").Value = strdata
                    cmdupd.Parameters("@CKEY").Value = "Z  005"
                    cmdupd.ExecuteNonQuery()
                    cmdupd.Dispose()

                End If
                cmd.Dispose()
                rs.Close()
            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

        Return NISRegister
    End Function

End Class
