Imports System.Data.SqlClient
Imports System.Threading.Tasks

Public Class WebPortalDB
    Dim conString As String = My.Settings.Webportal
    Async Function NewEmployee(nis As Integer) As Task(Of Task())
        Using connection As New SqlConnection(conString)
            Dim cmd As New SqlCommand("_NewEmployee", connection)
            cmd.CommandTimeout = 0
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@EREG03", SqlDbType.NVarChar).Value = nis
            Try
                connection.Open()
                Await cmd.ExecuteNonQueryAsync
            Catch ex As Exception
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Async Function NewEmployer(employerNo As Integer, subNo As Integer) As Task(Of Task())
        Using connection As New SqlConnection(conString)
            Dim cmd As New SqlCommand("_NewEmployer", connection)
            cmd.CommandTimeout = 0
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@RREG02", SqlDbType.NVarChar).Value = employerNo
            cmd.Parameters.AddWithValue("@RRSF02", SqlDbType.NVarChar).Value = subNo
            Try
                connection.Open()
                Await cmd.ExecuteNonQueryAsync
            Catch ex As Exception
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Async Function UpdateEmployee(nis As Integer) As Task(Of Task())
        Using connection As New SqlConnection(conString)
            Dim cmd As New SqlCommand("_UpdateEmployee", connection)
            cmd.CommandTimeout = 0
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@EREG03", SqlDbType.NVarChar).Value = nis
            Try
                connection.Open()
                Await cmd.ExecuteNonQueryAsync
            Catch ex As Exception
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Async Function UpdateEmployer(employerNo As Integer, subNo As Integer) As Task(Of Task())
        Using connection As New SqlConnection(conString)
            Dim cmd As New SqlCommand("_UpdateEmployer", connection)
            cmd.CommandTimeout = 0
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@RREG02", SqlDbType.NVarChar).Value = employerNo
            cmd.Parameters.AddWithValue("@RRSF02", SqlDbType.NVarChar).Value = subNo
            Try
                connection.Open()
                Await cmd.ExecuteNonQueryAsync
            Catch ex As Exception
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Async Function UpdateContributionEmprByPeriod(employerNo As Integer, subNo As Integer, period As Integer) As Task(Of Task())
        Using connection As New SqlConnection(conString)
            Dim cmd As New SqlCommand("_UpdateContributionEmprPeriod", connection)
            cmd.CommandTimeout = 0
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@RREG06", SqlDbType.NVarChar).Value = employerNo
            cmd.Parameters.AddWithValue("@RRSF06", SqlDbType.NVarChar).Value = subNo
            cmd.Parameters.AddWithValue("@PERIOD", SqlDbType.NVarChar).Value = period
            Try
                connection.Open()
                Await cmd.ExecuteNonQueryAsync
            Catch ex As Exception
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Async Function UpdateContributionByEmployee(nis As Integer) As Task(Of Task())
        Using connection As New SqlConnection(conString)
            Dim cmd As New SqlCommand("_UpdateContributionEmpe", connection)
            cmd.CommandTimeout = 0
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@EREG06", SqlDbType.NVarChar).Value = nis
            Try
                connection.Open()
                Await cmd.ExecuteNonQueryAsync
            Catch ex As Exception
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Async Function UpdateContributionByEmployer(employerNo As Integer, subNo As Integer) As Task(Of Task())
        Using connection As New SqlConnection(conString)
            Dim cmd As New SqlCommand("_UpdateContributionEmpr", connection)
            cmd.CommandTimeout = 0
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@RREG06", SqlDbType.NVarChar).Value = employerNo
            cmd.Parameters.AddWithValue("@RRSF06", SqlDbType.NVarChar).Value = subNo
            Try
                connection.Open()
                Await cmd.ExecuteNonQueryAsync
            Catch ex As Exception
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function


End Class
