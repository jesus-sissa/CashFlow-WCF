<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MonitoreoSucursales.aspx.vb" Inherits="SISSA.Cashflow.Web.MonitoreoSucursales" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="MainHeader" runat="server">
    <link href="Content/bootstrap-style-customlocal.css" type="text/css"  rel="stylesheet"/>
    <script>
        function showModalData() {
            $("#modalSucursales").modal();
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="titulo-local">
        <h3>Estado de conexion de las sucursales</h3>
    </div>

         <asp:UpdatePanel ID="udp_Datalist" runat="server">
                <ContentTemplate>
                    <div class="table-responsive">
                    <asp:DataList ID="dl_Sucursales"  runat="server" Width="100%"  DataKeyField="Clave" RepeatColumns="10">
                        <ItemTemplate>
                            <table id="Table1"  class="table">
                                <tr id="Tr1" runat="server">
                                    <td id="Td1" runat="server" style="text-align:center;">
                                        <asp:ImageButton CommandName="Mostrar" ID="img_Sucursal" runat="server" width="70px" height="70px" ImageUrl='<%#Eval("Conexion")%>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 70px;  text-align:center;">
                                        <asp:Label ID="lbl_NombreSucursal" runat="server"  Font-Size="X-Small" ForeColor="Blue"> <%#Eval("Sucursal")%></asp:Label>                  
                                    </td>
                                </tr>
                                <tr>   
                                    <td style="text-align:center; ">
                                        <asp:Label ID="lbl_UltimaConexion" runat="server"  Font-Size="X-Small" ForeColor="Blue"> <%#Eval("Ultima_Conexion")%></asp:Label>                  
                                        <asp:Label ID="lbl_HUltima_Conexion" runat="server"  Font-Size="X-Small" ForeColor="Blue"> <%#Eval("HUltima_Conexion")%></asp:Label>
                                     </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                    </div>

                    
     <div class="modal fade" id="modalSucursales">
        <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
      
        <div class="modal-header">
          <h4 class="modal-title"></h4>
        </div>
        
        <div class="modal-body">

            <div class="row">
                <div class="col-12">
                    <div class="form-group">
                    <label class="font-weight-bold">Sucursal</label>
                    <asp:TextBox ID="tbx_Sucursal" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-12">
                    <label class="font-weight-bold" >Telefono</label>
                    <asp:TextBox ID="tbx_Telefono" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            
            <div class="row">
                <div class="col-12">
                    <label class="font-weight-bold">Domicilio</label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="tbx_Domicilio"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-12">
                    <label class="font-weight-bold">Tiempo Desconexion</label>
                    <asp:TextBox ID="tbx_Tiempo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

        </div>
        
        <div class="modal-footer">
          <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
        </div>
        </div>
        </div>
        </div>
                </ContentTemplate> 
            </asp:UpdatePanel>
            <nav>
                <div class="row">
                    <div class="offset-4 col-4">
                        <ul class="pagination">
                        <li class="page-item"><asp:LinkButton ID="lkb_Anterior" runat="server" Text="Anterior" CssClass="page-link" ></asp:LinkButton></li>
                        <li class="page-item"><asp:LinkButton ID="lkb_Siguiente" runat="server" Text="Siguiente" CssClass="page-link"></asp:LinkButton></li>
                    </ul>
                    </div>
                </div>
            </nav>
</asp:Content>
