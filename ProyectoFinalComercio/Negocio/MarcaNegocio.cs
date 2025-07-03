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
                data.SetearConsulta("SELECT Id, Nombre, Activo FROM Marca");
                data.EjecutarLectura();
                while (data.Lector.Read())
                {
                    Marca aux= new Marca();
                    aux.Id = (int)data.Lector["Id"];
                    aux.Nombre = (string)data.Lector["Nombre"];
                    aux.Activo = (bool)data.Lector["Activo"];
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

        public List<Marca> FitrarMarcas(string campo, string criterio, string filtro, string estado)
        {

            List<Marca> list_filtrada = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Id, Nombre, Activo " +
                    "FROM Marca " +
                    "WHERE " ;

                if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        
                        case "Comienza con":
                            consulta += "Nombre like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;

                        default:                            
                            consulta += "Nombre = '" + filtro + "' ";
                            break;
                    }
                }
                
                else if (campo == "Id")
                {
                    switch (criterio)
                    {
                        case "Igual a":
                            consulta += "Id = " + filtro;
                            break;
                        case "Mayor a":
                            consulta += "Id > " + filtro;
                            break;
                        default:
                            consulta += "Id < " + filtro;
                            break;
                    }
                }

                if (estado != "Todos")
                {
                    if (estado == "Activos")
                        consulta += " AND Activo = 1";
                    else if (estado == "Inactivos")
                        consulta += " AND Activo = 0";
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Activo = (bool)datos.Lector["Activo"];
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

        public void EliminarMarcaLogico(Marca nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE Marca SET Activo = 0 WHERE Id = @id;");
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

