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
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario = negocio.ListarUsuarios();

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
            usuario.Rol = "Usuario";

            try
            {
                data.Agregar(usuario);
                ActualizarListas();

                // Limpiamos los campos del form :)
                limpiarCampos();

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al guardar el usuario: {ex.Message}');", true);
            }
        }

        protected void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            int idUsuario;
            if (!int.TryParse(ddlUsuarioEliminar.SelectedValue, out idUsuario) || idUsuario == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Seleccione un usuario válido.');", true);
                return;
            }

            Usuario usuario = new Usuario { Id = idUsuario };
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                negocio.EliminarUsuarioLogico(usuario);
                ActualizarListas();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al eliminar el usuario: {ex.Message}');", true);
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
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Seleccione un usuario válido.');", true);
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
                ActualizarListas();

                // Limpiamos los campos del form :)
                limpiarCampos();

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al guardar el usuario: {ex.Message}');", true);
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
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al eliminar el usuario: {ex.Message}');", true);
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
    }
}