<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestiónDeProductos.aspx.cs" Inherits="Comercio.GestiónDeProductos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />   
    <link href="StylePaginasDeGestiones.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="container p-5">
        <div class="row g-4">
           <h1>Gestion de productos</h1>
            <% foreach (Dominio.Producto temporalpr in Productos) { %>
                <div class="col-12 col-md-6"> 
                    
                    <div class="card mb-3" style="max-width: 100%;"> 
                        <div class="row g-0">
                            <div class="col-md-4 fondo-imagen">
                                <img src="<%: temporalpr.UrlImgProducto %>" class="img-fluid rounded-start" alt="...">
                            </div>
                            <div class="col-md-8">
                                <div class="card-body">
                                    <h5 class="card-title"><%: temporalpr.Nombre %></h5>
                                    <asp:Button ID="Button1" runat="server" Text="Modificar" CssClass="btn btn-outline-warning me-2"/>
                                    <asp:Button ID="Button2" runat="server" Text="Eliminar" CssClass="btn btn-outline-warning me-2"/>
                                    <asp:Button ID="Button3" runat="server" Text="Ver mas.." CssClass="btn btn-outline-warning me-2" />
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            <% } %>
        </div>
    </form>
</body>
</html>
