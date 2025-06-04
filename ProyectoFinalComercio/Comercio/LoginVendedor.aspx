<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginVendedor.aspx.cs" Inherits="Comercio.LoginVendedor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login de vendedor</title>
<link href="StyleLoginVendedor.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container_Vnd">
         <h1>Acesso</h1>
         <h2>Vendedor</h2>
         <asp:Label ID="lbUsuarioVnd" runat="server" Text="Usuario" CssClass="lbVndSty"></asp:Label>
         <br />
         <asp:TextBox ID="txtUsuarioVnd" runat="server" CssClass="txtVndSty"></asp:TextBox>
         <asp:Label ID="lbContraUsuarioVnd" runat="server" Text="Contraseña" CssClass="lbVndSty" ></asp:Label><br />
         <asp:TextBox ID="txtContraVnd" runat="server" CssClass="txtVndSty"></asp:TextBox>
         <asp:Button ID="btnIngresarVnd" runat="server" Text="Ingresar" CssClass="btnVndSty" OnClick="btnIngresarVnd_Click"/>
         <a href="AltaVendedor.aspx" class="lbVndSty lbPreguntaSty">¿Nuevo vendedor? Solicita tu alta :)</a>     
</div>
    </form>
</body>
</html>
