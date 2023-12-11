Option Explicit On
Option Strict Off

Namespace Entities

    Public Class ConnectionClientEntityObject

        Public Property ConnectionId As Int32
        Public Property RFC As String
        Public Property NombreComercial As String
        Public Property DatabaseServer As String
        Public Property DatabaseUser As String
        Public Property DatabasePassword As String
        Public Property DatabaseName As String

        Private _connectionString As String
        Public ReadOnly Property ConnectionString As String
            Get
                Return String.Format("Data Source={0}; Initial Catalog={1};User ID={2};Password={3};", DatabaseServer, DatabaseName, DatabaseUser, DatabasePassword)
            End Get
        End Property

        Public Sub New()
            ConnectionId = 0
            RFC = String.Empty
            NombreComercial = String.Empty
            DatabaseServer = String.Empty
            DatabaseUser = String.Empty
            DatabasePassword = String.Empty
            DatabaseName = String.Empty
        End Sub

    End Class

End Namespace