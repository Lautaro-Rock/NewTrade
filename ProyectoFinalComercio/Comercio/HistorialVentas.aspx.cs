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
    public partial class HistorialVentas : System.Web.UI.Page
    {

        public List<Venta> Ventas
        {
            get { return Session["Ventas"] as List<Venta>; }
            set { Session["Ventas"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Ventas = new VentaNegocio().ListarVentas();
                GvHistVentas.DataSource = Ventas;
                GvHistVentas.DataBind();
            }

        }

        private void CargarVentas()
        {
            try
            {
                Ventas = new VentaNegocio().ListarVentas();
                GvHistVentas.DataSource = Ventas;
                GvHistVentas.DataBind();

            }
            catch (Exception ex)
            {
                // Manejo de error básico con SweetAlert
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorCarga",
                    $"Swal.fire('Error', 'No se pudieron cargar las ventas: {ex.Message.Replace("'", "\\'")}', 'error');", true);
            }
        }


        protected void GvHistVentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int idVenta = Convert.ToInt32(e.CommandArgument);
            
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

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {

        }

        

        protected void filtroUno_TextChanged(object sender, EventArgs e)
        {

        }



        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlCampo.SelectedItem.ToString() == "Factura")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
            else if (ddlCampo.SelectedItem.ToString() == "Cliente")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
        }

        protected void btnBuscarAvanzado_Click(object sender, EventArgs e)
        {
            VentaNegocio user = new VentaNegocio();
            try
            {
                string campo = ddlCampo.Text;
                string criterio = ddlCriterio.Text;
                string filtro = txtFiltroAvanzado.Text.Trim();
                string estado = "Activo";

                GvHistVentas.DataSource = user.FiltrarVenta(campo, criterio, filtro, estado);
                GvHistVentas.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw ex;
            }
        }

        protected void checkFiltrarAvanzado_CheckedChanged(object sender, EventArgs e)
        {             
            PnlFiltroAvanzado.Visible = checkFiltrarAvanzado.Checked;
        }

        protected void btnVolverPanel_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelCtrlAdmin.aspx");
        }
    }
}