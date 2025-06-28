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
        public List<Cliente> Cliente = new List<Cliente>();

        public bool FiltroAvanzado { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!(Session["usuario"] != null && ((Dominio.Usuario)Session["usuario"]).Rol == "Administrador"))
            {
                Response.Redirect("Default.aspx");
            }

            NegocioCliente negocio = new NegocioCliente();
            Cliente = negocio.ListarClientes();
            FiltroAvanzado = checkAvanzado.Checked;
            if (!IsPostBack)
            {
                ActualizarListas();
                PanelFormAltaCliente.Visible = false;
                PanelListarCliente.Visible = true;
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

        protected void btnGuardarCliente_Click(object sender, EventArgs e)
        {
            Page.Validate("AltaCliente");
            if (!Page.IsValid)
            {
                return;
            }

            NegocioCliente data = new NegocioCliente();
            Cliente cliente = new Cliente();

            cliente.Nombre = txtNombreCliente.Text.Trim();
            cliente.Apellido = txtApellido.Text.Trim();
            cliente.Dni = int.Parse(txtDNI.Text.Trim());
            cliente.Email = txtEmail.Text.Trim();
            cliente.Rol = "Cliente"; 


            try
            {

                data.AgregarCliente(cliente);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "Swal.fire('¡Cliente registrado correctamente !', '', 'success');", true);
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
            int idCliente;
            if (!int.TryParse(ddlClienteEliminar.SelectedValue, out idCliente) || idCliente == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "Swal.fire('Ocurrió un error', 'Seleccione un cliente valido!', 'error');", true);
                return;
            }

            Cliente cliente = new Cliente { Id = idCliente };
            NegocioCliente negocio = new NegocioCliente();

            try
            {
                negocio.DeleteClienteLogico(cliente);
                ActualizarListas();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);
            }
        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Page.Validate("AltaCliente");
            if (!Page.IsValid)
            {
                return;
            }

            int idCliente;
            // Validamos que se haya seleccionado un producto
            if (!int.TryParse(ddlClienteModificar.SelectedValue, out idCliente) || idCliente == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "Swal.fire('Ocurrió un error', 'Seleccione un cliente valido!', 'error');", true);
                return;
            }

            Cliente cliente = new Cliente { Id = idCliente };
            NegocioCliente data = new NegocioCliente();

            cliente.Nombre = txtNombreCliente.Text.Trim();
            cliente.Apellido = txtApellido.Text.Trim();
            cliente.Dni = int.Parse(txtDNI.Text.Trim());
            cliente.Email = txtEmail.Text.Trim();

            try
            {

                data.EditarCliente(cliente);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire('¡Cliente editado correctamente!', '', 'success');", true);
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


        private void ActualizarListas()
        {
            // Actualizar productos
            NegocioCliente negocio = new NegocioCliente();
            Cliente = negocio.ListarClientes();
            ddlClienteEliminar.DataSource = Cliente;
            ddlClienteEliminar.DataValueField = "Id";
            ddlClienteEliminar.DataTextField = "Nombre";
            ddlClienteEliminar.DataBind();
            ddlClienteEliminar.Items.Insert(0, new ListItem("Seleccione un cliente", "0"));

            ddlClienteModificar.DataSource = Cliente;
            ddlClienteModificar.DataValueField = "Id";
            ddlClienteModificar.DataTextField = "Nombre";
            ddlClienteModificar.DataBind();
            ddlClienteModificar.Items.Insert(0, new ListItem("Seleccione un cliente", "0"));

            rptClientes.DataSource = Cliente;
            rptClientes.DataBind();

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
            btnGuardarCliente.Visible = false;
            btnModificar.Visible = true;
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
            btnGuardarCliente.Visible = true;
            btnModificar.Visible = false;
            divClienteModificar.Visible = false;
            limpiarCampos();
            ActualizarListas();
        }

        protected void ddlClienteModificar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCliente;
            if (int.TryParse(ddlClienteModificar.SelectedValue, out idCliente) && idCliente > 0)
            {
                Cliente cliente = Cliente.FirstOrDefault(p => p.Id == idCliente);
                if (cliente != null)
                {

                    txtNombreCliente.Text = cliente.Nombre;
                    txtApellido.Text = cliente.Apellido;
                    txtDNI.Text = cliente.Dni.ToString();
                    txtEmail.Text = cliente.Email;
                }
            }
            else
            {
                // Limpiar campos si no hay producto seleccionado
                limpiarCampos();
            }
        }

        protected void btnEliminarClienteListado_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int idCliente;
            if (int.TryParse(btn.CommandArgument, out idCliente))
            {
                NegocioCliente negocio = new NegocioCliente();
                Cliente cliente = new Cliente { Id = idCliente };
                try
                {
                    negocio.DeleteClienteLogico(cliente); 
                    ActualizarListas();
                }
                catch (Exception ex)
                {
                    string mensaje = ex.Message.Replace("'", "\\'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    $"Swal.fire('Ocurrió un error', '{mensaje}', 'error');", true);
                }
            }
        }

        protected void btnModificarClienteListado_Click(object sender, EventArgs e)
        {
            PanelListarCliente.Visible = false;
            PanelEliminarCliente.Visible = false;
            PanelFormAltaCliente.Visible = true;

            lblTituloAgregar.Visible = false;
            lblTituloModificar.Visible = true;
            btnGuardarCliente.Visible = false;
            btnModificar.Visible = true;
            divClienteModificar.Visible = true;

            var btn = (Button)sender;
            int idCliente;

            if (int.TryParse(btn.CommandArgument, out idCliente) && idCliente > 0)
            {
                Cliente cliente = Cliente.FirstOrDefault(p => p.Id == idCliente);
                if (cliente != null)
                {
                    txtNombreCliente.Text = cliente.Nombre;
                    txtApellido.Text = cliente.Apellido;
                    txtDNI.Text = cliente.Dni.ToString();
                    txtEmail.Text = cliente.Email;
                    // Actualizar el dropdown para modificar
                    ddlClienteModificar.SelectedValue = cliente.Id.ToString();
                }
            }
            else
            {
                // Limpiar campos si no hay producto seleccionado
                limpiarCampos();
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {

        }

        protected void checkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = checkAvanzado.Checked;
            txtFiltro.Enabled = !FiltroAvanzado;
        }

        protected void ddlCampoSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlCampoSelect.SelectedItem.ToString() == "DNI")
            {
                ddlCriterio.Items.Add("Igual a");
            }else if(ddlCampoSelect.SelectedItem.ToString() == "Nombre")
            {
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
        
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                NegocioCliente negocio = new NegocioCliente();

                string campo = ddlCampoSelect.SelectedItem.Text; 
                string criterio = ddlCriterio.SelectedItem.Text;
                string filtro = ddlFiltroAvanzado.Text.Trim();   
                string estado = ddlEstado.SelectedValue;

                rptClientes.DataSource = negocio.FiltrarCliente(campo, criterio, filtro, estado);
                rptClientes.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

    }
}