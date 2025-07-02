using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioProveedores
    {
        public List<Proveedor> ListarProveedores()
        {
            AccesoDatos data = new AccesoDatos();
            List<Proveedor> lista = new List<Proveedor>();
            try
            {
                data.SetearConsulta("SELECT Id, RazonSocial, Cuit, Email, Telefono, Direccion FROM Proveedor Where Activo = 1;");
                data.EjecutarLectura();
                while (data.Lector.Read())
                {
                    Proveedor nuevo = new Proveedor
                    {
                        Id = (int)data.Lector["Id"],
                        RazonSocial = (string)data.Lector["RazonSocial"],
                        Cuit = (string)data.Lector["Cuit"],
                        Email = (string)data.Lector["Email"],
                        Telefono = (string)data.Lector["Telefono"],
                        Direccion = (string)data.Lector["Direccion"]
                    };
                    lista.Add(nuevo);
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

        public List<Proveedor> Fitrar(string campo, string criterio, string filtro, string estado)
        {

            List<Proveedor> list_filtrada = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Id, RazonSocial, Cuit, Email, Telefono, Direccion, Activo FROM Proveedor WHERE ";

                if (campo == "RazonSocial" || campo == "Telefono" || campo == "Direccion")
                {                   
                    switch (criterio)
                    {
                        case "Contiene":
                            consulta += campo + " LIKE '%" + filtro + "%'";
                            break;
                    
                        case "Comienza con":
                            consulta += campo + " LIKE '" + filtro + "%' ";
                            break;

                        case "Termina con":
                            consulta += campo + " LIKE '%" + filtro + "'";
                            break;
                        default:
                            consulta += campo + " = " + filtro;
                            break;
                    }
                }
                else if (campo == "Email")
                {
                    switch (criterio)
                    {
                        case "Contiene":
                            consulta += campo + " LIKE '%" + filtro + "%'";
                            break;

                        case "Comienza con":
                            consulta += campo + " LIKE '" + filtro + "%' ";
                            break;

                        case "Termina con":
                            consulta += campo + " LIKE '%" + filtro + "'";
                            break;

                        case "Es igual a":
                            consulta += campo + " = " + filtro;
                            break;

                        default:
                            consulta += campo + " LIKE '%@" + filtro + "'";
                            break;
                    }
                }

                else
                {
                    switch (criterio)
                    {
                        case "Es igual a":
                            consulta += campo +  " = " + filtro;
                            break;

                        case "Comienza con":
                            consulta += campo + " LIKE '" + filtro + "%' ";
                            break;

                        default:
                            consulta += campo + " LIKE '%" + filtro + "%'"; 
                            break;
                    }
                }
                if (estado == "Solamente Activos")
                {
                    consulta += " AND Activo = 1";
                }
                else if (estado == "Solamente Inactivos")
                {
                    consulta += " AND Activo = 0";
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Proveedor aux= new Proveedor();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.RazonSocial = (string)datos.Lector["RazonSocial"];
                    aux.Cuit = (string)datos.Lector["Cuit"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Direccion = (string)datos.Lector["Direccion"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    list_filtrada.Add(aux);
                }
                return list_filtrada;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al aplicar filtros: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public int AgregarProveedores(Proveedor proveedor)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta(
                    "INSERT INTO Proveedor (RazonSocial, Direccion, Cuit, Email, Telefono, Activo) " +
                    "VALUES (@RazonSocial, @Direccion, @Cuit, @Email, @Telefono, 1); " +
                    "SELECT SCOPE_IDENTITY();"
                );

                data.SetearParametro("@RazonSocial", proveedor.RazonSocial);
                data.SetearParametro("@Direccion", proveedor.Direccion);
                data.SetearParametro("@Cuit", proveedor.Cuit);
                data.SetearParametro("@Email", proveedor.Email);
                data.SetearParametro("@Telefono", proveedor.Telefono);

                int id_insertado = Convert.ToInt32(data.EjecutarScalar());
                return id_insertado;
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

        public void ModificarProveedores(Proveedor nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE Proveedor SET RazonSocial = @RazonSocial, Cuit = @Cuit, Email = @Email, Telefono = @Telefono, Direccion = @Direccion WHERE Id = @id;");
                data.SetearParametro("@RazonSocial", nuevo.RazonSocial);
                data.SetearParametro("@Cuit", nuevo.Cuit);
                data.SetearParametro("@Email", nuevo.Email);
                data.SetearParametro("@Telefono", nuevo.Telefono);
                data.SetearParametro("@Direccion", nuevo.Direccion);
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
        public void EliminarProveedores(Proveedor nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("DELETE FROM Proveedor WHERE Id = @id;");
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

        public void EliminarProveedoresLogico(Proveedor nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE Proveedor SET Activo = 0 WHERE Id = @id;");
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
        public List<Proveedor> FiltrarProveedores(string campo, string criterio, string filtro, string estado)
        {

            List<Proveedor> list_filtrada = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Id, RazonSocial, Cuit, Email, Telefono, Direccion, Activo FROM Proveedor Where 1=1 ";

                if (campo == "Razon Social")
                {
                    switch (criterio)
                    {
                        case "Igual a":
                            consulta += " AND RazonSocial = '" + filtro + "'";
                            break;
                        case "Comienza con":
                            consulta += " AND RazonSocial like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += " AND RazonSocial like '%" + filtro + "'";
                            break;
                    }
                }
                else if (campo == "Cuit")
                {
                    switch (criterio)
                    {
                        case "Igual a":
                            consulta += " AND Cuit = '" + filtro + "'";
                            break;
                        case "Comienza con":
                            consulta += " AND Cuit like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += " AND Cuit like '%" + filtro + "'";
                            break;

                    }
                }
                else if (campo == "Email")
                {
                    switch (criterio)
                    {
                        case "Igual a":
                            consulta += " AND Email = '" + filtro + "'";
                            break;
                    }
                }
                else if (campo == "Direccion")
                {
                    switch (criterio)
                    {
                        case "Igual a":
                            consulta += " AND Direccion = '" + filtro + "'";
                            break;
                        case "Comienza con":
                            consulta += " AND Cuit Direccion '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += " AND Cuit Direccion '%" + filtro + "'";
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
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.RazonSocial = (string)datos.Lector["RazonSocial"];
                    aux.Cuit = (string)datos.Lector["Cuit"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    aux.Direccion = (string)datos.Lector["Direccion"];

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

        public List<int> ObtenerProductosAsociados(int idProveedor)
        {
            List<int> ids = new List<int>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT IdProducto FROM ProductoProveedor WHERE IdProveedor = @id");
                datos.SetearParametro("@id", idProveedor);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                    ids.Add((int)datos.Lector["IdProducto"]);
                return ids;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.CerrarConexion(); }

        }

        public void EliminarProductosAsociados(int idProveedor)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("DELETE FROM ProductoProveedor WHERE IdProveedor = @id");
                datos.SetearParametro("@id", idProveedor);
                datos.EjecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.CerrarConexion(); }
        }

        public void AsociarProductos(int idProveedor, List<int> idsProductos)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                foreach (int id in idsProductos)
                {
                    datos.SetearConsulta("INSERT INTO ProductoProveedor (IdProducto, IdProveedor) VALUES (@idProd, @idProv)");
                    datos.SetearParametro("@idProd", id);
                    datos.SetearParametro("@idProv", idProveedor);
                    datos.EjecutarAccion();
                }
            }
            catch (Exception ex) { throw ex; }
            finally { datos.CerrarConexion(); }
        }


    }
}
