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
            if (!(Session["usuario"] != null && ((Dominio.Usuario)Session["usuario"]).Rol == "Administrador"))
            {
                Response.Redirect("Default.aspx");
            }

            FiltroAvanzado = checkFiltrarAvanzado.Checked;
            if (!IsPostBack)
                CargarVentas();

            string evento = Request["__EVENTTARGET"];
            string argumento = Request["__EVENTARGUMENT"];

            if (evento == "EliminarVenta" && int.TryParse(argumento, out int idVenta))
            {
                try
                {
                    VentaNegocio negocio = new VentaNegocio();
                    negocio.OcultarVenta(idVenta);
                    CargarVentas(); 
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                        $"Swal.fire('Error', 'No se pudo eliminar la venta: {ex.Message.Replace("'", "\\'")}', 'error');", true);
                }
            }
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

            /* if (e.CommandName == "Eliminar") NO EN USO POR AHORA
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
            */
        }

        protected void filtroUno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filtro = filtroUno.Text.Trim().ToUpper();
                VentaNegocio negocio = new VentaNegocio();

                List<Venta> ventas = negocio.ListarVentas();
                List<Venta> filtradas = ventas.Where(v =>
                    (!string.IsNullOrEmpty(v.Cliente?.Nombre) && v.Cliente.Nombre.ToUpper().Contains(filtro)) ||
                    (!string.IsNullOrEmpty(v.Cliente?.Apellido) && v.Cliente.Apellido.ToUpper().Contains(filtro)) ||
                    (!string.IsNullOrEmpty(v.NumeroFactura) && v.NumeroFactura.ToUpper().Contains(filtro))
                ).ToList();

                gvVentas.DataSource = filtradas;
                gvVentas.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorFiltro",
                    $"Swal.fire('Error', 'No se pudo aplicar el filtro rápido: {ex.Message.Replace("'", "\\'")}', 'error');", true);
            }

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

        protected void btnBuscarVenta_Click(object sender, EventArgs e)
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

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            ddlCampoSelectUsuario.SelectedIndex = 0;
            ddlCriterio.Items.Clear();
            ddlFiltroAvanzado.Text = "";
            ddlEstado.SelectedIndex = 0;

            checkFiltrarAvanzado.Checked = false;
            filtroUno.Text = "";
            filtroUno.Enabled = true;

            VentaNegocio user = new VentaNegocio();
            gvVentas.DataSource = user.ListarVentas();
            gvVentas.DataBind();
        }
    }
}