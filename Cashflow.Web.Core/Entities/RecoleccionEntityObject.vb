Option Explicit On
Option Strict Off

Namespace Entities

    Public Class RecoleccionEntityObject

        Public Property Fecha As Date
        Public Property Fecha_Transmision As DateTime
        Public Property Fecha_Acreditacion As DateTime
        Public Property Cliente As String
        Public Property Cajero As String
        Public Property Folio As Integer
        Public Property Importe As Decimal
        Public Property Archivo As String

        Public Property Depositos As List(Of RecoleccionDetailEntityObject)

        Public Property Mensages As List(Of RecoleccionResponseEntityObject)

        Public Sub New()
            Fecha = DateTime.Now
            Fecha_Transmision = DateTime.Now
            Fecha_Acreditacion = DateTime.Now
            Cliente = String.Empty
            Cajero = String.Empty
            Folio = 0
            Importe = 0
            Archivo = String.Empty
            Depositos = New List(Of RecoleccionDetailEntityObject)()
            Mensages = New List(Of RecoleccionResponseEntityObject)()
        End Sub

    End Class

    Public Class RecoleccionDetailEntityObject
        Public Property ID As Decimal
        Public Property Id_Recoleccion As Decimal
        Public Property Clave_Cliente As String
        Public Property Clave_Sucursal As String
        Public Property Clave_Banco As String
        Public Property CRC As String
        Public Property Cuenta As String
        Public Property Divisa As String
        Public Property Tipo As String
        Public Property Monto As Decimal

        Public Property Remision As String

        Public Sub New()
            ID = 0
            Id_Recoleccion = 0
            Clave_Cliente = String.Empty
            Clave_Sucursal = String.Empty
            Clave_Banco = String.Empty
            CRC = String.Empty
            Cuenta = String.Empty
            Divisa = String.Empty
            Tipo = String.Empty
            Monto = 0
            Remision = String.Empty
        End Sub

    End Class

    Public Class RecoleccionResponseEntityObject
        Public Property MessageReturn As String

        Public Sub New()
            MessageReturn = String.Empty
        End Sub
    End Class

End Namespace