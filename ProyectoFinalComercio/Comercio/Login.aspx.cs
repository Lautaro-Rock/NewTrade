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
    public partial class Prueba : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnIngresarVnd_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario(); 
            usuario.Email = txtEmailUsuarioVnd.Text;
            usuario.Password = txtContraVnd.Text;
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {

                if (negocio.Loguear(usuario))
                {
                    Session.Add("usuario", usuario);
                    if (usuario.Rol == "Administrador")
                    {
                        Response.Redirect("PanelCtrlAdmin.aspx");
                    }
                    else if (usuario.Rol == "Vendedor")
                    {
                        Response.Redirect("PanelCtrlAdmin.aspx");
                    }
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