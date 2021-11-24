
Imports IBM.Data.DB2.iSeries
Imports ShareModels.Models.Benefit_Claims
Public Class InvalidityDB2
    Dim cn = DB2ConnectionS.as400
    Dim As400_lib = DB2ConnectionS.As400_lib
    Async Function InsertInvalidity(Invalidity As Document_Invalidity) As Task(Of Integer)

        Dim ClaimNo As Integer
        Try

            'EMPLOYERS REG
            Dim strCadena As String
            Dim intPos As Integer
            Dim EmprNo As String
            Dim EmprSub As String
            strCadena = Await SelectLastEmployer(Invalidity.NisNo)
            intPos = InStr(1, strCadena, "-") 'posicion de la "-"
            EmprNo = Mid(strCadena, 1, intPos - 1)
            EmprSub = Mid(strCadena, intPos + 1)


            ClaimNo = Await GenerarClaimNo()
            Await InsertInvalidityBENF(Invalidity, ClaimNo, EmprNo, EmprSub)
            Await InsertInvalidityCLMNCS(Invalidity, ClaimNo, EmprNo, EmprSub)

        Catch ex As iDB2Exception
            Throw ex
        End Try

        Return ClaimNo
    End Function

    Private Async Function InsertInvalidityBENF(Invalidity As Document_Invalidity, Clmn As String, EmprNo As String, Emprsub As String) As Task
        Try

            Using connection As New iDB2Connection(cn)

                If connection.State = ConnectionState.Closed Then
                    connection.Open()
                End If

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
                cmd1.Parameters("@EREG13").Value = Invalidity.NisNo
                cmd1.Parameters("@BENT13").Value = "6"
                cmd1.Parameters("@NATR13").Value = " "

                cmd1.Parameters("@CNCC13").Value = Invalidity.CreatedOn.Year \ 100
                cmd1.Parameters("@CNYY13").Value = Invalidity.CreatedOn.Year Mod 100
                cmd1.Parameters("@CNMM13").Value = Invalidity.CreatedOn.Month
                cmd1.Parameters("@CNDD13").Value = Invalidity.CreatedOn.Day
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
                cmd1.Parameters("@INTL13").Value = Invalidity.CompletedBy

                'DIAGNOSIS COD 
                cmd1.Parameters("@DIAG13").Value = " "

                cmd1.Parameters("@RREG13").Value = EmprNo
                cmd1.Parameters("@RRSF13").Value = Emprsub

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
    Async Function InsertInvalidityCLMNCS(Invalidity As Document_Invalidity, Clmn As String, EmprNo As String, EmprSub As String) As Task
        Try

            Using connection As New iDB2Connection(cn)

                If connection.State = ConnectionState.Closed Then
                    connection.Open()
                End If

                Dim cmdtext As String = " INSERT INTO  ""QS36F"".""" & As400_lib & ".CLMNCS""  (ACTVCS, CLMNCS, EREGCS, BENTCS, CNCCCS, CNYYCS, CNMMCS, CNDDCS, STATCS,
                                                  RFRCCS, RCOMCS, RTCSCS, LWRKCS, ACCDCS, DEADCS, UNEMPCS, CHIDCS, CRDCS, DEGDCS, PRMDCS, RREGCS, RRSFCS, RREGCS2, RRSFCS2,RREGCS3,
                                                  RRSFCS3, RREGCS4, RRSFCS4, RREGCS5, RRSFCS5, PROVFCS, RECPACS, GOVWCS, STAQCS, CMPQCS, SAVBCS, SAVTCS, EMPASCS, EMPRACS, EMPSACS, WBLINKCS)
                                        VALUES(@ACTVCS, @CLMNCS, @EREGCS, @BENTCS, @CNCCCS, @CNYYCS, @CNMMCS, @CNDDCS, @STATCS,
                                              @RFRCCS, @RCOMCS, @RTCSCS,  @LWRKCS, @ACCDCS, @DEADCS, @UNEMPCS, @CHIDCS, @CRDCS, @DEGDCS, @PRMDCS, @RREGCS, @RRSFCS, @RREGCS2, @RRSFCS2,
                                              @RREGCS3, @RRSFCS3, @RREGCS4, @RRSFCS4, @RREGCS5, @RRSFCS5, @PROVFCS, @RECPACS, @GOVWCS, @STAQCS, @CMPQCS, @SAVBCS,@SAVTCS, @EMPASCS, @EMPRACS, @EMPSACS, @WBLINKCS)"

                Dim cmd As New iDB2Command() With {
                            .CommandText = cmdtext,
                            .Connection = connection,
                            .CommandTimeout = 0
                        }

                cmd.DeriveParameters()
                cmd.Parameters("@ACTVCS").Value = "A"
                cmd.Parameters("@CLMNCS").Value = Clmn
                cmd.Parameters("@EREGCS").Value = Invalidity.NisNo
                cmd.Parameters("@BENTCS").Value = "6"
                cmd.Parameters("@CNCCCS").Value = Invalidity.CreatedOn.Year \ 100
                cmd.Parameters("@CNYYCS").Value = Invalidity.CreatedOn.Year Mod 100
                cmd.Parameters("@CNMMCS").Value = Invalidity.CreatedOn.Month
                cmd.Parameters("@CNDDCS").Value = Invalidity.CreatedOn.Day
                cmd.Parameters("@STATCS").Value = " "

                'REASON FOR REJECT
                cmd.Parameters("@RFRCCS").Value = " "

                'REJECT COMMENTS
                cmd.Parameters("@RCOMCS").Value = " "

                'REASON TO CLOSE
                cmd.Parameters("@RTCSCS").Value = " "

                'LAST DAY WORKED
                cmd.Parameters("@LWRKCS").Value = 0

                'DATE OF ACCIDENT
                cmd.Parameters("@ACCDCS").Value = 0

                'DATE OF DEATH
                cmd.Parameters("@DEADCS").Value = 0
                cmd.Parameters("@UNEMPCS").Value = 0

                'CHILD DATE OF BIRTH
                cmd.Parameters("@CHIDCS").Value = 0

                'DATE OF CLAIM RECEIVED
                cmd.Parameters("@CRDCS").Value = Now.Year * 10000 + Now.Month * 100 + Now.Day

                'DEGREE OF DISABLEMENT
                cmd.Parameters("@DEGDCS").Value = 0

                'PERMANENTLY OF INCAPABLE
                cmd.Parameters("@PRMDCS").Value = " "

                'EMPLOYERS REG
                cmd.Parameters("@RREGCS").Value = EmprNo
                cmd.Parameters("@RRSFCS").Value = EmprSub
                cmd.Parameters("@RREGCS2").Value = 0
                cmd.Parameters("@RRSFCS2").Value = 0
                cmd.Parameters("@RREGCS3").Value = 0
                cmd.Parameters("@RRSFCS3").Value = 0
                cmd.Parameters("@RREGCS4").Value = 0
                cmd.Parameters("@RRSFCS4").Value = 0
                cmd.Parameters("@RREGCS5").Value = 0
                cmd.Parameters("@RRSFCS5").Value = 0

                'PROVIDENT FUND CLAIM

                cmd.Parameters("@PROVFCS").Value = " "

                'PRECIPROCAL AGREEMENT

                cmd.Parameters("@RECPACS").Value = ""


                'GOVERNMENT CLAIM
                cmd.Parameters("@GOVWCS").Value = " "

                'STATEMENT QUERY
                cmd.Parameters("@STAQCS").Value = " "

                'COMPLIANCE QUERY
                cmd.Parameters("@CMPQCS").Value = " "

                cmd.Parameters("@SAVBCS").Value = Invalidity.CompletedBy
                cmd.Parameters("@SAVTCS").Value = CDate(Invalidity.CompletedTime).Year * 10000 + CDate(Invalidity.CompletedTime).Month * 100 + CDate(Invalidity.CompletedTime).Day

                'reassingempr
                cmd.Parameters("@EMPASCS").Value = "N"
                cmd.Parameters("@EMPRACS").Value = "0"
                cmd.Parameters("@EMPSACS").Value = "0"
                cmd.Parameters("@WBLINKCS").Value = Invalidity.WebPortalLink

                Await cmd.ExecuteNonQueryAsync()
                cmd.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try
    End Function

End Class
