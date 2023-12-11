<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="SISSA.Cashflow.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acceso</title>

    <link href="/Content/bootstrap.min.css" type="text/css" rel="stylesheet"/>
    <link href="/Style/sissa.css" rel="stylesheet" type="text/css" />

    <script src="/Scripts/jquery-3.5.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="/Scripts/sissa-functions.js" type="text/javascript"></script>

    <script src='https://www.google.com/recaptcha/api.js'></script> 
</head>
<body class="bg-light">
    <div class="container">
        <div class="row">
            <div class="offset-4 col-4 offset-4">
                <form id="form1" runat="server">
                    <!-- 
                    <asp:ScriptManager ID="spt_Manager" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="udp_Login" runat="server">
                        <ContentTemplate>
                        -->
                            <div class="card mt-5 ">
                            <div class="card-header font-custom-family text-center font-weight-bold text-white bg-danger">
                                <h3>SISSA</h3>
                            </div>
                            <div class="card-body">
                                <div class="form-group">
                                    <label>Clave Unica</label>
                                    <asp:TextBox ID="tbx_ClaveUnica" runat="server" CssClass="form-control text-uppercase font-weight-bold bg-light "></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label>Clave Usuario</label>
                                    <asp:TextBox ID="tbx_ClaveUsuario" runat="server" CssClass="form-control text-uppercase font-weight-bold bg-light"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label>Contraseña</label>
                                    <asp:TextBox ID="tbx_Contrasena" TextMode="Password" runat="server" CssClass="form-control text-uppercase font-weight-bold"></asp:TextBox>
                                </div>

                            </div>
                            <div class="card-footer">                                
                                 <asp:Button ID="btnAceptar" runat="server"   Text="Aceptar"  CssClass="btn btn-block btn-danger mt-2" />
                             <div class="text-center mt-2">
                      
                            <span id="siteseal">
                                <script type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=nod7IfQCmtK6GFfszh1dq7jB0eItIpDVQevxR9upzxVLB0qNZZh"></script>
                            </span>
                                  
                            </div>
                            </div>
                            </div>

                    <div class="modal fade" id="myModal">
                    <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header modal-header-bg "><h4 class="modal-title"></h4></div>
                        <div class="modal-body"></div>
                        <div class="modal-footer"><button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button></div>
                    </div>
                    </div>
                    </div>
                            <!--- 
                        </ContentTemplate>
                    </asp:UpdatePanel>
                                -->

                </form>
            </div>
        </div>
    </div>
</body>
</html>
