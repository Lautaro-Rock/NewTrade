<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPanelDeControles.Master" AutoEventWireup="true" CodeBehind="PanelCtrlAdmin.aspx.cs" Inherits="Comercio.PanellCtrlAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="container-fluid">
    <div class="row justify-content-center g-5 mb-5 row-height">

      <div class="col-md-4">
        <a href="GestiónDeProductos.aspx" style="text-decoration: none;">
          <div class="card card-custom">
            <img src="\images\package.png" alt="Caja" class="card-img">
            <h5 class="card-title">Gestión de productos</h5>
          </div>
        </a>
      </div>

      <div class="col-md-4">
        <a href="GestiónCompras.aspx" style="text-decoration: none;">
          <div class="card card-custom">
            <img src="\images\shopping-cart (1).png" alt="Carro" class="card-img" />
            <h5 class="card-title">Gestión de compras</h5>
          </div>
        </a>
      </div>

      <div class="col-md-4">
        <a href="GestiónVentas.aspx" style="text-decoration: none;">
          <div class="card card-custom">
            <img src="\images\acquisition.png" alt="Venta" class="card-img">
            <h5 class="card-title">Gestión de ventas</h5>
          </div>
        </a>
      </div>

    </div>

    <div class="row justify-content-center g-5 row-height">

      <div class="col-md-4">
        <a href="GestionClientes.aspx" style="text-decoration: none;">
          <div class="card card-custom">
            <img src="\images\target.png" alt="Cliente" class="card-img">
            <h5 class="card-title">Gestión de clientes</h5>
          </div>
        </a>
      </div>

      <div class="col-md-4">
        <a href="Proveedores.aspx" style="text-decoration: none;">
          <div class="card card-custom">
            <img src="\images\loading.png" alt="Proveedor" class="card-img">
            <h5 class="card-title">Gestión de proveedores</h5>
          </div>
        </a>
      </div>

      <div class="col-md-4">
        <a href="Reportes.aspx" style="text-decoration: none;">
          <div class="card card-custom">
            <img src="\images\paperwork.png" alt="Reportes" class="card-img">
            <h5 class="card-title">Reportes</h5>
          </div>
        </a>
      </div>

    </div>
  </div>
</asp:Content>

