<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReportViewer.aspx.vb" Inherits="SISSA.Cashflow.Web.ReportViewer" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SISSA - Cashflow WEB</title>

    <script type="text/javascript" src="/crystalreportviewers13/js/crviewer/crv.js">
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <CR:CrystalReportViewer ID="CrystalReportViewerArqueo" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSourceArqueo" />
            <CR:CrystalReportSource ID="CrystalReportSourceArqueo" runat="server">
                <Report FileName="~/Content/ArqueoTransito_v01e.rpt">
                    <Parameters>
                        <CR:Parameter Name="@fecha" DefaultValue="2020-09-02" />
                        <CR:Parameter Name="@CRC" DefaultValue="" />
                    </Parameters>
                </Report>
            </CR:CrystalReportSource>
        </div>
    </form>
</body>
</html>
