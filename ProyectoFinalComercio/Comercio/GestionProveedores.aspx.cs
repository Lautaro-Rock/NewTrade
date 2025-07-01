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
    public partial class GestionProveedores : System.Web.UI.Page
    {
        public List<Proveedor> Proveedor = new List<Proveedor>();

        public List<Producto> Productos
        {
            get { return Session["Productos"] as List<Producto>; }
            set { Session["Productos"] = value; }
        }

        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["usuario"] != null && ((Dominio.Usuario)Session["usuario"]).Rol == "Administrador"))
            {
                Response.Redirect("Default.aspx");
            }

            NegocioProveedores negocio = new NegocioProveedores();
            Proveedor = negocio.ListarProveedores();
            FiltroAvanzado = checkFiltrarAvanzado.Checked;
            if (!IsPostBack)
            {
                Productos = new ProductoNegocio().ListarProductos();
                ActualizarListas();
                PanelFormAltaProveedor.Visible = false;
                PanelListarProveedor.Visible = true;
                PanelEliminarProveedor.Visible = false;
            }

        }

        //Eventos relacionados a la seccion proveedores
        //
        protected void limpiarCampos()
        {
            txtNombreProveedor.Text = "";
            txtDireccionProveedor.Text = "";
            txtCUITProveedor.Text = "";
            txtEmailProveedor.Text = "";
            txtTelefonoProveedor.Text = "";
        }

        protected void btnGuardarProveedor_Click(object sender, EventArgs e)
        {
            Page.Validate("AltaProveedor");
            if (!Page.IsValid)
            {
                return;
            }

            NegocioProveedores data = new NegocioProveedores();
            Proveedor proveedor = new Proveedor();

            proveedor.RazonSocial = txtNombreProveedor.Text.Trim();
            proveedor.Direccion = txtDireccionProveedor.Text.Trim();
            proveedor.Cuit = txtCUITProveedor.Text.Trim();
            proveedor.Email = txtEmailProveedor.Text.Trim();
            proveedor.Telefono = txtTelefonoProveedor.Text.Trim();


            try
            {

                data.AgregarProveedores(proveedor);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "Swal.fire('¡Proveedor agregado!', '', 'success');", true);
                ActualizarListas();

                // Limpiamos los campos del form :)
                limpiarCampos();

            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);
            }
        }

        protected void btnEliminarProveedor_Click(object sender, EventArgs e)
        {
            int idProveedor;
            if (!int.TryParse(ddlProveedorEliminar.SelectedValue, out idProveedor) || idProveedor == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('Ocurrió un error', 'Seleccione un proveedor valido!', 'error');", true);
                return;
            }

            Proveedor proveedor = new Proveedor { Id = idProveedor };
            NegocioProveedores negocio = new NegocioProveedores();

            try
            {
                negocio.EliminarProveedoresLogico(proveedor);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "Swal.fire('¡Proveedor eliminado correctamente!', '', 'success');", true);
                ActualizarListas();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);
            }
        }
        protected void btnModificarProveedor_Click(object sender, EventArgs e)
        {
            Page.Validate("AltaProveedor");
            if (!Page.IsValid)
            {
                return;
            }

            int idProveedor;
            // Validamos que se haya seleccionado un proveedor
            if (!int.TryParse(ddlProveedorModificar.SelectedValue, out idProveedor) || idProveedor == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('Ocurrió un error', 'Seleccione un proveedor valido!', 'error');", true);
                return;
            }

            Proveedor proveedor = new Proveedor { Id = idProveedor };
            NegocioProveedores data = new NegocioProveedores();

            proveedor.RazonSocial = txtNombreProveedor.Text.Trim();
            proveedor.Direccion = txtDireccionProveedor.Text.Trim();
            proveedor.Cuit = txtCUITProveedor.Text.Trim();
            proveedor.Email = txtEmailProveedor.Text.Trim();
            proveedor.Telefono = txtTelefonoProveedor.Text.Trim();

            try
            {

                data.ModificarProveedores(proveedor);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('¡Proveedor modificado correctamente!', '', 'success');", true);
                ActualizarListas();

                // Limpiamos los campos del form :)

                limpiarCampos();


            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);
            }
        }


        private void ActualizarListas()
        {
            // Actualizar proveedores
            NegocioProveedores negocio = new NegocioProveedores();
            Proveedor = negocio.ListarProveedores();
            ddlProveedorEliminar.DataSource = Proveedor;
            ddlProveedorEliminar.DataValueField = "Id";
            ddlProveedorEliminar.DataTextField = "RazonSocial";
            ddlProveedorEliminar.DataBind();
            ddlProveedorEliminar.Items.Insert(0, new ListItem("Seleccione un proveedor", "0"));

            ddlProveedorModificar.DataSource = Proveedor;
            ddlProveedorModificar.DataValueField = "Id";
            ddlProveedorModificar.DataTextField = "RazonSocial";
            ddlProveedorModificar.DataBind();
            ddlProveedorModificar.Items.Insert(0, new ListItem("Seleccione un proveedor", "0"));

            rptProveedores.DataSource = Proveedor;
            rptProveedores.DataBind();

        }

        //Eventos relacionados a la seccion General
        protected void btnVolverPanelClick(object sender, EventArgs e)
        {
            Response.Redirect("PanelCtrlAdmin.aspx");
        }

        protected void btnListarProveedor_Click(object sender, EventArgs e)
        {
            PanelFormAltaProveedor.Visible = false;
            PanelEliminarProveedor.Visible = false;
            PanelListarProveedor.Visible = true;
            ActualizarListas();
        }

        protected void btnEliminarProveedorPanel_Click(object sender, EventArgs e)
        {
            PanelListarProveedor.Visible = false;
            PanelFormAltaProveedor.Visible = false;
            PanelEliminarProveedor.Visible = true;
            ActualizarListas();
        }

        protected void btnModificarProveedorPanel_Click(object sender, EventArgs e)
        {
            PanelListarProveedor.Visible = false;
            PanelEliminarProveedor.Visible = false;
            PanelFormAltaProveedor.Visible = true;

            lblTituloAgregarProveedor.Visible = false;
            lblTituloModificarProveedor.Visible = true;
            btnGuardarProveedor.Visible = false;
            btnModificarProveedor.Visible = true;
            divProveedorModificar.Visible = true;
            ActualizarListas();
        }

        protected void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            PanelListarProveedor.Visible = false;
            PanelEliminarProveedor.Visible = false;
            PanelFormAltaProveedor.Visible = true;

            lblTituloAgregarProveedor.Visible = true;
            lblTituloModificarProveedor.Visible = false;
            btnGuardarProveedor.Visible = true;
            btnModificarProveedor.Visible = false;
            divProveedorModificar.Visible = false;
            limpiarCampos();
            ActualizarListas();
        }

        protected void ddlProveedorModificar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProveedor;
            if (int.TryParse(ddlProveedorModificar.SelectedValue, out idProveedor) && idProveedor > 0)
            {
                Proveedor proveedor = Proveedor.FirstOrDefault(p => p.Id == idProveedor);
                if (proveedor != null)
                {

                    txtNombreProveedor.Text = proveedor.RazonSocial;
                    txtDireccionProveedor.Text = proveedor.Direccion;
                    txtCUITProveedor.Text = proveedor.Cuit;
                    txtEmailProveedor.Text = proveedor.Email;
                    txtTelefonoProveedor.Text = proveedor.Telefono;
                }
            }
            else
            {
                // Limpiar campos si no hay proveedor seleccionado
                limpiarCampos();
            }
        }

        protected void btnEliminarProveedorListado_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int idProveedor;
            if (int.TryParse(btn.CommandArgument, out idProveedor))
            {
                NegocioProveedores negocio = new NegocioProveedores();
                Proveedor proveedor = new Proveedor { Id = idProveedor };
                try
                {
                    negocio.EliminarProveedoresLogico(proveedor);
                    ActualizarListas();
                }
                catch (Exception ex)
                {
                    string mensaje = ex.Message.Replace("'", "\\'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);
                }
            }
        }

        protected void btnModificarProveedorListado_Click(object sender, EventArgs e)
        {
            PanelListarProveedor.Visible = false;
            PanelEliminarProveedor.Visible = false;
            PanelFormAltaProveedor.Visible = true;

            lblTituloAgregarProveedor.Visible = false;
            lblTituloModificarProveedor.Visible = true;
            btnGuardarProveedor.Visible = false;
            btnModificarProveedor.Visible = true;
            divProveedorModificar.Visible = true;

            var btn = (Button)sender;
            int idProveedor;

            if (int.TryParse(btn.CommandArgument, out idProveedor) && idProveedor > 0)
            {
                Proveedor proveedor = Proveedor.FirstOrDefault(p => p.Id == idProveedor);
                if (proveedor != null)
                {
                    txtNombreProveedor.Text = proveedor.RazonSocial;
                    txtDireccionProveedor.Text = proveedor.Direccion;
                    txtCUITProveedor.Text = proveedor.Cuit;
                    txtEmailProveedor.Text = proveedor.Email;
                    txtTelefonoProveedor.Text = proveedor.Telefono;
                    // Actualizar el dropdown para modificar
                    ddlProveedorModificar.SelectedValue = proveedor.Id.ToString();
                }
            }
            else
            {
                // Limpiar campos si no hay proveedor seleccionado
                limpiarCampos();
            }
        }

        protected void checkFiltrarAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = checkFiltrarAvanzado.Checked;
            filtroUno.Enabled = !FiltroAvanzado;
        }
        protected void filtroUno_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCampoSelectUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlCampoSelectUsuario.SelectedItem.ToString() == "Razon Social")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
            else if (ddlCampoSelectUsuario.SelectedItem.ToString() == "Cuit")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
            else if (ddlCampoSelectUsuario.SelectedItem.ToString() == "Email")
            {
                ddlCriterio.Items.Add("Igual a");
            }
            else if (ddlCampoSelectUsuario.SelectedItem.ToString() == "Direccion")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
        }


        protected void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            {
                NegocioProveedores prov = new NegocioProveedores();
                try
                {
                    string campo = ddlCampoSelectUsuario.Text;
                    string criterio = ddlCriterio.Text;
                    string filtro = ddlFiltroAvanzado.Text.Trim();
                    string estado = ddlEstado.SelectedValue;

                    rptProveedores.DataSource = prov.FiltrarProveedores(campo, criterio, filtro, estado);
                    rptProveedores.DataBind();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex);
                    throw ex;
                }
            }
        }

        protected void txtFiltroProducto_TextChanged(object sender, EventArgs e)
        {
            List<Producto> lista_rap_prov = Productos.FindAll(p =>
            p.Nombre.ToUpper().Contains(txtFiltroProducto.Text.ToUpper()));
            chkProductos.DataSource = lista_rap_prov;
            chkProductos.DataValueField = "Id";
            chkProductos.DataTextField = "Nombre";
            chkProductos.DataBind();

        }
    }
}
