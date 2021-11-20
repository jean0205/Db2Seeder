
Imports IBM.Data.DB2.iSeries
Imports ShareModels.Models.Benefit_Claims
Module ModBenfHelps
    Dim cn = DB2ConnectionS.as400
    Dim As400_lib = DB2ConnectionS.As400_lib

    Async Function GenerarClaimNo() As Task(Of Integer)
        Dim ClaimNo As Integer = 0
        Try
            Using connection As New iDB2Connection(cn)

                connection.Open()
                Dim rs As iDB2DataReader
                Dim cmdtextnewnum As String = "select * from ""QS36F"".""" & As400_lib & ".CONS"" where CKEY01 LIKE @CKEY"
                Dim cmd As New iDB2Command() With {
                            .CommandText = cmdtextnewnum,
                            .Connection = connection,
                            .CommandTimeout = 0
                        }
                cmd.DeriveParameters()
                cmd.Parameters("@CKEY").Value = "Z  005"

                rs = Await cmd.ExecuteReaderAsync
                If rs.Read Then
                    Dim CLMNNUMBER As String
                    CLMNNUMBER = rs("DATA01").ToString.Substring(12, 9)
                    ClaimNo = Convert.ToInt32(CLMNNUMBER)

                    'UPDATE CONS FILE
                    Dim cmdtxt As String = "UPDATE ""QS36F"".""" & As400_lib & ".CONS"" SET DATA01 = @DATA WHERE CKEY01 LIKE @CKEY "
                    Dim cmdupd As New iDB2Command With {
                        .CommandText = cmdtxt,
                        .Connection = connection,
                        .CommandTimeout = 0
                    }
                    cmdupd.DeriveParameters()
                    Dim strdata As String
                    Dim newclmn As Integer = Convert.ToInt32(CLMNNUMBER) + 1
                    Dim newclmnstr As String = Convert.ToString(newclmn)

                    While newclmnstr.Length < 9
                        newclmnstr = "0" + newclmnstr
                    End While

                    strdata = Mid(rs("DATA01"), 1, 12) + newclmnstr + Mid(rs("DATA01"), 22)
                    cmdupd.Parameters("@DATA").Value = strdata
                    cmdupd.Parameters("@CKEY").Value = "Z  005"
                    Await cmdupd.ExecuteNonQueryAsync()
                    cmdupd.Dispose()

                End If
                cmd.Dispose()
                rs.Close()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

        Return ClaimNo
    End Function


    Async Function SelectLastEmployer(nistxt1 As String) As Task(Of String)
        Dim period As String = ""
        Try

            Using connection As New iDB2Connection(cn)

                connection.Open()

                    Dim rs As iDB2DataReader
                Dim cmdtext As String = "Select concat(concat(RREG06,'-'),RRSF06) as cd,  MAX(CCEN06 * 10000 + CONY06 * 100 + CPER06)  
                                         FROM ""QS36F"".""" & As400_lib & ".CNTE"" INNER JOIN ""QS36F"".""" & As400_lib & ".EMPR"" On RREG06 = RREG02 And RRSF06 = RRSF02 
                                         WHERE ACTV06 = 'A'  AND Ereg06 = " & nistxt1 & " and EGIE06 <> 0 
                                         GROUP BY  RREG06, RRSF06, BTNO02
                                         ORDER BY MAX (CCEN06 * 10000 + CONY06 * 100 + CPER06) DESC"
                Dim cmd As New iDB2Command() With {
                            .CommandText = cmdtext,
                            .Connection = connection,
                            .CommandTimeout = 0
                        }
                rs = Await cmd.ExecuteReaderAsync
                If rs.Read Then
                    period = rs(0)
                End If
                rs.Close()
                cmd.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try
        Return period
    End Function
End Module
