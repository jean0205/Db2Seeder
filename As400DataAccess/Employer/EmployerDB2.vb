﻿Imports IBM.Data.DB2.iSeries
Imports ShareModels.Models

Public Class EmployerDB2
    Dim cn = DB2ConnectionS.as400
    Dim As400_lib = DB2ConnectionS.As400_lib

    Async Function InsertEmployers(Empr As Document_Employer) As Task(Of Integer)

        Dim EmprNo As Integer = 0
        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()

                Dim cmdEMPR As New iDB2Command With {
                    .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".EMPR"" 
                                         (ACTV02, RREG02, RRSF02, BNAM02, RNAM02, RADD02, RTWN02, RPCD02, RRGN02, RTNO02, BADD02, BTWN02, 
                                          BPCD02, BRGN02, BTNO02, NOME02, NOFE02, CENI02, DATI02, TCEN02, TDAT02, SECT02, INDC02, USER02, 
                                          CRDT02, OFFR02, ZONE02, GRAD02, STDT02, FAXN02, DORM02, DORD02, EML102, EML202, PDS102, PHN102, 
                                          PDS202, PHN202, CNM102, CPT102, CPH102, CEX102, CML102, CNM202, CPT202, CPH202, CEX202, CMM202, STMD02)
                                        VALUES ('A', @rreg, @rrsf, @bnam, @rnam, @radd, @rtwn, @rpcd, @rrgn, @rtno, @badd, @btwn, @bpcd, @brgn, 
                                          @btno, @nome, @nofe, @ceni, @dati, @tcen, @tdat, @sect, @indc, @user, @crdt, @offr, @zone, @grad, 
                                          @stdt, @faxn, @dorm, @dord, @eml1, @eml2, @pds1, @phn1, @pds2, @phn2, @cnm1, @cpt1, @cph1, @cex1, 
                                          @cml1, @cnm2, @cpt2, @cph2, @cex2, @cmm2, @stmd)",
                    .Connection = connection,
                    .CommandTimeout = 0
                }

                cmdEMPR.DeriveParameters()

                EmprNo = Await GenerarEmployerNo()

                cmdEMPR.Parameters("@rreg").Value = EmprNo
                cmdEMPR.Parameters("@rrsf").Value = ""
                ' Business Name
                cmdEMPR.Parameters("@bnam").Value = Empr.firmName
                'Employer Name
                cmdEMPR.Parameters("@rnam").Value = Empr.employerName
                'Employer Address
                cmdEMPR.Parameters("@radd").Value = ""
                'Employer town
                cmdEMPR.Parameters("@rtwn").Value = ""
                'Employer Postal code
                cmdEMPR.Parameters("@rpcd").Value = ""
                'Employer Area
                cmdEMPR.Parameters("@rrgn").Value = ""
                'Employer phone

                cmdEMPR.Parameters("@rtno").Value = If(Empr.mobile = Nothing, 0, Empr.mobile)
                ' Business address
                cmdEMPR.Parameters("@badd").Value = Empr.businessAddress
                ' Business Town
                cmdEMPR.Parameters("@btwn").Value = Empr.businessTown
                ' Business Postal Code
                cmdEMPR.Parameters("@bpcd").Value = "Postal Code"
                ' Business Area
                cmdEMPR.Parameters("@brgn").Value = Empr.businessParish
                ' Business phone
                cmdEMPR.Parameters("@btno").Value = If(Empr.businessPhone = Nothing, 0, Empr.businessPhone)

                'Type of Business
                cmdEMPR.Parameters("@indc").Value = 0

                ' Number of Employees
                cmdEMPR.Parameters("@nome").Value = Empr.maleEmployee
                cmdEMPR.Parameters("@nofe").Value = Empr.femaleEmployee

                'Entry Date

                cmdEMPR.Parameters("@ceni").Value = Now.Year \ 100
                cmdEMPR.Parameters("@dati").Value = (Now.Year Mod 100) * 10000 + Now.Month * 100 + Now.Day

                'Termination Date
                cmdEMPR.Parameters("@tcen").Value = 0
                cmdEMPR.Parameters("@tdat").Value = 0

                'Sector
                cmdEMPR.Parameters("@sect").Value = ""
                cmdEMPR.Parameters("@user").Value = "USER"

                'Creation date
                cmdEMPR.Parameters("@crdt").Value = Now.Year * 10000 + Now.Month * 100 + Now.Day

                'Inspector
                cmdEMPR.Parameters("@offr").Value = "000"

                'Zone
                cmdEMPR.Parameters("@zone").Value = ""
                'Grade
                cmdEMPR.Parameters("@grad").Value = ""

                'Business Commenced
                Dim BusComm As Date = Empr.businessCommencedDate
                cmdEMPR.Parameters("@stdt").Value = BusComm.Year * 10000 + BusComm.Month * 100 + BusComm.Day

                ' Business fax
                cmdEMPR.Parameters("@faxn").Value = If(Empr.fax = Nothing, 0, Empr.fax)
                ' Dorman
                cmdEMPR.Parameters("@dorm").Value = ""
                'Dorman date 

                cmdEMPR.Parameters("@dord").Value = 0
                'Business Email
                cmdEMPR.Parameters("@eml1").Value = Empr.emailAddress
                'Employer Email
                cmdEMPR.Parameters("@eml2").Value = ""
                'extra phone business
                cmdEMPR.Parameters("@pds1").Value = 0
                cmdEMPR.Parameters("@phn1").Value = If(Empr.secondMobile = Nothing, 0, Empr.secondMobile)

                'extra phone empr
                cmdEMPR.Parameters("@pds2").Value = 0
                cmdEMPR.Parameters("@phn2").Value = 0

                'Contact #1 Information
                'Name
                cmdEMPR.Parameters("@cnm1").Value = ""
                'posetion
                cmdEMPR.Parameters("@cpt1").Value = ""
                'phone
                cmdEMPR.Parameters("@cph1").Value = 0
                'ext.
                cmdEMPR.Parameters("@cex1").Value = 0
                'email
                cmdEMPR.Parameters("@cml1").Value = ""

                ' Contact #2 Information
                'name
                cmdEMPR.Parameters("@cnm2").Value = ""
                cmdEMPR.Parameters("@cpt2").Value = ""
                cmdEMPR.Parameters("@cph2").Value = 0
                cmdEMPR.Parameters("@cex2").Value = 0
                cmdEMPR.Parameters("@cmm2").Value = ""

                'Statement Start
                ' If Dtstmd.CustomFormat = " " Then
                cmdEMPR.Parameters("@stmd").Value = 0

                'cmdEMPR.Parameters("@stmd").Value = Dtstmd.Value.Year * 10000 + Dtstmd.Value.Month * 100 + Dtstmd.Value.Day



                If EmprNo <> 0 Then
                    Await cmdEMPR.ExecuteNonQueryAsync
                    Await InsertEmprExtr(EmprNo)
                    Await InsertBankInformationEmpr(Empr, EmprNo)
                End If

                cmdEMPR.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

        Return EmprNo
    End Function


    Async Function GenerarEmployerNo() As Task(Of Integer)
        Dim EmprNo As Integer = 0
        Try
            Using connection As New iDB2Connection(cn)
                Dim cmd As New iDB2Command()

                connection.Open()
                Dim rs As iDB2DataReader
                cmd.CommandText = "Select * FROM ""QS36F"".""" & As400_lib & ".CONS""                                
                                WHERE CKEY01 LIKE 'Z  100' "
                cmd.Connection = connection
                cmd.CommandTimeout = 0
                cmd.DeriveParameters()
                rs = Await cmd.ExecuteReaderAsync
                If rs.Read Then
                    Dim EMPRNUMBER As String
                    EMPRNUMBER = Checkdigitcalc(Convert.ToInt32(rs("DATA01").ToString.Substring(0, 9)) + 10).ToString
                    EmprNo = CInt(EMPRNUMBER)

                    'UPDATE CONS FILE
                    Dim cmdupd As New iDB2Command With {
                        .CommandText = "UPDATE ""QS36F"".""" & As400_lib & ".CONS""                                
                                SET DATA01 = @DATA WHERE CKEY01 LIKE @CKEY ",
                        .Connection = connection,
                        .CommandTimeout = 0
                    }
                    cmdupd.DeriveParameters()
                    Dim newemprno As String = EMPRNUMBER
                    While newemprno.Length < 9
                        newemprno = "0" + newemprno
                    End While

                    cmdupd.Parameters("@DATA").Value = newemprno
                    cmdupd.Parameters("@CKEY").Value = "Z  100"
                    Await cmdupd.ExecuteNonQueryAsync()
                    cmdupd.Dispose()

                End If
                cmd.Dispose()
                rs.Close()
            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

        Return EmprNo
    End Function

    Async Function InsertBankInformationEmpr(Empr As Document_Employer, Emprn As Integer) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                'insert in ni.xport for datacard
                Dim cmd As New iDB2Command() With {
                .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".ACCEMPR"" 
                                                 (ACTV1, EMPR1, EMPS1, BANK1, ACCN1, ACCTYP1, VEND1)
                                          VALUES (@ACTV, @EMPR, @EMPS, @BANK, @ACCN, @ACCTYP, @VEND)",
                .Connection = connection,
                .CommandTimeout = 0
                }

                cmd.DeriveParameters()
                cmd.Parameters("@ACTV").Value = "A"
                cmd.Parameters("@EMPR").Value = Emprn
                cmd.Parameters("@EMPS").Value = "0"
                cmd.Parameters("@BANK").Value = Empr.bank
                cmd.Parameters("@ACCN").Value = Empr.accountNo
                cmd.Parameters("@ACCTYP").Value = Empr.accountType
                cmd.Parameters("@VEND").Value = "0"

                Await cmd.ExecuteNonQueryAsync()
                cmd.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function


    Async Function InsertEmprExtr(Emprn As Integer) As Task

        Try
            Using connection As New iDB2Connection(cn)
                connection.Open()
                'insert in ni.xport for datacard
                Dim cmd As New iDB2Command() With {
                .CommandText = "INSERT INTO ""QS36F"".""" & As400_lib & ".EXTR"" 
                                               (ACT202, REG202, RSF202, REM202) VALUES  ('A', @reg2, @rsf2, @rem2)",
                .Connection = connection,
                .CommandTimeout = 0
                }

                cmd.DeriveParameters()
                cmd.Parameters("@reg2").Value = Emprn
                cmd.Parameters("@rsf2").Value = "0"
                cmd.Parameters("@rem2").Value = "E"

                Await cmd.ExecuteNonQueryAsync()
                cmd.Dispose()

            End Using
        Catch ex As iDB2Exception
            Throw ex
        End Try

    End Function
End Class
