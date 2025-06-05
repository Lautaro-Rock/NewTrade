<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginVendedor.aspx.cs" Inherits="Comercio.LoginVendedor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Comsys - Login de vendedor</title>
<link href="StyleLoginVendedor.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container_Vnd">
         <h1>Acesso</h1>
         <h2>Vendedor</h2>
         <asp:Label ID="lbEmailUsuarioVnd" runat="server" Text="Email" CssClass="lbVndSty"></asp:Label>
         <br />
         <asp:TextBox ID="txtEmailUsuarioVnd" runat="server" CssClass="txtVndSty"></asp:TextBox>
         <asp:Label ID="lbContraUsuarioVnd" runat="server" Text="Contraseña" CssClass="lbVndSty" ></asp:Label><br />
         <asp:TextBox ID="txtContraVnd" runat="server" CssClass="txtVndSty"></asp:TextBox>
         <asp:Button ID="btnIngresarVnd" runat="server" Text="Ingresar" CssClass="btnVndSty" OnClick="btnIngresarVnd_Click"/>
         <a href="FormularioAltaVendedor.aspx" class="lbVndSty lbPreguntaSty">¿No tenes cuenta? Crea una cuenta</a>     
</div>
    </form>
</body>
</html>
