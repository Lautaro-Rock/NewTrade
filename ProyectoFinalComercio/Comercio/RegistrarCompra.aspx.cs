using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Microsoft.Ajax.Utilities;
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
                    throw ex;
                }
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
                    dgvProductos.DataSource = new ProductoNegocio().ListarProductosxProveedor(id_proveedor);
                    dgvProductos.DataBind();


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
                    dgvProductos.DataSource = new ProductoNegocio().ListarProductosxProveedor(id_proveedor);
                    dgvProductos.DataBind();


                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex);
                    throw;
                }
            }

        }


    }
}