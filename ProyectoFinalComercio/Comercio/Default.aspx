<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Comercio.Inicial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.6/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-4Q6Gf2aSP4eDXB8Miphtr37CMZZQ5oXLH2yaXMJ2w8e2ZtHTl7GptT4jmndRuHDT" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.6/dist/js/bootstrap.bundle.min.js" integrity="sha384-j1CDi7MgGQ12Z7Qab0qlWQ/Qqz24Gc6BM0thvEMVjHnfYGF0rmFCozFSxQBxwHKO" crossorigin="anonymous"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Comsys</title>
    <link href="StyleDefault.css" rel="stylesheet" type="text/css" />
</head>
<body class="d-flex justify-content-center align-items-center vh-100">
    <form id="form1" runat="server">
        <div class="Textos_de_bienvenida">
            <h1>Bienvenido a</h1>
            <h1>Comsys</h1>
            <p>Accede a tu cuenta</p>
            <div class="d-flex justify-content-center gap-3 mt-4 flex-wrap">
            <asp:Button ID="btnIngresoVnd" runat="server" Text="Soy vendedor" CssClass="btn_vendedor" OnClick="btnIngresoVnd_Click"/>
            <asp:Button ID="btnIngresoAdm" runat="server" Text="Soy administrador" CssClass="btn_administrador" OnClick="btnIngresoAdm_Click" />
        </div>
        </div>
    </form>
</body>
</html>
