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

            string evento = Request["__EVENTTARGET"];
            string argumento = Request["__EVENTARGUMENT"];

            if (evento == "EliminarCompra" && int.TryParse(argumento, out int idCompra))
            {
                try
                {
                    CompraNegocio negocio = new CompraNegocio();
                    negocio.OcultarCompra(idCompra);
                    CargarCompras();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                        $"Swal.fire('Error', 'No se pudo eliminar la compra: {ex.Message.Replace("'", "\\'")}', 'error');", true);
                }
            }

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

        protected void TxtFiltroRápidoCompras_TextChanged(object sender, EventArgs e)
        {

        }

        protected void CheckFiltroAvanzadoCompras_CheckedChanged(object sender, EventArgs e)
        {
            bool se_activo = CheckFiltroAvanzadoCompras.Checked;
           

            if (se_activo)
            {                
                PnlFiltroAvanzadoCompras.Visible = se_activo;
                TxtFiltroRápidoCompras.Text = "";
                TxtFiltroRápidoCompras.Enabled = !se_activo;
                TxtFiltroAvanzadoCompras.Visible = se_activo;
                

            }
            else {
                PnlFiltroAvanzadoCompras.Visible = se_activo;
                TxtFiltroAvanzadoCompras.Text = "";
                TxtFiltroRápidoCompras.Enabled = !se_activo;
                DdlCampoCompras.SelectedIndex = 0;
                DdlCriterioCompras.Items.Clear();

            }
        }

        protected void DdlCampoCompras_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnBuscarAvanzadoCompras_Click(object sender, EventArgs e)
        {

        }

        protected void BtnLimpiarAvanzadoMarca_Click(object sender, EventArgs e)
        {

        }
    }
}