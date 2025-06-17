using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioTipoProducto
    {

        public List<TipoProducto> ListarTiposDeProductos()
        {
            AccesoDatos data = new AccesoDatos();
            List<TipoProducto> list_tipos_de_productos = new List<TipoProducto>();
            try
            {
                data.SetearConsulta("SELECT Id, Nombre FROM TipoProducto;");
                data.EjecutarLectura();
                while (data.Lector.Read())
                {
                    TipoProducto aux = new TipoProducto();
                    aux.Id = (int)data.Lector["Id"];
                    aux.Nombre = (string)data.Lector["Nombre"];
                    list_tipos_de_productos.Add(aux);
                }
                return list_tipos_de_productos;
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



        public void AgregarTipoProducto(TipoProducto nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("INSERT INTO TipoProducto (Nombre) VALUES (@nombre);");
                data.SetearParametro("@nombre", nuevo.Nombre);
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
        public void ModificarTipoProducto(TipoProducto nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE TipoProducto SET Nombre = @nombre WHERE Id = @id;");
                data.SetearParametro("@nombre", nuevo.Nombre);
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
        public void EliminarTipoProducto(TipoProducto nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("DELETE FROM TipoProducto WHERE Id = @id;");
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

        public void EliminarTipoProductoLogico(TipoProducto nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE TipoProducto SET Activo = 0 WHERE Id = @id;");
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
