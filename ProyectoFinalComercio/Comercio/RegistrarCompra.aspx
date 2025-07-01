<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrarCompra.aspx.cs" Inherits="Comercio.RegistrarCompra" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet" />
    <link href="Compras.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="container p-5">
            <h2 class="mb-4 text-white">Registrar nueva compra</h2>
            <div class="mb-4 border border-white p-3 rounded">
                <h4 class="text-white mb-3"><i class="bi bi-person me-2"></i>Mis proveedores</h4>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="mb-3">
                            <asp:TextBox ID="txtProveedorSeleccionado" runat="server" CssClass="form-control w-50" Placeholder="Haga clic en 'Buscar proveedor' o 'Nuevo proveedor'" Enabled="false" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="grupoFiltrosProveedor">
                    <div class="row">
                        <div class="col-md-12">
                            <button class="btn btn-outline-warning mb-3" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFiltroRapidoProveedor" aria-expanded="false" aria-controls="collapseFiltroRapidoProveedor">
                                <i class="bi bi-fast-forward me-1"></i>Filtro rápido
                            </button>
                            <button class="btn btn-outline-info mb-3 ms-2" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFiltroAvanzadoProveedor" aria-expanded="false" aria-controls="collapseFiltroAvanzadoProveedor">
                                <i class="bi bi-funnel me-1"></i>Filtro avanzado
                            </button>
                        </div>
                    </div>
                    <!-- Esté va ser el contenido que tendrá el filtro rapido -->
                    <div class="mb-3">
                        <div class="collapse" id="collapseFiltroRapidoProveedor" data-bs-parent="#grupoFiltrosProveedor">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="mb-5">
                                        <asp:Label ID="lbBusqRapProv" runat="server" CssClass="form-label text-white" Text="Buscar por nombre o CUIT"></asp:Label>
                                        <asp:TextBox ID="txtResultBusqRap" runat="server" CssClass="form-control mt-2" AutoPostBack="true" OnTextChanged="txtResultBusqRap_TextChanged" />
                                    </div>
                                    <asp:GridView ID="gvProveedores" runat="server" AutoGenerateColumns="False" CssClass="table color-table-personalizado"
                                        OnRowCommand="gvProveedores_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="RazonSocial" HeaderText="Razón Social" />
                                            <asp:BoundField DataField="Cuit" HeaderText="CUIT" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" />
                                            <asp:BoundField DataField="Telefono" HeaderText="Télefono" />
                                            <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                                            <asp:TemplateField HeaderText="Seleccione una proveedor">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnSeleccionar_prov" runat="server"
                                                        CommandName="Este"
                                                        CommandArgument='<%# Eval("Id") %>'
                                                        CssClass="btn btn-outline-primary" Text="Este" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <!-- Esté va ser el contenido que tendrá el filtro avanzado -->
                    <div class="mb-3">
                        <div class="collapse" id="collapseFiltroAvanzadoProveedor" data-bs-parent="#grupoFiltrosProveedor">
                            <asp:UpdatePanel ID="upFiltroAvanzadoProveedor" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="card card-body color-personalizado-cart">
                                        <div class="row g-3">
                                            <div class="col-md-4">
                                                <asp:Label ID="lbCampo" runat="server" CssClass="form-label" Text="Campo"></asp:Label>
                                                <asp:DropDownList ID="ddlCampoProv" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCampoProv_SelectedIndexChanged">
                                                    <asp:ListItem Text="-- Seleccione un campo --" Value="?" />
                                                    <asp:ListItem Text="Por Razón social" Value="RazonSocial"></asp:ListItem>
                                                    <asp:ListItem Text="Por CUIT" Value="Cuit"></asp:ListItem>
                                                    <asp:ListItem Text="Por Email" Value="Email"></asp:ListItem>
                                                    <asp:ListItem Text="Por Teléfono" Value="Telefono"></asp:ListItem>
                                                    <asp:ListItem Text="Por Dirección" Value="Direccion"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="lbCriterioProv" runat="server" CssClass="form-label" Text="Criterio"></asp:Label>
                                                <asp:DropDownList ID="ddlCriterioProv" runat="server" CssClass="form-select">                                                    
                                                </asp:DropDownList>                                                                                
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="lbFiltro" runat="server" CssClass="form-label" Text="Filtro"></asp:Label>
                                                <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="row g-3 mt-2">
                                            <div class="col-md-4">
                                                <asp:Label ID="lbEstado" runat="server" CssClass="form-label" Text="Estado"></asp:Label>
                                                <asp:DropDownList ID="DdlEstado" runat="server" CssClass="form-select">
                                                    <asp:ListItem Text="-- Seleccione un estado --" Value="?" />
                                                    <asp:ListItem Text="Solamente Activos" Value="Activos"></asp:ListItem>
                                                    <asp:ListItem Text="Solamente Inactivos" Value="Inactivos"></asp:ListItem>
                                                    <asp:ListItem Text="Todos" Value="Todos"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Button ID="btnBuscarProveedor_avanzado" runat="server" CssClass="btn btn-primary mt-3" Text="Buscar proveedor" OnClick="btnBuscarProveedor_avanzado_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvProvFiltroAvanzado" runat="server" AutoGenerateColumns="False" CssClass="table color-table-personalizado mt-4"
                                        OnRowCommand="gvProvFiltroAvanzado_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="RazonSocial" HeaderText="Razón Social" />
                                            <asp:BoundField DataField="Cuit" HeaderText="CUIT" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" />
                                            <asp:BoundField DataField="Telefono" HeaderText="Télefono" />
                                            <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                                            <asp:TemplateField HeaderText="Estado">
                                                <ItemTemplate>
                                                    <i class='<%# Convert.ToBoolean(Eval("Activo")) ? "bi bi-check-circle-fill text-success" : "bi bi-x-circle-fill text-danger" %>'></i>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Seleccione una proveedor">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnSeleccionar_prov_avanzado" runat="server"
                                                        CommandName="Este"
                                                        CommandArgument='<%# Eval("Id") %>'
                                                        CssClass="btn btn-outline-primary" Text="Este" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hfIdProveedorSeleccionado" runat="server" />

            <!-- Esto es lo que tendrá productos -->
            <div class="mb-4 border border-white p-3 rounded">
                <h4 class="text-white mb-3"><i class="bi bi-box-seam me-2"></i>Productos relacionados al proveedor seleccionado</h4>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="dgvProductos" runat="server" AutoGenerateColumns="False" OnRowCommand="dgvProductos_RowCommand" DataKeyNames="Id" 
                            CssClass="table color-table-personalizado mt-5">
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio" />
                                <asp:BoundField DataField="Stock" HeaderText="Stock" />
                                <asp:BoundField DataField="StockMin" HeaderText="Stock Mínimo" />
                                <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                <asp:BoundField DataField="TipoProducto" HeaderText="Categoría" />
                                <asp:TemplateField HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control w-75" TextMode="Number" Text="0" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agregar">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAgregar" runat="server"
                                        CssClass="btn btn-outline-primary"
                                        Text="Agregar"
                                        CommandName="Agregar"
                                        CommandArgument='<%# Eval("Id") %>' />

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>


            <asp:UpdatePanel ID="upDetalleCompra" runat="server">
            <ContentTemplate>
                <asp:Panel ID="PanelDetalleCompra" runat="server" CssClass="bg-light rounded shadow-sm p-4 mt-4 mb-3">
                    <h5 class="fw-bold text-dark mb-3">Detalle de productos seleccionados</h5>

                    <asp:GridView ID="gvDetalleCompra" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered text-dark"
                        EmptyDataText="Todavía no se agregó ningún producto.">
                        <Columns>
                            <asp:TemplateField HeaderText="Producto">
                                <ItemTemplate>
                                    <%# Eval("Producto.Nombre") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                            <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C2}" />
                            <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C2}" />
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button ID="btnQuitar" runat="server"
                                        Text="Quitar"
                                        OnClick="btnQuitar_Click"
                                        CommandArgument='<%# Eval("Producto.Id") %>'
                                        CssClass="btn btn-danger btn-sm" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <div class="text-end mt-3">
                        <asp:Button ID="btnVaciarDetalleCompra" runat="server"
                            Text="Vaciar lista"
                            CssClass="btn btn-outline-danger"
                            OnClick="btnVaciarDetalleCompra_Click" />
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


            <!-- Para ver el total y confirmar compra -->
            <asp:UpdatePanel ID="upTotalCompra" runat="server">
                <ContentTemplate>
                    <div class="card p-4 color-personalizado-cart">
                        <div class="row mb-3">
                            <div class="col-6 fs-3">
                                <asp:Label ID="lbTotal" runat="server" Text="Total" />
                            </div>
                            <div class="col-6 text-end fs-3">
                                <asp:Label ID="lbPrecio" runat="server" Text="$ " />
                            </div>
                        </div>

                         </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:HiddenField ID="hfIdCompra" runat="server" />

                <asp:UpdatePanel ID="upBtnConfirmarCompra" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar compra"
                            OnClick="btnConfirmarCompra_Click" CssClass="btn btn-warning w-100 mt-2" />
                        <asp:Button ID="btnModificarCompra" runat="server" Text="Modificar compra"
                        CssClass="btn btn-warning w-100 mt-2"
                        OnClick="btnModificarCompra_Click"
                        Visible="false" />

                         <asp:Button ID="btnVolver" runat="server"
                         Text="Salir"
                         CssClass="btn btn-secondary mt-3 me-2"
                         OnClick="btnVolver_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>


            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</body>
</html>
