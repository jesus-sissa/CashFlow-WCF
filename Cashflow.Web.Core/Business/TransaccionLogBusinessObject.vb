Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Data
Imports SISSA.Cashflow.Web.Core.Entities

Namespace Business
    Public Class TransaccionLogBusinessObject

        Private _TransaccionLogDAO As TransaccionLogDataObject

#Region "Propiedades"
        Private Property _connectionString As String

        Public Property ConnectionString As String
            Get
                Return _connectionString
            End Get
            Set(value As String)
                _connectionString = value
            End Set
        End Property
#End Region

#Region "Constructor"
        Public Sub New(ConnectionString As String)
            _connectionString = ConnectionString
            _TransaccionLogDAO = New TransaccionLogDataObject()
            _TransaccionLogDAO.ConnectionString = _connectionString
        End Sub
#End Region

#Region "Methods"
        Public Function InsertTransaccionLog(TransaccionLogInfo As TransaccionLogEntityObject) As String
            Return _TransaccionLogDAO.InsertTransaccionLog(TransaccionLogInfo)
        End Function
#End Region
    End Class

End Namespace
