Imports IBM.Data.DB2.iSeries
Imports ShareModels.Models

Public Class ElectRemittanceDB2
    Dim cn = DB2ConnectionS.as400
    Dim As400_lib = DB2ConnectionS.As400_lib

    Async Function UpdEmnfile(EmpeCntr As EmployeeContributionRecord, EmprRemitt As Document_Remittance) As Task

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

                Dim strCadena As String
                Dim intPos As Integer
                Dim EmprNo As String
                Dim EmprSub As String
                strCadena = EmprRemitt.employerNumber
                intPos = InStr(1, strCadena, "-") 'posicion de la "-"
                EmprNo = Mid(strCadena, 1, intPos - 1)
                EmprSub = Mid(strCadena, intPos + 1)
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
End Class
