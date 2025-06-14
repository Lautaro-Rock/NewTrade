<%@ Page Title="" Language="C#" MasterPageFile="~/Logins.Master" AutoEventWireup="true" CodeBehind="LoginVendedor.aspx.cs" Inherits="Comercio.Prueba" %>

<asp:Content ContentPlaceHolderID="phSubtitulo" runat="server">
    Vendedor
</asp:Content>

<asp:Content ContentPlaceHolderID="phFormulario" runat="server">
    <asp:Label ID="lbEmailUsuarioVnd" runat="server" Text="Email" CssClass="lbVndSty"></asp:Label><br />
    <asp:TextBox ID="txtEmailUsuarioVnd" runat="server" CssClass="txtVndSty"></asp:TextBox><br />

    <asp:Label ID="lbContraUsuarioVnd" runat="server" Text="Contraseña" CssClass="lbVndSty"></asp:Label><br />
    <asp:TextBox ID="txtContraVnd" runat="server" CssClass="txtVndSty" TextMode="Password"></asp:TextBox><br />

    <asp:Button ID="btnIngresarVnd" runat="server" Text="Ingresar" CssClass="btnVndSty" OnClick="btnIngresarVnd_Click" />
</asp:Content>

<asp:Content ContentPlaceHolderID="phLinkExtra" runat="server">
    <a href="FormularioAltaVendedor.aspx" class="lbVndSty lbPreguntaSty">¿No tenés cuenta? Crea una cuenta</a>
</asp:Content>
