Public Class DB2ConnectionS

    Public Shared as400 As String = "DataSource=192.168.210.10; CheckConnectionOnOpen=True; DataCompression=True; UserID=spalacio;                                  Password=sara1985; Connect Timeout=0"

    ' Shared enviroment As String = "Test"
    Shared enviroment As String = "Live"

    Public Shared Function As400_lib() As String
        Return If(enviroment = "Test", "TT", If(enviroment = "Live", "NI", ""))
    End Function

End Class
