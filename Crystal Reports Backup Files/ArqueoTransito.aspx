<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ArqueoTransito.aspx.vb" Inherits="SISSA.Cashflow.Web.ArqueoTransito1" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v20.1, Version=20.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v20.1, Version=20.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="MainHeader" runat="server">
    <script>
        function showPdfButton_Click(s, e) {
            //var dateArqueo = dtp_fecha.GetDate();
            //var reportViewerUrl = "ArqueoTransitoViewer.aspx?fecha=" + dateArqueo.toLocaleDateString("es-MX");
            
            ////Se abre ventana de visor de reporte
            //window.open(reportViewerUrl);

            //return false;
        }

        function PrintInNewWindow() {
            var url = document.URL.split('?')[0];
            var printWindowWrapper = window.open(url + "?Print=true", "_blank");

            printWindowWrapper.addEventListener("load", function (e) {
                if (printWindowWrapper.document.contentType !== "text/html")
                    printWindowWrapper.print();
            });
        }
    </script>
    <style type="text/css">
        .dirTB {
            direction: rtl;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div id="top-content-lg">

            <div class="row top-title">
                <div class="col-md-10">
                    <h3 class="mt-1">Arqueo en Transito</h3>
                </div>
                <div class="col-md-2">
                    <dx:ASPxButton ID="btnExportPdf" runat="server" Text="" RenderMode="Link" AutoPostBack="true">
                        <ClientSideEvents Click="function(s,e){ showPdfButton_Click(s,e); }" />
                        <Image Url="/Content/Images/file_pdf_view_48.png" UrlDisabled="/Content/Images/file_pdf_view_48_dis.png" />
                    </dx:ASPxButton>
                    <iframe id="FrameToPrint" name="PrintingFrame" style="position:absolute; left: -10000px; top: -10000px;"></iframe>
                    <dx:ASPxButton ID="btnExportarXls" runat="server" Text="" RenderMode="Link">
                        <Image Url="/Content/Images/file_excel_down_48.png" UrlDisabled="/Content/Images/file_excel_down_48_dis.png" />
                    </dx:ASPxButton>                    
                </div>
            </div>

            <div class="dev-filter">
                <dx:BootstrapFormLayout ID="BootstrapFormLayout" runat="server" AlignItemCaptionsInAllGroups="True">
                    <Items>
                        <dx:BootstrapLayoutGroup ColCount="12" ColSpanMd="8" ColumnCount="12" ShowCaption="False" Name="itemGroup1">
                            <Items>
                                <dx:BootstrapLayoutItem Caption="Fecha" ColSpanMd="7" RequiredMarkDisplayMode="Required" Name="itemFecha">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">
                                            <dx:BootstrapDateEdit ID="dtpFecha" runat="server" ClientInstanceName="dtp_fecha">
                                                <ValidationSettings SetFocusOnError="True">
                                                    <RequiredField IsRequired="True" ErrorText="Debe indicar la [Fecha]" />
                                                </ValidationSettings>
                                            </dx:BootstrapDateEdit>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:BootstrapLayoutItem>
                                <dx:BootstrapLayoutItem Caption="Boveda:" ColSpanMd="7" Name="itemBoveda">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">
                                            <dx:BootstrapComboBox ID="cmbBoveda" runat="server" NullText="Todos" TextField="CR" ValueField="CR">
                                            </dx:BootstrapComboBox>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:BootstrapLayoutItem>
                                <dx:BootstrapLayoutItem Caption="Buscar" ColSpanMd="3" ShowCaption="False" Name="itemBoton" BeginRow="True" HorizontalAlign="Left">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">
                                            <dx:BootstrapButton ID="btnBuscar" runat="server" AutoPostBack="true" Text="Buscar" Width="200px">
                                                <SettingsBootstrap RenderOption="Danger" />
                                            </dx:BootstrapButton>                                    
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:BootstrapLayoutItem>
                            </Items>
                        </dx:BootstrapLayoutGroup>
                        <dx:BootstrapLayoutGroup ColCount="12" ColSpanMd="4" ColumnCount="12" ShowCaption="False" Name="itemGroup2">
                            <Items>
                                <dx:BootstrapLayoutItem Caption="Saldo Inicial:" ColSpanMd="12" Name="itemSaldoInicial">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">
                                            <dx:BootstrapTextBox ID="txtSaldoInicial" runat="server">
                                                <CssClasses Control="dirTB" />
                                            </dx:BootstrapTextBox>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:BootstrapLayoutItem>
                                <dx:BootstrapLayoutItem Caption="Acreditaciones" ColSpanMd="12" Name="itemAcreditaciones">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">
                                            <dx:BootstrapTextBox ID="txtAcreditaciones" runat="server">
                                                <CssClasses Control="dirTB" />
                                            </dx:BootstrapTextBox>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:BootstrapLayoutItem>
                                <dx:BootstrapLayoutItem Caption="Recolecciones" ColSpanMd="12" Name="itemRecolecciones">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">
                                            <dx:BootstrapTextBox ID="txtRecolecciones" runat="server">
                                                <CssClasses Control="dirTB" />
                                            </dx:BootstrapTextBox>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:BootstrapLayoutItem>
                                <dx:BootstrapLayoutItem Caption="Saldo Final" ColSpanMd="12" Name="itemSaldoFinal">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">
                                            <dx:BootstrapTextBox ID="txtSaldoFinal" runat="server">
                                                <CssClasses Control="dirTB" />
                                            </dx:BootstrapTextBox>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:BootstrapLayoutItem>
                            </Items>
                        </dx:BootstrapLayoutGroup>
                    </Items>

                </dx:BootstrapFormLayout>
            </div>
        </div>
        <div id="main-content">
            <div class="dev-grid">

                <div class="detail-left">
                    <dx:ASPxGridView ID="ASPxGridViewAcreditaciones" runat="server" AutoGenerateColumns="False" Width="100%">
                        <SettingsPopup>
                            <HeaderFilter MinHeight="140px"></HeaderFilter>
                        </SettingsPopup>
                        <Columns>
                            <dx:GridViewDataDateColumn Caption="Fecha" FieldName="Fecha" Name="gcFecha" VisibleIndex="0" Width="90px">
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="Archivo" FieldName="Archivo" Name="gcArchivo" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Importe" FieldName="Importe" Name="gcImporte" VisibleIndex="2" Width="120px">
                                <PropertiesTextEdit DisplayFormatString="c" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </div>

                <div class="detail-right">
                    <dx:ASPxGridView ID="ASPxGridViewRecolecciones" runat="server" AutoGenerateColumns="False" Width="100%">
                        <SettingsPopup>
                            <HeaderFilter MinHeight="140px"></HeaderFilter>
                        </SettingsPopup>
                        <Columns>
                            <dx:GridViewDataDateColumn Caption="Fecha" FieldName="Fecha" Name="gcFecha" ReadOnly="True" VisibleIndex="0">
                                <PropertiesDateEdit Width="90px">
                                </PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="Cliente" FieldName="Nombre_Cliente" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Cajero" FieldName="Clave_Cajero" Name="gcCajero" ReadOnly="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Folio" FieldName="Folio" Name="gcFolio" ReadOnly="True" VisibleIndex="3">
                                <PropertiesTextEdit Width="80px">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Importe" FieldName="Importe" Name="gcImporte" ReadOnly="True" VisibleIndex="4">
                                <PropertiesTextEdit DisplayFormatString="c" Width="120px">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </div>

                <div id="grid-export">
                    <dx:ASPxGridView ID="GridViewExport" runat="server" AutoGenerateColumns="true" Visible="false">
                    </dx:ASPxGridView>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
