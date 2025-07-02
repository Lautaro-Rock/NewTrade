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
        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            FiltroAvanzado = checkFiltrarAvanzado.Checked;
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
            Response.Redirect("PanelCtrlAdmin.aspx");
        }

        protected void gvVentas_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idVenta = Convert.ToInt32(gvVentas.DataKeys[index].Value);

            if (e.CommandName == "Modificar")
            {
                Response.Redirect("NuevaVenta.aspx?id=" + idVenta);
            }

            if (e.CommandName == "Eliminar")
            {
                try
                {
                    new VentaNegocio().BajaLogicaVenta(idVenta);
                    CargarVentas(); // Re-lista después de anular
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ventaAnulada",
                        "Swal.fire('Anulada', 'La venta fue anulada correctamente.', 'success');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorAnular",
                        $"Swal.fire('Error', 'No se pudo anular la venta: {ex.Message.Replace("'", "\\'")}', 'error');", true);
                }
            }
        }

        protected void filtroUno_TextChanged(object sender, EventArgs e)
        {

        }

        protected void checkFiltrarAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = checkFiltrarAvanzado.Checked;
            filtroUno.Enabled = !FiltroAvanzado;
        }

        protected void ddlCampoSelectUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlCampoSelectUsuario.SelectedItem.ToString() == "Factura")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
            else if (ddlCampoSelectUsuario.SelectedItem.ToString() == "Cliente")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
        }

        protected void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            VentaNegocio user = new VentaNegocio();
            try
            {
                string campo = ddlCampoSelectUsuario.Text;
                string criterio = ddlCriterio.Text;
                string filtro = ddlFiltroAvanzado.Text.Trim();
                string estado = ddlEstado.SelectedValue;

                gvVentas.DataSource = user.FiltrarVenta(campo, criterio, filtro, estado);
                gvVentas.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw ex;
            }
        }
    }
}