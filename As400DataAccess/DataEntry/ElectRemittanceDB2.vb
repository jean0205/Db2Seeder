Imports IBM.Data.DB2.iSeries
Imports ShareModels.Models

Public Class ElectRemittanceDB2
    Dim cn = DB2ConnectionS.as400
    Dim As400_lib = DB2ConnectionS.As400_lib

    Async Function PostRemittances(EmprRemitt As Document_Remittance) As Task

        Dim lst As List(Of EmployeeContributionRecord) = EmprRemitt.employeeContributionRecords

        'Employer no and Sub
        Dim strCadena As String
        Dim intPos As Integer
        Dim EmprNo As String
        Dim EmprSub As String
        strCadena = EmprRemitt.employerNumber
        intPos = InStr(1, strCadena, "-") 'posicion de la "-"
        EmprNo = Mid(strCadena, 1, intPos - 1)
        EmprSub = Mid(strCadena, intPos + 1)
        Try
            For Each item As EmployeeContributionRecord In lst

                Await UpdEmnfile(item, EmprNo, EmprSub)
                Await UpdTotalCntrEmpe(item)
                Await InsertECXE(item, EmprNo, EmprSub)
                Await UpdECWE(item, EmprNo, EmprSub)

            Next

            'Variables
            Dim totalcontrs As Decimal = EmprRemitt.employeeContributionRecords.Sum(Function(x) x.contributions)
            Dim totalins As Decimal = EmprRemitt.employeeContributionRecords.Sum(Function(x) x.insurableEarnings)
            'period
            Dim periods = EmprRemitt.employeeContributionRecords.Select(Function(x) New With {x.contributionPeriodYear, x.contributionPeriodMonth})
            Dim Periodx = periods(0).contributionPeriodYear * 100 + periods(0).contributionPeriodMonth
            'frequency
            Dim Frequency = EmprRemitt.employeeContributionRecords.Select(Function(x) New With {x.frequency})
            Dim freq = Frequency(0).frequency
            'cant empe
            Dim cantempe As Decimal = EmprRemitt.employeeContributionRecords.Count()


            'compstat updated
            Await UpdArsum(EmprNo, EmprSub, Periodx, totalins, totalcontrs)
            Await UpdArop(EmprNo, EmprSub, Periodx, totalcontrs)
            Await UpdAritm(EmprNo, EmprSub, Periodx, totalins, totalcontrs)
            Await UpdSTAT(EmprNo, EmprSub, Periodx, cantempe)

            'empr update
            Await InsertRCXE(EmprNo, EmprSub, Periodx, freq, totalins, totalcontrs)
            Await InsertCONH(EmprNo, EmprSub, Periodx, freq, cantempe)
            Await UpdRCWE(EmprNo, EmprSub, Periodx, freq, totalins, totalcontrs)

        Catch ex As iDB2Exception
            Throw ex
        End Try


    End Function

