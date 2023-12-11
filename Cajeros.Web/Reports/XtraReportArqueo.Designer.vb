<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XtraReportArqueo
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XtraReportArqueo))
        Dim StoredProcQuery1 As DevExpress.DataAccess.Sql.StoredProcQuery = New DevExpress.DataAccess.Sql.StoredProcQuery()
        Dim QueryParameter1 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter2 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim StoredProcQuery2 As DevExpress.DataAccess.Sql.StoredProcQuery = New DevExpress.DataAccess.Sql.StoredProcQuery()
        Dim QueryParameter3 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter4 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim StoredProcQuery3 As DevExpress.DataAccess.Sql.StoredProcQuery = New DevExpress.DataAccess.Sql.StoredProcQuery()
        Dim QueryParameter5 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter6 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.xrSubreportSalidas = New DevExpress.XtraReports.UI.XRSubreport()
        Me.fecha = New DevExpress.XtraReports.Parameters.Parameter()
        Me.xrSubreportEntradas = New DevExpress.XtraReports.UI.XRSubreport()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.subPageHeader_a = New DevExpress.XtraReports.UI.SubBand()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.picLogoBanorte = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Text3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Text2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Text1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.picLogoSissa = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.subPageHeader_b = New DevExpress.XtraReports.UI.SubBand()
        Me.Text5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Text4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.subPageFooter_a = New DevExpress.XtraReports.UI.SubBand()
        Me.xrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.xrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.subPageFooter_b = New DevExpress.XtraReports.UI.SubBand()
        Me.xrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.subReportFotter_b = New DevExpress.XtraReports.UI.SubBand()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine4 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.sqlDataSourceArqueo = New DevExpress.DataAccess.Sql.SqlDataSource(Me.components)
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 24.00001!
        Me.TopMargin.Name = "TopMargin"
        '
        'BottomMargin
        '
        Me.BottomMargin.Name = "BottomMargin"
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrSubreportSalidas, Me.xrSubreportEntradas})
        Me.Detail.HeightF = 56.41683!
        Me.Detail.Name = "Detail"
        '
        'xrSubreportSalidas
        '
        Me.xrSubreportSalidas.LocationFloat = New DevExpress.Utils.PointFloat(383.3333!, 0!)
        Me.xrSubreportSalidas.Name = "xrSubreportSalidas"
        Me.xrSubreportSalidas.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("fecha", Me.fecha))
        Me.xrSubreportSalidas.ReportSource = New SISSA.Cashflow.Web.XtraReportDetailRecolecciones()
        Me.xrSubreportSalidas.SizeF = New System.Drawing.SizeF(406.0417!, 43.83335!)
        '
        'fecha
        '
        Me.fecha.Name = "fecha"
        Me.fecha.Type = GetType(Date)
        Me.fecha.ValueInfo = "1753-01-01"
        '
        'xrSubreportEntradas
        '
        Me.xrSubreportEntradas.LocationFloat = New DevExpress.Utils.PointFloat(10.00004!, 0!)
        Me.xrSubreportEntradas.Name = "xrSubreportEntradas"
        Me.xrSubreportEntradas.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("fecha", Me.fecha))
        Me.xrSubreportEntradas.ReportSource = New SISSA.Cashflow.Web.XtraReportDetailAcreditaciones()
        Me.xrSubreportEntradas.SizeF = New System.Drawing.SizeF(358.1251!, 43.83335!)
        '
        'PageHeader
        '
        Me.PageHeader.HeightF = 0!
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.SubBands.AddRange(New DevExpress.XtraReports.UI.SubBand() {Me.subPageHeader_a, Me.subPageHeader_b})
        '
        'subPageHeader_a
        '
        Me.subPageHeader_a.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel10, Me.XrLabel9, Me.picLogoBanorte, Me.XrLabel1, Me.Text3, Me.Text2, Me.Text1, Me.picLogoSissa})
        Me.subPageHeader_a.HeightF = 212.4166!
        Me.subPageHeader_a.Name = "subPageHeader_a"
        '
        'XrLabel10
        '
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(610.625!, 79.1667!)
        Me.XrLabel10.Multiline = True
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(57.29169!, 23.0!)
        Me.XrLabel10.Text = "Fecha:"
        '
        'XrLabel9
        '
        Me.XrLabel9.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "?fecha")})
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(667.9167!, 79.1667!)
        Me.XrLabel9.Multiline = True
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(112.0832!, 23.0!)
        Me.XrLabel9.Text = "XrLabel9"
        Me.XrLabel9.TextFormatString = "{0:dd/MM/yyyy}"
        '
        'picLogoBanorte
        '
        Me.picLogoBanorte.ImageSource = New DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("picLogoBanorte.ImageSource"))
        Me.picLogoBanorte.LocationFloat = New DevExpress.Utils.PointFloat(524.1667!, 0!)
        Me.picLogoBanorte.Name = "picLogoBanorte"
        Me.picLogoBanorte.SizeF = New System.Drawing.SizeF(265.2083!, 79.1667!)
        '
        'XrLabel1
        '
        Me.XrLabel1.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CE_GetArqueoTransitoSaldos].[saldo_inicial]")})
        Me.XrLabel1.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(127.7084!, 175.0!)
        Me.XrLabel1.Multiline = True
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(182.2917!, 16.66667!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.Text = "XrLabel1"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrLabel1.TextFormatString = "{0:c2}"
        '
        'Text3
        '
        Me.Text3.BackColor = System.Drawing.Color.White
        Me.Text3.BorderColor = System.Drawing.Color.Black
        Me.Text3.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.Text3.BorderWidth = 1.0!
        Me.Text3.CanGrow = False
        Me.Text3.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Text3.ForeColor = System.Drawing.Color.Black
        Me.Text3.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 175.0!)
        Me.Text3.Name = "Text3"
        Me.Text3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.Text3.SizeF = New System.Drawing.SizeF(117.7084!, 16.66667!)
        Me.Text3.StylePriority.UseFont = False
        Me.Text3.StylePriority.UseTextAlignment = False
        Me.Text3.Text = "Saldo inicial:"
        Me.Text3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'Text2
        '
        Me.Text2.BackColor = System.Drawing.Color.White
        Me.Text2.BorderColor = System.Drawing.Color.Black
        Me.Text2.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.Text2.BorderWidth = 1.0!
        Me.Text2.CanGrow = False
        Me.Text2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Text2.ForeColor = System.Drawing.Color.Black
        Me.Text2.LocationFloat = New DevExpress.Utils.PointFloat(224.3751!, 127.0833!)
        Me.Text2.Name = "Text2"
        Me.Text2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.Text2.SizeF = New System.Drawing.SizeF(362.2917!, 18.75!)
        Me.Text2.StylePriority.UseFont = False
        Me.Text2.Text = "Sucursal: MTY"
        Me.Text2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'Text1
        '
        Me.Text1.BackColor = System.Drawing.Color.White
        Me.Text1.BorderColor = System.Drawing.Color.Black
        Me.Text1.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.Text1.BorderWidth = 1.0!
        Me.Text1.CanGrow = False
        Me.Text1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Text1.ForeColor = System.Drawing.Color.Black
        Me.Text1.LocationFloat = New DevExpress.Utils.PointFloat(224.3751!, 106.25!)
        Me.Text1.Multiline = True
        Me.Text1.Name = "Text1"
        Me.Text1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.Text1.SizeF = New System.Drawing.SizeF(362.2917!, 20.83331!)
        Me.Text1.StylePriority.UseFont = False
        Me.Text1.Text = "Arqueo Virtual - CR8072"
        Me.Text1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'picLogoSissa
        '
        Me.picLogoSissa.BackColor = System.Drawing.Color.White
        Me.picLogoSissa.BorderColor = System.Drawing.Color.Black
        Me.picLogoSissa.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.picLogoSissa.BorderWidth = 1.0!
        Me.picLogoSissa.Font = New System.Drawing.Font("Times New Roman", 9.75!)
        Me.picLogoSissa.ForeColor = System.Drawing.Color.Black
        Me.picLogoSissa.ImageSource = New DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("picLogoSissa.ImageSource"))
        Me.picLogoSissa.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.picLogoSissa.Name = "picLogoSissa"
        Me.picLogoSissa.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.picLogoSissa.SizeF = New System.Drawing.SizeF(316.0417!, 79.1667!)
        '
        'subPageHeader_b
        '
        Me.subPageHeader_b.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.Text5, Me.Text4})
        Me.subPageHeader_b.HeightF = 36.37498!
        Me.subPageHeader_b.Name = "subPageHeader_b"
        '
        'Text5
        '
        Me.Text5.BackColor = System.Drawing.Color.White
        Me.Text5.BorderColor = System.Drawing.Color.Black
        Me.Text5.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.Text5.BorderWidth = 1.0!
        Me.Text5.CanGrow = False
        Me.Text5.Font = New System.Drawing.Font("Arial", 11.0!)
        Me.Text5.ForeColor = System.Drawing.Color.Black
        Me.Text5.LocationFloat = New DevExpress.Utils.PointFloat(383.3333!, 10.00001!)
        Me.Text5.Name = "Text5"
        Me.Text5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.Text5.SizeF = New System.Drawing.SizeF(358.1251!, 16.66666!)
        Me.Text5.StylePriority.UseTextAlignment = False
        Me.Text5.Text = "Detalle salidas:"
        Me.Text5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'Text4
        '
        Me.Text4.BackColor = System.Drawing.Color.White
        Me.Text4.BorderColor = System.Drawing.Color.Black
        Me.Text4.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.Text4.BorderWidth = 1.0!
        Me.Text4.CanGrow = False
        Me.Text4.Font = New System.Drawing.Font("Arial", 11.0!)
        Me.Text4.ForeColor = System.Drawing.Color.Black
        Me.Text4.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 10.00001!)
        Me.Text4.Name = "Text4"
        Me.Text4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.Text4.SizeF = New System.Drawing.SizeF(358.1251!, 16.66666!)
        Me.Text4.StylePriority.UseTextAlignment = False
        Me.Text4.Text = "Detalle entradas:"
        Me.Text4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'PageFooter
        '
        Me.PageFooter.HeightF = 0!
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.SubBands.AddRange(New DevExpress.XtraReports.UI.SubBand() {Me.subPageFooter_a, Me.subPageFooter_b})
        '
        'subPageFooter_a
        '
        Me.subPageFooter_a.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrLabel5, Me.xrLabel4, Me.xrLine2, Me.xrLine1})
        Me.subPageFooter_a.Name = "subPageFooter_a"
        '
        'xrLabel5
        '
        Me.xrLabel5.BackColor = System.Drawing.Color.White
        Me.xrLabel5.BorderColor = System.Drawing.Color.Black
        Me.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.xrLabel5.BorderWidth = 1.0!
        Me.xrLabel5.CanGrow = False
        Me.xrLabel5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xrLabel5.ForeColor = System.Drawing.Color.Black
        Me.xrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(429.9999!, 83.33334!)
        Me.xrLabel5.Name = "xrLabel5"
        Me.xrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel5.SizeF = New System.Drawing.SizeF(349.9999!, 16.66667!)
        Me.xrLabel5.StylePriority.UseFont = False
        Me.xrLabel5.StylePriority.UseTextAlignment = False
        Me.xrLabel5.Text = "JEFE DE PROCESO"
        Me.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'xrLabel4
        '
        Me.xrLabel4.BackColor = System.Drawing.Color.White
        Me.xrLabel4.BorderColor = System.Drawing.Color.Black
        Me.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.xrLabel4.BorderWidth = 1.0!
        Me.xrLabel4.CanGrow = False
        Me.xrLabel4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xrLabel4.ForeColor = System.Drawing.Color.Black
        Me.xrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 83.33334!)
        Me.xrLabel4.Name = "xrLabel4"
        Me.xrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel4.SizeF = New System.Drawing.SizeF(338.5417!, 16.66667!)
        Me.xrLabel4.StylePriority.UseFont = False
        Me.xrLabel4.StylePriority.UseTextAlignment = False
        Me.xrLabel4.Text = "" & Global.Microsoft.VisualBasic.ChrW(9) & "ADMINISTRADOR CAJA GENERAL"
        Me.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify
        '
        'xrLine2
        '
        Me.xrLine2.LocationFloat = New DevExpress.Utils.PointFloat(429.9998!, 73.95834!)
        Me.xrLine2.Name = "xrLine2"
        Me.xrLine2.SizeF = New System.Drawing.SizeF(350.0!, 9.375!)
        '
        'xrLine1
        '
        Me.xrLine1.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 73.95834!)
        Me.xrLine1.Name = "xrLine1"
        Me.xrLine1.SizeF = New System.Drawing.SizeF(350.0!, 9.375!)
        '
        'subPageFooter_b
        '
        Me.subPageFooter_b.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrLabel3})
        Me.subPageFooter_b.Name = "subPageFooter_b"
        '
        'xrLabel3
        '
        Me.xrLabel3.BackColor = System.Drawing.Color.White
        Me.xrLabel3.BorderColor = System.Drawing.Color.Black
        Me.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.xrLabel3.BorderWidth = 1.0!
        Me.xrLabel3.CanGrow = False
        Me.xrLabel3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xrLabel3.ForeColor = System.Drawing.Color.Black
        Me.xrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 10.00001!)
        Me.xrLabel3.Name = "xrLabel3"
        Me.xrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel3.SizeF = New System.Drawing.SizeF(769.9999!, 35.20839!)
        Me.xrLabel3.StylePriority.UseFont = False
        Me.xrLabel3.Text = "CERTIFICAMOS QUE LAS EXISTENCIAS FÍSICAS ARRIBA DECLARADAS SE ENCUENTRAN EN CUSTO" &
    "DIA EN ESTA COMPAÑÍA Y QUE SON PROPIEDAD ENTERAMENTE DE BANCO MERCANTIL DEL NORT" &
    "E"
        Me.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'ReportFooter
        '
        Me.ReportFooter.HeightF = 0!
        Me.ReportFooter.Name = "ReportFooter"
        Me.ReportFooter.SubBands.AddRange(New DevExpress.XtraReports.UI.SubBand() {Me.subReportFotter_b})
        '
        'subReportFotter_b
        '
        Me.subReportFotter_b.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel12, Me.XrLabel11, Me.XrLabel8, Me.XrLabel7, Me.XrLine4, Me.XrLine3, Me.XrLabel6, Me.xrLabel2})
        Me.subReportFotter_b.HeightF = 84.29112!
        Me.subReportFotter_b.Name = "subReportFotter_b"
        '
        'XrLabel12
        '
        Me.XrLabel12.BackColor = System.Drawing.Color.White
        Me.XrLabel12.BorderColor = System.Drawing.Color.Black
        Me.XrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel12.BorderWidth = 1.0!
        Me.XrLabel12.CanGrow = False
        Me.XrLabel12.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel12.ForeColor = System.Drawing.Color.Black
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(383.3333!, 10.00001!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(117.7084!, 16.66667!)
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = "Total salidas:"
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel11
        '
        Me.XrLabel11.BackColor = System.Drawing.Color.White
        Me.XrLabel11.BorderColor = System.Drawing.Color.Black
        Me.XrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel11.BorderWidth = 1.0!
        Me.XrLabel11.CanGrow = False
        Me.XrLabel11.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel11.ForeColor = System.Drawing.Color.Black
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 10.00001!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(117.7084!, 16.66667!)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = "Total entradas:"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel8
        '
        Me.XrLabel8.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CE_GetArqueoTransitoSaldos].[creditos]")})
        Me.XrLabel8.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(542.5!, 10.00001!)
        Me.XrLabel8.Multiline = True
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(237.5!, 16.66667!)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.StylePriority.UseTextAlignment = False
        Me.XrLabel8.Text = "XrLabel8"
        Me.XrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrLabel8.TextFormatString = "{0:c2}"
        '
        'XrLabel7
        '
        Me.XrLabel7.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CE_GetArqueoTransitoSaldos].[cargos]")})
        Me.XrLabel7.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(176.4585!, 10.00001!)
        Me.XrLabel7.Multiline = True
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(191.6667!, 16.66667!)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "XrLabel7"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrLabel7.TextFormatString = "{0:c2}"
        '
        'XrLine4
        '
        Me.XrLine4.LocationFloat = New DevExpress.Utils.PointFloat(383.3333!, 0.9165446!)
        Me.XrLine4.Name = "XrLine4"
        Me.XrLine4.SizeF = New System.Drawing.SizeF(406.0417!, 2.083333!)
        '
        'XrLine3
        '
        Me.XrLine3.LocationFloat = New DevExpress.Utils.PointFloat(15.83335!, 0.9165446!)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.SizeF = New System.Drawing.SizeF(345.8333!, 2.083333!)
        '
        'XrLabel6
        '
        Me.XrLabel6.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CE_GetArqueoTransitoSaldos].[saldo_final]")})
        Me.XrLabel6.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(368.1252!, 56.87501!)
        Me.XrLabel6.Multiline = True
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(190.4166!, 16.66669!)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.Text = "XrLabel6"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrLabel6.TextFormatString = "{0:c2}"
        '
        'xrLabel2
        '
        Me.xrLabel2.BackColor = System.Drawing.Color.White
        Me.xrLabel2.BorderColor = System.Drawing.Color.Black
        Me.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.xrLabel2.BorderWidth = 1.0!
        Me.xrLabel2.CanGrow = False
        Me.xrLabel2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.xrLabel2.ForeColor = System.Drawing.Color.Black
        Me.xrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(207.7085!, 56.87501!)
        Me.xrLabel2.Name = "xrLabel2"
        Me.xrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel2.SizeF = New System.Drawing.SizeF(160.4167!, 16.66667!)
        Me.xrLabel2.StylePriority.UseFont = False
        Me.xrLabel2.Text = "Saldo final:"
        Me.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'sqlDataSourceArqueo
        '
        Me.sqlDataSourceArqueo.ConnectionName = "ReportConnectionString"
        Me.sqlDataSourceArqueo.Name = "sqlDataSourceArqueo"
        StoredProcQuery1.MetaSerializable = "<Meta X=""20"" Y=""20"" Width=""237"" Height=""88"" />"
        StoredProcQuery1.Name = "CE_GetArqueoTransitoAcreditaciones"
        QueryParameter1.Name = "@fecha"
        QueryParameter1.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter1.Value = New DevExpress.DataAccess.Expression("?fecha", GetType(Date))
        QueryParameter2.Name = "@CRC"
        QueryParameter2.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter2.Value = New DevExpress.DataAccess.Expression(" ", GetType(String))
        StoredProcQuery1.Parameters.Add(QueryParameter1)
        StoredProcQuery1.Parameters.Add(QueryParameter2)
        StoredProcQuery1.StoredProcName = "CE_GetArqueoTransitoAcreditaciones"
        StoredProcQuery2.MetaSerializable = "<Meta X=""277"" Y=""20"" Width=""233"" Height=""122"" />"
        StoredProcQuery2.Name = "CE_GetArqueoTransitoRecolecciones"
        QueryParameter3.Name = "@fecha"
        QueryParameter3.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter3.Value = New DevExpress.DataAccess.Expression("?fecha", GetType(Date))
        QueryParameter4.Name = "@CRC"
        QueryParameter4.Type = GetType(String)
        StoredProcQuery2.Parameters.Add(QueryParameter3)
        StoredProcQuery2.Parameters.Add(QueryParameter4)
        StoredProcQuery2.StoredProcName = "CE_GetArqueoTransitoRecolecciones"
        StoredProcQuery3.Name = "CE_GetArqueoTransitoSaldos"
        QueryParameter5.Name = "@fecha"
        QueryParameter5.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter5.Value = New DevExpress.DataAccess.Expression("?fecha", GetType(Date))
        QueryParameter6.Name = "@CRC"
        QueryParameter6.Type = GetType(String)
        StoredProcQuery3.Parameters.Add(QueryParameter5)
        StoredProcQuery3.Parameters.Add(QueryParameter6)
        StoredProcQuery3.StoredProcName = "CE_GetArqueoTransitoSaldos"
        Me.sqlDataSourceArqueo.Queries.AddRange(New DevExpress.DataAccess.Sql.SqlQuery() {StoredProcQuery1, StoredProcQuery2, StoredProcQuery3})
        Me.sqlDataSourceArqueo.ResultSchemaSerializable = resources.GetString("sqlDataSourceArqueo.ResultSchemaSerializable")
        '
        'XtraReportArqueo
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.TopMargin, Me.BottomMargin, Me.Detail, Me.PageHeader, Me.PageFooter, Me.ReportFooter})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.sqlDataSourceArqueo})
        Me.DataMember = "CE_GetArqueoTransitoAcreditaciones"
        Me.DataSource = Me.sqlDataSourceArqueo
        Me.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Margins = New System.Drawing.Printing.Margins(30, 30, 24, 100)
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.fecha})
        Me.Version = "18.2"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents xrSubreportSalidas As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents fecha As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents xrSubreportEntradas As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents subPageHeader_a As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents Text3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Text2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Text1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents picLogoSissa As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents subPageHeader_b As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents Text5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Text4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents subPageFooter_a As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents xrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents xrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents subPageFooter_b As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents subReportFotter_b As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents xrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents sqlDataSourceArqueo As DevExpress.DataAccess.Sql.SqlDataSource
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine4 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents picLogoBanorte As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
End Class
