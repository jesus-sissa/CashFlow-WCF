Imports DevExpress.XtraReports.Web

Public Class ArqueoTransitoViewer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Dim paramDate As String = Request("fecha")
            Dim _fecha As Date = Convert.ToDateTime(paramDate)

            Dim report As XtraReportArqueo = New XtraReportArqueo()
            report.Parameters("fecha").Value = _fecha

            Dim cachedReportSource = New CachedReportSourceWeb(report)
            ReportViewerArqueo.OpenReport(cachedReportSource)

        End If

    End Sub

    Private Sub ConfigureCrystalReports(Fecha As Date)
        Dim ReportPathAndName As String
        Dim ReportOutputPath As String

        Try
            ReportPathAndName = Server.MapPath("~/Content/Documents/ArqueoTransito_v01c.rpt")
            ReportOutputPath = Server.MapPath("~/Content/Documents/ArqueoTransito_v01c.pdf")


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class