#Region "EmploymentRecord"
    Async Function UpdEmnfile(EmpeCntr As EmployeeContributionRecord, EmprNo As String, EmprSub As String) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                'insert in NI.EMNT for
                Dim rs As iDB2DataReader
                Dim cmd As New iDB2Command() With {
                .CommandText = "select count(*) as cant from ""QS36F"".""" & As400_lib & ".EMNT"" where ACTV04 = 'A' AND EREG04 = @ereg AND RREG04 = @rreg AND RRSF04 = @rrsf",
                .Connection = connection,
                .CommandTimeout = 0
                }

                cmd.DeriveParameters()
                cmd.Parameters("@rreg").iDB2Value = EmprNo
                cmd.Parameters("@rrsf").iDB2Value = EmprSub
                cmd.Parameters("@ereg").iDB2Value = EmpeCntr.employeeNumber

                rs = Await cmd.ExecuteReaderAsync
                If rs.Read Then
                    If rs("cant") = 0 Then
                        Await SelectUpdNoEMPLOY(EmpeCntr, EmprNo, EmprSub)
                    End If
                End If
                cmd.Dispose()
                rs.Close()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function
    Async Function SelectUpdNoEMPLOY(EmpeCntr As EmployeeContributionRecord, EmprNo As String, EmprSub As String) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                'insert in NI.EMNT for
                Dim rs As iDB2DataReader
                Dim cmd As New iDB2Command() With {
                .CommandText = "select count(*) as cant from ""QS36F"".""" & As400_lib & ".TNEMPL"" where EREG09 = @ereg AND RREG09 = @rreg AND RRSF09 = @rrsf",
                .Connection = connection,
                .CommandTimeout = 0
                }

                cmd.DeriveParameters()
                cmd.Parameters("@rreg").iDB2Value = EmprNo
                cmd.Parameters("@rrsf").iDB2Value = EmprSub
                cmd.Parameters("@ereg").iDB2Value = EmpeCntr.employeeNumber

                rs = Await cmd.ExecuteReaderAsync
                If rs.Read Then
                    If rs("cant") = 0 Then

                        'UPDATE  FILE
                        Dim cmdNempl As New iDB2Command With {
                            .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".TNEMPL""
                                                 (LIN#09, RREG09, RRSF09, CONY09, CONM09, EREG09, NAM09, FREQ09, PWK109, PWK209, PWK309, PWK409, PWK509, USER09, POST09, POSTT)
                                                 VALUES(@LIN, @RREG, @RRSF, @CONY, @CONM, @EREG, @NAM, @FREQ, @PWK1, @PWK2, @PWK3, @PWK4, @PWK5, @USER, CURRENT DATE, CURRENT TIME) ",
                            .Connection = connection,
                            .CommandTimeout = 0
                        }
                        cmdNempl.DeriveParameters()
                        cmdNempl.Parameters("@LIN").Value = EmpeCntr.rowNumber
                        cmdNempl.Parameters("@RREG").Value = EmprNo
                        cmdNempl.Parameters("@RRSF").Value = EmprSub
                        cmdNempl.Parameters("@CONY").Value = EmpeCntr.contributionPeriodYear
                        cmdNempl.Parameters("@CONM").Value = EmpeCntr.contributionPeriodMonth
                        cmdNempl.Parameters("@EREG").Value = EmpeCntr.employeeNumber
                        cmdNempl.Parameters("@NAM").Value = EmpeCntr.employeeName
                        cmdNempl.Parameters("@FREQ").Value = EmpeCntr.frequency

                        If EmpeCntr.week1.hasWorked = False Then
                            cmdNempl.Parameters("@PWK1").Value = " "
                        Else
                            cmdNempl.Parameters("@PWK1").Value = "Y"
                        End If
                        If EmpeCntr.week2.hasWorked = False Then
                            cmdNempl.Parameters("@PWK2").Value = " "
                        Else
                            cmdNempl.Parameters("@PWK2").Value = "Y"
                        End If
                        If EmpeCntr.week3.hasWorked = False Then
                            cmdNempl.Parameters("@PWK3").Value = " "
                        Else
                            cmdNempl.Parameters("@PWK3").Value = "Y"
                        End If
                        If EmpeCntr.week4.hasWorked = False Then
                            cmdNempl.Parameters("@PWK4").Value = " "
                        Else
                            cmdNempl.Parameters("@PWK4").Value = "Y"
                        End If
                        If EmpeCntr.week5.hasWorked = False Then
                            cmdNempl.Parameters("@PWK5").Value = " "
                        Else
                            cmdNempl.Parameters("@PWK5").Value = "Y"
                        End If
                        cmdNempl.Parameters("@USER").Value = "UserID"
                        Await cmdNempl.ExecuteNonQueryAsync
                        cmdNempl.Dispose()

                    End If

                End If
                cmd.Dispose()
                rs.Close()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function
    Async Function SelectUpdMPLOY(EmpeCntr As EmployeeContributionRecord, EmprNo As String, EmprSub As String) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                'insert in NI.EMNT for
                Dim rs As iDB2DataReader
                Dim cmd As New iDB2Command() With {
                .CommandText = "select count(*) as cant from ""QS36F"".""" & As400_lib & ".TNEMPL"" where EREG09 = @ereg AND RREG09 = @rreg AND RRSF09 = @rrsf",
                .Connection = connection,
                .CommandTimeout = 0
                }

                cmd.DeriveParameters()
                cmd.Parameters("@rreg").iDB2Value = EmprNo
                cmd.Parameters("@rrsf").iDB2Value = EmprSub
                cmd.Parameters("@ereg").iDB2Value = EmpeCntr.employeeNumber

                rs = Await cmd.ExecuteReaderAsync
                If rs.Read Then
                    If rs("cant") = 0 Then

                        'UPDATE  FILE
                        Dim cmdNempl As New iDB2Command With {
                            .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".TNEMPL""
                                                 (LIN#09, RREG09, RRSF09, CONY09, CONM09, EREG09, NAM09, FREQ09, PWK109, PWK209, PWK309, PWK409, PWK509, USER09, POST09, POSTT)
                                                 VALUES(@LIN, @RREG, @RRSF, @CONY, @CONM, @EREG, @NAM, @FREQ, @PWK1, @PWK2, @PWK3, @PWK4, @PWK5, @USER, CURRENT DATE, CURRENT TIME) ",
                            .Connection = connection,
                            .CommandTimeout = 0
                        }
                        cmdNempl.DeriveParameters()
                        cmdNempl.Parameters("@LIN").Value = EmpeCntr.rowNumber
                        cmdNempl.Parameters("@RREG").Value = EmprNo
                        cmdNempl.Parameters("@RRSF").Value = EmprSub
                        cmdNempl.Parameters("@CONY").Value = EmpeCntr.contributionPeriodYear
                        cmdNempl.Parameters("@CONM").Value = EmpeCntr.contributionPeriodMonth
                        cmdNempl.Parameters("@EREG").Value = EmpeCntr.employeeNumber
                        cmdNempl.Parameters("@NAM").Value = EmpeCntr.employeeName
                        cmdNempl.Parameters("@FREQ").Value = EmpeCntr.frequency

                        If EmpeCntr.week1.hasWorked = False Then
                            cmdNempl.Parameters("@PWK1").Value = " "
                        Else
                            cmdNempl.Parameters("@PWK1").Value = "Y"
                        End If
                        If EmpeCntr.week2.hasWorked = False Then
                            cmdNempl.Parameters("@PWK2").Value = " "
                        Else
                            cmdNempl.Parameters("@PWK2").Value = "Y"
                        End If
                        If EmpeCntr.week3.hasWorked = False Then
                            cmdNempl.Parameters("@PWK3").Value = " "
                        Else
                            cmdNempl.Parameters("@PWK3").Value = "Y"
                        End If
                        If EmpeCntr.week4.hasWorked = False Then
                            cmdNempl.Parameters("@PWK4").Value = " "
                        Else
                            cmdNempl.Parameters("@PWK4").Value = "Y"
                        End If
                        If EmpeCntr.week5.hasWorked = False Then
                            cmdNempl.Parameters("@PWK5").Value = " "
                        Else
                            cmdNempl.Parameters("@PWK5").Value = "Y"
                        End If
                        cmdNempl.Parameters("@USER").Value = "UserID"
                        Await cmdNempl.ExecuteNonQueryAsync
                        cmdNempl.Dispose()

                    End If

                End If
                cmd.Dispose()
                rs.Close()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "EmployeeFiles"

    Async Function UpdTotalCntrEmpe(EmpeCntr As EmployeeContributionRecord) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                'insert in NI.EMNT for
                Dim rs As iDB2DataReader
                Dim totcontr As Integer = 0
                Dim cmd As New iDB2Command() With {
                .CommandText = "select TOTC03 from ""QS36F"".""" & As400_lib & ".EMPE"" WHERE EREG03 = @ERG And ACTV03 = 'A'",
                .Connection = connection,
                .CommandTimeout = 0
                }

                cmd.DeriveParameters()
                cmd.Parameters("@ERG").iDB2Value = EmpeCntr.employeeNumber
                rs = Await cmd.ExecuteReaderAsync
                If rs.Read Then
                    totcontr = Trim(rs("TOTC03").ToString)
                    Dim cmdt2 As New iDB2Command With {
                     .CommandText = "Update ""QS36F"".""" & As400_lib & ".EMPE"" SET TOTC03 = @TOTC03 WHERE EREG03 = @ERG And ACTV03 = 'A'",
                     .Connection = connection,
                     .CommandTimeout = 0
                 }
                    cmdt2.DeriveParameters()
                    cmdt2.Parameters("@ERG").iDB2Value = EmpeCntr.employeeNumber
                    Dim totcontr1 As Object = totcontr
                    If (totcontr1 Is DBNull.Value) Then
                        cmdt2.Parameters("@TOTC03").Value = EmpeCntr.weeksWorked
                        Await cmdt2.ExecuteNonQueryAsync()
                        cmdt2.Dispose()
                    Else
                        cmdt2.Parameters("@TOTC03").Value = totcontr + EmpeCntr.weeksWorked
                        Await cmdt2.ExecuteNonQueryAsync()
                        cmdt2.Dispose()
                    End If
                End If

                cmd.Dispose()
                rs.Close()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function

    '******Insert ECXE AFTER POST EMPLOYEE CONTRIBUTION WORK FILE*******
    Async Function InsertECXE(EmpeCntr As EmployeeContributionRecord, EmprNo As String, EmprSub As String) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                'insert in NI.EMNT for

                Dim CMDE As New iDB2Command With {
                            .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".ECXE"" 
                                                (ACTV09, RREG09, RRSF09, CCEN09, CONY09, CONM09, LIN#09, EREG09, EGIE09, ECNB09,
                                                 PWK109, PWK209, PWK309, PWK409, PWK509, PWK609, PAGE09, FREQ09, ERN109, ERN209,
                                                 ERN309, ERN409, ERN509, ERN609, WKSW09, USER09, POST09, FILL09)
                                          VALUES(@ACTV, @RREG, @RRSF, @CCEN, @CONY, @CONM, @LIN#, @EREG, @EGIE, @ECNB,
                                                 @PWK1, @PWK2, @PWK3, @PWK4, @PWK5, @PWK6, @PAGE, @FREQ, @ERN1, @ERN2,
                                                 @ERN3, @ERN4, @ERN5, @ERN6, @WKSW, @USER, @POST, @FILL)",
                            .Connection = connection,
                            .CommandTimeout = 0
                        }
                CMDE.DeriveParameters()

                CMDE.Parameters("@ACTV").Value = "A"
                CMDE.Parameters("@RREG").Value = EmprNo
                CMDE.Parameters("@RRSF").Value = EmprSub
                Dim cent = EmpeCntr.contributionPeriodYear \ 100
                Dim YearX = EmpeCntr.contributionPeriodYear - (cent * 100)
                CMDE.Parameters("@CCEN").Value = cent
                CMDE.Parameters("@CONY").Value = YearX
                CMDE.Parameters("@CONM").Value = EmpeCntr.contributionPeriodMonth
                CMDE.Parameters("@LIN#").Value = EmpeCntr.rowNumber
                CMDE.Parameters("@EREG").Value = EmpeCntr.employeeNumber
                CMDE.Parameters("@EGIE").Value = EmpeCntr.insurableEarnings
                CMDE.Parameters("@ECNB").Value = EmpeCntr.contributions

                ' Weeks
                If EmpeCntr.week1.hasWorked = False Then
                    CMDE.Parameters("@PWK1").Value = ""
                Else
                    CMDE.Parameters("@PWK1").Value = "Y"
                End If
                If EmpeCntr.week2.hasWorked = False Then
                    CMDE.Parameters("@PWK2").Value = ""
                Else
                    CMDE.Parameters("@PWK2").Value = "Y"
                End If
                If EmpeCntr.week3.hasWorked = False Then
                    CMDE.Parameters("@PWK3").Value = ""
                Else
                    CMDE.Parameters("@PWK3").Value = "Y"
                End If
                If EmpeCntr.week4.hasWorked = False Then
                    CMDE.Parameters("@PWK4").Value = ""
                Else
                    CMDE.Parameters("@PWK4").Value = "Y"
                End If
                If EmpeCntr.week5.hasWorked = False Then
                    CMDE.Parameters("@PWK5").Value = ""
                Else
                    CMDE.Parameters("@PWK5").Value = "Y"
                End If

                CMDE.Parameters("@PWK6").Value = ""
                CMDE.Parameters("@PAGE").Value = ""
                CMDE.Parameters("@FREQ").Value = EmpeCntr.frequency

                CMDE.Parameters("@ERN1").Value = If(EmpeCntr.week1.hasWorked = False, 0.00, EmpeCntr.week1.amount)
                CMDE.Parameters("@ERN2").Value = If(EmpeCntr.week2.hasWorked = False, 0.00, EmpeCntr.week2.amount)
                CMDE.Parameters("@ERN3").Value = If(EmpeCntr.week3.hasWorked = False, 0.00, EmpeCntr.week3.amount)
                CMDE.Parameters("@ERN4").Value = If(EmpeCntr.week4.hasWorked = False, 0.00, EmpeCntr.week4.amount)
                CMDE.Parameters("@ERN5").Value = If(EmpeCntr.week5.hasWorked = False, 0.00, EmpeCntr.week5.amount)


                CMDE.Parameters("@ERN6").Value = "0.00"
                CMDE.Parameters("@WKSW").Value = EmpeCntr.weeksWorked
                CMDE.Parameters("@USER").Value = "UserID"
                CMDE.Parameters("@POST").Value = ""
                CMDE.Parameters("@FILL").Value = ""
                Await CMDE.ExecuteNonQueryAsync
                CMDE.Dispose()


            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function

    ' Update CNTE Employee Contribution File
    Async Function UpdateCNTE(EmpeCntr As EmployeeContributionRecord, EmprNo As String, EmprSub As String) As Task

        Try
            Using connection As New iDB2Connection(cn)


                connection.Open()
                Dim rsc As iDB2DataReader

                Dim ern1 As Decimal = 0.0
                Dim ern2 As Decimal = 0.0
                Dim ern3 As Decimal = 0.0
                Dim ern4 As Decimal = 0.0
                Dim ern5 As Decimal = 0.0
                Dim w1 As String
                Dim w2 As String
                Dim w3 As String
                Dim w4 As String
                Dim w5 As String
                Dim Wkx As String = ""
                Dim egie1 As Decimal = 0.0
                Dim ecnb2 As Decimal = 0.0
                Dim rcnb3 As Decimal = 0.0
                Dim ACTV As String = ""
                Dim cmd3 As New iDB2Command()
                cmd3.CommandText = "Select  EGIE06, ECNB06, RCNB06, ERN106, ERN206, ERN306, ERN406, ERN506, CT#106, CT#206, CT#306, CT#406, CT#506, WKSW06, Actv06  From ""QS36F"".""" & As400_lib & ".CNTE""  Where Rreg06 = @Rreg06 And Rrsf06 = @Rrsf06  And Ereg06 = @Ereg And ((Ccen06*100)+ Cony06) = @CONY And Cper06 = @CONM And(Actv06 = @Actv06 Or Actv06 = @Actv)"
                cmd3.Connection = connection
                cmd3.CommandTimeout = 0
                cmd3.DeriveParameters()
                cmd3.Parameters("@Rreg06").iDB2Value = EmprNo
                cmd3.Parameters("@Rrsf06").iDB2Value = EmprSub
                cmd3.Parameters("@Actv06").iDB2Value = "A"
                cmd3.Parameters("@Actv").iDB2Value = "D"
                cmd3.Parameters("@Ereg").iDB2Value = EmpeCntr.employeeNumber
                cmd3.Parameters("@CONY").iDB2Value = EmpeCntr.contributionPeriodYear
                cmd3.Parameters("@CONM").iDB2Value = EmpeCntr.contributionPeriodMonth
                rsc = Await cmd3.ExecuteReaderAsync

                If rsc.Read Then
                    'update
                    egie1 = rsc("EGIE06")
                    ecnb2 = rsc("ECNB06")
                    rcnb3 = rsc("RCNB06")
                    ern1 = rsc("ERN106")
                    ern2 = rsc("ERN206")
                    ern3 = rsc("ERN306")
                    ern4 = rsc("ERN406")
                    ern5 = rsc("ERN506")
                    w1 = rsc("CT#106")
                    w2 = rsc("CT#206")
                    w3 = rsc("CT#306")
                    w4 = rsc("CT#406")
                    w5 = rsc("CT#506")
                    ACTV = rsc("Actv06")
                    Dim w As Object = rsc("WKSW06")
                    If (w Is DBNull.Value) Then
                        Wkx = 0
                    Else
                        Wkx = rsc("WKSW06").ToString
                    End If
                    Dim egie1x As Object = egie1
                    If (egie1x Is DBNull.Value) Then
                        egie1 = 0
                    End If
                    Dim ecnb2x As Object = ecnb2
                    If (ecnb2x Is DBNull.Value) Then
                        ecnb2 = 0
                    End If
                    Dim rcnb3x As Object = rcnb3
                    If (rcnb3x Is DBNull.Value) Then
                        rcnb3 = 0
                    End If

                    Dim w1X As Object = w1
                    If (w1X Is DBNull.Value) Then
                        w1 = ""
                    End If
                    Dim w2X As Object = w2
                    If (w2X Is DBNull.Value) Then
                        w2 = ""
                    End If
                    Dim W3X As Object = w3
                    If (W3X Is DBNull.Value) Then
                        w3 = ""
                    End If
                    Dim w4x As Object = w4
                    If (w4x Is DBNull.Value) Then
                        w4 = ""
                    End If
                    Dim w5x As Object = w5
                    If (w5x Is DBNull.Value) Then
                        w5 = ""
                    End If

                    If ACTV = "A" Then

                        Dim cmdup1 As New iDB2Command With {
                                .CommandText = "UPDATE ""QS36F"".""" & As400_lib & ".CNTE"" SET EGIE06 = @EGIE, ECNB06= @ECNB, RCNB06 = @RCNB, ERN106 = @ERN1, ERN206 = @ERN2, ERN306 = @ERN3, ERN406 = @ERN4, ERN506 = @ERN5, CT#106 = @CT#106, CT#206 = @CT#206, CT#306 = @CT#306, CT#406 = @CT#406, CT#506 = @CT#506, WKSW06 =@WKSW06 
                                  Where Rreg06 = @Rreg06 And Rrsf06 = @Rrsf06 And Actv06 = @Actv06 And Ereg06 = @Ereg And ((Ccen06*100)+ Cony06) = @CONY And Cper06 = @CONM",
                                .Connection = connection,
                                .CommandTimeout = 0
                                                   }
                        cmdup1.DeriveParameters()
                        cmdup1.Parameters("@Actv06").iDB2Value = "A"
                        cmdup1.Parameters("@Rreg06").iDB2Value = EmprNo
                        cmdup1.Parameters("@Rrsf06").iDB2Value = EmprSub
                        cmdup1.Parameters("@Ereg").iDB2Value = EmpeCntr.employeeNumber
                        cmdup1.Parameters("@CONY").iDB2Value = EmpeCntr.contributionPeriodYear
                        cmdup1.Parameters("@CONM").iDB2Value = EmpeCntr.contributionPeriodMonth

                        'Contribution and Earnings

                        cmdup1.Parameters("@EGIE").Value = EmpeCntr.insurableEarnings + egie1
                        Dim empe As Decimal = 0.0
                        Dim empr As Decimal = 0.0
                        'empe = row1(12)
                        'cmdup1.Parameters("@ECNB").Value = empe + ecnb2
                        'empr = row1(13)
                        'cmdup1.Parameters("@RCNB").Value = empr + rcnb3

                        cmdup1.Parameters("@ERN1").Value = ern1 + If(EmpeCntr.week1.hasWorked = False, 0.00, EmpeCntr.week1.amount)
                        cmdup1.Parameters("@ERN2").Value = ern2 + If(EmpeCntr.week2.hasWorked = False, 0.00, EmpeCntr.week2.amount)
                        cmdup1.Parameters("@ERN3").Value = ern3 + If(EmpeCntr.week3.hasWorked = False, 0.00, EmpeCntr.week3.amount)
                        cmdup1.Parameters("@ERN4").Value = ern4 + If(EmpeCntr.week4.hasWorked = False, 0.00, EmpeCntr.week4.amount)
                        cmdup1.Parameters("@ERN5").Value = ern5 + If(EmpeCntr.week5.hasWorked = False, 0.00, EmpeCntr.week5.amount)
                        'week
                        If EmpeCntr.week1.hasWorked = False Then
                            cmdup1.Parameters("@CT#106").Value = Trim(w1)
                        Else
                            cmdup1.Parameters("@CT#106").Value = "P"
                        End If

                        If EmpeCntr.week2.hasWorked = False Then
                            cmdup1.Parameters("@CT#206").Value = Trim(w2)
                        Else
                            cmdup1.Parameters("@CT#206").Value = "P"
                        End If

                        If EmpeCntr.week3.hasWorked = False Then
                            cmdup1.Parameters("@CT#306").Value = w3
                        Else
                            cmdup1.Parameters("@CT#306").Value = "P"
                        End If

                        If EmpeCntr.week4.hasWorked = False Then
                            cmdup1.Parameters("@CT#406").Value = w4
                        Else
                            cmdup1.Parameters("@CT#406").Value = "P"
                        End If
                        If EmpeCntr.week5.hasWorked = False Then
                            cmdup1.Parameters("@CT#506").Value = w5
                        Else
                            cmdup1.Parameters("@CT#506").Value = "P"
                        End If
                        If (Wkx + EmpeCntr.weeksWorked >= CantMonday(EmpeCntr.contributionPeriodYear, EmpeCntr.contributionPeriodMonth)) = True Then
                            cmdup1.Parameters("@WKSW06").Value = CantMonday(EmpeCntr.contributionPeriodYear, EmpeCntr.contributionPeriodMonth)
                        Else
                            cmdup1.Parameters("@WKSW06").Value = Wkx + EmpeCntr.weeksWorked
                        End If

                        Await cmdup1.ExecuteNonQueryAsync()
                        cmdup1.Dispose()

                    ElseIf ACTV = "D" Then

                        Dim cmdup1 As New iDB2Command With {
                                .CommandText = "UPDATE ""QS36F"".""" & As400_lib & ".CNTE"" SET EGIE06 = @EGIE, ECNB06= @ECNB, RCNB06 = @RCNB, ERN106 = @ERN1, ERN206 = @ERN2, ERN306 = @ERN3, ERN406 = @ERN4, ERN506 = @ERN5, CT#106 = @CT#106, CT#206 = @CT#206, CT#306 = @CT#306, CT#406 = @CT#406, CT#506 = @CT#506, WKSW06 =@WKSW06, Actv06 = @Actv Where Rreg06 = @Rreg06 And Rrsf06 = @Rrsf06 And Actv06 = @Actv06 And Ereg06 = @Ereg And ((Ccen06*100)+ Cony06) = @CONY And Cper06 = @CONM",
                                .Connection = connection,
                                .CommandTimeout = 0
                                                   }
                        cmdup1.DeriveParameters()
                        cmdup1.Parameters("@Actv06").iDB2Value = "D"
                        cmdup1.Parameters("@Rreg06").iDB2Value = EmprNo
                        cmdup1.Parameters("@Rrsf06").iDB2Value = EmprSub
                        cmdup1.Parameters("@Ereg").iDB2Value = EmpeCntr.employeeNumber
                        cmdup1.Parameters("@CONY").iDB2Value = EmpeCntr.contributionPeriodYear
                        cmdup1.Parameters("@CONM").iDB2Value = EmpeCntr.contributionPeriodMonth

                        'Contribution and Earnings

                        cmdup1.Parameters("@EGIE").Value = EmpeCntr.insurableEarnings
                        Dim empe As Decimal = 0.0
                        Dim empr As Decimal = 0.0
                        'empe = row1(12)
                        'cmdup1.Parameters("@ECNB").Value = empe
                        'empr = row1(13)
                        'cmdup1.Parameters("@RCNB").Value = empr

                        cmdup1.Parameters("@ERN1").Value = If(EmpeCntr.week1.hasWorked = False, 0.00, EmpeCntr.week1.amount)
                        cmdup1.Parameters("@ERN2").Value = If(EmpeCntr.week1.hasWorked = False, 0.00, EmpeCntr.week2.amount)
                        cmdup1.Parameters("@ERN3").Value = If(EmpeCntr.week1.hasWorked = False, 0.00, EmpeCntr.week3.amount)
                        cmdup1.Parameters("@ERN4").Value = If(EmpeCntr.week1.hasWorked = False, 0.00, EmpeCntr.week4.amount)
                        cmdup1.Parameters("@ERN5").Value = If(EmpeCntr.week1.hasWorked = False, 0.00, EmpeCntr.week5.amount)
                        'week
                        cmdup1.Parameters("@CT#106").Value = If(EmpeCntr.week1.hasWorked = False, " ", "P")
                        cmdup1.Parameters("@CT#206").Value = If(EmpeCntr.week2.hasWorked = False, " ", "P")
                        cmdup1.Parameters("@CT#306").Value = If(EmpeCntr.week3.hasWorked = False, " ", "P")
                        cmdup1.Parameters("@CT#406").Value = If(EmpeCntr.week4.hasWorked = False, " ", "P")
                        cmdup1.Parameters("@CT#506").Value = If(EmpeCntr.week5.hasWorked = False, " ", "P")

                        If (EmpeCntr.weeksWorked >= CantMonday(EmpeCntr.contributionPeriodYear, EmpeCntr.contributionPeriodMonth)) = True Then
                            cmdup1.Parameters("@WKSW06").Value = CantMonday(EmpeCntr.contributionPeriodYear, EmpeCntr.contributionPeriodMonth)
                        Else
                            cmdup1.Parameters("@WKSW06").Value = EmpeCntr.weeksWorked
                        End If
                        cmdup1.Parameters("@ACTV").Value = "A"

                        Await cmdup1.ExecuteNonQueryAsync()
                        cmdup1.Dispose()
                    End If
                Else
                    'insert
                    Await InsertCNTE(EmpeCntr, EmprNo, EmprSub)

                End If
                cmd3.Dispose()
                rsc.Close()
            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function
    Async Function InsertCNTE(EmpeCntr As EmployeeContributionRecord, EmprNo As String, EmprSub As String) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                'insert in NI.EMNT for

                Dim cmdCNTE As New iDB2Command With {
                            .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".TNEMPL""
                                                 (LIN#09, RREG09, RRSF09, CONY09, CONM09, EREG09, NAM09, FREQ09, PWK109, PWK209, PWK309, PWK409, PWK509, USER09, POST09, POSTT)
                                                 VALUES(@LIN, @RREG, @RRSF, @CONY, @CONM, @EREG, @NAM, @FREQ, @PWK1, @PWK2, @PWK3, @PWK4, @PWK5, @USER, CURRENT DATE, CURRENT TIME) ",
                            .Connection = connection,
                            .CommandTimeout = 0
                        }
                cmdCNTE.DeriveParameters()
                cmdCNTE.Parameters("@ACTV06").Value = "A"
                cmdCNTE.Parameters("@EREG06").Value = EmpeCntr.employeeNumber
                cmdCNTE.Parameters("@RREG06").Value = EmprNo
                cmdCNTE.Parameters("@RRSF06").Value = EmprSub
                Dim cent = EmpeCntr.contributionPeriodYear \ 100
                Dim YearX = EmpeCntr.contributionPeriodYear - (cent * 100)
                cmdCNTE.Parameters("@CCEN06").Value = cent
                cmdCNTE.Parameters("@CONY06").Value = YearX
                cmdCNTE.Parameters("@CPER06").Value = EmpeCntr.contributionPeriodMonth
                cmdCNTE.Parameters("@EGIE06").Value = EmpeCntr.insurableEarnings
                cmdCNTE.Parameters("@ECNB06").Value = EmpeCntr.contributions '*************************
                ' Week

                If EmpeCntr.week1.hasWorked = False Then
                    cmdCNTE.Parameters("@CT#106").Value = ""
                Else
                    cmdCNTE.Parameters("@CT#106").Value = "P"
                End If

                If EmpeCntr.week2.hasWorked = False Then
                    cmdCNTE.Parameters("@CT#206").Value = ""
                Else
                    cmdCNTE.Parameters("@CT#206").Value = "P"
                End If
                If EmpeCntr.week3.hasWorked = False Then
                    cmdCNTE.Parameters("@CT#306").Value = ""
                Else
                    cmdCNTE.Parameters("@CT#306").Value = "P"
                End If

                If EmpeCntr.week4.hasWorked = False Then
                    cmdCNTE.Parameters("@CT#406").Value = ""
                Else
                    cmdCNTE.Parameters("@CT#406").Value = "P"
                End If
                If EmpeCntr.week5.hasWorked = False Then
                    cmdCNTE.Parameters("@CT#506").Value = ""
                Else
                    cmdCNTE.Parameters("@CT#506").Value = "P"
                End If

                cmdCNTE.Parameters("@CT#606").Value = ""

                cmdCNTE.Parameters("@EGRS06").Value = 0.0
                cmdCNTE.Parameters("@RCNB06").Value = "" '****************
                cmdCNTE.Parameters("@PAGE06").Value = ""
                cmdCNTE.Parameters("@FREQ06").Value = EmpeCntr.frequency
                cmdCNTE.Parameters("@ERN106").Value = EmpeCntr.week1.amount
                cmdCNTE.Parameters("@ERN206").Value = EmpeCntr.week2.amount
                cmdCNTE.Parameters("@ERN306").Value = EmpeCntr.week3.amount
                cmdCNTE.Parameters("@ERN406").Value = EmpeCntr.week4.amount
                cmdCNTE.Parameters("@ERN506").Value = EmpeCntr.week5.amount
                cmdCNTE.Parameters("@ERN606").Value = 0.0
                cmdCNTE.Parameters("@WKSW06").Value = EmpeCntr.weeksWorked
                cmdCNTE.Parameters("@CRIE06").Value = 0.0
                cmdCNTE.Parameters("@CRD106").Value = 0.0
                cmdCNTE.Parameters("@CRD206").Value = 0.0
                cmdCNTE.Parameters("@CRD306").Value = 0.0
                cmdCNTE.Parameters("@CRD406").Value = 0.0
                cmdCNTE.Parameters("@CRD506").Value = 0.0
                cmdCNTE.Parameters("@CRD606").Value = 0.0
                cmdCNTE.Parameters("@FILL06").Value = ""

                Await cmdCNTE.ExecuteNonQueryAsync
                cmdCNTE.Dispose()



            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function

    Async Function UpdECWE(EmpeCntr As EmployeeContributionRecord, EmprNo As String, EmprSub As String) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                Dim rs As iDB2DataReader
                Dim totcontr As Integer = 0
                Dim cmd As New iDB2Command() With {
                .CommandText = "Select count(*) as cant  From ""QS36F"".""" & As400_lib & ".ECWE"" " &
                        " Where Rreg09 = @Rreg09 And Rrsf09 = @Rrsf09 And ((Ccen09*100)+ Cony09) = @CONY And CONM09 = @CONM And LIN#09 = @LIN#",
                .Connection = connection,
                .CommandTimeout = 0
                }

                cmd.DeriveParameters()
                cmd.Parameters("@Rreg09").iDB2Value = EmprNo
                cmd.Parameters("@Rrsf09").iDB2Value = EmprSub
                cmd.Parameters("@CONY").iDB2Value = EmpeCntr.contributionPeriodYear
                cmd.Parameters("@CONM").iDB2Value = EmpeCntr.contributionPeriodMonth
                cmd.Parameters("@LIN#").iDB2Value = EmpeCntr.rowNumber
                rs = Await cmd.ExecuteReaderAsync
                If rs.Read Then
                    If rs("cant") > 0 Then

                        Dim cmdU As String = "UPDATE ""QS36F"".""" & As400_lib & ".ECWE"" SET Actv09 = @Actv,  CCEN09 = @CCEN, CONY09 = @CONY, CONM09 = @CONM, EREG09 = @EREG, EGIE09 = @EGIE, ECNB09= @ECNB,  PWK109 = @PWK1, PWK209 = @PWK2, PWK309 = @PWK3, PWK409 = @PWK4, PWK509 = @PWK5, FREQ09 = @FREQ,   ERN109 = @ERN1, ERN209 = @ERN2, ERN309 = @ERN3, ERN409 = @ERN4, ERN509 = @ERN5, WKSW09 = @WKSW, USER09 = @USER Where Rreg09 = @Rreg09 And Rrsf09 = @Rrsf09 And((Ccen09*100)+ Cony09) = @CONY09 And CONM09= @CONM09 And LIN#09 = @LIN#"
                        Dim cmdup As New iDB2Command() With {
                            .CommandText = cmdU,
                            .Connection = connection,
                            .CommandTimeout = 0
                        }
                        cmdup.DeriveParameters()
                        cmdup.Parameters("@Rreg09").iDB2Value = EmprNo
                        cmdup.Parameters("@Rrsf09").iDB2Value = EmprSub
                        cmdup.Parameters("@CONY09").iDB2Value = EmpeCntr.contributionPeriodYear
                        cmdup.Parameters("@CONM09").iDB2Value = EmpeCntr.contributionPeriodMonth
                        cmdup.Parameters("@LIN#").iDB2Value = EmpeCntr.rowNumber
                        'UPDATE a
                        cmdup.Parameters("@ACTV").Value = "A"

                        Dim centx = EmpeCntr.contributionPeriodYear \ 100
                        Dim yearx = EmpeCntr.contributionPeriodYear - (centx * 100)
                        cmdup.Parameters("@CCEN").Value = centx
                        cmdup.Parameters("@CONY").Value = yearx
                        cmdup.Parameters("@CONM").Value = EmpeCntr.contributionPeriodMonth
                        cmdup.Parameters("@EREG").Value = EmpeCntr.employeeNumber
                        cmdup.Parameters("@EGIE").Value = EmpeCntr.insurableEarnings
                        cmdup.Parameters("@ECNB").Value = EmpeCntr.contributions

                        ' Week
                        If EmpeCntr.week1.hasWorked = False Then
                            cmdup.Parameters("@PWK1").Value = ""
                        Else
                            cmdup.Parameters("@PWK1").Value = "Y"
                        End If
                        If EmpeCntr.week2.hasWorked = False Then
                            cmdup.Parameters("@PWK2").Value = ""
                        Else
                            cmdup.Parameters("@PWK2").Value = "Y"
                        End If
                        If EmpeCntr.week3.hasWorked = False Then
                            cmdup.Parameters("@PWK3").Value = ""
                        Else
                            cmdup.Parameters("@PWK3").Value = "Y"
                        End If
                        If EmpeCntr.week4.hasWorked = False Then
                            cmdup.Parameters("@PWK4").Value = ""
                        Else
                            cmdup.Parameters("@PWK4").Value = "Y"
                        End If
                        If EmpeCntr.week5.hasWorked = False Then
                            cmdup.Parameters("@PWK5").Value = ""
                        Else
                            cmdup.Parameters("@PWK5").Value = "Y"
                        End If
                        cmdup.Parameters("@FREQ").Value = EmpeCntr.frequency
                        cmdup.Parameters("@ERN1").Value = If(EmpeCntr.week1.hasWorked = False, 0.00, EmpeCntr.week1.amount)
                        cmdup.Parameters("@ERN2").Value = If(EmpeCntr.week2.hasWorked = False, 0.00, EmpeCntr.week2.amount)
                        cmdup.Parameters("@ERN3").Value = If(EmpeCntr.week3.hasWorked = False, 0.00, EmpeCntr.week3.amount)
                        cmdup.Parameters("@ERN4").Value = If(EmpeCntr.week4.hasWorked = False, 0.00, EmpeCntr.week4.amount)
                        cmdup.Parameters("@ERN5").Value = If(EmpeCntr.week5.hasWorked = False, 0.00, EmpeCntr.week5.amount)
                        cmdup.Parameters("@ERN6").Value = "0.00"
                        cmdup.Parameters("@WKSW").Value = EmpeCntr.weeksWorked
                        cmdup.Parameters("@USER").Value = "userID"
                        Await cmdup.ExecuteNonQueryAsync()
                        cmdup.Dispose()
                    Else
                        Await InsertECWE(EmpeCntr, EmprNo, EmprSub)
                    End If
                End If

                cmd.Dispose()
                rs.Close()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function
    Async Function InsertECWE(EmpeCntr As EmployeeContributionRecord, EmprNo As String, EmprSub As String) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                'insert in NI.EMNT for

                Dim cmd As New iDB2Command With {
                            .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".ECWE"" 
                                               (ACTV09, RREG09, RRSF09, CCEN09, CONY09, CONM09, LIN#09, EREG09, EGIE09, ECNB09,
                                               PWK109, PWK209, PWK309, PWK409, PWK509, PWK609, PAGE09, FREQ09, ERN109, ERN209,
                                               ERN309, ERN409, ERN509, ERN609, WKSW09, USER09, POST09, FILL09)
                                        VALUES(@ACTV, @RREG, @RRSF, @CCEN, @CONY, @CONM, @LIN#, @EREG, @EGIE, @ECNB,
                                               @PWK1, @PWK2, @PWK3, @PWK4, @PWK5, @PWK6, @PAGE, @FREQ, @ERN1, @ERN2,
                                               @ERN3, @ERN4, @ERN5, @ERN6, @WKSW, @USER, @POST, @FILL)",
                            .Connection = connection,
                            .CommandTimeout = 0
                        }
                cmd.DeriveParameters()
                cmd.Parameters("@ACTV").Value = "A"
                cmd.Parameters("@RREG").Value = EmprNo
                cmd.Parameters("@RRSF").Value = EmprSub
                '((Ccen09*100)+ Cony09
                Dim centx = EmpeCntr.contributionPeriodYear \ 100
                Dim yearx = EmpeCntr.contributionPeriodYear - (centx * 100)
                cmd.Parameters("@CCEN").Value = centx
                cmd.Parameters("@CONY").Value = yearx
                cmd.Parameters("@CONM").Value = EmpeCntr.contributionPeriodMonth
                cmd.Parameters("@LIN#").Value = EmpeCntr.rowNumber
                cmd.Parameters("@EREG").Value = EmpeCntr.employeeNumber
                cmd.Parameters("@EGIE").Value = EmpeCntr.insurableEarnings
                cmd.Parameters("@ECNB").Value = EmpeCntr.contributions


                ' Weeks
                If EmpeCntr.week1.hasWorked = False Then
                    cmd.Parameters("@PWK1").Value = ""
                Else
                    cmd.Parameters("@PWK1").Value = "Y"
                End If
                If EmpeCntr.week2.hasWorked = False Then
                    cmd.Parameters("@PWK2").Value = ""
                Else
                    cmd.Parameters("@PWK2").Value = "Y"
                End If
                If EmpeCntr.week3.hasWorked = False Then
                    cmd.Parameters("@PWK3").Value = ""
                Else
                    cmd.Parameters("@PWK3").Value = "Y"
                End If
                If EmpeCntr.week4.hasWorked = False Then
                    cmd.Parameters("@PWK4").Value = ""
                Else
                    cmd.Parameters("@PWK4").Value = "Y"
                End If
                If EmpeCntr.week5.hasWorked = False Then
                    cmd.Parameters("@PWK5").Value = ""
                Else
                    cmd.Parameters("@PWK5").Value = "Y"
                End If

                cmd.Parameters("@PWK6").Value = ""
                cmd.Parameters("@PAGE").Value = ""
                cmd.Parameters("@FREQ").Value = EmpeCntr.frequency

                cmd.Parameters("@ERN1").Value = If(EmpeCntr.week1.hasWorked = False, 0.00, EmpeCntr.week1.amount)
                cmd.Parameters("@ERN2").Value = If(EmpeCntr.week2.hasWorked = False, 0.00, EmpeCntr.week2.amount)
                cmd.Parameters("@ERN3").Value = If(EmpeCntr.week3.hasWorked = False, 0.00, EmpeCntr.week3.amount)
                cmd.Parameters("@ERN4").Value = If(EmpeCntr.week4.hasWorked = False, 0.00, EmpeCntr.week4.amount)
                cmd.Parameters("@ERN5").Value = If(EmpeCntr.week5.hasWorked = False, 0.00, EmpeCntr.week5.amount)
                cmd.Parameters("@ERN6").Value = "0.00"
                cmd.Parameters("@WKSW").Value = EmpeCntr.weeksWorked
                cmd.Parameters("@USER").Value = "UserID"
                cmd.Parameters("@POST").Value = ""
                cmd.Parameters("@FILL").Value = ""
                Await cmd.ExecuteNonQueryAsync
                cmd.Dispose()


            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function
    Public Function CantMonday(ByVal anno As Integer, ByVal mes As Integer)
        ' le pasas el year, mes y dia 
        Dim cantidad As Integer = 0
        Dim fechaRef As New Date(anno, mes, 1)
        While fechaRef.DayOfWeek <> DayOfWeek.Monday
            fechaRef = fechaRef.AddDays(1)
            'Year(Date.Now)
        End While
        Dim fecha As Date
        For i As Integer = 0 To 5
            fecha = fechaRef.AddDays(i * 7)
            If fecha.Month = mes Then
                cantidad = cantidad + 1
            Else
                Exit For
            End If
        Next
        Return cantidad
    End Function

#End Region


#Region "EmployerFiles"
    '******Insert RCXE AFTER POST EMPLOYER CONTRIBUTION WORK FILE*******
    Async Function InsertRCXE(EmprNo As String, EmprSub As String, period As String, Freq As String, totalins As Decimal, totalcontrs As Decimal) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                Dim cmd As New iDB2Command With {
                            .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".RCXE""
                                          (ACTV10, RREG10, RRSF10, CCEN10, CONY10, CONM10, TCON10, FINP10, INTP10, AMPD10, IWK110, IWK210, IWK310, IWK410, IWK510, IWK610, BCHI10, BCHC10, CENR10, DATR10, EDIT10, LOCK10, FREQ10, USER10, WSID10, DATE10, FILL10)
                                   VALUES(@ACTV10, @RREG10, @RRSF10, @CCEN10, @CONY10, @CONM10, @TCON10, @FINP10, @INTP10, @AMPD10, @IWK110, @IWK210, @IWK310, @IWK410, @IWK510, @IWK610,         @BCHI10, @BCHC10, @CENR10, @DATR10, @EDIT10, @LOCK10, @FREQ10, @USER10, @WSID10, @DATE10, @FILL10)",
                            .Connection = connection,
                            .CommandTimeout = 0
                        }
                cmd.DeriveParameters()

                cmd.Parameters("@ACTV10").Value = "A"
                cmd.Parameters("@RREG10").Value = EmprNo
                cmd.Parameters("@RRSF10").Value = EmprSub
                Dim centX = Mid(period, 1, 4) \ 100
                Dim yearX = Mid(period, 1, 4) - (centX * 100)
                cmd.Parameters("@CCEN10").Value = centX
                cmd.Parameters("@CONY10").Value = yearX
                cmd.Parameters("@CONM10").Value = Mid(period, 5, 2)

                cmd.Parameters("@TCON10").Value = 0.0
                cmd.Parameters("@FINP10").Value = 0.0
                cmd.Parameters("@INTP10").Value = 0.0
                cmd.Parameters("@AMPD10").Value = 0.0
                ' Number of Weekms
                Dim cantidad As Integer = 0
                Dim numberW = New List(Of String)
                Dim fechaRef As New Date(Mid(period, 1, 4), Mid(period, 5, 2), 1)
                While fechaRef.DayOfWeek <> DayOfWeek.Monday
                    fechaRef = fechaRef.AddDays(1)
                    'Year(Date.Now)
                End While

                Dim fecha As Date
                For i As Integer = 0 To 5
                    fecha = fechaRef.AddDays(i * 7)
                    If fecha.Month = Mid(period, 5, 2) Then
                        cantidad = cantidad + 1
                        Dim nWeek As Integer = -1
                        nWeek = CInt(DatePart(DateInterval.WeekOfYear, fecha, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFullWeek))
                        numberW.Add(nWeek)
                    Else
                        Exit For
                    End If
                Next
                cmd.Parameters("@IWK110").Value = numberW(0)
                cmd.Parameters("@IWK210").Value = numberW(1)
                cmd.Parameters("@IWK310").Value = numberW(2)
                cmd.Parameters("@IWK410").Value = numberW(3)
                If cantidad = 5 Then
                    cmd.Parameters("@IWK510").Value = numberW(4)
                Else
                    cmd.Parameters("@IWK510").Value = 0
                End If

                cmd.Parameters("@IWK610").Value = 0
                cmd.Parameters("@BCHI10").Value = totalins
                cmd.Parameters("@BCHC10").Value = totalcontrs
                cmd.Parameters("@FREQ10").Value = Freq

                cmd.Parameters("@CENR10").Value = Now.Year \ 100
                cmd.Parameters("@DATR10").Value = (((Now.Year Mod 100) * 10000) + Now.Month * 100) + Now.Day
                cmd.Parameters("@EDIT10").Value = "P"
                cmd.Parameters("@LOCK10").Value = ""
                cmd.Parameters("@USER10").Value = "USERID"
                cmd.Parameters("@WSID10").Value = "W1"
                cmd.Parameters("@DATE10").Value = (((Now.Year Mod 100) * 10000) + (Now.Month * 100)) + Now.Day
                cmd.Parameters("@FILL10").Value = ""

                Await cmd.ExecuteNonQueryAsync
                cmd.Dispose()


            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function
    Async Function InsertCONH(EmprNo As String, EmprSub As String, period As String, freq As String, cantempe As Integer) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()

                Dim rsch As iDB2DataReader
                Dim cmdch As New iDB2Command() With {
                       .CommandText = "Select EM0127, EM0227, EM0327, EM0427, EM0527, EM0627, EM0727, EM0827, EM0927, EM1027, EM1127, EM1227 From ""QS36F"".""" & As400_lib & ".CONH""                    Where Rreg27 = @Rreg27  And Rrsf27 = @Rrsf27 And ((Ccen27*100)+ Cony27) = @CONY And Actv27 = 'A' And Freq27 = @Freq27",
                       .Connection = connection,
                       .CommandTimeout = 0
              }
                cmdch.DeriveParameters()
                cmdch.Parameters("@Rreg27").iDB2Value = EmprNo
                cmdch.Parameters("@Rrsf27").iDB2Value = EmprSub
                cmdch.Parameters("@CONY").iDB2Value = Mid(period, 1, 4)
                cmdch.Parameters("@Freq27").iDB2Value = freq

                rsch = Await cmdch.ExecuteReaderAsync
                Dim EM01 As Integer = 0
                Dim EM02 As Integer = 0
                Dim EM03 As Integer = 0
                Dim EM04 As Integer = 0
                Dim EM05 As Integer = 0
                Dim EM06 As Integer = 0
                Dim EM07 As Integer = 0
                Dim EM08 As Integer = 0
                Dim EM09 As Integer = 0
                Dim EM010 As Integer = 0
                Dim EM011 As Integer = 0
                Dim EM012 As Integer = 0
                If rsch.Read() Then
                    EM01 = rsch("EM0127")
                    EM02 = rsch("EM0227")
                    EM03 = rsch("EM0327")
                    EM04 = rsch("EM0427")
                    EM05 = rsch("EM0527")
                    EM06 = rsch("EM0627")
                    EM07 = rsch("EM0727")
                    EM08 = rsch("EM0827")
                    EM09 = rsch("EM0927")
                    EM010 = rsch("EM1027")
                    EM011 = rsch("EM1127")
                    EM012 = rsch("EM1227")

                    Dim EM01x As Object = EM01
                    If (EM01x Is DBNull.Value) Then
                        EM01 = 0
                    End If
                    Dim EM02x As Object = EM02
                    If (EM02x Is DBNull.Value) Then
                        EM02 = 0
                    End If

                    Dim EM03x As Object = EM03
                    If (EM03x Is DBNull.Value) Then
                        EM03 = 0
                    End If

                    Dim EM04x As Object = EM04
                    If (EM04x Is DBNull.Value) Then
                        EM04 = 0
                    End If

                    Dim EM05x As Object = EM05
                    If (EM05x Is DBNull.Value) Then
                        EM05 = 0
                    End If

                    Dim EM06x As Object = EM06
                    If (EM06x Is DBNull.Value) Then
                        EM06 = 0
                    End If

                    Dim EM07x As Object = EM07
                    If (EM07x Is DBNull.Value) Then
                        EM07 = 0
                    End If

                    Dim EM08x As Object = EM08
                    If (EM08x Is DBNull.Value) Then
                        EM08 = 0
                    End If

                    Dim EM09x As Object = EM09
                    If (EM09x Is DBNull.Value) Then
                        EM09 = 0
                    End If

                    Dim EM010x As Object = EM010
                    If (EM010x Is DBNull.Value) Then
                        EM010 = 0
                    End If

                    Dim EM011x As Object = EM011
                    If (EM011x Is DBNull.Value) Then
                        EM011 = 0
                    End If

                    Dim EM012x As Object = EM012
                    If (EM012x Is DBNull.Value) Then
                        EM012 = 0
                    End If


                    Dim cmdXCONH As String = "UPDATE ""QS36F"".""" & As400_lib & ".CONH"" SET EM0127 = @EM0127, EM0227 = @EM0227, EM0327 = @EM0327, EM0427= @EM0427, EM0527 = @EM0527, EM0627 = @EM0627, EM0727 = @EM0727, EM0827 = @EM0827, EM0927 = @EM0927, EM1027 = @EM1027, EM1127 = @EM1127, EM1227 = @EM1227, LMO#27 = @LMO#27, LWK#27 = @LWK#27, FILL27 = @FILL27 Where Rreg27 = @Rreg27 And Rrsf27 = @Rrsf27 And ((Ccen27*100)+ Cony27) = @CONY And Actv27 = 'A' And Freq27 = @Freq27"

                    Dim cmdupX As New iDB2Command() With {
                       .Connection = connection,
                       .CommandTimeout = 0
                    }
                    cmdupX.DeriveParameters()
                    cmdupX.Parameters("@Rreg27").iDB2Value = EmprNo
                    cmdupX.Parameters("@Rrsf27").iDB2Value = EmprSub
                    cmdupX.Parameters("@CONY").iDB2Value = Mid(period, 1, 4)
                    cmdupX.Parameters("@Freq27").iDB2Value = freq
                    'UPDATE 
                    Dim monthN As Integer = Mid(period, 5, 2)
                    If monthN = 1 Then
                        cmdupX.Parameters("@EM0127").Value = EM01 + cantempe
                    Else
                        cmdupX.Parameters("@EM0127").Value = EM01
                    End If

                    If monthN = 2 Then
                        cmdupX.Parameters("@EM0227").Value = EM02 + cantempe
                    Else
                        cmdupX.Parameters("@EM0227").Value = EM02
                    End If
                    If monthN = 3 Then
                        cmdupX.Parameters("@EM0327").Value = EM03 + cantempe
                    Else
                        cmdupX.Parameters("@EM0327").Value = EM03
                    End If
                    If monthN = 4 Then
                        cmdupX.Parameters("@EM0427").Value = EM04 + cantempe
                    Else
                        cmdupX.Parameters("@EM0427").Value = EM04
                    End If
                    If monthN = 5 Then
                        cmdupX.Parameters("@EM0527").Value = EM05 + cantempe
                    Else
                        cmdupX.Parameters("@EM0527").Value = EM05
                    End If
                    If monthN = 6 Then
                        cmdupX.Parameters("@EM0627").Value = EM06 + cantempe
                    Else
                        cmdupX.Parameters("@EM0627").Value = EM06
                    End If
                    If monthN = 7 Then
                        cmdupX.Parameters("@EM0727").Value = EM07 + cantempe
                    Else
                        cmdupX.Parameters("@EM0727").Value = EM07
                    End If
                    If monthN = 8 Then
                        cmdupX.Parameters("@EM0827").Value = EM08 + cantempe
                    Else
                        cmdupX.Parameters("@EM0827").Value = EM08
                    End If
                    If monthN = 9 Then
                        cmdupX.Parameters("@EM0927").Value = EM09 + cantempe
                    Else
                        cmdupX.Parameters("@EM0927").Value = EM09
                    End If
                    If monthN = 10 Then
                        cmdupX.Parameters("@EM1027").Value = EM010 + cantempe
                    Else
                        cmdupX.Parameters("@EM1027").Value = EM010
                    End If
                    If monthN = 11 Then
                        cmdupX.Parameters("@EM1127").Value = EM011 + cantempe
                    Else
                        cmdupX.Parameters("@EM1127").Value = EM011
                    End If
                    If monthN = 12 Then
                        cmdupX.Parameters("@EM1227").Value = EM012 + cantempe
                    Else
                        cmdupX.Parameters("@EM1227").Value = EM012
                    End If

                    cmdupX.Parameters("@LMO#27").Value = monthN

                    ' Number of Week

                    Dim fechaRef1 As New Date(Mid(period, 1, 4), monthN, 1)
                    While fechaRef1.DayOfWeek <> DayOfWeek.Monday
                        fechaRef1 = fechaRef1.AddDays(1)
                    End While
                    Dim mesL As String = ""
                    Dim nWeek As Integer = -1
                    Dim fecha As Date
                    For i As Integer = 0 To 5
                        fecha = fechaRef1.AddDays(i * 7)
                        If fecha.Month = monthN Then
                            nWeek = CInt(DatePart(DateInterval.WeekOfYear, fecha, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFullWeek))
                        Else
                            Exit For
                        End If
                        mesL = nWeek
                    Next
                    cmdupX.Parameters("@LWK#27").Value = mesL
                    cmdupX.Parameters("@FILL27").Value = ""
                    Await cmdupX.ExecuteNonQueryAsync()
                    cmdupX.Dispose()

                Else

                    '*************Insert CONH Contribution History File****


                    Dim CMDTCONH As String = "INSERT INTO ""QS36F"".""" & As400_lib & ".CONH""" &
                                                          "(ACTV27, RREG27, RRSF27, CCEN27, CONY27, FREQ27, EM0127, EM0227, EM0327, EM0427, EM0527, EM0627, EM0727, EM0827, EM0927, EM1027, EM1127, EM1227, WK0127, WK0227, WK0327, WK0427, WK0527, WK0627, WK0727, WK0827, WK0927, WK1027, WK1127, WK1227,LMO#27, LWK#27, FILL27)" &
                                                             " VALUES(@ACTV27, @RREG27, @RRSF27, @CCEN27, @CONY27, @FREQ27, @EM0127, @EM0227, @EM0327, @EM0427, @EM0527, @EM0627, @EM0727, @EM0827, @EM0927, @EM1027, @EM1127, @EM1227, @WK0127, @WK0227, @WK0327, @WK0427, @WK0527, @WK0627, @WK0727, @WK0827, @WK0927, @WK1027, @WK1127, @WK1227,@LMO#27, @LWK#27, @FILL27)"

                    Dim cmdCOHN As New iDB2Command() With {
                                .Connection = connection,
                       .CommandTimeout = 0
                    }
                    cmdCOHN.DeriveParameters()
                    Dim Centx = Mid(period, 1, 4) \ 100
                    Dim Yearx = Mid(period, 1, 4) - (Centx * 100)
                    'Insert
                    cmdCOHN.Parameters("@ACTV27").Value = "A"
                    cmdCOHN.Parameters("@RREG27").Value = EmprNo
                    cmdCOHN.Parameters("@RRSF27").Value = EmprSub
                    cmdCOHN.Parameters("@CCEN27").Value = Centx
                    cmdCOHN.Parameters("@CONY27").Value = Yearx
                    cmdCOHN.Parameters("@FREQ27").Value = freq
                    Dim monthx As Integer = Mid(period, 5, 2)
                    If monthx = 1 Then
                        cmdCOHN.Parameters("@EM0127").Value = EM01 + cantempe
                    Else
                        cmdCOHN.Parameters("@EM0127").Value = EM01
                    End If
                    If monthx = 2 Then
                        cmdCOHN.Parameters("@EM0227").Value = EM02 + cantempe
                    Else
                        cmdCOHN.Parameters("@EM0227").Value = EM02
                    End If
                    If monthx = 3 Then
                        cmdCOHN.Parameters("@EM0327").Value = EM03 + cantempe
                    Else
                        cmdCOHN.Parameters("@EM0327").Value = EM03
                    End If
                    If monthx = 4 Then
                        cmdCOHN.Parameters("@EM0427").Value = EM04 + cantempe
                    Else
                        cmdCOHN.Parameters("@EM0427").Value = EM04
                    End If
                    If monthx = 5 Then
                        cmdCOHN.Parameters("@EM0527").Value = EM05 + cantempe
                    Else
                        cmdCOHN.Parameters("@EM0527").Value = EM05
                    End If
                    If monthx = 6 Then
                        cmdCOHN.Parameters("@EM0627").Value = EM06 + cantempe
                    Else
                        cmdCOHN.Parameters("@EM0627").Value = EM06
                    End If
                    If monthx = 7 Then
                        cmdCOHN.Parameters("@EM0727").Value = EM07 + cantempe
                    Else
                        cmdCOHN.Parameters("@EM0727").Value = EM07
                    End If
                    If monthx = 8 Then
                        cmdCOHN.Parameters("@EM0827").Value = EM08 + cantempe
                    Else
                        cmdCOHN.Parameters("@EM0827").Value = EM08
                    End If
                    If monthx = 9 Then
                        cmdCOHN.Parameters("@EM0927").Value = EM09 + cantempe
                    Else
                        cmdCOHN.Parameters("@EM0927").Value = EM09
                    End If
                    If monthx = 10 Then
                        cmdCOHN.Parameters("@EM1027").Value = EM010 + cantempe
                    Else
                        cmdCOHN.Parameters("@EM1027").Value = EM010
                    End If
                    If monthx = 11 Then
                        cmdCOHN.Parameters("@EM1127").Value = EM011 + cantempe
                    Else
                        cmdCOHN.Parameters("@EM1127").Value = EM011
                    End If
                    If monthx = 12 Then
                        cmdCOHN.Parameters("@EM1227").Value = EM012 + cantempe
                    Else
                        cmdCOHN.Parameters("@EM1227").Value = EM012
                    End If
                    cmdCOHN.Parameters("@WK0127").Value = 0
                    cmdCOHN.Parameters("@WK0227").Value = 0
                    cmdCOHN.Parameters("@WK0327").Value = 0
                    cmdCOHN.Parameters("@WK0427").Value = 0
                    cmdCOHN.Parameters("@WK0527").Value = 0
                    cmdCOHN.Parameters("@WK0627").Value = 0
                    cmdCOHN.Parameters("@WK0727").Value = 0
                    cmdCOHN.Parameters("@WK0827").Value = 0
                    cmdCOHN.Parameters("@WK0927").Value = 0
                    cmdCOHN.Parameters("@WK1027").Value = 0
                    cmdCOHN.Parameters("@WK1127").Value = 0
                    cmdCOHN.Parameters("@WK1227").Value = 0
                    cmdCOHN.Parameters("@LMO#27").Value = monthx

                    ' Number of Week
                    Dim fechaRef2 As New Date(Mid(period, 1, 4), monthx, 1)
                    While fechaRef2.DayOfWeek <> DayOfWeek.Monday
                        fechaRef2 = fechaRef2.AddDays(1)
                    End While
                    Dim mesL As String = ""
                    Dim fecha2 As Date
                    Dim nWeek As Integer = -1
                    For i As Integer = 0 To 5
                        fecha2 = fechaRef2.AddDays(i * 7)
                        If fecha2.Month = monthx Then
                            nWeek = CInt(DatePart(DateInterval.WeekOfYear, fecha2, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFullWeek))
                        Else
                            Exit For
                        End If
                        mesL = nWeek
                    Next
                    cmdCOHN.Parameters("@LWK#27").Value = mesL
                    cmdCOHN.Parameters("@FILL27").Value = ""
                    Await cmdCOHN.ExecuteNonQueryAsync()
                    cmdCOHN.Dispose()

                End If
                rsch.Close()
                cmdch.Dispose()


            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function

    '******  Update RCWE Employer Contribution Work File ******
    Async Function UpdRCWE(EmprNo As String, EmprSub As String, Period As String, freq As String, totalins As Decimal, totalcontrs As Decimal) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()

                Dim rs As iDB2DataReader
                Dim cmdRCWE As New iDB2Command() With {
                .CommandText = "Select count(*) as cant  From ""QS36F"".""" & As400_lib & ".RCWE"" " &
                " Where Rreg10 = @Rreg10 And Rrsf10 = @Rrsf10 And ((Ccen10*100)+ Cony10) = @CONY And CONM10 = @CONM",
                .Connection = connection,
                .CommandTimeout = 0
                }
                cmdRCWE.DeriveParameters()
                cmdRCWE.Parameters("@Rreg10").iDB2Value = EmprNo
                cmdRCWE.Parameters("@Rrsf10").iDB2Value = EmprSub
                cmdRCWE.Parameters("@CONY").iDB2Value = Mid(Period, 1, 4)
                cmdRCWE.Parameters("@CONM").iDB2Value = Mid(Period, 5, 2)
                rs = Await cmdRCWE.ExecuteReaderAsync

                If rs.Read Then

                    If rs("cant") > 0 Then
                        Dim cmdpX As New iDB2Command With {
                         .CommandText = "UPDATE ""QS36F"".""" & As400_lib & ".RCWE"" SET Actv10= @Actv10, Bchi10 = @Bchi10, Bchc10 = @Bchc10, Cenr10 = @Cenr10, Datr10 = @Datr10, Edit10 =              @Edit10, User10 = @User10, Date10 = @Date10 Where Rreg10 = @Rreg10 And Rrsf10 = @Rrsf10  And((Ccen10*100)+ Cony10) = @CONY10 And CONM10 = @CONM10",
                         .Connection = connection,
                         .CommandTimeout = 0
                                            }
                        cmdpX.DeriveParameters()
                        cmdpX.Parameters("@Rreg10").iDB2Value = EmprNo
                        cmdpX.Parameters("@Rrsf10").iDB2Value = EmprSub
                        cmdpX.Parameters("@CONY10").iDB2Value = Mid(Period, 1, 4)
                        cmdpX.Parameters("@CONM10").iDB2Value = Mid(Period, 5, 2)
                        'UPDATE a
                        cmdpX.Parameters("@Actv10").Value = "D"
                        cmdpX.Parameters("@Bchi10").Value = totalins
                        cmdpX.Parameters("@Bchc10").Value = totalcontrs
                        cmdpX.Parameters("@Cenr10").Value = Now.Year \ 100
                        cmdpX.Parameters("@Datr10").Value = (Now.Year Mod 100) * 10000 + Now.Month * 100 + Now.Day
                        cmdpX.Parameters("@Edit10").Value = "P"
                        cmdpX.Parameters("@User10").Value = "UserID"
                        cmdpX.Parameters("@Date10").Value = (Now.Year Mod 100) * 10000 + Now.Month * 100 + Now.Day
                        Await cmdpX.ExecuteNonQueryAsync()
                        cmdpX.Dispose()
                    Else

                        '*************Insert RCWE Employer Contribution Work File ******

                        Dim cmd As New iDB2Command With {
                                .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".RCWE""
                                         (ACTV10, RREG10, RRSF10, CCEN10, CONY10, CONM10, TCON10, FINP10, INTP10, AMPD10, IWK110, IWK210, IWK310, IWK410, IWK510, IWK610, BCHI10, BCHC10, CENR10, DATR10, EDIT10, LOCK10, FREQ10, USER10, WSID10, DATE10, FILL10)
                                         VALUES(@ACTV10, @RREG10, @RRSF10, @CCEN10, @CONY10, @CONM10, @TCON10, @FINP10, @INTP10, @AMPD10, @IWK110, @IWK210, @IWK310, @IWK410, @IWK510, @IWK610, @BCHI10, @BCHC10, @CENR10, @DATR10, @EDIT10, @LOCK10, @FREQ10, @USER10, @WSID10, @DATE10, @FILL10)",
                                .Connection = connection,
                                .CommandTimeout = 0
                            }
                        cmd.DeriveParameters()

                        cmd.Parameters("@ACTV10").Value = "A"
                        cmd.Parameters("@RREG10").Value = EmprNo
                        cmd.Parameters("@RRSF10").Value = EmprSub
                        Dim centx = Mid(Period, 1, 4) \ 100
                        Dim yearx = Mid(Period, 1, 4) - (centx * 100)
                        cmd.Parameters("@CCEN10").Value = centx
                        cmd.Parameters("@CONY10").Value = yearx
                        cmd.Parameters("@CONM10").Value = Mid(Period, 5, 2)
                        cmd.Parameters("@TCON10").Value = 0.0
                        cmd.Parameters("@FINP10").Value = 0.0
                        cmd.Parameters("@INTP10").Value = 0.0
                        cmd.Parameters("@AMPD10").Value = 0.0

                        ' Number of Week
                        Dim mes1 As String = Mid(Period, 5, 2)
                        Dim cantidad As Integer = 0
                        Dim numberW = New List(Of String)
                        Dim fechaRef3 As New Date(Mid(Period, 1, 4), mes1, 1)

                        While fechaRef3.DayOfWeek <> DayOfWeek.Monday
                            fechaRef3 = fechaRef3.AddDays(1)
                            'Year(Date.Now)
                        End While

                        Dim fecha3 As Date
                        For i As Integer = 0 To 5
                            fecha3 = fechaRef3.AddDays(i * 7)
                            If fecha3.Month = mes1 Then
                                cantidad = cantidad + 1
                                Dim nWeek As Integer = -1
                                nWeek = CInt(DatePart(DateInterval.WeekOfYear, fecha3, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFullWeek))
                                numberW.Add(nWeek)
                            Else
                                Exit For
                            End If
                        Next
                        cmd.Parameters("@IWK110").Value = numberW(0)
                        cmd.Parameters("@IWK210").Value = numberW(1)
                        cmd.Parameters("@IWK310").Value = numberW(2)
                        cmd.Parameters("@IWK410").Value = numberW(3)
                        If cantidad = 5 Then
                            cmd.Parameters("@IWK510").Value = numberW(4)
                        Else
                            cmd.Parameters("@IWK510").Value = 0
                        End If
                        cmd.Parameters("@IWK610").Value = 0
                        cmd.Parameters("@BCHI10").Value = totalins
                        cmd.Parameters("@BCHC10").Value = totalcontrs
                        cmd.Parameters("@FREQ10").Value = freq
                        cmd.Parameters("@CENR10").Value = Now.Year \ 100
                        cmd.Parameters("@DATR10").Value = (Now.Year Mod 100) * 10000 + Now.Month * 100 + Now.Day
                        cmd.Parameters("@EDIT10").Value = "P"
                        cmd.Parameters("@LOCK10").Value = ""
                        cmd.Parameters("@USER10").Value = "UserID"
                        cmd.Parameters("@WSID10").Value = "W1"
                        cmd.Parameters("@DATE10").Value = (Now.Year Mod 100) * 10000 + Now.Month * 100 + Now.Day
                        cmd.Parameters("@FILL10").Value = ""

                        Await cmd.ExecuteNonQueryAsync
                        cmd.Dispose()
                    End If

                End If
                cmdRCWE.Dispose()
                rs.Close()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "COMPSTAT"
    '******** Update ARSUM Employer Accounts Receivable
    Async Function UpdArsum(EmprNo As String, EmprSub As String, Period As String, totalins As Decimal, totalcontrs As Decimal) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()

                Dim rs As iDB2DataReader
                Dim cmdARS As New iDB2Command() With {
                .CommandText = "select ERN104, CNT104, PMT104  from ""QS36F"".""" & As400_lib & ".ARSUM"" where REG104 = @rreg AND RSF104= @rrsf AND PRD104 = @PRD",
                .Connection = connection,
                .CommandTimeout = 0
                }
                cmdARS.DeriveParameters()
                cmdARS.Parameters("@rreg").iDB2Value = EmprNo
                cmdARS.Parameters("@rrsf").iDB2Value = EmprSub
                cmdARS.Parameters("@PRD").iDB2Value = Period
                rs = Await cmdARS.ExecuteReaderAsync

                If rs.Read Then
                    Dim ERN As Decimal = 0.0
                    Dim CNT As Decimal = 0.0
                    Dim PMT As Decimal = 0.0
                    Dim ERNx As Object = ERN
                    If (ERNx Is DBNull.Value) Then
                        ERN = 0
                    End If
                    Dim CNTx As Object = CNT
                    If (CNTx Is DBNull.Value) Then
                        CNT = 0
                    End If
                    Dim TOTx As Object = PMT
                    If (TOTx Is DBNull.Value) Then
                        PMT = 0
                    End If

                    Dim cmdpX As New iDB2Command With {
                             .CommandText = "UPDATE ""QS36F"".""" & As400_lib & ".ARSUM"" SET  ERN104 = @ERN104, CNT104 = @CNT104, ESE104 = @ESE104, ESP104 = @ESP104 Where REG104 = @rreg AND RSF104= @rrsf AND PRD104 = @PRD",
                             .Connection = connection,
                             .CommandTimeout = 0
                                                }
                    cmdpX.DeriveParameters()
                    cmdpX.Parameters("@rreg").iDB2Value = EmprNo
                    cmdpX.Parameters("@rrsf").iDB2Value = EmprSub
                    cmdpX.Parameters("@PRD").iDB2Value = Period
                    cmdpX.Parameters("@ERN104").Value = Math.Round(totalins, 2) + Math.Round(ERN, 2)
                    cmdpX.Parameters("@CNT104").Value = Math.Round(totalcontrs, 2) + Math.Round(CNT, 2)
                    cmdpX.Parameters("@ESE104").Value = 0.0
                    cmdpX.Parameters("@ESP104").Value = 0.0
                    ' cmdpX.Parameters("@PMT104").Value = TOTALCONT
                    Await cmdpX.ExecuteNonQueryAsync()
                    cmdpX.Dispose()

                Else

                    Dim cmdARSM As New iDB2Command With {
                                .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".ARSUM""
                                   (REG104, RSF104, PRD104, ERN104, CNT104, ESE104, ESP104, INT104, SUR104, ACI104, BAL104, PMT104, TOT104, FIN104, PEN104, ADJ104, FPD104, LPD104, PIT104)
                                  VALUES(@REG104, @RSF104, @PRD104, @ERN104, @CNT104, @ESE104, @ESP104, @INT104, @SUR104, @ACI104, @BAL104, @PMT104, @TOT104, @FIN104, @PEN104, @ADJ104, @FPD104, @LPD104, @PIT104)",
                                .Connection = connection,
                                .CommandTimeout = 0
                            }
                    cmdARSM.DeriveParameters()

                    cmdARSM.Parameters("@REG104").Value = EmprNo
                    cmdARSM.Parameters("@RSF104").Value = EmprSub

                    cmdARSM.Parameters("@PRD104").Value = Period And
                    cmdARSM.Parameters("@ERN104").Value = Math.Round(totalins, 2)
                    cmdARSM.Parameters("@CNT104").Value = Math.Round(totalcontrs, 2)
                    cmdARSM.Parameters("@ESE104").Value = 0.0
                    cmdARSM.Parameters("@ESP104").Value = 0.0
                    cmdARSM.Parameters("@INT104").Value = 0.0
                    cmdARSM.Parameters("@SUR104").Value = 0.0
                    cmdARSM.Parameters("@ACI104").Value = 0.0
                    cmdARSM.Parameters("@BAL104").Value = 0.0
                    cmdARSM.Parameters("@PMT104").Value = 0.0
                    cmdARSM.Parameters("@TOT104").Value = 0.00
                    cmdARSM.Parameters("@FIN104").Value = 0.0
                    cmdARSM.Parameters("@PEN104").Value = 0.0
                    cmdARSM.Parameters("@ADJ104").Value = 0.0
                    cmdARSM.Parameters("@FPD104").Value = 0
                    cmdARSM.Parameters("@LPD104").Value = 0
                    cmdARSM.Parameters("@PIT104").Value = 0.0
                    Await cmdARSM.ExecuteNonQueryAsync
                    cmdARSM.Dispose()
                End If
                cmdARS.Dispose()
                rs.Close()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function

    '******* Update AROP Employer Accounts Receivable
    Async Function UpdArop(EmprNo As String, EmprSub As String, Period As String, totalcontrs As Decimal) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()

                Dim rs As iDB2DataReader
                Dim cmdAROP As New iDB2Command() With {
                .CommandText = "Select CNTR12, MTOT12 from ""QS36F"".""" & As400_lib & ".AROP"" where ACTV12 = 'A' AND RREG12 = @rreg AND RRSF12 = @rrsf AND CCEN12 = @ccen AND CNYR12 = @cony AND CPER12 = @conm",
                .Connection = connection,
                .CommandTimeout = 0
                }

                cmdAROP.DeriveParameters()
                cmdAROP.Parameters("@rreg").iDB2Value = EmprNo
                cmdAROP.Parameters("@rrsf").iDB2Value = EmprSub
                Dim cent1 As Integer
                Dim year1 As Integer
                cent1 = Mid(Period, 1, 4) \ 100
                year1 = Mid(Period, 1, 4) - (cent1 * 100)
                cmdAROP.Parameters("@ccen").iDB2Value = cent1
                cmdAROP.Parameters("@cony").iDB2Value = year1
                cmdAROP.Parameters("@conm").iDB2Value = Mid(Period, 5, 2)
                rs = Await cmdAROP.ExecuteReaderAsync
                Dim CNTR As String = 0.00
                Dim MTOT As String = 0.00
                If rs.Read Then
                    CNTR = rs(0).ToString
                    MTOT = rs(1).ToString
                    Dim CNTRx As Object = CNTR
                    If (CNTRx Is DBNull.Value) Then
                        CNTR = 0.0
                    End If

                    Dim MTOTx As Object = MTOT
                    If (MTOTx Is DBNull.Value) Then
                        MTOT = 0.0
                    End If


                    Dim cmdupx As New iDB2Command With {
                             .CommandText = "UPDATE ""QS36F"".""" & As400_lib & ".AROP"" SET SUBC12 = @SUBC12, SUBD12 = @SUBD12, CNTR12 = @CNTR12, MTOT12 = @MTOT12 
                                             Where ACTV12 = 'A' AND RREG12 = @rreg AND RRSF12 = @rrsf AND CCEN12 = @ccen AND CNYR12 = @cony AND CPER12 = @conm",
                             .Connection = connection,
                             .CommandTimeout = 0
                                                }
                    cmdupx.DeriveParameters()
                    cmdupx.Parameters("@rreg").iDB2Value = EmprNo
                    cmdupx.Parameters("@rrsf").iDB2Value = EmprSub
                    cmdupx.Parameters("@ccen").iDB2Value = cent1
                    cmdupx.Parameters("@cony").iDB2Value = year1
                    cmdupx.Parameters("@conm").iDB2Value = Mid(Period, 5, 2)

                    cmdupx.Parameters("@SUBC12").Value = (Now.Year) \ 100
                    cmdupx.Parameters("@SUBD12").Value = (Now.Year Mod 100) * 10000 + Now.Month * 100 + Now.Day

                    Dim valor As Decimal = Convert.ToDecimal(CNTR)
                    cmdupx.Parameters("@CNTR12").Value = Math.Round(valor, 2) + Math.Round(totalcontrs, 2)
                    Dim valor1 As Decimal = Convert.ToDecimal(MTOT)
                    cmdupx.Parameters("@MTOT12").Value = Math.Round(valor1, 2) + Math.Round(totalcontrs, 2)

                    Await cmdupx.ExecuteNonQueryAsync()
                    cmdupx.Dispose()
                Else


                    Dim cmd As New iDB2Command With {
                                .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".AROP""
                                                      (ACTV12, RREG12, RRSF12, CCEN12, CNYR12, CPER12, SUBC12, SUBD12, CNTR12, PENL12, INTR12, FINE12, ADJM12,PAYM12, MTOT12, FILL12)
                                               VALUES (@ACTV, @RREG, @RRSF, @CCEN, @CONY, @CONM, @SUBC, @SUBD, @CNTR, @PENL, @INTR, @FINE, @ADJM, @PAYM, @MTOT, @FILL)",
                                .Connection = connection,
                                .CommandTimeout = 0
                            }
                    cmd.DeriveParameters()
                    cmd.Parameters("@ACTV").Value = "A"
                    cmd.Parameters("@RREG").Value = EmprNo
                    cmd.Parameters("@RRSF").Value = EmprSub
                    cmd.Parameters("@CCEN").Value = cent1
                    cmd.Parameters("@CONY").Value = year1
                    cmd.Parameters("@CONM").Value = Mid(Period, 5, 2)
                    cmd.Parameters("@SUBC").Value = Now.Year \ 100
                    cmd.Parameters("@SUBD").Value = (Now.Year Mod 100) * 10000 + Now.Month * 100 + Now.Day
                    cmd.Parameters("@CNTR").Value = Math.Round(totalcontrs, 2)
                    cmd.Parameters("@PENL").Value = 0.0
                    cmd.Parameters("@INTR").Value = 0.0
                    cmd.Parameters("@FINE").Value = 0.0
                    cmd.Parameters("@PAYM").Value = 0.0
                    cmd.Parameters("@ADJM").Value = 0.0
                    cmd.Parameters("@MTOT").Value = Math.Round(totalcontrs, 2)
                    cmd.Parameters("@FILL").Value = ""
                    Await cmd.ExecuteNonQueryAsync
                    cmd.Dispose()

                End If
                cmdAROP.Dispose()
                rs.Close()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function

    '***********Update ARITM  Employer A/R detail*********************
    Async Function UpdAritm(EmprNo As String, EmprSub As String, Period As String, totalins As Decimal, totalcontrs As Decimal) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()

                Dim rs As iDB2DataReader
                Dim cmdARITM As New iDB2Command() With {
                .CommandText = "select ERN103, CNT103, SUR103  FROM ""QS36F"".""" & As400_lib & ".ARITM"" WHERE REG103 = @rreg AND RSF103= @rrsf AND TXD103 = @TXD103 AND PRD103 = @PRD",
                .Connection = connection,
                .CommandTimeout = 0
                }

                cmdARITM.DeriveParameters()
                cmdARITM.Parameters("@rreg").iDB2Value = EmprNo
                cmdARITM.Parameters("@rrsf").iDB2Value = EmprSub
                cmdARITM.Parameters("@PRD").iDB2Value = Period
                cmdARITM.Parameters("@TXD103").iDB2Value = (Now.Year) * 10000 + Now.Month * 100 + Now.Day
                rs = Await cmdARITM.ExecuteReaderAsync
                Dim ERNt As Decimal = 0.0
                Dim CNTt As Decimal = 0.0
                Dim PMTt As Decimal = 0.0

                If rs.Read Then

                    ERNt = rs("ERN103")
                    CNTt = rs("CNT103")
                    PMTt = rs("SUR103")
                    Dim ERNx As Object = ERNt
                    If (ERNx Is DBNull.Value) Then
                        ERNt = 0
                    End If
                    Dim CNTx As Object = CNTt
                    If (CNTx Is DBNull.Value) Then
                        CNTt = 0
                    End If

                    Dim TOTx As Object = PMTt
                    If (TOTx Is DBNull.Value) Then
                        PMTt = 0
                    End If

                    Dim cmdpx As New iDB2Command With {
                             .CommandText = "UPDATE ""QS36F"".""" & As400_lib & ".ARITM"" SET ERN103 = @ERN103, CNT103 = @CNT103,SUR103= @SUR103 Where REG103 = @rreg AND RSF103= @rrsf AND PRD103 = @PRD AND TXD103 = @TXD103",
                             .Connection = connection,
                             .CommandTimeout = 0
                                                }
                    cmdpx.DeriveParameters()
                    cmdpx.Parameters("@rreg").iDB2Value = EmprNo
                    cmdpx.Parameters("@rrsf").iDB2Value = EmprSub
                    cmdpx.Parameters("@PRD").iDB2Value = Period
                    cmdpx.Parameters("@TXD103").iDB2Value = (Now.Year) * 10000 + Now.Month * 100 + Now.Day
                    cmdpx.Parameters("@ERN103").Value = Math.Round(totalins, 2) + Math.Round(ERNt, 2)
                    cmdpx.Parameters("@CNT103").Value = Math.Round(totalcontrs, 2) + Math.Round(CNTt, 2)
                    cmdpx.Parameters("@SUR103").Value = Math.Round(totalcontrs, 2) / 10 + Math.Round(PMTt, 2)
                    Await cmdpx.ExecuteNonQueryAsync()
                    cmdpx.Dispose()
                Else


                    Dim cmd As New iDB2Command With {
                                .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".ARITM""
                                  (REG103, RSF103, PRD103, TYP103, TXD103, SEQ103, ERN103, CNT103, PMT103, CNP103, INT103, SUR103, FIN103, PEN103, ADJ103, EST103)
                                 VALUES(@REG103, @RSF103, @PRD103, @TYP103, @TXD103, @SEQ103, @ERN103, @CNT103, @PMT103, @CNP103, @INT103, @SUR103, @FIN103, @PEN103, @ADJ103, @EST103)",
                                .Connection = connection,
                                .CommandTimeout = 0
                            }
                    cmd.DeriveParameters()
                    cmd.Parameters("@REG103").Value = EmprNo
                    cmd.Parameters("@RSF103").Value = EmprSub
                    cmd.Parameters("@PRD103").Value = Period
                    cmd.Parameters("@TYP103").Value = "C"
                    cmd.Parameters("@TXD103").Value = (Now.Year) * 10000 + Now.Month * 100 + Now.Day
                    cmd.Parameters("@SEQ103").Value = 1
                    cmd.Parameters("@ERN103").Value = Math.Round(totalins, 2)
                    cmd.Parameters("@CNT103").Value = Math.Round(totalcontrs, 2)
                    cmd.Parameters("@PMT103").Value = 0.0
                    cmd.Parameters("@CNP103").Value = 0.0
                    cmd.Parameters("@INT103").Value = 0.0
                    cmd.Parameters("@SUR103").Value = Math.Round(totalcontrs, 2) / 10
                    cmd.Parameters("@FIN103").Value = 0.00
                    cmd.Parameters("@PEN103").Value = 0.0
                    cmd.Parameters("@ADJ103").Value = 0.0
                    cmd.Parameters("@EST103").Value = ""

                    Await cmd.ExecuteNonQueryAsync
                    cmd.Dispose()

                End If
                cmdARITM.Dispose()
                rs.Close()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function

    '*********** Update STAT Remittance Statistics File**********************
    Async Function UpdSTAT(EmprNo As String, EmprSub As String, Period As String, cantempe As Integer) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()

                Dim rs As iDB2DataReader
                Dim cmdStat As New iDB2Command() With {
                .CommandText = "select count(*) as cant from ""QS36F"".""" & As400_lib & ".STAT"" where ACTV34 = 'A' AND RREG34 = @rreg AND RRSF34 = @rrsf AND CCEN34 = @ccen AND CONY34 = @cony AND CONM34 = @conm",
                .Connection = connection,
                .CommandTimeout = 0
                }
                cmdStat.DeriveParameters()
                cmdStat.Parameters("@rreg").iDB2Value = EmprNo
                cmdStat.Parameters("@rrsf").iDB2Value = EmprSub
                Dim centx = Mid(Period, 1, 4) \ 100
                Dim yearx = Mid(Period, 1, 4) - (centx * 100)
                cmdStat.Parameters("@ccen").iDB2Value = centx
                cmdStat.Parameters("@cony").iDB2Value = yearx
                cmdStat.Parameters("@conm").iDB2Value = Mid(Period, 5, 2)
                rs = Await cmdStat.ExecuteReaderAsync
                If rs.Read Then
                    If rs("cant") = 0 Then

                        Dim CMDTEXT As String = "INSERT INTO ""QS36F"".""" & As400_lib & ".STAT""
                                        (ACTV34, RREG34, RRSF34, CCEN34, CONY34, CONM34, CORS34, ORGS34, SUBC34, SUBD34, USER34, KEYC34, KEYD34, COMM34, LIN#34, FILL34)
                                          VALUES(@ACTV, @RREG, @RRSF, @CCEN, @CONY, @CONM, @CORS, @ORGS, @SUBC, @SUBD, @USER, @KEYC, @KEYD, @COMM, @LIN#, @FILL)"

                        Dim cmdus As New iDB2Command() With {
                        .CommandText = CMDTEXT,
                        .Connection = connection,
                        .CommandTimeout = 0
                        }
                        cmdus.DeriveParameters()
                        cmdus.Parameters("@ACTV").Value = "A"
                        cmdus.Parameters("@RREG").Value = EmprNo
                        cmdus.Parameters("@RRSF").Value = EmprSub
                        cmdus.Parameters("@CCEN").Value = centx
                        cmdus.Parameters("@CONY").Value = yearx
                        cmdus.Parameters("@CONM").Value = Mid(Period, 5, 2)
                        cmdus.Parameters("@CORS").Value = ""
                        cmdus.Parameters("@ORGS").Value = "PST"
                        cmdus.Parameters("@SUBC").Value = Now.Year \ 100
                        cmdus.Parameters("@SUBD").Value = (Now.Year Mod 100) * 10000 + Now.Month * 100 + Now.Day
                        cmdus.Parameters("@USER").Value = "UserID"
                        cmdus.Parameters("@KEYC").Value = 0.0
                        cmdus.Parameters("@KEYD").Value = 0.0
                        cmdus.Parameters("@COMM").Value = ""
                        cmdus.Parameters("@LIN#").Value = cantempe
                        cmdus.Parameters("@FILL").Value = ""
                        cmdus.ExecuteNonQuery()
                        cmdus.Dispose()
                        Await cmdus.ExecuteNonQueryAsync()
                        cmdus.Dispose()
                    End If
                End If
                rs.Close()
                cmdStat.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function

#End Region

End Class
