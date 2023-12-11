Option Explicit On
Option Strict Off

Imports DevExpress.Export
Imports DevExpress.Web
Imports DevExpress.Web.Bootstrap
Imports DevExpress.XtraPrinting
Imports SISSA.Cashflow.Web.Core.Business
Imports SISSA.Cashflow.Web.Core.Entities

Public Class DepositosReceptores
    Inherits System.Web.UI.Page

    Private _ClientesBAL As ClienteBusinessObject = New ClienteBusinessObject()
    Private _CatalogosBAL As CatalogoBusinessObject = New CatalogoBusinessObject()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            '//Se inicializan controles al cargar la pagina
            dtpFechaInicial.Date = Date.Today
            dtpFechaFinal.Date = Date.Today
            btnExportar.Enabled = False

            '//Se carga los catalogos de ayuda del filtro
            LoadCatalogosCajerosEmpresariales()
        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim dt As DataTable

        Dim _CajerosEmpresarialesBAL As CajerosEmpresarialesBusinessObject = New CajerosEmpresarialesBusinessObject()
        Dim _FilterInfo As CajerosEmpresarialesFilterEntityObject = New CajerosEmpresarialesFilterEntityObject()

        '//Se establcen parametros de filtro de consulta
        With _FilterInfo
            .FechaInicial = dtpFechaInicial.Value
            .FechaFinal = dtpFechaFinal.Value
            .ClaveCliente = IIf(cmbCliente.Value <> "todos", cmbCliente.Value, "")
            .ClaveSucursal = IIf(cmbSucursal.Value <> "todos", cmbSucursal.Value, "")
            .Tipo = cmbTipo.Value
            .Divisa = cmbDivisas.Value
        End With

        '//Se obtiene la informacion del reporte y se almacen en la variable de sesion DtAcreditaciones
        If cmbTipo.Value = "D" Then
            dt = _CajerosEmpresarialesBAL.GetTransaccionesDepositos(_FilterInfo)
        Else
            dt = _CajerosEmpresarialesBAL.GetTransaccionesRetiros(_FilterInfo)
        End If

        Session("TipoMovimiento") = IIf(cmbTipo.Value = "D", "depositos", "retiros")
        Session("DtAcreditaciones") = dt

        gridDepositos.DataBind()
        GridExporter.DataBind()

        If chkGenerarExcel.Checked Then
            Dim params() As String = New String() {Session("TipoMovimiento").ToString, Date.Now.ToString("yyyyMMdd"), Date.Now.ToString("HHmmss")}
            Dim fileName As String = String.Format("sissa_{0}_{1}_{2}", params)
            ASPxGridViewExporter.WriteXlsxToResponse(fileName, New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        End If
    End Sub

    Protected Sub cmbCliente_DataBinding(sender As Object, e As EventArgs) Handles cmbCliente.DataBinding
        cmbCliente.DataSource = CType(Session("DtClientes"), DataTable)
    End Sub

    Protected Sub cmbSucursal_DataBinding(sender As Object, e As EventArgs) Handles cmbSucursal.DataBinding
        cmbSucursal.DataSource = CType(Session("DtSucursales"), DataTable)
    End Sub

    Protected Sub cmbSucursal_Callback(sender As Object, e As CallbackEventArgsBase)
        Dim _claveCliente = e.Parameter

        If _claveCliente IsNot Nothing AndAlso _claveCliente <> "todos" Then
            Session("DtSucursales") = _ClientesBAL.GetCatalogoSucursalesByCliente(_claveCliente)
        Else
            Session("DtSucursales") = _ClientesBAL.GetCatalogoSucursales()
        End If

        cmbSucursal.DataSource = CType(Session("DtSucursales"), DataTable)
        cmbSucursal.DataBind()

        cmbSucursal.Items.Insert(0, New BootstrapListEditItem("Todos", "todos"))
        cmbSucursal.SelectedIndex = 0
    End Sub

    Protected Sub gridDepositos_Init(sender As Object, e As EventArgs) Handles gridDepositos.Init
        Dim gv As ASPxGridView = CType(sender, ASPxGridView)
        gv.SettingsText.EmptyDataRow = " "
    End Sub
    Protected Sub gridDepositos_DataBinding(sender As Object, e As EventArgs) Handles gridDepositos.DataBinding
        '//Se establece el DataSource del grid para mantenerlo durante los postback
        gridDepositos.DataSource = CType(Session("DtAcreditaciones"), DataTable)
    End Sub

    Protected Sub gridDepositos_DataBound(sender As Object, e As EventArgs) Handles gridDepositos.DataBound
        Dim gv As ASPxGridView = CType(sender, ASPxGridView)
        Dim dt = CType(gv.DataSource, DataTable)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            btnExportar.Enabled = True
        Else
            btnExportar.Enabled = False
            gv.SettingsText.EmptyDataRow = "NO SE ENCONTRO NINGUNA TRANSACCIÓN"
        End If

        'se valida si se muestra el footer
        gv.Settings.ShowFooter = (gv.PageIndex = gv.PageCount - 1)
    End Sub

    Protected Sub GridExporter_DataBinding(sender As Object, e As EventArgs) Handles GridExporter.DataBinding
        '//Se establece el DataSource del grid para mantenerlo durante los postback
        GridExporter.DataSource = CType(Session("DtAcreditaciones"), DataTable)
    End Sub

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Dim params() As String = New String() {Session("TipoMovimiento").ToString, Date.Now.ToString("yyyyMMdd"), Date.Now.ToString("HHmmss")}
        Dim fileName As String = String.Format("sissa_{0}_{1}_{2}", params)

        ASPxGridViewExporter.WriteXlsxToResponse(fileName, New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
    End Sub

#Region "Functions"
    Public Sub LoadCatalogosCajerosEmpresariales()
        Dim dtClientes = _ClientesBAL.GetCatalogoClientes
        Session("DtClientes") = dtClientes
        cmbCliente.DataBind()
        cmbCliente.Items.Insert(0, New BootstrapListEditItem("Todos", "todos"))
        cmbCliente.SelectedIndex = 0

        Dim dtSucursales = _ClientesBAL.GetCatalogoSucursales()
        Session("DtSucursales") = dtSucursales
        cmbSucursal.DataBind()
        cmbSucursal.Items.Insert(0, New BootstrapListEditItem("Todos", "todos"))
        cmbSucursal.SelectedIndex = 0

        Dim dtCajasBancarias = _CatalogosBAL.GetCatalogoCajasBancarias
        cmbBoveda.DataSource = dtCajasBancarias
        cmbBoveda.DataBind()
        cmbBoveda.Items.Insert(0, New BootstrapListEditItem("Todos", "todos"))
        cmbBoveda.SelectedIndex = 0

        cmbCajero.Items.Insert(0, New BootstrapListEditItem("Todos", "todos"))
        cmbCajero.Items.Insert(1, New BootstrapListEditItem("ND2122", "ND2122"))
        cmbCajero.SelectedIndex = 0

        cmbEstatus.Items.Insert(0, New BootstrapListEditItem("Todos", "todos"))
        cmbEstatus.Items.Insert(0, New BootstrapListEditItem("EN PROCESO", "EN PROCESO"))
        cmbEstatus.Items.Insert(0, New BootstrapListEditItem("PRE-ACREDITADO", "PRE-ACREDITADO"))
        cmbEstatus.Items.Insert(0, New BootstrapListEditItem("ACREDITADO", "ACREDITADO"))
        cmbEstatus.SelectedIndex = 0
    End Sub
#End Region
End Class