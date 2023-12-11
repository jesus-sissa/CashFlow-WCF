<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ConsultaLog.aspx.vb" Inherits="SISSA.Cashflow.Web.ConsultaLog" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="MainHeader" runat="server">

    <link href="jquery-ui/jquery-ui.min.css" type="text/css" rel="stylesheet" />
    <link href="jquery-ui/jquery-ui.structure.min.css" rel="stylesheet" />
    <script src="jquery-ui/jquery-ui.min.js"></script>
    <link href="jquery-ui/jquery-ui.structure.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-style-customlocal.css" type="text/css" rel="stylesheet" />
    <script src="Scripts/jquery-ui-1.12.1.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" />

    <script>

        $(document).ready(function () {
            $("#<%= tbx_FechaInicio.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy"
                
           });
           $("#<%= tbx_FechaFin.ClientID%>").datepicker({
               dateFormat: "dd/mm/yy"
           });

       });

        function validar() {

            var tbxFechaInicio = document.getElementById("<%= tbx_FechaInicio.ClientID%>");
            var tbxFechaFin = document.getElementById("<%= tbx_FechaFin.ClientID%>");
            var ddlSucursal = document.getElementById("<%= ddl_Sucursales.ClientID%>");
            var LogDescripcion = document.getElementById("<%= TxtDescripcion.ClientID%>");
            var ChkDescripcion = document.getElementById("<%= ChkDescripcion.ClientID%>");
            var chkSucursal = document.getElementById("<%= chkSucursal.ClientID%>");

            var fechaInicioValue = tbxFechaInicio.value;
            var fechaFinValue = tbxFechaFin.value;
            var LogDescripcionValue = LogDescripcion.value;
            var sucursalValue = ddlSucursal.options[ddlSucursal.selectedIndex].value;
            var chkSucursalValue = chkSucursal.checked;
            var chkDescripcionValue = ChkDescripcion.checked;

            if (fechaInicioValue == "" || fechaInicioValue == null) {
                showModal("Consulta Log", "Seleccione la Fecha Inicio.");
                return false;
            }

            if (fechaFinValue == "" || tbxFechaFin == null) {
                showModal("Consulta Log", "Seleccione la Fecha Fin.");
                return false;
            }

            if (fechaInicioValue > fechaFinValue) {
                showModal("Consulta Log", "Fecha inicio no puede ser mayor a fecha final. ");
                return false;
            }
            if (fechaFinValue < fechaInicioValue) {
                showModal("Consulta Log", "Fecha fin no puede ser menor a fecha inicio. ");
                return false;
            }

            if (sucursalValue == "0" && !chkSucursalValue) {
                showModal("Consulta Log", "Seleccione la Sucursal o marque la casilla «Todos».");
                return false;
            }
            if (LogDescripcionValue == "" && !chkDescripcionValue) {
                showModal("Consulta Log", "Escriba una descripcion de incidente o marque la casilla «Todos».");
                return false;
            }
            return true;
        }

        function Limpiar() {
            $('#TxtDescripcion').val('');
            $('#ddl_Sucursales').val(0);
            $("#chkSucursal").prop("checked", false);
            $("#ChkDescripcion").prop("checked", false);
            $('#tbx_FechaInicio').val('<%=Format(DateTime.Now, "dd/MM/yyyy")%>');
            $('#tbx_FechaFin').val('<%=Format(DateTime.Now, "dd/MM/yyyy")%>');
            //$("#gv_Log").html("");
        }

        function BuscarDescripcion() {
            $("#TxtDescripcion").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "ConsultaLog.aspx/GetDescripcionLog",
                        data: "{'Descripcion':'" + $('#TxtDescripcion').val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert("No Match");
                        }
                    });
                }
            });
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="titulo-local mb-2">
        <h3>Consulta de Log</h3>
    </div>

    <div class="row">
        <div class="col-4">
            <div class="form-group">
                <label>Fecha Inicio</label>
                <asp:TextBox ID="tbx_FechaInicio" runat="server"  placeholder="Fecha Inicio..." autocomplete="off" class="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-4">
            <div class="form-group">
                <label>Fecha Fin</label>
                <asp:TextBox ID="tbx_FechaFin" runat="server" Class="form-control" placeholder="Fecha Final..." autocomplete="off"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-4">
            <label>Sucursal</label>
        </div>

    </div>
    <div class="row">
        <div class="col-8">
            <asp:DropDownList ID="ddl_Sucursales" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-4">
            <div class="form-check">
                <div class="form-check-input">
                    <asp:CheckBox ID="chkSucursal" runat="server" AutoPostBack="True" />
                </div>
                <div class="form-check-label">
                    <label>Todos</label>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-4">
            <label>Descripción</label>
        </div>
    </div>
    <div class="row">
        <div class="col-8">
            <asp:TextBox ID="TxtDescripcion" runat="server" CssClass="form-control" placeholder="Escriba incidencia..." OnKeyDown="BuscarDescripcion()" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
        </div>
        <div class="col-4">
            <div class="form-check">
                <div class="form-check-input">
                    <asp:CheckBox ID="ChkDescripcion" runat="server" AutoPostBack="True" />
                </div>
                <div class="form-check-label">
                    <label>Todos</label>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="offset-2 col-3">
            <asp:Button ID="btn_Mostrar" runat="server" Text="Mostrar" CssClass="btn btn-block btn-danger mt-2" OnClientClick="return validar();" />
        </div>
        <div class="col-3">
            <asp:Button ID="btn_Limpiar" Text="Limpiar" CssClass="btn btn-block btn-danger mt-2" runat="server" OnClientClick="Limpiar();" />
        </div>
    </div>

    <div class="titulo-local">
        <h4>Incidencias</h4>
    </div>

    <div class="table-responsive mt-3">

             <asp:GridView ID="gv_Log" runat="server" AutoGenerateColumns="False"
            class="table-sm table-bordered table-hover"  PageSize="25"
            AllowPaging="True">
            <Columns>

                <asp:BoundField DataField="IdLog" HeaderText="Id Log" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle Width="100px" Wrap="False" />
                </asp:BoundField>

                <asp:BoundField DataField="IdCajero" HeaderText="Cajero">
                    <ItemStyle HorizontalAlign="Center" Width="90px" Wrap="False" />
                </asp:BoundField>

                <asp:BoundField DataField="NombreSuc" HeaderText="Sucursal" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                </asp:BoundField>

                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" Width="150px" Wrap="False" />
                </asp:BoundField>
              <%--  <asp:BoundField DataField="NombrePantalla" HeaderText="Pantalla" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" Width="90px" Wrap="False" />
                </asp:BoundField>--%>
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center" Width="90px" Wrap="False" />
                </asp:BoundField>

                <asp:BoundField DataField="Hora" HeaderText="Hora" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center" Width="70px" Wrap="False" />
                </asp:BoundField>

                <asp:BoundField DataField="DescripcionConcat" HeaderText="Descripción Completa" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" Width="300px" Wrap="False" />
                </asp:BoundField>

            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
