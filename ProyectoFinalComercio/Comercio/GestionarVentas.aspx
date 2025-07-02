<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestionarVentas.aspx.cs" Inherits="Comercio.GestionarVentas" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Gestión de Ventas</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="StyleGestionProductos.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body runat="server" id="bodyTag">
    <form id="form1" runat="server">

        <!-- Botón para desplegar la barra lateral -->
        <button id="toggleSidebar" type="button" class="btn btn-warning">☰</button>

        <!-- Barra lateral -->
        <div id="sidebar" class="sidebar">
            <h1 class="sidebar-title">Comsys</h1>

            <div class="accordion accordion-flush" id="accordionSidebar">
                <div class="accordion-item bg-transparent border-0">
                    <h2 class="accordion-header">
                        <button class="accordion-button collapsed bg-transparent text-light ps-0" type="button" data-bs-toggle="collapse" data-bs-target="#collapseVentas">
                            Sección Ventas
                        </button>
                    </h2>
                    <div id="collapseVentas" class="accordion-collapse collapse show" data-bs-parent="#accordionSidebar">
                        <div class="accordion-body ps-3">
                            <asp:LinkButton ID="btnNuevaVenta" runat="server" OnClick="btnNuevaVenta_Click" CssClass="sidebar-link hover-effect">Nueva venta</asp:LinkButton>
                            <asp:LinkButton ID="btnActualizarListado" runat="server" OnClick="btnActualizarListado_Click" CssClass="sidebar-link hover-effect">Actualizar listado</asp:LinkButton>
                        </div>
                    </div>
                </div>

                <div class="accordion-item bg-transparent border-0">
                    <h2 class="accordion-header">
                        <button class="accordion-button collapsed bg-transparent text-light ps-0" type="button" data-bs-toggle="collapse" data-bs-target="#collapseGeneral">
                            Sección General
                        </button>
                    </h2>
                    <div id="collapseGeneral" class="accordion-collapse collapse" data-bs-parent="#accordionSidebar">
                        <div class="accordion-body ps-3">
                            <asp:LinkButton ID="btnVolver" runat="server" OnClick="btnVolver_Click" CssClass="sidebar-link hover-effect">Volver al panel</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="blurOverlay" class="blur-overlay"></div>

        <!-- Panel de ventas -->
        <div class="container mt-5">
            <h1 class="text-center text-white mb-4" style="font-family: 'Special Elite', monospace; font-size: 2.5rem;">
                Gestión de Ventas
            </h1>

            <div class="container mt-3 mb-4">
  <div class="row justify-content-center align-items-center g-3">
    <div class="col-md-3 d-flex justify-content-center align-items-center">
      <asp:Label Text="Filtrar" runat="server" AssociatedControlID="filtroUno" class="me-2 mb-0" />
      <asp:TextBox runat="server" ID="filtroUno" CssClass="form-control" AutoPostBack="true" OnTextChanged="filtroUno_TextChanged"  style="max-width: 250px;" />
    </div>
    <div class="col-md-2 d-flex align-items-center">
      <asp:CheckBox ID="checkFiltrarAvanzado" runat="server" AutoPostBack="true" OnCheckedChanged="checkFiltrarAvanzado_CheckedChanged"/>
      <asp:Label Text="Filtro Avanzado" runat="server" AssociatedControlID="checkFiltrarAvanzado" CssClass="ms-1 mb-0" />
    </div>
  </div>
</div>

    <% if (FiltroAvanzado) { %>
        <div class="row justify-content-center g-3 ">
            <div class="col-md-2">
                <asp:Label Text="Campo" runat="server" AssociatedControlID="ddlCampoSelectUsuario" />
                <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="ddlCampoSelectUsuario" OnSelectedIndexChanged="ddlCampoSelectUsuario_SelectedIndexChanged">
                    <asp:ListItem Text="Factura" />
                    <asp:ListItem Text="Cliente" />
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Label Text="Criterio" runat="server" AssociatedControlID="ddlCriterio" />
                <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-control" default="Selecciona un campo" />
            </div>
            <div class="col-md-2">
                <asp:Label Text="Filtro" runat="server" AssociatedControlID="ddlFiltroAvanzado" />
                <asp:TextBox runat="server" ID="ddlFiltroAvanzado" CssClass="form-control" />
            </div>
            <div class="col-md-2">
                <asp:Label Text="Estado" runat="server" AssociatedControlID="ddlEstado" />
                <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control">
                    <asp:ListItem Text="Todos" />
                    <asp:ListItem Text="Activo" />
                    <asp:ListItem Text="Inactivo" />
                </asp:DropDownList>
            </div>
        </div>
<div class="row my-4">
    <div class="col text-center">
        <asp:Button Text="Buscar" runat="server" CssClass="btn btn-primary w-25" ID="btnBuscarVenta" OnClick="btnBuscarVenta_Click" />
        <asp:Button ID="btnLimpiarFiltro" runat="server"
        Text="Limpiar Filtros"
        CssClass="btn btn-secondary"
        OnClick="btnLimpiarFiltro_Click" />
    </div>
</div>

    <% } %>

            <asp:GridView ID="gvVentas" runat="server" DataKeyNames="Id" AutoGenerateColumns="False"
                CssClass="table table-dark table-hover text-white mt-4" OnRowCommand="gvVentas_RowCommand">
                <Columns>
                    <asp:BoundField DataField="NumeroFactura" HeaderText="Factura" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                    <asp:BoundField DataField="Usuario" HeaderText="Registrado por:" />
                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C2}" />
                    <asp:ButtonField ButtonType="Button" Text="Modificar" CommandName="Modificar" ControlStyle-CssClass="btn btn-warning btn-sm" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <button type="button" class="btn btn-danger btn-sm" onclick="confirmarEliminacion('<%# Eval("Id") %>')">
                                    Eliminar
                                </button>
                            </ItemTemplate>
                        </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>

    <!-- Scripts -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        const toggleBtn = document.getElementById('toggleSidebar');
        const sidebar = document.getElementById('sidebar');
        const blurOverlay = document.getElementById('blurOverlay');

        toggleBtn.addEventListener('click', function () {
            sidebar.classList.toggle('show');
            blurOverlay.classList.toggle('active');
            toggleBtn.classList.toggle('move-right');
        });

        blurOverlay.addEventListener('click', function () {
            sidebar.classList.remove('show');
            blurOverlay.classList.remove('active');
            toggleBtn.classList.remove('move-right');
        });
    </script>

    <script>
        function confirmarEliminacion(idCompra) {
            Swal.fire({
                title: '¿Estás seguro?',
                text: "Esta acción eliminará la venta.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {
                    __doPostBack('EliminarVenta', idCompra);
                }
            });
        }
    </script>
</body>
</html>

