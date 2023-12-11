Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Data
Imports SISSA.Cashflow.Web.Core.Entities
Imports SISSA.Cashflow.Web.Core.Business.Interfaces

Namespace Business
    'd

    Public Class TransaccionBusinessObject
        Implements ITransaccionBusinessObject

        Private _TransaccionDAO As TransaccionDataObject

#Region "Propiedades"
        Private Property _connectionString As String

        Public Property ConnectionString As String Implements ITransaccionBusinessObject.ConnectionString
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
            _TransaccionDAO = New TransaccionDataObject()
            _TransaccionDAO.ConnectionString = _connectionString
        End Sub
#End Region

#Region "Methods"
        Public Function InsertTransaccion(RFC As String, TransaccionInfo As TransaccionEntityObject) As String Implements ITransaccionBusinessObject.InsertTransaccion
            Throw New NotImplementedException()
        End Function

        Public Function GetTransaccionesNoSincronizadasList() As List(Of TransaccionEntityObject) Implements ITransaccionBusinessObject.GetTransaccionesNoSincronizadasList
            Throw New NotImplementedException()
        End Function

        Public Function GetTransaccionesNoSincronizadasTable() As DataTable Implements ITransaccionBusinessObject.GetTransaccionesNoSincronizadasTable
            Throw New NotImplementedException()
        End Function

        Public Function UpdateEstatusSincronizado(TransaccionInfo As Object) As Integer Implements ITransaccionBusinessObject.UpdateEstatusSincronizado
            Throw New NotImplementedException()
        End Function
#End Region

#Region "Functions"

#End Region

    End Class
End Namespace