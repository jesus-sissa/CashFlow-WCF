<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MonitorArchivos.aspx.vb" Inherits="SISSA.Cashflow.Web.MonitorArchivos" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v20.1, Version=20.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v20.1, Version=20.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainHeader" runat="server">
    <script type="text/javascript">
        function OnClick(s, e, acreditacionID) {
            //alert(acreditacionID);
            Popup.PerformCallback(acreditacionID);
        }
        function OnEndCallback(s, e) {
            Popup.Show();
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row top-content-lg">
        <div class="row top-title">
            <div class="col-md-12"><h3 class="mt-1">Monitor de Archivos</h3></div>
        </div>
        <dx:bootstrapformlayout ID="BootstrapFormLayout" runat="server" AlignItemCaptionsInAllGroups="True">
            <Items>
                <dx:BootstrapLayoutItem Caption="Bóveda" ColSpanMd="4" Name="itemBoveda">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapComboBox ID="cmbBoveda" runat="server"  NullText="Todos" TextField="CR" ValueField="CR">
                            </dx:BootstrapComboBox>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>                    
                <dx:BootstrapLayoutItem Caption="F. Inicial" ColSpanMd="4" Name="itemFechaInicial">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapDateEdit ID="dtpFechaInicial" runat="server" ClientInstanceName="dtp_fechaInicial">
                                <%--<ClientSideEvents LostFocus="function(s,e) { OnLostFocusValidateDate(s); }" />--%>
                                <CaptionSettings RequiredMark="*" />                                        
                                <ValidationSettings>
                                    <RequiredField IsRequired="true" ErrorText="Debe indicar [Fecha inicial]" />
                                </ValidationSettings>                                        
                            </dx:BootstrapDateEdit>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
                <dx:BootstrapLayoutItem Caption="F. Final" ColSpanMd="4" Name="itemFechaFinal">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapDateEdit ID="dtpFechaFinal" runat="server" ClientInstanceName="dtp_fechaFinal">
                                <%--<ClientSideEvents LostFocus="function(s,e) { OnLostFocusValidateDate(s); }" />--%>
                                <CaptionSettings RequiredMark="*" />
                                <ValidationSettings EnableCustomValidation="true">
                                    <RequiredField IsRequired="true" ErrorText="Debe indicar [Fecha final]" />
                                </ValidationSettings>                                        
                            </dx:BootstrapDateEdit>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>

                <dx:BootstrapLayoutItem Caption="Boton" ColSpanMd="3" ShowCaption="False" Name="otemBoton" BeginRow="True" HorizontalAlign="Left">
                    <ContentCollection>
                        <dx:ContentControl runat="server">                                    
                            <dx:BootstrapButton ID="btnBuscar" runat="server" ClientInstanceName="btnbuscar" AutoPostBack="true" Text="Buscar" Width="200px">
                                <SettingsBootstrap RenderOption="Danger" />                                                                                
                            </dx:BootstrapButton>                                    
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
                <dx:BootstrapLayoutItem Caption="Generar excel directo" ColSpanMd="6" HorizontalAlign="Left" Name="itemGenerarExcel" ShowCaption="False">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapCheckBox ID="chkGenerarExcel" runat="server" Text="Generar excel directo" CheckState="Unchecked">
                            </dx:BootstrapCheckBox>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
                <dx:BootstrapLayoutItem ColSpanMd="3" HorizontalAlign="Right" ShowCaption="False">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:ASPxButton ID="btnExportar" runat="server" Text="" RenderMode="Link">
                                <Image Url="/Content/Images/file_excel_down_48.png" UrlDisabled="/Content/Images/file_excel_down_48_dis.png" />
                            </dx:ASPxButton>                            
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
            </Items>
        </dx:bootstrapformlayout>
    </div>
    <div class="row">
        <dx:ASPxGridView ID="gridMonitor" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="Id_Acreditacion">
            <Columns>
                <dx:GridViewDataTextColumn Caption="ID" FieldName="Id_Acreditacion" VisibleIndex="0" Visible="False">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Right" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Caja" FieldName="CRC" VisibleIndex="1" Width="40px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Proveedor" FieldName="Proveedor" VisibleIndex="2" Width="60px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="A. Depositos" FieldName="ArchivoDeposito" ToolTip="Archivo Depositos" VisibleIndex="3" Width="130px">
                </dx:GridViewDataTextColumn>
                <%--<dx:GridViewDataTextColumn Caption="A. Cheque" FieldName="ArchivoCheque" ToolTip="Archivo Cheque" VisibleIndex="3" Width="130px">
                </dx:GridViewDataTextColumn>--%>
                <dx:GridViewDataTextColumn Caption="Fecha" FieldName="Fecha" VisibleIndex="4" Width="70px">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" />                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="F. Transmisión" FieldName="Fecha_Transmision" VisibleIndex="5" Width="110px">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" />                   
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="F. Notificación" FieldName="Fecha_Acreditacion" VisibleIndex="6" Width="110px">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" />                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Cajeros" FieldName="Cajeros" VisibleIndex="7" Width="150px">
                    <CellStyle Wrap="True" />
                    <Settings AllowEllipsisInText="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Estatus" FieldName="Estatus" VisibleIndex="8" Width="90px">
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Corte" FieldName="Corte" VisibleIndex="9" Width="50px">
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="N. Depositos" FieldName="NumeroDepositos" ToolTip="Numero Depositos" VisibleIndex="10" Width="80px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Right" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="T. Depositos" FieldName="TotalDepositos" ToolTip="Total Depositos" VisibleIndex="11" Width="120px">
                    <PropertiesTextEdit DisplayFormatString="c" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Right" />
                </dx:GridViewDataTextColumn>
                <%--<dx:GridViewCommandColumn Caption="Detalle" VisibleIndex="15" Width="70px" ToolTip="Detalle">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton>
                            <Image ToolTip="Detalle" Url="../Imagenes/view_details.png" />
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewCommandColumn>--%>
                <dx:GridViewDataColumn FieldName="Detalle" VisibleIndex="16" Width="70px" ToolTip="Detalle">
                    <DataItemTemplate>
                        <dx:ASPxButton ID="btnDetail" runat="server" AutoPostBack="false" OnLoad="btnDetail_Load">
                            <Image Url="../Imagenes/view_details.png" />
                        </dx:ASPxButton>
                    </DataItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" />
                </dx:GridViewDataColumn>
                
            </Columns>
        </dx:ASPxGridView>
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter" runat="server" GridViewID="gridMonitor">
        </dx:ASPxGridViewExporter>
    </div>
    
    <dx:ASPxPopupControl ID="Popup" runat="server" ClientInstanceName="Popup" ShowCloseButton="false" CloseAction="OuterMouseClick" 
        OnWindowCallback="Popup_WindowCallback" Modal="true" PopupAction="None" PopupElementID="gridMonitor" 
        PopupHorizontalAlign="Center" PopupVerticalAlign="Middle">
        <ClientSideEvents EndCallback="OnEndCallback" />
        <ContentCollection>
            <dx:PopupControlContentControl>
                <dx:ASPxGridView ID="gridDetalle" runat="server" AutoGenerateColumns="false" 
                    OnInit="gridDetalle_Init">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Folio" FieldName="ID" VisibleIndex="1" Width="80px">
						    <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Fecha" FieldName="Fecha_Transmision" VisibleIndex="2" Width="120px">
                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center" />                   
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Cuenta" FieldName="Cuenta" VisibleIndex="3" Width="80px">
						    <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Referencia" FieldName="Referencia" VisibleIndex="4" Width="100px">
						    <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Importe" FieldName="Monto" VisibleIndex="5" Width="120px">
                            <PropertiesTextEdit DisplayFormatString="c" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Right" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager Mode="ShowPager" Position="Bottom" PageSize="10" Visible="true" />
                    <Settings ShowFooter="true" />
                    <TotalSummary>
                        <dx:ASPxSummaryItem FieldName="Monto" SummaryType="Sum" DisplayFormat="{0:c}" />
                    </TotalSummary>
                </dx:ASPxGridView>
            </dx:PopupControlContentControl>
        </ContentCollection>

    </dx:ASPxPopupControl>
</asp:Content>
