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
    public partial class GestionProveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           NegocioProveedores negocio = new NegocioProveedores();
            Proveedores.DataSource = negocio.ListarProveedores();
            Proveedores.DataBind();
        }

        protected void RegisterProv_Click(object sender, EventArgs e)
        {
            try
            {
                Proveedor proveedor = new Proveedor();
                proveedor.RazonSocial = TextRazonSocial.Text;
                proveedor.Cuit = TextCuit.Text;
                proveedor.Email = TextEmail.Text;   
                proveedor.Telefono = TextTelefono.Text;
                proveedor.Direccion = TextDireccion.Text;

                NegocioProveedores negocio = new NegocioProveedores();
                negocio.AgregarProveedores(proveedor);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void EditProveedor_Click(object sender, EventArgs e)
        {
            Proveedor editProveedor = new Proveedor();
            editProveedor.Id = int.Parse(IdEdit.Text);
            editProveedor.RazonSocial = TextEditRazon.Text;
            editProveedor.Cuit = TextEditCuit.Text;
            editProveedor.Email = TextEditEmail.Text;
            editProveedor.Telefono = TextEditTel.Text;
            editProveedor.Direccion = TextEditDire.Text;

            NegocioProveedores negocio = new NegocioProveedores();
            negocio.ModificarProveedores(editProveedor);
        }

        protected void DeleteProv_Click(object sender, EventArgs e)
        {
            Proveedor proveedor = new Proveedor();
            proveedor.Id = int.Parse(IdDelete.Text);

            NegocioProveedores negocio = new NegocioProveedores();
            negocio.EliminarProveedores(proveedor);
        }
    }
}