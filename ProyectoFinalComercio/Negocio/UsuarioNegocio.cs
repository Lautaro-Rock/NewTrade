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

        public bool validarCredenciales(string email, string password)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Usuario WHERE Email = @email AND Password = @password");
                datos.setearParametro("@email", email);
                datos.setearParametro("@password", password);

                datos.ejecutarLectura();

                if (datos.ConexionDataReader.Read())
                {
                    int count = (int)datos.ConexionDataReader[0];
                    if (count > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
