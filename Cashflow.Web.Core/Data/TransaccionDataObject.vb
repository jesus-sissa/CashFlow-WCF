Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Entities
Imports SISSA.Cashflow.Web.Core.Data.Interfaces
Imports System.Data.SqlClient
Imports System.Text

Namespace Data

    Public Class TransaccionDataObject
        Inherits BaseDataObject
        Implements ITransaccionDataObject

        Public Function InsertTransaccion(RFC As String, TransaccionInfo As TransaccionEntityObject) As String Implements ITransaccionDataObject.InsertTransaccion
            Throw New NotImplementedException()
        End Function

        Public Function GetTransaccionNoSincronizadasList() As List(Of TransaccionEntityObject) Implements ITransaccionDataObject.GetTransaccionesNoSincronizadasList
            Throw New NotImplementedException()
        End Function

        Public Function GetTransaccionNoSincronizadasTable() As DataTable Implements ITransaccionDataObject.GetTransaccionesNoSincronizadasTable
            Throw New NotImplementedException()
        End Function

        Public Function UpdateEstatusSincronizado(TransaccionInfo As Object) As Integer Implements ITransaccionDataObject.UpdateEstatusSincronizado
            Throw New NotImplementedException()
        End Function
    End Class

End Namespace