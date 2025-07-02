using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;
using Helpers;

namespace Comercio
{
    public partial class NuevaVenta : System.Web.UI.Page
    {
        public List<Producto> Productos
        {
            get { return Session["Productos"] as List<Producto>; }
            set { Session["Productos"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
               Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {

                    int idVenta = int.Parse(Request.QueryString["id"]);
                    CargarVentaParaModificar(idVenta);
                    btnConfirmarVenta.Visible = false;
                    btnModificarVenta.Visible = true;
                    Session["IdVentaEnEdicion"] = idVenta;
                }
                else
                {
                    btnConfirmarVenta.Visible = true;
                    btnModificarVenta.Visible = false;
                }

                try
                {
                    Productos = new ProductoNegocio().ListarProductos();

                    // Si venís del catálogo, se descuenta del stock lo que ya está en el carrito
                    if (Session["DesdeCatalogo"] != null && (bool)Session["DesdeCatalogo"])
                    {
                        var detalleCatalogo = Session["VentaDetalle"] as List<DetalleVenta>;
                        if (detalleCatalogo != null && detalleCatalogo.Any())
                        {
                            foreach (var d in detalleCatalogo)
                            {
                                Producto original = Productos.FirstOrDefault(p => p.Id == d.Producto.Id);
                                if (original != null)
                                    original.Stock -= d.Cantidad;
                            }
                        }
                        Session["DesdeCatalogo"] = null;

                        // Cargamos los productos agregados al carrito
                        if (detalleCatalogo != null && detalleCatalogo.Any())
                        {
                            gvDetalleVenta.DataSource = detalleCatalogo;
                            gvDetalleVenta.DataBind();
                            decimal totalConGanancia = detalleCatalogo.Sum(d =>
                            {
                                decimal ganancia = d.Producto.PorcentajeGanancia ?? 0;
                                decimal precioFinal = d.PrecioUnitario * (1 + ganancia / 100);
                                return precioFinal * d.Cantidad;
                            });
                            lbPrecio.Text = totalConGanancia.ToString("C2");

                        }

                        // Validamos si estabamos en modo edicion:
                        if (Session["IdVentaEnEdicion"] != null)
                        {
                            btnConfirmarVenta.Visible = false;
                            btnModificarVenta.Visible = true;
                            hfIdVenta.Value = Session["IdVentaEnEdicion"].ToString();
                        }

                        // Validamos si ya había un cliente seleccionado 
                        if (Session["IdClienteSeleccionado"] != null)
                        {
                            hfIdClienteSeleccionado.Value = Session["IdClienteSeleccionado"].ToString();
                            txtClienteSeleccionado.Text = Session["NombreClienteSeleccionado"].ToString();
                        }

                    }

                    var negocioCliente = new NegocioCliente();
                    Session["lista"] = negocioCliente.ListarClientes();

                    gvClientes.DataSource = Session["lista"];
                    gvClientes.DataBind();


                    dgvProductos.DataSource = Productos;
                    dgvProductos.DataBind();

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorGuardando",
                    $"Swal.fire('Error', 'Detalle: {ex.Message.Replace("'", "\\'")}', 'error');", true);
                }
            }
        }

        protected string ObtenerSubtotalConGanancia(object dataItem)
        {
            var detalle = dataItem as DetalleVenta;
            if (detalle == null) return "";

            decimal porcentaje = detalle.Producto.PorcentajeGanancia ?? 0;
            decimal precioFinal = detalle.PrecioUnitario * (1 + porcentaje / 100);
            decimal subtotalFinal = precioFinal * detalle.Cantidad;

            return subtotalFinal.ToString("C2");
        }

