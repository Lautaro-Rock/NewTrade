<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestiónDeProductos.aspx.cs" Inherits="Comercio.Prototipo" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="StyleGestionProductos.css" rel="stylesheet" />
    <title>Gestión de Productos</title>
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
                        <button class="accordion-button collapsed bg-transparent text-light ps-0" type="button" data-bs-toggle="collapse" data-bs-target="#collapseProductos">
                            Sección Productos
                        </button>
                    </h2>
                    <div id="collapseProductos" class="accordion-collapse collapse" data-bs-parent="#accordionSidebar">
                        <div class="accordion-body ps-3">
                            <asp:LinkButton ID="btnAgregarProd" runat="server" OnClick="btnAgregarProdClick" CssClass="sidebar-link hover-effect">Agregar producto</asp:LinkButton>
                            <asp:LinkButton ID="btnModificarProd" runat="server" OnClick="btnModificarProd_Click" CssClass="sidebar-link hover-effect">Modificar producto</asp:LinkButton>
                            <asp:LinkButton ID="btnEliminarProd" runat="server" OnClick="btnEliminarProdClick" CssClass="sidebar-link hover-effect">Eliminar producto</asp:LinkButton>
                            <asp:LinkButton ID="btnListarProd" runat="server" OnClick="btnListarProdClick" CssClass="sidebar-link hover-effect">Listar productos</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="accordion-item bg-transparent border-0">
                    <h2 class="accordion-header">
                        <button class="accordion-button collapsed bg-transparent text-light ps-0" type="button" data-bs-toggle="collapse" data-bs-target="#collapseMarcas">
                            Sección Marcas
                        </button>
                    </h2>
                    <div id="collapseMarcas" class="accordion-collapse collapse" data-bs-parent="#accordionSidebar">
                        <div class="accordion-body ps-3">
                            <asp:LinkButton ID="btnPanelAgregarMarca" runat="server" OnClick="btnPanelAgregarMarcaClick" CssClass="sidebar-link hover-effect">Agregar marca</asp:LinkButton>
                            <asp:LinkButton ID="btnPanelModificarMarca" runat="server" OnClick="btnPanelModificarMarca_Click" CssClass="sidebar-link hover-effect">Modificar marca</asp:LinkButton>
                            <asp:LinkButton ID="btnPanelEliminarMarca" runat="server" OnClick="btnPanelEliminarMarcaClick" CssClass="sidebar-link hover-effect">Eliminar marca</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" runat="server" CssClass="sidebar-link hover-effect" Visible ="false">Listar marcas</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="accordion-item bg-transparent border-0">
                    <h2 class="accordion-header">
                        <button class="accordion-button collapsed bg-transparent text-light ps-0" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTipos">
                            Sección Tipos
                        </button>
                    </h2>
                    <div id="collapseTipos" class="accordion-collapse collapse" data-bs-parent="#accordionSidebar">
                        <div class="accordion-body ps-3">
                            <asp:LinkButton ID="btnPanelAgregarTipo" runat="server" OnClick="btnPanelAgregarTipo_Click" CssClass="sidebar-link hover-effect">Agregar tipo de producto</asp:LinkButton>
                            <asp:LinkButton ID="btnPanelModificarTipo" runat="server" OnClick="btnPanelModificarTipo_Click" CssClass="sidebar-link hover-effect">Modificar tipo de producto</asp:LinkButton>
                            <asp:LinkButton ID="btnPanelEliminarTipo" runat="server" OnClick="btnPanelEliminarTipo_Click" CssClass="sidebar-link hover-effect">Eliminar tipo de producto</asp:LinkButton>
                            <asp:LinkButton ID="btnListarTipo" runat="server" CssClass="sidebar-link hover-effect" Visible ="false">Listar todos los tipos de productos</asp:LinkButton>
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
            <asp:Panel ID="PanelFormAltaProd" runat="server" CssClass="container bg-light rounded-4 shadow-lg p-4 mt-5">
                <asp:Label ID="lblTituloAgregar" runat="server" CssClass="h2 text-center mb-4 text-dark fw-bold" Text="Formulario para agregar producto"></asp:Label>
                <asp:Label ID="lblTituloModificar" runat="server" CssClass="h2 text-center mb-4 text-dark fw-bold" Text="Formulario para modificar producto" Visible="false"></asp:Label>

                <div class="mb-3" runat="server" id="divProductoModificar" visible="false">
                    <asp:Label AssociatedControlID="ddlProductoModificar" runat="server" CssClass="form-label fw-semibold text-dark">Producto</asp:Label>
                    <asp:DropDownList ID="ddlProductoModificar" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProductoModificar_SelectedIndexChanged" />
                </div>

                <div class="row g-4">
                    <!-- Columna izquierda -->
                    <div class="col-12 col-md-6">
                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtNombreProd" runat="server" CssClass="form-label fw-semibold text-dark">Nombre del producto</asp:Label>
                            <asp:TextBox ID="txtNombreProd" runat="server" CssClass="form-control" placeholder="Ej: Taladro Black+Decker" />
                            <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio" ControlToValidate="txtNombreProd" runat="server" ForeColor="Red" ValidationGroup="AltaProducto" />
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
    <div class="input-group align-items-center">
        <span class="input-group-text bg-secondary text-white">$</span>
        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" placeholder="0.00" TextMode="Number" />

        <!-- Contenedor para validadores alineados junto al input -->
        <span style="display: inline-flex; flex-direction: column; margin-left: 10px;">
            <asp:RequiredFieldValidator 
                ErrorMessage="El precio es obligatorio" 
                ControlToValidate="txtPrecio" 
                runat="server" 
                ForeColor="Red" 
                ValidationGroup="AltaProducto" 
                Display="Dynamic" />
            <asp:CompareValidator  
                ID="cvPrecioMayorCero"    
                runat="server"  
                ControlToValidate="txtPrecio"  
                Operator="GreaterThan" 
                ValueToCompare="0"  
                Type="Double" 
                ErrorMessage="El precio debe ser mayor a cero" 
                ForeColor="Red" 
                ValidationGroup="AltaProducto" 
                Display="Dynamic" />
        </span>
    </div>
