using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class LoginVendedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresarVnd_Click(object sender, EventArgs e)
        {
            string nombre = txtEmailUsuarioVnd.Text.Trim();
            string password = txtContraVnd.Text.Trim();

            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {

                bool esValido = negocio.ValidarCredenciales(nombre, password);

                if (esValido)
                {
                    Response.Redirect("PanelDeControlVendedor.aspx");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Email o contraseña incorrectos');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
        }
    }
}