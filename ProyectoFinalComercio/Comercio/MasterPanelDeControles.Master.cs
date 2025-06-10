using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class MasterPanelDeControles : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Simulación para pruebas 
            if (Session["TipoUsuario"] == null)
            {
                Session["TipoUsuario"] = "vendedor"; // o "admin"
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            string tipoUsuario = Session["TipoUsuario"]?.ToString();
            if (!string.IsNullOrEmpty(tipoUsuario))
            {
                if (tipoUsuario == "admin")
                    bodyTag.Attributes["class"] = "body-admin";
                else if (tipoUsuario == "vendedor")
                    bodyTag.Attributes["class"] = "body-vendedor";
            }
        }
    }
}