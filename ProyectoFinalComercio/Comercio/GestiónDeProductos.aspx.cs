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
        public List<Producto> Productos
        {
            get { return Session["Productos"] as List<Producto>; }
            set { Session["Productos"] = value; }
        }
        public List<Marca> lista_marcas = new List<Marca>();
        public List<TipoProducto> lista_tipos = new List<TipoProducto>();
        protected void Page_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcas = new MarcaNegocio();
            lista_marcas = marcas.ListarMarcas();

            NegocioTipoProducto tipos = new NegocioTipoProducto();
            lista_tipos = tipos.ListarTiposDeProductos();

            if (!IsPostBack)
            {
                ProductoNegocio negocio = new ProductoNegocio();
                Productos = negocio.ListarProductos();

                PanelFormAltaProd.Visible = false;
                PanelListarProd.Visible = true;
                PanelAgregarMarca.Visible= false;
                PanelEliminarProducto.Visible = false;
                PanelEliminarMarca.Visible = false;
                PanelEliminarCategoria.Visible = false;
                PanelAgregarCategoria.Visible = false;
                ddlCampo.Items.Insert(0, new ListItem("Seleccione un campo", "0"));
                repProductos.DataSource = Productos;
                repProductos.DataBind();
            }
            this.PreRender += Page_PreRender;

        }

        //Eventos relacionados a la seccion productos
        //
        protected void limpiarCampos()
        {
            txtNombreProd.Text = "";
            if (ddlMarcas.Items.Count > 0)
                ddlMarcas.SelectedIndex = 0;
            if (ddlTipoDeProducto.Items.Count > 0)
                ddlTipoDeProducto.SelectedIndex = 0;
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtStockMin.Text = "";
            txtUrlImagen.Text = "";
        }

        //Agregar producto
        protected void btnAgregarProdClick(object sender, EventArgs e)
        {
            PanelListarProd.Visible = false;
            PanelAgregarMarca.Visible = false;
            PanelEliminarProducto.Visible = false;
            PanelEliminarMarca.Visible = false;
            PanelFormAltaProd.Visible = true;
            PanelEliminarCategoria.Visible = false;
            PanelAgregarCategoria.Visible = false;

            lblTituloAgregar.Visible = true;
            lblTituloModificar.Visible = false;
            btnGuardarProducto.Visible = true;
            btnModificarProducto.Visible = false;
            divProductoModificar.Visible = false;
            limpiarCampos();
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
            PanelEliminarCategoria.Visible = false;
            PanelAgregarCategoria.Visible = false;

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
            PanelEliminarCategoria.Visible = false;
            PanelAgregarCategoria.Visible = false;
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
            PanelEliminarCategoria.Visible = false;
            PanelAgregarCategoria.Visible = false;
            ActualizarListas();
            repProductos.DataSource = Productos;
            repProductos.DataBind();
        }


        protected void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtFiltroRapido.Text.ToUpper();
            List<Producto> filtrado = Productos.FindAll(p => p.Nombre.ToUpper().Contains(filtro));
            repProductos.DataSource = filtrado;
            repProductos.DataBind();

        }
        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlCampo.SelectedItem.Text == "Por precio")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
            }
            else
            {
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
        }

        protected void chkFiltroActivo_CheckedChanged(object sender, EventArgs e)
        {
            txtFiltroRapido.Enabled = !chkFiltroActivo.Checked;
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que se haya seleccionado un campo y criterio válidos
                if (ddlCampo.SelectedValue == "0" || ddlCriterio.SelectedItem == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "Swal.fire('Filtro incompleto', 'Seleccioná un campo y un criterio antes de buscar.', 'warning');", true);
                    return;
                }


                // Validar que el valor ingresado sea numérico si se filtra por precio
                if (ddlCampo.SelectedValue == "Precio")
                {
                    // Validar que se haya ingresado un filtro
                    if (string.IsNullOrWhiteSpace(txtFiltroAvanzado.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            "Swal.fire('Filtro vacío', 'Ingresá un valor para poder filtrar por precio.', 'warning');", true);
                        return;
                    }

                    decimal valor;
                    if (!decimal.TryParse(txtFiltroAvanzado.Text, out valor))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            "Swal.fire('Dato inválido', 'El valor del precio debe ser un número.', 'warning');", true);
                        return;
                    }
                }


                ProductoNegocio negocio = new ProductoNegocio();
                repProductos.DataSource = negocio.Fitrar(ddlCampo.SelectedItem.ToString(),
                    ddlCriterio.SelectedItem.ToString(), txtFiltroAvanzado.Text,
                    ddlActivo.SelectedItem.ToString());
                repProductos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                throw;
            }
        }

        protected void btnLimpiarFiltroAvanzado_Click(object sender, EventArgs e)
        {

        }



        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            Page.Validate("AltaProducto");
            if (!Page.IsValid)
            {
                return;
            }

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

            if (producto.Precio < 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Precio no valido.');", true);
                return;
            }

            try
            {

                data.AgregarProductos(producto);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "Swal.fire('¡Producto agregado!', '', 'success');", true);
                ActualizarListas();

                // Limpiamos los campos del form :)
                limpiarCampos();

            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idProducto;
            if (!int.TryParse(ddlProductos.SelectedValue, out idProducto) || idProducto == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                 "Swal.fire('Ocurrió un error', 'Seleccione un producto valido', 'error');", true);

                return;
            }

            Producto producto = new Producto { Id = idProducto };
            ProductoNegocio negocio = new ProductoNegocio();

            try
            {
                negocio.EliminarProductoLogico(producto);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "Swal.fire('¡Producto dado de baja!', '', 'success');", true);
                ActualizarListas();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);

            }
        }

        protected void setearCamposProductoSeleccionado(Producto producto)
        {
            // Aca se valida si el producto tiene una marca o tipo de producto que fue eliminado 
            if (!lista_tipos.Any(t => t.Id == producto.TipoProducto.Id))
            {
                ddlTipoDeProducto.Items.Insert(0, new ListItem("Tipo de producto eliminado", producto.TipoProducto.Id.ToString()));
            }
            ddlTipoDeProducto.SelectedValue = producto.TipoProducto.Id.ToString();


            if (!lista_marcas.Any(m => m.Id == producto.Marca.Id))
            {
                ddlMarcas.Items.Insert(0, new ListItem("Marca eliminada", producto.Marca.Id.ToString()));
            }
            ddlMarcas.SelectedValue = producto.Marca.Id.ToString();
            //

            txtNombreProd.Text = producto.Nombre;
            ddlMarcas.SelectedValue = lista_marcas.FirstOrDefault(m => m.Nombre == producto.Marca.Nombre)?.Id.ToString() ?? "0";
            ddlTipoDeProducto.SelectedValue = lista_tipos.FirstOrDefault(t => t.Nombre == producto.TipoProducto.Nombre)?.Id.ToString() ?? "0";
            txtPrecio.Text = producto.Precio.ToString(CultureInfo.InvariantCulture);
            txtStock.Text = producto.Stock.ToString();
            txtStockMin.Text = producto.StockMin.ToString();
            txtUrlImagen.Text = producto.UrlImgProducto;
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
                    setearCamposProductoSeleccionado(producto);
                }
            }
            else
            {
                // Limpiar campos si no hay producto seleccionado
                limpiarCampos();
            }
        }
        protected void btnModificarProducto_Click(object sender, EventArgs e)
        {
            Page.Validate("AltaProducto");
            if (!Page.IsValid)
            {
                return; 
            }

            int idProducto;
            // Validamos que se haya seleccionado un producto
            if (!int.TryParse(ddlProductoModificar.SelectedValue, out idProducto) || idProducto == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('Ocurrió un error', 'Seleccione un producto valido', 'error');", true);
                return;
            }

            // Validamos que no se esten seleccionando marcas o tipos de producto eliminados
            if (ddlTipoDeProducto.SelectedItem.Text == "Tipo de producto eliminado" ||
                ddlMarcas.SelectedItem.Text == "Marca eliminada")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "Swal.fire('Ocurrió un error', 'Seleccione una marca y tipo de producto valido', 'error');", true);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('¡Producto modificado!', '', 'success');", true);
                ActualizarListas();

                // Limpiamos los campos del form :)
                limpiarCampos();


            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);
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
            PanelEliminarCategoria.Visible = false;
            PanelAgregarCategoria.Visible = false;

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "Swal.fire('Ocurrió un error', 'Debe agregarle un nombre a la marca', 'error');", true);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
              "Swal.fire('Ocurrió un error', 'La marca ya existe!', 'error');", true);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                     "Swal.fire('¡Marca agregada!', '', 'success');", true);

                } catch (Exception ex) {

                    string mensaje = ex.Message.Replace("'", "\\'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);

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
            PanelEliminarCategoria.Visible = false;
            PanelAgregarCategoria.Visible = false;
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "Swal.fire('¡Marca eliminada!', '', 'success');", true);
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
            PanelEliminarCategoria.Visible = false;
            PanelAgregarCategoria.Visible = false;

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "Swal.fire('Ocurrió un error', 'Seleccione una marca valida!', 'error');", true);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreMarca.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('Ocurrió un error', 'Ingrese el nombre de la marca!', 'error');", true);
                return;
            }

            MarcaNegocio negocio = new MarcaNegocio();
            Marca marca = new Marca { Id = idMarca, Nombre = txtNombreMarca.Text.Trim(), Activo = true };

            try
            {
                negocio.ModificarMarca(marca);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "Swal.fire('¡Marca agregada!', 'Marca modificada', 'success');", true);

                ActualizarListas();
                txtNombreMarca.Text = ""; 
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);
            }
        }

        // Modificar marca END

        // Eventos relaciondos a Tipo de Producto
        //
        // Agregar tipo de producto START
        protected void btnPanelAgregarTipo_Click(object sender, EventArgs e)
        {
            PanelListarProd.Visible = false;
            PanelFormAltaProd.Visible = false;
            PanelEliminarProducto.Visible = false;
            PanelEliminarMarca.Visible = false;
            PanelAgregarMarca.Visible = false;
            PanelEliminarCategoria.Visible = false;
            PanelAgregarCategoria.Visible = true;


            lblAgregarCategoria.Visible = true;
            lblModificarCategoria.Visible = false;
            divCategoriaModificar.Visible = false;
            btnAgregarCategoria.Visible = true;
            btnModificarCategoria.Visible = false;
            txtNombreCategoria.Text = "";
        }
        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            NegocioTipoProducto categoria = new NegocioTipoProducto();
            List<TipoProducto> listaTipoProducto = categoria.ListarTiposDeProductos();

            if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('Ocurrió un error', 'Ingrese el nombre del tipo de marca!', 'error');", true);
                return;
            }
            bool existe = false;

            foreach (TipoProducto recorrer in listaTipoProducto)
            {
                if (recorrer.Nombre.Equals(txtNombreCategoria.Text, StringComparison.OrdinalIgnoreCase))
                {
                    existe = true;
                    break;
                }
            }
            if (existe)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('Ocurrió un error', El tipo de marca ya existe!', 'error');", true);
            }
            else
            {

                try
                {
                    TipoProducto nuevo = new TipoProducto();
                    nuevo.Nombre = txtNombreCategoria.Text;
                    nuevo.Activo = true;
                    NegocioTipoProducto agregar = new NegocioTipoProducto();
                    agregar.AgregarTipoProducto(nuevo);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "Swal.fire('¡Tipo de producto agregado correctamente!', '', 'success');", true);

                }
                catch (Exception ex)
                {

                    string mensaje = ex.Message.Replace("'", "\\'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);

                }


            }
        }

        // Agregar tipo de producto END 

        // Modificar tipo de producto START

        protected void btnPanelModificarTipo_Click(object sender, EventArgs e)
        {
            PanelListarProd.Visible = false;
            PanelFormAltaProd.Visible = false;
            PanelEliminarProducto.Visible = false;
            PanelEliminarMarca.Visible = false;
            PanelAgregarMarca.Visible = false;
            PanelEliminarCategoria.Visible = false;
            PanelAgregarCategoria.Visible = true;

            lblModificarCategoria.Visible = true;
            lblAgregarCategoria.Visible = false;
            divCategoriaModificar.Visible = true;
            btnAgregarCategoria.Visible = false;
            btnModificarCategoria.Visible = true;
            ActualizarListas();
        }

        protected void ddlCategoriaModificar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategoria;
            if (int.TryParse(ddlCategoriaModificar.SelectedValue, out idCategoria) && idCategoria > 0)
            {
                TipoProducto categoria = lista_tipos.FirstOrDefault(m => m.Id == idCategoria);
                if (categoria != null)
                    txtNombreCategoria.Text = categoria.Nombre;
            }
            else
            {
                txtNombreCategoria.Text = "";
            }
        }

        protected void btnModificarCategoria_Click(object sender, EventArgs e)
        {
            int idCategoria;
            if (!int.TryParse(ddlCategoriaModificar.SelectedValue, out idCategoria) || idCategoria == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('Ocurrió un error', 'Seleccione un tipo de producto valido!', 'error');", true);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('Ocurrió un error', 'El nombre no puede estar vacio!', 'error');", true);
                return;
            }

            NegocioTipoProducto negocio = new NegocioTipoProducto();
            TipoProducto tipoProducto = new TipoProducto { Id = idCategoria, Nombre = txtNombreCategoria.Text.Trim(), Activo = true };

            try
            {
                negocio.ModificarTipoProducto(tipoProducto);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('¡Tipo de producto modificado correctamente!', '', 'success');", true);
                ActualizarListas();
                txtNombreCategoria.Text = "";
            }
            catch (Exception ex)
            {
                 string mensaje = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);
            }
        }
        // Modificar tipo de producto END 

        // Eliminar tipo de producto START

        protected void btnPanelEliminarTipo_Click(object sender, EventArgs e)
        {
            PanelFormAltaProd.Visible = false;
            PanelListarProd.Visible = false;
            PanelAgregarMarca.Visible = false;
            PanelEliminarProducto.Visible = false;
            PanelEliminarMarca.Visible = false;
            PanelEliminarCategoria.Visible = true;
            PanelAgregarCategoria.Visible = false;
            ActualizarListas();
        }

        protected void ddlCategoriasEliminar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategoriasEliminar.SelectedIndex > 0)
                PanelConfirmarEliminarCategoria.Visible = true;
            else
                PanelConfirmarEliminarCategoria.Visible = false;
        }

        protected void btnEliminarCategoria_Click(object sender, EventArgs e)
        {
            int idCategoria;
            if (!int.TryParse(ddlCategoriasEliminar.SelectedValue, out idCategoria) || idCategoria == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "Swal.fire('Ocurrió un error', 'Seleccione un tipo de producto valido!', 'error');", true);
                return;
            }

            NegocioTipoProducto negocio = new NegocioTipoProducto();
            TipoProducto tipoProducto = new TipoProducto { Id = idCategoria };
            try
            {
                negocio.EliminarTipoProductoLogico(tipoProducto);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "Swal.fire('¡Tipo producto eliminado correctamente!', '', 'success');", true);
                ActualizarListas();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);
            }
        }

        // Eliminar tipo de producto END

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

            ddlCategoriaModificar.DataSource = lista_tipos;
            ddlCategoriaModificar.DataValueField = "Id";
            ddlCategoriaModificar.DataTextField = "Nombre"; 
            ddlCategoriaModificar.DataBind();
            ddlCategoriaModificar.Items.Insert(0, new ListItem("Seleccione un Tipo de Producto", "0"));

            ddlCategoriasEliminar.DataSource = lista_tipos;
            ddlCategoriasEliminar.DataValueField = "Id";
            ddlCategoriasEliminar.DataTextField = "Nombre";
            ddlCategoriasEliminar.DataBind();
            ddlCategoriasEliminar.Items.Insert(0, new ListItem("Seleccione un Tipo de Producto", "0"));

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

        protected void btnModificarPListado_Click(object sender, EventArgs e)
        {
            
            PanelListarProd.Visible = false;
            PanelAgregarMarca.Visible = false;
            PanelEliminarProducto.Visible = false;
            PanelEliminarMarca.Visible = false;
            PanelEliminarCategoria.Visible = false;
            PanelAgregarCategoria.Visible = false;
            PanelFormAltaProd.Visible = true;

            lblTituloAgregar.Visible = false;
            lblTituloModificar.Visible = true;
            btnGuardarProducto.Visible = false;
            btnModificarProducto.Visible = true;
            divProductoModificar.Visible = true;
            ActualizarListas();

            var btn = (Button)sender;
            int idProducto;

            if (int.TryParse(btn.CommandArgument, out idProducto) && idProducto > 0)
            {
                Producto producto = Productos.FirstOrDefault(p => p.Id == idProducto);
                 if (producto != null)
                 {
                     setearCamposProductoSeleccionado(producto);
                    ddlProductoModificar.SelectedValue = producto.Id.ToString();
                }
            }
            else
            {
                // Limpiar campos si no hay producto seleccionado
                limpiarCampos();
            }
        }

        protected void btnEliminarProductoListado_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int idProducto;
            if (int.TryParse(btn.CommandArgument, out idProducto))
            {
                ProductoNegocio negocio = new ProductoNegocio();
                Producto producto = new Producto { Id = idProducto };
                try
                {
                    negocio.EliminarProductoLogico(producto);
                    ActualizarListas();
                    txtFiltroRapido_TextChanged(txtFiltroRapido, EventArgs.Empty);

                }
                catch (Exception ex)
                {
                    string mensaje = ex.Message.Replace("'", "\\'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);
                }
            }
        }

        // [JLS] Este evento es para que los botones de modificar y eliminar en el listado puedan hacer un postback y asi funcionar correctamente :)
        protected void Page_PreRender(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repProductos.Items)
            {
                Button btnModificar = (Button)item.FindControl("btnModificarPListado");
                if (btnModificar != null)
                    ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnModificar);

                Button btnEliminar = (Button)item.FindControl("btnEliminarProductoListado");
                if (btnEliminar != null)
                    ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnEliminar);
            }
        }

        protected void btnLimpiarFiltroAvanzado_Click1(object sender, EventArgs e)
        {
            // Limpiar campos de filtro rápido y avanzado
            txtFiltroRapido.Text = string.Empty;
            txtFiltroAvanzado.Text = string.Empty;
            ddlCampo.SelectedIndex = 0;
            ddlCriterio.Items.Clear(); 
            ddlActivo.SelectedIndex = 0;

            // Recargar todos los productos
            ProductoNegocio negocio = new ProductoNegocio();
            Productos = negocio.ListarProductos();
            repProductos.DataSource = Productos;
            repProductos.DataBind();
        }
    }
}