</div>
         </div>
                    <!-- Columna derecha -->
                    <div class="col-12 col-md-6">
                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtStock" runat="server" CssClass="form-label fw-semibold text-dark">Stock</asp:Label>
                            <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" placeholder="Ej: 100 unidades" TextMode="Number" />
                            <asp:RequiredFieldValidator ErrorMessage="El stock es obligatorio" ControlToValidate="txtStock" runat="server" ForeColor="Red" ValidationGroup="AltaProducto" />
                        </div>

                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtStockMin" runat="server" CssClass="form-label fw-semibold text-dark">Stock mínimo</asp:Label>
                            <asp:TextBox ID="txtStockMin" runat="server" CssClass="form-control" placeholder="Ej: 10" TextMode="Number" />
                            <asp:RequiredFieldValidator ErrorMessage="El stock minimo es obligatorio" ControlToValidate="txtStockMin" runat="server" ForeColor="Red" ValidationGroup="AltaProducto" />
                        </div>

                        <div class="form-group mb-3">
                            <asp:Label AssociatedControlID="txtUrlImagen" runat="server" CssClass="form-label fw-semibold text-dark">URL de la imagen</asp:Label>
                            <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control" placeholder="https://ejemplo.com/imagen.jpg" />
                            <asp:RequiredFieldValidator ErrorMessage="Complete con cualquier URL" ControlToValidate="txtUrlImagen" runat="server" ForeColor="Red" ValidationGroup="AltaProducto" />
                        </div>

                        <div class="d-grid mt-4">
                            <asp:Button ID="btnGuardarProducto" runat="server" Text="Agregar producto" CssClass="btn btn-success btn-lg fw-bold" OnClick="btnGuardarProducto_Click" ValidationGroup="AltaProducto" />
                            <asp:Button ID="btnModificarProducto" runat="server" Text="Modificar producto" CssClass="btn btn-warning btn-lg fw-bold" OnClick="btnModificarProducto_Click" Visible="false" ValidationGroup="AltaProducto" />
                        </div>
                    </div>
                </div>
            </asp:Panel>

        </div>

        <%-- Panel del listado de productos --%>
