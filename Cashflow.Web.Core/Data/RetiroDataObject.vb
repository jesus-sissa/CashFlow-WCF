Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Entities
Imports SISSA.Cashflow.Web.Core.Data.Interfaces
Imports System.Data.SqlClient
Imports System.Text

Namespace Data

    Public Class RetiroDataObject
        Inherits BaseDataObject
        Implements ITransaccionDataObject

        Public Function InsertTransaccion(RFC As String, TransaccionInfo As TransaccionEntityObject) As String Implements ITransaccionDataObject.InsertTransaccion
            Dim storeProcedureHeader As String = "CashflowWEB_InsertTransaccionRetiro"
            Dim storeProcedureDetail As String = "CashflowWEB_InsertTransaccionRetiroDetail"

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
                    transaction = conn.BeginTransaction("InsertRetiroTransaction")

                    cmd1 = New SqlCommand(storeProcedureHeader, conn, transaction)
                    cmd2 = New SqlCommand(storeProcedureDetail, conn, transaction)

                    Try
                        '//Se prepara la insercion del registro de encabezado del Deposito
                        params = New String() {"@IdTransaccion", "@ClaveSucursal", "@Fecha", "@HoraInicial", "@HoraFinal", "@ImporteTotal", "@Status", "@UsuarioRegistro", "@Observaciones", "@NumeroRemision", "@NumeroEnvase", "@TotalMXN", "@TotalUSD", "@TotalUSDconvert", "@ImporteOtros", "@ImporteOtrosD"}
                        values = New Object() {TransaccionInfo.IdTransaccion, TransaccionInfo.ClaveSucursal, TransaccionInfo.Fecha, TransaccionInfo.HoraInicio, TransaccionInfo.HoraFin, TransaccionInfo.ImporteTotal, TransaccionInfo.Status, TransaccionInfo.UsuarioRegistro, TransaccionInfo.Observaciones, TransaccionInfo.NumeroRemision, TransaccionInfo.NumeroEnvase, TransaccionInfo.TotalMXN, TransaccionInfo.TotalUSD, TransaccionInfo.TotalUSDConvert, TransaccionInfo.ImporteOtros, TransaccionInfo.ImporteOtrosD}

                        cmd1.CommandType = CommandType.StoredProcedure
                        cmd1.CommandTimeout = 480

                        '//Se establcen los parametros del Store Procedure
                        SetCommandParameters(cmd1, params, values)

                        '//Se ejecuta comendo de insert encabezado de Deposito
                        result = CType(cmd1.ExecuteScalar, Guid)

                        '//Se inserta el detalle del Deposito
                        For Each mov As TransaccionDetailEntityObject In TransaccionInfo.Movimientos
                            '//Se prepara la insercion del registro de detalle del Deposito
                            params = New String() {"@IdTransaccion", "@ClaveSucursal", "@ClaveDenominacion", "@Cantidad_Piezas", "@Importe", "@Denominacion"}
                            values = New Object() {TransaccionInfo.IdTransaccion, TransaccionInfo.ClaveSucursal, mov.ClaveDenominacion, mov.CantidadPiezas, mov.Importe, mov.Denominacion}

                            cmd2.CommandType = CommandType.StoredProcedure
                            cmd1.CommandTimeout = 480

                            '//Se establcen los parametros del Store Procedure
                            SetCommandParameters(cmd2, params, values)

                            '//Se ejecuta comendo de insert encabezado de Deposito
                            cmd2.ExecuteScalar()
                            '//res = CType(cmd2.ExecuteScalar, Integer)
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
                Throw New Exception("Data.RetiroDataObject.InsertTransaccion : " + ex1.Message)
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
                sb.AppendLine("SELECT top 500 suc.Clave_Cliente, ret.* ")
                sb.AppendLine("FROM Retiros ret")
                sb.AppendLine("left join Sucursales suc on ret.Clave_Sucursal = suc.Clave_Sucursal ")
                'sb.AppendLine("WHERE ISNULL(Acreditado,0) = 0;")
                'DEBUG
                sb.AppendLine("WHERE ret.FECHA >= '2020-07-20';")

                dt = ExecuteDataTable(sb.ToString, Nothing, Nothing)

                Return dt
            Catch ex As Exception

            End Try
        End Function

        Public Function UpdateEstatusSincronizado(TransaccionInfo As Object) As Integer Implements ITransaccionDataObject.UpdateEstatusSincronizado
            Throw New NotImplementedException()
        End Function

#Region "Functions"
        Private Function GetEntityObject(row As DataRow) As TransaccionEntityObject
            Dim _transaccionInfo As TransaccionEntityObject = New TransaccionEntityObject()

            Try
                If row IsNot Nothing Then
                    _transaccionInfo.IdTransaccion = row("Id_Retiro")
                    _transaccionInfo.ClaveSucursal = row("Clave_Sucursal").ToString
                    _transaccionInfo.ClaveCliente = row("Clave_Cliente").ToString
                    _transaccionInfo.TipoMovimiento = "R"
                    _transaccionInfo.Fecha = row("Fecha")
                    _transaccionInfo.HoraInicio = row("Hora_Inicio")
                    _transaccionInfo.HoraFin = row("Hora_Fin")
                    _transaccionInfo.ImporteTotal = row("Importe_Total")
                    _transaccionInfo.Status = row("Status").ToString
                    _transaccionInfo.UsuarioRegistro = row("Usuario_Registro").ToString
                    _transaccionInfo.Observaciones = row("Observaciones").ToString
                    _transaccionInfo.NumeroRemision = row("Numero_Remision")
                    _transaccionInfo.NumeroEnvase = row("Numero_Envase").ToString
                    _transaccionInfo.TotalMXN = row("Total_MXP")
                    _transaccionInfo.TotalUSD = row("Total_USD")
                    _transaccionInfo.TotalUSDConvert = row("TotalUSD_Convert")
                    _transaccionInfo.ImporteOtros = row("Importe_Otros")
                    _transaccionInfo.ImporteOtrosD = row("Importe_OtrosD")
                    _transaccionInfo.Cargo = row("Importe_Total")
                    _transaccionInfo.Abono = 0
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
#End Region

    End Class
End Namespace