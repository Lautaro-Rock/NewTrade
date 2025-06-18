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
            ProductoNegocio productoNegocio = new ProductoNegocio();
            Productos = productoNegocio.ListarProductos();

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

                ddlProductos.DataSource = Productos;
                ddlProductos.DataValueField = "Id";
                ddlProductos.DataTextField = "Nombre";
                ddlProductos.DataBind();
                ddlProductos.Items.Insert(0, new ListItem("Seleccione un producto", "0"));

            }

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

        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            ProductoNegocio data = new ProductoNegocio();
            Producto producto = new Producto();

            producto.Nombre = txtNombreProd.Text.Trim();
            producto.Marca = new Marca { Id = int.Parse(ddlMarcas.SelectedValue) };
            producto.TipoProducto = new TipoProducto { Id = int.Parse(ddlTipoDeProducto.SelectedValue) };
            producto.Precio = decimal.Parse(txtPrecio.Text.Trim());
            producto.Stock = int.Parse(txtStock.Text.Trim());
            producto.StockMin = int.Parse(txtStockMin.Text.Trim());
            producto.UrlImgProducto = txtUrlImagen.Text.Trim();
            producto.Activo = true;

            try
            {

                data.AgregarProductos(producto);

                // Limpiamos los campos del form :)
                txtNombreProd.Text = "";
                ddlMarcas.SelectedIndex = 0;
                ddlTipoDeProducto.SelectedIndex = 0;
                txtPrecio.Text = "";
                txtStock.Text = "";
                txtStockMin.Text = "";
                txtUrlImagen.Text = "";

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al guardar el producto: {ex.Message}');", true);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idProducto;
            if (!int.TryParse(ddlProductos.SelectedValue, out idProducto) || idProducto == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Seleccione un producto válido.');", true);
                return;
            }

            Producto producto = new Producto { Id = idProducto };
            ProductoNegocio negocio = new ProductoNegocio();

            try
            {
                negocio.EliminarProductoLogico(producto);

                Productos = negocio.ListarProductos();
                ddlProductos.DataSource = Productos;
                ddlProductos.DataValueField = "Id";
                ddlProductos.DataTextField = "Nombre";
                ddlProductos.DataBind();
                ddlProductos.Items.Insert(0, new ListItem("Seleccione un producto", "0"));
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al eliminar el producto: {ex.Message}');", true);
            }
        }

    }
}