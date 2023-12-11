Imports System.Data.SqlClient
Imports System.Web.Services
Imports ConexionesDatos.cls_Datos

Public Class ConsultaLog
    Inherits PaginaBase

#Region "-Variables"


    Public Function CrearEstructuras() As DataTable
        Dim dtLog As New DataTable("Log")
        dtLog.Columns.Add("IdLog", GetType(Integer))
        dtLog.Columns.Add("IdCajero", GetType(String))
        dtLog.Columns.Add("NombreSuc", GetType(String))
        dtLog.Columns.Add("Descripcion", GetType(String))
        dtLog.Columns.Add("NombrePantalla", GetType(String))
        dtLog.Columns.Add("Fecha", GetType(DateTime))
        dtLog.Columns.Add("Hora", GetType(String))
        dtLog.Columns.Add("DescripcionConcat", GetType(String))
        Return dtLog
    End Function



#End Region


#Region "-Inicio"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then Exit Sub
        Call cn.fn_Crear_Log("PAGINA: CONSULTA DE LOG")
        tbx_FechaFin.Text = Format(Date.Now(), "dd/MM/yyyy")
        tbx_FechaInicio.Text = Format(Date.Now(), "dd/MM/yyyy")
        Dim dt_Sucursales As DataTable = cn.fn_ConsultaSucursales()
        fn_LlenarDDL(ddl_Sucursales, dt_Sucursales, "NombreSucursal", "Clave", "0")
        MostrarGridVacio()
    End Sub

#End Region


#Region "-Metodos o Funciones"

    <WebMethod()>
    Public Shared Function GetDescripcionLog(ByVal Descripcion As String) As List(Of String)

        Dim DescripList As List(Of String) = New List(Of String)()
        Dim rd As SqlDataReader
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand
        Try
            cnn = Crea_ConexionSTD()
            cnn.Open()
            cmd = Crea_Comando("[CashflowWEB_GetDescripcionLog]", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Descripcion", SqlDbType.VarChar, Descripcion)
            rd = cmd.ExecuteReader()
            While (rd.Read())
                DescripList.Add(rd("Descripcion").ToString())
            End While
            rd.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString())
            cnn.Close()
        End Try
        Return DescripList
    End Function

    Private Sub GetMovimientosLog()
        ConsultarLog()
    End Sub
    Private Sub Limpiar()
        Call MostrarGridVacio()
        gv_Log.SelectedIndex = -1
    End Sub

    Sub MostrarGridVacio()
        gv_Log.DataSource = fn_CreaGridVacio("IdLog,IdCajero,NombreSuc,Descripcion,Fecha,Hora,DescripcionConcat")
        gv_Log.DataBind()
    End Sub

    Private Sub ConsultarLog()

        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand
        Dim dt As DataTable = New DataTable()

        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("[CashflowWEB_ConsultaLog]", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@FechaInicio", SqlDbType.DateTime, tbx_FechaInicio.Text)
            Crea_Parametro(cmd, "@FechaFinal", SqlDbType.DateTime, tbx_FechaFin.Text)
            Crea_Parametro(cmd, "@ClaveSucursal", SqlDbType.VarChar, IIf(chkSucursal.Checked = True, DBNull.Value, ddl_Sucursales.SelectedValue))
            Crea_Parametro(cmd, "@IdDescripcion", SqlDbType.VarChar, IIf(ChkDescripcion.Checked = True, "", TxtDescripcion.Text))
            dt = EjecutaConsulta(cmd)
            If dt.Rows.Count = 0 Then
                fn_Alerta("No se encontraron Registros con los Datos especificados.")
            End If
            gv_Log.DataSource = dt
            gv_Log.DataBind()
        Catch ex As Exception
            fn_Alerta(ex.ToString())
        End Try
    End Sub

    Protected Sub btn_Mostrar_Click(sender As Object, e As EventArgs) Handles btn_Mostrar.Click
        GetMovimientosLog()
    End Sub

    Protected Sub btn_Limpiar_Click(sender As Object, e As EventArgs) Handles btn_Limpiar.Click
        tbx_FechaFin.Text = Format(Date.Now(), "dd/MM/yyyy")
        tbx_FechaInicio.Text = Format(Date.Now(), "dd/MM/yyyy")
        ddl_Sucursales.SelectedValue = "0"
        chkSucursal.Checked = False
        ChkDescripcion.Checked = False
        MostrarGridVacio()
        ddl_Sucursales.Enabled = True
        TxtDescripcion.Enabled = True
    End Sub

    Protected Sub chkSucursal_CheckedChanged(sender As Object, e As EventArgs) Handles chkSucursal.CheckedChanged
        If chkSucursal.Checked = True Then
            ddl_Sucursales.SelectedValue = "0"
            ddl_Sucursales.Enabled = False
        Else
            ddl_Sucursales.Enabled = True
        End If
    End Sub

    Protected Sub ChkDescripcion_CheckedChanged(sender As Object, e As EventArgs) Handles ChkDescripcion.CheckedChanged
        If ChkDescripcion.Checked = True Then
            TxtDescripcion.Text = ""
            TxtDescripcion.Enabled = False
        Else
            TxtDescripcion.Enabled = True
        End If
    End Sub
#End Region
End Class