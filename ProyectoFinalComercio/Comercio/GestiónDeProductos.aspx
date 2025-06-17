<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestiónDeProductos.aspx.cs" Inherits="Comercio.Prototipo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="StyleGestionProductos.css" rel="stylesheet" />
    <title>Gestión de Productos</title>
</head>
<body runat="server" id="bodyTag">
    <form id="form1" runat="server">
        <button id="toggleSidebar" type="button" class="btn btn-warning">☰</button>
        <div id="sidebar" class="sidebar">
            <h1 class="sidebar-title">Comsys</h1>
            <asp:LinkButton ID="btnListarProd" runat="server" OnClick="btnListarProdClick" CssClass="sidebar-link hover-effect">Listar productos</asp:LinkButton>
            <asp:LinkButton ID="btnAgregarProd" runat="server" OnClick="btnAgregarProdClick" CssClass="sidebar-link hover-effect">Agregar producto</asp:LinkButton>
            <asp:LinkButton ID="btnModificarProd" runat="server" CssClass="sidebar-link hover-effect">Modificar producto</asp:LinkButton>
            <asp:LinkButton ID="btnEliminarProd" runat="server" OnClick="btnEliminarProdClick" CssClass="sidebar-link hover-effect">Eliminar producto</asp:LinkButton>
            <asp:LinkButton ID="btnAgregarMarca" runat="server" OnClick="btnAgregarMarcaClick" CssClass="sidebar-link hover-effect">Agregar marca</asp:LinkButton>
            <asp:LinkButton ID="btnVolverPanel" runat="server" OnClick="btnVolverPanelClick" CssClass="sidebar-link hover-effect">Volver al panel</asp:LinkButton>
        </div>
        <div id="blurOverlay" class="blur-overlay"></div>

        <%-- Lo tengo que separar porque me re pierdo --%>

        <%-- Panel del formulario de alta producto --%>
        <div class="container p-5">
            <asp:Panel ID="PanelFormAltaProd" runat="server" CssClass="container bg-light rounded-4 shadow-lg p-4 mt-5">
    <h2 class="text-center mb-4 text-dark fw-bold">Formulario para agregar producto</h2>

    <div class="row g-4">
        <!-- Columna izquierda -->
        <div class="col-12 col-md-6">
            <div class="form-group mb-3">
                <asp:Label AssociatedControlID="txtNombreProd" runat="server" CssClass="form-label fw-semibold text-dark">Nombre del producto</asp:Label>
                <asp:TextBox ID="txtNombreProd" runat="server" CssClass="form-control" placeholder="Ej: Taladro Black+Decker" />
            </div>

            <div class="form-group mb-3">
                <asp:Label AssociatedControlID="ddlMarcas" runat="server" CssClass="form-label fw-semibold text-dark">Marca</asp:Label>
                <asp:DropDownList ID="ddlMarcas" runat="server" CssClass="form-select" />
            </div>

            <div class="form-group mb-3">
                <asp:Label AssociatedControlID="ddlTipoDeProducto" runat="server" CssClass="form-label fw-semibold text-dark">Tipo de producto</asp:Label>
                <asp:DropDownList ID="ddlTipoDeProducto" runat="server" CssClass="form-select" />
            </div>

            <div class="form-group mb-3">
                <asp:Label AssociatedControlID="txtPrecio" runat="server" CssClass="form-label fw-semibold text-dark">Precio</asp:Label>
                <div class="input-group">
                    <span class="input-group-text bg-secondary text-white">$</span>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" placeholder="0.00" TextMode="Number" />
                </div>
            </div>
        </div>

        <!-- Columna derecha -->
        <div class="col-12 col-md-6">
            <div class="form-group mb-3">
                <asp:Label AssociatedControlID="txtStock" runat="server" CssClass="form-label fw-semibold text-dark">Stock</asp:Label>
                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" placeholder="Ej: 100 unidades" TextMode="Number" />
            </div>

            <div class="form-group mb-3">
                <asp:Label AssociatedControlID="TextBox4" runat="server" CssClass="form-label fw-semibold text-dark">Stock mínimo</asp:Label>
                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" placeholder="Ej: 10" TextMode="Number" />
            </div>

            <div class="form-group mb-3">
                <asp:Label AssociatedControlID="TextBox2" runat="server" CssClass="form-label fw-semibold text-dark">URL de la imagen</asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="https://ejemplo.com/imagen.jpg" />
            </div>

            <div class="d-grid mt-4">
                <asp:Button ID="btnGuardarProducto" runat="server" Text="Agregar producto" CssClass="btn btn-success btn-lg fw-bold" />
            </div>
        </div>
    </div>