        protected void txtBuscarCliente_TextChanged(object sender, EventArgs e)
        {
            var lista = Session["lista"] as List<Cliente>;
            var filtrados = lista.FindAll(c =>
                c.Nombre.ToUpper().Contains(txtBuscarCliente.Text.ToUpper())
            );

            gvClientes.DataSource = filtrados;
            gvClientes.DataBind();

            // Reabrir el panel de búsqueda si se cerró por el postback
            ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirBuscarCliente", @"
                    setTimeout(function() {
                    var myCollapse = document.getElementById('collapseBuscarCliente');
                    if (myCollapse) {
                        var bsCollapse = new bootstrap.Collapse(myCollapse, { toggle: false });
                        bsCollapse.show();
                    }
                }, 100);
            ", true);
        }

        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string apellido_cliente = gvClientes.SelectedRow.Cells[1].Text;
            string nombre_cliente = gvClientes.SelectedRow.Cells[0].Text;
            txtClienteSeleccionado.Text = apellido_cliente + ", " + nombre_cliente;
            int idCliente = Convert.ToInt32(gvClientes.SelectedDataKey.Value);
            hfIdClienteSeleccionado.Value = idCliente.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "cerrarPanel", @"
                    setTimeout(function() {
                    var myCollapse = document.getElementById('collapseBuscarCliente');
                    if (myCollapse) {
                        var bsCollapse = bootstrap.Collapse.getOrCreateInstance(myCollapse);
                        bsCollapse.hide();
                    }
                }, 100);
            ", true);
        }

