Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Data
Imports SISSA.Cashflow.Web.Core.Entities

Namespace Business

    Public Class ClienteBusinessObject

        Private _ClienteDAO As ClienteDataObject

#Region "Properties"
        Private Property _connectionString As String

        Public Property ConnectionString As String
            Get
                Return _connectionString
            End Get
            Set(value As String)
                _connectionString = value
                If _ClienteDAO IsNot Nothing Then
                    _ClienteDAO.ConnectionString = _connectionString
                End If
            End Set
        End Property
#End Region

#Region "Constructor"
        Public Sub New()
            _ClienteDAO = New ClienteDataObject()
        End Sub

        Public Sub New(ConnectionString As String)
            _connectionString = ConnectionString
            _ClienteDAO = New ClienteDataObject()
            _ClienteDAO.ConnectionString = _connectionString
        End Sub
#End Region

#Region "Methods"
        Public Function GetCatalogoClientes() As DataTable
            Return _ClienteDAO.GetCatalogoClientes
        End Function
        Public Function GetCatalogoSucursales()
            Return _ClienteDAO.GetCatalogoSucursales()
        End Function
        Public Function GetCatalogoSucursalesByCliente(ClaveCliente As String)
            Return _ClienteDAO.GetCatalogoSucursalesByCliente(ClaveCliente)
        End Function
#End Region

    End Class

End Namespace