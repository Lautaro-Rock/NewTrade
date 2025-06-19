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
            UsuarioNegocio userCliente = new UsuarioNegocio();
            Clientes.DataSource = userCliente.ListarClientes();
            Clientes.DataBind();
        }

        protected void InsertClient_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario user = new Usuario();
                user.Nombre = TxtNombre.Text;
                user.Apellido = TxtApellido.Text;
                user.Dni = int.Parse(TxtD.Text);
                user.Email = TxtEmail.Text;
                user.Rol = "Cliente";

                UsuarioNegocio userNegocio = new UsuarioNegocio();
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
                Usuario usuario = new Usuario();
                usuario.Nombre = txtEditNombre.Text;
                usuario.Apellido = txtEditApellido.Text;
                usuario.Email = TxtEmail.Text;
                usuario.Dni = int.Parse(txtEditDni.Text);
                usuario.Rol = "Cliente";

                UsuarioNegocio nrg = new UsuarioNegocio();
                nrg.EditarUsuarioCliente(usuario);


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
                Usuario user = new Usuario();
                user.Email = DeleteEmail.Text;
                user.Rol = "Cliente";

                UsuarioNegocio nrg = new UsuarioNegocio();
                nrg.DeleteCliente(user);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}