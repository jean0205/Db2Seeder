
Imports IBM.Data.DB2.iSeries
Imports ShareModels.Models.Sickness_Claim
Public Class SicknessDB2
    Dim cn = DB2ConnectionS.as400
    Public As400_lib = DB2ConnectionS.As400_lib

    Async Function InsertSickness(Sickness As Document_Sickness, ClaimNumber As Integer?) As Task(Of Integer)

        Dim ClaimNo As Integer
        Try

            'EMPLOYERS REG
            Dim strCadena As String
            Dim intPos As Integer
            Dim EmprNo As String
            Dim EmprSub As String
            strCadena = Sickness.employerEntity.First().employerNis
            intPos = InStr(1, strCadena, "-") 'posicion de la "-"
            EmprNo = Mid(strCadena, 1, intPos - 1)
            EmprSub = Mid(strCadena, intPos + 1)


            If ClaimNumber Is Nothing Then
                ClaimNo = Await GenerarClaimNo()
            Else
                ClaimNo = ClaimNumber
            End If

            Await InsertSickBenf(Sickness, ClaimNo, EmprNo, EmprSub)
            Await InsertSickCLMNCS(Sickness, ClaimNo, EmprNo, EmprSub)

        Catch ex As iDB2Exception
            Throw ex
        End Try

        Return ClaimNo
    End Function
    Private Async Function InsertSickBenf(Sickness As Document_Sickness, Clmn As String, EmprNo As String, Emprsub As String) As Task
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
                cmd1.Parameters("@EREG13").Value = Sickness.nisNo
                cmd1.Parameters("@BENT13").Value = "2"
                cmd1.Parameters("@NATR13").Value = "S"

                cmd1.Parameters("@CNCC13").Value = Sickness.createdOn.Year \ 100
                cmd1.Parameters("@CNYY13").Value = Sickness.createdOn.Year Mod 100
                cmd1.Parameters("@CNMM13").Value = Sickness.createdOn.Month
                cmd1.Parameters("@CNDD13").Value = Sickness.createdOn.Day
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
                cmd1.Parameters("@INTL13").Value = Sickness.CompletedBy

                'DIAGNOSIS COD
                cmd1.Parameters("@DIAG13").Value = 0

                cmd1.Parameters("@RREG13").Value = EmprNo
                cmd1.Parameters("@RRSF13").Value = Emprsub

                cmd1.Parameters("@FILL13").Value = " "

                'LAST DAY WORKED
                cmd1.Parameters("@LWRK13").Value = CDate(Sickness.lastWorkedDate).Year * 10000 + CDate(Sickness.lastWorkedDate).Month * 100 + CDate(Sickness.lastWorkedDate).Day

                cmd1.Parameters("@ACCD13").Value = 0

                'DIAGNOSIS COD
                cmd1.Parameters("@DIAN13").Value = If(Sickness.icdCode = Nothing, 0, Sickness.icdCode)

                Await cmd1.ExecuteNonQueryAsync()
                cmd1.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try
    End Function
    Private Async Function InsertSickCLMNCS(Sickness As Document_Sickness, Clmn As String, EmprNo As String, EmprSub As String) As Task
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
                cmd.Parameters("@EREGCS").Value = Sickness.nisNo
                cmd.Parameters("@BENTCS").Value = "2"
                cmd.Parameters("@CNCCCS").Value = Sickness.createdOn.Year \ 100
                cmd.Parameters("@CNYYCS").Value = Sickness.createdOn.Year Mod 100
                cmd.Parameters("@CNMMCS").Value = Sickness.createdOn.Month
                cmd.Parameters("@CNDDCS").Value = Sickness.createdOn.Day
                cmd.Parameters("@STATCS").Value = " "

                'REASON FOR REJECT
                cmd.Parameters("@RFRCCS").Value = " "

                'REJECT COMMENTS
                cmd.Parameters("@RCOMCS").Value = " "

                'REASON TO CLOSE
                cmd.Parameters("@RTCSCS").Value = " "

                'LAST DAY WORKED
                cmd.Parameters("@LWRKCS").Value = CDate(Sickness.lastWorkedDate).Year * 10000 + CDate(Sickness.lastWorkedDate).Month * 100 + CDate(Sickness.lastWorkedDate).Day

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

                cmd.Parameters("@SAVBCS").Value = Sickness.CompletedBy
                cmd.Parameters("@SAVTCS").Value = CDate(Sickness.CompletedTime).Year * 10000 + CDate(Sickness.CompletedTime).Month * 100 + CDate(Sickness.CompletedTime).Day

                'reassingempr
                If Sickness.consent = 1 Then
                    cmd.Parameters("@EMPASCS").Value = "Y"
                    cmd.Parameters("@EMPRACS").Value = EmprNo
                    cmd.Parameters("@EMPSACS").Value = EmprSub
                    If Sickness.employerBank = Nothing Or Sickness.employerAccountNo = Nothing Or Sickness.employerAccountType Is Nothing Then
                    Else
                        Await InsertBankInformationEmpr(Sickness, EmprNo, EmprSub)
                    End If

                Else
                    cmd.Parameters("@EMPASCS").Value = "N"
                    cmd.Parameters("@EMPRACS").Value = "0"
                    cmd.Parameters("@EMPSACS").Value = "0"

                    If Sickness.bank = Nothing Or Sickness.accountNo = Nothing Or Sickness.accountType Is Nothing Then
                    Else
                        Await InsertBankInformationEmpe(Sickness, Clmn)
                    End If

                End If

                cmd.Parameters("@WBLINKCS").Value = Sickness.WebPortalLink

                Await cmd.ExecuteNonQueryAsync()
                cmd.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try
    End Function


    'Private Async Function InsertSickCLMNBF(Sickness As Document_Sickness, Clmn As String, EmprNo As String, EmprSub As String) As Task
    '    Try

    '        Using connection As New iDB2Connection(cn)

    '            If connection.State = ConnectionState.Closed Then
    '                connection.Open()
    '            End If

    '            Dim cmdtext As String = " INSERT INTO  ""QS36F"".""" & As400_lib & ".CLMNBF""  (ACTVBF, CLMNBF, EREGBF, BENTBF, BTRMBF,CNCCBF,CNYYBF,CNMMBF, CNDDBF, STATBF, DIANBF, LWRKBF, ACCDBF, DEADBF, CHIDBF, CRDBF, CPFDBF, CPTDBF, 
    '                                                                                           DEGDBF, PRMDBF, RREGBF, RRSFBF, RREGBF2, RRSFBF2, RREGBF3, RRSFBF3, RREGBF4, RRSFBF4, RREGBF5, RRSFBF5,
    '                                                                                           PROVFBF, RECPABF, GOVWBF, STAQBF, CMPQBF, MPAYBF, BANKBF, ACNOBF, WBLINKCS)
    '                                                                                    VALUES(@ACTVBF, @CLMNBF, @EREGBF, @BENTBF, @BTRMBF, @CNCCBF, @CNYYBF, @CNMMBF, @CNDDBF, @STATBF, @DIANBF, @LWRKBF, @ACCDBF, @DEADBF, @CHIDBF, @CRDBF, @CPFDBF, @CPTDBF,
    '                                                                                           @DEGDBF, @PRMDBF, @RREGBF, @RRSFBF, @RREGBF2, @RRSFBF2, @RREGBF3, @RRSFBF3, @RREGBF4, @RRSFBF4, @RREGBF5, @RRSFBF5,
    '                                                                                           @PROVFBF, @RECPABF, @GOVWBF, @STAQBF, @CMPQBF, @MPAYBF, @BANKBF, @ACNOBF, @WBLINKCS)"

    '            Dim cmd As New iDB2Command() With {
    '                        .CommandText = cmdtext,
    '                        .Connection = connection,
    '                        .CommandTimeout = 0
    '                    }


    '            cmd.DeriveParameters()
    '            cmd.Parameters("@ACTVBF").Value = "A"
    '            cmd.Parameters("@CLMNBF").Value = Clmn
    '            cmd.Parameters("@EREGBF").Value = Sickness.nisNo
    '            cmd.Parameters("@BENTBF").Value = "2"
    '                    'BENEFIT CLASSIFICATION L,S
    '            cmd.Parameters("@BTRMBF").Value = "S"

    '            cmd.Parameters("@CNCCBF").Value = Sickness.createdOn.Year \ 100
    '            cmd.Parameters("@CNYYBF").Value = Sickness.createdOn.Year Mod 100
    '            cmd.Parameters("@CNMMBF").Value = Sickness.createdOn.Month
    '            cmd.Parameters("@CNDDBF").Value = Sickness.createdOn.Day

    '            cmd.Parameters("@STATBF").Value = " "

    '            'LAST DAY WORKED
    '            cmd.Parameters("@LWRKBF").Value = CDate(Sickness.lastWorkedDate).Year * 10000 + CDate(Sickness.lastWorkedDate).Month * 100 + CDate(Sickness.lastWorkedDate).Day

    '            'Diagnosis code
    '            cmd.Parameters("@DIANBF").Value = 0

    '            'DATE OF ACCIDENT
    '            cmd.Parameters("@ACCDBF").Value = 0

    '            'DATE OF DEATH
    '            cmd.Parameters("@DEADBF").Value = 0

    '            'CHILD DATE OF BIRTH
    '            cmd.Parameters("@CHIDBF").Value = 0

    '            'DATE OF CLAIM RECEIVED
    '            cmd.Parameters("@CRDBF").Value = Now.Year * 10000 + Now.Month * 100 + Now.Day

    '            'DATE OF CLAIM PERIODO FROM
    '            Dim Datefrom As Date = Sickness.incapableDateFrom
    '            cmd.Parameters("@CPFDBF").Value = Datefrom.Year * 10000 + Datefrom.Month * 100 + Datefrom.Day

    '            'DATE OF CLAIM PERIODO TO
    '            Dim DateTo As Date = Sickness.incapableDateTo
    '            cmd.Parameters("@CPTDBF").Value = DateTo.Year * 10000 + DateTo.Month * 100 + DateTo.Day

    '            'DEGREE OF DISABLEMENT
    '            cmd.Parameters("@DEGDBF").Value = 0

    '            'PERMANENTLY OF INCAPABLE
    '            cmd.Parameters("@PRMDBF").Value = " "

    '            'EMPLOYERS REG
    '            cmd.Parameters("@RREGBF").Value = EmprNo
    '            cmd.Parameters("@RRSFBF").Value = EmprSub
    '            cmd.Parameters("@RREGBF2").Value = 0
    '            cmd.Parameters("@RRSFBF2").Value = 0
    '            cmd.Parameters("@RREGBF3").Value = 0
    '            cmd.Parameters("@RRSFBF3").Value = 0
    '            cmd.Parameters("@RREGBF4").Value = 0
    '            cmd.Parameters("@RRSFBF4").Value = 0
    '            cmd.Parameters("@RREGBF5").Value = 0
    '            cmd.Parameters("@RRSFBF5").Value = 0

    '            'PROVIDENT FUND CLAIM

    '            cmd.Parameters("@PROVFBF").Value = " "

    '            'PRECIPROCAL AGREEMENT

    '            cmd.Parameters("@RECPABF").Value = ""
    '            'GOVERNMENT CLAIM
    '            cmd.Parameters("@GOVWBF").Value = " "

    '            'STATEMENT QUERY
    '            cmd.Parameters("@STAQBF").Value = " "

    '            'COMPLIANCE QUERY
    '            cmd.Parameters("@CMPQBF").Value = " "

    '            ' Bank
    '            cmd.Parameters("@MPAYBF").Value = "B"
    '            'reassingempr
    '            If Sickness.consent = 1 Then
    '                cmd.Parameters("@BANKBF").Value = " "
    '                cmd.Parameters("@ACNOBF").Value = "0"

    '            Else
    '                cmd.Parameters("@BANKBF").Value = Mid(Sickness.bank, 5, 4)
    '                cmd.Parameters("@ACNOBF").Value = Sickness.accountNo

    '                If Sickness.bank = Nothing Or Sickness.accountNo = Nothing Or Sickness.accountType Is Nothing Then
    '                Else
    '                    Await InsertBankInformationEmpe(Sickness, Clmn)
    '                End If

    '            End If

    '            cmd.Parameters("@WBLINKCS").Value = Sickness.WebPortalLink
    '            Await cmd.ExecuteNonQueryAsync()
    '            cmd.Dispose()

    '        End Using
    '    Catch ex As iDB2Exception
    '        Throw ex
    '    End Try
    'End Function

    Async Function InsertBankInformationEmpe(Sickness As Document_Sickness, Clmn As String) As Task

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
                cmd.Parameters("@EMPE").Value = Sickness.nisNo
                cmd.Parameters("@CLMN").Value = Clmn
                cmd.Parameters("@BANK").Value = Sickness.bank
                cmd.Parameters("@ACCN").Value = Sickness.accountNo
                If Sickness.accountType = 1 Then
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
    Async Function InsertBankInformationEmpr(Sickness As Document_Sickness, Emprn As Integer, Emprsub As Integer) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                'insert in ni.xport for datacard

                Dim rsclm As iDB2DataReader
                Dim cmd As New iDB2Command() With {
                .CommandText = "Select * From ""QS36F"".""" & As400_lib & ".ACCEMPR"" WHERE EMPR1 = @EMPR1 And EMPS1 = @EMPS1",
                 .Connection = connection,
                .CommandTimeout = 0
                }

                cmd.DeriveParameters()
                cmd.Parameters("@EMPR1").Value = Trim(Emprn)
                cmd.Parameters("@EMPS1").Value = Trim(Emprsub)
                rsclm = cmd.ExecuteReader
                If rsclm.Read() = True Then
                Else

                    Dim cmdX As New iDB2Command() With {
                .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".ACCEMPR"" 
                                                 (ACTV1, EMPR1, EMPS1, BANK1, ACCN1, ACCTYP1, VEND1)
                                          VALUES (@ACTV, @EMPR, @EMPS, @BANK, @ACCN, @ACCTYP, @VEND)",
                .Connection = connection,
                .CommandTimeout = 0
                }

                    cmdX.DeriveParameters()
                    cmdX.Parameters("@ACTV").Value = "A"
                    cmdX.Parameters("@EMPR").Value = Emprn
                    cmdX.Parameters("@EMPS").Value = Emprsub
                    cmdX.Parameters("@BANK").Value = Sickness.employerBank
                    cmdX.Parameters("@ACCN").Value = Sickness.employerAccountNo
                    If Sickness.employerAccountType = 1 Then
                        cmdX.Parameters("@ACCTYP").Value = "SAV"
                    Else
                        cmdX.Parameters("@ACCTYP").Value = "DDA"
                    End If

                    cmdX.Parameters("@VEND").Value = "0"

                    Await cmdX.ExecuteNonQueryAsync()
                    cmdX.Dispose()

                End If


            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function


End Class
