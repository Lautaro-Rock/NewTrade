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
    public partial class Prototipo2 : System.Web.UI.Page
    {
        public List<Producto> Productos = new List<Producto>();
        public List<Marca> lista_marcas = new List<Marca>();
        public List<TipoProducto> lista_tipos = new List<TipoProducto>();
        protected void Page_Load(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            Productos = negocio.ListarProductos();


            MarcaNegocio marcas = new MarcaNegocio();
            lista_marcas = marcas.ListarMarcas();

            NegocioTipoProducto tipos = new NegocioTipoProducto();
            lista_tipos = tipos.ListarTiposDeProductos();

            if (!IsPostBack)
            {
                PanelFormAltaCliente.Visible = false;
                PanelListarCliente.Visible = false;
                PanelEliminarCliente.Visible = false;
            }

        }

        //Eventos relacionados a la seccion productos
        //
        protected void limpiarCampos()
        {
            txtNombreCliente.Text = "";
            txtApellido.Text = "";
            txtDNI.Text = "";
            txtEmail.Text = "";
        }

        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            Page.Validate("AltaCliente");
            if (!Page.IsValid)
            {
                return;
            }

            ProductoNegocio data = new ProductoNegocio();
            Producto producto = new Producto();

            producto.Nombre = txtNombreCliente.Text.Trim();
            producto.Precio = decimal.Parse(txtApellido.Text.Trim());
            producto.Stock = int.Parse(txtDNI.Text.Trim());
            producto.StockMin = int.Parse(txtEmail.Text.Trim());
            producto.Activo = true;

            if (producto.Precio < 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Precio no valido.');", true);
                return;
            }

            try
            {

                data.AgregarProductos(producto);
                ActualizarListas();

                // Limpiamos los campos del form :)
                limpiarCampos();

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al guardar el producto: {ex.Message}');", true);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idProducto;
            if (!int.TryParse(ddlClienteEliminar.SelectedValue, out idProducto) || idProducto == 0)
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
        protected void btnModificarProducto_Click(object sender, EventArgs e)
        {
            Page.Validate("AltaCliente");
            if (!Page.IsValid)
            {
                return;
            }

            int idProducto;
            // Validamos que se haya seleccionado un producto
            if (!int.TryParse(ddlClienteModificar.SelectedValue, out idProducto) || idProducto == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Seleccione un producto válido.');", true);
                return;
            }

            Producto producto = new Producto { Id = idProducto };
            ProductoNegocio data = new ProductoNegocio();

            producto.Nombre = txtNombreCliente.Text.Trim();
            producto.Precio = decimal.Parse(txtApellido.Text.Trim());
            producto.Stock = int.Parse(txtDNI.Text.Trim());
            producto.StockMin = int.Parse(txtEmail.Text.Trim());
            producto.Activo = true;

            try
            {

                data.ModificarProducto(producto);
                ActualizarListas();

                // Limpiamos los campos del form :)
                limpiarCampos();


            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al guardar el producto: {ex.Message}');", true);
            }
        }


        private void ActualizarListas()
        {
            // Actualizar productos
            ProductoNegocio productoNegocio = new ProductoNegocio();
            Productos = productoNegocio.ListarProductos();
            ddlClienteEliminar.DataSource = Productos;
            ddlClienteEliminar.DataValueField = "Id";
            ddlClienteEliminar.DataTextField = "Nombre";
            ddlClienteEliminar.DataBind();
            ddlClienteEliminar.Items.Insert(0, new ListItem("Seleccione un producto", "0"));

            ddlClienteModificar.DataSource = Productos;
            ddlClienteModificar.DataValueField = "Id";
            ddlClienteModificar.DataTextField = "Nombre";
            ddlClienteModificar.DataBind();
            ddlClienteModificar.Items.Insert(0, new ListItem("Seleccione un producto", "0"));

            // Actualizar marcas
            MarcaNegocio marcas = new MarcaNegocio();
            lista_marcas = marcas.ListarMarcas();


            // Actualizar tipos de producto
            NegocioTipoProducto tipos = new NegocioTipoProducto();
            lista_tipos = tipos.ListarTiposDeProductos();

        }

        //Eventos relacionados a la seccion General
        protected void btnVolverPanelClick(object sender, EventArgs e)
        {
            Response.Redirect("PanelCtrlAdmin.aspx");
        }

        protected void btnListarCliente_Click(object sender, EventArgs e)
        {
            PanelFormAltaCliente.Visible = false;
            PanelEliminarCliente.Visible = false;
            PanelListarCliente.Visible = true;
            ActualizarListas();
        }

        protected void btnEliminarCliente_Click(object sender, EventArgs e)
        {
            PanelListarCliente.Visible = false;
            PanelFormAltaCliente.Visible = false;
            PanelEliminarCliente.Visible = true;
            ActualizarListas();
        }

        protected void btnModificarCliente_Click(object sender, EventArgs e)
        {
            PanelListarCliente.Visible = false;
            PanelEliminarCliente.Visible = false;
            PanelFormAltaCliente.Visible = true;

            lblTituloAgregar.Visible = false;
            lblTituloModificar.Visible = true;
            btnGuardarProducto.Visible = false;
            btnModificarProducto.Visible = true;
            divClienteModificar.Visible = true;
            ActualizarListas();
        }

        protected void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            PanelListarCliente.Visible = false;
            PanelEliminarCliente.Visible = false;
            PanelFormAltaCliente.Visible = true;

            lblTituloAgregar.Visible = true;
            lblTituloModificar.Visible = false;
            btnGuardarProducto.Visible = true;
            btnModificarProducto.Visible = false;
            divClienteModificar.Visible = false;
            limpiarCampos();
            ActualizarListas();
        }

        protected void ddlClienteModificar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProducto;
            if (int.TryParse(ddlClienteModificar.SelectedValue, out idProducto) && idProducto > 0)
            {
                Producto producto = Productos.FirstOrDefault(p => p.Id == idProducto);
                if (producto != null)
                {

                    txtNombreCliente.Text = producto.Nombre;
                    txtApellido.Text = producto.Precio.ToString(CultureInfo.InvariantCulture);
                    txtDNI.Text = producto.Stock.ToString();
                    txtEmail.Text = producto.StockMin.ToString();
                }
            }
            else
            {
                // Limpiar campos si no hay producto seleccionado
                limpiarCampos();
            }
        }
    }
}