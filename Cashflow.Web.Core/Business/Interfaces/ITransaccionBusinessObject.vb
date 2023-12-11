Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Entities

Namespace Business.Interfaces

    Public Interface ITransaccionBusinessObject

        Property ConnectionString As String
        Function InsertTransaccion(RFC As String, TransaccionInfo As TransaccionEntityObject) As String
        Function GetTransaccionesNoSincronizadasList() As List(Of TransaccionEntityObject)
        Function GetTransaccionesNoSincronizadasTable() As DataTable
        Function UpdateEstatusSincronizado(TransaccionInfo) As Integer
    End Interface

End Namespace