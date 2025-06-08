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
    public partial class FormularioAltaVendedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio data = new UsuarioNegocio();
            Usuario usuario = new Usuario();

            usuario.Nombre = boxNombre.Text.Trim();
            usuario.Apellido = boxApellido.Text.Trim();
            usuario.Email = boxEmail.Text.Trim();
            usuario.Dni = int.Parse(boxDNI.Text.Trim());
            usuario.Password = boxPassword.Text.Trim();
            usuario.Rol = "Vendedor";

            try
            {
                
                data.Agregar(usuario);

                Response.Redirect("LoginVendedor.aspx");
            }
            catch (Exception ex)
            {

                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al registrar el usuario: {ex.Message}');", true);
            }
           
        }
    }
}