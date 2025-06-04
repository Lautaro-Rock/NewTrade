<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginAdmin.aspx.cs" Inherits="Comercio.LoginAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login De Administrador</title>
<link href="StyleLoginAdmin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Acesso</h1>
            <h2>Administrador</h2>
            <asp:Label ID="lbUsuarioAdmn" runat="server" Text="Usuario" CssClass="lbAdmnlSty"></asp:Label>
            <br />
            <asp:TextBox ID="txtUsuarioAdmn" runat="server" CssClass="txtAdmnSty"></asp:TextBox>
            <asp:Label ID="lbContraUsuarioAdmn" runat="server" Text="Contraseña" CssClass="lbAdmnlSty" ></asp:Label><br />
            <asp:TextBox ID="txtContraAdmn" runat="server" CssClass="txtAdmnSty"></asp:TextBox>
            <asp:Button ID="btnIngresarAdmn" runat="server" Text="Ingresar" CssClass="btnAdmnSty" OnClick="btnIngresarAdmn_Click"/>
                   
        </div>
    </form>
</body>
</html>
