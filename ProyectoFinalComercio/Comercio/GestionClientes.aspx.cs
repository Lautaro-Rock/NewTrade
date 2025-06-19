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
            NegocioCliente userCliente = new NegocioCliente();
            Clientes.DataSource = userCliente.ListarClientes();
            Clientes.DataBind();
        }

        protected void InsertClient_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente user = new Cliente();
                user.Nombre = TxtNombre.Text;
                user.Apellido = TxtApellido.Text;
                user.Dni = int.Parse(TxtD.Text);
                user.Email = TxtEmail.Text;
                user.Rol = "Cliente";

                NegocioCliente userNegocio = new NegocioCliente();
                userNegocio.AgregarUsuarioCliente(user);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void EditClient_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Nombre = txtEditNombre.Text;
                cliente.Apellido = txtEditApellido.Text;
                cliente.Email = TxtEmail.Text;
                cliente.Dni = int.Parse(txtEditDni.Text);
                cliente.Rol = "Cliente";

                NegocioCliente nrg = new NegocioCliente();
                nrg.EditarUsuarioCliente(cliente);


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
                nrg.DeleteCliente(cliente);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}