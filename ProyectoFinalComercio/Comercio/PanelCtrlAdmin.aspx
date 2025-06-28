<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPanelDeControles.Master" AutoEventWireup="true" CodeBehind="PanelCtrlAdmin.aspx.cs" Inherits="Comercio.PanellCtrlAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="container-fluid">
    <div class="row justify-content-center g-5 mb-5 row-height">

    <%if (Session["usuario"] != null && ((Dominio.Usuario)Session["usuario"]).Rol == "Administrador")
        {
     %>

      <div class="col-md-4">
        <a href="GestiónDeProductos.aspx" style="text-decoration: none;">
          <div class="card card-custom">
            <img src="\images\package.png" alt="Caja" class="card-img">
            <h5 class="card-title">Gestión de productos</h5>
          </div>
        </a>
      </div>

    <% } %>

      <div class="col-md-4">
        <a href="GestiónCompras.aspx" style="text-decoration: none;">
          <div class="card card-custom">
            <img src="\images\shopping-cart (1).png" alt="Carro" class="card-img" />
            <h5 class="card-title">Gestión de compras</h5>
          </div>
        </a>
      </div>

      <div class="col-md-4">
        <a href="GestionarVentas.aspx" style="text-decoration: none;">
          <div class="card card-custom">
            <img src="\images\acquisition.png" alt="Venta" class="card-img">
            <h5 class="card-title">Gestión de ventas</h5>
          </div>
        </a>
      </div>

    </div>

        <%if (Session["usuario"] != null && ((Dominio.Usuario)Session["usuario"]).Rol == "Administrador")
            { %>
    <div class="row justify-content-center g-5 row-height">

      <div class="col-md-4">
        <a href="GestionClientes.aspx" style="text-decoration: none;">
          <div class="card card-custom">
            <img src="\images\client-white.png" alt="Cliente" class="card-img">
            <h5 class="card-title">Gestión de clientes</h5>
          </div>
        </a>
      </div>

      <div class="col-md-4">
        <a href="GestionProveedores.aspx" style="text-decoration: none;">
          <div class="card card-custom">
            <img src="\images\loading.png" alt="Proveedor" class="card-img">
            <h5 class="card-title">Gestión de proveedores</h5>
          </div>
        </a>
      </div>

          <div class="col-md-4">
            <a href="GestionarUsuarios.aspx" style="text-decoration: none;">
              <div class="card card-custom">
                <img src="\images\target.png" alt="Usuarios" class="card-img">
                <h5 class="card-title">Gestión de usuarios</h5>
              </div>
            </a>
          </div>
        <% } %>
    </div>
  </div>
</asp:Content>