<div class="container">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updPanelFiltro" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelListarProd" runat="server">
             

<h1 class="text-center text-white mb-4" style="font-family: 'Special Elite', monospace; font-size: 2.5rem;">
  Lista de Productos del sistema de nuestra app!
</h1>

<div class="container mt-3 mb-4">
    <!-- FILTRO RÁPIDO -->
    <div class="row justify-content-center align-items-center g-3">
        <div class="col-md-3 d-flex justify-content-center align-items-center">
            <asp:Label Text="Filtrar" runat="server" AssociatedControlID="txtFiltroRapido" CssClass="me-2 mb-0 fw-bold" />
            <asp:TextBox
               ID="txtFiltroRapido"
                runat="server"
                AutoPostBack="true"
                OnTextChanged="txtFiltroRapido_TextChanged"
                CssClass="form-control"
                placeholder="Escriba para buscar..."
                style="max-width: 250px;" />
        </div>
        <div class="col-md-2 d-flex align-items-center">
            <asp:CheckBox ID="chkFiltroActivo" runat="server" CssClass="form-check-input me-2" AutoPostBack="true" OnCheckedChanged="chkFiltroActivo_CheckedChanged" />
            <asp:Label Text="Filtro Avanzado" runat="server" AssociatedControlID="chkFiltroActivo" CssClass="form-check-label mb-0" />
        </div>
    </div>

    <!-- FILTRO AVANZADO -->
    <% if (chkFiltroActivo.Checked) { %>
        <div class="row justify-content-center g-3 mt-3">
            <div class="col-md-2">
                <asp:Label ID="lbCampo" runat="server" Text="Campo" CssClass="form-label fw-bold" />
                <asp:DropDownList ID="ddlCampo" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                    <asp:ListItem Text="Por nombre" Value="Nombre"></asp:ListItem>
                    <asp:ListItem Text="Por marca" Value="Marca"></asp:ListItem>
                    <asp:ListItem Text="Por tipo" Value="Tipo"></asp:ListItem>
                    <asp:ListItem Text="Por precio" Value="Precio"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lbCriterio" runat="server" Text="Criterio" CssClass="form-label fw-bold" />
                <asp:DropDownList ID="ddlCriterio" runat="server" CssClass="form-select" />
            </div>
            <div class="col-md-2">
                <asp:Label ID="lbFiltro" runat="server" Text="Filtro" CssClass="form-label fw-bold" />
                <asp:TextBox ID="txtFiltroAvanzado" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-2">
                <asp:Label ID="lbActivo" runat="server" Text="Estado" CssClass="form-label fw-bold" />
                <asp:DropDownList ID="ddlActivo" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Todos" Value="Todos"></asp:ListItem>
                    <asp:ListItem Text="Solo los activos" Value="Activos"></asp:ListItem>
                    <asp:ListItem Text="Solo los inactivos" Value="Inactivos"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="row my-4">
            <div class="col text-center">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary w-25" OnClick="btnBuscar_Click" />
            </div>
        </div>
    <% } %>
</div>


</div>
<div class="row mb-4">
    <div class="col-12 text-end">
        <asp:Button ID="btnAgregarProducto" runat="server" Text="Nuevo producto" CssClass="btn btn-success" Visible="false" />
    </div>
