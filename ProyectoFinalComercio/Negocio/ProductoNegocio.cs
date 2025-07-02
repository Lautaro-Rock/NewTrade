using Dominio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> ListarProductos()
        {
            AccesoDatos data = new AccesoDatos();
            List<Producto> lista = new List<Producto>();
            try
            {
                data.SetearConsulta("SELECT P.Id AS ID, P.Nombre, M.Nombre AS Marca, P.Precio, P.Stock, P.StockMinimo, P.UrlImgProducto, P.PorcentajeGanancia, T.Nombre AS Categoria " +
                    "FROM Producto P " +
                    "INNER JOIN Marca M ON M.Id = P.IdMarca " +
                    "INNER JOIN TipoProducto T ON T.Id = P.IdTipoProducto " +
                    "WHERE P.Activo = 1;");
                data.EjecutarLectura();
                while (data.Lector.Read())
                {
                    Producto nuevo = new Producto();

                    nuevo.Id = (int)data.Lector["Id"];
                    nuevo.Nombre = (string)data.Lector["Nombre"];
                    nuevo.Marca = new Marca();
                    nuevo.Marca.Nombre = (string)data.Lector["Marca"];
                    nuevo.Precio = data.Lector["Precio"] != DBNull.Value ? Convert.ToDecimal(data.Lector["Precio"]) : 0m;
                    nuevo.Stock = (int)data.Lector["Stock"];
                    nuevo.StockMin = (int)data.Lector["StockMinimo"];
                    nuevo.UrlImgProducto = (string)data.Lector["UrlImgProducto"];
                    nuevo.PorcentajeGanancia = data.Lector["PorcentajeGanancia"] != DBNull.Value ? Convert.ToDecimal(data.Lector["PorcentajeGanancia"]) : (decimal?)null;
                    nuevo.TipoProducto = new TipoProducto();
                    nuevo.TipoProducto.Nombre = (string)data.Lector["Categoria"];

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

        public List<Producto> ListarProductosxProveedor(int id_proveedor)
        {
            AccesoDatos data = new AccesoDatos();
            List<Producto> lista = new List<Producto>();
            try
            {
                data.SetearConsulta(
           "SELECT P.Id AS ID, P.Nombre, M.Nombre AS Marca, P.Precio, P.Stock, P.StockMinimo, P.UrlImgProducto, P.PorcentajeGanancia, T.Nombre AS Categoria " +
           "FROM Producto P " +
           "INNER JOIN ProductoProveedor PP ON PP.IdProducto = P.Id " +
           "INNER JOIN Marca M ON M.Id = P.IdMarca " +
           "INNER JOIN TipoProducto T ON T.Id = P.IdTipoProducto " +
           "WHERE PP.IdProveedor = " + id_proveedor + " AND P.Activo = 1"
       );
                data.EjecutarLectura();
                while (data.Lector.Read())
                {
                    Producto nuevo = new Producto();

                    nuevo.Id = (int)data.Lector["Id"];
                    nuevo.Nombre = (string)data.Lector["Nombre"];
                    nuevo.Marca = new Marca();
                    nuevo.Marca.Nombre = (string)data.Lector["Marca"];
                    nuevo.Precio = data.Lector["Precio"] != DBNull.Value ? Convert.ToDecimal(data.Lector["Precio"]) : 0m;
                    nuevo.Stock = (int)data.Lector["Stock"];
                    nuevo.StockMin = (int)data.Lector["StockMinimo"];
                    nuevo.UrlImgProducto = (string)data.Lector["UrlImgProducto"];
                    nuevo.PorcentajeGanancia = data.Lector["PorcentajeGanancia"] != DBNull.Value ? Convert.ToDecimal(data.Lector["PorcentajeGanancia"]) : (decimal?)null;
                    nuevo.TipoProducto = new TipoProducto();
                    nuevo.TipoProducto.Nombre = (string)data.Lector["Categoria"];

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

        public List<Producto> Fitrar(string campo, string criterio, string filtro, string estado)
        {

            List<Producto> list_filtrada = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT P.Id AS ID, P.Nombre, M.Nombre AS Marca, P.Precio, P.Stock, P.StockMinimo, P.UrlImgProducto, T.Nombre AS Categoria " +
                    "FROM Producto P " +
                    "INNER JOIN Marca M ON M.Id = P.IdMarca " +
                    "INNER JOIN TipoProducto T ON T.Id = P.IdTipoProducto " +
                    "WHERE ";

                if (campo == "Por nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "P.Nombre like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "P.Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "P.Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Por marca")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "M.Nombre like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "M.Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "M.Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Por tipo")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "T.Nombre like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "T.Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "T.Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Por precio")
                {
                    switch (criterio)
                    {
                        case "Igual a":
                            consulta += "P.Precio = " + filtro;
                            break;
                        case "Mayor a":
                            consulta += "P.Precio > " + filtro;
                            break;
                        default:
                            consulta += "P.Precio < " + filtro;
                            break;
                    }
                }

                if (estado == "Solo los activos")
                {
                    consulta += " AND P.Activo = 1";
                }
                else if (estado == "Solo los inactivos")
                {
                    consulta += " AND P.Activo = 0";
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Marca = new Marca();
                    aux.Marca.Nombre = (string)datos.Lector["Marca"];
                    aux.Precio = datos.Lector["Precio"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["Precio"]) : 0m;
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.StockMin = (int)datos.Lector["StockMinimo"];
                    aux.UrlImgProducto = (string)datos.Lector["UrlImgProducto"];
                    aux.TipoProducto = new TipoProducto();
                    aux.TipoProducto.Nombre = (string)datos.Lector["Categoria"];
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


        public int AgregarProductos(Producto nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {

                data.SetearConsulta("INSERT INTO Producto (Nombre, IdMarca, Precio, Stock, StockMinimo, IdTipoProducto, UrlImgProducto, Activo, PorcentajeGanancia) " +
                    "VALUES (@Nombre, @IdMarca, @Precio, @Stock, @StockMinimo, @IdTipoProducto, @UrlImgProducto, @Activo, @PorcentajeGanancia);"+
                    "SELECT SCOPE_IDENTITY();");
                data.SetearParametro("@Nombre", nuevo.Nombre);
                data.SetearParametro("@IdMarca", nuevo.Marca.Id);
                data.SetearParametro("@Precio", nuevo.Precio.ToString(CultureInfo.InvariantCulture));
                data.SetearParametro("@Stock", nuevo.Stock);
                data.SetearParametro("@StockMinimo", nuevo.StockMin);
                data.SetearParametro("@IdTipoProducto", nuevo.TipoProducto.Id);
                data.SetearParametro("@UrlImgProducto", nuevo.UrlImgProducto ?? (object)DBNull.Value);
                data.SetearParametro("@Activo", nuevo.Activo);
                data.SetearParametro("@PorcentajeGanancia", nuevo.PorcentajeGanancia ?? (object)DBNull.Value);

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
        public void ModificarProducto(Producto nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE Producto SET Nombre = @Nombre, Precio = @Precio, Stock = @Stock, StockMinimo = @StockMinimo, IdMarca = @IdMarca, IdTipoProducto = @IdTipoProducto, PorcentajeGanancia = @PorcentajeGanancia WHERE Id = @id;");
                data.SetearParametro("@Nombre", nuevo.Nombre);
                data.SetearParametro("@Precio", nuevo.Precio.ToString(CultureInfo.InvariantCulture));
                data.SetearParametro("@Stock", nuevo.Stock);
                data.SetearParametro("@StockMinimo", nuevo.StockMin);
                data.SetearParametro("@IdMarca", nuevo.Marca.Id);
                data.SetearParametro("@IdTipoProducto", nuevo.TipoProducto.Id);
                data.SetearParametro("@PorcentajeGanancia", nuevo.PorcentajeGanancia ?? (object)DBNull.Value);
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
        public void EliminarProducto(Producto nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("DELETE FROM Producto WHERE Id = @id;");
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

        public void EliminarProductoLogico(Producto nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE Producto SET Activo = 0 WHERE Id = @id;");
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

        public void AsociarProductoAProveedores(int id_producto, List<int> ids_proveedores)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {

                foreach (int id_proveedor in ids_proveedores)
                {
                    data.SetearConsulta("INSERT INTO ProductoProveedor (IdProducto, IdProveedor) VALUES (@IdProducto, @IdProveedor);");
                    data.SetearParametro("@IdProducto", id_producto);
                    data.SetearParametro("@IdProveedor", id_proveedor);
                    data.EjecutarAccion();
                }
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

        public List<int> ObtenerIdsProveedoresPorProducto(int idProducto)
        {
            List<int> ids = new List<int>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT IdProveedor FROM ProductoProveedor WHERE IdProducto = @idProducto");
                datos.SetearParametro("@idProducto", idProducto);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                    ids.Add((int)datos.Lector["IdProveedor"]);

                return ids;
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

        public void EliminarAsociacionesProducto(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("DELETE FROM ProductoProveedor WHERE IdProducto = @idProducto");
                datos.SetearParametro("@idProducto", idProducto);
                datos.EjecutarAccion();
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


    }
}
