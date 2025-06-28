using System;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Negocio
{
    public class VentaNegocio
    {

        public void AgregarVentaCompleta(Venta venta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.AbrirConexion();
                datos.ComenzarTransaccion();

                // Insertar la venta principal
                datos.SetearConsulta(@"
                INSERT INTO VENTA (IdCliente, Fecha, IdVendedor, NumeroFactura, Total, Activo)
                VALUES (@IdCliente, @Fecha, @IdUsuario, @NumeroFactura, @Total, 1);
                SELECT SCOPE_IDENTITY();");

                datos.SetearParametro("@IdCliente", venta.Cliente.Id);
                datos.SetearParametro("@Fecha", venta.Fecha);
                datos.SetearParametro("@IdUsuario", venta.Usuario.Id);
                datos.SetearParametro("@NumeroFactura", venta.NumeroFactura);
                datos.SetearParametro("@Total", venta.Total);

                int idVenta = Convert.ToInt32(datos.EjecutarScalar());

                // Insertar cada detalle
                foreach (var d in venta.DetalleList)
                {
                    datos.SetearConsulta(@"
                    INSERT INTO DETALLEVENTA (IdVenta, IdProducto, Cantidad, PrecioUnitario, Activo)
                    VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario, 1)");

                    datos.SetearParametro("@IdVenta", idVenta);
                    datos.SetearParametro("@IdProducto", d.Producto.Id);
                    datos.SetearParametro("@Cantidad", d.Cantidad);
                    datos.SetearParametro("@PrecioUnitario", d.PrecioUnitario);

                    datos.EjecutarAccion();

                    // Actualizar stock en PRODUCTO
                    datos.SetearConsulta("UPDATE PRODUCTO SET Stock = Stock - @Cantidad WHERE Id = @IdProducto");
                    datos.SetearParametro("@Cantidad", d.Cantidad);
                    datos.SetearParametro("@IdProducto", d.Producto.Id);

                    datos.EjecutarAccion();
                }

                datos.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                datos.RollbackTransaccion();
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

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
            List<Venta> lista = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(@"
                SELECT V.Id, V.NumeroFactura, V.Fecha, V.Total,
                       C.IdCliente AS IdCliente, C.Nombre AS NombreCliente, C.Apellido AS ApellidoCliente,
                       U.Id AS IdUsuario, U.Nombre AS NombreUsuario, U.Apellido AS ApellidoUsuario
                FROM Venta V
                INNER JOIN Cliente C ON V.IdCliente = C.IdCliente
                INNER JOIN Usuario U ON V.IdVendedor = U.Id
                WHERE V.Activo = 1
                ORDER BY V.Fecha DESC");

                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta venta = new Venta();
                    venta.Id = (int)datos.Lector["Id"];
                    venta.NumeroFactura = datos.Lector["NumeroFactura"].ToString();
                    venta.Fecha = (DateTime)datos.Lector["Fecha"];
                    venta.Total = (decimal)datos.Lector["Total"];

                    venta.Cliente = new Cliente()
                    {
                        Id = (int)datos.Lector["IdCliente"],
                        Nombre = datos.Lector["NombreCliente"].ToString(),
                        Apellido = datos.Lector["ApellidoCliente"].ToString()
                    };

                    venta.Usuario = new Usuario()
                    {
                        Id = (int)datos.Lector["IdUsuario"],
                        Nombre = datos.Lector["NombreUsuario"].ToString(),
                        Apellido = datos.Lector["ApellidoUsuario"].ToString()
                    };

                    lista.Add(venta);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar ventas: " + ex.Message, ex);
            }
            finally
            {
                datos.CerrarConexion();
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

        public int ObtenerUltimoNumeroFactura()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT ISNULL(MAX(CAST(NumeroFactura AS INT)), 0) FROM VENTA");
                datos.AbrirConexion();
                return Convert.ToInt32(datos.EjecutarScalar());
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


    }
}