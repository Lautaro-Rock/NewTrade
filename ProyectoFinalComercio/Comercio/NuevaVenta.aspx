﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NuevaVenta.aspx.cs" Inherits="Comercio.NuevaVenta" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Nueva Venta</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet" />
    <link href="Esperanza.css" rel="stylesheet" />
</head>
<body class="azul-venta">

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="container p-5">
            <h2 class="mb-4 text-white">Registrar nueva venta</h2>

            <!-- CLIENTE -->
            <div class="accordion mb-4" id="accordionCliente">
                <div class="accordion-item border-white">
                    <h2 class="accordion-header">
                        <button class="accordion-button txt-color-personalizado" type="button" data-bs-toggle="collapse" data-bs-target="#collapseCliente">
                            <i class="bi bi-person me-2"></i>Cliente
                        </button>
                    </h2>
                    <div id="collapseCliente" class="accordion-collapse collapse show">
                        <div class="accordion-body">

                            <!-- Cliente seleccionado -->
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="mb-3">
                                        <asp:TextBox ID="txtClienteSeleccionado" runat="server" CssClass="form-control txt-cliente-seleccionado-color w-50" Placeholder="Haga clic en 'Buscar cliente'" Enabled="false" />
                                        <asp:HiddenField ID="hfIdClienteSeleccionado" runat="server" />
                                        <asp:HiddenField ID="hfIdVenta" runat="server" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="row g-4">
                                <!-- Buscar Cliente -->
                                <div class="col-md-12">
                                    <div class="border rounded p-3 bg-dark-subtle">
                                        <h5 class="text-white"><i class="bi bi-search me-2"></i>Buscar cliente</h5>
                                        <asp:UpdatePanel ID="upBuscarCliente" runat="server">
                                            <ContentTemplate>
                                                <div class="mb-2">
                                                    <label class="form-label text-white">Buscar por nombre o DNI</label>
                                                    <asp:TextBox ID="txtBuscarCliente" runat="server" CssClass="form-control txt-color-personalizado" AutoPostBack="true" OnTextChanged="txtBuscarCliente_TextChanged" />
                                                </div>
                                                <asp:GridView ID="gvClientes" runat="server" OnSelectedIndexChanged="gvClientes_SelectedIndexChanged" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover">
                                                    <Columns>
                                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                                                        <asp:BoundField DataField="Dni" HeaderText="Dni" />
                                                        <asp:CommandField ShowSelectButton="True" SelectText="Este" HeaderText="Seleccione el cliente" />
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                     
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <!-- PRODUCTOS -->
            <div class="accordion mb-4" id="accordionProducto">
                <div class="accordion-item border-white">
                    <h2 class="accordion-header">
                        <button class="accordion-button collapsed txt-color-personalizado" type="button" data-bs-toggle="collapse" data-bs-target="#collapseProducto">
                            <i class="bi bi-box-seam me-2"></i>Productos
                        </button>
                    </h2>
                    <div id="collapseProducto" class="accordion-collapse collapse">
                        <div class="accordion-body">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>

                                    <div class="row">
                                        <label class="form-label">Búsqueda rápida</label>
                                        <asp:TextBox ID="txtBuscarProducto" runat="server" CssClass="form-control mb-3" AutoPostBack="true" OnTextChanged="txtBuscarProducto_TextChanged" Placeholder="Ingrese un nombre, stock, marca, o precio para buscar" />
                                    </div>

                                    <div class="row">
                                        <div class="form-check form-switch">
                                            <div class="form-check form-switch">
                                                <asp:CheckBox ID="chkNativeSwitch" runat="server" CssClass="form-check-input" />
                                                <asp:Label ID="lblNativeSwitch" runat="server" AssociatedControlID="chkNativeSwitch" CssClass="form-check-label" Text="Native switch haptics" />
                                            </div>
                                        </div>
                                    </div>

                                    <asp:GridView ID="dgvProductos" runat="server" AutoGenerateColumns="False" OnRowCommand="dgvProductos_RowCommand" CssClass="table table-bordered table-hover">
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
                                            <asp:TemplateField HeaderText="¿Agregar a la venta?">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnAgregarProd_a_venta" runat="server"
                                                        CommandName="Agregar"
                                                        CommandArgument='<%# Eval("Id") %>'
                                                        CssClass="btn btn-outline-primary" Text="Agregar" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <%-- Carrito de compra --%>
                                    <asp:Panel ID="PanelDetalleVenta" runat="server" CssClass="bg-light rounded shadow-sm p-4 mt-4 mb-3">
                                    <h5 class="fw-bold text-dark mb-3">Detalle de productos seleccionados</h5>

                                    <asp:GridView ID="gvDetalleVenta" runat="server"
                                        AutoGenerateColumns="False"
                                        CssClass="table table-striped table-bordered text-dark"
                                        EmptyDataText="Todavía no se agregó ningún producto.">

                                        <Columns>
                                            <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                            <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C2}" />
                                            <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C2}" />
                                        </Columns>
                                    </asp:GridView>

                                    <div class="text-end mt-3">
                                        <asp:Button ID="btnVaciarDetalleVenta" runat="server" Text="Vaciar lista"
                                            CssClass="btn btn-outline-danger"
                                            OnClick="btnVaciarDetalleVenta_Click" />
                                    </div>
                                </asp:Panel>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

            <!-- TOTAL + OBSERVACIONES -->
            <div class="card bg-dark-subtle p-4 text-white">
                <div class="row mb-3">
                    <div class="col-6 fs-3">
                        <asp:Label ID="lbTotal" runat="server" Text="Total" />
                    </div>
                    <div class="col-6 text-end fs-3">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbPrecio" runat="server" Text="$ " />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="mb-3">
                    <label class="form-label">Observaciones</label>
                    <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control txt-color-personalizado" />
                </div>
                <asp:UpdatePanel ID="upConfirmarVenta" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnConfirmarVenta" runat="server"
                        Text="Confirmar venta"
                        CssClass="btn btn-warning w-100 mt-2"
                        OnClick="btnConfirmarVenta_Click" />
                    <asp:Button ID ="btnModificarVenta" runat="server" 
                        Text="Modificar venta"
                        CssClass="btn btn-warning w-100 mt-2"
                        OnClick="btnModificarVenta_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>