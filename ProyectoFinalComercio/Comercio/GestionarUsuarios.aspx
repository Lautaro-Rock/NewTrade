<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestionarUsuarios.aspx.cs" Inherits="Comercio.GestionarUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="StyleGestionProductos.css" rel="stylesheet" />
    <title>Gestión de Usuarios</title>
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
                        <button class="accordion-button collapsed bg-transparent text-light ps-0" type="button" data-bs-toggle="collapse" data-bs-target="#collapseUsuarios">
                            Sección Usuarios
                        </button>
                    </h2>
                    <div id="collapseUsuarios" class="accordion-collapse collapse" data-bs-parent="#accordionSidebar">
                        <div class="accordion-body ps-3">
                            <asp:LinkButton ID="btnAgregarUsuario" runat="server" OnClick="btnAgregarUsuario_Click" CssClass="sidebar-link hover-effect">Agregar usuario</asp:LinkButton>
                            <asp:LinkButton ID="btnModificarUsuarioPanel" runat="server" OnClick="btnModificarUsuarioPanel_Click" CssClass="sidebar-link hover-effect">Modificar usuario</asp:LinkButton>
                            <asp:LinkButton ID="btnEliminarUsuarioPanel" runat="server" OnClick="btnEliminarUsuarioPanel_Click" CssClass="sidebar-link hover-effect">Eliminar usuario</asp:LinkButton>
                            <asp:LinkButton ID="btnListarUsuario" runat="server" OnClick="btnListarUsuario_Click" CssClass="sidebar-link hover-effect">Listar usuario</asp:LinkButton>
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

        <%-- Panel del formulario de alta usuario --%>
        <div class="container p-5">
            <asp:Panel ID="PanelFormAltaUsuario" runat="server" CssClass="container bg-light rounded-4 shadow-lg p-4 mt-5">
                <asp:Label ID="lblTituloAgregarUsuario" runat="server" CssClass="h2 text-center mb-4 text-dark fw-bold" Text="Formulario para agregar usuario"></asp:Label>
                <asp:Label ID="lblTituloModificarUsuario" runat="server" CssClass="h2 text-center mb-4 text-dark fw-bold" Text="Formulario para modificar usuario" Visible="false"></asp:Label>

                <div class="mb-3" runat="server" id="divUsuarioModificar" visible="false">
                    <asp:Label AssociatedControlID="ddlUsuarioModificar" runat="server" CssClass="form-label fw-semibold text-dark">Usuario</asp:Label>
                    <asp:DropDownList ID="ddlUsuarioModificar" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlUsuarioModificar_SelectedIndexChanged" />
                </div>

                <div class="row g-4">
                    <!-- Columna izquierda -->
                    <div class="col-12 col-md-6">
                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtNombreUsuario" runat="server" CssClass="form-label fw-semibold text-dark">Nombre</asp:Label>
                            <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" placeholder="Ej: Juan" />
                            <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio" ControlToValidate="txtNombreUsuario" runat="server" ForeColor="Red" ValidationGroup="AltaUsuario" />
                            <asp:RegularExpressionValidator  ID="revNombreUsuario"   runat="server"  ControlToValidate="txtNombreUsuario"  ValidationExpression="^.{2,50}$" ErrorMessage="El nombre debe tener entre 2 y 50 caracteres"  ForeColor="Red" ValidationGroup="AltaUsuario" />

                        </div>

                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtApellidoUsuario" runat="server" CssClass="form-label fw-semibold text-dark">Apellido</asp:Label>
                            <asp:TextBox ID="txtApellidoUsuario" runat="server" CssClass="form-control" placeholder="Ej: Pérez" />
                            <asp:RequiredFieldValidator ErrorMessage="El apellido es obligatorio" ControlToValidate="txtApellidoUsuario" runat="server" ForeColor="Red" ValidationGroup="AltaUsuario" />
                            <asp:RegularExpressionValidator  ID="revApellidoUsuario" runat="server" ControlToValidate="txtApellidoUsuario"  ValidationExpression="^.{2,50}$" ErrorMessage="El apellido debe tener entre 2 y 50 caracteres" ForeColor="Red" ValidationGroup="AltaUsuario" />
                        </div>

                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtDNIUsuario" runat="server" CssClass="form-label fw-semibold text-dark">DNI</asp:Label>
                            <asp:TextBox ID="txtDNIUsuario" runat="server" CssClass="form-control" placeholder="Ej: 47293241" />
                            <asp:RequiredFieldValidator ErrorMessage="El DNI es obligatorio" ControlToValidate="txtDNIUsuario" runat="server" ForeColor="Red" ValidationGroup="AltaUsuario" />
                            <asp:RegularExpressionValidator  ID="revDNIUsuario"  runat="server" ControlToValidate="txtDNIUsuario"  ValidationExpression="^\d{7,8}$" ErrorMessage="El DNI debe tener 7 u 8 dígitos numéricos"  ForeColor="Red" ValidationGroup="AltaUsuario" />

                        </div>

                    </div>

                    <!-- Columna derecha -->
                    <div class="col-12 col-md-6">

                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtEmailUsuario" runat="server" CssClass="form-label fw-semibold text-dark">Email</asp:Label>
                            <asp:TextBox ID="txtEmailUsuario" runat="server" CssClass="form-control" placeholder="Ej: juan.perez@email.com" />
                            <asp:RequiredFieldValidator ErrorMessage="El email es obligatorio" ControlToValidate="txtEmailUsuario" runat="server" ForeColor="Red" ValidationGroup="AltaUsuario" />
                          <asp:RegularExpressionValidator  ID="revEmailUsuario"  runat="server" ControlToValidate="txtEmailUsuario"  ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w{2,4}$"  ErrorMessage="El email no tiene un formato válido"   ForeColor="Red"  ValidationGroup="AltaUsuario" />


                        </div>

                        <div class="form-group mb-3">
    <asp:Label AssociatedControlID="txtContraUsuario" runat="server" CssClass="form-label fw-semibold text-dark">Contraseña</asp:Label>
    <div class="d-flex align-items-start">
        <asp:TextBox 
            ID="txtContraUsuario" 
            runat="server" 
            CssClass="form-control me-2" 
            placeholder="Ej: Contraseñasegura993!" 
            TextMode="Password" />

        <span style="display: inline-flex; flex-direction: column;">
            <asp:RequiredFieldValidator 
                ErrorMessage="La contraseña es obligatoria" 
                ControlToValidate="txtContraUsuario" 
                runat="server" 
                ForeColor="Red" 
                ValidationGroup="AltaUsuario" 
                Display="Dynamic" />
            
            <asp:RegularExpressionValidator 
                ID="revContraUsuario"  
                runat="server"  
                ControlToValidate="txtContraUsuario"  
                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"  
                ErrorMessage="La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un número"  
                ForeColor="Red"  
                ValidationGroup="AltaUsuario" 
                Display="Dynamic" />
                      </span>
                      </div>
                        </div>
                        <div class="d-grid mt-4">
                            <asp:Button ID="btnGuardarUsuario" runat="server" Text="Agregar usuario" CssClass="btn btn-success btn-lg fw-bold" OnClick="btnGuardarUsuario_Click" ValidationGroup="AltaUsuario" />
                            <asp:Button ID="btnModificarUsuario" runat="server" Text="Modificar usuario" CssClass="btn btn-warning btn-lg fw-bold" OnClick="btnModificarUsuario_Click" Visible="false" ValidationGroup="AltaUsuario" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <%-- Panel del listado de usuarios --%>
        <div class="container">
            <asp:Panel ID="PanelListarUsuario" runat="server">
               <h1>Lista de Usuarios...</h1>
                <div class="row g-4">
                   <asp:Repeater ID="rptUsuarios" runat="server">
                        <ItemTemplate>
                            <div class="col-12 col-md-6">
                                <div class="card mb-3" style="max-width: 100%;">
                                    <div class="row g-0">
                                        <div class="col-md-4 fondo-imagen">
                                            <img src="\images\user.png" class="img-fluid rounded-start" alt="...">
                                        </div>
                                        <div class="col-md-8">
                                            <div class="card-body">
                                                <h5 class="card-title"><%# Eval("Nombre") %> <%# Eval("Apellido") %></h5>
                                                <p class="card-text mb-1"><strong>DNI:</strong> <%# Eval("Dni") %></p>
                                                <p class="card-text mb-1"><strong>Email:</strong> <%# Eval("Email") %></p>
                                                <asp:Button ID="btnModificarUsuarioListado" runat="server" Text="Modificar" CssClass="btn btn-outline-warning me-2" CommandArgument='<%# Eval("Id") %>' OnClick="btnModificarUsuarioListado_Click" />
                                                <asp:Button ID="btnEliminarUsuarioListado" runat="server" Text="Eliminar" CssClass="btn btn-outline-warning me-2" CommandArgument='<%# Eval("Id") %>' OnClick="btnEliminarUsuarioListado_Click" />
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

        <%-- Panel del eliminar usuario --%>
        <asp:Panel ID="PanelEliminarUsuario" runat="server" CssClass="container bg-light text-dark rounded-4 shadow p-4 mt-4" Style="max-width: 750px;">
            <h3 class="text-center fw-bold mb-4">Eliminar usuario</h3>

            <div class="mb-3">
                <label for="ddlUsuarioEliminar" class="form-label fw-semibold">Seleccione el usuario</label>
                <asp:DropDownList ID="ddlUsuarioEliminar" runat="server" CssClass="form-select" AutoPostBack="true" />
            </div>

            <asp:Panel ID="PanelConfirmacionUsuario" runat="server" Visible="true" CssClass="bg-warning bg-opacity-10 border border-warning rounded-3 p-3 mt-3">
                <p class="text-warning fw-semibold mb-3">¿Estás seguro que querés eliminar este usuario?</p>
                <div class="d-flex justify-content-end gap-3">
                    <asp:Button ID="btnEliminarUsuario" runat="server" Text="Eliminar" CssClass="btn btn-danger px-4" OnClick="btnEliminarUsuario_Click" />
                    <asp:Button ID="btnCancelarUsuario" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-4" />
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