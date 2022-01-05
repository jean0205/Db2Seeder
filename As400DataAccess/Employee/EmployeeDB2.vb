Imports IBM.Data.DB2.iSeries
Imports ShareModels.Models

Public Class EmployeeDB2
    Dim cn = DB2ConnectionS.as400
    Dim As400_lib = DB2ConnectionS.As400_lib
    Dim WebCache As New WebPortalDB
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
    Async Function InsertEmployees(Empe As Document_Employee) As Task(Of Integer)

        Dim NisNumber As Integer = 0
        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()

                Dim cmd As New iDB2Command With {
                    .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".EMPE"" 
                                              (ACTV03, EREG03, LNAM03, FNAM03, MNAM03, ESEX03, EADD03, ETWN03, REGN03, EPCD03,
                                               PDS103, PHN103, PDS203, PHN203, PLOB03, NATL03, MRTL03, AFTN03, CENB03, DATB03,
                                               CEND03, DATD03, CENI03, DATI03, VERB03, VERM03, PROV03, RBEN03, TREG03, ADD103,
                                               ADD203, ADD303, ADD403, EML103, EML203, CRTB03, CRTD03, TOTC03, AFTD03)
                                         VALUES(@ACTV, @EREG, @LNAM, @FNAM, @MNAM, @ESEX, @EADD, @ETWN, @REGN, @EPCD,
                                               @PDS1, @PHN1, @PDS2, @PHN2, @PLOB, @NATL, @MRTL, @AFTN, @CENB, @DATB,
                                               @CEND, @DATD, @CENI, @DATI, @VERB, @VERM, @PROV, @RBEN, @TREG, @ADD1,
                                               @ADD2, @ADD3, @ADD4, @EML1, @EML2, @CRTB, @CRTD, @TOTC , @AFTD)",
                    .Connection = connection,
                    .CommandTimeout = 0
                }

                cmd.DeriveParameters()

                NisNumber = Await GenerarNIS()

                cmd.Parameters("@ACTV").Value = "A"
                cmd.Parameters("@EREG").Value = NisNumber
                cmd.Parameters("@LNAM").Value = Empe.lastName.ToUpper
                cmd.Parameters("@FNAM").Value = Empe.firstName.ToUpper
                cmd.Parameters("@MNAM").Value = If(Empe.middleName = Nothing, "", Empe.middleName.ToUpper)

                'Sex
                Select Case Empe.sex
                    Case 1
                        cmd.Parameters("@ESEX").Value = "M"
                    Case 2
                        cmd.Parameters("@ESEX").Value = "F"
                End Select

                cmd.Parameters("@EADD").Value = Empe.address.ToUpper
                cmd.Parameters("@ETWN").Value = Empe.town.ToUpper
                cmd.Parameters("@REGN").Value = Empe.parish.ToUpper

                'Telephone #
                cmd.Parameters("@EPCD").Value = If(Empe.homePhoneNumber = Nothing, 0, Empe.homePhoneNumber)
                'mobile
                cmd.Parameters("@PDS1").Value = "mobile"
                cmd.Parameters("@PHN1").Value = If(Empe.primaryMobileNumber = Nothing, 0, Empe.primaryMobileNumber)
                ' Tele work
                cmd.Parameters("@PDS2").Value = "work"
                cmd.Parameters("@PHN2").Value = If(Empe.businessPhoneNumber = Nothing, 0, Empe.businessPhoneNumber)

                'Place Of Birth
                cmd.Parameters("@PLOB").Value = "Place Of Birth".ToUpper
                cmd.Parameters("@NATL").Value = Empe.nationality.ToUpper

                'Marital Status
                Select Case Empe.maritalStatus
                    Case 1
                        cmd.Parameters("@MRTL").Value = "S"
                    Case 2
                        cmd.Parameters("@MRTL").Value = "M"
                    Case 3
                        cmd.Parameters("@MRTL").Value = "D"
                    Case 4
                        cmd.Parameters("@MRTL").Value = "W"
                End Select

                cmd.Parameters("@AFTN").Value = 0

                '  Date of birth
                Dim DOB As Date = Empe.dateOfBirth
                cmd.Parameters("@CENB").Value = DOB.Year \ 100
                cmd.Parameters("@DATB").Value = (DOB.Year Mod 100) * 10000 + DOB.Month * 100 + DOB.Day

                'verificacion 
                cmd.Parameters("@VERB").Value = "V"

                '  cmd.Parameters("@VERB").Value = " "


                cmd.Parameters("@VERM").Value = "V"

                'cmd.Parameters("@VERM").Value = " "


                'Date of death
                cmd.Parameters("@CEND").Value = 0
                cmd.Parameters("@DATD").Value = 0

                'Date of Entrance
                cmd.Parameters("@CENI").Value = Now.Year \ 100
                cmd.Parameters("@DATI").Value = (Now.Year Mod 100) * 10000 + Now.Month * 100 + Now.Day

                'pfund.Checked Then
                cmd.Parameters("@PROV").Value = "N"

                'Chkrbenf.Checked Then
                cmd.Parameters("@RBEN").Value = " "
                cmd.Parameters("@TREG").Value = 0

                'Extra Address
                cmd.Parameters("@ADD1").Value = ""
                cmd.Parameters("@ADD2").Value = ""
                cmd.Parameters("@ADD3").Value = ""
                cmd.Parameters("@ADD4").Value = ""

                cmd.Parameters("@EML1").Value = If(Empe.emailAddress = Nothing, "", Empe.emailAddress)
                cmd.Parameters("@EML2").Value = ""

                cmd.Parameters("@CRTB").Value = Empe.CompletedBy.ToUpper
                cmd.Parameters("@CRTD").Value = Now.Year * 10000 + Now.Month * 100 + Now.Day
                cmd.Parameters("@TOTC").Value = 0

                cmd.Parameters("@AFTD").Value = 0
                ' 
                If NisNumber <> 0 Then
                    Await cmd.ExecuteNonQueryAsync
                    Await InsertDataCard(Empe, NisNumber)
                    Await InsertEmpeAuxilliary(Empe, NisNumber)
                    Await InsertBankInformation(Empe, NisNumber)
                    Await WebCache.NewEmployee(NisNumber)
                End If

                cmd.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

        Return NisNumber
    End Function
    Async Function GenerarNIS() As Task(Of Integer)
        Dim NISRegister As Integer = 0
        Try
            Using connection As New iDB2Connection(cn)
                Dim cmd As New iDB2Command()

                connection.Open()
                Dim rs As iDB2DataReader
                cmd.CommandText = "Select * FROM ""QS36F"".""" & As400_lib & ".CONS""                                
                                WHERE CKEY01 LIKE 'Z  005' "
                cmd.Connection = connection
                cmd.CommandTimeout = 0
                cmd.DeriveParameters()
                rs = Await cmd.ExecuteReaderAsync
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
                    Await cmdupd.ExecuteNonQueryAsync()
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
    Async Function InsertDataCard(Empe As Document_Employee, Nisn As Integer) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                'insert in ni.xport for datacard
                Dim cmdINSXPORT As New iDB2Command() With {
                .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".XPORT"" 
                               (NISNUM, FNAME, LNAME, SEX, DOB, NATL, PLOB, ISSUED, CARDPRINT, LASTMOD, PHOTO, SIGNATURE) 
                                VALUES (@nisnum , @fname,@lname, @sex, @dob, @natl, @plob, date('01/01/0001'), @cardprint, @lastmod, @photo, @signature)",
                .Connection = connection,
                .CommandTimeout = 0
                }


                cmdINSXPORT.DeriveParameters()
                Dim nistxt As String
                nistxt = Nisn.ToString
                While nistxt.Length < 9
                    nistxt = "0" + nistxt
                End While
                cmdINSXPORT.Parameters("@nisnum").Value = nistxt
                cmdINSXPORT.Parameters("@fname").Value = Empe.firstName.ToUpper
                cmdINSXPORT.Parameters("@lname").Value = Empe.lastName.ToUpper
                'Sex
                Select Case Empe.sex
                    Case 1
                        cmdINSXPORT.Parameters("@sex").Value = "MALE"
                    Case 2
                        cmdINSXPORT.Parameters("@sex").Value = "FEMALE"
                End Select

                Dim DtDOB As Date = Empe.dateOfBirth
                cmdINSXPORT.Parameters("@dob").Value = DtDOB.Day.ToString + "/" + DtDOB.Month.ToString + "/" + DtDOB.Year.ToString
                cmdINSXPORT.Parameters("@natl").Value = Empe.nationality.ToUpper
                cmdINSXPORT.Parameters("@plob").Value = "Place Of Birth"
                cmdINSXPORT.Parameters("@cardprint").Value = 0
                cmdINSXPORT.Parameters("@lastmod").Value = ""
                cmdINSXPORT.Parameters("@photo").Value = ""
                cmdINSXPORT.Parameters("@signature").Value = ""
                Await cmdINSXPORT.ExecuteNonQueryAsync()
                cmdINSXPORT.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function
    Async Function InsertEmpeAuxilliary(Empe As Document_Employee, Nisn As Integer) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                'insert in ni.xport for datacard
                Dim CMDINSEAUX As New iDB2Command() With {
                .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".EAUX"" (EREG05, CENM05, DATM05) VALUES(@EREG, @CENM, @DATM)",
                .Connection = connection,
                .CommandTimeout = 0
                }

                CMDINSEAUX.DeriveParameters()
                Dim nistxt As String
                nistxt = Nisn.ToString
                CMDINSEAUX.Parameters("@EREG").Value = nistxt

                If Empe.dateOfMarriage Is Nothing Then
                    CMDINSEAUX.Parameters("@CENM").Value = 0
                    CMDINSEAUX.Parameters("@DATM").Value = 0
                Else
                    Dim DtDoM As Date = Empe.dateOfMarriage
                    CMDINSEAUX.Parameters("@CENM").Value = DtDoM.Year \ 100
                    CMDINSEAUX.Parameters("@DATM").Value = (DtDoM.Year Mod 100) * 10000 + DtDoM.Month * 100 + DtDoM.Day
                End If

                Await CMDINSEAUX.ExecuteNonQueryAsync()
                CMDINSEAUX.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function
    Async Function InsertBankInformation(Empe As Document_Employee, Nisn As Integer) As Task

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
                cmd.Parameters("@EMPE").Value = Nisn
                cmd.Parameters("@CLMN").Value = "0"
                cmd.Parameters("@BANK").Value = Empe.bank
                cmd.Parameters("@ACCN").Value = Empe.accountNumber
                cmd.Parameters("@ACCTYP").Value = Empe.accountType
                Await cmd.ExecuteNonQueryAsync()
                cmd.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function


End Class
