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
    public partial class Prototipo : System.Web.UI.Page
    {
        public List<Producto> Productos = new List<Producto>();
        public List<Marca> lista_marcas = new List<Marca>();
        public List<TipoProducto> lista_tipos = new List<TipoProducto>();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                
                MarcaNegocio marcas = new MarcaNegocio();
                NegocioTipoProducto tipos = new NegocioTipoProducto();
                
                lista_marcas = marcas.ListarMarcas();
                lista_tipos = tipos.ListarTiposDeProductos();
                
                ddlMarcas.DataSource = lista_marcas;
                ddlMarcas.DataValueField = "Id";
                ddlMarcas.DataTextField= "Nombre";
                ddlMarcas.DataBind();

                ddlFiltroMarca.DataSource = lista_marcas;
                ddlFiltroMarca.DataValueField = "Id";
                ddlFiltroMarca.DataTextField = "Nombre";
                ddlFiltroMarca.DataBind();
                ddlFiltroMarca.Items.Insert(0, new ListItem("Todas las marcas", "0"));

                ddlTipoDeProducto.DataSource = lista_tipos;
                ddlTipoDeProducto.DataValueField = "Id";
                ddlTipoDeProducto.DataTextField = "Nombre";
                ddlTipoDeProducto.DataBind();

                ddlFiltroTipo.DataSource = lista_tipos;
                ddlFiltroTipo.DataValueField = "Id";
                ddlFiltroTipo.DataTextField = "Nombre";
                ddlFiltroTipo.DataBind();
                ddlFiltroTipo.Items.Insert(0, new ListItem("Todos los tipos", "0"));
                
                PanelFormAltaProd.Visible = false;
                PanelListarProd.Visible = false;
                PanelAgregarMarca.Visible= false;
                PanelEliminarProducto.Visible = false;



            }

            ProductoNegocio productoNegocio = new ProductoNegocio();
            Productos = productoNegocio.ListarProductos();
        }
        protected void btnListarProdClick(object sender, EventArgs e)
        {
            PanelFormAltaProd.Visible = false;
            PanelAgregarMarca.Visible = false;
            PanelEliminarProducto.Visible = false;
            PanelListarProd.Visible = true;

        }

        protected void btnAgregarProdClick(object sender, EventArgs e)
        {
            PanelListarProd.Visible = false;
            PanelAgregarMarca.Visible = false;
            PanelEliminarProducto.Visible = false;
            PanelFormAltaProd.Visible = true;
        }

        protected void btnAgregarMarcaClick(object sender, EventArgs e)
        {
            PanelListarProd.Visible = false;
            PanelFormAltaProd.Visible = false;
            PanelEliminarProducto.Visible = false;
            PanelAgregarMarca.Visible = true;
        }

        protected void btnEliminarProdClick(object sender, EventArgs e)
        {
            PanelListarProd.Visible = false;
            PanelFormAltaProd.Visible = false;
            PanelAgregarMarca.Visible = false;
            PanelEliminarProducto.Visible = true;
        }

        protected void btnVolverPanelClick(object sender, EventArgs e)
        {
            Response.Redirect("PanelCtrlAdmin.aspx");
        }


    }
}