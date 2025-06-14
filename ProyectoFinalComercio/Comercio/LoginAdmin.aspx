<%@ Page Title="Login Admin" Language="C#" MasterPageFile="~/Logins.Master" AutoEventWireup="true" CodeBehind="LoginAdmin.aspx.cs" Inherits="Comercio.PruebaAdmin" %>

<asp:Content ContentPlaceHolderID="phSubtitulo" runat="server">
    Administrador
</asp:Content>

<asp:Content ContentPlaceHolderID="phFormulario" runat="server">
    <asp:Label ID="lbUsuarioAdmn" runat="server" Text="Usuario" CssClass="lbAdmnlSty"></asp:Label><br />
    <asp:TextBox ID="txtUsuarioAdmn" runat="server" CssClass="txtAdmnSty"></asp:TextBox><br />

    <asp:Label ID="lbContraUsuarioAdmn" runat="server" Text="Contraseña" CssClass="lbAdmnlSty"></asp:Label><br />
    <asp:TextBox ID="txtContraAdmn" runat="server" CssClass="txtAdmnSty" TextMode="Password"></asp:TextBox><br />

    <asp:Button ID="btnIngresarAdmn" runat="server" Text="Ingresar" CssClass="btnAdmnSty" OnClick="btnIngresarAdmn_Click" />
</asp:Content>
