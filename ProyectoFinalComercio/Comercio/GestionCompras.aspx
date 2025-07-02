<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="GestionCompras.aspx.cs" Inherits="Comercio.GestionCompras" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Gestión de Compras</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="StyleGestionProductos.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />

         <!-- Botón para desplegar la barra lateral -->
         <button id="toggleSidebar" type="button" class="btn btn-warning">☰</button>

         <!-- Barra lateral -->
         <div id="sidebar" class="sidebar">
             <h1 class="sidebar-title">Comsys</h1>

             <div class="accordion accordion-flush" id="accordionSidebar">
                 <div class="accordion-item bg-transparent border-0">
                     <h2 class="accordion-header">
                         <button class="accordion-button collapsed bg-transparent text-light ps-0" type="button" data-bs-toggle="collapse" data-bs-target="#collapseCompras">
                             Sección Compras
                         </button>
                     </h2>
                     <div id="collapseCompras" class="accordion-collapse collapse show" data-bs-parent="#accordionSidebar">
                         <div class="accordion-body ps-3">
                             <asp:LinkButton ID="btnNuevaCompra" runat="server" OnClick="btnNuevaCompra_Click" CssClass="sidebar-link hover-effect">Nueva compra</asp:LinkButton>
                             <asp:LinkButton ID="btnActualizarListado" runat="server" OnClick="btnActualizarListado_Click" CssClass="sidebar-link hover-effect">Actualizar listado</asp:LinkButton>
                         </div>
                     </div>
                 </div>

                 <div class="accordion-item bg-transparent border-0">
                     <h2 class="accordion-header">
                         <button class="accordion-button collapsed bg-transparent text-light ps-0" type="button" data-bs-toggle="collapse" data-bs-target="#collapseGeneral">
                             Sección General
                         </button>
                     </h2>
                     <div id="collapseGeneral" class="accordion-collapse collapse" data-bs-parent="#accordionSidebar">
                         <div class="accordion-body ps-3">
                             <asp:LinkButton ID="btnVolver" runat="server" OnClick="btnVolver_Click" CssClass="sidebar-link hover-effect">Volver al panel</asp:LinkButton>
                         </div>
                     </div>
                 </div>
             </div>
         </div>

         <div id="blurOverlay" class="blur-overlay"></div>

        <div class="container mt-5">
            <h1 class="text-center text-white mb-4" style="font-family: 'Special Elite', monospace; font-size: 2.5rem;">
                Gestión de Compras
            </h1>

            <asp:GridView ID="gvCompras" runat="server" DataKeyNames="Id" AutoGenerateColumns="False"
                CssClass="table table-dark table-hover text-white mt-4" OnRowCommand="gvCompras_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Proveedor.RazonSocial" HeaderText="Proveedor" />
                    <asp:TemplateField HeaderText="Registrado por:">
                        <ItemTemplate>
                            <%# Eval("Usuario.Nombre") + " " + Eval("Usuario.Apellido") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C2}" />
                    <asp:ButtonField ButtonType="Button" Text="Modificar/Ver detalle" CommandName="Ver" ControlStyle-CssClass="btn btn-warning btn-sm" />
                    <asp:TemplateField>
                    <ItemTemplate>
                        <button type="button" class="btn btn-danger btn-sm" onclick="confirmarEliminacion('<%# Eval("Id") %>')">
                            Eliminar
                        </button>
                    </ItemTemplate>
                </asp:TemplateField>


                </Columns>
            </asp:GridView>

        </div>
    </form>

    <!-- Scripts -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script>
            const toggleBtn = document.getElementById('toggleSidebar');
            const sidebar = document.getElementById('sidebar');
            const blurOverlay = document.getElementById('blurOverlay');

            toggleBtn.addEventListener('click', function () {
                sidebar.classList.toggle('show');
                blurOverlay.classList.toggle('active');
                toggleBtn.classList.toggle('move-right');
            });

            blurOverlay.addEventListener('click', function () {
                sidebar.classList.remove('show');
                blurOverlay.classList.remove('active');
                toggleBtn.classList.remove('move-right');
            });
    </script>
    <script>
        function confirmarEliminacion(idCompra) {
            Swal.fire({
                title: '¿Estás seguro?',
                text: "Esta acción ocultará la compra.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {
                    __doPostBack('EliminarCompra', idCompra);
                }
            });
        }
    </script>

</body>
</html>
