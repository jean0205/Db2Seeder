
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
    Async Function InsertBadt(BenfTyp As String, Clmn As String) As Task
        Try

            Using connection As New iDB2Connection(cn)

                connection.Open()

                Dim cmdtext As String = " INSERT INTO ""QS36F"".""" & As400_lib & ".BADT""  " &
                                                                         "(CLMN206, LINE206, BENT206, OUSR206, CUSR206, CDTE206, CTME206, ODTE206, OTME206) " &
                                                                  "Values(@CLMN206, @LINE206, @BENT206, @OUSR206, @CUSR206, @CDTE206, @CTME206, CURRENT_DATE, CURRENT_TIME)"
                Dim cmdBadt As New iDB2Command() With {
                            .CommandText = cmdtext,
                            .Connection = connection,
                            .CommandTimeout = 0
                        }

                ' Insert Bef payment BADT

                cmdBadt.Parameters("@CLMN206").Value = Clmn
                cmdBadt.Parameters("@LINE206").Value = 0
                cmdBadt.Parameters("@BENT206").Value = BenfTyp
                cmdBadt.Parameters("@OUSR206").Value = "UserID"
                cmdBadt.Parameters("@CUSR206").Value = " "
                cmdBadt.Parameters("@CDTE206").Value = Date.MinValue
                cmdBadt.Parameters("@CTME206").Value = Date.MinValue

                Await cmdBadt.ExecuteNonQueryAsync()
                cmdBadt.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try
    End Function
End Module
