using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class GestionCompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarCompras();
        }

        private void CargarCompras()
        {
            try
            {
                CompraNegocio negocio = new CompraNegocio();
                gvCompras.DataSource = negocio.ListarCompras();
                gvCompras.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorCarga",
                    $"Swal.fire('Error', 'No se pudieron cargar las compras: {ex.Message.Replace("'", "\\'")}', 'error');", true);
            }
        }

        protected void btnNuevaCompra_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarCompra.aspx");
        }

        protected void gvCompras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idCompra = Convert.ToInt32(gvCompras.DataKeys[index].Value);

            if (e.CommandName == "Ver")
            {
                Response.Redirect("RegistrarCompra.aspx?id=" + idCompra);
            }
        }

        protected void btnActualizarListado_Click(object sender, EventArgs e)
        {
            CargarCompras();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelCtrlAdmin.aspx");
        }


    }
}