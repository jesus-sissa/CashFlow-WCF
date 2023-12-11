Option Explicit On
Option Strict Off

Namespace Entities

    Public Class TransaccionEntityObject
        Public Property IdTransaccion As Int32
        Public Property ClaveSucursal As String
        Public Property ClaveCajero As String
        Public Property ClaveCliente As String
        Public Property TipoMovimiento As String

        Public Property Fecha As DateTime
        Public Property HoraInicio As TimeSpan
        Public Property HoraFin As TimeSpan
        Public Property ImporteTotal As Decimal
        Public Property Status As Char
        Public Property IdRetiro As Int32
        Public Property UsuarioRegistro As String
        Public Property TotalMXN As Decimal
        Public Property TotalUSD As Decimal
        Public Property TotalUSDConvert As Decimal
        Public Property TipoCambio As Decimal
        Public Property Cargo As Decimal
        Public Property Abono As Decimal

        Public Property Observaciones As String
        Public Property NumeroRemision As Double
        Public Property NumeroEnvase As String
        Public Property ImporteOtros As Decimal
        Public Property ImporteOtrosD As Decimal

        Public Property Folio As String
        Public Property Tipo As Integer
        Public Property IdCorte As Int32
        Public Property EsEfectivo As Boolean
        Public Property IdCaja As Int32
        Public Property ClaveCaja As String
        Public Property NumeroCuenta As String
        Public Property Referencia As String
        Public Property Divisa As String
        Public Property Acreditado As Boolean

        Public Property Movimientos As List(Of TransaccionDetailEntityObject)
        Public Property rowID As String

        Public Sub New()
            IdTransaccion = 0
            ClaveSucursal = String.Empty
            ClaveCajero = String.Empty
            ClaveCliente = String.Empty
            TipoMovimiento = String.Empty
            Fecha = DateTime.Now
            HoraInicio = TimeSpan.Zero
            HoraFin = TimeSpan.Zero
            ImporteTotal = 0
            Status = "F"
            IdRetiro = 0
            UsuarioRegistro = String.Empty
            TotalMXN = 0
            TotalUSD = 0
            TotalUSDConvert = 0
            TipoCambio = 0
            Folio = 0
            Cargo = 0
            Abono = 0
            Tipo = 0
            IdCorte = 0
            EsEfectivo = False
            IdCaja = 0
            ClaveCaja = String.Empty
            NumeroCuenta = String.Empty
            Referencia = String.Empty
            Divisa = String.Empty
            Acreditado = False
            Movimientos = New List(Of TransaccionDetailEntityObject)()
            rowID = String.Empty

            Observaciones = String.Empty
            NumeroRemision = 0
            NumeroEnvase = String.Empty
            ImporteOtros = 0
            ImporteOtrosD = 0
        End Sub
    End Class

    Public Class TransaccionDetailEntityObject
        Public Property ID As Integer
        Public Property IdTransaccion As Int32
        Public Property ClaveSucursal As String
        Public Property SerieCasete As String
        Public Property ClaveDenominacion As String
        Public Property Denominacion As Decimal
        Public Property CantidadPiezas As Integer
        Public Property Importe As Decimal
        Public Property ClaveMoneda As String
        Public Property SerieValidador As String
        Public Property NumeroValidador As Int32

        Public Sub New()
            ID = 0
            IdTransaccion = 0
            ClaveSucursal = String.Empty
            SerieCasete = String.Empty
            ClaveDenominacion = String.Empty
            Denominacion = 0
            CantidadPiezas = 0
            Importe = 0
            ClaveMoneda = String.Empty
            SerieValidador = String.Empty
            NumeroValidador = 0
        End Sub
    End Class

End Namespace