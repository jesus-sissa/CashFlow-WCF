Option Explicit On
Option Strict Off

Namespace Entities

    Public Class CajerosEmpresarialesFilterEntityObject

        Public Property FechaInicial As Date
        Public Property FechaFinal As Date

        Public Property Cajero As String
        Public Property Boveda As String
        Public Property FechaTransmision As Date
        Public Property ClaveCliente As String
        Public Property ClaveSucursal As String

        Public Property Tipo As String
        Public Property Estatus As String
        Public Property Cuenta As String
        Public Property Divisa As String

        Public Sub New()
            FechaInicial = Date.Today
            FechaFinal = Date.Today
            Cajero = String.Empty
            Boveda = String.Empty
            FechaTransmision = Nothing
            ClaveCliente = String.Empty
            ClaveSucursal = String.Empty
            Tipo = String.Empty
            Cuenta = String.Empty
            Divisa = String.Empty
        End Sub
    End Class

End Namespace