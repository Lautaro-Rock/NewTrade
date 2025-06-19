<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GestionClientes.aspx.cs" Inherits="Comercio.GestionClientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="StyleCss/ClientesStyle.Css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
    <section class="section-clientes-one">
        <h2 class="h2Clientes">Seccion Clientes</h2>
        <p class="text-style">
            En esta sección, el vendedor podrá utilizar todas las funcionalidades CRUD (Crear, Leer, Actualizar y Eliminar) para administrar de forma eficiente los datos de los clientes. Esto incluye la posibilidad de registrar nuevos clientes, consultar sus datos, modificar la información existente y eliminar registros si fuera necesario. Una herramienta esencial para mantener una base de datos precisa, ordenada y al servicio de una atención al cliente de calidad.
        </p>
    </section>
    <section class="alerts-section">
  <div class="py-5 d-flex justify-content-center">
    <div class="w-75">
        <div class="alert alert-success text-center" role="alert">
            Los vendedores solo podrán administrar clientes
        </div>
        <div class="alert alert-danger text-center" role="alert">
            Para administrar vendedores debes loguearte como administrador
        </div>
    </div>
</div>
    </section>
   <section class="sectionAcordion">
    <div>
        <div class="accordion" id="accordionExample">
            <div class="accordion-item">
                <h2 class="accordion-header">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                        data-bs-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                        La importancia de nuestros clientes
                    </button>
                </h2>
                <div id="collapseOne" class="accordion-collapse collapse" data-bs-parent="#accordionExample">
                    <div class="accordion-body">
                        <strong>Los clientes son el motor de nuestro crecimiento.</strong> Establecer relaciones duraderas,
                        confiables y humanas con cada uno de ellos es esencial para garantizar la sostenibilidad de nuestro negocio.
                        Su satisfacción y fidelidad reflejan el compromiso de la empresa con la calidad, la atención personalizada
                        y la mejora continua de nuestros servicios.
                    </div>
                </div>
            </div>
            <div class="accordion-item">
                <h2 class="accordion-header">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                        data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                        Nuestro negocio multipropósito
                    </button>
                </h2>
                <div id="collapseTwo" class="accordion-collapse collapse" data-bs-parent="#accordionExample">
                    <div class="accordion-body">
                        <strong>Nuestra empresa ofrece soluciones integrales a diversos sectores.</strong> Gracias a una estructura
                        flexible y un enfoque dinámico, nos adaptamos a las distintas necesidades del mercado, ofreciendo productos
                        y servicios personalizados. Esta versatilidad nos permite crecer en múltiples áreas sin perder el foco en la
                        excelencia y en la atención al cliente.
                    </div>
                </div>
            </div>
            <div class="accordion-item">
                <h2 class="accordion-header">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                        data-bs-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                        La responsabilidad de nuestros vendedores
                    </button>
                </h2>
                <div id="collapseThree" class="accordion-collapse collapse" data-bs-parent="#accordionExample">
                    <div class="accordion-body">
                        <strong>Los vendedores son embajadores de la empresa.</strong> Su labor va mucho más allá de una transacción:
                        deben brindar una atención cercana, registrar información precisa, acompañar al cliente durante el proceso de compra
                        y mantener una comunicación clara y profesional. Su compromiso con la transparencia, el respeto y la eficiencia
                        es clave para construir relaciones de confianza.
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
 <section class="clientesList">
         <h2 class="h2ClientesTwo">Nuestros clientes</h2>
         <p class="text-styleTwo">
                A continuación, se presenta el listado completo de todos los clientes actualmente registrados en nuestra base de datos. Esta información es fundamental para llevar un seguimiento detallado de nuestras relaciones comerciales, facilitando la gestión de ventas, el contacto directo con los clientes y la planificación de estrategias orientadas a la fidelización y mejora continua del servicio.
         </p>
         <div>
          <asp:GridView ID="Clientes" runat="server" CssClass="table table-striped table-bordered table-hover tabla-proveedores" AutoGenerateColumns="False">
          <Columns>
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
        <asp:BoundField DataField="Email" HeaderText="Correo" />
        <asp:BoundField DataField="Rol" HeaderText="Rol" />
       </Columns>
        </asp:GridView>
         </div>
 </section>
 <section class="section-new-client">  
      <h2 class="h2ClientesInsert">Ingresar un nuevo Cliente</h2>
      <p class="text-styleInster">¡Esta sección está diseñada para ingresar un nuevo cliente a nuestro sistema!</p>
  <div class="container">
     <div class="row justify-content-center">
         <div class="col-md-6">
             <div class="mb-3">
                 <asp:Label ID="Label1" runat="server" Text="Ingrese su Nombre" CssClass="form-label text-white"></asp:Label>
                 <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control"></asp:TextBox>
             </div>

             <div class="mb-3">
                 <asp:Label ID="Label2" runat="server" Text="Ingrese su Apellido" CssClass="form-label text-white"></asp:Label>
                 <asp:TextBox ID="TxtApellido" runat="server" CssClass="form-control"></asp:TextBox>
             </div>

             <div class="mb-3">
                 <asp:Label ID="Label3" runat="server" Text="Ingrese su DNI" CssClass="form-label text-white"></asp:Label>
                 <asp:TextBox ID="TxtD" runat="server" CssClass="form-control"></asp:TextBox>
             </div>

             <div class="mb-3">
                 <asp:Label ID="Label4" runat="server" Text=" Ingrese su Email" CssClass="form-label text-white"></asp:Label>
                 <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control"></asp:TextBox>
             </div>

             <div class="d-flex justify-content-center">
             <asp:Button ID="InsertClient" onclick="InsertClient_Click" runat="server"  Text="Registrar Cliente" CssClass="btn btn-primary btn-md rounded-pill px-5 my-3"/>
             </div>
         </div>
     </div>
 </div>
 </section>
     <section class="section-edit-client">  
      <h2 class="h2ClientesInsert">Edita un Cliente</h2>
      <p class="text-styleInster">¡Esta sección está diseñada para editar un cliente de nuestro sistema. Ingrese el email para editar el usuario!</p>
  <div class="container">
     <div class="row justify-content-center">
         <div class="col-md-6">
                <div class="mb-3">
                 <asp:Label ID="Label9" runat="server" Text=" Ingrese el email del usuario " CssClass="form-label text-white"></asp:Label>
                 <asp:TextBox ID="txtEditEmail" runat="server" CssClass="form-control"></asp:TextBox>
             </div>

             <div class="mb-3">
                 <asp:Label ID="Label6" runat="server" Text="Ingrese su Nombre" CssClass="form-label text-white"></asp:Label>
                 <asp:TextBox ID="txtEditNombre" runat="server" CssClass="form-control"></asp:TextBox>
             </div>

             <div class="mb-3">
                 <asp:Label ID="Label7" runat="server" Text="Ingrese su Apellido" CssClass="form-label text-white"></asp:Label>
                 <asp:TextBox ID="txtEditApellido" runat="server" CssClass="form-control"></asp:TextBox>
             </div>

             <div class="mb-3">
                 <asp:Label ID="Label8" runat="server" Text="Ingrese su DNI" CssClass="form-label text-white"></asp:Label>
                 <asp:TextBox ID="txtEditDni" runat="server" CssClass="form-control"></asp:TextBox>
             </div>

             <div class="d-flex justify-content-center">
             <asp:Button ID="EditClient" OnClick="EditClient_Click"  runat="server"  Text="Editar Cliente" CssClass="btn btn-warning btn-md rounded-pill px-5 my-3"/>
             </div>
         </div>
     </div>
 </div>
 </section>
        <section class="section-delete-client">  
     <h2 class="h2ClientesInsert">Eliminar un Cliente</h2>
     <p class="text-styleInster">¡Esta sección está diseñada para eliminar un cliente de nuestro sistema. Ingrese el email para eliminar el usuario!</p>
 <div class="container">
    <div class="row justify-content-center">
        <div class="col-md-6">
               <div class="mb-3">
                <asp:Label ID="Label11" runat="server" Text=" Ingrese el email del usuario " CssClass="form-label text-white"></asp:Label>
                <asp:TextBox ID="DeleteEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="d-flex justify-content-center">
            <asp:Button ID="DeleteClient" OnClick="DeleteClient_Click" runat="server"  Text="Eliminar Cliente" CssClass="btn btn-danger btn-md rounded-pill px-5 my-3"/>
            </div>
        </div>
    </div>
</div>
</section>
</div>
</asp:Content>