</asp:Panel>

        </div>

        <%-- Panel del listado de productos --%>
        <div class="container">
            <asp:Panel ID="PanelListarProd" runat="server">
                <h1>Lista de productos...</h1>
                <div class="row g-4">
                    <% foreach (Dominio.Producto temporalpr in Productos)
                        { %>
                    <div class="col-12 col-md-6">

                        <div class="card mb-3" style="max-width: 100%;">
                            <div class="row g-0">
                                <div class="col-md-4 fondo-imagen">
                                    <img src="<%: temporalpr.UrlImgProducto %>" class="img-fluid rounded-start" alt="...">
                                </div>
                                <div class="col-md-8">
                                    <div class="card-body">
                                        <h5 class="card-title"><%: temporalpr.Nombre %></h5>
                                        <asp:Button ID="Button1" runat="server" Text="Modificar" CssClass="btn btn-outline-warning me-2" />
                                        <asp:Button ID="Button2" runat="server" Text="Eliminar" CssClass="btn btn-outline-warning me-2" />
                                        <asp:Button ID="Button3" runat="server" Text="Ver mas.." CssClass="btn btn-outline-warning me-2" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <% } %>
                </div>
            </asp:Panel>
        </div>

        <%-- Panel del agregar marca--%>
        <div class="container d-flex justify-content-center align-items-center">
           <asp:Panel ID="PanelAgregarMarca" runat="server" CssClass="container bg-light text-dark rounded-4 shadow p-4 mt-3" Style="max-width: 700px;">
    <h2 class="text-center fw-bold mb-4">Registrar nueva marca</h2>

    <div class="mb-3">
        <label for="txtNombreMarca" class="form-label fw-semibold">Nombre de la marca</label>
        <asp:TextBox ID="txtNombreMarca" runat="server" CssClass="form-control" />
    </div>

    <div class="d-grid gap-2">
        <asp:Button ID="btnGuardarMarca" runat="server" Text="Guardar marca" CssClass="btn btn-success" />
        <asp:HyperLink ID="lnkVolver" runat="server" NavigateUrl="~/AgregarProducto.aspx" CssClass="btn btn-outline-secondary">
            Volver
        </asp:HyperLink>
    </div>
</asp:Panel>

        </div>

        <%-- Panel del eliminar producto --%>
       <asp:Panel ID="PanelEliminarProducto" runat="server" CssClass="container bg-light text-dark rounded-4 shadow p-4 mt-4" Style="max-width: 750px;">
    <h3 class="text-center fw-bold mb-4">Eliminar producto</h3>

    <div class="row g-3 mb-3">
        <div class="col-12 col-md-6">
            <label for="ddlFiltroMarca" class="form-label fw-semibold">Filtrar por marca</label>
            <asp:DropDownList ID="ddlFiltroMarca" runat="server" CssClass="form-select" AutoPostBack="true" />
        </div>
        <div class="col-12 col-md-6">
            <label for="ddlFiltroTipo" class="form-label fw-semibold">Filtrar por tipo</label>
            <asp:DropDownList ID="ddlFiltroTipo" runat="server" CssClass="form-select" AutoPostBack="true" />
        </div>
    </div>

    <div class="mb-3">
        <label for="ddlProductos" class="form-label fw-semibold">Seleccione el producto</label>
        <asp:DropDownList ID="ddlProductos" runat="server" CssClass="form-select" AutoPostBack="true" />
    </div>

    <asp:Panel ID="PanelConfirmacion" runat="server" Visible="true" CssClass="bg-warning bg-opacity-10 border border-warning rounded-3 p-3 mt-3">
        <p class="text-warning fw-semibold mb-3">¿Estás seguro que querés eliminar este producto?</p>
        <div class="d-flex justify-content-end gap-3">
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger px-4" />
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
