using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class CatalogoProductos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();

                var productos = new ProductoNegocio().ListarProductos();

                // Si venimos desde la venta:
                if (Session["DesdeVenta"] != null && (bool)Session["DesdeVenta"])
                {
                    var detalle = Session["DetalleDesdeVenta"] as List<DetalleVenta>;

                    if (detalle != null && productos != null)
                    {
                        foreach (var d in detalle)
                        {
                            Producto p = productos.FirstOrDefault(prod => prod.Id == d.Producto.Id);
                            if (p != null)
                                p.Stock -= d.Cantidad;
                        }

                        // También cargar el carrito en la vista
                        gvDetalleCatalogo.DataSource = detalle;
                        gvDetalleCatalogo.DataBind();
                        decimal totalConGanancia = detalle.Sum(d =>
                        {
                            decimal ganancia = d.Producto.PorcentajeGanancia ?? 0;
                            decimal precioFinal = d.PrecioUnitario * (1 + ganancia / 100);
                            return precioFinal * d.Cantidad;
                        });
                        lblTotalCatalogo.Text = "Total: " + totalConGanancia.ToString("C2");


                        // Y que permanezca disponible para seguir trabajando
                        Session["VentaDetalle"] = detalle;
                    }

                    Session["DesdeVenta"] = null;
                }

                Session["Productos"] = productos;
                repCatalogo.DataSource = productos;
                repCatalogo.DataBind();
            }

        }

        protected void repCatalogo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Agregar")
            {
                int idProducto = Convert.ToInt32(e.CommandArgument);
                List<Producto> productos = Session["Productos"] as List<Producto>;
                Producto producto = productos.FirstOrDefault(p => p.Id == idProducto);

                TextBox txtCantidad = e.Item.FindControl("txtCantidad") as TextBox;
                int cantidad = int.TryParse(txtCantidad.Text, out int c) ? c : 0;

                if (cantidad <= 0 || producto == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "cantidadInvalida",
                        "Swal.fire('Cantidad inválida', 'Debe ingresar una cantidad mayor a cero.', 'warning');", true);
                    return;
                }

                if (producto.Stock - cantidad < producto.StockMin)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "stockMin",
                        $"Swal.fire('Stock mínimo', 'No se puede agregar esa cantidad. Stock final menor al mínimo permitido.', 'warning');", true);
                    return;
                }

                var detalle = Session["VentaDetalle"] as List<DetalleVenta> ?? new List<DetalleVenta>();

                if (detalle.Any(d => d.Producto.Id == producto.Id))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "yaAgregado",
                        "Swal.fire('Ya agregado', 'Este producto ya está en el carrito.', 'info');", true);
                    return;
                }

                detalle.Add(new DetalleVenta
                {
                    Producto = producto,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio,
                    Activo = true
                });

                producto.Stock -= cantidad;
                Session["VentaDetalle"] = detalle;
                Session["Productos"] = productos;
                repCatalogo.DataSource = productos;
                repCatalogo.DataBind();

                gvDetalleCatalogo.DataSource = detalle;
                gvDetalleCatalogo.DataBind();
                decimal totalConGanancia = detalle.Sum(d =>
                {
                    decimal ganancia = d.Producto.PorcentajeGanancia ?? 0;
                    return d.PrecioUnitario * (1 + ganancia / 100) * d.Cantidad;
                });
                lblTotalCatalogo.Text = "Total: " + totalConGanancia.ToString("C2");


                ScriptManager.RegisterStartupScript(this, this.GetType(), "agregado",
                    "Swal.fire('Agregado', 'El producto se agregó correctamente.', 'success');", true);
            }
        }

        private void CargarProductos(List<Producto> filtrados = null)
        {
            List<Producto> productos = filtrados ?? new ProductoNegocio().ListarProductos().Where(p => p.Activo).ToList();
            Session["Productos"] = productos;
            repCatalogo.DataSource = productos;
            repCatalogo.DataBind();

            if (Session["VentaDetalle"] is List<DetalleVenta> detalle && detalle.Any())
            {
                gvDetalleCatalogo.DataSource = detalle;
                gvDetalleCatalogo.DataBind();
                decimal totalConGanancia = detalle.Sum(d =>
                {
                    decimal ganancia = d.Producto.PorcentajeGanancia ?? 0;
                    decimal precioFinal = d.PrecioUnitario * (1 + ganancia / 100);
                    return precioFinal * d.Cantidad;
                });
                lblTotalCatalogo.Text = "Total: " + totalConGanancia.ToString("C2");

            }

        }

        protected void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            var productos = Session["Productos"] as List<Producto> ?? new List<Producto>();

            string filtro = txtFiltroRapido.Text.ToUpper();

            var filtrados = productos.Where(p => p.Nombre.ToUpper().Contains(filtro)).ToList();
            CargarProductos(filtrados);
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();

            switch (ddlCampo.SelectedValue)
            {
                case "Precio":
                    ddlCriterio.Items.Add("Mayor a");
                    ddlCriterio.Items.Add("Menor a");
                    ddlCriterio.Items.Add("Igual a");
                    break;
                default:
                    ddlCriterio.Items.Add("Contiene");
                    ddlCriterio.Items.Add("Comienza con");
                    ddlCriterio.Items.Add("Termina con");
                    break;
            }
        }

        protected void chkFiltroAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            txtFiltroRapido.Enabled = !chkFiltroAvanzado.Checked;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (ddlCampo.SelectedValue == "0" || ddlCriterio.SelectedItem == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertFiltroIncompleto",
                    "Swal.fire('Filtro incompleto', 'Seleccioná un campo y un criterio.', 'warning');", true);
                return;
            }

            // Validación específica para precio
            if (ddlCampo.SelectedValue == "Precio")
            {
                if (string.IsNullOrWhiteSpace(txtFiltroAvanzado.Text) ||
                    !decimal.TryParse(txtFiltroAvanzado.Text, out _))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertPrecio",
                        "Swal.fire('Filtro inválido', 'El valor del precio debe ser un número.', 'warning');", true);
                    return;
                }
            }

            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                repCatalogo.DataSource = negocio.Fitrar(
                    ddlCampo.SelectedItem.ToString(),
                    ddlCriterio.SelectedItem.ToString(),
                    txtFiltroAvanzado.Text,
                    "Solo los activos"); 
                repCatalogo.DataBind();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorFiltro",
                    $"Swal.fire('Error al filtrar', '{ex.Message.Replace("'", "\\'")}', 'error');", true);
            }
        }

        protected void gvDetalleCatalogo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Quitar")
            {
                int idProducto = int.Parse(e.CommandArgument.ToString());

                var detalle = Session["VentaDetalle"] as List<DetalleVenta>;
                var productos = Session["Productos"] as List<Producto>;

                var item = detalle.FirstOrDefault(d => d.Producto.Id == idProducto);
                if (item != null)
                {
                    detalle.Remove(item);

                    Producto stockRestaurado = productos.FirstOrDefault(p => p.Id == idProducto);
                    if (stockRestaurado != null)
                        stockRestaurado.Stock += item.Cantidad;

                    Session["VentaDetalle"] = detalle;
                    Session["Productos"] = productos;

                    gvDetalleCatalogo.DataSource = detalle;
                    gvDetalleCatalogo.DataBind();
                    if (detalle.Any())
                    {
                        decimal totalConGanancia = detalle.Sum(d =>
                        {
                            decimal ganancia = d.Producto.PorcentajeGanancia ?? 0;
                            return d.PrecioUnitario * (1 + ganancia / 100) * d.Cantidad;
                        });
                        lblTotalCatalogo.Text = "Total: " + totalConGanancia.ToString("C2");
                    }
                    else
                    {
                        lblTotalCatalogo.Text = "";
                    }

                    repCatalogo.DataSource = productos;
                    repCatalogo.DataBind();
                }
            }
        }


        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Session["DesdeCatalogo"] = true;
            Response.Redirect("NuevaVenta.aspx");
        }


    }
}