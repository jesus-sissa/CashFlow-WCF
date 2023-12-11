Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Data
Imports SISSA.Cashflow.Web.Core.Entities
Imports SISSA.Cashflow.Web.Core.Business.Interfaces

Namespace Business

    Public Class RetiroBusinessObject
        Implements ITransaccionBusinessObject

        Private _RetiroDAO As RetiroDataObject

#Region "Properties"
        Private Property _connectionString As String

        Public Property ConnectionString As String Implements ITransaccionBusinessObject.ConnectionString
            Get
                Return _connectionString
            End Get
            Set(value As String)
                _connectionString = value
                If _RetiroDAO IsNot Nothing Then
                    _RetiroDAO.ConnectionString = _connectionString
                End If
            End Set
        End Property
#End Region

#Region "Constructor"
        Public Sub New()
            _RetiroDAO = New RetiroDataObject
        End Sub

        Public Sub New(ConnectionString As String)
            _connectionString = ConnectionString
            _RetiroDAO = New RetiroDataObject
            _RetiroDAO.ConnectionString = _connectionString
        End Sub
#End Region

#Region "Methods"
        Public Function InsertTransaccion(RFC As String, TransaccionInfo As TransaccionEntityObject) As String Implements ITransaccionBusinessObject.InsertTransaccion
            Return _RetiroDAO.InsertTransaccion(RFC, TransaccionInfo)
        End Function

        Public Function GetTransaccionesNoSincronizadasList() As List(Of TransaccionEntityObject) Implements ITransaccionBusinessObject.GetTransaccionesNoSincronizadasList
            Return _RetiroDAO.GetTransaccionesNoSincronizadasList()
        End Function
        Public Function GetTransaccionesNoSincronizadasTable() As DataTable Implements ITransaccionBusinessObject.GetTransaccionesNoSincronizadasTable
            Return _RetiroDAO.GetTransaccionesNoSincronizadasTable()
        End Function

        Public Function UpdateEstatusSincronizado(TransaccionInfo As Object) As Integer Implements ITransaccionBusinessObject.UpdateEstatusSincronizado
            Return _RetiroDAO.UpdateEstatusSincronizado(TransaccionInfo)
        End Function
#End Region

    End Class

End Namespace