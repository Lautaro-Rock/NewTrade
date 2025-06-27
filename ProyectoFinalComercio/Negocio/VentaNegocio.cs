using System;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Negocio
{
    public class VentaNegocio
    {
        public void AgregarVenta(Venta venta)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta(@"INSERT INTO VENTA (IdCliente, Fecha, IdUsuario, NumeroFactura, Total, Activo)
                                      VALUES (@IdCliente, @Fecha, @IdUsuario, @NumeroFactura, @Total, 1)");
                data.SetearParametro("@IdCliente", venta.Cliente.Id);
                data.SetearParametro("@Fecha", venta.Fecha);
                data.SetearParametro("@IdUsuario", venta.Usuario.Id);
                data.SetearParametro("@NumeroFactura", venta.NumeroFactura);
                data.SetearParametro("@Total", venta.Total);

                data.EjecutarAccion(); // Opcional: podés usar ExecuteScalar y recuperar el ID si querés insertar los detalles después.
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

        public List<Venta> ListarVentas()
        {
            AccesoDatos data = new AccesoDatos();
            List<Venta> lista = new List<Venta>();

            try
            {
                data.SetearConsulta("SELECT IdVenta, Fecha, NumeroFactura, Total FROM VENTA WHERE Activo = 1");
                data.EjecutarLectura();

                while (data.Lector.Read())
                {
                    Venta venta = new Venta
                    {
                        Id = (int)data.Lector["IdVenta"],
                        Fecha = (DateTime)data.Lector["Fecha"],
                        NumeroFactura = (string)data.Lector["NumeroFactura"],
                        Total = (decimal)data.Lector["Total"],
                        Activo = true
                    };
                    lista.Add(venta);
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

        public void BajaLogicaVenta(Venta venta)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE VENTA SET Activo = 0 WHERE IdVenta = @Id");
                data.SetearParametro("@Id", venta.Id);
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