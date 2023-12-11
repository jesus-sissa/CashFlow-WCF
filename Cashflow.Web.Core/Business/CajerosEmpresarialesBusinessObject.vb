Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Data
Imports SISSA.Cashflow.Web.Core.Entities
Imports SISSA.Cashflow.Web.Core.Business.Interfaces

Namespace Business

    Public Class CajerosEmpresarialesBusinessObject

        Private _CajerosEmpresarialesDAL As CajerosEmpresarialesDataObject

#Region "Propiedades"

        Private Property _connectionString As String
        Public ReadOnly Property ConnectionString As String
            Get
                Return _CajerosEmpresarialesDAL.ConnectionString
            End Get
        End Property
#End Region

#Region "Constructor"
        Public Sub New()
            _CajerosEmpresarialesDAL = New CajerosEmpresarialesDataObject()
        End Sub

        Public Sub New(ConnectionString As String)
            _connectionString = ConnectionString
            _CajerosEmpresarialesDAL = New CajerosEmpresarialesDataObject()
            _CajerosEmpresarialesDAL.ConnectionString = _connectionString
        End Sub
#End Region

#Region "Methods"
        Public Function GetTransaccionesDepositos(Filter As CajerosEmpresarialesFilterEntityObject) As DataTable
            Return _CajerosEmpresarialesDAL.GetTransaccionesDepositos(Filter)
        End Function

        Public Function GetTransaccionesRetiros(Filter As CajerosEmpresarialesFilterEntityObject) As DataTable
            Return _CajerosEmpresarialesDAL.GetTransaccionesRetiros(Filter)
        End Function

        Public Function GetAcreditacionesTransmitidas(Filter As CajerosEmpresarialesFilterEntityObject) As DataTable
            Return _CajerosEmpresarialesDAL.GetAcreditacionesTransmitidas(Filter)
        End Function

        Public Function GetBaseCajerosReceptores(Filter As CajerosEmpresarialesFilterEntityObject) As DataTable
            Return _CajerosEmpresarialesDAL.GetBaseCajerosReceptores(Filter)
        End Function

        Public Function GetMonitorArchivos(Filter As CajerosEmpresarialesFilterEntityObject) As DataTable
            Return _CajerosEmpresarialesDAL.GetMonitorArchivos(Filter)
        End Function

        Public Function GetMonitorArchivosDetalle(AcreditacionID As Int32) As DataTable
            Return _CajerosEmpresarialesDAL.GetMonitorArchivosDetalle(AcreditacionID)
        End Function

        Public Function SincronizaTransaccion(RFC As String, TransaccionesInfo As TransaccionEntityObject) As Integer
            Return _CajerosEmpresarialesDAL.SincronizaTransaccion(RFC, TransaccionesInfo)
        End Function

        Public Function GetReporteDepositosRecolecciones(Filter As CajerosEmpresarialesFilterEntityObject) As DataTable
            Return _CajerosEmpresarialesDAL.GetReporteDepositosRecolecciones(Filter)
        End Function

        Public Function ProcesaTransaccionesAcreditables(Fecha As Date) As DataTable
            Return _CajerosEmpresarialesDAL.ProcesaTransaccionesAcreditables(Fecha)
        End Function

        Public Function ArqueoTransitoSaldoInicial(Fecha As Date, Optional CRC As String = "") As DataTable
            Return _CajerosEmpresarialesDAL.ArqueoTransitoSaldoInicial(Fecha, CRC)
        End Function

        Public Function ArqueoTransitoAcreditaciones(Fecha As Date, Optional CRC As String = "") As DataTable
            Return _CajerosEmpresarialesDAL.ArqueoTransitoAcreditaciones(Fecha, CRC)
        End Function

        Public Function ArqueoTransitoRecolecciones(Fecha As Date, Optional CRC As String = "") As DataTable
            Return _CajerosEmpresarialesDAL.ArqueoTransitoRecolecciones(Fecha, CRC)
        End Function

        Public Function ArqueoTransitoGeneral(Fecha As Date, Optional CRC As String = "") As DataTable
            Return _CajerosEmpresarialesDAL.ArqueoTransitoGeneral(Fecha, CRC)
        End Function

        Public Function RollbackUpdateAcreditacion(NIdarchivo As Int32, NArchivo As String, NComentario As String) As UInt16
            Return _CajerosEmpresarialesDAL.RollbackUpdateAcreditacion(NIdarchivo, NArchivo, NComentario)
        End Function

        Public Function SucessAcreditacion(NIdarchivo As Int32, NArchivo As String, NComentario As String) As UInt16
            Return _CajerosEmpresarialesDAL.SucessAcreditacion(NIdarchivo, NArchivo, NComentario)
        End Function
#End Region

#Region "Functions"

#End Region
    End Class

End Namespace