using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class NegocioDetalleVenta
    {
        public void AgregarDetalle(DetalleVenta detalle, int idVenta)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta(@"INSERT INTO DETALLEVENTA (IdVenta, IdProducto, Cantidad, PrecioUnitario, Activo) 
                                      VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario, 1)");
                data.SetearParametro("@IdVenta", idVenta);
                data.SetearParametro("@IdProducto", detalle.Producto.Id);
                data.SetearParametro("@Cantidad", detalle.Cantidad);
                data.SetearParametro("@PrecioUnitario", detalle.PrecioUnitario);

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

        public List<DetalleVenta> ListarPorVenta(int idVenta)
        {
            AccesoDatos data = new AccesoDatos();
            List<DetalleVenta> lista = new List<DetalleVenta>();
            try
            {
                data.SetearConsulta(@"SELECT DV.IdDetalleVenta, DV.Cantidad, DV.PrecioUnitario, P.Id AS IdProducto, P.Nombre 
                                    FROM DETALLEVENTA DV
                                    JOIN PRODUCTO P ON DV.IdProducto = P.Id
                                    WHERE DV.IdVenta = @IdVenta AND DV.Activo = 1
                                    ");
                data.SetearParametro("@IdVenta", idVenta);
                data.EjecutarLectura();

                while (data.Lector.Read())
                {
                    DetalleVenta det = new DetalleVenta();

                    det.IdDetalleVenta = (int)data.Lector["IdDetalleVenta"];
                    det.Producto = new Producto();
                    det.Producto.Id = (int)data.Lector["IdProducto"];
                    det.Cantidad = (int)data.Lector["Cantidad"];
                    det.PrecioUnitario = (decimal)data.Lector["PrecioUnitario"];
                    det.Activo = true;

                    lista.Add(det);
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

        public void BajaLogica(DetalleVenta det)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetearConsulta("UPDATE DETALLEVENTA SET Activo = 0 WHERE IdDetalleVenta = @Id");
                data.SetearParametro("@Id", det.IdDetalleVenta);
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
