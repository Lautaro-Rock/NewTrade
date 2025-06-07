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
        public void AgregarTipoProducto(TipoProducto nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
               data.SetearConsulta("INSERT INTO TipoProducto (Nombre) VALUES (@nombre);");
                data.SetearParametro("@nombre", nuevo.nombre);
                data.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }finally
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
                data.SetearParametro("@nombre", nuevo.nombre);
                data.SetearParametro("@id", nuevo.id);
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
                data.SetearParametro("@id", nuevo.id);
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
