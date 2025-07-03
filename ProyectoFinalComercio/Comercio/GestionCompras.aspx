<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="GestionCompras.aspx.cs" Inherits="Comercio.GestionCompras" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Gestión de Compras</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="StyleGestionProductos.css" rel="stylesheet" />
    <link href="Compras.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />

        <!-- Botón para desplegar la barra lateral -->
        <button id="toggleSidebar" type="button" class="btn btn-warning">☰</button>

        <!-- Barra lateral -->
        <div id="sidebar" class="sidebar">
            <h1 class="sidebar-title">Comsys</h1>

            <div class="accordion accordion-flush" id="accordionSidebar">
                <div class="accordion-item bg-transparent border-0">
                    <h2 class="accordion-header">
                        <button class="accordion-button collapsed bg-transparent text-light ps-0" type="button" data-bs-toggle="collapse" data-bs-target="#collapseCompras">
                            Sección Compras
                        </button>
                    </h2>
                    <div id="collapseCompras" class="accordion-collapse collapse show" data-bs-parent="#accordionSidebar">
                        <div class="accordion-body ps-3">
                            <asp:LinkButton ID="btnNuevaCompra" runat="server" OnClick="btnNuevaCompra_Click" CssClass="sidebar-link hover-effect">Nueva compra</asp:LinkButton>
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


        <div class="container p-3">
            <asp:UpdatePanel ID="UpPnlCompras" runat="server">
                <ContentTemplate>
                    <div class="text-center mb-4">
                        <h2 class="text-white fw-bold">Gestión de Compras</h2>
                        <hr class="border-light" />
                    </div>
                    <div class="row g-3 align-items-end mb-4">
                        <div class="col-md-4">
                            <asp:Label Text="Filtro rápido" runat="server" AssociatedControlID="TxtFiltroRápidoCompras" CssClass="form-label text-white" />
                            <asp:TextBox ID="TxtFiltroRápidoCompras" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtFiltroRápidoCompras_TextChanged" />
                        </div>
                    </div>
                    <div class="row g-3 align-items-end mb-4">
                        <div class="col-md-4">
                            <div>
                                <asp:CheckBox ID="CheckFiltroAvanzadoCompras" runat="server" AutoPostBack="true" OnCheckedChanged="CheckFiltroAvanzadoCompras_CheckedChanged" />
                                <asp:Label Text="Filtro Avanzado" runat="server" AssociatedControlID="CheckFiltroAvanzadoCompras" CssClass="form-check-label text-white ms-2" />
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="PnlFiltroAvanzadoCompras" runat="server" Visible="false" CssClass="mb-4">
                        <div class="row g-3 align-items-end">
                            <div class="col-md-3">
                                <asp:Label Text="Campo" runat="server" AssociatedControlID="DdlCampoCompras" CssClass="form-label text-white" />
                                <asp:DropDownList runat="server" ID="DdlCampoCompras" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="DdlCampoCompras_SelectedIndexChanged">
                                    <asp:ListItem Text="--> Seleccione un campo <--" Value="" />
                                    <asp:ListItem Text="Por Fecha" Value="Nombre" />
                                    <asp:ListItem Text="Por Total" Value="Id" />
                                    <asp:ListItem Text="Por Nombre del Proveedor" Value="ProveedorNombre" />
                                    <asp:ListItem Text="Por Nombre del Usuario" Value="UsuarioNombre" />
                                    <asp:ListItem Text="Por Apellido del Usuario" Value="UsuarioApellido" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label Text="Criterio" runat="server" AssociatedControlID="DdlCriterioCompras" CssClass="form-label text-white" />
                                <asp:DropDownList runat="server" ID="DdlCriterioCompras" CssClass="form-select" />
                            </div>
                            <div class="col-md-3">
                                <asp:Label Text="Filtro" runat="server" AssociatedControlID="TxtFiltroAvanzadoCompras" CssClass="form-label text-white" />
                                <asp:TextBox runat="server" ID="TxtFiltroAvanzadoCompras" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="lblEstadoCompra" runat="server" CssClass="form-label text-white" Text="Estado de la compra" />
                            <asp:DropDownList ID="ddlEstadoCompra" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Solo activos" Value="1" />
                                <asp:ListItem Text="Solo inactivos" Value="0" />
                                <asp:ListItem Text="Todos" Value="todos" />
                            </asp:DropDownList>
                        </div>


                        <div class="row g-3 mt-3">
                            <div class="col-md-3 d-flex gap-2">
                                <asp:Button ID="BtnBuscarAvanzadoCompras" runat="server" Text="Buscar" CssClass="btn btn-outline-info w-50" OnClick="BtnBuscarAvanzadoCompras_Click" />
                                <asp:Button ID="BtnLimpiarAvanzadoMarca" runat="server" Text="Limpiar" CssClass="btn btn-outline-light w-50" OnClick="BtnLimpiarAvanzadoMarca_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:GridView ID="gvCompras" runat="server" DataKeyNames="Id" AutoGenerateColumns="False"
                        CssClass="table color-table-personalizado" OnRowCommand="gvCompras_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Proveedor.RazonSocial" HeaderText="Proveedor" />
                            <asp:TemplateField HeaderText="Registrado por:">
                                <ItemTemplate>
                                    <%# Eval("Usuario.Nombre") + " " + Eval("Usuario.Apellido") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C2}" />
                            <asp:ButtonField ButtonType="Button" Text="Modificar/Ver detalle" CommandName="Ver" ControlStyle-CssClass="btn btn-warning btn-sm" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <button type="button" class="btn btn-danger btn-sm" onclick="confirmarEliminacion('<%# Eval("Id") %>')">
                                        Eliminar
                                    </button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
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
                text: "Esta acción ocultará la compra.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {
                    __doPostBack('EliminarCompra', idCompra);
                }
            });
        }
    </script>

</body>
</html>
