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
        public void Agregar(Usuario nuevo)
        {
  
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.SetearConsulta("INSERT Usuario (Nombre, Apellido, Email, DNI, Password, Rol) " +
                "VALUES('" + nuevo.Nombre + "', '" + nuevo.Apellido + "', '" + nuevo.Email + "', " + nuevo.Dni + ", '" + nuevo.Password + "', '" + nuevo.Rol + "')");
                data.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.CerrarConexion();
            }
        }

        public bool ValidarCredenciales(string email, string password)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM Usuario WHERE Email = @email AND Password = @password");
                datos.SetearParametro("@email", email);
                datos.SetearParametro("@password", password);

                datos.EjecutarLectura();

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
                datos.CerrarConexion();
            }
        }

        public void EliminarUsuarioLogico(Usuario nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE Usuario SET Activo = 0 WHERE Id = @id;");
                data.SetearParametro("@id", nuevo.Id);
                data.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.CerrarConexion();
            }
        }
    }
}
