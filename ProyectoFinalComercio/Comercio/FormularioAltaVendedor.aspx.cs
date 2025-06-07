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

            usuario.nombre = boxNombre.Text.Trim();
            usuario.apellido = boxApellido.Text.Trim();
            usuario.email = boxEmail.Text.Trim();
            usuario.dni = int.Parse(boxDNI.Text.Trim());
            usuario.password = boxPassword.Text.Trim();
            usuario.rol = "Vendedor";

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