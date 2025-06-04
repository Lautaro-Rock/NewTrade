<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PanelDeControlAdmin.aspx.cs" Inherits="Comercio.PanelDeControlAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="Content/StylePanelDeControlAdmn.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="row">
  <div class="card-wrapper">
    <div class="card">
      <img src="/images/package-violeta.png" alt="Caja"  />
      <h5>Gestión de productos</h5>
    </div>
  </div>
  <div class="card-wrapper">
    <div class="card">
      <img src="/images/shopping-cart-violeta.png" alt="Carro"  />
      <h5>Gestión de compras</h5>
    </div>
  </div>
  <div class="card-wrapper">
    <div class="card">
      <img src="/images/profit-violeta.png" alt="Venta"  />
      <h5>Gestión de ventas</h5>
    </div>
  </div>
  <div class="card-wrapper">
  <div class="card">
    <img src="/images/client.png" alt="Cliente"/>
    <h5>Gestión de clientes</h5>
  </div>
</div>
  <div class="card-wrapper">
    <div class="card">
      <img src="/images/delivery-truck.png" alt="Proveedor"/>
      <h5>Gestión de proveedores</h5>
    </div>
  </div>
  <div class="card-wrapper">
    <div class="card">
      <img src="/images/analysis.png" alt="Reporte"/>
      <h5>Reportes</h5>
    </div>
  </div>
 <div class="card-wrapper card-wrapper-wide">
  <div class="card card-horizontal">
    <img src="/images/target-violeta.png" alt="Vendedores" class="icono-vendedor-horizontal" />
    <div>
      <h5>Gestión de vendedores</h5>
    </div>
  </div>
</div>
</div>



</asp:Content>
