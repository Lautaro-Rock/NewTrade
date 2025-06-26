using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

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
            List<Producto> lista_buscada = Productos.FindAll(p =>
            p.Nombre.ToUpper().Contains(txtBuscarProducto.Text.ToUpper()));
            dgvProductos.DataSource = lista_buscada;
            dgvProductos.DataBind();

        }

        protected void dgvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Agregar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = dgvProductos.Rows[index];

                TextBox txtCantidad = (TextBox)fila.FindControl("txtCantidad");


                float precio_total = ViewState["TotalPrecio"] != null ? (float)ViewState["TotalPrecio"] : 0;

                float precio = (float.Parse(fila.Cells[1].Text) * int.Parse(txtCantidad.Text));

                precio_total += precio;

                ViewState["TotalPrecio"] = precio_total;


                lbPrecio.Text = precio_total.ToString("C2");


            }
        }
    }
}