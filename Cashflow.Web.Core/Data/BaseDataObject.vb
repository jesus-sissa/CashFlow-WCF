Option Explicit On
Option Strict Off
Imports System.Data.SqlClient

Namespace Data

    Public Class BaseDataObject
        Protected _ConnectionString As String

        Public Property ConnectionString As String
            Get
                Return _ConnectionString
            End Get
            Set(ByVal value As String)
                _ConnectionString = value
            End Set
        End Property

        Public Sub New()
            _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConexionCentral").ConnectionString
        End Sub

        Public Function ExecuteNonQuery(ByVal CommandText As String, ByVal ParamNames As String(), ByVal ParamValues As Object()) As Integer
            Dim conn As SqlConnection
            Dim cmd As SqlCommand
            Dim res As Integer
            conn = New SqlConnection(ConnectionString)
            conn.Open()
            cmd = New SqlCommand(CommandText, conn)
            cmd.CommandTimeout = 480

            If ParamNames IsNot Nothing Then
                SetCommandParameters(cmd, ParamNames, ParamValues)
            End If

            res = cmd.ExecuteNonQuery()
            conn.Close()
            If conn IsNot Nothing Then conn.Dispose()
            If cmd IsNot Nothing Then cmd.Dispose()
            Return res
        End Function

        Public Function ExecuteScalar(ByVal CommandText As String, ByVal ParamNames As String(), ByVal ParamValues As Object()) As Object
            Dim conn As SqlConnection
            Dim cmd As SqlCommand
            Dim obj As Object
            conn = New SqlConnection(ConnectionString)
            conn.Open()
            cmd = New SqlCommand(CommandText, conn)
            cmd.CommandTimeout = 480

            If ParamNames IsNot Nothing Then
                SetCommandParameters(cmd, ParamNames, ParamValues)
            End If

            obj = cmd.ExecuteScalar()
            conn.Close()
            If conn IsNot Nothing Then conn.Dispose()
            If cmd IsNot Nothing Then cmd.Dispose()
            Return obj
        End Function

        Public Function ExecuteDataTable(ByVal CommandText As String, ByVal ParamNames As String(), ByVal ParamValues As Object()) As DataTable
            Dim conn As SqlConnection
            Dim cmd As SqlCommand
            Dim da As SqlDataAdapter
            Dim dt As DataTable = New DataTable()

            conn = New SqlConnection(ConnectionString)
            conn.Open()

            cmd = New SqlCommand(CommandText, conn)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 480

            If ParamNames IsNot Nothing Then
                SetCommandParameters(cmd, ParamNames, ParamValues)
            End If

            da = New SqlDataAdapter(cmd)
            da.Fill(dt)
            conn.Close()

            If conn IsNot Nothing Then conn.Dispose()
            If cmd IsNot Nothing Then cmd.Dispose()
            If da IsNot Nothing Then da.Dispose()
            Return dt
        End Function

        Public Function ExecuteStoreProcedureNonQuery(ByVal StoreProcedure As String, ByVal ParamNames As String(), ByVal ParamValues As Object()) As Integer
            Dim conn As SqlConnection
            Dim cmd As SqlCommand
            Dim res As Integer
            conn = New SqlConnection(ConnectionString)
            conn.Open()

            cmd = New SqlCommand(StoreProcedure, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 480

            If ParamNames IsNot Nothing Then
                SetCommandParameters(cmd, ParamNames, ParamValues)
            End If

            res = cmd.ExecuteNonQuery()
            conn.Close()
            If conn IsNot Nothing Then conn.Dispose()
            If cmd IsNot Nothing Then cmd.Dispose()
            Return res
        End Function

        Public Function ExecuteStoreProcedureScalar(ByVal StoreProcedure As String, ByVal ParamNames As String(), ByVal ParamValues As Object(), Optional Transaction As SqlTransaction = Nothing) As Object
            Dim conn As SqlConnection
            Dim cmd As SqlCommand
            Dim obj As Object

            conn = New SqlConnection(ConnectionString)
            conn.Open()

            cmd = New SqlCommand(StoreProcedure, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 480

            If ParamNames IsNot Nothing Then
                SetCommandParameters(cmd, ParamNames, ParamValues)
            End If

            obj = cmd.ExecuteScalar()
            conn.Close()
            If conn IsNot Nothing Then conn.Dispose()
            If cmd IsNot Nothing Then cmd.Dispose()
            Return obj
        End Function

        Public Function ExecuteStoreProcedureDatatable(ByVal StoreProcedure As String, ByVal ParamNames As String(), ByVal ParamValues As Object(), Optional Transaction As SqlTransaction = Nothing) As DataTable
            Dim conn As SqlConnection
            Dim cmd As SqlCommand
            Dim da As SqlDataAdapter
            Dim dt As DataTable = New DataTable()

            conn = New SqlConnection(ConnectionString)
            conn.Open()

            cmd = New SqlCommand(StoreProcedure, conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 480

            If ParamNames IsNot Nothing Then
                SetCommandParameters(cmd, ParamNames, ParamValues)
            End If

            da = New SqlDataAdapter(cmd)
            da.Fill(dt)
            conn.Close()

            If conn IsNot Nothing Then conn.Dispose()
            If cmd IsNot Nothing Then cmd.Dispose()
            If da IsNot Nothing Then da.Dispose()
            Return dt
        End Function

        Protected Sub SetCommandParameters(ByVal Command As SqlCommand, ByVal ParamNames As String(), ByVal ParamValues As Object())
            '//Se elimina los parametros existentes
            Command.Parameters.Clear()

            '//Se valida la correspondincia entre parametros y valores
            If ParamNames.Length = ParamValues.Length Then
                '//Se anexa el listado de parametros y sus correspondientes valores
                For i As Integer = 0 To ParamNames.Length - 1
                    Command.Parameters.AddWithValue(ParamNames(i), ParamValues(i))
                Next
            End If
        End Sub
    End Class


End Namespace