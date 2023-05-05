Imports IBM.Data.DB2.iSeries
Imports ShareModels.Models.Benefit_Claims
Public Class UnemploymentBenefitDB2
    Dim cn = DB2ConnectionS.as400
    Public As400_lib = DB2ConnectionS.As400_lib

    Async Function InsertUnemployment(claim As Document_UEB_Empe, ClaimNumber As Integer?) As Task(Of Integer)

        Dim ClaimNo As Integer
        Try
            If ClaimNumber Is Nothing Then
                ClaimNo = Await GenerarClaimNo() + 10000
            Else
                ClaimNo = ClaimNumber
            End If
            Await InsertClaimBENF(claim, ClaimNo)
            Await InsertClaimCLMNCS(claim, ClaimNo)
            Await InsertClaimCLMNBF(claim, ClaimNo)
            Await InsertBankInformationBADT(ClaimNo)

        Catch ex As iDB2Exception
            Throw ex
        End Try

        Return ClaimNo
    End Function
    Private Async Function InsertClaimBENF(claim As Document_UEB_Empe, Clmn As String) As Task
        Try

            Using connection As New iDB2Connection(cn)

                If connection.State = ConnectionState.Closed Then
                    connection.Open()
                End If

                Dim cmdtext As String = " INSERT INTO  ""QS36F"".""" & As400_lib & ".BENF""  " &
                                                                        " (ACTV13, CLMN13, EREG13, BENT13, NATR13, CNCC13, CNYY13, CNMM13, CNDD13, STAT13, DIAG13, RFRC13, RCOM13, RTCS13, COTC13, INTL13, RREG13, RRSF13, FILL13, DIAN13, LWRK13, ACCD13)
                                 VALUES(@ACTV13, @CLMN13, @EREG13, @BENT13, @NATR13, @CNCC13, @CNYY13, @CNMM13, @CNDD13, @STAT13,@DIAG13, @RFRC13, @RCOM13, @RTCS13, @COTC13, @INTL13, @RREG13, @RRSF13, @FILL13, @DIAN13, @LWRK13, @ACCD13)"
                Dim cmd1 As New iDB2Command() With {
                            .CommandText = cmdtext,
                            .Connection = connection,
                            .CommandTimeout = 0
                        }

                cmd1.DeriveParameters()
                cmd1.Parameters("@ACTV13").Value = "A"
                cmd1.Parameters("@CLMN13").Value = Clmn
                cmd1.Parameters("@EREG13").Value = claim.nisNo
                cmd1.Parameters("@BENT13").Value = "U"
                cmd1.Parameters("@NATR13").Value = "S"

                cmd1.Parameters("@CNCC13").Value = CDate(claim.createdOn).Year \ 100
                cmd1.Parameters("@CNYY13").Value = CDate(claim.createdOn).Year Mod 100
                cmd1.Parameters("@CNMM13").Value = CDate(claim.createdOn).Month
                cmd1.Parameters("@CNDD13").Value = CDate(claim.createdOn).Day
                cmd1.Parameters("@STAT13").Value = ""

                'REASON FOR REJECT
                cmd1.Parameters("@RFRC13").Value = ""

                'REJECT COMMENTS
                cmd1.Parameters("@RCOM13").Value = ""

                'REASON TO CLOSE
                cmd1.Parameters("@RTCS13").Value = ""

                'CAUSE OF TYPE
                cmd1.Parameters("@COTC13").Value = ""

                'USER INITIALS
                cmd1.Parameters("@INTL13").Value = "Webportal"    'claim.CompletedBy si se decide revisar el CS cambiar aqui, sino se postearia el nombre del claimant 

                'DIAGNOSIS COD
                cmd1.Parameters("@DIAG13").Value = 0
                cmd1.Parameters("@DIAN13").Value = 0

                'El numero del employer no se recoge en el claim
                cmd1.Parameters("@RREG13").Value = 0
                cmd1.Parameters("@RRSF13").Value = 0

                cmd1.Parameters("@FILL13").Value = ""

                'LAST DAY WORKED
                'Mientras no se recoge el dato en el claim se deja en 0

                If claim.lastDateOfWorkLastEmployer.HasValue Then
                    cmd1.Parameters("@LWRK13").Value = CDate(claim.lastDateOfWorkLastEmployer).Year * 10000 + CDate(claim.lastDateOfWorkLastEmployer).Month * 100 + CDate(claim.lastDateOfWorkLastEmployer).Day
                Else
                    cmd1.Parameters("@LWRK13").Value = 0
                End If



                cmd1.Parameters("@ACCD13").Value = 0

                Await cmd1.ExecuteNonQueryAsync()
                cmd1.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try
    End Function
    Private Async Function InsertClaimCLMNCS(claim As Document_UEB_Empe, Clmn As String) As Task
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
                cmd.Parameters("@EREGCS").Value = claim.nisNo
                cmd.Parameters("@BENTCS").Value = "U"
                cmd.Parameters("@CNCCCS").Value = CDate(claim.createdOn).Year \ 100
                cmd.Parameters("@CNYYCS").Value = CDate(claim.createdOn).Year Mod 100
                cmd.Parameters("@CNMMCS").Value = CDate(claim.createdOn).Month
                cmd.Parameters("@CNDDCS").Value = CDate(claim.createdOn).Day
                cmd.Parameters("@STATCS").Value = ""

                'REASON FOR REJECT
                cmd.Parameters("@RFRCCS").Value = ""

                'REJECT COMMENTS
                cmd.Parameters("@RCOMCS").Value = ""

                'REASON TO CLOSE
                cmd.Parameters("@RTCSCS").Value = ""


                'LAST DAY WORKED
                'Mientras no se recoge el dato en el claim se deja en 0
                If claim.lastDateOfWorkLastEmployer.HasValue Then
                    Dim lwd = CDate(claim.lastDateOfWorkLastEmployer).Year * 10000 + CDate(claim.lastDateOfWorkLastEmployer).Month * 100 + CDate(claim.lastDateOfWorkLastEmployer).Day
                    cmd.Parameters("@LWRKCS").Value = lwd
                    cmd.Parameters("@UNEMPCS").Value = lwd
                Else
                    cmd.Parameters("@LWRKCS").Value = 0
                    cmd.Parameters("@UNEMPCS").Value = 0
                End If


                'DATE OF ACCIDENT
                cmd.Parameters("@ACCDCS").Value = 0

                'DATE OF DEATH
                cmd.Parameters("@DEADCS").Value = 0


                'CHILD DATE OF BIRTH
                cmd.Parameters("@CHIDCS").Value = 0

                'DATE OF CLAIM RECEIVED
                cmd.Parameters("@CRDCS").Value = Now.Year * 10000 + Now.Month * 100 + Now.Day

                'DEGREE OF DISABLEMENT
                cmd.Parameters("@DEGDCS").Value = 0

                'PERMANENTLY OF INCAPABLE
                cmd.Parameters("@PRMDCS").Value = ""

                'EMPLOYERS REG
                cmd.Parameters("@RREGCS").Value = 0
                cmd.Parameters("@RRSFCS").Value = 0
                cmd.Parameters("@RREGCS2").Value = 0
                cmd.Parameters("@RRSFCS2").Value = 0
                cmd.Parameters("@RREGCS3").Value = 0
                cmd.Parameters("@RRSFCS3").Value = 0
                cmd.Parameters("@RREGCS4").Value = 0
                cmd.Parameters("@RRSFCS4").Value = 0
                cmd.Parameters("@RREGCS5").Value = 0
                cmd.Parameters("@RRSFCS5").Value = 0

                'PROVIDENT FUND CLAIM

                cmd.Parameters("@PROVFCS").Value = ""

                'PRECIPROCAL AGREEMENT

                cmd.Parameters("@RECPACS").Value = ""


                'GOVERNMENT CLAIM
                cmd.Parameters("@GOVWCS").Value = ""

                'STATEMENT QUERY
                cmd.Parameters("@STAQCS").Value = ""

                'COMPLIANCE QUERY
                cmd.Parameters("@CMPQCS").Value = ""

                cmd.Parameters("@SAVBCS").Value = claim.CompletedBy
                cmd.Parameters("@SAVTCS").Value = CDate(claim.CompletedTime).Year * 10000 + CDate(claim.CompletedTime).Month * 100 + CDate(claim.CompletedTime).Day

                'No assigment to the employer

                cmd.Parameters("@EMPASCS").Value = "N"
                cmd.Parameters("@EMPRACS").Value = 0
                cmd.Parameters("@EMPSACS").Value = 0

                If claim.bank = Nothing Or claim.accountNo = Nothing Or claim.accountType Is Nothing Then
                Else
                    Await InsertBankInformationEmpe(claim, Clmn)
                End If

                cmd.Parameters("@WBLINKCS").Value = claim.WebPortalLink
                Await cmd.ExecuteNonQueryAsync()
                cmd.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try
    End Function
    Private Async Function InsertClaimCLMNBF(claim As Document_UEB_Empe, Clmn As String) As Task
        Try

            Using connection As New iDB2Connection(cn)

                If connection.State = ConnectionState.Closed Then
                    connection.Open()
                End If

                Dim cmdtext As String = " INSERT INTO  ""QS36F"".""" & As400_lib & ".CLMNBF""  
                                                      (ACTVBF, CLMNBF, EREGBF, BENTBF, BTRMBF,CNCCBF,CNYYBF,CNMMBF, CNDDBF, STATBF, DIANBF,  LWRKBF, ACCDBF, DEADBF, CHIDBF, CRDBF, CPFDBF, CPTDBF, DEGDBF,PRMDBF,RREGBF, RRSFBF, RREGBF2, RRSFBF2, RREGBF3, RRSFBF3, RREGBF4, RRSFBF4, RREGBF5, RRSFBF5, PROVFBF, RECPABF, GOVWBF, STAQBF, CMPQBF, MPAYBF, BANKBF, ACNOBF, WBLINKCS,UNEMPBF)
                                            VALUES(@ACTVBF, @CLMNBF, @EREGBF, @BENTBF, @BTRMBF, @CNCCBF, @CNYYBF, @CNMMBF, @CNDDBF, @STATBF,@DIANBF, @LWRKBF, @ACCDBF, @DEADBF, @CHIDBF, @CRDBF, @CPFDBF, @CPTDBF, @DEGDBF, @PRMDBF, @RREGBF, @RRSFBF, @RREGBF2, @RRSFBF2, @RREGBF3, @RRSFBF3, @RREGBF4, @RRSFBF4,@RREGBF5, @RRSFBF5, @PROVFBF, @RECPABF, @GOVWBF, @STAQBF, @CMPQBF, @MPAYBF, @BANKBF, @ACNOBF, @WBLINKCS,@UNEMPBF)"

                Dim cmd As New iDB2Command() With {
                            .CommandText = cmdtext,
                            .Connection = connection,
                            .CommandTimeout = 0
                        }

                cmd.DeriveParameters()
                cmd.Parameters("@ACTVBF").Value = "A"
                cmd.Parameters("@CLMNBF").Value = Clmn
                cmd.Parameters("@EREGBF").Value = claim.nisNo
                cmd.Parameters("@BENTBF").Value = "U"

                cmd.Parameters("@CNCCBF").Value = CDate(claim.createdOn).Year \ 100
                cmd.Parameters("@CNYYBF").Value = CDate(claim.createdOn).Year Mod 100
                cmd.Parameters("@CNMMBF").Value = CDate(claim.createdOn).Month
                cmd.Parameters("@CNDDBF").Value = CDate(claim.createdOn).Day

                'BENEFIT CLASSIFICATION L,S
                cmd.Parameters("@BTRMBF").Value = " "
                cmd.Parameters("@STATBF").Value = " "

                'LAST DAY WORKED
                'Mientras no se recoge el dato en el claim se deja en 0
                If claim.lastDateOfWorkLastEmployer.HasValue Then
                    Dim lwd = CDate(claim.lastDateOfWorkLastEmployer).Year * 10000 + CDate(claim.lastDateOfWorkLastEmployer).Month * 100 + CDate(claim.lastDateOfWorkLastEmployer).Day

                    cmd.Parameters("@LWRKBF").Value = lwd
                    cmd.Parameters("@UNEMPBF").Value = lwd
                Else
                    cmd.Parameters("@LWRKBF").Value = 0
                    cmd.Parameters("@UNEMPBF").Value = 0
                End If

                'DATE OF ACCIDENT
                cmd.Parameters("@ACCDBF").Value = 0
                'DATE OF DEATH
                cmd.Parameters("@DEADBF").Value = 0
                'CHILD DATE OF BIRTH
                cmd.Parameters("@CHIDBF").Value = 0
                'DATE OF CLAIM RECEIVED
                cmd.Parameters("@CRDBF").Value = Now.Year * 10000 + Now.Month * 100 + Now.Day
                'DEGREE OF DISABLEMENT
                cmd.Parameters("@DEGDBF").Value = 0

                'PERMANENTLY OF INCAPABLE
                cmd.Parameters("@PRMDBF").Value = ""

                'EMPLOYERS REG
                cmd.Parameters("@RREGBF").Value = 0
                cmd.Parameters("@RRSFBF").Value = 0
                cmd.Parameters("@RREGBF2").Value = 0
                cmd.Parameters("@RRSFBF2").Value = 0
                cmd.Parameters("@RREGBF3").Value = 0
                cmd.Parameters("@RRSFBF3").Value = 0
                cmd.Parameters("@RREGBF4").Value = 0
                cmd.Parameters("@RRSFBF4").Value = 0
                cmd.Parameters("@RREGBF5").Value = 0
                cmd.Parameters("@RRSFBF5").Value = 0

                'PROVIDENT FUND CLAIM
                cmd.Parameters("@PROVFBF").Value = ""

                'PRECIPROCAL AGREEMENT
                cmd.Parameters("@RECPABF").Value = ""

                'GOVERNMENT CLAIM
                cmd.Parameters("@GOVWBF").Value = ""

                'STATEMENT QUERY
                cmd.Parameters("@STAQBF").Value = ""

                'COMPLIANCE QUERY
                cmd.Parameters("@CMPQBF").Value = ""

                cmd.Parameters("@DIANBF").Value = ""

                'TODO si el periodo se puede entrar desde el portal web este es el parametro a cambiar
                'DATE OF CLAIM PERIODO FROM
                cmd.Parameters("@CPFDBF").Value = 0

                'DATE OF CLAIM PERIODO TO
                cmd.Parameters("@CPTDBF").Value = 0

                'MPAYBF, BANKBF, ACNOBF
                cmd.Parameters("@MPAYBF").Value = "B"
                cmd.Parameters("@BANKBF").Value = CInt(Val(New Text.StringBuilder((From ch In claim.bank.ToCharArray Where IsNumeric(ch)).ToArray).ToString))
                cmd.Parameters("@ACNOBF").Value = claim.accountNo

                cmd.Parameters("@WBLINKCS").Value = claim.WebPortalLink




                Await cmd.ExecuteNonQueryAsync()
                cmd.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try
    End Function
    Async Function InsertBankInformationEmpe(claim As Document_UEB_Empe, Clmn As String) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                'insert in ni.xport for datacard
                Dim cmd As New iDB2Command() With {
                .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".ACCTYPE"" 
                                                 (ACTV, EMPE, CLMN, BANK, ACCN, ACCTYP)
                                          VALUES (@ACTV, @EMPE, @CLMN, @BANK, @ACCN, @ACCTYP)",
                .Connection = connection,
                .CommandTimeout = 0
                }

                cmd.DeriveParameters()
                cmd.Parameters("@ACTV").Value = "A"
                cmd.Parameters("@EMPE").Value = claim.nisNo
                cmd.Parameters("@CLMN").Value = Clmn
                cmd.Parameters("@BANK").Value = claim.bank
                cmd.Parameters("@ACCN").Value = claim.accountNo
                If claim.accountType = 1 Then
                    cmd.Parameters("@ACCTYP").Value = "SAV"
                Else
                    cmd.Parameters("@ACCTYP").Value = "DDA"
                End If
                Await cmd.ExecuteNonQueryAsync()
                cmd.Dispose()
            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function
    Async Function InsertBankInformationBADT(Clmn As String) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                'insert in ni.xport for datacard
                Dim cmd As New iDB2Command() With {
                .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".BADT"" (CLMN206, LINE206, BENT206, OUSR206,                                         CUSR206, CDTE206, CTME206, ODTE206, OTME206) 
                                                 Values(@CLMN206, @LINE206, @BENT206, @OUSR206, @CUSR206, @CDTE206, @CTME206, CURRENT_DATE, CURRENT_TIME)",
                .Connection = connection,
                .CommandTimeout = 0
                }

                cmd.DeriveParameters()
                cmd.Parameters("@CLMN206").Value = Clmn
                cmd.Parameters("@LINE206").Value = 0
                cmd.Parameters("@BENT206").Value = "U"
                cmd.Parameters("@OUSR206").Value = "WEBPORTAL"
                cmd.Parameters("@CUSR206").Value = " "
                cmd.Parameters("@CDTE206").Value = Date.MinValue
                cmd.Parameters("@CTME206").Value = DateTime.MinValue
                Await cmd.ExecuteNonQueryAsync()
                cmd.Dispose()
            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function



End Class
