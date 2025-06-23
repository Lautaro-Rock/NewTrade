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
                data.SetearConsulta("SELECT P.Id AS ID, P.Nombre, M.Nombre AS Marca, P.Precio, P.Stock, P.StockMinimo, P.UrlImgProducto, T.Nombre AS Categoria " +
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


        public void AgregarProductos(Producto nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {

                data.SetearConsulta("INSERT INTO Producto (Nombre, IdMarca, Precio, Stock, StockMinimo, IdTipoProducto, UrlImgProducto, Activo) " +
                    "VALUES (@Nombre, @IdMarca, @Precio, @Stock, @StockMinimo, @IdTipoProducto, @UrlImgProducto, @Activo);");
                data.SetearParametro("@Nombre", nuevo.Nombre);
                data.SetearParametro("@IdMarca", nuevo.Marca.Id);
                data.SetearParametro("@Precio", nuevo.Precio.ToString(CultureInfo.InvariantCulture));
                data.SetearParametro("@Stock", nuevo.Stock);
                data.SetearParametro("@StockMinimo", nuevo.StockMin);
                data.SetearParametro("@IdTipoProducto", nuevo.TipoProducto.Id);
                data.SetearParametro("@UrlImgProducto", nuevo.UrlImgProducto ?? (object)DBNull.Value);
                data.SetearParametro("@Activo", nuevo.Activo);
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
        public void ModificarProducto(Producto nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE Producto SET Nombre = @Nombre, Precio = @Precio, Stock = @Stock, StockMinimo = @StockMinimo, IdMarca = @IdMarca, IdTipoProducto = @IdTipoProducto WHERE Id = @id;");
                data.SetearParametro("@Nombre", nuevo.Nombre);
                data.SetearParametro("@Precio", nuevo.Precio.ToString(CultureInfo.InvariantCulture));
                data.SetearParametro("@Stock", nuevo.Stock);
                data.SetearParametro("@StockMinimo", nuevo.StockMin);
                data.SetearParametro("@IdMarca", nuevo.Marca.Id);
                data.SetearParametro("@IdTipoProducto", nuevo.TipoProducto.Id);
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
    }
}
