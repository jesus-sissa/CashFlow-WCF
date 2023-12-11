Option Explicit On
Option Strict Off

Namespace Entities
    Public Class TransaccionLogEntityObject
        Public Property IdCajero As String
        Public Property IdUsuario As Int32
        Public Property ClaveSucursal As String
        Public Property Fecha As Date
        Public Property Hora As String
        Public Property IdDescripcion As Int32
        Public Property IdPantalla As Int32
        Public Property Descripcion As String

        Public Sub New()
            IdCajero = String.Empty
            IdUsuario = 0
            ClaveSucursal = String.Empty
            Fecha = Date.Now
            Hora = Date.Now.ToShortTimeString
            IdDescripcion = 0
            IdPantalla = 0
            Descripcion = String.Empty
        End Sub

    End Class

End Namespace
