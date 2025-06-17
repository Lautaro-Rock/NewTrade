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
            <asp:Panel ID="PanelFormAltaProd" runat="server">
                <h1 style="text-align: center">Formulario para agregar producto</h1>
                <div class="row pt-3">
                    <div class="col-5">
                        <div class="mb-3">
                            <label for="txtNombreProd" class="form-label">Nombre del producto</label>
                            <asp:TextBox runat="server" ID="txtNombreProd" CssClass="form-control" />
                        </div>
                        <%--
                            <div class="mb-3">
    <label for="txtMarcaProd" class="form-label">Marca</label>
    <asp:TextBox runat="server" ID="txtMarcaProd" CssClass="form-control" />
</div>

                        --%>
                        <div class="mb-3">
                            <label for="ddlMarcas" class="form-label">Marca</label>
                            <asp:DropDownList ID="ddlMarcas" runat="server" CssClass="form-select form-select-lg mb-3"></asp:DropDownList>
                        </div>

                        <div class="mb-3">
                            <label for="ddlTipoDeProducto" class="form-label">Tipo de producto</label>
                            <asp:DropDownList ID="ddlTipoDeProducto" runat="server" CssClass="form-select form-select-lg mb-3"></asp:DropDownList>
                        </div>
                        <div class="mb-3">
                            <label for="txtPrecio" class="form-label">Precio</label>
                            <div class="input-group">
                                <span class="input-group-text">$</span>
                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" placeholder="0.00" TextMode="Number" />
                            </div>
                        </div>
                        <div class="mb-3">
                            <label for="txtStock" class="form-label">Stock</label>
                            <asp:TextBox ID="txtStock" runat="server" CssClass="form-control"
                                placeholder="Ingrese el stock" TextMode="Number" ClientIDMode="Static" />
                        </div>
                    </div>

                    <div class="col-1"></div>

                    <div class="col-5">
                        <div class="mb-3">
                            <label for="txtStockMinimo" class="form-label">
                                Stock Minimo
                            </label>
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"
                                placeholder="Ingrese el stock minimo" TextMode="Number" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label for="txtImagen" class="form-label">Imagen</label>
                            <asp:TextBox runat="server" ID="TextBox2" CssClass="form-control" placeholder="Aca deberás ingresar el url de la imagen" TextMode="Number" ClientIDMode="Static" />
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
            <asp:Panel ID="PanelAgregarMarca" runat="server" CssClass="panel-marca text-white p-5 rounded shadow" Style="max-width: 500px; width: 100%;">

                <h2 class="text-center mb-4">Registrar nueva marca</h2>

                <div class="mb-3">
                    <label for="txtNombreMarca" class="form-label">Nombre de la marca</label>
                    <asp:TextBox ID="txtNombreMarca" runat="server" CssClass="form-control" />
                </div>

                <div class="d-grid gap-2">
                    <asp:Button ID="Button4" runat="server" Text="Guardar marca" CssClass="btn btn-success" />
                    <asp:HyperLink ID="lnkVolver" runat="server" NavigateUrl="~/AgregarProducto.aspx" CssClass="btn btn-outline-light">
                Volver
                    </asp:HyperLink>
                </div>
            </asp:Panel>
        </div>

        <%-- Panel del eliminar producto --%>
        <asp:Panel ID="PanelEliminarProducto" runat="server" CssClass=" panel-marca rounded-4 shadow-lg text-white container mt-5">
            <h3 class="text-center mb-4">Eliminar producto</h3>
            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="ddlFiltroMarca" class="form-label">Filtrar por marca</label>
                    <asp:DropDownList ID="ddlFiltroMarca" runat="server" CssClass="form-select" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-md-6">
                    <label for="ddlFiltroTipo" class="form-label">Filtrar por tipo</label>
                    <asp:DropDownList ID="ddlFiltroTipo" runat="server" CssClass="form-select" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="mb-3">
                <label for="ddlProductos" class="form-label">Seleccione el producto</label>
                <asp:DropDownList ID="ddlProductos" runat="server" CssClass="form-select" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <asp:Panel ID="PanelConfirmacion" runat="server" Visible="true">
                <p class="mt-3 text-warning">¿Estás seguro que querés eliminar este producto?</p>
                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" />
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
