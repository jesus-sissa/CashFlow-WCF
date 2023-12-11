Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Entities
Imports SISSA.Cashflow.Web.Core.Data.Interfaces
Imports System.Data.SqlClient
Imports System.Text

Namespace Data

    Public Class CajerosEmpresarialesDataObject
        Inherits BaseDataObject

        Public Sub New()
            Me.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConexionCajerosEmpresariales").ConnectionString
        End Sub

        Public Function GetTransaccionesDepositos(Filter As CajerosEmpresarialesFilterEntityObject) As DataTable
            Dim storeProcedure As String = "CE_GetTransaccionesDepositos"
            Dim dt As DataTable

            Dim _filter As String
            Dim params() As String
            Dim values As Object

            Try
                _filter = CreateFilterTransaccionSentence(Filter)
                params = New String() {"@dinamic_filter"}
                values = New Object() {_filter}

                dt = ExecuteStoreProcedureDatatable(storeProcedure, params, values)
                Return dt

            Catch ex As Exception

            End Try
        End Function

        Public Function GetTransaccionesRetiros(Filter As CajerosEmpresarialesFilterEntityObject) As DataTable
            Dim storeProcedure As String = "CE_GetTransaccionesRetiros"
            Dim dt As DataTable

            Dim _filter As String
            Dim params() As String
            Dim values As Object

            Try
                _filter = CreateFilterTransaccionSentence(Filter)
                params = New String() {"@dinamic_filter"}
                values = New Object() {_filter}

                dt = ExecuteStoreProcedureDatatable(storeProcedure, params, values)
                Return dt

            Catch ex As Exception

            End Try
        End Function

        Public Function GetAcreditacionesTransmitidas(Filter As CajerosEmpresarialesFilterEntityObject) As DataTable
            Dim storeProcedure As String = "CE_GetAcreditacionesTransmitidas"
            Dim dt As DataTable

            Dim _filter As String
            Dim params() As String
            Dim values As Object

            Try
                _filter = CreateFilterAcreditacionesTransmitidas(Filter)
                params = New String() {"@dinamic_filter"}
                values = New Object() {_filter}

                dt = ExecuteStoreProcedureDatatable(storeProcedure, params, values)
                Return dt

            Catch ex As Exception

            End Try
        End Function

        Public Function GetBaseCajerosReceptores(Filter As CajerosEmpresarialesFilterEntityObject) As DataTable
            Dim storeProcedure As String = "CE_GetBaseCajerosReceptores"
            Dim dt As DataTable

            Dim _filter As String
            Dim params() As String
            Dim values As Object

            Try
                _filter = CreateFilterBaseCajeros(Filter)
                params = New String() {"@dinamic_filter"}
                values = New Object() {_filter}

                dt = ExecuteStoreProcedureDatatable(storeProcedure, params, values)
                Return dt

            Catch ex As Exception

            End Try
        End Function

        Public Function GetMonitorArchivos(Filter As CajerosEmpresarialesFilterEntityObject) As DataTable
            Dim storeProcedure As String = "CE_GetMonitorArchivos"
            Dim dt As DataTable

            Dim _filter As String
            Dim params() As String
            Dim values As Object

            Try
                _filter = CreateFilterMonitorArchivos(Filter)
                params = New String() {"@FECHA_I", "@FECHA_F", "@CRC"}
                values = New Object() {Filter.FechaInicial, Filter.FechaFinal, Filter.Boveda}

                dt = ExecuteStoreProcedureDatatable(storeProcedure, params, values)
                Return dt

            Catch ex As Exception

            End Try
        End Function

        Public Function GetMonitorArchivosDetalle(AcreditacionID As Int32) As DataTable
            Dim storeProcedure As String = "CE_GetMonitorArchivosDetalle"
            Dim dt As DataTable

            Dim params() As String
            Dim values As Object

            Try
                params = New String() {"@Id_Acreditacion"}
                values = New Object() {AcreditacionID}

                dt = ExecuteStoreProcedureDatatable(storeProcedure, params, values)
                Return dt

            Catch ex As Exception

            End Try
        End Function


        Public Function SincronizaTransaccion(RFC As String, TransaccionInfo As TransaccionEntityObject) As Integer
            Dim storeProcedureHeader As String = "CE_InsertTransaccion"
            Dim storeProcedureDetail As String = "CE_InsertTransaccionDetail"

            Dim params() As String
            Dim values() As Object
            Dim result As Integer
            Dim res As Integer

            Dim cmd1 As SqlCommand
            '//Dim cmd2 As SqlCommand
            Dim transaction As SqlTransaction

            Try
                Using conn As SqlConnection = New SqlConnection(Me.ConnectionString)
                    '//Se abre la conexion a la BD
                    conn.Open()

                    '//Se crea el comendo sql de execucion
                    cmd1 = conn.CreateCommand()
                    '//cmd2 = conn.CreateCommand()

                    '//Se crea la transaccion sql local
                    transaction = conn.BeginTransaction("InsertTransaction")

                    cmd1 = New SqlCommand(storeProcedureHeader, conn, transaction)
                    '//cmd2 = New SqlCommand(storeProcedureDetail, conn, transaction)

                    Try
                        '//Se prepara la insercion del registro de encabezado del Deposito
                        params = New String() {"@IdTransaccion", "@ClaveSucursal", "@Clave_Cliente", "@RFC", "@TipoMovimiento", "@Fecha", "@HoraInicial", "@HoraFinal", "@ImporteTotal", "@Status", "@IdRetiro", "@UsuarioRegistro", "@Observaciones", "@NumeroRemision", "@NumeroEnvase", "@TotalMXN", "@TotalUSD", "@TotalUSDconvert", "@ImporteOtros", "@ImporteOtrosD", "@TipoCambio", "@Folio", "@Cargo", "@Abono", "@Tipo", "@IdCorte", "@EsEfectivo", "@IdCaja", "@ClaveCaja", "@NumeroCuenta", "@Referencia", "@Divisa", "@Acreditado", "@rowid"}
                        values = New Object() {TransaccionInfo.IdTransaccion, TransaccionInfo.ClaveSucursal, TransaccionInfo.ClaveCliente, RFC, TransaccionInfo.TipoMovimiento, TransaccionInfo.Fecha, TransaccionInfo.HoraInicio, TransaccionInfo.HoraFin, TransaccionInfo.ImporteTotal, TransaccionInfo.Status, TransaccionInfo.IdRetiro, TransaccionInfo.UsuarioRegistro, TransaccionInfo.Observaciones, TransaccionInfo.NumeroRemision, TransaccionInfo.NumeroEnvase, TransaccionInfo.TotalMXN, TransaccionInfo.TotalUSD, TransaccionInfo.TotalUSDConvert, TransaccionInfo.ImporteOtros, TransaccionInfo.ImporteOtrosD, TransaccionInfo.TipoCambio, TransaccionInfo.Folio, TransaccionInfo.Cargo, TransaccionInfo.Abono, TransaccionInfo.Tipo, TransaccionInfo.IdCorte, IIf(TransaccionInfo.EsEfectivo, "S", "N"), TransaccionInfo.IdCaja, TransaccionInfo.ClaveCaja, TransaccionInfo.NumeroCuenta, TransaccionInfo.Referencia, TransaccionInfo.Divisa, IIf(TransaccionInfo.Acreditado, "S", "N"), TransaccionInfo.rowID}

                        cmd1.CommandType = CommandType.StoredProcedure
                        cmd1.CommandTimeout = 480

                        '//Se establcen los parametros del Store Procedure
                        SetCommandParameters(cmd1, params, values)

                        '//Se ejecuta comendo de insert encabezado de Deposito
                        result = cmd1.ExecuteNonQuery()

                        ''//Se inserta el detalle del Deposito
                        'For Each mov As TransaccionDetailEntityObject In TransaccionInfo.Movimientos
                        '    '//Se prepara la insercion del registro de detalle del Deposito
                        '    params = New String() {"@IdTransaccion", "@ClaveSucursal", "@SerieCaset", "@ClaveDenominacion", "@Denominacion", "@CantidadPiezas", "@Importe", "@ClaveMoneda", "@SerieValidador", "@NumeroValidador"}
                        '    values = New Object() {TransaccionInfo.IdTransaccion, TransaccionInfo.ClaveSucursal, mov.SerieCasete, mov.ClaveDenominacion, mov.Denominacion, mov.CantidadPiezas, mov.Importe, mov.ClaveMoneda, mov.SerieValidador, mov.NumeroValidador}

                        '    cmd2.CommandType = CommandType.StoredProcedure
                        '    cmd1.CommandTimeout = 480

                        '    '//Se establcen los parametros del Store Procedure
                        '    SetCommandParameters(cmd2, params, values)

                        '    '//Se ejecuta comendo de insert encabezado de Deposito
                        '    res = CType(cmd2.ExecuteScalar, Integer)
                        'Next

                        '//Se ejecuta proceso COMMIT de la transaccion
                        transaction.Commit()

                        Return result

                    Catch ex2 As Exception
                        Try
                            transaction.Rollback()
                            Throw ex2
                        Catch ex3 As Exception
                            Throw ex3
                        End Try
                    End Try
                End Using

            Catch ex1 As Exception
                Tools.CashflowEventLog.WriteToEventLog(ex1.Message, "Cashflow", EventLogEntryType.Error)
                Throw New Exception("Data.CajerosEmpresarialesDataObject.InsertTransaccion : " + ex1.Message)
            End Try
        End Function

        ''' <summary>
        ''' Descripcion: Funcion que obtiene los registros de consulta de los depositos/recolecciones recolectadas de todas las sucursales
        ''' Tipo: Reporte
        ''' </summary>
        ''' <param name="Filter">Filtro dinamico para consulta y busqueda de registros de Deposito/Recoleccion</param>
        ''' <returns>Tabla que contiene los registros de Deposito/Recoleccion con base en los criterios de busqueda</returns>
        Public Function GetReporteDepositosRecolecciones(Filter As CajerosEmpresarialesFilterEntityObject) As DataTable
            Dim storeProcedure As String = "CE_GetDepositosRecolecciones"
            Dim dt As DataTable

            Dim _filter As String
            Dim params() As String
            Dim values As Object

            Try
                _filter = CreateFilterDepositosReceptores(Filter)
                params = New String() {"@dinamic_filter"}
                values = New Object() {_filter}

                dt = ExecuteStoreProcedureDatatable(storeProcedure, params, values)
                Return dt

            Catch ex As Exception

            End Try
        End Function

        ''' <summary>
        ''' Descripcion: Funcion que obtiene los registros de transmiones realizadas para acreditacion.
        ''' Tipo: Reporte
        ''' </summary>
        ''' <param name="Filter">Filtro dinamico para consulta y busqueda de registros de Deposito/Recoleccion</param>
        ''' <returns></returns>
        Public Function GetReporteAcreditacionesTransmitidas(Filter As CajerosEmpresarialesFilterEntityObject) As DataTable
            Dim storeProcedure As String = "CE_GetAcreditacionesTransmitidas"
            Dim dt As DataTable

            Dim _filter As String
            Dim params() As String
            Dim values As Object

            Try
                _filter = CreateFilterAcreditacionesTransmitidas(Filter)
                params = New String() {"@dinamic_filter"}
                values = New Object() {_filter}

                dt = ExecuteStoreProcedureDatatable(storeProcedure, params, values)
                Return dt

            Catch ex As Exception

            End Try
        End Function

        ''' <summary>
        ''' Descripcion: Funcion que obtiene los registros para ser transmitidos para pre-acreditacion.
        ''' Tipo: Proceso
        ''' </summary>
        ''' <param name="Fecha">Fecha Final de consulta, se obtendran todos los registros no transmitidos anteriores a la fecha</param>
        ''' <returns></returns>
        Public Function GetTransaccionesToTransfer(Fecha As Date) As DataTable
            Dim storeProcedure As String = "CE_GetTransaccionesDepositosToTransfer"
            Dim dt As DataTable

            Dim params() As String
            Dim values As Object

            Try
                params = New String() {"@fecha"}
                values = New Object() {Fecha}

                dt = ExecuteStoreProcedureDatatable(storeProcedure, params, values)
                Return dt

            Catch ex As Exception

            End Try
        End Function

        Public Function ProcesaTransaccionesAcreditables(Fecha As Date) As DataTable
            Dim storeProcedure As String = "CE_proceso_FileToTransferAcreditaciones"
            Dim dt As DataTable

            Dim params() As String
            Dim values As Object

            Try
                params = New String() {"@fecha"}
                values = New Object() {Fecha}

                dt = ExecuteStoreProcedureDatatable(storeProcedure, params, values)
                Return dt

            Catch ex As Exception

            End Try
        End Function

        Public Function ArqueoTransitoSaldoInicial(fecha As Date, Optional CRC As String = "") As DataTable
            Dim storeProcedure As String = "CE_GetArqueoTransitoSaldos"
            Dim dt As DataTable

            Dim params() As String
            Dim values As Object

            Try
                params = New String() {"@fecha", "@CRC"}
                values = New Object() {fecha, CRC}

                dt = ExecuteStoreProcedureDatatable(storeProcedure, params, values)
                Return dt

            Catch ex As Exception

            End Try
        End Function
        Public Function ArqueoTransitoAcreditaciones(Fecha As Date, Optional CRC As String = "") As DataTable
            Dim storeProcedure As String = "CE_GetArqueoTransitoAcreditaciones"
            Dim dt As DataTable

            Dim params() As String
            Dim values As Object

            Try
                params = New String() {"@fecha", "@CRC"}
                values = New Object() {Fecha, CRC}

                dt = ExecuteStoreProcedureDatatable(storeProcedure, params, values)
                Return dt

            Catch ex As Exception

            End Try
        End Function
        Public Function ArqueoTransitoRecolecciones(Fecha As Date, Optional CRC As String = "") As DataTable
            Dim storeProcedure As String = "CE_GetArqueoTransitoRecolecciones"
            Dim dt As DataTable

            Dim params() As String
            Dim values As Object

            Try
                params = New String() {"@fecha", "@CRC"}
                values = New Object() {Fecha, CRC}

                dt = ExecuteStoreProcedureDatatable(storeProcedure, params, values)
                Return dt

            Catch ex As Exception

            End Try
        End Function
        Public Function ArqueoTransitoGeneral(Fecha As Date, Optional CRC As String = "") As DataTable
            Dim storeProcedure As String = "CE_GetArqueoTransitoGeneral"
            Dim dt As DataTable

            Dim params() As String
            Dim values As Object

            Try
                params = New String() {"@fecha", "@CRC"}
                values = New Object() {Fecha, CRC}

                dt = ExecuteStoreProcedureDatatable(storeProcedure, params, values)
                Return dt

            Catch ex As Exception

            End Try
        End Function

        Public Function RollbackUpdateAcreditacion(DIdarchivo As Int32, DArchivo As String, DComentario As String) As UInt16
            Dim exito As UInt16
            exito = 0
            Dim storeProcedureHeader As String = "CE_Proceso_RollbackUpdateAcreditacion"

            Dim params() As String
            Dim values() As Object
            Dim result As Object

            Dim cmd As SqlCommand

            Dim transaction As SqlTransaction

            Try
                Using conn As SqlConnection = New SqlConnection(Me.ConnectionString)
                    '//Se abre la conexion a la BD
                    conn.Open()

                    '//Se crea comando sql de ejecucion
                    cmd = conn.CreateCommand

                    '//Se crea la transaccion sql local
                    transaction = conn.BeginTransaction("RollbackAcreditacion")

                    cmd = New SqlCommand(storeProcedureHeader, conn, transaction)

                    Try
                        '//Se prepara la insercion del registro de encabezado de la recoleccion
                        params = New String() {"@IDARCHIVO", "@NOMBREARCHIVO", "@COMENTARIO"}
                        values = New Object() {DIdarchivo, DArchivo, DComentario}

                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandTimeout = 480
                        '//Se establcen los parametros del Store Procedure
                        SetCommandParameters(cmd, params, values)

                        '//Se ejecuta comendo de insert encabezado de Deposito
                        result = cmd.ExecuteScalar()

                        transaction.Commit()
                        conn.Close()
                        If (Convert.ToInt64(result.ToString()) > 0) Then
                            exito = 1
                            Return exito
                        Else
                            Return exito
                        End If
                    Catch ex2 As Exception
                        Try
                            transaction.Rollback()
                            conn.Close()
                            exito = 0
                            Return exito
                            Throw ex2
                        Catch ex3 As Exception
                            Throw ex3
                        End Try
                    End Try


                End Using
            Catch ex1 As Exception
                Tools.CashflowEventLog.WriteToEventLog(ex1.Message, "Cashflow", EventLogEntryType.Error)
                Throw New Exception("Data.CajerosEmpresarialesDataObject.UpdateAcreditacionDepositos : " + ex1.Message)
            End Try
        End Function

        Public Function SucessAcreditacion(DIarchivo As Int32, DArchivo As String, DComentario As String) As UInt16
            Dim exito As UInt16
            exito = 0
            Dim storeProcedureHeader As String = "CE_Proceso_SucessAcreditacion"

            Dim params() As String
            Dim values() As Object
            Dim result As Object

            Dim cmd As SqlCommand

            Dim transaction As SqlTransaction

            Try
                Using conn As SqlConnection = New SqlConnection(Me.ConnectionString)
                    '//Se abre la conexion a la BD
                    conn.Open()

                    '//Se crea comando sql de ejecucion
                    cmd = conn.CreateCommand

                    '//Se crea la transaccion sql local
                    transaction = conn.BeginTransaction("SucessAcreditacion")

                    cmd = New SqlCommand(storeProcedureHeader, conn, transaction)

                    Try
                        '//Se prepara la insercion del registro de encabezado de la recoleccion
                        params = New String() {"@IDARCHIVO", "@NOMBREARCHIVO", "@COMENTARIO"}
                        values = New Object() {DIarchivo, DArchivo, DComentario}

                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandTimeout = 480
                        '//Se establcen los parametros del Store Procedure
                        SetCommandParameters(cmd, params, values)

                        '//Se ejecuta comendo de insert encabezado de Deposito
                        result = cmd.ExecuteScalar()

                        transaction.Commit()
                        conn.Close()
                        If (Convert.ToInt64(result.ToString()) > 0) Then
                            exito = 1
                            Return exito
                        Else
                            Return exito
                        End If
                    Catch ex2 As Exception
                        Try
                            transaction.Rollback()
                            conn.Close()
                            exito = 0
                            Return exito
                            Throw ex2
                        Catch ex3 As Exception
                            Throw ex3
                        End Try
                    End Try


                End Using
            Catch ex1 As Exception
                Tools.CashflowEventLog.WriteToEventLog(ex1.Message, "Cashflow", EventLogEntryType.Error)
                Throw New Exception("Data.CajerosEmpresarialesDataObject.UpdateAcreditacionDepositos : " + ex1.Message)
            End Try
        End Function


