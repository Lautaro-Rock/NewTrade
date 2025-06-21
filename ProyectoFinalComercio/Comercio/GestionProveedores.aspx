<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="GestionProveedores.aspx.cs" Inherits="Comercio.GestionProveedores" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="StyleGestionProductos.css" rel="stylesheet" />
    <title>Gestión de Proveedores</title>
</head>
<body runat="server" id="bodyTag">
    <form id="form1" runat="server">
        <button id="toggleSidebar" type="button" class="btn btn-warning">☰</button>
        <div id="sidebar" class="sidebar">
            <h1 class="sidebar-title">Comsys</h1>

            <div class="accordion accordion-flush" id="accordionSidebar">
                <%--COLUMNAS DE LA IZQUIERDA--%>
                <div class="accordion-item bg-transparent border-0">
                    <h2 class="accordion-header">
                        <button class="accordion-button collapsed bg-transparent text-light ps-0" type="button" data-bs-toggle="collapse" data-bs-target="#collapseProveedores">
                            Sección Proveedores
                        </button>
                    </h2>
                    <div id="collapseProveedores" class="accordion-collapse collapse" data-bs-parent="#accordionSidebar">
                        <div class="accordion-body ps-3">
                            <asp:LinkButton ID="btnAgregarProveedor" runat="server" OnClick="btnAgregarProveedor_Click" CssClass="sidebar-link hover-effect">Agregar proveedor</asp:LinkButton>
                            <asp:LinkButton ID="btnModificarProveedorPanel" runat="server" OnClick="btnModificarProveedorPanel_Click" CssClass="sidebar-link hover-effect">Modificar proveedor</asp:LinkButton>
                            <asp:LinkButton ID="btnEliminarProveedorPanel" runat="server" OnClick="btnEliminarProveedorPanel_Click" CssClass="sidebar-link hover-effect">Eliminar proveedor</asp:LinkButton>
                            <asp:LinkButton ID="btnListarProveedor" runat="server" OnClick="btnListarProveedor_Click" CssClass="sidebar-link hover-effect">Listar proveedor</asp:LinkButton>
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
                            <asp:LinkButton ID="btnVolverPanel" runat="server" OnClick="btnVolverPanelClick" CssClass="sidebar-link hover-effect">Volver al panel</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="blurOverlay" class="blur-overlay"></div>

        <%-- Panel del formulario de alta proveedor --%>
        <div class="container p-5">
            <asp:Panel ID="PanelFormAltaProveedor" runat="server" CssClass="container bg-light rounded-4 shadow-lg p-4 mt-5">
                <asp:Label ID="lblTituloAgregarProveedor" runat="server" CssClass="h2 text-center mb-4 text-dark fw-bold" Text="Formulario para agregar proveedor"></asp:Label>
                <asp:Label ID="lblTituloModificarProveedor" runat="server" CssClass="h2 text-center mb-4 text-dark fw-bold" Text="Formulario para modificar proveedor" Visible="false"></asp:Label>

                <div class="mb-3" runat="server" id="divProveedorModificar" visible="false">
                    <asp:Label AssociatedControlID="ddlProveedorModificar" runat="server" CssClass="form-label fw-semibold text-dark">Proveedor</asp:Label>
                    <asp:DropDownList ID="ddlProveedorModificar" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProveedorModificar_SelectedIndexChanged" />
                </div>

                <div class="row g-4">
                    <!-- Columna izquierda -->
                    <div class="col-12 col-md-6">
                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtNombreProveedor" runat="server" CssClass="form-label fw-semibold text-dark">Razón social</asp:Label>
                            <asp:TextBox ID="txtNombreProveedor" runat="server" CssClass="form-control" placeholder="Ej: Comsys SA" />
                            <asp:RequiredFieldValidator ErrorMessage="La razón social es obligatoria" ControlToValidate="txtNombreProveedor" runat="server" ForeColor="Red" ValidationGroup="AltaProveedor" />
                        </div>

                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtDireccionProveedor" runat="server" CssClass="form-label fw-semibold text-dark">Dirección</asp:Label>
                            <asp:TextBox ID="txtDireccionProveedor" runat="server" CssClass="form-control" placeholder="Ej: Machain 9321" />
                            <asp:RequiredFieldValidator ErrorMessage="La dirección es obligatoria" ControlToValidate="txtDireccionProveedor" runat="server" ForeColor="Red" ValidationGroup="AltaProveedor" />
                        </div>

                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtTelefonoProveedor" runat="server" CssClass="form-label fw-semibold text-dark">Telefono</asp:Label>
                            <asp:TextBox ID="txtTelefonoProveedor" runat="server" CssClass="form-control" placeholder="Ej: 54321912" />
                            <asp:RequiredFieldValidator ErrorMessage="El número de telefono es obligatorio" ControlToValidate="txtTelefonoProveedor" runat="server" ForeColor="Red" ValidationGroup="AltaProveedor" />
                        </div>

                    </div>

                    <!-- Columna derecha -->
                    <div class="col-12 col-md-6">
                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtCUITProveedor" runat="server" CssClass="form-label fw-semibold text-dark">CUIT</asp:Label>
                            <asp:TextBox ID="txtCUITProveedor" runat="server" CssClass="form-control" placeholder="Ej: 23-3219876-2" />
                            <asp:RequiredFieldValidator ErrorMessage="El CUIT es obligatorio" ControlToValidate="txtCUITProveedor" runat="server" ForeColor="Red" ValidationGroup="AltaProveedor" />
                        </div>

                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtEmailProveedor" runat="server" CssClass="form-label fw-semibold text-dark">Email</asp:Label>
                            <asp:TextBox ID="txtEmailProveedor" runat="server" CssClass="form-control" placeholder="Ej: comsys@email.com" />
                            <asp:RequiredFieldValidator ErrorMessage="El email es obligatorio" ControlToValidate="txtEmailProveedor" runat="server" ForeColor="Red" ValidationGroup="AltaProveedor" />
                        </div>

                        <div class="d-grid mt-4">
                            <asp:Button ID="btnGuardarProveedor" runat="server" Text="Agregar proveedor" CssClass="btn btn-success btn-lg fw-bold" OnClick="btnGuardarProveedor_Click" ValidationGroup="AltaProveedor" />
                            <asp:Button ID="btnModificarProveedor" runat="server" Text="Modificar proveedor" CssClass="btn btn-warning btn-lg fw-bold" OnClick="btnModificarProveedor_Click" Visible="false" ValidationGroup="AltaProveedor" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <%-- Panel del listado de proveedores --%>
        <div class="container">
            <asp:Panel ID="PanelListarProveedor" runat="server">
               <h1>Lista de Proveedores...</h1>
                <div class="row g-4">
                   <asp:Repeater ID="rptProveedores" runat="server">
                        <ItemTemplate>
                            <div class="col-12 col-md-6">
                                <div class="card mb-3" style="max-width: 100%;">
                                    <div class="row g-0">
                                        <div class="col-md-4 fondo-imagen">
                                            <img src="\images\delivery-truck.png" class="img-fluid rounded-start" alt="...">
                                        </div>
                                        <div class="col-md-8">
                                            <div class="card-body">
                                                <h5 class="card-title"><%# Eval("RazonSocial") %> </h5>
                                                <p class="card-text mb-1"><strong>Email:</strong> <%# Eval("Email") %></p>
                                                <p class="card-text mb-1"><strong>CUIT:</strong> <%# Eval("Cuit") %></p>
                                                <p class="card-text mb-1"><strong>Telefono:</strong> <%# Eval("Telefono") %></p
                                                <p class="card-text mb-1"><strong>Dirección:</strong> <%# Eval("Direccion") %></p>
                                                <asp:Button ID="btnModificarProveedorListado" runat="server" Text="Modificar" CssClass="btn btn-outline-warning me-2" CommandArgument='<%# Eval("Id") %>' OnClick="btnModificarProveedorListado_Click" />
                                                <asp:Button ID="btnEliminarProveedorListado" runat="server" Text="Eliminar" CssClass="btn btn-outline-warning me-2" CommandArgument='<%# Eval("Id") %>' OnClick="btnEliminarProveedorListado_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:Panel>
        </div>

        <%-- Panel del eliminar proveedor --%>
        <asp:Panel ID="PanelEliminarProveedor" runat="server" CssClass="container bg-light text-dark rounded-4 shadow p-4 mt-4" Style="max-width: 750px;">
            <h3 class="text-center fw-bold mb-4">Eliminar proveedor</h3>

            <div class="mb-3">
                <label for="ddlProveedorEliminar" class="form-label fw-semibold">Seleccione el proveedor</label>
                <asp:DropDownList ID="ddlProveedorEliminar" runat="server" CssClass="form-select" AutoPostBack="true" />
            </div>

            <asp:Panel ID="PanelConfirmacionProveedor" runat="server" Visible="true" CssClass="bg-warning bg-opacity-10 border border-warning rounded-3 p-3 mt-3">
                <p class="text-warning fw-semibold mb-3">¿Estás seguro que querés eliminar este proveedor?</p>
                <div class="d-flex justify-content-end gap-3">
                    <asp:Button ID="btnEliminarProveedor" runat="server" Text="Eliminar" CssClass="btn btn-danger px-4" OnClick="btnEliminarProveedor_Click" />
                    <asp:Button ID="btnCancelarProveedor" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-4" />
                </div>
            </asp:Panel>
        </asp:Panel>

    </form>

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
</body>
</html>
