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
        protected void Page_Load(object sender, EventArgs e)
        {
           NegocioProveedores negocio = new NegocioProveedores();
            Proveedores.DataSource = negocio.ListarProveedores();
            Proveedores.DataBind();
        }
    }
}