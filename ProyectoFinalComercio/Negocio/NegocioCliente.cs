using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioCliente
    {
        public List<Cliente> ListarClientes()
        {
            AccesoDatos data = new AccesoDatos();
            List<Cliente> lista = new List<Cliente>();
            try
            {
                data.SetearConsulta("SELECT  Nombre, Apellido, DNI, Email, Rol FROM CLIENTE WHERE Rol='Cliente';");
                data.EjecutarLectura();
                while (data.Lector.Read())
                {
                    Cliente user = new Cliente
                    {
                        Nombre = (string)data.Lector["Nombre"],
                        Apellido = (string)data.Lector["Apellido"],
                        Dni = int.Parse(data.Lector["Dni"].ToString()),
                        Email = (string)data.Lector["Email"],
                        Rol = (string)data.Lector["Rol"],
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

        public void AgregarCliente(Cliente nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("INSERT INTO CLIENTE (Nombre, Apellido, DNI, Email,Rol) " +
                 "VALUES (@Nombre, @Apellido, @DNI, @Email,@Rol);");

                data.SetearParametro("@Nombre", nuevo.Nombre);
                data.SetearParametro("@Apellido", nuevo.Apellido);
                data.SetearParametro("@DNI", nuevo.Dni);
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

        public void EditarCliente(Cliente edit)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE CLIENTE SET Nombre = @Nombre, Apellido = @Apellido, DNI = @DNI, Email = @Email, Rol=@Rol WHERE Email = @Email AND Rol=@Rol");
                data.SetearParametro("@Nombre", edit.Nombre);
                data.SetearParametro("@Apellido", edit.Apellido);
                data.SetearParametro("@DNI", edit.Dni);
                data.SetearParametro("@Email", edit.Email);
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

        public void DeleteCliente(Cliente nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("DELETE FROM CLIENTE WHERE Email = @Email AND Rol=@Rol;");
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

    }
}
