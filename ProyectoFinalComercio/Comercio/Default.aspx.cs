using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class Inicial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresoVnd_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginVendedor.aspx");
        }

        protected void btnIngresoAdm_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginAdmin.aspx");
        }
    }
}