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
            if (!IsPostBack)
            {         
                PanelFormAltaProd.Visible = false;
                PanelListarProd.Visible = false;
                PanelAgregarMarca.Visible= false;
                PanelEliminarProducto.Visible = false;
                PanelEliminarMarca.Visible = false;
            }

        }

        //Eventos relacionados a la seccion productos
        //
        //Agregar producto
        protected void btnAgregarProdClick(object sender, EventArgs e)
        {
            PanelListarProd.Visible = false;
            PanelAgregarMarca.Visible = false;
            PanelEliminarProducto.Visible = false;
            PanelEliminarMarca.Visible = false;
            PanelFormAltaProd.Visible = true;

            lblTituloAgregar.Visible = true;
            lblTituloModificar.Visible = false;
            btnGuardarProducto.Visible = true;
            btnModificarProducto.Visible = false;
            divProductoModificar.Visible = false;
            ActualizarListas();
        }

        //Modificar producto
        protected void btnModificarProd_Click(object sender, EventArgs e)
        {
            PanelListarProd.Visible = false;
            PanelAgregarMarca.Visible = false;
            PanelEliminarProducto.Visible = false;
            PanelEliminarMarca.Visible = false;
            PanelFormAltaProd.Visible = true;

            lblTituloAgregar.Visible = false;
            lblTituloModificar.Visible = true;
            btnGuardarProducto.Visible = false;
            btnModificarProducto.Visible = true;
            divProductoModificar.Visible = true;
            ActualizarListas();
        }

        //Eliminar producto
        protected void btnEliminarProdClick(object sender, EventArgs e)
        {
            PanelListarProd.Visible = false;
            PanelFormAltaProd.Visible = false;
            PanelAgregarMarca.Visible = false;
            PanelEliminarMarca.Visible = false;
            PanelEliminarProducto.Visible = true;
            ActualizarListas();
        }

        //Listar productos
        protected void btnListarProdClick(object sender, EventArgs e)
        {
            PanelFormAltaProd.Visible = false;
            PanelAgregarMarca.Visible = false;
            PanelEliminarProducto.Visible = false;
            PanelEliminarMarca.Visible = false;
            PanelListarProd.Visible = true;
            ActualizarListas();
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
                ActualizarListas();

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
                ActualizarListas();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al eliminar el producto: {ex.Message}');", true);
            }
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
                ActualizarListas();

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


        //Eventos relacionados a la seccion marcas
        //
        //Agregar marca START
        protected void btnPanelAgregarMarcaClick(object sender, EventArgs e)
        {
            PanelListarProd.Visible = false;
            PanelFormAltaProd.Visible = false;
            PanelEliminarProducto.Visible = false;
            PanelEliminarMarca.Visible = false;
            PanelAgregarMarca.Visible = true;

            lblTituloAgregarMarca.Visible = true;
            lblTituloModificarMarca.Visible = false;
            divMarcaModificar.Visible = false;
            btnAgregarMarca.Visible = true;
            btnModificarMarca.Visible = false;
            txtNombreMarca.Text = "";
        }
        protected void btnAgregarMarcaClick(object sender, EventArgs e)
        {
            MarcaNegocio marcas = new MarcaNegocio();
            List <Marca> lista_marcas_act = marcas.ListarMarcas();

            if (string.IsNullOrWhiteSpace(txtNombreMarca.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El nombre de la marca no puede estar vacío.');", true);
                return;
            }
            bool existe = false;
            
            foreach (Marca recorrer in lista_marcas_act)
            {
                if(recorrer.Nombre.Equals(txtNombreMarca.Text, StringComparison.OrdinalIgnoreCase))
                {
                    existe = true;
                    break;
                }
            }
            if (existe)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('La marca ya existe.');", true);
            }
            else
            {

                try
                {
                    Marca nueva_marca = new Marca();
                    nueva_marca.Nombre = txtNombreMarca.Text;
                    nueva_marca.Activo = true;
                    MarcaNegocio para_agregar = new MarcaNegocio();
                    para_agregar.AgregarMarca(nueva_marca);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('¡Marca agregada exitosamente!');", true);

                } catch (Exception ex) {

                    throw ex;

                }


            }
        }

        // Agregar marca END

        //Eliminar marca START

        protected void btnPanelEliminarMarcaClick(object sender, EventArgs e)
        {
            PanelFormAltaProd.Visible = false;
            PanelListarProd.Visible = false;
            PanelAgregarMarca.Visible = false;
            PanelEliminarProducto.Visible = false;
            PanelEliminarMarca.Visible = true;
            ActualizarListas();
        }

        protected void btnEliminarMarca2_Click(object sender, EventArgs e)
        {
            int idMarca = int.Parse(ddlMarcasEliminar.SelectedValue);
            MarcaNegocio negocio = new MarcaNegocio();
            Marca marca = new Marca { Id = idMarca };
            try
            {
                negocio.EliminarMarcaLogico(marca);
                ActualizarListas();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al eliminar la marca: {ex.Message}');", true);
            }
        }

        protected void ddlMarcasEliminar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMarcasEliminar.SelectedIndex > 0)
                PanelConfirmarEliminarMarca.Visible = true;
            else
                PanelConfirmarEliminarMarca.Visible = false;
        }

        // Eliminar marca END

        // Modificar marca START
        protected void btnPanelModificarMarca_Click(object sender, EventArgs e)
        {
            PanelListarProd.Visible = false;
            PanelFormAltaProd.Visible = false;
            PanelEliminarProducto.Visible = false;
            PanelEliminarMarca.Visible = false;
            PanelAgregarMarca.Visible = true;

            lblTituloAgregarMarca.Visible = false;
            lblTituloModificarMarca.Visible = true;
            divMarcaModificar.Visible = true;
            btnAgregarMarca.Visible = false;
            btnModificarMarca.Visible = true;
            ActualizarListas();
        }

        protected void ddlMarcaModificar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idMarca;
            if (int.TryParse(ddlMarcaModificar.SelectedValue, out idMarca) && idMarca > 0)
            {
                Marca marca = lista_marcas.FirstOrDefault(m => m.Id == idMarca);
                if (marca != null)
                    txtNombreMarca.Text = marca.Nombre;
            }
            else
            {
                txtNombreMarca.Text = "";
            }
        }

        protected void btnModificarMarca_Click(object sender, EventArgs e)
        {
            int idMarca;
            if (!int.TryParse(ddlMarcaModificar.SelectedValue, out idMarca) || idMarca == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Seleccione una marca válida.');", true);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreMarca.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El nombre no puede estar vacío.');", true);
                return;
            }

            MarcaNegocio negocio = new MarcaNegocio();
            Marca marca = new Marca { Id = idMarca, Nombre = txtNombreMarca.Text.Trim(), Activo = true };

            try
            {
                negocio.ModificarMarca(marca);
                ActualizarListas();
                txtNombreMarca.Text = ""; 
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al modificar la marca: {ex.Message}');", true);
            }
        }

        // Modificar marca END

        private void ActualizarListas()
        {
            // Actualizar productos
            ProductoNegocio productoNegocio = new ProductoNegocio();
            Productos = productoNegocio.ListarProductos();
            ddlProductos.DataSource = Productos;
            ddlProductos.DataValueField = "Id";
            ddlProductos.DataTextField = "Nombre";
            ddlProductos.DataBind();
            ddlProductos.Items.Insert(0, new ListItem("Seleccione un producto", "0"));

            ddlProductoModificar.DataSource = Productos;
            ddlProductoModificar.DataValueField = "Id";
            ddlProductoModificar.DataTextField = "Nombre";
            ddlProductoModificar.DataBind();
            ddlProductoModificar.Items.Insert(0, new ListItem("Seleccione un producto", "0"));

            // Actualizar marcas
            MarcaNegocio marcas = new MarcaNegocio();
            lista_marcas = marcas.ListarMarcas();

            ddlMarcas.DataSource = lista_marcas;
            ddlMarcas.DataValueField = "Id";
            ddlMarcas.DataTextField = "Nombre";
            ddlMarcas.DataBind();

            ddlFiltroMarca.DataSource = lista_marcas;
            ddlFiltroMarca.DataValueField = "Id";
            ddlFiltroMarca.DataTextField = "Nombre";
            ddlFiltroMarca.DataBind();
            ddlFiltroMarca.Items.Insert(0, new ListItem("Todas las marcas", "0"));

            ddlMarcasEliminar.DataSource = lista_marcas;
            ddlMarcasEliminar.DataValueField = "Id";
            ddlMarcasEliminar.DataTextField = "Nombre";
            ddlMarcasEliminar.DataBind();
            ddlMarcasEliminar.Items.Insert(0, new ListItem("Selecciona una marca", "0"));

            ddlMarcaModificar.DataSource = lista_marcas;
            ddlMarcaModificar.DataValueField = "Id";
            ddlMarcaModificar.DataTextField = "Nombre";
            ddlMarcaModificar.DataBind();
            ddlMarcaModificar.Items.Insert(0, new ListItem("Seleccione una marca", "0"));

            // Actualizar tipos de producto
            NegocioTipoProducto tipos = new NegocioTipoProducto();
            lista_tipos = tipos.ListarTiposDeProductos();

            ddlTipoDeProducto.DataSource = lista_tipos;
            ddlTipoDeProducto.DataValueField = "Id";
            ddlTipoDeProducto.DataTextField = "Nombre";
            ddlTipoDeProducto.DataBind();

            ddlFiltroTipo.DataSource = lista_tipos;
            ddlFiltroTipo.DataValueField = "Id";
            ddlFiltroTipo.DataTextField = "Nombre";
            ddlFiltroTipo.DataBind();
            ddlFiltroTipo.Items.Insert(0, new ListItem("Todos los tipos", "0"));
        }

        //Eventos relacionados a la seccion General
        protected void btnVolverPanelClick(object sender, EventArgs e)
        {
            Response.Redirect("PanelCtrlAdmin.aspx");
        }

    }
}