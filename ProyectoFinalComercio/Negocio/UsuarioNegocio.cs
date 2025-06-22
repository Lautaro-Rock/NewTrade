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
        public List<Usuario> ListarUsuarios()
        {
            AccesoDatos data = new AccesoDatos();
            List<Usuario> lista = new List<Usuario>();

            try
            {
                data.SetearConsulta("SELECT Id, Nombre, Apellido, Email, DNI, Password, Rol FROM Usuario WHERE Activo = 1;");
                data.EjecutarLectura();
                while (data.Lector.Read())
                {
                    Usuario user = new Usuario
                    {
                        Id = (int)data.Lector["Id"],
                        Nombre = (string)data.Lector["Nombre"],
                        Apellido = (string)data.Lector["Apellido"],
                        Email = (string)data.Lector["Email"],
                        Dni = int.Parse(data.Lector["DNI"].ToString()),
                        Password = (string)data.Lector["Password"],
                        Rol = (string)data.Lector["Rol"]
                    };
                    lista.Add(user);
                }
                return lista;
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
        
        public void EditarUsuario(Usuario nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE Usuario SET Nombre = @nombre, Apellido = @apellido, Email = @email, DNI = @dni, Password = @password WHERE Id = @id;");
                data.SetearParametro("@id", nuevo.Id);
                data.SetearParametro("@nombre", nuevo.Nombre);
                data.SetearParametro("@apellido", nuevo.Apellido);
                data.SetearParametro("@email", nuevo.Email);
                data.SetearParametro("@dni", nuevo.Dni);
                data.SetearParametro("@password", nuevo.Password);
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

        public bool Loguear(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, Nombre, Apellido, DNI, Email, Password, Rol FROM Usuario WHERE Email = @email AND Password = @password AND Activo=1;");
                datos.SetearParametro("@email", user.Email);
                datos.SetearParametro("@password", user.Password);

                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    user.Id = (int)datos.Lector["Id"];
                    user.Nombre = (string)datos.Lector["Nombre"];
                    user.Apellido = (string)datos.Lector["Apellido"];
                    user.Email = (string)datos.Lector["Email"];
                    user.Dni = int.Parse(datos.Lector["DNI"].ToString());
                    user.Password = (string)datos.Lector["Password"];
                    user.Rol = (string)datos.Lector["Rol"];
                    
                    return true; 
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
