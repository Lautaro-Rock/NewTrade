using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

            lblTituloAgregar.Visible = true;
            lblTituloModificar.Visible = false;
            btnGuardarProducto.Visible = true;
            btnModificarProducto.Visible = false;
            divProductoModificar.Visible = false;

            // Limpiar campos
            //txtNombreProd.Text = "";
            //ddlMarcas.SelectedIndex = 0;
            //ddlTipoDeProducto.SelectedIndex = 0;
            //txtPrecio.Text = "";
            //txtStock.Text = "";
            //txtStockMin.Text = "";
            //txtUrlImagen.Text = "";
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

                // [JLS] Mejorar: Se deberían actualizar los productos disponibles para eliminar en el dropdown
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al eliminar el producto: {ex.Message}');", true);
            }
        }

        protected void btnModificarProd_Click(object sender, EventArgs e)
        {
            PanelListarProd.Visible = false;
            PanelAgregarMarca.Visible = false;
            PanelEliminarProducto.Visible = false;
            PanelFormAltaProd.Visible = true;

            lblTituloAgregar.Visible = false;
            lblTituloModificar.Visible = true;
            btnGuardarProducto.Visible = false;
            btnModificarProducto.Visible = true;
            divProductoModificar.Visible = true;

            // Cargar productos en el dropdown
            ddlProductoModificar.DataSource = Productos;
            ddlProductoModificar.DataValueField = "Id";
            ddlProductoModificar.DataTextField = "Nombre";
            ddlProductoModificar.DataBind();
            ddlProductoModificar.Items.Insert(0, new ListItem("Seleccione un producto", "0"));

            // Limpiar campos
            //txtNombreProd.Text = "";
            //ddlMarcas.SelectedIndex = 0;
            //ddlTipoDeProducto.SelectedIndex = 0;
            //txtPrecio.Text = "";
            //txtStock.Text = "";
            //txtStockMin.Text = "";
            //txtUrlImagen.Text = "";
        }

        protected void ddlProductoModificar_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Recargamos las listas de marcas y tipos de producto si es necesario
            if (lista_marcas == null || lista_marcas.Count == 0)
            {
                MarcaNegocio marcas = new MarcaNegocio();
                lista_marcas = marcas.ListarMarcas();
                ddlMarcas.DataSource = lista_marcas;
                ddlMarcas.DataValueField = "Id";
                ddlMarcas.DataTextField = "Nombre";
                ddlMarcas.DataBind();
            }
            if (lista_tipos == null || lista_tipos.Count == 0)
            {
                NegocioTipoProducto tipos = new NegocioTipoProducto();
                lista_tipos = tipos.ListarTiposDeProductos();
                ddlTipoDeProducto.DataSource = lista_tipos;
                ddlTipoDeProducto.DataValueField = "Id";
                ddlTipoDeProducto.DataTextField = "Nombre";
                ddlTipoDeProducto.DataBind();
            }

            int idProducto;
            if (int.TryParse(ddlProductoModificar.SelectedValue, out idProducto) && idProducto > 0)
            {
                Producto producto = Productos.FirstOrDefault(p => p.Id == idProducto);
                if (producto != null)
                {
                    txtNombreProd.Text = producto.Nombre;
                    ddlMarcas.SelectedValue = lista_marcas.FirstOrDefault(m => m.Nombre == producto.Marca.Nombre)?.Id.ToString() ?? "0";
                    ddlTipoDeProducto.SelectedValue = lista_tipos.FirstOrDefault(t => t.Nombre == producto.TipoProducto.Nombre)?.Id.ToString() ?? "0";
                    txtPrecio.Text = producto.Precio.ToString(CultureInfo.InvariantCulture);
                    txtStock.Text = producto.Stock.ToString();
                    txtStockMin.Text = producto.StockMin.ToString();
                    txtUrlImagen.Text = producto.UrlImgProducto;
                }
            }
            else
            {
                // Limpiar campos si no hay producto seleccionado
                txtNombreProd.Text = "";
                ddlMarcas.SelectedIndex = 0;
                ddlTipoDeProducto.SelectedIndex = 0;
                txtPrecio.Text = "";
                txtStock.Text = "";
                txtStockMin.Text = "";
                txtUrlImagen.Text = "";
            }
        }
        protected void btnModificarProducto_Click(object sender, EventArgs e)
        {
            int idProducto;
            if (!int.TryParse(ddlProductoModificar.SelectedValue, out idProducto) || idProducto == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Seleccione un producto válido.');", true);
                return;
            }

            Producto producto = new Producto { Id = idProducto };
            ProductoNegocio data = new ProductoNegocio();
            
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

                data.ModificarProducto(producto);

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

        protected void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            try
            {
                Marca newMarca = new Marca();
                newMarca.Nombre = txtNombreMarca.Text;
                newMarca.Activo = true;

                MarcaNegocio ngr = new MarcaNegocio();
                ngr.AgregarMarca(newMarca);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('¡Marca agregada exitosamente!');", true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}