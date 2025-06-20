using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class GestionClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
            }
        }

        private void CargarClientes()
        {
            NegocioCliente userCliente = new NegocioCliente();
            Clientes.DataSource = userCliente.ListarClientes();
            Clientes.DataBind();
        }

        protected void InsertClient_Click(object sender, EventArgs e)
        {
            Page.Validate("AltaCliente");
            if (!Page.IsValid)
            {
                return;
            }

            try
            {

                Cliente user = new Cliente();
                user.Nombre = TxtNombre.Text;
                user.Apellido = TxtApellido.Text;
                user.Dni = int.Parse(TxtD.Text);
                user.Email = TxtEmail.Text;
                user.Rol = "Cliente";

                NegocioCliente userNegocio = new NegocioCliente();
                userNegocio.AgregarCliente(user);
                CargarClientes();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void EditClient_Click(object sender, EventArgs e)
        {
            Page.Validate("ModificarCliente");
            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                Cliente cliente = new Cliente();
                cliente.Nombre = txtEditNombre.Text;
                cliente.Apellido = txtEditApellido.Text;
                cliente.Email = txtEditEmail.Text;
                cliente.Dni = int.Parse(txtEditDni.Text);
                cliente.Rol = "Cliente";

                NegocioCliente nrg = new NegocioCliente();
                nrg.EditarCliente(cliente);
                CargarClientes();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void DeleteClient_Click(object sender, EventArgs e)
        {

            try
            {
                Cliente cliente = new Cliente();
                cliente.Email = DeleteEmail.Text;
                cliente.Rol = "Cliente";

                NegocioCliente nrg = new NegocioCliente();
                nrg.DeleteClienteLogico(cliente);
                CargarClientes();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}