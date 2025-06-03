<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicial.aspx.cs" Inherits="Comercio.Inicial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.6/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-4Q6Gf2aSP4eDXB8Miphtr37CMZZQ5oXLH2yaXMJ2w8e2ZtHTl7GptT4jmndRuHDT" crossorigin="anonymous">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Comsys</title>
    <style>
        
        body{
            background-color: #53096E;
            color: #FFFFFF;
        }
        .Textos_de_bienvenida h1{
            font-size: 60px;
        }
        .Textos_de_bienvenida p{
            font-size: 30px;
        }

       .Textos_de_bienvenida::before {
           content: "";
           position: absolute;
           top: 50%;
           left: 48%;
           width: 40vw;
           height: 40vw;
           max-width: 500px;
           max-height: 500px;
           background: url('/images/empty-cart.png') no-repeat center center;
           background-size: contain;
           filter: grayscale(100%) opacity(0.1);
           transform: translate(-50%, -50%);
           z-index: -1;
           pointer-events: none;
}

@media (max-width: 400px) {
    .Textos_de_bienvenida::before {
        width: 20vw;
        height: 20vw;
    }
}

.btn_vendedor {
    background-color: #FFA714;      
    color: #290152;                 
    padding: 15px 10px;             
    font-size: 20px;
    font-weight: bold;
    border: none;
    border-radius: 10px;
    cursor: pointer;
    transition: background-color 0.3s;
    margin-top: 20px;
}

.btn_vendedor:hover {
    background-color: #dddddd;      
}

.btn_administrador {
    background-color: #FFFFFF;      
          
    color: #120026;                 
    padding: 15px 10px;             
    font-size: 20px;
    font-weight: bold;
    border: none;
    border-radius: 10px;
    cursor: pointer;
    transition: background-color 0.3s;
    margin-top: 20px;
}

.btn_administrador:hover {
    background-color: #dddddd;      
}



    </style>
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

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.6/dist/js/bootstrap.bundle.min.js" integrity="sha384-j1CDi7MgGQ12Z7Qab0qlWQ/Qqz24Gc6BM0thvEMVjHnfYGF0rmFCozFSxQBxwHKO" crossorigin="anonymous"></script>
</body>
</html>
