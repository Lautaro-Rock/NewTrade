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
        public List<Usuario> ListarClientes()
        {
            AccesoDatos data = new AccesoDatos();
            List<Usuario> lista = new List<Usuario>();
            try
            {
                data.SetearConsulta("SELECT Id, Nombre, Apellido, DNI, Email, Password, Rol FROM Usuario WHERE Rol='Cliente';");
                data.EjecutarLectura();
                while (data.conexionDataReader.Read())
                {
                    Usuario user = new Usuario
                    {
                        Nombre = (string)data.conexionDataReader["Nombre"],
                        Apellido = (string)data.conexionDataReader["Apellido"],
                        Dni = int.Parse(data.conexionDataReader["Dni"].ToString()),
                        Email = (string)data.conexionDataReader["Email"],
                        Password = (string)data.conexionDataReader["Password"],
                        Rol = (string)data.conexionDataReader["Rol"],
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

        public void AgregarUsuarioCliente(Usuario nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("INSERT INTO Usuario (Nombre, Apellido, DNI, Email, Password,Rol) " +
                 "VALUES (@Nombre, @Apellido, @DNI, @Email,@Password,@Rol);");

                data.SetearParametro("@Nombre", nuevo.Nombre);
                data.SetearParametro("@Apellido", nuevo.Apellido);
                data.SetearParametro("@DNI", nuevo.Dni);
                data.SetearParametro("@Email", nuevo.Email);
                data.SetearParametro("@Password", nuevo.Password);
                data.SetearParametro("@Rol", nuevo.Rol);
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

        public void EditarUsuarioCliente(Usuario edit)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, DNI = @DNI, Email = @Email, Password = @Password, Rol=@Rol WHERE Email = @Email; AND Rol=@Rol");
                data.SetearParametro("@Nombre", edit.Nombre);
                data.SetearParametro("@Apellido", edit.Apellido);
                data.SetearParametro("@DNI", edit.Dni);
                data.SetearParametro("@Email", edit.Email);
                data.SetearParametro("@Password", edit.Password);
                data.SetearParametro("@Rol", edit.Rol);
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

        public void DeleteCliente(Usuario nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("DELETE FROM Usuario WHERE Email = @Email AND Rol=@Rol;");
                data.SetearParametro("@Email", nuevo.Email);
                data.SetearParametro("@Rol", nuevo.Rol);
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
