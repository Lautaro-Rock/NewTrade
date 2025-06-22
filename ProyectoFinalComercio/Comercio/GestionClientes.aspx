<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestionClientes.aspx.cs" Inherits="Comercio.Prototipo2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="StyleGestionProductos.css" rel="stylesheet" />
    <title>Gestión de Clientes</title>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
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
                        <button class="accordion-button collapsed bg-transparent text-light ps-0" type="button" data-bs-toggle="collapse" data-bs-target="#collapseClientes">
                            Sección Clientes
                        </button>
                    </h2>
                    <div id="collapseClientes" class="accordion-collapse collapse" data-bs-parent="#accordionSidebar">
                        <div class="accordion-body ps-3">
                            <asp:LinkButton ID="btnAgregarCliente" runat="server" OnClick="btnAgregarCliente_Click" CssClass="sidebar-link hover-effect">Agregar cliente</asp:LinkButton>
                            <asp:LinkButton ID="btnModificarCliente" runat="server" OnClick="btnModificarCliente_Click" CssClass="sidebar-link hover-effect">Modificar cliente</asp:LinkButton>
                            <asp:LinkButton ID="btnEliminarCliente" runat="server" OnClick="btnEliminarCliente_Click" CssClass="sidebar-link hover-effect">Eliminar cliente</asp:LinkButton>
                            <asp:LinkButton ID="btnListarCliente" runat="server" OnClick="btnListarCliente_Click" CssClass="sidebar-link hover-effect">Listar cliente</asp:LinkButton>
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

        <%-- Panel del formulario de alta producto --%>
        <div class="container p-5">
            <asp:Panel ID="PanelFormAltaCliente" runat="server" CssClass="container bg-light rounded-4 shadow-lg p-4 mt-5">
                <asp:Label ID="lblTituloAgregar" runat="server" CssClass="h2 text-center mb-4 text-dark fw-bold" Text="Formulario para agregar cliente"></asp:Label>
                <asp:Label ID="lblTituloModificar" runat="server" CssClass="h2 text-center mb-4 text-dark fw-bold" Text="Formulario para modificar cliente" Visible="false"></asp:Label>

                <div class="mb-3" runat="server" id="divClienteModificar" visible="false">
                    <asp:Label AssociatedControlID="ddlClienteModificar" runat="server" CssClass="form-label fw-semibold text-dark">Cliente</asp:Label>
                    <asp:DropDownList ID="ddlClienteModificar" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlClienteModificar_SelectedIndexChanged" />
                </div>

                <div class="row g-4">
                    <!-- Columna izquierda -->
                    <div class="col-12 col-md-6">
                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtNombreCliente" runat="server" CssClass="form-label fw-semibold text-dark">Nombre</asp:Label>
                            <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="form-control" placeholder="Ej: Roberto" />
                            <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio" ControlToValidate="txtNombreCliente" runat="server" ForeColor="Red" ValidationGroup="AltaCliente" />
                        </div>

                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtApellido" runat="server" CssClass="form-label fw-semibold text-dark">Apellido</asp:Label>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ej: Sánchez" />
                            <asp:RequiredFieldValidator ErrorMessage="El apellido es obligatorio" ControlToValidate="txtApellido" runat="server" ForeColor="Red" ValidationGroup="AltaCliente" />
                        </div>
                    </div>

                    <!-- Columna derecha -->
                    <div class="col-12 col-md-6">
                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtDNI" runat="server" CssClass="form-label fw-semibold text-dark">DNI</asp:Label>
                            <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" placeholder="Ej: 47293241" />
                            <asp:RequiredFieldValidator ErrorMessage="El DNI es obligatorio" ControlToValidate="txtDNI" runat="server" ForeColor="Red" ValidationGroup="AltaCliente" />
                        </div>

                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtEmail" runat="server" CssClass="form-label fw-semibold text-dark">Email</asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ej: roberto.sanchez@gmail.com" />
                            <asp:RequiredFieldValidator ErrorMessage="El email es obligatorio" ControlToValidate="txtEmail" runat="server" ForeColor="Red" ValidationGroup="AltaCliente" />
                        </div>

                        <div class="d-grid mt-4">
                            <asp:Button ID="btnGuardarCliente" runat="server" Text="Agregar producto" CssClass="btn btn-success btn-lg fw-bold" OnClick="btnGuardarCliente_Click" ValidationGroup="AltaProducto" />
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar producto" CssClass="btn btn-warning btn-lg fw-bold" OnClick="btnModificar_Click" Visible="false" ValidationGroup="AltaProducto" />
                        </div>
                    </div>
                </div>
            </asp:Panel>

        </div>

        <%-- Panel del listado de productos --%>
        <div class="container">
            <asp:Panel ID="PanelListarCliente" runat="server">
               <h1>Lista de Clientes...</h1>
                <div class="row g-4">
                   <asp:Repeater ID="rptClientes" runat="server">
                        <ItemTemplate>
                            <div class="col-12 col-md-6">
                                <div class="card mb-3" style="max-width: 100%;">
                                    <div class="row g-0">
                                        <div class="col-md-4 fondo-imagen">
                                            <img src="\images\client.png" class="img-fluid rounded-start" alt="...">
                                        </div>
                                        <div class="col-md-8">
                                            <div class="card-body">
                                                <h5 class="card-title"><%# Eval("Nombre") %> <%# Eval("Apellido") %></h5>
                                                <p class="card-text mb-1"><strong>DNI:</strong> <%# Eval("Dni") %></p>
                                                <p class="card-text mb-1"><strong>Email:</strong> <%# Eval("Email") %></p>
                                                <asp:Button ID="btnModificarClienteListado" runat="server" Text="Modificar" CssClass="btn btn-outline-warning me-2" CommandArgument='<%# Eval("Id") %>' OnClick="btnModificarClienteListado_Click" />
                                                <asp:Button ID="btnEliminarClienteListado" runat="server" Text="Eliminar" CssClass="btn btn-outline-warning me-2" CommandArgument='<%# Eval("Id") %>' OnClick="btnEliminarClienteListado_Click" />
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

        <%-- Panel del eliminar producto --%>
        <asp:Panel ID="PanelEliminarCliente" runat="server" CssClass="container bg-light text-dark rounded-4 shadow p-4 mt-4" Style="max-width: 750px;">
            <h3 class="text-center fw-bold mb-4">Eliminar cliente</h3>

            <div class="mb-3">
                <label for="ddlClienteEliminar" class="form-label fw-semibold">Seleccione el cliente</label>
                <asp:DropDownList ID="ddlClienteEliminar" runat="server" CssClass="form-select" AutoPostBack="true" />
            </div>

            <asp:Panel ID="PanelConfirmacion" runat="server" Visible="true" CssClass="bg-warning bg-opacity-10 border border-warning rounded-3 p-3 mt-3">
                <p class="text-warning fw-semibold mb-3">¿Estás seguro que querés eliminar este cliente?</p>
                <div class="d-flex justify-content-end gap-3">
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger px-4" OnClick="btnEliminar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-4" />
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