#Region "Functions"
        Private Function CreateFilterTransaccionSentence(Filter As CajerosEmpresarialesFilterEntityObject)
            Dim sb As StringBuilder

            sb = New StringBuilder()

            If Filter IsNot Nothing Then
                If Filter.FechaInicial <> Nothing Then
                    sb.AppendFormat(" DATEADD(dd,0,DATEDIFF(dd, 0,t.Fecha)) between  DATEADD(dd,0,DATEDIFF(dd,0,'{0}')) and DATEADD(dd,0,DATEDIFF(dd,0,'{1}'))", Filter.FechaInicial.ToString("yyyy-MM-dd"), Filter.FechaFinal.ToString("yyyy-MM-dd"))
                    sb.AppendLine()
                End If

                If Filter.ClaveCliente <> String.Empty Then
                    sb.AppendFormat(" and t.Clave_Cliente = '{0}' {1}", Filter.ClaveCliente, vbCrLf)
                    sb.AppendLine()
                End If

                If Filter.ClaveSucursal <> String.Empty Then
                    sb.AppendFormat(" and t.Clave_Sucursal = '{0}' {1}", Filter.ClaveSucursal, vbCrLf)
                    sb.AppendLine()
                End If

                If Filter.Tipo <> String.Empty Then
                    sb.AppendFormat(" and t.Tipo_Movimiento = '{0}' {1}", Filter.Tipo, vbCrLf)
                    sb.AppendLine()
                End If

                If Filter.Divisa <> String.Empty Then
                    sb.AppendFormat(" and t.Divisa = '{0}' {1}", Filter.Divisa, vbCrLf)
                    sb.AppendLine()
                End If
            End If

            Return sb.ToString()
        End Function

        Private Function CreateFilterDepositosReceptores(Filter As CajerosEmpresarialesFilterEntityObject)
            Dim sb As StringBuilder

            sb = New StringBuilder()

            If Filter IsNot Nothing Then
                If Filter.FechaInicial <> Nothing Then
                    sb.AppendFormat(" DATEADD(dd,0,DATEDIFF(dd, 0,t.Fecha)) between  DATEADD(dd,0,DATEDIFF(dd,0,'{0}')) and DATEADD(dd,0,DATEDIFF(dd,0,'{1}'))", Filter.FechaInicial.ToString("yyyy-MM-dd"), Filter.FechaFinal.ToString("yyyy-MM-dd"))
                    sb.AppendLine()
                End If

                If Filter.ClaveCliente <> String.Empty Then
                    sb.AppendFormat(" and t.Clave_Cliente = '{0}' {1}", Filter.ClaveCliente, vbCrLf)
                    sb.AppendLine()
                End If

                If Filter.ClaveSucursal <> String.Empty Then
                    sb.AppendFormat(" and t.Clave_Sucursal = '{0}' {1}", Filter.ClaveSucursal, vbCrLf)
                    sb.AppendLine()
                End If

                If Filter.Tipo <> String.Empty Then
                    sb.AppendFormat(" and t.Tipo_Movimiento = '{0}' {1}", Filter.Tipo, vbCrLf)
                    sb.AppendLine()
                End If

                If Filter.Divisa <> String.Empty Then
                    sb.AppendFormat(" and t.Divisa = '{0}' {1}", Filter.Divisa, vbCrLf)
                    sb.AppendLine()
                End If
            End If

            Return sb.ToString()
        End Function

        Private Function CreateFilterAcreditacionesTransmitidas(Filter As CajerosEmpresarialesFilterEntityObject)
            Dim sb As StringBuilder

            sb = New StringBuilder()

            If Filter IsNot Nothing Then
                If Filter.FechaInicial <> Nothing Then
                    sb.AppendFormat(" DATEADD(dd,0,DATEDIFF(dd, 0,e.Fecha_Transmision)) between  DATEADD(dd,0,DATEDIFF(dd,0,'{0}')) and DATEADD(dd,0,DATEDIFF(dd,0,'{1}'))", Filter.FechaInicial.ToString("yyyy-MM-dd"), Filter.FechaFinal.ToString("yyyy-MM-dd"))
                    sb.AppendLine()
                End If

                If Filter.ClaveCliente <> String.Empty Then
                    sb.AppendFormat(" and d.Clave_Cliente = '{0}' {1}", Filter.ClaveCliente, vbCrLf)
                    sb.AppendLine()
                End If

                If Filter.ClaveSucursal <> String.Empty Then
                    sb.AppendFormat(" and d.Clave_Sucursal = '{0}' {1}", Filter.ClaveSucursal, vbCrLf)
                    sb.AppendLine()
                End If

            End If

            Return sb.ToString()
        End Function

        Private Function CreateFilterBaseCajeros(Filter As CajerosEmpresarialesFilterEntityObject)
            Dim sb As StringBuilder

            sb = New StringBuilder()

            If Filter IsNot Nothing Then
                If Filter.FechaInicial <> Nothing Then
                    sb.Append(" (1 > 0)")
                    sb.AppendLine()
                End If

                If Filter.ClaveCliente <> String.Empty Then
                    sb.AppendFormat(" and suc.Clave_Cliente = '{0}' {1}", Filter.ClaveCliente, vbCrLf)
                    sb.AppendLine()
                End If

                If Filter.ClaveSucursal <> String.Empty Then
                    sb.AppendFormat(" and suc.Clave_Sucursal = '{0}' {1}", Filter.ClaveSucursal, vbCrLf)
                    sb.AppendLine()
                End If

            End If

            Return sb.ToString()
        End Function

        Private Function CreateFilterMonitorArchivos(Filter As CajerosEmpresarialesFilterEntityObject)
            Dim sb As StringBuilder

            sb = New StringBuilder()

            If Filter IsNot Nothing Then
                If Filter.FechaInicial <> Nothing Then
                    sb.Append(" (1 > 0)")
                    sb.AppendLine()
                End If

                If Filter.FechaFinal <> Nothing Then
                    sb.Append(" (1 > 0)")
                    sb.AppendLine()
                End If

                If Filter.ClaveCliente <> String.Empty Then
                    sb.AppendFormat(" and suc.Clave_Cliente = '{0}' {1}", Filter.ClaveCliente, vbCrLf)
                    sb.AppendLine()
                End If

                If Filter.ClaveSucursal <> String.Empty Then
                    sb.AppendFormat(" and suc.Clave_Sucursal = '{0}' {1}", Filter.ClaveSucursal, vbCrLf)
                    sb.AppendLine()
                End If

            End If

            Return sb.ToString()
        End Function
#End Region

    End Class

End Namespace