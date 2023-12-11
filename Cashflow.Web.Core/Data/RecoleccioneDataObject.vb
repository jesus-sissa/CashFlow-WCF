Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Entities
Imports SISSA.Cashflow.Web.Core.Data.Interfaces
Imports System.Data.SqlClient
Imports System.Text

Namespace Data
    Public Class RecoleccioneDataObject
        Inherits BaseDataObject

        Public Function InsertRecoleccion(RecoleccionObj As RecoleccionEntityObject)
            Dim storeProcedureHeader As String = "CE_InsertRecoleccion"

            Dim params() As String
            Dim values() As Object
            Dim result As Object

            Dim cmd As SqlCommand
            '//Dim cmd2 As SqlCommand

            Dim transaction As SqlTransaction

            Try
                Using conn As SqlConnection = New SqlConnection(Me.ConnectionString)
                    '//Se abre la conexion a la BD
                    conn.Open()

                    '//Se crea comando sql de ejecucion
                    cmd = conn.CreateCommand
                    '//cmd2 = conn.CreateCommand

                    '//Se crea la transaccion sql local
                    transaction = conn.BeginTransaction("InsertRecoleccion")

                    cmd = New SqlCommand(storeProcedureHeader, conn, transaction)

                    Try
                        '//Se prepara la insercion del registro de encabezado de la recoleccion
                        params = New String() {"@fecha", "@fechaTransmision", "@fechaAcreditacion", "@monto", "@archivo"}
                        values = New Object() {RecoleccionObj.Fecha, RecoleccionObj.Fecha_Transmision, RecoleccionObj.Fecha_Acreditacion, RecoleccionObj.Importe, RecoleccionObj.Archivo}

                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandTimeout = 480
                        '//Se establcen los parametros del Store Procedure
                        SetCommandParameters(cmd, params, values)

                        '//Se ejecuta comendo de insert encabezado de Deposito
                        result = cmd.ExecuteScalar()

                        For Each mov As RecoleccionDetailEntityObject In RecoleccionObj.Depositos
                            'InsertRecoleccionDetail(Convert.ToDecimal(result), mov)
                            InsertRecoleccionD(result.ToString(), mov)
                        Next

                        transaction.Commit()
                        conn.Close()

                    Catch ex2 As Exception
                        Try
                            transaction.Rollback()
                            conn.Close()
                            Throw ex2
                        Catch ex3 As Exception
                            Throw ex3
                        End Try
                    End Try


                End Using
            Catch ex1 As Exception
                Tools.CashflowEventLog.WriteToEventLog(ex1.Message, "Cashflow", EventLogEntryType.Error)
                Throw New Exception("Data.RecoleccioneDataObject.InsertRecoleccion : " + ex1.Message)
            End Try
        End Function

        Private Function InsertRecoleccionD(idrecoleccion As String, RecolecionD As RecoleccionDetailEntityObject)
            Dim storeProcedureDetail As String = "CE_InsertRecoleccionDetail"

            Dim params() As String
            Dim values() As Object

            Dim cmd2 As SqlCommand

            Dim transaction As SqlTransaction

            Try
                Using conn As SqlConnection = New SqlConnection(Me.ConnectionString)
                    '//Se abre la conexion a la BD
                    conn.Open()

                    '//Se crea comando sql de ejecucion
                    cmd2 = conn.CreateCommand

                    '//Se crea la transaccion sql local
                    transaction = conn.BeginTransaction("InsertRecoleccionDetail")

                    cmd2 = New SqlCommand(storeProcedureDetail, conn, transaction)

                    Try
                        '//Se prepara la insercion del registro de encabezado de la recoleccion
                        params = New String() {"@Id_Recoleccion", "@ClaveCliente", "@ClaveSucursal", "@ClaveBanco", "@CRC", "@Cuenta", "@Divisa", "@Tipo", "@Monto", "@Remision"}
                        values = New Object() {idrecoleccion, RecolecionD.Clave_Cliente, RecolecionD.Clave_Sucursal, RecolecionD.Clave_Banco, RecolecionD.CRC, RecolecionD.Cuenta, RecolecionD.Divisa, "R", RecolecionD.Monto, RecolecionD.Remision}

                        cmd2.CommandType = CommandType.StoredProcedure
                        cmd2.CommandTimeout = 480
                        '//Se establcen los parametros del Store Procedure
                        SetCommandParameters(cmd2, params, values)

                        '//Se ejecuta comendo de insert encabezado de Deposito
                        cmd2.ExecuteScalar()

                        transaction.Commit()
                        conn.Close()

                    Catch ex2 As Exception
                        Try
                            transaction.Rollback()
                            conn.Close()
                            Throw ex2
                        Catch ex3 As Exception
                            Throw ex3
                        End Try
                    End Try


                End Using
            Catch ex1 As Exception
                Tools.CashflowEventLog.WriteToEventLog(ex1.Message, "Cashflow", EventLogEntryType.Error)
                Throw New Exception("Data.RecoleccioneDataObject.InsertRecoleccion : " + ex1.Message)
            End Try

        End Function

        Public Function CheckDivisafromCuenta(cuenta As String, divisa As Int32) As Boolean
            Dim correcto As Boolean
            correcto = False
            Dim storeProcedureDetail As String = "CE_QueryDivisaNumeroCuenta"

            Dim params() As String
            Dim values() As Object

            Dim cmd3 As SqlCommand

            Dim transaction As SqlTransaction
            Try
                Using conn As SqlConnection = New SqlConnection(Me.ConnectionString)
                    '//Se abre la conexion a la BD
                    conn.Open()

                    '//Se crea comando sql de ejecucion
                    cmd3 = conn.CreateCommand

                    '//Se crea la transaccion sql local
                    transaction = conn.BeginTransaction("ConsultaDivisaxcuenta")

                    cmd3 = New SqlCommand(storeProcedureDetail, conn, transaction)

                    Try
                        '//Se prepara la insercion del registro de encabezado de la recoleccion
                        params = New String() {"@nocuenta"}
                        values = New Object() {cuenta}

                        cmd3.CommandType = CommandType.StoredProcedure
                        cmd3.CommandTimeout = 480
                        '//Se establcen los parametros del Store Procedure
                        SetCommandParameters(cmd3, params, values)

                        '//Se ejecuta comendo de insert encabezado de Deposito
                        '//cmd3.ExecuteScalar()
                        Dim reader As SqlDataReader
                        reader = cmd3.ExecuteReader()

                        If (reader.Read()) Then
                            Dim vDivisa As Int32
                            vDivisa = Convert.ToInt32(reader(0).ToString())
                            If (divisa = vDivisa) Then
                                correcto = True
                            End If
                        End If
                        reader.Close()
                        transaction.Commit()
                        conn.Close()

                        Return correcto

                    Catch ex2 As Exception
                        Try
                            transaction.Rollback()
                            conn.Close()
                            Return correcto
                            Throw ex2
                        Catch ex3 As Exception
                            Throw ex3
                        End Try
                    End Try
                End Using
            Catch ex1 As Exception
                Return correcto
                Tools.CashflowEventLog.WriteToEventLog(ex1.Message, "Cashflow", EventLogEntryType.Error)
                Throw New Exception("Data.RecoleccioneDataObject.InsertRecoleccion : " + ex1.Message)
            End Try
        End Function

    End Class
End Namespace

