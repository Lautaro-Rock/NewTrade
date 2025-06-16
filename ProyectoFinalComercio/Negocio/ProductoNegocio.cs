using Dominio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
                    "INNER JOIN Marca M ON P.IdMarca = M.Id " +
                    "INNER JOIN TipoProducto T ON T.Id = P.IdTipoProducto;" +
                    "WHERE P.Activo = 1;");
                data.EjecutarLectura();
                while (data.conexionDataReader.Read())
                {
                    Producto nuevo = new Producto();

                    nuevo.Id = (int)data.conexionDataReader["Id"];
                    nuevo.Nombre = (string)data.conexionDataReader["Nombre"];
                    nuevo.Marca = new Marca();
                    nuevo.Marca.Nombre = (string)data.conexionDataReader["Marca"];
                    nuevo.Precio = data.ConexionDataReader["Precio"] != DBNull.Value ? Convert.ToDecimal(data.ConexionDataReader["Precio"]) : 0m;
                    nuevo.Stock = (int)data.conexionDataReader["Stock"];
                    nuevo.StockMin = (int)data.conexionDataReader["StockMinimo"];
                    nuevo.UrlImgProducto = (string)data.conexionDataReader["UrlImgProducto"];
                    nuevo.TipoProducto = new TipoProducto();
                    nuevo.TipoProducto.Nombre = (string)data.conexionDataReader["Categoria"];

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

       


        public void AgregarProductos(Producto nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("INSERT INTO Producto (Nombre, IdMarca, Precio, Stock, StockMinimo, IdTipoProducto) " +
                    "VALUES('" + nuevo.Nombre + "', " + nuevo.Marca.Id + ", " + nuevo.Precio.ToString(CultureInfo.InvariantCulture) + ", " + nuevo.Stock + ", " + nuevo.StockMin + ", " + nuevo.TipoProducto + ");"); 
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
        public void ModificarProveedores(Producto nuevo)
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
