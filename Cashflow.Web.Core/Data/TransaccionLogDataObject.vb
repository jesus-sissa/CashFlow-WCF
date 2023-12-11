Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Entities
Imports System.Data.SqlClient
Imports System.Text

Namespace Data
    Public Class TransaccionLogDataObject
        Inherits BaseDataObject

        Public Function InsertTransaccionLog(TransaccionLogInfo As TransaccionLogEntityObject) As String
            Dim storeProcedureHeader As String = "CashflowWEB_AddLog"

            Dim params() As String
            Dim values() As Object
            Dim result As Guid
            Dim res As Integer

            Dim cmd1 As SqlCommand
            Dim transaction As SqlTransaction

            Try
                Using conn As SqlConnection = New SqlConnection(Me.ConnectionString)
                    '//Se abre la conexion a la BD
                    conn.Open()

                    '//Se crea el comendo sql de execucion
                    cmd1 = conn.CreateCommand()

                    '//Se crea la transaccion sql local
                    transaction = conn.BeginTransaction("InsertTransactionLog")

                    cmd1 = New SqlCommand(storeProcedureHeader, conn, transaction)

                    Try
                        '//Se prepara la insercion del registro de encabezado del Deposito
                        params = New String() {"@id_CajeroReceptor", "@iIdUsuario", "@Clave_Sucursal", "@Fecha", "@Hora", "@iIdLog_Descripcion", "@iIdPantalla", "@sDescripcion"}
                        values = New Object() {TransaccionLogInfo.IdCajero, TransaccionLogInfo.IdUsuario, TransaccionLogInfo.ClaveSucursal, TransaccionLogInfo.Fecha, TransaccionLogInfo.Hora, TransaccionLogInfo.IdDescripcion, TransaccionLogInfo.IdPantalla, TransaccionLogInfo.Descripcion}

                        cmd1.CommandType = CommandType.StoredProcedure
                        cmd1.CommandTimeout = 480

                        '//Se establcen los parametros del Store Procedure
                        SetCommandParameters(cmd1, params, values)

                        '//Se ejecuta comendo de insert encabezado de Deposito
                        result = CType(cmd1.ExecuteScalar, Guid)

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
                Throw New Exception("Data.TransaccionLogDataObject.InsertTransaccionLog : " + ex1.Message)
            End Try
        End Function

    End Class

End Namespace
