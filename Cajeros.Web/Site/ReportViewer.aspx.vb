Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class ReportViewer
    Inherits System.Web.UI.Page

    ' varibale general del formulario.
    Dim crReportDocument As ReportDocument

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim paramDate As String = Request("fecha")
            Dim _fecha As Date = Convert.ToDateTime(paramDate)

            ConfigureCrystalReports(_fecha)
        End If
    End Sub

    Private Sub ConfigureCrystalReports(Fecha As Date)
        Dim ReportPathAndName As String
        Dim ReportOutputPath As String

        Try
            ReportPathAndName = Server.MapPath("~/Content/Documents/ArqueoTransito_v01e.rpt")
            ReportOutputPath = Server.MapPath("~/Content/Documents/ArqueoTransito_v01d.pdf")

            SetReportDatabaseLogon(CrystalReportSourceArqueo.ReportDocument)

            'Se depueran los paramtros del ReportSoruce
            Dim _params As CrystalDecisions.Web.ParameterCollection = CrystalReportSourceArqueo.Report.Parameters
            _params.Clear()

            Dim p1 As CrystalDecisions.Web.Parameter = New CrystalDecisions.Web.Parameter()
            Dim p2 As CrystalDecisions.Web.Parameter = New CrystalDecisions.Web.Parameter()

            p1.Name = "@fecha"
            p1.DefaultValue = Fecha.ToString("yyyy-MM-dd")

            p2.Name = "@CRC"
            p2.DefaultValue = ""

            _params.Add(p1)
            _params.Add(p2)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub SetReportDatabaseLogon(crReportDocument As ReportDocument)
        Dim rptConn As ConnectionInfo
        rptConn = New ConnectionInfo()
        rptConn.ServerName = "sissa.southcentralus.cloudapp.azure.com"
        rptConn.DatabaseName = "db_SISSA_CASHFLOW_CE"
        rptConn.UserID = "sa"
        'rptConn.Password = "$1ss@2020"
        rptConn.Password = "Siss@2020"

        Dim rptDatabase As CrystalDecisions.CrystalReports.Engine.Database = crReportDocument.Database
        Dim rptTables As CrystalDecisions.CrystalReports.Engine.Tables = rptDatabase.Tables

        For Each obj As CrystalDecisions.CrystalReports.Engine.Table In rptTables
            If Not String.IsNullOrEmpty(obj.LogOnInfo.ConnectionInfo.DatabaseName) Then
                Dim rptLogOnInfo As TableLogOnInfo = obj.LogOnInfo
                rptLogOnInfo.ConnectionInfo = rptConn
                obj.ApplyLogOnInfo(rptLogOnInfo)
                obj.Location = String.Format("{0}.dbo.{1}", rptConn.DatabaseName, obj.Location.Substring(obj.Location.LastIndexOf(".") + 1))
                obj.LogOnInfo.ConnectionInfo.ServerName = rptConn.ServerName
            End If
        Next

    End Sub


End Class