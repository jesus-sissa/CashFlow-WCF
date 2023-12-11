<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DepositosReceptores.aspx.vb" Inherits="SISSA.Cashflow.Web.DepositosReceptores" %>
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
    <div class="row top-content-lg">
        <div class="row top-title">
            <div class="col-md-12"><h3 class="mt-1">Depositos/Recolecciones</h3></div>
        </div>
        <dx:BootstrapFormLayout ID="BootstrapFormLayout" runat="server">
            <Items>
                <dx:BootstrapLayoutItem BeginRow="True" Caption="Cajero" ColSpanMd="4" Name="itemBanco">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapComboBox ID="cmbCajero" runat="server">
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
                                <ClientSideEvents LostFocus="function(s,e) { OnLostFocusValidateDate(s); }" />
                                <CaptionSettings RequiredMark="*" />
                                <ValidationSettings EnableCustomValidation="true">
                                    <RequiredField IsRequired="true" ErrorText="Debe indicar [Fecha final]" />
                                </ValidationSettings>                                        
                            </dx:BootstrapDateEdit>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
                <dx:BootstrapLayoutItem BeginRow="True" Caption="Bóveda" ColSpanMd="4" Name="itemBoveda">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapComboBox ID="cmbBoveda" runat="server"  NullText="Todos" TextField="CR" ValueField="CR">
                            </dx:BootstrapComboBox>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
                <dx:BootstrapLayoutItem Caption="F. Transmisión" ColSpanMd="4" Name="itemFechaTransmision">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapDateEdit ID="dtpFechaTransmision" runat="server">
                            </dx:BootstrapDateEdit>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
                <dx:BootstrapLayoutItem Caption="Tipo" ColSpanMd="4" Name="item">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapComboBox ID="cmbTipo" runat="server" SelectedIndex="0">
                                <Items>
                                    <dx:BootstrapListEditItem Text="Deposito" Value="D" />
                                    <dx:BootstrapListEditItem Text="Recoleción" Value="R" />
                                </Items>
                            </dx:BootstrapComboBox>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
                <dx:BootstrapLayoutItem BeginRow="True" Caption="Cliente" ColSpanMd="8" Name="itemCliente">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapComboBox ID="cmbCliente" runat="server" ClientInstanceName="cmbcliente" NullText="Todos" TextField="Nombre_Comercial" ValueField="Clave_Cliente" 
                                DropDownStyle="DropDownList" Width="100%">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnClientChange(s); }" />
                            </dx:BootstrapComboBox>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
                <dx:BootstrapLayoutItem Caption="Sucursal" Name="itemSucursal" ColSpanMd="4">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapComboBox ID="cmbSucursal" runat="server" ClientInstanceName="cmbsucursal" NullText="Todos" TextField="Nombre_Comercial" ValueField="Clave_Sucursal" 
                                DropDownStyle="DropDownList" OnCallback="cmbSucursal_Callback">
                            </dx:BootstrapComboBox>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
                <dx:BootstrapLayoutItem BeginRow="True" Caption="Estatus" Name="itemEstatus" ColSpanMd="4">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapComboBox ID="cmbEstatus" runat="server">
                            </dx:BootstrapComboBox>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
                <dx:BootstrapLayoutItem Caption="Cuenta" Name="itemCuenta" ColSpanMd="4">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapTextBox ID="txtCuenta" runat="server" NullText=" ">
                                <MaskSettings Mask="9999999999" />
                            </dx:BootstrapTextBox>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapLayoutItem>
                <dx:BootstrapLayoutItem Caption="Divisa" Name="itemDivisa" ColSpanMd="4">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <dx:BootstrapComboBox ID="cmbDivisas" runat="server" SelectedIndex="0">
                                <Items>
                                    <dx:BootstrapListEditItem Text="MXN" Value="MXN" />
                                    <dx:BootstrapListEditItem Text="DLLS" Value="DLLS" />
                                </Items>
                            </dx:BootstrapComboBox>
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
        </dx:BootstrapFormLayout>
    </div>
    <div class="row">
        <dx:ASPxGridView ID="gridDepositos" runat="server" AutoGenerateColumns="False" Width="100%">
            <SettingsPopup>
                <HeaderFilter MinHeight="140px"></HeaderFilter>
            </SettingsPopup>
            <Settings ShowFooter="true" />
            <SettingsPager Mode="ShowPager" Position="Bottom" PageSize="10" Visible="true" />
            <Columns>
                <dx:GridViewDataTextColumn Caption="Transacción" FieldName="Id_Transaccion" Name="gcTransaccion" ReadOnly="True" VisibleIndex="0" Width="80px">
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="ID Banco" FieldName="Clave_Banco" Name="gcClaveBanco" ReadOnly="True" VisibleIndex="2" Width="60px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Cliente" FieldName="Nombre_Cliente" Name="gcNombreCliente" ReadOnly="True" VisibleIndex="4">
                    <HeaderStyle HorizontalAlign="Left" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Sucursal" FieldName="Nombre_Sucursal" Name="gcNombreSucursal" ReadOnly="True" VisibleIndex="6">
                    <HeaderStyle HorizontalAlign="Left" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="ID" FieldName="Clave_Sucursal" Name="gcClaveSucursal" ReadOnly="True" VisibleIndex="8" Width="60px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" />                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Bóveda/CRC" FieldName="CRC" Name="gcCRC" ReadOnly="True" VisibleIndex="10" Width="40px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Cuenta" FieldName="Cuenta" Name="gcCuenta" ReadOnly="True" VisibleIndex="12" Width="80px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Envase" FieldName="Envase" Name="gcEnvase" ReadOnly="True" VisibleIndex="14" Width="70px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Referencia" FieldName="Referencia" Name="gcReferencia" ReadOnly="True" VisibleIndex="16" Width="80px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Fecha" FieldName="Fecha" Name="gcFecha" ReadOnly="True" VisibleIndex="18" Width="120px">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" />                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Monto" FieldName="Monto" Name="gcMonto" ReadOnly="True" VisibleIndex="20" Width="100px">
                    <PropertiesTextEdit DisplayFormatString="c" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Right" />                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Divisa" FieldName="Divisa" Name="gcDivisa" ReadOnly="True" VisibleIndex="22" Width="60px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" />                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Tipo" FieldName="Tipo_Movimiento" Name="gcTipo" ReadOnly="True" VisibleIndex="24" Width="50px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" />                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="B_1000" FieldName="B_1000" ReadOnly="True" Visible="False" VisibleIndex="50" Width="50px" />
                <dx:GridViewDataTextColumn Caption="B_500" FieldName="B_500" ReadOnly="True" Visible="False" VisibleIndex="51" Width="50px" />
                <dx:GridViewDataTextColumn Caption="B_200" FieldName="B_200" ReadOnly="True" Visible="False" VisibleIndex="52" Width="50px" />
                <dx:GridViewDataTextColumn Caption="B_100" FieldName="B_100" ReadOnly="True" Visible="False" VisibleIndex="53" Width="50px" />
                <dx:GridViewDataTextColumn Caption="B_50" FieldName="B_50" ReadOnly="True" Visible="False" VisibleIndex="54" Width="50px" />
                <dx:GridViewDataTextColumn Caption="B_20" FieldName="B_20" ReadOnly="True" Visible="False" VisibleIndex="55" Width="50px" />
            </Columns>
            <TotalSummary>
                <dx:ASPxSummaryItem FieldName="Id_Transaccion" SummaryType="Count" DisplayFormat="Cont.: {0}" />
                <dx:ASPxSummaryItem FieldName="Monto" SummaryType="Sum" DisplayFormat="{0:c}" />
            </TotalSummary>
        </dx:ASPxGridView>
        
    </div>
    <div class="row">
        <dx:ASPxGridView ID="GridExporter" runat="server" AutoGenerateColumns="False" Width="100%" Visible="false">
            <SettingsPopup>
                <HeaderFilter MinHeight="140px"></HeaderFilter>
            </SettingsPopup>
            <SettingsPager Mode="ShowPager" Position="Bottom" PageSize="20" Visible="true" />
            <Columns>
                <dx:GridViewDataTextColumn Caption="Transacción" FieldName="Id_Transaccion" Name="gcTransaccion" ReadOnly="True" VisibleIndex="0" Width="80px">
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="ID Banco" FieldName="Clave_Banco" Name="gcClaveBanco" ReadOnly="True" VisibleIndex="2" Width="60px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Cliente" FieldName="Nombre_Cliente" Name="gcNombreCliente" ReadOnly="True" VisibleIndex="4">
                    <HeaderStyle HorizontalAlign="Left" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Sucursal" FieldName="Nombre_Sucursal" Name="gcNombreSucursal" ReadOnly="True" VisibleIndex="6">
                    <HeaderStyle HorizontalAlign="Left" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="ID" FieldName="Clave_Sucursal" Name="gcClaveSucursal" ReadOnly="True" VisibleIndex="8" Width="60px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" />                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Bóveda/CRC" FieldName="CRC" Name="gcCRC" ReadOnly="True" VisibleIndex="10" Width="40px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Cuenta" FieldName="Cuenta" Name="gcCuenta" ReadOnly="True" VisibleIndex="12" Width="80px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Envase" FieldName="Envase" Name="gcEnvase" ReadOnly="True" VisibleIndex="14" Width="70px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Referencia" FieldName="Referencia" Name="gcReferencia" ReadOnly="True" VisibleIndex="16" Width="80px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Fecha" FieldName="Fecha" Name="gcFecha" ReadOnly="True" VisibleIndex="18" Width="100px">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="No de Piezas" FieldName="Piezas" Name="gcPiezas" ReadOnly="True" VisibleIndex="20" Width="110px">                    
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Right" />                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Monto" FieldName="Monto" Name="gcMonto" ReadOnly="True" VisibleIndex="22" Width="110px">
                    <PropertiesTextEdit DisplayFormatString="c" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Right" />                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Divisa" FieldName="Divisa" Name="gcDivisa" ReadOnly="True" VisibleIndex="24" Width="60px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" />                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Tipo" FieldName="Tipo_Movimiento" Name="gcTipo" ReadOnly="True" VisibleIndex="26" Width="50px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" />                    
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="B_1000" FieldName="B_1000" ReadOnly="True" Visible="True" VisibleIndex="50" Width="50px" />
                <dx:GridViewDataTextColumn Caption="S_1000" FieldName="S_1000" ReadOnly="True" Visible="True" VisibleIndex="51" Width="50px" />
                <dx:GridViewDataTextColumn Caption="B_500" FieldName="B_500" ReadOnly="True" Visible="True" VisibleIndex="52" Width="50px" />
                <dx:GridViewDataTextColumn Caption="S_500" FieldName="S_500" ReadOnly="True" Visible="True" VisibleIndex="53" Width="50px" />
                <dx:GridViewDataTextColumn Caption="B_200" FieldName="B_200" ReadOnly="True" Visible="True" VisibleIndex="54" Width="50px" />
                <dx:GridViewDataTextColumn Caption="S_200" FieldName="S_200" ReadOnly="True" Visible="True" VisibleIndex="55" Width="50px" />
                <dx:GridViewDataTextColumn Caption="B_100" FieldName="B_100" ReadOnly="True" Visible="True" VisibleIndex="56" Width="50px" />
                <dx:GridViewDataTextColumn Caption="S_100" FieldName="S_100" ReadOnly="True" Visible="True" VisibleIndex="57" Width="50px" />
                <dx:GridViewDataTextColumn Caption="B_50" FieldName="B_50" ReadOnly="True" Visible="True" VisibleIndex="58" Width="50px" />
                <dx:GridViewDataTextColumn Caption="S_50" FieldName="S_50" ReadOnly="True" Visible="True" VisibleIndex="59" Width="50px" />
                <dx:GridViewDataTextColumn Caption="B_20" FieldName="B_20" ReadOnly="True" Visible="True" VisibleIndex="60" Width="50px" />
                <dx:GridViewDataTextColumn Caption="S_20" FieldName="S_20" ReadOnly="True" Visible="True" VisibleIndex="61" Width="50px" />

                <dx:GridViewDataTextColumn Caption="Archivo" FieldName="Archivo" Name="gcArchivo" ReadOnly="True" VisibleIndex="70" Width="130px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Left" />                    
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter" runat="server" ExportedRowType="All" GridViewID="GridExporter">
        </dx:ASPxGridViewExporter>
    </div>
</asp:Content>