        protected void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            List<Producto> lista_buscada = Productos.FindAll(p => p.Nombre.ToUpper().Contains(txtBuscarProducto.Text.ToUpper()));
            dgvProductos.DataSource = lista_buscada;
            dgvProductos.DataBind();

        }

        protected void dgvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Agregar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = ((Control)e.CommandSource).NamingContainer as GridViewRow;
                TextBox txtCantidad = fila.FindControl("txtCantidad") as TextBox;


                int cantidad;

                if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "cantidadInvalida",
                        "Swal.fire('Cantidad inválida', 'Debe ingresar una cantidad mayor a cero.', 'warning');", true);
                    return;
                }


                int idProducto = Convert.ToInt32(e.CommandArgument);
                List<Producto> productos = Session["Productos"] as List<Producto>;
                Producto producto = productos.FirstOrDefault(p => p.Id == idProducto);


                if (producto == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "productoNoEncontrado",
                        "Swal.fire('Error', 'Producto no encontrado.', 'error');", true);
                    return;
                }

                List<DetalleVenta> articulosAgregados = Session["VentaDetalle"] as List<DetalleVenta> ?? new List<DetalleVenta>();

                if (articulosAgregados.Any(d => d.Producto.Id == producto.Id))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "productoRepetido",
                        "Swal.fire('Ya agregado', 'Este producto ya fue agregado a la venta.', 'info');", true);
                    return;
                }

                int stockFinal = producto.Stock - cantidad;
                if (stockFinal < producto.StockMin)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "stockMinimo",
                        $"Swal.fire('Stock mínimo', 'No podés agregar esa cantidad. El stock quedaría por debajo del mínimo permitido ({producto.StockMin}).', 'warning');", true);
                    return;
                }


                // Descontar stock "en memoria"
                producto.Stock -= cantidad;
                Productos = productos;


                DetalleVenta nuevo = new DetalleVenta
                {
                    Producto = producto,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio,
                    Activo = true
                };

                articulosAgregados.Add(nuevo);
                Session["VentaDetalle"] = articulosAgregados;
                gvDetalleVenta.DataSource = Session["VentaDetalle"] as List<DetalleVenta>;
                gvDetalleVenta.DataBind();

                // Total actualizado
                decimal totalConGanancia = articulosAgregados.Sum(d =>
                {
                    decimal ganancia = d.Producto.PorcentajeGanancia ?? 0;
                    decimal precioFinal = d.PrecioUnitario * (1 + ganancia / 100);
                    return precioFinal * d.Cantidad;
                });
                lbPrecio.Text = totalConGanancia.ToString("C2");

                dgvProductos.DataSource = productos;
                dgvProductos.DataBind();


            }
        }
        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idProducto = Convert.ToInt32(btn.CommandArgument);

            var detalle = Session["VentaDetalle"] as List<DetalleVenta>;
            var productos = Productos;

            var item = detalle.FirstOrDefault(d => d.Producto.Id == idProducto);
            if (item != null)
            {
                detalle.Remove(item);

                Producto original = productos.FirstOrDefault(p => p.Id == idProducto);
                if (original != null)
                    original.Stock += item.Cantidad;

                Session["VentaDetalle"] = detalle;
                Productos = productos;

                gvDetalleVenta.DataSource = detalle;
                gvDetalleVenta.DataBind();

                dgvProductos.DataSource = productos;
                dgvProductos.DataBind();

                decimal totalConGanancia = detalle.Sum(d =>
                {
                    decimal ganancia = d.Producto.PorcentajeGanancia ?? 0;
                    decimal precioFinal = d.PrecioUnitario * (1 + ganancia / 100);
                    return precioFinal * d.Cantidad;
                });
                lbPrecio.Text = totalConGanancia.ToString("C2");

            }

        }

        protected void btnVaciarDetalleVenta_Click(object sender, EventArgs e)
        {
            List<DetalleVenta> detalleActual = Session["VentaDetalle"] as List<DetalleVenta>;
            List<Producto> productos = Productos;

            if (detalleActual != null)
            {
                foreach (var item in detalleActual)
                {
                    Producto original = productos.FirstOrDefault(p => p.Id == item.Producto.Id);
                    if (original != null)
                    {
                        original.Stock += item.Cantidad;
                    }
                }
            }

            // Resetear lista
            Session["VentaDetalle"] = new List<DetalleVenta>();

            // Actualizar Grid de productos y total
            Productos = productos;
            dgvProductos.DataSource = productos;
            dgvProductos.DataBind();

            gvDetalleVenta.DataSource = null;
            gvDetalleVenta.DataBind();

            lbPrecio.Text = "$0.00";

        }

        protected void btnConfirmarVenta_Click(object sender, EventArgs e)
        {
            var detalle = Session["VentaDetalle"] as List<DetalleVenta>;
            if (detalle == null || !detalle.Any())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "sinProductos",
                    "Swal.fire('Sin productos', 'Agregá al menos un producto antes de confirmar la venta.', 'error');", true);
                return;
            }

            if (string.IsNullOrEmpty(hfIdClienteSeleccionado.Value) || !int.TryParse(hfIdClienteSeleccionado.Value, out int idCliente))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "clienteNoSeleccionado",
                    "Swal.fire('Cliente no seleccionado', 'Por favor, seleccioná un cliente válido.', 'error');", true);
                return;
            }


            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "usuarioNull",
                    "Swal.fire('Error', 'No se encontró el usuario logueado.', 'error');", true);
                return;
            }

            Venta venta = new Venta
            {
                Cliente = new Cliente { Id = idCliente },
                Usuario = new Usuario { Id = usuario.Id },
                DetalleList = detalle,
                NumeroFactura = VentasHelper.GenerarNumeroFactura(),
                Total = detalle.Sum(d => d.Subtotal),
                Fecha = DateTime.Now,
                Activo = true
            };

            try
            {
                new VentaNegocio().AgregarVentaCompleta(venta);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ventaExitosa",
                    "Swal.fire('¡Venta confirmada!', 'Se registró correctamente.', 'success');", true);

                Session["VentaDetalle"] = new List<DetalleVenta>();
                gvDetalleVenta.DataSource = null;
                gvDetalleVenta.DataBind();

                lbPrecio.Text = "$0.00";

                Session["IdClienteSeleccionado"] = null;
                Session["NombreClienteSeleccionado"] = null;


                Productos = new ProductoNegocio().ListarProductos();
                dgvProductos.DataSource = Productos;
                dgvProductos.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorGuardando",
                $"Swal.fire('Error', 'Detalle: {ex.Message.Replace("'", "\\'")}', 'error');", true);

            }


        }

        private void CargarVentaParaModificar(int idVenta)
        {
            try
            {
                Venta venta = new VentaNegocio().ObtenerVentaPorId(idVenta);

                if (((Dominio.Usuario)Session["usuario"]).Rol == "Vendedor" && venta.Usuario.Id != ((Dominio.Usuario)Session["usuario"]).Id)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "Swal.fire('Acceso denegado', 'No podés modificar una venta que no registraste.', 'error');", true);
                    Response.Redirect("GestionarVentas.aspx");
                    return;
                }

                // Setear cliente
                txtClienteSeleccionado.Text = $"{venta.Cliente.Nombre}, {venta.Cliente.Apellido}";
                hfIdClienteSeleccionado.Value = venta.Cliente.Id.ToString();

                // Setear detalles de la venta
                Session["VentaDetalle"] = venta.DetalleList;
                gvDetalleVenta.DataSource = venta.DetalleList;
                gvDetalleVenta.DataBind();

                // Mostrar precio total
                lbPrecio.Text = venta.Total.ToString("C2");

                // Guardar ID de la venta en un hidden field
                hfIdVenta.Value = venta.Id.ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorCargandoVenta",
                    $"Swal.fire('Error', 'No se pudo cargar la venta: {ex.Message.Replace("'", "\\'")}', 'error');", true);
                return;
            }

        }

        protected void btnModificarVenta_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(hfIdVenta.Value, out int idVenta))
                return;

            var detalle = Session["VentaDetalle"] as List<DetalleVenta>;

            if (detalle == null || !detalle.Any())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "sinProductos",
                    "Swal.fire('Sin productos', 'Agregá al menos un producto antes de confirmar la venta.', 'error');", true);
                return;
            }

            if (string.IsNullOrEmpty(hfIdClienteSeleccionado.Value) || !int.TryParse(hfIdClienteSeleccionado.Value, out int idCliente))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "clienteNoSeleccionado",
                    "Swal.fire('Cliente no seleccionado', 'Por favor, seleccioná un cliente válido.', 'error');", true);
                return;
            }


            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "usuarioNull",
                    "Swal.fire('Error', 'No se encontró el usuario logueado.', 'error');", true);
                return;
            }

            Venta venta = new Venta
            {
                Id = idVenta,
                Cliente = new Cliente { Id = idCliente },
                DetalleList = detalle,
                Total = detalle.Sum(d => d.Subtotal),
                Activo = true
            };

            try
            {
                new VentaNegocio().ModificarVentaCompleta(venta);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ventaModificada",
                    "Swal.fire('¡Venta modificada!', 'Los cambios fueron guardados correctamente.', 'success');", true);

                Session["IdClienteSeleccionado"] = null;
                Session["NombreClienteSeleccionado"] = null;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorModificar",
                    $"Swal.fire('Error', 'No se pudo modificar: {ex.Message.Replace("'", "\\'")}', 'error');", true);
            }

        }

        protected void btnVerCatalogoDetallado_Click(object sender, EventArgs e)
        {
            // Si ya tenemos productos agregados al carrito antes de ir al catalogo, los transportamos
            if (Session["VentaDetalle"] != null)
            {
                Session["DetalleDesdeVenta"] = Session["VentaDetalle"];
            }
            // Si ya tenemos un cliente asignado tambien lo guardamos
            if (!string.IsNullOrEmpty(hfIdClienteSeleccionado.Value))
            {
                Session["IdClienteSeleccionado"] = hfIdClienteSeleccionado.Value;
                Session["NombreClienteSeleccionado"] = txtClienteSeleccionado.Text;
            }


            Session["DesdeVenta"] = true;
            Response.Redirect("CatalogoProductos.aspx");

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            // Reseteamos los sessions
            Session["IdVentaEnEdicion"] = null;
            Session["IdClienteSeleccionado"] = null;
            Session["NombreClienteSeleccionado"] = null;
            Session["VentaDetalle"] = null;


            if (((Dominio.Usuario)Session["usuario"]).Rol == "Administrador")
            {
                Response.Redirect("GestionarVentas.aspx");
            }
            else
            {
                Response.Redirect("PanelCtrlAdmin.aspx");
            }
        }
    }
}