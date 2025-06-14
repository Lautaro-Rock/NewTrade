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
    public partial class GestiónDeProductos : System.Web.UI.Page
    {
        public List<Producto> Productos = new List<Producto>();

        protected void Page_Load(object sender, EventArgs e)
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            Productos = productoNegocio.ListarProductos();


        }
    }
}