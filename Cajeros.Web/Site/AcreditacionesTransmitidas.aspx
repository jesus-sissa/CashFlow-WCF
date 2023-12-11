﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AcreditacionesTransmitidas.aspx.vb" Inherits="SISSA.Cashflow.Web.AcreditacionesTransmitidas" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v20.1, Version=20.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v20.1, Version=20.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="MainHeader" runat="server">
    <script type="text/javascript">        
        function OnClientChange(cmbClient) {
            cmbsucursal.PerformCallback(cmbClient.GetSelectedItem().value.toString());
        }

        function OnLostFocusValidateDate(s) {
            var datetimeStart = dtp_fechaInicial.GetDate();
            var datetimeEnd = dtp_fechaFinal.GetDate()

            if (Date.parse(datetimeStart) > Date.parse(datetimeEnd)) {
                s.SetIsValid(false);
                s.SetErrorText('La fecha final no puede ser menor a la fecha inicial.');
                s.Focus();                
            }            
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row top-content-md">
        <div class="row top-title">
            <div class="col-md-12"><h3 class="mt-1">Acreditaciones Transmitidas</h3></div>
        </div>
        <dx:BootstrapFormLayout ID="BootstrapFormLayout" runat="server">
            <Items>
                <dx:BootstrapLayoutItem BeginRow="True" Caption="Boveda" ColSpanMd="4" Name="itemBoveda">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapComboBox ID="cmbBoveda" runat="server" NullText="Todos" TextField="CR" ValueField="CR">
                            </dx:BootstrapComboBox>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
                <dx:BootstrapLayoutItem Caption="F. Inicial" ColSpanMd="4" Name="itemFechaInicial">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapDateEdit ID="dtpFechaInicial" runat="server" ClientInstanceName="dtp_fechaInicial">
                                <%--<ClientSideEvents LostFocus="function (s,e) { OnLostFocusValidateDate(s); }" />--%>
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
                                <ClientSideEvents LostFocus="function (s,e) { OnLostFocusValidateDate(s); }" />
                                <CaptionSettings RequiredMark="*" />                                        
                                <ValidationSettings>
                                    <RequiredField IsRequired="true" ErrorText="Debe indicar [Fecha final]" />
                                </ValidationSettings>
                            </dx:BootstrapDateEdit>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
                <dx:BootstrapLayoutItem Caption="Cliente" ColSpanMd="8" Name="itemCliente">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapComboBox ID="cmbCliente" runat="server" ClientInstanceName="cmbcliente" NullText="Todos" TextField="Nombre_Comercial" ValueField="Clave_Cliente"
                                DropDownStyle="DropDownList">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnClientChange(s); }" />
                            </dx:BootstrapComboBox>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
                <dx:BootstrapLayoutItem Caption="Sucursal" ColSpanMd="4" Name="itemSucursal">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapComboBox ID="cmbSucursal" runat="server" ClientInstanceName="cmbsucursal" NullText="Todos" TextField="Nombre_Comercial" ValueField="Clave_Sucursal"  
                                DropDownStyle="DropDownList" OnCallback="cmbSucursal_Callback">                                        
                            </dx:BootstrapComboBox>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
                <dx:BootstrapLayoutItem Caption="Boton" ColSpanMd="3" ShowCaption="False" Name="itemBoton" BeginRow="True" HorizontalAlign="Left">
                    <ContentCollection>
                        <dx:ContentControl runat="server">                                    
                            <dx:BootstrapButton ID="btnBuscar" runat="server" AutoPostBack="true" Text="Buscar" Width="200px">                                        
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
        </dx:BootstrapFormLayout>
    </div>
    <div class="row">
        <dx:ASPxGridView ID="gridAcreditaciones" runat="server" AutoGenerateColumns="false" Width="100%">
            <SettingsPopup>
                <HeaderFilter MinHeight="140px"></HeaderFilter>
            </SettingsPopup>
            <Settings ShowFooter="true" />
            <SettingsPager Mode="ShowPager" Position="Bottom" PageSize="10" Visible="true" />
            <Columns>
                <dx:GridViewDataTextColumn Caption="Transacción" FieldName="Id_Transaccion" Name="gcTransaccion" ReadOnly="True" VisibleIndex="0" Width="80px">
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="ID Banco" FieldName="Clave_Banco" Name="gcClaveBanco" ReadOnly="True" VisibleIndex="1" Width="60px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Cliente" FieldName="Nombre_Cliente" Name="gcNombreCliente" ReadOnly="True" VisibleIndex="2">
                    <HeaderStyle HorizontalAlign="Left" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Sucursal" FieldName="Nombre_Sucursal" Name="gcNombreSucursal" ReadOnly="True" VisibleIndex="3">
                    <HeaderStyle HorizontalAlign="Left" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="ID" FieldName="Clave_Sucursal" Name="gcClaveSucursal" ReadOnly="True" VisibleIndex="4" Width="60px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" />                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Bóveda/CRC" FieldName="CRC" Name="gcCRC" ReadOnly="True" VisibleIndex="5" Width="40px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Cuenta" FieldName="Cuenta" Name="gcCuenta" ReadOnly="True" VisibleIndex="6" Width="80px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>                
                <dx:GridViewDataTextColumn Caption="F.Acreditación" FieldName="Fecha_Acreditacion" Name="gcFechaAcreditacion" ReadOnly="True" VisibleIndex="8" Width="120px">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Monto" FieldName="Monto" Name="gcMonto" ReadOnly="True" VisibleIndex="9" Width="110px">
                    <PropertiesTextEdit DisplayFormatString="c" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Divisa" FieldName="Divisa" Name="gcDivisa" ReadOnly="True" VisibleIndex="10" Width="60px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" />                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Archivo" FieldName="Archivo" Name="gcArchivo" ReadOnly="True" VisibleIndex="12" Width="130px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Left" />                    
                </dx:GridViewDataTextColumn>
            </Columns>
            <TotalSummary>
                <dx:ASPxSummaryItem FieldName="Id_Transaccion" SummaryType="Count" DisplayFormat="Cont.: {0}" />
                <dx:ASPxSummaryItem FieldName="Monto" SummaryType="Sum" DisplayFormat="{0:c}" />
            </TotalSummary>
        </dx:ASPxGridView>
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter" runat="server" GridViewID="gridAcreditaciones">
        </dx:ASPxGridViewExporter>
    </div>
</asp:Content>
