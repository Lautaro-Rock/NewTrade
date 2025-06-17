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
                data.SetearConsulta("SELECT Id, RazonSocial, Cuit, Email, Telefono, Direccion FROM Proveedor;");
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
        public void AgregarProveedores(Proveedor nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("INSERT INTO Proveedor (RazonSocial, Cuit, Email, Telefono, Direccion) " +
                 "VALUES (@RazonSocial, @Cuit, @Email, @Telefono, @Direccion);");

                data.SetearParametro("@RazonSocial", nuevo.RazonSocial);
                data.SetearParametro("@Cuit", nuevo.Cuit);
                data.SetearParametro("@Email", nuevo.Email);
                data.SetearParametro("@Telefono", nuevo.Telefono);
                data.SetearParametro("@Direccion", nuevo.Direccion);
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
    }
}
