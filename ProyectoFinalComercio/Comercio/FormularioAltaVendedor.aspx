<%@ Page Title="Formulario Alta Vendedor" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FormularioAltaVendedor.aspx.cs" Inherits="Comercio.FormularioAltaVendedor" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
  
</asp:Content>

<asp:Content ID="ContentMain" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-12 col-md-8 col-lg-6">
                <div class="bg-white p-4 rounded shadow">
                    <h2 class="mb-4 text-center styleRegister">Bienvenido al registro de vendedores</h2>

                    <div class="row mb-3">
                        <div class="col-12">
                            <label for="boxNombre" class="form-label text-center d-block">Ingrese su Nombre</label>
                            <asp:TextBox ID="boxNombre" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio" ControlToValidate="boxNombre" runat="server" ForeColor="Red"  />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-12">
                            <label for="boxApellido" class="form-label text-center d-block">Ingrese su Apellido</label>
                            <asp:TextBox ID="boxApellido" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ErrorMessage="El apellido es obligatorio" ControlToValidate="boxApellido" runat="server" ForeColor="Red" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-12">
                            <label for="boxDNI" class="form-label text-center d-block">Ingrese su DNI</label>
                            <asp:TextBox ID="boxDNI" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ErrorMessage="El DNI es obligatorio" ControlToValidate="boxDNI" runat="server" ForeColor="Red" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-12">
                            <label for="boxEmail" class="form-label text-center d-block">Ingrese su Email</label>
                            <asp:TextBox ID="boxEmail" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ErrorMessage="El Email es obligatorio" ControlToValidate="boxEmail" runat="server" ForeColor="Red" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-12">
                            <label for="boxPassword" class="form-label text-center d-block">Ingrese su Contraseña</label>
                            <asp:TextBox ID="boxPassword" runat="server" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator ErrorMessage="La contraseña es obligatoria" ControlToValidate="boxPassword" runat="server" ForeColor="Red" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12 d-flex justify-content-center">
                            <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse en el sistema" CssClass="btn btn-warning w-100" OnClick ="btnRegistrar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

