using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Dominio;
using Negocio;

namespace Comercio
{
    public partial class RegistrarCompra : System.Web.UI.Page
    {
        public List<Proveedor> Proveedores
        {
            get { return Session["Proveedores"] as List<Proveedor>; }
            set { Session["Proveedores"] = value; }
        }

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
                    Proveedores = new NegocioProveedores().ListarProveedores();

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorInit",
                        $"Swal.fire('Error', 'No se pudieron cargar los proveedores: {ex.Message.Replace("'", "\\'")}', 'error');", true);
                }

            }
            else
            {
                if (Session["Productos"] != null)
                    Productos = Session["Productos"] as List<Producto>;
            }
        }

        protected void ddlCampoProv_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterioProv.Items.Clear();
            switch (ddlCampoProv.SelectedItem.Text)
            {
                case "Por Razón social":
                case "Por Teléfono":
                case "Por Dirección":
                    ddlCriterioProv.Items.Add("Contiene");
                    ddlCriterioProv.Items.Add("Comienza con");
                    ddlCriterioProv.Items.Add("Termina con");
                    ddlCriterioProv.Items.Add("Es igual a");
                    break;

                case "Por Email":
                    ddlCriterioProv.Items.Add("Contiene");
                    ddlCriterioProv.Items.Add("Comienza con");
                    ddlCriterioProv.Items.Add("Termina con");
                    ddlCriterioProv.Items.Add("Es igual a");
                    ddlCriterioProv.Items.Add("Dominio es");
                    break;

                case "Por CUIT":
                    ddlCriterioProv.Items.Add("Es igual a");
                    ddlCriterioProv.Items.Add("Comienza con");
                    ddlCriterioProv.Items.Add("Contiene");
                    break;

            }

        }

        protected void txtResultBusqRap_TextChanged(object sender, EventArgs e)
        {


            List<Proveedor> lista_rapida = Proveedores.FindAll(p => p.RazonSocial.ToUpper().Contains(txtResultBusqRap.Text.ToUpper()));
            gvProveedores.DataSource = lista_rapida;
            gvProveedores.DataBind();
        }

        protected void gvProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Este")
            {
                try
                {
                    int id_proveedor = Convert.ToInt32(e.CommandArgument);
                    Button btn = (Button)e.CommandSource;
                    GridViewRow fila = (GridViewRow)btn.NamingContainer;
                    txtProveedorSeleccionado.Text = fila.Cells[0].Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CerrarCollapseRapido", @"
                 var myCollapse = document.getElementById('collapseFiltroRapidoProveedor');
                 if (myCollapse) {
                 var bsCollapse = bootstrap.Collapse.getOrCreateInstance(myCollapse);
                 bsCollapse.hide();
                 }
                 ", true);
                    txtResultBusqRap.Text = "";
                    gvProveedores.DataSource = null;
                    gvProveedores.DataBind();
                    Productos = new ProductoNegocio().ListarProductosxProveedor(id_proveedor);
                    dgvProductos.DataSource = Productos;
                    dgvProductos.DataBind();

                    Session["ProveedorSeleccionado"] = id_proveedor;

                    // Si se cambia de proveedor, reseteamos el carrito
                    Session["CompraDetalle"] = new List<DetalleCompra>();
                    gvDetalleCompra.DataSource = null;
                    gvDetalleCompra.DataBind();
                    lbPrecio.Text = "$0.00";


                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex);
                    throw;
                }

            }
        }

        protected void btnBuscarProveedor_avanzado_Click(object sender, EventArgs e)
        {
            try
            {
                gvProvFiltroAvanzado.DataSource = new NegocioProveedores().Fitrar(ddlCampoProv.SelectedValue, ddlCriterioProv.SelectedItem.Text, txtFiltro.Text, DdlEstado.SelectedItem.Text);
                gvProvFiltroAvanzado.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                throw;
            }
        }

        protected void gvProvFiltroAvanzado_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Este")
            {
                try
                {
                    int id_proveedor = Convert.ToInt32(e.CommandArgument);
                    GridViewRow fila = ((Control)e.CommandSource).NamingContainer as GridViewRow;
                    txtProveedorSeleccionado.Text = fila.Cells[0].Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CerrarCollapseRapido", @"
                 var myCollapse = document.getElementById('collapseFiltroAvanzadoProveedor');
                 if (myCollapse) {
                 var bsCollapse = bootstrap.Collapse.getOrCreateInstance(myCollapse);
                 bsCollapse.hide();
                 }
                 ", true);
                    gvProvFiltroAvanzado.DataSource = null;
                    gvProvFiltroAvanzado.DataBind();
                    txtFiltro.Text = "";
                    ddlCampoProv.SelectedIndex = 0; 
                    DdlEstado.SelectedIndex = 0;
                    Productos = new ProductoNegocio().ListarProductosxProveedor(id_proveedor);
                    dgvProductos.DataSource = Productos;
                    dgvProductos.DataBind();

                    Session["ProveedorSeleccionado"] = id_proveedor;

                    // Si se cambia de proveedor, reseteamos el carrito
                    Session["CompraDetalle"] = new List<DetalleCompra>();
                    gvDetalleCompra.DataSource = null;
                    gvDetalleCompra.DataBind();
                    lbPrecio.Text = "$0.00";



                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex);
                    throw;
                }
            }

        }

        protected void dgvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Agregar")
            {
                GridViewRow fila = ((Control)e.CommandSource).NamingContainer as GridViewRow;
                int rowIndex = fila.RowIndex;
                int idProducto = (int)dgvProductos.DataKeys[rowIndex].Value;

                TextBox txtCantidad = fila.FindControl("txtCantidad") as TextBox;

                if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "cantidadInvalida",
                        "Swal.fire('Cantidad inválida', 'Debe ingresar una cantidad mayor a cero.', 'warning');", true);
                    return;
                }

                List<Producto> productos = Session["Productos"] as List<Producto>;
                Producto producto = Productos.FirstOrDefault(p => p.Id == idProducto);
                if (producto == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "productoNoEncontrado",
                        "Swal.fire('Error', 'No se encontró el producto en sesión.', 'error');", true);
                    return;
                }


                List<DetalleCompra> detalle = Session["CompraDetalle"] as List<DetalleCompra> ?? new List<DetalleCompra>();

                if (detalle.Any(d => d.Producto.Id == producto.Id))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "productoRepetido",
                        "Swal.fire('Ya agregado', 'Este producto ya fue agregado a la compra.', 'info');", true);
                    return;
                }

                // Sumamos el stock en memoria
                producto.Stock += cantidad;
                Productos = productos;

                DetalleCompra nuevo = new DetalleCompra
                {
                    Producto = producto,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio, // o podrías permitir ingresar precio de compra
                    Activo = true
                };

                detalle.Add(nuevo);
                Session["CompraDetalle"] = detalle;

                // Pintar grilla detalle
                gvDetalleCompra.DataSource = detalle;
                gvDetalleCompra.DataBind();
                lbPrecio.Text = detalle.Sum(d => d.Subtotal).ToString("C2");

                // Refrescar tabla de productos
                dgvProductos.DataSource = productos;
                dgvProductos.DataBind();
            }

        }

        protected void btnVaciarDetalleCompra_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
         
        }



    }
}