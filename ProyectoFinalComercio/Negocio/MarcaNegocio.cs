using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> ListarMarcas()
        {
            AccesoDatos data = new AccesoDatos();
            List<Marca> list_de_marcas = new List<Marca>();
            try
            {
                data.SetearConsulta("SELECT Id, Nombre FROM Marca;");
                data.EjecutarLectura();
                while (data.Lector.Read())
                {
                    Marca aux= new Marca();
                    aux.Id = (int)data.Lector["Id"];
                    aux.Nombre = (string)data.Lector["Nombre"];
                    list_de_marcas.Add(aux);
                }
                return list_de_marcas;
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









        public void AgregarMarca(Marca nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("INSERT INTO Marca (Nombre) VALUES (@nombre);");
                data.SetearParametro("@nombre", nuevo.Nombre);
                data.EjecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                data.CerrarConexion();
            }
        }

        public void ModificarMarca(Marca nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE Marca SET Nombre = @nombre WHERE Id = @id;");
                data.SetearParametro("@nombre", nuevo.Nombre);
                data.SetearParametro("@id", nuevo.Id);
                data.EjecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                data.CerrarConexion();
            }
        }

        public void EliminarMarca(Marca nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("DELETE FROM Marca WHERE Id = @id;");
                data.SetearParametro("@id", nuevo.Id);
                data.EjecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                data.CerrarConexion();
            }
        }
    }
}

