using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class PanellCtrlAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("Default.aspx");
                    return;
                }

                var usuario = (Dominio.Usuario)Session["usuario"];

                if (usuario.Rol == "Administrador")
                {
                    PanelAdministrador.Visible = true;
                }
                else if (usuario.Rol == "Vendedor")
                {
                    PanelVendedor.Visible = true;
                }
            }

        }
    }
}