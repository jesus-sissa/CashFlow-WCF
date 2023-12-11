Imports DevExpress.Export
Imports DevExpress.Web
Imports DevExpress.Web.Bootstrap
Imports DevExpress.XtraPrinting
Imports SISSA.Cashflow.Web.Core.Business
Imports SISSA.Cashflow.Web.Core.Entities

Public Class MonitorArchivos
    Inherits System.Web.UI.Page

    Private _CatalogosBAL As CatalogoBusinessObject = New CatalogoBusinessObject()

#Region "Properties"
    Private Property CurrentAcreditacionID() As String
        Get
            Return If(Session("AcreditacionID") Is Nothing, String.Empty, Session("AcreditacionID").ToString())
        End Get
        Set(ByVal value As String)
            Session("AcreditacionID") = value
        End Set
    End Property
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            '//Se inicializan controles al cargar la pagina
            dtpFechaInicial.Date = Date.Today
            dtpFechaFinal.Date = Date.Today
            btnExportar.Enabled = False

            '//Se inicializa control de id
            CurrentAcreditacionID = String.Empty

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
            .Boveda = IIf(cmbBoveda.Value <> "todos", cmbBoveda.Value, "")
            .FechaInicial = dtpFechaInicial.Value
            .FechaFinal = dtpFechaFinal.Value
        End With

        '//Se obtiene la informacion del reporte y se almacen en la variable de sesion DtBaseCajeros
        dt = _CajerosEmpresarialesBAL.GetMonitorArchivos(_FilterInfo)
        Session("DtMonitorMaster") = dt

        gridMonitor.DataBind()

        If chkGenerarExcel.Checked Then
            ASPxGridViewExporter.WriteXlsxToResponse(String.Format("sissa_monitorArchivos_{0}_{1}", Date.Now.ToString("yyyyMMdd"), Date.Now.ToString("HHmmss")), New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        End If
    End Sub
    Protected Sub gridMonitor_Init(sender As Object, e As EventArgs) Handles gridMonitor.Init
        Dim gv As ASPxGridView = CType(sender, ASPxGridView)
        gv.SettingsText.EmptyDataRow = " "
    End Sub
    Protected Sub gridMonitor_DataBinding(sender As Object, e As EventArgs) Handles gridMonitor.DataBinding
        '//Se establece el DataSource del grid para mantenerlo durante los postback
        gridMonitor.DataSource = CType(Session("DtMonitorMaster"), DataTable)
    End Sub

    Protected Sub gridMonitor_DataBound(sender As Object, e As EventArgs) Handles gridMonitor.DataBound
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

    Protected Sub gridDetalle_Init(sender As Object, e As EventArgs)
        DetailsApply()
    End Sub

    Protected Sub gridDetalle_DataBinding(sender As Object, e As EventArgs) Handles gridDetalle.DataBinding
        '//Se establece el DataSource del grid para mantenerlo durante los postback
        gridDetalle.DataSource = CType(Session("DtMonitorDetalle"), DataTable)
    End Sub

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        ASPxGridViewExporter.WriteXlsxToResponse(String.Format("sissa_monitorArchivos_{0}_{1}", Date.Now.ToString("yyyyMMdd"), Date.Now.ToString("HHmmss")), New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
    End Sub

    Protected Sub btnDetail_Load(sender As Object, e As EventArgs)
        Dim btn As ASPxButton = sender
        Dim container As GridViewDataItemTemplateContainer = btn.NamingContainer
        Dim acreditacionID As String = DataBinder.Eval(container.DataItem, "Id_Acreditacion").ToString()
        btn.ClientSideEvents.Click = String.Format("function (s, e) {{ OnClick(s, e, {0}) }}", acreditacionID)
    End Sub

    Protected Sub Popup_WindowCallback(source As Object, e As PopupWindowCallbackArgs)
        CurrentAcreditacionID = e.Parameter
        DetailsApply()
    End Sub

#Region "Functions"
    Public Sub LoadCatalogosCajerosEmpresariales()
        Dim dtCajasBancarias = _CatalogosBAL.GetCatalogoCajasBancarias
        cmbBoveda.DataSource = dtCajasBancarias
        cmbBoveda.DataBind()
        cmbBoveda.Items.Insert(0, New BootstrapListEditItem("Todos", "todos"))
        cmbBoveda.SelectedIndex = 0
    End Sub

    Private Sub DetailsApply()
        Dim dt As DataTable
        Dim _CajerosEmpresarialesBAL As CajerosEmpresarialesBusinessObject

        If (Not String.IsNullOrEmpty(CurrentAcreditacionID)) Then
            _CajerosEmpresarialesBAL = New CajerosEmpresarialesBusinessObject()
            dt = _CajerosEmpresarialesBAL.GetMonitorArchivosDetalle(CInt(CurrentAcreditacionID))

            Session("DtMonitorDetalle") = dt
            gridDetalle.DataBind()
        End If
    End Sub















#End Region
End Class