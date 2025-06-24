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
                        string nombreLogueado = usuario.Nombre;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                         $@"Swal.fire({{
                          title: 'Bienvenido {nombreLogueado}!',
                          text: 'Iniciando sesión correctamente...',
                          icon: 'success',
                          showConfirmButton: false,
                          timer: 2000
                          }}).then(() => {{
                           window.location.href = 'PanelCtrlAdmin.aspx';
                          }});", true);
                    }
                    else if (usuario.Rol == "Vendedor")
                    {
                         string nombreLogueado = usuario.Nombre;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                          $@"Swal.fire({{
                          title: 'Bienvenido {nombreLogueado}!',
                          text: 'Iniciando sesión correctamente...',
                          icon: 'success',
                          showConfirmButton: false,
                          timer: 2000
                          }}).then(() => {{
                           window.location.href = 'PanelCtrlVendedor.aspx';
                          }});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                   "Swal.fire('Ocurrió un error', 'Email y/o contraseña incorrecta!', 'error');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);
            }
        }
    }
}