Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Entities
Imports SISSA.Cashflow.Web.Core.Data.Interfaces
Imports System.Data.SqlClient
Imports System.Text

Namespace Data

    Public Class DepositoDataObject
        Inherits BaseDataObject
        Implements ITransaccionDataObject

        Public Function InsertTransaccion(RFC As String, TransaccionInfo As TransaccionEntityObject) As String Implements ITransaccionDataObject.InsertTransaccion
            Dim storeProcedureHeader As String = "CashflowWEB_InsertTransaccionDeposito"
            Dim storeProcedureDetail As String = "CashflowWEB_InsertTransaccionDepositoDetail"

            Dim params() As String
            Dim values() As Object
            Dim result As Guid
            Dim res As Integer

            Dim cmd1 As SqlCommand
            Dim cmd2 As SqlCommand
            Dim transaction As SqlTransaction

            Try
                Using conn As SqlConnection = New SqlConnection(Me.ConnectionString)
                    '//Se abre la conexion a la BD
                    conn.Open()

                    '//Se crea el comendo sql de execucion
                    cmd1 = conn.CreateCommand()
                    cmd2 = conn.CreateCommand()

                    '//Se crea la transaccion sql local
                    transaction = conn.BeginTransaction("InsertDepositoTransaction")

                    cmd1 = New SqlCommand(storeProcedureHeader, conn, transaction)
                    cmd2 = New SqlCommand(storeProcedureDetail, conn, transaction)

                    Try
                        '//Se prepara la insercion del registro de encabezado del Deposito
                        params = New String() {"@IdTransaccion", "@ClaveSucursal", "@ClaveCajero", "@Fecha", "@HoraInicial", "@HoraFinal", "@ImporteTotal", "@Status", "@IdRetiro", "@UsuarioRegistro", "@TotalMXN", "@TotalUSD", "@TotalUSDconvert", "@TipoCambio", "@Folio", "@Tipo", "@IdCorte", "@EsEfectivo", "@IdCaja", "@ClaveCaja", "@NumeroCuenta", "@Referencia", "@Divisa", "@Acreditado"}
                        values = New Object() {TransaccionInfo.IdTransaccion, TransaccionInfo.ClaveSucursal, TransaccionInfo.ClaveCajero, TransaccionInfo.Fecha, TransaccionInfo.HoraInicio, TransaccionInfo.HoraFin, TransaccionInfo.ImporteTotal, TransaccionInfo.Status, TransaccionInfo.IdRetiro, TransaccionInfo.UsuarioRegistro, TransaccionInfo.TotalMXN, TransaccionInfo.TotalUSD, TransaccionInfo.TotalUSDConvert, TransaccionInfo.TipoCambio, TransaccionInfo.Folio, TransaccionInfo.Tipo, TransaccionInfo.IdCorte, IIf(TransaccionInfo.EsEfectivo, "S", "N"), TransaccionInfo.IdCaja, TransaccionInfo.ClaveCaja, TransaccionInfo.NumeroCuenta, TransaccionInfo.Referencia, TransaccionInfo.Divisa, IIf(TransaccionInfo.Acreditado, "S", "N")}

                        cmd1.CommandType = CommandType.StoredProcedure
                        cmd1.CommandTimeout = 480

                        '//Se establcen los parametros del Store Procedure
                        SetCommandParameters(cmd1, params, values)

                        '//Se ejecuta comendo de insert encabezado de Deposito
                        result = CType(cmd1.ExecuteScalar, Guid)

                        '//Se inserta el detalle del Deposito
                        For Each mov As TransaccionDetailEntityObject In TransaccionInfo.Movimientos

                            res = InsertDepositoDetail(mov)

                            ''//Se prepara la insercion del registro de detalle del Deposito
                            'params = New String() {"@IdTransaccion", "@ClaveSucursal", "@SerieCaset", "@ClaveDenominacion", "@Denominacion", "@CantidadPiezas", "@Importe", "@ClaveMoneda", "@SerieValidador", "@NumeroValidador"}
                            'values = New Object() {TransaccionInfo.IdTransaccion, TransaccionInfo.ClaveSucursal, mov.SerieCasete, mov.ClaveDenominacion, mov.Denominacion, mov.CantidadPiezas, mov.Importe, mov.ClaveMoneda, mov.SerieValidador, mov.NumeroValidador}

                            'cmd2.CommandType = CommandType.StoredProcedure
                            'cmd1.CommandTimeout = 480

                            ''//Se establcen los parametros del Store Procedure
                            'SetCommandParameters(cmd2, params, values)

                            ''//Se ejecuta comendo de insert encabezado de Deposito
                            'res = CType(cmd2.ExecuteScalar, Integer)
                        Next

                        '//Se ejecuta proceso COMMIT de la transaccion
                        transaction.Commit()

                        Return result.ToString

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
                Throw New Exception("Data.DepositoDataObject.InsertTransaccion : " + ex1.Message)
            End Try
        End Function

        Public Function UpdateReferenciaAndEstatus(DepositoInfo As TransaccionEntityObject) As Integer
            Dim res As Integer

            Dim sb As StringBuilder
            Dim cmd1 As SqlCommand
            Dim transaction As SqlTransaction

            Try
                Using conn As SqlConnection = New SqlConnection(Me.ConnectionString)
                    '//Se abre la conexion a la BD
                    conn.Open()

                    '//Se crea el comendo sql de execucion
                    cmd1 = conn.CreateCommand()

                    '//Se crea la transaccion sql local
                    transaction = conn.BeginTransaction("UpdateDepositoTransaction")

                    Try
                        sb = New StringBuilder
                        sb.AppendLine("UPDATE Depositos")
                        sb.AppendLine("SET ")
                        sb.AppendFormat("Id_Retiro = {0}", DepositoInfo.IdRetiro)
                        sb.AppendFormat(",Status = '{0}'", DepositoInfo.Status)
                        sb.AppendLine()
                        sb.AppendFormat("WHERE Id_Deposito = {0} AND Clave_Sucursal = '{1}'", DepositoInfo.IdTransaccion, DepositoInfo.ClaveSucursal)

                        cmd1 = New SqlCommand(sb.ToString, conn)
                        cmd1.Transaction = transaction
                        cmd1.CommandType = CommandType.Text
                        cmd1.CommandTimeout = 480

                        '//Se ejecuta comendo de insert encabezado de Deposito
                        res = cmd1.ExecuteNonQuery

                        '//Se ejecuta proceso COMMIT de la transaccion
                        transaction.Commit()

                        Return res

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
                Throw New Exception("Data.DepositoDataObject.UpdateReferenciaAndEstatus : " + ex1.Message)
            End Try
        End Function

        Public Function GetTransaccionesNoSincronizadasList() As List(Of TransaccionEntityObject) Implements ITransaccionDataObject.GetTransaccionesNoSincronizadasList
            Dim dt As DataTable
            Dim _transaccionList As List(Of TransaccionEntityObject) = New List(Of TransaccionEntityObject)

            Try
                dt = GetTransaccionesNoSincronizadasTable()

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each row As DataRow In dt.Rows
                        _transaccionList.Add(GetEntityObject(row))
                    Next
                End If

                Return _transaccionList

            Catch ex As Exception

            End Try
        End Function
        Public Function GetTransaccionesNoSincronizadasTable() As DataTable Implements ITransaccionDataObject.GetTransaccionesNoSincronizadasTable
            Dim sb As StringBuilder
            Dim dt As DataTable

            Try
                sb = New StringBuilder()
                sb.AppendLine("SELECT TOP 1000 dep.Id_Deposito")
                sb.AppendLine(",suc.Clave_Cliente")
                sb.AppendLine(",suc.Clave_Cliente as Clave_Sucursal")
                sb.AppendLine(",'D' as TipoMovimiento")
                sb.AppendLine(",dep.Fecha")
                sb.AppendLine(",dep.Hora_Inicio")
                sb.AppendLine(",dep.Hora_Fin")
                sb.AppendLine(",dep.Importe_Total")
                sb.AppendLine(",dep.[Status]")
                sb.AppendLine(",dep.Id_Retiro")
                sb.AppendLine(",dep.Usuario_Registro")
                sb.AppendLine(",dep.Total_MXP")
                sb.AppendLine(",dep.Total_USD")
                sb.AppendLine(",dep.TotalUSD_Convert")
                sb.AppendLine(",dep.TipoCambio_USD")
                sb.AppendLine(",dep.Folio")
                sb.AppendLine(",dep.Tipo as TipoDeposito")
                sb.AppendLine(",dep.Id_Corte")
                sb.AppendLine(",dep.Es_Efectivo")
                sb.AppendLine(",dep.Id_Caja")
                sb.AppendLine(",dep.Clave_Caja")
                sb.AppendLine("FROM Depositos dep")
                sb.AppendLine("left join Sucursales suc on dep.Clave_Sucursal = suc.Clave_Sucursal")
                'sb.AppendLine("WHERE ISNULL(Acreditado,0) = 0;")

                'DEBUG
                sb.AppendLine("WHERE dep.FECHA >= '2020-07-20';")

                dt = ExecuteDataTable(sb.ToString, Nothing, Nothing)

                Return dt
            Catch ex As Exception

            End Try
        End Function

        Public Function UpdateEstatusSincronizado(TransaccionInfo As Object) As Integer Implements ITransaccionDataObject.UpdateEstatusSincronizado

        End Function

