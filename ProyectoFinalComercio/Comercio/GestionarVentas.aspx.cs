using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio; 

namespace Comercio
{
    public partial class GestionarVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarVentas();
        }

        private void CargarVentas()
        {
            try
            {
                VentaNegocio negocio = new VentaNegocio();
                gvVentas.DataSource = negocio.ListarVentas();
                gvVentas.DataBind();
            }
            catch (Exception ex)
            {
                // Manejo de error básico con SweetAlert
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorCarga",
                    $"Swal.fire('Error', 'No se pudieron cargar las ventas: {ex.Message.Replace("'", "\\'")}', 'error');", true);
            }
        }

        protected void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("NuevaVenta.aspx");
        }

        protected void btnActualizarListado_Click(object sender, EventArgs e)
        {
            CargarVentas();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Panel.aspx"); // o el nombre que tenga tu panel principal
        }

        protected void gvVentas_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idVenta = Convert.ToInt32(gvVentas.DataKeys[index].Value);
                Response.Redirect("NuevaVenta.aspx?id=" + idVenta);
            }
        }

    }
}