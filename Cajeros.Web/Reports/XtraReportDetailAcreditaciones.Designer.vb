<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XtraReportDetailAcreditaciones
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
        Dim StoredProcQuery1 As DevExpress.DataAccess.Sql.StoredProcQuery = New DevExpress.DataAccess.Sql.StoredProcQuery()
        Dim QueryParameter1 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter2 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XtraReportDetailAcreditaciones))
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblAcreditacionFecha = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.sqlDataSourceEntradas = New DevExpress.DataAccess.Sql.SqlDataSource(Me.components)
        Me.fecha = New DevExpress.XtraReports.Parameters.Parameter()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 0!
        Me.TopMargin.Name = "TopMargin"
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 0!
        Me.BottomMargin.Name = "BottomMargin"
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel2, Me.lblAcreditacionFecha, Me.xrLabel1})
        Me.Detail.HeightF = 20.83333!
        Me.Detail.Name = "Detail"
        '
        'XrLabel2
        '
        Me.XrLabel2.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Archivo]")})
        Me.XrLabel2.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(70.83334!, 0!)
        Me.XrLabel2.Multiline = True
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(174.0416!, 17.08333!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.Text = "XrLabel2"
        '
        'lblAcreditacionFecha
        '
        Me.lblAcreditacionFecha.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Fecha]")})
        Me.lblAcreditacionFecha.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblAcreditacionFecha.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.lblAcreditacionFecha.Multiline = True
        Me.lblAcreditacionFecha.Name = "lblAcreditacionFecha"
        Me.lblAcreditacionFecha.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblAcreditacionFecha.SizeF = New System.Drawing.SizeF(70.83334!, 17.08333!)
        Me.lblAcreditacionFecha.StylePriority.UseFont = False
        Me.lblAcreditacionFecha.TextFormatString = "{0:dd/MM/yyyy}"
        '
        'xrLabel1
        '
        Me.xrLabel1.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Importe]")})
        Me.xrLabel1.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.xrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(244.8749!, 0!)
        Me.xrLabel1.Multiline = True
        Me.xrLabel1.Name = "xrLabel1"
        Me.xrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel1.SizeF = New System.Drawing.SizeF(103.1251!, 17.08333!)
        Me.xrLabel1.StylePriority.UseFont = False
        Me.xrLabel1.Text = "xrLabel1"
        Me.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.xrLabel1.TextFormatString = "{0:c2}"
        '
        'sqlDataSourceEntradas
        '
        Me.sqlDataSourceEntradas.ConnectionName = "ReportConnectionString"
        Me.sqlDataSourceEntradas.Name = "sqlDataSourceEntradas"
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
        Me.sqlDataSourceEntradas.Queries.AddRange(New DevExpress.DataAccess.Sql.SqlQuery() {StoredProcQuery1})
        Me.sqlDataSourceEntradas.ResultSchemaSerializable = resources.GetString("sqlDataSourceEntradas.ResultSchemaSerializable")
        '
        'fecha
        '
        Me.fecha.Name = "fecha"
        Me.fecha.Type = GetType(Date)
        Me.fecha.ValueInfo = "1753-01-01"
        '
        'XtraReportDetailAcreditaciones
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.TopMargin, Me.BottomMargin, Me.Detail})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.sqlDataSourceEntradas})
        Me.DataMember = "CE_GetArqueoTransitoAcreditaciones"
        Me.DataSource = Me.sqlDataSourceEntradas
        Me.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Margins = New System.Drawing.Printing.Margins(100, 402, 0, 0)
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.fecha})
        Me.Version = "18.2"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents xrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents sqlDataSourceEntradas As DevExpress.DataAccess.Sql.SqlDataSource
    Friend WithEvents fecha As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblAcreditacionFecha As DevExpress.XtraReports.UI.XRLabel
End Class
