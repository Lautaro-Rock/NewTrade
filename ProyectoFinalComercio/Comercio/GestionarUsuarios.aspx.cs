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
    public partial class GestionarUsuarios : System.Web.UI.Page
    {
        public List<Usuario> Usuario = new List<Usuario>();

       public bool FiltroAvanzado { get; set;}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["usuario"] != null && ((Dominio.Usuario)Session["usuario"]).Rol == "Administrador"))
            {
                Response.Redirect("Default.aspx");
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario = negocio.ListarUsuarios();
            FiltroAvanzado = checkFiltrarAvanzado.Checked;
            if (!IsPostBack)
            {
                ActualizarListas();
                PanelFormAltaUsuario.Visible = false;
                PanelListarUsuario.Visible = true;
                PanelEliminarUsuario.Visible = false;
            }

        }

        //Eventos relacionados a la seccion usuarios
        //
        protected void limpiarCampos()
        {
            txtNombreUsuario.Text = "";
            txtApellidoUsuario.Text = "";
            txtDNIUsuario.Text = "";
            txtEmailUsuario.Text = "";
            txtContraUsuario.Text = "";
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            Page.Validate("AltaUsuario");
            if (!Page.IsValid)
            {
                return;
            }

            UsuarioNegocio data = new UsuarioNegocio();
            Usuario usuario = new Usuario();

            usuario.Nombre = txtNombreUsuario.Text.Trim();
            usuario.Apellido = txtApellidoUsuario.Text.Trim();
            usuario.Dni = int.Parse(txtDNIUsuario.Text.Trim());
            usuario.Email = txtEmailUsuario.Text.Trim();
            usuario.Password = txtContraUsuario.Text.Trim(); 
            usuario.Rol = "Vendedor";

            try
            {
                data.Agregar(usuario);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('¡Nuevo usuario agregado!', '', 'success');", true);
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

        protected void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            int idUsuario;
            if (!int.TryParse(ddlUsuarioEliminar.SelectedValue, out idUsuario) || idUsuario == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('Ocurrió un error', 'Seleccione un usuario valido!', 'error');", true);
                return;
            }

            Usuario usuario = new Usuario { Id = idUsuario };
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                negocio.EliminarUsuarioLogico(usuario);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('¡Usuario dado de baja correctamente!', '', 'success');", true);
                ActualizarListas();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);
            }
        }
        protected void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            Page.Validate("AltaUsuario");
            if (!Page.IsValid)
            {
                return;
            }

            int idUsuario;
            // Validamos que se haya seleccionado un usuario
            if (!int.TryParse(ddlUsuarioModificar.SelectedValue, out idUsuario) || idUsuario == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "Swal.fire('Ocurrió un error', 'Seleccione un usuario valido!', 'error');", true);
                return;
            }

            Usuario usuario = new Usuario { Id = idUsuario };
            UsuarioNegocio data = new UsuarioNegocio();

            usuario.Nombre = txtNombreUsuario.Text.Trim();
            usuario.Apellido = txtApellidoUsuario.Text.Trim();
            usuario.Dni = int.Parse(txtDNIUsuario.Text.Trim());
            usuario.Password = txtContraUsuario.Text.Trim(); 
            usuario.Email = txtEmailUsuario.Text.Trim();

            try
            {
                data.EditarUsuario(usuario);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('¡Usuario editado correctamente!', '', 'success');", true);
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
            // Actualizar usuarios
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario = negocio.ListarUsuarios();
            ddlUsuarioEliminar.DataSource = Usuario;
            ddlUsuarioEliminar.DataValueField = "Id";
            ddlUsuarioEliminar.DataTextField = "Nombre";
            ddlUsuarioEliminar.DataBind();
            ddlUsuarioEliminar.Items.Insert(0, new ListItem("Seleccione un usuario", "0"));

            ddlUsuarioModificar.DataSource = Usuario;
            ddlUsuarioModificar.DataValueField = "Id";
            ddlUsuarioModificar.DataTextField = "Nombre";
            ddlUsuarioModificar.DataBind();
            ddlUsuarioModificar.Items.Insert(0, new ListItem("Seleccione un usuario", "0"));

            rptUsuarios.DataSource = Usuario;
            rptUsuarios.DataBind();

        }

        //Eventos relacionados a la seccion General
        protected void btnVolverPanelClick(object sender, EventArgs e)
        {
            Response.Redirect("PanelCtrlAdmin.aspx");
        }

        protected void btnListarUsuario_Click(object sender, EventArgs e)
        {
            PanelFormAltaUsuario.Visible = false;
            PanelEliminarUsuario.Visible = false;
            PanelListarUsuario.Visible = true;
            ActualizarListas();
        }

        protected void btnEliminarUsuarioPanel_Click(object sender, EventArgs e)
        {
            PanelListarUsuario.Visible = false;
            PanelFormAltaUsuario.Visible = false;
            PanelEliminarUsuario.Visible = true;
            ActualizarListas();
        }

        protected void btnModificarUsuarioPanel_Click(object sender, EventArgs e)
        {
            PanelListarUsuario.Visible = false;
            PanelEliminarUsuario.Visible = false;
            PanelFormAltaUsuario.Visible = true;

            lblTituloAgregarUsuario.Visible = false;
            lblTituloModificarUsuario.Visible = true;
            btnGuardarUsuario.Visible = false;
            btnModificarUsuario.Visible = true;
            divUsuarioModificar.Visible = true;
            limpiarCampos();
            ActualizarListas();
        }

        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            PanelListarUsuario.Visible = false;
            PanelEliminarUsuario.Visible = false;
            PanelFormAltaUsuario.Visible = true;

            lblTituloAgregarUsuario.Visible = true;
            lblTituloModificarUsuario.Visible = false;
            btnGuardarUsuario.Visible = true;
            btnModificarUsuario.Visible = false;
            divUsuarioModificar.Visible = false;
            limpiarCampos();
            ActualizarListas();
        }

        protected void ddlUsuarioModificar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idUsuario;
            if (int.TryParse(ddlUsuarioModificar.SelectedValue, out idUsuario) && idUsuario > 0)
            {
                Usuario usuario = Usuario.FirstOrDefault(p => p.Id == idUsuario);
                if (usuario != null)
                {
                    txtNombreUsuario.Text = usuario.Nombre;
                    txtApellidoUsuario.Text = usuario.Apellido;
                    txtDNIUsuario.Text = usuario.Dni.ToString();
                    txtEmailUsuario.Text = usuario.Email;
                    txtContraUsuario.Text = usuario.Password; 
                }
            }
            else
            {
                // Limpiar campos si no hay usuario seleccionado
                limpiarCampos();
            }
        }

        protected void btnEliminarUsuarioListado_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int idUsuario;
            if (int.TryParse(btn.CommandArgument, out idUsuario))
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuario = new Usuario { Id = idUsuario };
                try
                {
                    negocio.EliminarUsuarioLogico(usuario);
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

        protected void btnModificarUsuarioListado_Click(object sender, EventArgs e)
        {
            PanelListarUsuario.Visible = false;
            PanelEliminarUsuario.Visible = false;
            PanelFormAltaUsuario.Visible = true;

            lblTituloAgregarUsuario.Visible = false;
            lblTituloModificarUsuario.Visible = true;
            btnGuardarUsuario.Visible = false;
            btnModificarUsuario.Visible = true;
            divUsuarioModificar.Visible = true;

            var btn = (Button)sender;
            int idUsuario;

            if (int.TryParse(btn.CommandArgument, out idUsuario) && idUsuario > 0)
            {
                Usuario usuario = Usuario.FirstOrDefault(p => p.Id == idUsuario);
                if (usuario != null)
                {
                    txtNombreUsuario.Text = usuario.Nombre;
                    txtApellidoUsuario.Text = usuario.Apellido;
                    txtDNIUsuario.Text = usuario.Dni.ToString();
                    txtEmailUsuario.Text = usuario.Email;
                    txtContraUsuario.Text = usuario.Password;
                    // Actualizar el dropdown para modificar
                    ddlUsuarioModificar.SelectedValue = usuario.Id.ToString();
                }
            }
            else
            {
                // Limpiar campos si no hay usuario seleccionado
                limpiarCampos();
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
            if (ddlCampoSelectUsuario.SelectedItem.ToString() == "DNI")
            {
                ddlCriterio.Items.Add("Igual a");
            }
            else if (ddlCampoSelectUsuario.SelectedItem.ToString() == "Nombre")
            {
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
            else if (ddlCampoSelectUsuario.SelectedItem.ToString() == "Apellido")
            {
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
            else if (ddlCampoSelectUsuario.SelectedItem.ToString() == "Email")
            {
                ddlCriterio.Items.Add("Igual a");
            }
        }

        protected void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            UsuarioNegocio user = new UsuarioNegocio();
            try
            {
                string campo = ddlCampoSelectUsuario.Text;
                string criterio = ddlCriterio.Text;
                string filtro = ddlFiltroAvanzado.Text.Trim();
                string estado = ddlEstado.SelectedValue;

                rptUsuarios.DataSource = user.FiltrarUsuario(campo, criterio, filtro, estado);
                rptUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw ex;
            }
            
        }
    }
}