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
                data.SetearConsulta("SELECT  IdCliente, Nombre, Apellido, DNI, Email, Rol FROM CLIENTE WHERE Rol='Cliente' AND Activo=1;");
                data.EjecutarLectura();
                while (data.Lector.Read())
                {
                    Cliente user = new Cliente
                    {
                        Id = (int)data.Lector["IdCliente"],
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

        public List<Cliente> FiltrarCliente(string campo, string criterio, string filtro, string estado)
        {

            List<Cliente> list_filtrada = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT IdCliente, Nombre, Apellido, DNI, Email, Rol, Activo FROM CLIENTE WHERE 1=1 ";

                if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += " AND Nombre like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += " AND Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += " AND Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "DNI")
                {
                    switch (criterio)
                    {
                        case "Igual a":
                            consulta += " AND DNI = '" + filtro + "'";
                            break;
                    }
                }


                if (estado == "Activo")
                {
                    consulta += " AND Activo = 1";
                }
                else if (estado == "Inactivo")
                {
                    consulta += " AND Activo = 0";
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.Id = (int)datos.Lector["IdCliente"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Rol = (string)datos.Lector["Rol"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    string dniStr = datos.Lector["DNI"] != DBNull.Value ? datos.Lector["DNI"].ToString() : null;
                    int dniInt = 0;
                    if (!string.IsNullOrEmpty(dniStr) && int.TryParse(dniStr, out int parsedDni))
                    {
                        dniInt = parsedDni;
                    }
                    else
                    {
                        dniInt = 0;
                    }
                    aux.Dni = dniInt;
                    list_filtrada.Add(aux);
                }
                return list_filtrada;

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


        public void AgregarCliente(Cliente nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("INSERT INTO CLIENTE (Nombre, Apellido, DNI, Email,Rol, Activo) " +
                 "VALUES (@Nombre, @Apellido, @DNI, @Email,@Rol, @Activo);");

                data.SetearParametro("@Nombre", nuevo.Nombre);
                data.SetearParametro("@Apellido", nuevo.Apellido);
                data.SetearParametro("@DNI", nuevo.Dni);
                data.SetearParametro("@Email", nuevo.Email);
                data.SetearParametro("@Rol", nuevo.Rol);
                data.SetearParametro("@Activo", 1);
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
                data.SetearConsulta("UPDATE CLIENTE SET Nombre = @Nombre, Apellido = @Apellido, DNI = @DNI, Email = @Email WHERE IdCliente = @IdCliente");
                data.SetearParametro("@IdCliente", edit.Id);
                data.SetearParametro("@Nombre", edit.Nombre);
                data.SetearParametro("@Apellido", edit.Apellido);
                data.SetearParametro("@DNI", edit.Dni);
                data.SetearParametro("@Email", edit.Email);
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

        public void DeleteClienteLogico(Cliente nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE CLIENTE SET Activo=0 WHERE IdCliente = @IdCliente;");
                data.SetearParametro("@IdCliente", nuevo.Id);
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