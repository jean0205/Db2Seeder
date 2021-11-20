
Imports IBM.Data.DB2.iSeries
Imports ShareModels.Models.Benefit_Claims
Public Class SicknessDB2
    Dim cn = DB2ConnectionS.as400
    Dim As400_lib = DB2ConnectionS.As400_lib

    Async Function InsertSickness(Agepension As Document_AgeBenefit) As Task(Of Integer)

        Dim ClaimNo As Integer
        Try

            ClaimNo = Await GenerarClaimNo()
            '     Await InsertBenf(Agepension, ClaimNo)


        Catch ex As iDB2Exception
            Throw ex
        End Try

        Return ClaimNo
    End Function
End Class
