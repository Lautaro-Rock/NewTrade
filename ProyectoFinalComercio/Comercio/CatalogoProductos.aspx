<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatalogoProductos.aspx.cs" Inherits="Comercio.CatalogoProductos" %>

<!DOCTYPE html>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Catálogo de Productos para Venta</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="StyleGestionProductos.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
    <asp:UpdatePanel ID="updCatalogo" runat="server">
        <ContentTemplate>

        <asp:Panel CssClass="container pt-5" runat="server">
            
            <!-- Título -->
            <h1 class="text-center text-white mb-4" style="font-family: 'Special Elite', monospace; font-size: 2.5rem;">
                Catálogo de Productos para Venta
            </h1>

            <!-- Botón Volver -->
            <div class="row mb-3">
                <div class="col text-end">
                    <asp:Button ID="btnVolver" runat="server" Text="Volver a la Venta" CssClass="btn btn-secondary" OnClick="btnVolver_Click" UseSubmitBehavior="false"
                    CausesValidation="false" />
                </div>
            </div>


               <div class="container mb-4">
                    <!-- Filtro rápido -->
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <asp:Label Text="Filtro rápido:" runat="server" CssClass="fw-semibold me-2" AssociatedControlID="txtFiltroRapido" />
                            <asp:TextBox ID="txtFiltroRapido" runat="server" CssClass="form-control" AutoPostBack="true"
                                         OnTextChanged="txtFiltroRapido_TextChanged" placeholder="Buscar por nombre..." />
                        </div>
                        <div class="col-md-6 d-flex align-items-end justify-content-end">
                            <asp:CheckBox ID="chkFiltroAvanzado" runat="server" CssClass="form-check-input me-2"
                                          AutoPostBack="true" OnCheckedChanged="chkFiltroAvanzado_CheckedChanged" />
                            <asp:Label Text="Filtro avanzado" runat="server" AssociatedControlID="chkFiltroAvanzado" CssClass="form-check-label fw-normal" />
                        </div>
                    </div>

                    <% if (chkFiltroAvanzado.Checked) { %>
                        <div class="row g-3 align-items-end mb-3">
                            <div class="col-md-3">
                                <asp:Label ID="lblCampo" runat="server" Text="Campo" CssClass="form-label fw-semibold" />
                                <asp:DropDownList ID="ddlCampo" runat="server" CssClass="form-select" AutoPostBack="true"
                                                  OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                                    <asp:ListItem Text="Por nombre" Value="Nombre" />
                                    <asp:ListItem Text="Por marca" Value="Marca" />
                                    <asp:ListItem Text="Por tipo" Value="Tipo" />
                                    <asp:ListItem Text="Por precio" Value="Precio" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblCriterio" runat="server" Text="Criterio" CssClass="form-label fw-semibold" />
                                <asp:DropDownList ID="ddlCriterio" runat="server" CssClass="form-select" />
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblFiltro" runat="server" Text="Valor" CssClass="form-label fw-semibold" />
                                <asp:TextBox ID="txtFiltroAvanzado" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-3 d-grid">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary"
                                            OnClick="btnBuscar_Click" />
                            </div>
                        </div>
                    <% } %>
            </div>

            <!-- Cards de productos -->
            <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 g-4">
                <asp:Repeater ID="repCatalogo" runat="server" OnItemCommand="repCatalogo_ItemCommand" >
                <ItemTemplate>
                    <div class="col">

                                <div class="card shadow h-100">
                                    <img src='<%# Eval("UrlImgProducto") %>' class="card-img-top p-2" style="max-height: 200px; object-fit: contain;" />
                                    <div class="card-body d-flex flex-column">
                                        <h5 class="card-title text-primary fw-bold"><%# Eval("Nombre") %></h5>
                                        <p class="card-text"><strong>Marca:</strong> <%# Eval("Marca.Nombre") %></p>
                                        <p class="card-text"><strong>Tipo:</strong> <%# Eval("TipoProducto.Nombre") %></p>
                                        <p class="card-text"><strong>Precio:</strong> $<%# Eval("Precio", "{0:N2}") %></p>
                                        <p class="card-text"><strong>Stock:</strong> <%# Eval("Stock") %> | Mín: <%# Eval("StockMin") %></p>

                                        <div class="mt-auto">
                                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control form-control-sm mb-2" Text="1" TextMode="Number" />
                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar"
                                                CommandName="Agregar"
                                                CommandArgument='<%# Eval("Id") %>'
                                                CssClass="btn btn-success btn-sm w-100" />
                                        </div>
                                    </div>
                                </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            </div>

            <!-- Carrito de productos -->

            <h3 class="mt-5 text-white">Productos agregados a la venta</h3>

            <asp:GridView ID="gvDetalleCatalogo" runat="server"
                AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped bg-white mt-3"
                OnRowCommand="gvDetalleCatalogo_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="Producto" DataField="Producto.Nombre" />
                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                    <asp:BoundField HeaderText="Precio U." DataField="PrecioUnitario" DataFormatString="{0:C2}" />
                    <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" DataFormatString="{0:C2}" />

                    <asp:TemplateField HeaderText="Acción">
                        <ItemTemplate>
                            <asp:Button ID="btnQuitar" runat="server" Text="Quitar"
                                CommandName="Quitar"
                                CommandArgument='<%# Eval("Producto.Id") %>'
                                CssClass="btn btn-danger btn-sm" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Label ID="lblTotalCatalogo" runat="server" CssClass="text-white fw-bold fs-5" />


        </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>


