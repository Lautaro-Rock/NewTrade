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
                int idUsuario = ((Usuario)Session["usuario"]).Id;
                VentaNegocio negocio = new VentaNegocio();

                Ventas = negocio.FiltrarVenta("", "", "", "Activo", idUsuario);
                GvHistVentas.DataSource = Ventas;
                GvHistVentas.DataBind();
            }

        }

        private void CargarVentas()
        {
            try
            {
                int idUsuario = ((Usuario)Session["usuario"]).Id;
                VentaNegocio negocio = new VentaNegocio();

                Ventas = negocio.FiltrarVenta("", "", "", "Activo", idUsuario);
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
            
            if (e.CommandName == "Ver")
            {
                Session["ModoVisualizacion"] = true;
                Response.Redirect("NuevaVenta.aspx?id=" + idVenta);

            }

        }

        protected void filtroUno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filtro = filtroUno.Text.Trim().ToUpper();
                VentaNegocio negocio = new VentaNegocio();

                int idUsuario = ((Usuario)Session["usuario"]).Id;

                Ventas = negocio.FiltrarVenta("", "", "", "Activo", idUsuario);

                List<Venta> filtradas = Ventas.Where(v =>
                    (!string.IsNullOrEmpty(v.Cliente?.Nombre) && v.Cliente.Nombre.ToUpper().Contains(filtro)) ||
                    (!string.IsNullOrEmpty(v.Cliente?.Apellido) && v.Cliente.Apellido.ToUpper().Contains(filtro)) ||
                    (!string.IsNullOrEmpty(v.NumeroFactura) && v.NumeroFactura.ToUpper().Contains(filtro))
                ).ToList();

                GvHistVentas.DataSource = filtradas;
                GvHistVentas.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorFiltro",
                    $"Swal.fire('Error', 'No se pudo aplicar el filtro rápido: {ex.Message.Replace("'", "\\'")}', 'error');", true);
            }
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
                int idUsuario = ((Usuario)Session["usuario"]).Id;

                GvHistVentas.DataSource = user.FiltrarVenta(campo, criterio, filtro, estado, idUsuario);
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

        protected void btnLimpiarAvanzado_Click(object sender, EventArgs e)
        {
            // Limpiar controles
            ddlCampo.ClearSelection();
            ddlCriterio.Items.Clear();
            txtFiltroAvanzado.Text = "";

            // Recargar las ventas del usuario sin filtros
            int idUsuario = ((Usuario)Session["usuario"]).Id;
            VentaNegocio negocio = new VentaNegocio();

            GvHistVentas.DataSource = negocio.FiltrarVenta("", "", "", "Activo", idUsuario);
            GvHistVentas.DataBind();
        }

    }
}