﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ArqueoTransitoViewer.aspx.vb" Inherits="SISSA.Cashflow.Web.ArqueoTransitoViewer" %>
<%@ Register Assembly="DevExpress.XtraReports.v20.1.Web.WebForms, Version=20.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="MainHeader" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h3>Arqueo en Transito</h3>
        <dx:aspxwebdocumentviewer ID="ReportViewerArqueo" runat="server">
        </dx:aspxwebdocumentviewer>
    </div>
</asp:Content>
