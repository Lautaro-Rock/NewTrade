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
            try
            {
                string filtro = TxtFiltroRápidoCompras.Text.Trim().ToLower();

                if (string.IsNullOrEmpty(filtro))
                {
                    CargarCompras();
                    return;
                }

                var todas = new CompraNegocio().ListarCompras();

                var filtradas = todas.Where(c =>
                    c.Proveedor.RazonSocial.ToLower().Contains(filtro) ||
                    c.Usuario.Nombre.ToLower().Contains(filtro)
                ).ToList();

                gvCompras.DataSource = filtradas;
                gvCompras.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorFiltroRapido",
                    $"Swal.fire('Error', 'No se pudo aplicar el filtro rápido: {ex.Message.Replace("'", "\\'")}', 'error');", true);
            }

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
            DdlCriterioCompras.Items.Clear();
            string campo = DdlCampoCompras.SelectedItem.Text;

            if (campo == "Por Fecha")
            {
                DdlCriterioCompras.Items.Add("Contiene");
            }
            else if (campo == "Por Total")
            {
                DdlCriterioCompras.Items.Add("Mayor a");
                DdlCriterioCompras.Items.Add("Menor a");
                DdlCriterioCompras.Items.Add("Igual a");
            }
            else
            {
                DdlCriterioCompras.Items.Add("Contiene");
                DdlCriterioCompras.Items.Add("Comienza con");
                DdlCriterioCompras.Items.Add("Termina con");
                DdlCriterioCompras.Items.Add("Es igual a");
            }

        }

        protected void BtnBuscarAvanzadoCompras_Click(object sender, EventArgs e)
        {
            try
            {
                string campo = DdlCampoCompras.SelectedItem.Text;

                if (string.IsNullOrEmpty(campo) || campo == "--> Seleccione un campo <--")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "campoInvalido",
                        "Swal.fire('Campo no válido', 'Por favor seleccioná un campo para aplicar el filtro.', 'warning');", true);
                    return;
                }


                string criterio = DdlCriterioCompras.SelectedItem.Text;
                string filtro = TxtFiltroAvanzadoCompras.Text;
                string estadoFiltro = ddlEstadoCompra.SelectedValue;

                var resultados = new CompraNegocio().FiltrarCompras(campo, criterio, filtro, estadoFiltro);

                gvCompras.DataSource = resultados;
                gvCompras.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorFiltro",
                    $"Swal.fire('Error al filtrar', '{ex.Message.Replace("'", "\\'")}', 'error');", true);
                return; 
            }

        }

        protected void BtnLimpiarAvanzadoMarca_Click(object sender, EventArgs e)
        {
            TxtFiltroAvanzadoCompras.Text = "";
            DdlCampoCompras.SelectedIndex = 0;
            DdlCriterioCompras.Items.Clear();
            PnlFiltroAvanzadoCompras.Visible = false;
            CheckFiltroAvanzadoCompras.Checked = false;
            TxtFiltroRápidoCompras.Enabled = true;
            CargarCompras(); 

        }
    }
}