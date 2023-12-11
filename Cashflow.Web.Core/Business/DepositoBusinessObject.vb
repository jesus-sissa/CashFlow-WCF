Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Data
Imports SISSA.Cashflow.Web.Core.Entities
Imports SISSA.Cashflow.Web.Core.Business.Interfaces

Namespace Business

    Public Class DepositoBusinessObject
        Implements ITransaccionBusinessObject

        Private _DespositoDAO As DepositoDataObject

#Region "Properties"
        Private Property _connectionString As String

        Public Property ConnectionString As String Implements ITransaccionBusinessObject.ConnectionString
            Get
                Return _connectionString
            End Get
            Set(value As String)
                _connectionString = value
                If _DespositoDAO IsNot Nothing Then
                    _DespositoDAO.ConnectionString = _connectionString
                End If
            End Set
        End Property
#End Region

#Region "Constructor"
        Public Sub New()
            _DespositoDAO = New DepositoDataObject()
        End Sub

        Public Sub New(ConnectionString As String)
            _connectionString = ConnectionString
            _DespositoDAO = New DepositoDataObject()
            _DespositoDAO.ConnectionString = _connectionString
        End Sub
#End Region

#Region "Methods"
        Public Function InsertTransaccion(RFC As String, TransaccionInfo As TransaccionEntityObject) As String Implements ITransaccionBusinessObject.InsertTransaccion
            Return _DespositoDAO.InsertTransaccion(RFC, TransaccionInfo)
        End Function

        Public Function UpdateReferenciaAndEstatus(Deposito As TransaccionEntityObject) As String
            Return _DespositoDAO.UpdateReferenciaAndEstatus(Deposito)
        End Function

        'Public Function UpdateReferenciaAndEstatus(Depositos As List(Of TransaccionEntityObject)) As String
        '    If Depositos IsNot Nothing AndAlso Depositos.Count > 0 Then
        '        For Each deposito As TransaccionEntityObject In Depositos
        '            _DespositoDAO.UpdateReferenciaAndEstatus(deposito)
        '        Next
        '    End If
        'End Function

        Public Function GetTransaccionesNoSincronizadasList() As List(Of TransaccionEntityObject) Implements ITransaccionBusinessObject.GetTransaccionesNoSincronizadasList
            Return _DespositoDAO.GetTransaccionesNoSincronizadasList()
        End Function
        Public Function GetTransaccionesNoSincronizadasTable() As DataTable Implements ITransaccionBusinessObject.GetTransaccionesNoSincronizadasTable
            Return _DespositoDAO.GetTransaccionesNoSincronizadasTable()
        End Function

        Public Function UpdateEstatusSincronizado(TransaccionInfo As Object) As Integer Implements ITransaccionBusinessObject.UpdateEstatusSincronizado
            Return _DespositoDAO.UpdateEstatusSincronizado(TransaccionInfo)
        End Function
#End Region

    End Class

End Namespace