</div>
<div class="row g-4 justify-content-center">
    <asp:Repeater ID="repProductos" runat="server">
        <ItemTemplate>
            <div class="col-sm-12 col-md-6 col-lg-4 d-flex">
                <div class="card card-uniforme mb-3 w-100 border-warning shadow-sm">
                    <div class="row g-0 h-100">
                        <div class="col-md-4 fondo-imagen">
                            <img src='<%# Eval("UrlImgProducto") %>' class="img-fluid rounded-start h-100 object-fit-contain" alt="Imagen del producto" />
                        </div>
                        <div class="col-md-8">
                            <div class="card-body card-body-uniforme d-flex flex-column justify-content-between">
                                <div class="contenido-texto">
                                    <h5 class="card-title"><strong><%# Eval("Nombre") %></strong></h5>
                                    <p class="card-text mb-1"><strong>Marca:</strong> <%# Eval("Marca") %></p>
                                    <p class="card-text mb-1"><strong>Tipo:</strong> <%# Eval("TipoProducto") %></p>
                                    <p class="card-text mb-1"><strong>Precio:</strong> $<%# Eval("Precio", "{0:N2}") %></p>
                                </div>
                                <div>
                                    <asp:Button ID="btnModificarPListado" runat="server" Text="Modificar" CssClass="btn btn-outline-warning me-2 w-100" CommandArgument='<%# Eval("Id") %>' OnClick="btnModificarPListado_Click" />
                                    <asp:Button ID="btnEliminarProductoListado" runat="server" Text="Eliminar" CssClass="btn btn-outline-danger mt-2 me-2 w-100" CommandArgument='<%# Eval("Id") %>' OnClick="btnEliminarProductoListado_Click" OnClientClick="return confirm('¿Estás seguro que deseas eliminar este producto?');" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
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
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger px-4" OnClick="btnEliminar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-4" />
                </div>
            </asp:Panel>
        </asp:Panel>

        <%-- MARCAS --%>

        <%-- Panel del agregar y modificar marca--%>
        <div class="container d-flex justify-content-center align-items-center">
            <asp:Panel ID="PanelAgregarMarca" runat="server" CssClass="container bg-light text-dark rounded-4 shadow p-4 mt-3" Style="max-width: 700px;">
                <asp:Label ID="lblTituloAgregarMarca" runat="server" CssClass="h2 text-center mb-4 text-dark fw-bold" Text="Registrar nueva marca"></asp:Label>
                <asp:Label ID="lblTituloModificarMarca" runat="server" CssClass="h2 text-center mb-4 text-dark fw-bold" Text="Modificar marca" Visible="false"></asp:Label>
                <div class="mb-3" id="divMarcaModificar" runat="server" visible="false">
                    <asp:Label AssociatedControlID="ddlMarcaModificar" runat="server" CssClass="form-label fw-semibold text-dark">Marca</asp:Label>
                    <asp:DropDownList ID="ddlMarcaModificar" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMarcaModificar_SelectedIndexChanged" />
                </div>
                <div class="mb-3">
                    <label for="txtNombreMarca" class="form-label fw-semibold">Nombre de la marca</label>
                    <asp:TextBox ID="txtNombreMarca" runat="server" CssClass="form-control" />
                </div>
                <div class="d-grid gap-2">
                    <asp:Button ID="btnAgregarMarca" OnClick="btnAgregarMarcaClick" runat="server" Text="Guardar marca" CssClass="btn btn-success" />
                    <asp:Button ID="btnModificarMarca" runat="server" Text="Guardar cambios" CssClass="btn btn-warning" Visible="false" OnClick="btnModificarMarca_Click" />
                    <asp:HyperLink ID="lnkVolver" runat="server" NavigateUrl="~/AgregarProducto.aspx" CssClass="btn btn-outline-secondary">Volver
                    </asp:HyperLink>
                </div>
            </asp:Panel>
        </div>

        <%-- Panel del eliminar marca--%>
        <div class="container d-flex justify-content-center align-items-center">
            <asp:Panel ID="PanelEliminarMarca" runat="server" CssClass="container bg-light text-dark rounded-4 shadow p-4 mt-4" Style="max-width: 700px;">
                <h2 class="text-center fw-bold mb-4">Eliminar marca</h2>
                <div class="mb-3">
                    <label for="ddlMarcasEliminar" class="form-label fw-semibold">Seleccione una marca</label>
                    <asp:DropDownList ID="ddlMarcasEliminar" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMarcasEliminar_SelectedIndexChanged" />
                </div>
                <asp:Panel ID="PanelConfirmarEliminarMarca" runat="server" Visible="false" CssClass="bg-warning bg-opacity-10 border border-warning rounded-3 p-3 mt-3">
                    <p class="text-warning fw-semibold mb-3">¿Estás seguro que querés eliminar esta marca?</p>
                    <div class="d-flex justify-content-end gap-3">
                        <asp:Button ID="btnEliminarMarca2" runat="server" Text="Eliminar" CssClass="btn btn-danger px-4" OnClick="btnEliminarMarca2_Click" />
                        <asp:Button ID="btnCancelarEliminarMarca" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-4" />
                    </div>
                </asp:Panel>
            </asp:Panel>
        </div>

        <%-- CATEGORIA (Tipo de producto) --%>

        <%-- Panel del agregar y modificar Categoria--%>
        <div class="container d-flex justify-content-center align-items-center">
            <asp:Panel ID="PanelAgregarCategoria" runat="server" CssClass="container bg-light text-dark rounded-4 shadow p-4 mt-3" Style="max-width: 700px;">
                <asp:Label ID="lblAgregarCategoria" runat="server" CssClass="h2 text-center mb-4 text-dark fw-bold" Text="Registrar nuevo Tipo de Producto"></asp:Label>
                <asp:Label ID="lblModificarCategoria" runat="server" CssClass="h2 text-center mb-4 text-dark fw-bold" Text="Modificar Tipo de Producto" Visible="false"></asp:Label>
                <div class="mb-3" id="divCategoriaModificar" runat="server" visible="false">
                    <asp:Label AssociatedControlID="ddlCategoriaModificar" runat="server" CssClass="form-label fw-semibold text-dark">Tipo de Producto</asp:Label>
                    <asp:DropDownList ID="ddlCategoriaModificar" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoriaModificar_SelectedIndexChanged" />
                </div>
                <div class="mb-3">
                    <label for="txtNombreCategoria" class="form-label fw-semibold">Nombre del Tipo de Producto</label>
                    <asp:TextBox ID="txtNombreCategoria" runat="server" CssClass="form-control" />
                </div>
                <div class="d-grid gap-2">
                    <asp:Button ID="btnAgregarCategoria" OnClick="btnAgregarCategoria_Click" runat="server" Text="Guardar Tipo de Producto" CssClass="btn btn-success" />
                    <asp:Button ID="btnModificarCategoria" runat="server" Text="Guardar cambios" CssClass="btn btn-warning" Visible="false" OnClick="btnModificarCategoria_Click" />
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/AgregarProducto.aspx" CssClass="btn btn-outline-secondary">Volver
                    </asp:HyperLink>
                </div>
            </asp:Panel>
        </div>

        <%-- Panel del eliminar Categoria--%>
        <div class="container d-flex justify-content-center align-items-center">
            <asp:Panel ID="PanelEliminarCategoria" runat="server" CssClass="container bg-light text-dark rounded-4 shadow p-4 mt-4" Style="max-width: 700px;">
                <h2 class="text-center fw-bold mb-4">Eliminar Tipo de Producto</h2>
                <div class="mb-3">
                    <label for="ddlCategoriasEliminar" class="form-label fw-semibold">Seleccione un Tipo de Producto</label>
                    <asp:DropDownList ID="ddlCategoriasEliminar" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoriasEliminar_SelectedIndexChanged" />
                </div>
                <asp:Panel ID="PanelConfirmarEliminarCategoria" runat="server" Visible="false" CssClass="bg-warning bg-opacity-10 border border-warning rounded-3 p-3 mt-3">
                    <p class="text-warning fw-semibold mb-3">¿Estás seguro que querés eliminar este Tipo de Producto?</p>
                    <div class="d-flex justify-content-end gap-3">
                        <asp:Button ID="btnEliminarCategoria" runat="server" Text="Eliminar" CssClass="btn btn-danger px-4" OnClick="btnEliminarCategoria_Click" />
                        <asp:Button ID="Button6" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-4" />
                    </div>
                </asp:Panel>
            </asp:Panel>
        </div>




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
