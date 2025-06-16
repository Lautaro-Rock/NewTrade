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
     <section>
      <h2 class="Crud-Style">Gestión Estratégica de Proveedores</h2>
     <p class="text-info-proveedores-dos">
       Los proveedores son una pieza clave en el funcionamiento de nuestra empresa. Gracias a ellos, podemos garantizar el abastecimiento constante de productos y materiales esenciales para nuestra operación. Esta sección está destinada a facilitar una gestión eficaz y ordenada de todos nuestros proveedores, promoviendo relaciones comerciales sólidas, información actualizada y procesos de compra más ágiles. Un manejo eficiente de los proveedores contribuye directamente a la calidad del servicio que brindamos a nuestros clientes.
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
<section class="section-two-proveedores">
    <h2 class="Crud-Style">Ingresar un nuevo proveedor</h2>
    <p class="text-info-proveedores-dos">¡Esta sección está diseñada para ingresar un nuevo proveedor a nuestro sistema!</p>
     <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="mb-3">
                    <asp:Label ID="Label1" runat="server" Text="Razón Social" CssClass="form-label text-white"></asp:Label>
                    <asp:TextBox ID="TextRazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="Label2" runat="server" Text="CUIT" CssClass="form-label text-white"></asp:Label>
                    <asp:TextBox ID="TextCuit" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="Label3" runat="server" Text="Email" CssClass="form-label text-white"></asp:Label>
                    <asp:TextBox ID="TextEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="Label4" runat="server" Text="Teléfono" CssClass="form-label text-white"></asp:Label>
                    <asp:TextBox ID="TextTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="Label5" runat="server" Text="Dirección" CssClass="form-label text-white"></asp:Label>
                    <asp:TextBox ID="TextDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="d-flex justify-content-center">
                <asp:Button ID="RegisterProv" runat="server" OnClick="RegisterProv_Click" Text="Registrar proveedor" CssClass="btn btn-primary btn-md rounded-pill px-5 my-3"/>
                </div>
            </div>
        </div>
    </div>
</section>
<section class="section-color">
     <h2 class="Crud-Style pt-4">Editar un proveedor</h2>
 <p class="text-info-proveedores-dos">¡Esta sección está diseñada para editar un  proveedor existense en nuestro sistema!</p>
  <div class="container">
     <div class="row justify-content-center">
         <div class="col-md-6">
               <div class="mb-3">
                  <asp:Label ID="Label11" runat="server" Text="ID del proveedor" CssClass="form-label text-white"></asp:Label>
                  <asp:TextBox ID="IdEdit" runat="server" CssClass="form-control"></asp:TextBox>
              </div>
             <div class="mb-3">
                 <asp:Label ID="Label6" runat="server" Text="Razón Social" CssClass="form-label text-white"></asp:Label>
                 <asp:TextBox ID="TextEditRazon" runat="server" CssClass="form-control"></asp:TextBox>
             </div> 
             <div class="mb-3">
                 <asp:Label ID="Label7" runat="server" Text="CUIT" CssClass="form-label text-white"></asp:Label>
                 <asp:TextBox ID="TextEditCuit" runat="server" CssClass="form-control"></asp:TextBox>
             </div>

             <div class="mb-3">
                 <asp:Label ID="Label8" runat="server" Text="Email" CssClass="form-label text-white"></asp:Label>
                 <asp:TextBox ID="TextEditEmail" runat="server" CssClass="form-control"></asp:TextBox>
             </div>

             <div class="mb-3">
                 <asp:Label ID="Label9" runat="server" Text="Teléfono" CssClass="form-label text-white"></asp:Label>
                 <asp:TextBox ID="TextEditTel" runat="server" CssClass="form-control"></asp:TextBox>
             </div>

             <div class="mb-3">
                 <asp:Label ID="Label10" runat="server" Text="Dirección" CssClass="form-label text-white"></asp:Label>
                 <asp:TextBox ID="TextEditDire" runat="server" CssClass="form-control"></asp:TextBox>
             </div>

             <div class="d-flex justify-content-center">
             <asp:Button ID="EditProveedor" runat="server" OnClick="EditProveedor_Click"  Text="Editar proveedor" CssClass="btn btn-warning btn-md rounded-pill px-5 my-3"/>
             </div>
         </div>
     </div>
 </div>
</section>
    <section class="section-two-proveedores ">
     <h2 class="Crud-Style pt-4 ">Eliminar un proveedor</h2>
 <p class="text-info-proveedores-dos">¡Esta sección está diseñada para eliminar un proveedor existense en nuestro sistema!</p>
  <div class="container">
     <div class="row justify-content-center">
         <div class="col-md-6">
               <div class="mb-3">
                  <asp:Label ID="Label12" runat="server" Text="ID del proveedor" CssClass="form-label text-white"></asp:Label>
                  <asp:TextBox ID="IdDelete" runat="server" CssClass="form-control"></asp:TextBox>
              </div>
             <div class="d-flex justify-content-center">
             <asp:Button ID="DeleteProv" runat="server" OnClick="DeleteProv_Click"  Text="Eliminar proveedor" CssClass="btn btn-danger btn-md rounded-pill px-5 my-3"/>
             </div>
         </div>
     </div>
 </div>
</section>
   
 
</asp:Content>
