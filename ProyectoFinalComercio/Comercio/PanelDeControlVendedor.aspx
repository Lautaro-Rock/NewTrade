<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PanelDeControlVendedor.aspx.cs" Inherits="Comercio.PanelDeControl1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="Content/StylePanelDeControlVnd.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="row"> 
  <div class="card-wrapper">
  <a href="GestionProductos.aspx" class="card-link">
    <div class="card">
      <img src="/images/package.png" alt="Caja" />
      <h5>Gestión de productos</h5>
    </div>
  </a>
</div>
        
  <div class="card-wrapper">
    <a href="GestionCompras.aspx" class="card-link">
      <div class="card">
      <img src="/images/shopping-cart (1).png" alt="Carro"  />
      <h5>Gestión de compras</h5>
    </div>
        </a>
  </div>
  <div class="card-wrapper">
    <a href="GestionVentas.aspx" class="card-link">
      <div class="card">
      <img src="/images/acquisition.png" alt="Venta"  />
      <h5>Gestión de ventas</h5>
    </div>
        </a>
  </div>
  <div class="card-wrapper">
  <a href="GestionClientes.aspx" class="card-link">
      <div class="card">
    <img src="/images/target.png" alt="Cliente"/>
    <h5>Gestión de clientes</h5>
  </div>
      </a>
</div>
  <div class="card-wrapper">
    <a href="GestionProveedores.aspx" class="card-link">
      <div class="card">
      <img src="/images/loading.png" alt="Proveedor"/>
      <h5>Gestión de proveedores</h5>
    </div>
        </a>
  </div>
  <div class="card-wrapper">
    <a href="Reportes.aspx" class="card-link">
      <div class="card">
      <img src="/images/paperwork.png" alt="Reporte"/>
      <h5>Reportes</h5>
    </div>
        </a>
  </div>
</div>
    </asp:Content>