Imports System.IO
Imports System.Data
Imports DevExpress.Web
Imports DevExpress.Web.Bootstrap
Imports SISSA.Cashflow.Web.Core.Business
Imports DevExpress.XtraPrinting

Public Class ArqueoTransito1
    Inherits System.Web.UI.Page

    Dim _CajerosEmpresarialesBAL As CajerosEmpresarialesBusinessObject = New CajerosEmpresarialesBusinessObject()
    Private _CatalogosBAL As CatalogoBusinessObject = New CatalogoBusinessObject()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            '//Se inicializan controles al cargar la pagina
            dtpFecha.Date = Date.Today
            btnExportPdf.Enabled = False
            btnExportarXls.Enabled = False

            '//Se carga los catalogos de ayuda del filtro
            LoadCatalogosCajerosEmpresariales()
        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim dt0 As DataTable
        Dim dt1 As DataTable
        Dim dt2 As DataTable
        Dim dt3 As DataTable

        Dim saldo_inicial As Decimal = 0
        Dim acreditaciones As Decimal = 0
        Dim recolecciones As Decimal = 0
        Dim _crc As String = IIf(cmbBoveda.Value.ToString().ToLower = "todos", "", cmbBoveda.Value.ToString)

        Try
            dt0 = _CajerosEmpresarialesBAL.ArqueoTransitoSaldoInicial(dtpFecha.Value, _crc)
            Session("DtSaldoInicial") = dt0

            dt1 = _CajerosEmpresarialesBAL.ArqueoTransitoAcreditaciones(dtpFecha.Value, _crc)
            Session("DtAcreditaciones") = dt1
            ASPxGridViewAcreditaciones.DataBind()

            dt2 = _CajerosEmpresarialesBAL.ArqueoTransitoRecolecciones(dtpFecha.Value, _crc)
            Session("DtRecolecciones") = dt2
            ASPxGridViewRecolecciones.DataBind()

            dt3 = _CajerosEmpresarialesBAL.ArqueoTransitoGeneral(dtpFecha.Value, _crc)
            Session("DtArqueoGeneral") = dt3
            GridViewExport.DataBind()

            If dt0 IsNot Nothing AndAlso dt0.Rows.Count > 0 Then
                saldo_inicial = Convert.ToDecimal(dt0.Rows(0)("saldo_inicial"))
            End If

            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                acreditaciones = dt1.Compute("SUM(Importe)", "")
            End If

            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                recolecciones = dt2.Compute("SUM(Importe)", "")
            End If

            txtSaldoInicial.Text = saldo_inicial.ToString("c")
            txtAcreditaciones.Text = acreditaciones.ToString("c")
            txtRecolecciones.Text = recolecciones.ToString("c")
            txtSaldoFinal.Text = (saldo_inicial + acreditaciones - recolecciones).ToString("c")

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnExportPdf_Click(sender As Object, e As EventArgs) Handles btnExportPdf.Click
        Dim _fecha As Date = dtpFecha.Value

        Dim report As XtraReportArqueo = New XtraReportArqueo()
        report.Parameters("fecha").Value = _fecha

        Dim format As String = "pdf"
        Dim fileName As String = String.Format("arqueoTransitoGeneral_{0}.{1}", _fecha.ToString("yyyyMMdd"), format)

        Using ms As New MemoryStream()
            report.ExportToPdf(ms)
            WriteDocumentToResponse(ms.ToArray(), format, False, fileName)
        End Using

    End Sub

    Protected Sub btnExportarXls_Click(sender As Object, e As EventArgs) Handles btnExportarXls.Click
        Dim dt = CType(Session("DtRecolecciones"), DataTable)
        Dim exo As XlsExportOptionsEx = New XlsExportOptionsEx()

        Dim _fecha As Date = dtpFecha.Value
        Dim params() As String = New String() {"ArqueoTransitoGeneral", _fecha.ToString("yyyyMMdd")}
        Dim fileName As String = String.Format("sissa_{0}_{1}", params)

        GridViewExport.DataSource = dt

        exo.ExportType = DevExpress.Export.ExportType.WYSIWYG
        GridViewExport.ExportXlsToResponse(fileName, exo)

    End Sub

    Protected Sub ASPxGridViewAcreditaciones_Init(sender As Object, e As EventArgs) Handles ASPxGridViewAcreditaciones.Init
        Dim gv As ASPxGridView = CType(sender, ASPxGridView)
        gv.SettingsText.EmptyDataRow = " "
    End Sub

    Protected Sub ASPxGridViewAcreditaciones_DataBinding(sender As Object, e As EventArgs) Handles ASPxGridViewAcreditaciones.DataBinding
        '//Se establece el DataSource del grid para mantenerlo durante los postback
        ASPxGridViewAcreditaciones.DataSource = CType(Session("DtAcreditaciones"), DataTable)
    End Sub

    Protected Sub ASPxGridViewAcreditaciones_DataBound(sender As Object, e As EventArgs) Handles ASPxGridViewAcreditaciones.DataBound
        Dim dt1 = CType(ASPxGridViewAcreditaciones.DataSource, DataTable)
        Dim dt2 = CType(ASPxGridViewRecolecciones.DataSource, DataTable)

        If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Or (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
            btnExportarXls.Enabled = True
            btnExportPdf.Enabled = True
        Else
            'btnExportarXls.Enabled = False
            ASPxGridViewAcreditaciones.SettingsText.EmptyDataRow = "NO SE ENCONTRO NINGUN ARCHIVO"
        End If
    End Sub

    Protected Sub ASPxGridViewRecolecciones_Init(sender As Object, e As EventArgs) Handles ASPxGridViewRecolecciones.Init
        Dim gv As ASPxGridView = CType(sender, ASPxGridView)
        gv.SettingsText.EmptyDataRow = " "
    End Sub

    Protected Sub ASPxGridViewRecolecciones_DataBinding(sender As Object, e As EventArgs) Handles ASPxGridViewRecolecciones.DataBinding
        '//Se establece el DataSource del grid para mantenerlo durante los postback
        ASPxGridViewRecolecciones.DataSource = CType(Session("DtRecolecciones"), DataTable)
    End Sub

    Protected Sub ASPxGridViewRecolecciones_DataBound(sender As Object, e As EventArgs) Handles ASPxGridViewRecolecciones.DataBound
        Dim dt1 = CType(ASPxGridViewAcreditaciones.DataSource, DataTable)
        Dim dt2 = CType(ASPxGridViewRecolecciones.DataSource, DataTable)

        If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Or (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
            btnExportarXls.Enabled = True
        Else
            btnExportarXls.Enabled = False
            ASPxGridViewRecolecciones.SettingsText.EmptyDataRow = "NO SE ENCONTRO NINGUNA TRANSACCIÓN"
        End If
    End Sub

    Protected Sub GridViewExport_DataBinding(sender As Object, e As EventArgs) Handles GridViewExport.DataBinding
        GridViewExport.DataSource = CType(Session("DtArqueoGeneral"), DataTable)
    End Sub

#Region "Functions"
    Private Sub LoadCatalogosCajerosEmpresariales()
        Dim dtCajasBancarias = _CatalogosBAL.GetCatalogoCajasBancarias
        cmbBoveda.DataSource = dtCajasBancarias
        cmbBoveda.DataBind()
        cmbBoveda.Items.Insert(0, New BootstrapListEditItem("Todos", "todos"))
        cmbBoveda.SelectedIndex = 0
    End Sub

    Private Sub WriteDocumentToResponse(ByVal documentData() As Byte, ByVal format As String, ByVal isInline As Boolean, ByVal fileName As String)
        Dim contentType_Renamed As String
        Dim disposition As String = If(isInline, "inline", "attachment")

        Select Case format.ToLower()
            Case "xls"
                contentType_Renamed = "application/vnd.ms-excel"
            Case "xlsx"
                contentType_Renamed = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Case "mht"
                contentType_Renamed = "message/rfc822"
            Case "html"
                contentType_Renamed = "text/html"
            Case "txt", "csv"
                contentType_Renamed = "text/plain"
            Case "png"
                contentType_Renamed = "image/png"
            Case Else
                contentType_Renamed = String.Format("application/{0}", format)
        End Select

        Response.Clear()
        Response.ContentType = contentType_Renamed
        Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", disposition, fileName))
        Response.BinaryWrite(documentData)
        Response.End()
    End Sub

#End Region

End Class