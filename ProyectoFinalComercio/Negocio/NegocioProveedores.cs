using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    internal class NegocioProveedores
    {
        public void AgregarProveedores(Proveedor nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("INSERT INTO Proveedor (@RazonSocial, @Cuit, @Email, @Telefono, @Direccion) " +
                "VALUES ('@RazonSocia', '@Cuit', '@Email', '@Telefono0', ' @Direccion');");
                data.SetearParametro("@RazonSocial", nuevo.razonSocial);
                data.SetearParametro("@Cuit", nuevo.cuit);
                data.SetearParametro("@Email", nuevo.email);
                data.SetearParametro("@Telefono", nuevo.telefono);
                data.SetearParametro("@Direccion", nuevo.direccion);
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
        public void ModificarProveedores(Proveedor nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE Proveedor SET RazonSocial = @RazonSocial, Cuit = @Cuit, Email = @Email, Telefono = @Telefono, Direccion = @Direccion WHERE Id = @id;");
                data.SetearParametro("@RazonSocial", nuevo.razonSocial);
                data.SetearParametro("@Cuit", nuevo.cuit);
                data.SetearParametro("@Email", nuevo.email);
                data.SetearParametro("@Telefono", nuevo.telefono);
                data.SetearParametro("@Direccion", nuevo.direccion);
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
        public void EliminarProveedores(Proveedor nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("DELETE FROM Proveedor WHERE Id = @id;");
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