#Region "Functions"
        Private Function GetEntityObject(row As DataRow) As TransaccionEntityObject
            Dim _transaccionInfo As TransaccionEntityObject = New TransaccionEntityObject()

            Try
                If row IsNot Nothing Then
                    _transaccionInfo.IdTransaccion = row("Id_Deposito")
                    _transaccionInfo.ClaveSucursal = row("Clave_Sucursal").ToString
                    _transaccionInfo.ClaveCliente = row("Clave_Cliente").ToString
                    _transaccionInfo.TipoMovimiento = "D"
                    _transaccionInfo.Fecha = row("Fecha")
                    _transaccionInfo.HoraInicio = row("Hora_Inicio")
                    _transaccionInfo.HoraFin = row("Hora_Fin")
                    _transaccionInfo.ImporteTotal = row("Importe_Total")
                    _transaccionInfo.Status = row("Status").ToString
                    _transaccionInfo.IdRetiro = IIf(Not IsDBNull(row("Id_Retiro")), row("Id_Retiro"), 0)
                    _transaccionInfo.UsuarioRegistro = row("Usuario_Registro").ToString
                    _transaccionInfo.TotalMXN = row("Total_MXP")
                    _transaccionInfo.TotalUSD = row("Total_USD")
                    _transaccionInfo.TotalUSDConvert = row("TotalUSD_Convert")
                    _transaccionInfo.TipoCambio = row("TipoCambio_USD")
                    _transaccionInfo.Folio = row("Folio")
                    _transaccionInfo.Cargo = 0
                    _transaccionInfo.Abono = row("Importe_Total")
                    _transaccionInfo.Tipo = row("TipoDeposito")
                    _transaccionInfo.IdCorte = row("Id_Corte")
                    _transaccionInfo.EsEfectivo = IIf(row("Es_Efectivo").ToString = "S", True, False)
                    _transaccionInfo.IdCaja = row("Id_Caja")
                    _transaccionInfo.ClaveCaja = row("Clave_Caja")
                    '_transaccionInfo.NumeroCuenta = row("NumeroCuenta")
                    '_transaccionInfo.Referencia = row("Referencia")
                    '_transaccionInfo.Divisa = row("Divisa")
                    '_transaccionInfo.Acreditado = IIf(row("Acreditado").ToString.Trim = "", False, True)
                    '_transaccionInfo.rowID = row("rowid").ToString

                    'DEBUG
                    If row.Table.Columns.Contains("rowid") Then
                        _transaccionInfo.rowID = row("rowid").ToString
                    End If
                    'DEBUG
                    If _transaccionInfo.rowID = String.Empty Then
                        _transaccionInfo.rowID = Guid.NewGuid.ToString
                    End If

                End If

                Return _transaccionInfo
            Catch ex As Exception
                Throw New Exception("GetEntityObject - " + ex.Message)
            End Try
        End Function

        Private Function InsertDepositoDetail(Detail As TransaccionDetailEntityObject) As Integer
            Dim sb As StringBuilder = New StringBuilder
            Dim sql As String
            Dim res As Integer

            Try
                sb.Append("INSERT INTO [dbo].[DepositosD](")
                sb.Append("[Id_Deposito]")
                sb.Append(",[Serie_Caset]")
                sb.Append(",[Clave_Sucursal]")
                sb.Append(",[Clave_Denominacion]")
                sb.Append(",[Denominacion]")
                sb.Append(",[Cantidad_Piezas]")
                sb.Append(",[Importe]")
                sb.Append(",[Clave_Moneda]")
                sb.Append(",[Serie_Validador]")
                sb.Append(",[Numero_Validador])")
                sb.AppendLine()
                sb.Append("VALUES(")
                sb.AppendFormat("{0}", Detail.IdTransaccion)
                sb.AppendFormat(",{0}", Detail.SerieCasete)
                sb.AppendFormat(",'{0}'", Detail.ClaveSucursal)
                sb.AppendFormat(",'{0}'", Detail.ClaveDenominacion)
                sb.AppendFormat(",{0}", Detail.Denominacion)
                sb.AppendFormat(",{0}", Detail.CantidadPiezas)
                sb.AppendFormat(",{0}", Detail.Importe)
                sb.AppendFormat(",'{0}'", Detail.ClaveMoneda)
                sb.AppendFormat(",'{0}'", Detail.SerieValidador)
                sb.AppendFormat(",'{0}'", Detail.NumeroValidador)
                sb.AppendLine(")")
                sb.Append("SELECT IDENT_CURRENT('DepositosD')")

                sql = sb.ToString

                res = ExecuteScalar(sql, Nothing, Nothing)
                Return res

            Catch ex As Exception
                Throw New Exception("InsertDepositoDetail: " + ex.Message)
            End Try
        End Function
#End Region

    End Class

End Namespace