<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GestionProveedores.aspx.cs" Inherits="Comercio.GestionProveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="StyleCss/GestionProveedores.Css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-one-proveedores">
         <h2 class="Style-Proveedores">sección de proveedores</h2>
        <p class="text-info-proveedores">
            Esta sección está diseñada para administrar de forma centralizada toda la información relacionada con los proveedores de la empresa. Desde aquí, el usuario puede consultar, agregar, modificar o eliminar registros de proveedores, asegurando que los datos estén siempre actualizados. Además, permite llevar un control organizado de los datos de contacto, condiciones comerciales y demás detalles relevantes, facilitando así los procesos de compra, abastecimiento y comunicación con los distintos proveedores.
        </p>
    </section>
     <section class="section-two-proveedores">
      <h2 class="Crud-Style">Funcionalidades crud</h2>
     <p class="text-info-proveedores-dos">
         Bienvenido! podrás realizar todas las operaciones necesarias para gestionar proveedores de manera eficiente. Se incluyen funcionalidades para crear nuevos registros, consultar información existente, modificar datos y eliminar proveedores cuando sea necesario. Esta herramienta está pensada para mantener actualizada la base de datos de proveedores, optimizando los procesos administrativos y asegurando una gestión ágil y centralizada.
     </p>
 </section>
    <section>
        <div id="carouselExampleCaptions" class="carousel slide">
  <div class="carousel-indicators">
    <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
    <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="1" aria-label="Slide 2"></button>
    <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="2" aria-label="Slide 3"></button>
  </div>
  <div class="carousel-inner">
    <div class="carousel-item active">
      <img src="ImagenesWeb/ImagenComercio.jpeg" class="d-block w-100" alt="Comercio">
      <div class="carousel-caption d-none d-md-block">
        <h5>Lleva las riendas de tu negocio</h5>
      </div>
    </div>
    <div class="carousel-item">
      <img src="ImagenesWeb/ImagenDos.jpeg" class="d-block w-100" alt="Proveedor">
      <div class="carousel-caption d-none d-md-block">
        <h5>Lleva el control de tus proveedores</h5>
      </div>
    </div>
  </div>
  <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Previous</span>
  </button>
  <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Next</span>
  </button>
</div>
 </section>
  <section class="user-list">
      <p class="text-list">A continuación se presenta la lista completa de todos los proveedores registrados en nuestra base de datos. Aquí podrás consultar la información actualizada de cada uno, facilitando la gestión y el seguimiento de las relaciones comerciales de la empresa."</p>
     <div>
<asp:GridView runat="server" ID="Proveedores" CssClass="table table-striped table-bordered table-hover tabla-proveedores"></asp:GridView>
</div>
</section>
   
 
</asp:Content>
