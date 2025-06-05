using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Dominio;


namespace Negocio
{
    public class UsuarioNegocio
    {
        public void agregar(Usuario nuevo)
        {
  
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.setearConsulta("INSERT Usuario (Nombre, Apellido, Email, DNI, Password, Rol) " +
                "VALUES('" + nuevo.nombre + "', '" + nuevo.apellido + "', '" + nuevo.email + "', " + nuevo.dni + ", '" + nuevo.password + "', '" + nuevo.rol + "')");
                data.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.cerrarConexion();
            }
        }
    }
}
