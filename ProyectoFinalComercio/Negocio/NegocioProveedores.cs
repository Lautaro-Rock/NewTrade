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
                while (data.conexionDataReader.Read())
                {
                    Proveedor nuevo = new Proveedor
                    {
                        Id = (int)data.conexionDataReader["Id"],
                        RazonSocial = (string)data.conexionDataReader["RazonSocial"],
                        Cuit = (string)data.conexionDataReader["Cuit"],
                        Email = (string)data.conexionDataReader["Email"],
                        Telefono = (string)data.conexionDataReader["Telefono"],
                        Direccion = (string)data.conexionDataReader["Direccion"]
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
    }
}
