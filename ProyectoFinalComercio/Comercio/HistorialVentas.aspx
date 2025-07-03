<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HistorialVentas.aspx.cs" Inherits="Comercio.HistorialVentas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Historial de ventas</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet" />
    <link href="Compras.css" rel="stylesheet" />
</head>
<body id="body_hist_ventas">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
        <asp:Panel ID="PnlHistVentas" runat="server" CssClass="container p-0">
            <asp:UpdatePanel ID="UpPnlHistVentas" runat="server">
                <ContentTemplate>
                    <h2 class="text-white mt-4 mb-3">Mi historial de ventas</h2>
                    <div class="container mt-3 mb-4">
                        <div class="row align-items-end g-3">
                            <div class="col-md-3">
                                <asp:Label Text="Filtrar" runat="server" AssociatedControlID="filtroUno" CssClass="form-label text-white" />
                                <asp:TextBox runat="server" ID="filtroUno" CssClass="form-control" AutoPostBack="true" OnTextChanged="filtroUno_TextChanged" />
                            </div>
                            <div class="col-md-3">
                                <asp:CheckBox ID="checkFiltrarAvanzado" runat="server" AutoPostBack="true" OnCheckedChanged="checkFiltrarAvanzado_CheckedChanged" />
                                <asp:Label Text="Filtro Avanzado" runat="server" AssociatedControlID="checkFiltrarAvanzado" CssClass="ms-1 mb-0 text-white" />
                            </div>
                            <div class="col-md-3 d-flex align-items-end">
                                <asp:Button ID="btnVolverPanel" runat="server" Text="Volver al Panel"
                                    CssClass="btn btn-outline-light" OnClick="btnVolverPanel_Click" />
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="PnlFiltroAvanzado" runat="server" Visible="false" CssClass="mb-4">
                        <div class="row align-items-end g-3">
                            <div class="col-md-2">
                                <asp:Label Text="Campo" runat="server" AssociatedControlID="ddlCampo" CssClass="form-label text-white" />
                                <asp:DropDownList runat="server" ID="ddlCampo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                                    <asp:ListItem Text="Factura" />
                                    <asp:ListItem Text="Cliente" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:Label Text="Criterio" runat="server" AssociatedControlID="ddlCriterio" CssClass="form-label text-white" />
                                <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-control" />
                            </div>
                            <div class="col-md-2">
                                <asp:Label Text="Filtro" runat="server" AssociatedControlID="txtFiltroAvanzado" CssClass="form-label text-white" />
                                <asp:TextBox runat="server" ID="txtFiltroAvanzado" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="row align-items-end g-3 mt-3">
                            <div class="col-md-2 d-flex align-items-end mb-3">
                                <asp:Button ID="btnBuscarAvanzado" runat="server" Text="Buscar" CssClass="btn btn-primary w-100 me-2" OnClick="btnBuscarAvanzado_Click" />
                                <asp:Button ID="btnLimpiarAvanzado" runat="server" Text="Limpiar filtro"
                                    CssClass="btn btn-secondary w-100"
                                    OnClick="btnLimpiarAvanzado_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:GridView ID="GvHistVentas" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-hover text-white color-table-personalizado"
                        OnRowCommand="GvHistVentas_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="NumeroFactura" HeaderText="Factura" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C2}" />
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button ID="BtnVer" runat="server" Text="Ver"
                                        CommandName="Ver" CommandArgument='<%# Eval("Id") %>'
                                        CssClass="btn btn-outline-info btn-sm me-2" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </form>
</body>

</html>
