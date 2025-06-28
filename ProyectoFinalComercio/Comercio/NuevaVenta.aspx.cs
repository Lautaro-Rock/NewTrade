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
            if (!IsPostBack)
            {
                try
                {
                    Productos = new ProductoNegocio().ListarProductos();

                    var negocioCliente = new NegocioCliente();
                    Session["lista"] = negocioCliente.ListarClientes();

                    dgvProductos.DataSource = Productos;
                    dgvProductos.DataBind();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
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

                if (cantidad > producto.Stock)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "stockInsuficiente",
                        $"Swal.fire('Stock insuficiente', 'Solo hay {producto.Stock} unidades disponibles.', 'warning');", true);
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
                lbPrecio.Text = articulosAgregados.Sum(d => d.Cantidad * d.PrecioUnitario).ToString("C2");
                dgvProductos.DataSource = productos;
                dgvProductos.DataBind();


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
    }
}