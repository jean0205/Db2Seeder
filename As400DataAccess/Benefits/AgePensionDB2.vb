Imports IBM.Data.DB2.iSeries
Imports ShareModels.Models.Benefit_Claims
Public Class AgePensionDB2
    Dim cn = DB2ConnectionS.as400
    Dim As400_lib = DB2ConnectionS.As400_lib

    Async Function InsertAgePension(Agepension As Document_AgeBenefit) As Task(Of Integer)

        Dim ClaimNo As Integer
        Try

            ClaimNo = Await GenerarClaimNo()
            Await InsertBenf(Agepension, ClaimNo)

        Catch ex As iDB2Exception
            Throw ex
        End Try

        Return ClaimNo
    End Function

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
    Async Function InsertBenf(Agepension As Document_AgeBenefit, Clmn As String) As Task
        Try

            Using connection As New iDB2Connection(cn)

                connection.Open()

                Dim cmdtext As String = " INSERT INTO  ""QS36F"".""" & As400_lib & ".BENF""  " &
                                                                        " (ACTV13, CLMN13, EREG13, BENT13, NATR13, CNCC13, CNYY13, CNMM13, CNDD13, STAT13, " &
                                                                         "DIAG13, RFRC13, RCOM13, RTCS13, COTC13, INTL13, RREG13, RRSF13, FILL13, DIAN13, LWRK13, ACCD13)" &
                                                                        " VALUES(@ACTV13, @CLMN13, @EREG13, @BENT13, @NATR13, @CNCC13, @CNYY13, @CNMM13, @CNDD13, @STAT13, " &
                                                                        " @DIAG13, @RFRC13, @RCOM13, @RTCS13, @COTC13, @INTL13, @RREG13, @RRSF13, @FILL13, @DIAN13, @LWRK13, @ACCD13)"
                Dim cmd1 As New iDB2Command() With {
                            .CommandText = cmdtext,
                            .Connection = connection,
                            .CommandTimeout = 0
                        }

                cmd1.DeriveParameters()
                cmd1.Parameters("@ACTV13").Value = "A"
                cmd1.Parameters("@CLMN13").Value = Clmn
                cmd1.Parameters("@EREG13").Value = Agepension.nisNo
                cmd1.Parameters("@BENT13").Value = "4"
                cmd1.Parameters("@NATR13").Value = ""

                cmd1.Parameters("@CNCC13").Value = Agepension.createdOn.Year \ 100
                cmd1.Parameters("@CNYY13").Value = Agepension.createdOn.Year Mod 100
                cmd1.Parameters("@CNMM13").Value = Agepension.createdOn.Month
                cmd1.Parameters("@CNDD13").Value = Agepension.createdOn.Day
                cmd1.Parameters("@STAT13").Value = " "

                'REASON FOR REJECT
                cmd1.Parameters("@RFRC13").Value = " "

                'REJECT COMMENTS
                cmd1.Parameters("@RCOM13").Value = " "

                'REASON TO CLOSE
                cmd1.Parameters("@RTCS13").Value = " "

                'CAUSE OF TYPE
                cmd1.Parameters("@COTC13").Value = " "

                'USER INITIALS
                cmd1.Parameters("@INTL13").Value = "USERID"

                'DIAGNOSIS COD
                cmd1.Parameters("@DIAG13").Value = " "

                cmd1.Parameters("@RREG13").Value = 0
                cmd1.Parameters("@RRSF13").Value = 0

                cmd1.Parameters("@FILL13").Value = " "

                'LAST DAY WORKED
                cmd1.Parameters("@LWRK13").Value = 0

                cmd1.Parameters("@ACCD13").Value = 0

                'DIAGNOSIS COD
                cmd1.Parameters("@DIAN13").Value = " "

                Await cmd1.ExecuteNonQueryAsync()
                cmd1.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try
    End Function
End Class
