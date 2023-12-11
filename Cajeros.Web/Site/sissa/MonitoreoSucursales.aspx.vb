Option Explicit On
Option Strict Off

Public Class MonitoreoSucursales
    Inherits PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then Exit Sub
        Call cn.fn_Crear_Log("PAGINA: MONITOREO DE SUCURSALES")
        Session("PaginaActual") = 0

        Call Monitoreo_Sucursales()
    End Sub


    Private Sub Monitoreo_Sucursales()
        '3Minutos es predefinido
        'Dim dt_conexiones As DataTable = cn.fn_Sucursales_Monitoreo(3)
        Dim dt_conexiones As DataTable = Me.cn.fn_GetConexionMonitoreo()
        If dt_conexiones IsNot Nothing Then
            For Each iR As DataRow In dt_conexiones.Rows
                If Convert.ToInt32(iR("Minutos")) > 30 Then
                    CnMail.EnviarCorreo("CASH FLOW WEB MONITORING - SUCURSAL " + iR("NombreSucursal").ToString() + ", SIN CONEXIÓN.",
                                          "<strong> LA SUCURSAL " + iR("NombreSucursal") + "</strong> <br/>" +
                                          " lleva sin conexion a internet mas de <strong>" + iR("Minutos").ToString() + " minutos</strong>, su ultima conexion fue: <strong>" + iR("Ultima_Conexion").ToString() + " con " +
                                          iR("HUltima_Conexion").ToString() + "</strong><br/> favor de contactar directamente a la sucursal lo mas pronto posible para restablecer conexion.<br/>
                                            saludos y buen dia!")
                End If
            Next
            Tabla("tbl_conexiones") = dt_conexiones
            Call PaginadoDatalist()
        End If
    End Sub


    Private Sub PaginadoDatalist()
        Dim pgd As PagedDataSource = New PagedDataSource

        pgd.DataSource = Tabla("tbl_conexiones").DefaultView
        pgd.CurrentPageIndex = Session("PaginaActual")
        pgd.AllowPaging = True
        ' * Ya no se toma en cuenta la propiedad {RepeatColumns} repeticion de columnas.
        pgd.PageSize = 55 'Nº de elementos a mostrar*.
        ' 
        ' lkb_Siguiente.Enabled = (pgd.IsLastPage = False)
        'lkb_Anterior.Enabled = (pgd.IsFirstPage = False)
        dl_Sucursales.DataSource = pgd
        dl_Sucursales.DataBind()

    End Sub

    Private Function MostrarDatosSucursal(ByVal ClaveSucursal As String) As Boolean
        Dim dt_Detalle As DataTable = cn.fn_Sucursales_MonitoreoDetalle(ClaveSucursal)
        If dt_Detalle Is Nothing OrElse dt_Detalle.Rows.Count = 0 Then Return False

        If dt_Detalle.Rows.Count > 0 Then
            tbx_Sucursal.Text = dt_Detalle.Rows(0)("Nombre_corto")
            tbx_Telefono.Text = dt_Detalle.Rows(0)("Telefono")
            tbx_Domicilio.Text = dt_Detalle.Rows(0)("Domicilio")
            tbx_Tiempo.Text = dt_Detalle.Rows(0)("TiempoDesconexion")
        End If

        Return True
    End Function


    Private Sub dl_Sucursales_ItemCommand(source As Object, e As DataListCommandEventArgs) Handles dl_Sucursales.ItemCommand
        If e.CommandName = "Mostrar" Then
            'Call Limpiar()
            If MostrarDatosSucursal(dl_Sucursales.DataKeys(e.Item.ItemIndex)) Then
                '   mpeEditOrder.Show()
                ScriptManager.RegisterClientScriptBlock(Me, Me.Page.GetType(), "key_modalData", "showModalData();", True)
            End If
        End If
    End Sub
End Class