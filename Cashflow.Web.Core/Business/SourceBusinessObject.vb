Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Data
Imports SISSA.Cashflow.Web.Core.Entities

Namespace Business

    Public Class SourceBusinessObject

        Dim _SourceDAO As SourceDataObject

        Public Function GetDepositosTransactionsTable(FechaInicial As Date, FechaFinal As Date) As List(Of AcreditacionEntityObject)
            _SourceDAO = New SourceDataObject()
            Return _SourceDAO.GetDepositosTransactionsTable(FechaInicial, FechaFinal)
        End Function

        Public Function GetRetirosTransactionsTable(FechaInicial As Date, FechaFinal As Date) As List(Of RecoleccionEntityObject)
            _SourceDAO = New SourceDataObject()
            Return _SourceDAO.GetRetirosTransactionsTable(FechaInicial, FechaFinal)
        End Function

    End Class

End Namespace