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


        public void BajaLogicaVenta(int idVenta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.AbrirConexion();
                datos.ComenzarTransaccion();

                // Obtener detalles de la venta
                datos.SetearConsulta("SELECT IdProducto, Cantidad FROM DETALLEVENTA WHERE IdVenta = @IdVenta AND Activo = 1");
                datos.SetearParametro("@IdVenta", idVenta);
                datos.EjecutarLectura();

                var stockARevertir = new List<(int IdProducto, int Cantidad)>();
                while (datos.Lector.Read())
                {
                    stockARevertir.Add((
                        (int)datos.Lector["IdProducto"],
                        (int)datos.Lector["Cantidad"]
                    ));
                }
                datos.CerrarLector();

                // Restaurar stock
                foreach (var item in stockARevertir)
                {
                    datos.SetearConsulta("UPDATE PRODUCTO SET Stock = Stock + @Cantidad WHERE Id = @IdProducto");
                    datos.SetearParametro("@Cantidad", item.Cantidad);
                    datos.SetearParametro("@IdProducto", item.IdProducto);
                    datos.EjecutarAccion();
                }

                // Desactivar la venta
                datos.SetearConsulta("UPDATE VENTA SET Activo = 0 WHERE Id = @IdVenta");
                datos.SetearParametro("@IdVenta", idVenta);
                datos.EjecutarAccion();

                datos.ConfirmarTransaccion();
            }
            catch (Exception ex)
            {
                datos.RollbackTransaccion();
                throw new Exception("Error al anular la venta: " + ex.Message, ex);
            }
            finally
            {
                datos.CerrarConexion();
            }

        }

        public int ObtenerUltimoNumeroFactura()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT ISNULL(MAX(CAST(SUBSTRING(NumeroFactura, 8, 8) AS INT)), 0) FROM VENTA");
                datos.AbrirConexion();
                return Convert.ToInt32(datos.EjecutarScalar());
            }
            catch
            {
                return 0;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public Venta ObtenerVentaPorId(int id)
        {
            Venta venta = new Venta();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Traer la venta principal
                datos.SetearConsulta(@"
                SELECT V.Id, V.NumeroFactura, V.Fecha, V.Total,
                       C.IdCliente AS IdCliente, C.Nombre AS NombreCliente, C.Apellido AS ApellidoCliente,
                       U.Id AS IdUsuario, U.Nombre AS NombreUsuario, U.Apellido AS ApellidoUsuario
                FROM Venta V
                INNER JOIN Cliente C ON V.IdCliente = C.IdCliente
                INNER JOIN Usuario U ON V.IdVendedor = U.Id
                WHERE V.Id = @IdVenta");

                datos.SetearParametro("@IdVenta", id);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    venta.Id = (int)datos.Lector["Id"];
                    venta.NumeroFactura = datos.Lector["NumeroFactura"].ToString();
                    venta.Fecha = (DateTime)datos.Lector["Fecha"];
                    venta.Total = (decimal)datos.Lector["Total"];

                    venta.Cliente = new Cliente
                    {
                        Id = (int)datos.Lector["IdCliente"],
                        Nombre = datos.Lector["NombreCliente"].ToString(),
                        Apellido = datos.Lector["ApellidoCliente"].ToString()
                    };

                    venta.Usuario = new Usuario
                    {
                        Id = (int)datos.Lector["IdUsuario"],
                        Nombre = datos.Lector["NombreUsuario"].ToString(),
                        Apellido = datos.Lector["ApellidoUsuario"].ToString()
                    };
                }
                datos.CerrarConexion();

                // Traer los detalles de la venta
                venta.DetalleList = new List<DetalleVenta>();
                AccesoDatos datosDetalle = new AccesoDatos();

                datosDetalle.SetearConsulta(@"
                SELECT DV.Cantidad, DV.PrecioUnitario, 
                       P.Id AS IdProducto, P.Nombre, P.Stock, P.Precio, P.UrlImgProducto
                FROM DETALLEVENTA DV
                INNER JOIN PRODUCTO P ON P.Id = DV.IdProducto
                WHERE DV.IdVenta = @IdVenta AND DV.Activo = 1");

                datosDetalle.SetearParametro("@IdVenta", id);
                datosDetalle.EjecutarLectura();

                while (datosDetalle.Lector.Read())
                {
                    DetalleVenta detalle = new DetalleVenta
                    {
                        Cantidad = (int)datosDetalle.Lector["Cantidad"],
                        PrecioUnitario = (decimal)datosDetalle.Lector["PrecioUnitario"],
                        Activo = true,
                        Producto = new Producto
                        {
                            Id = (int)datosDetalle.Lector["IdProducto"],
                            Nombre = datosDetalle.Lector["Nombre"].ToString(),
                            Stock = (int)datosDetalle.Lector["Stock"],
                            Precio = (decimal)datosDetalle.Lector["Precio"],
                            UrlImgProducto = datosDetalle.Lector["UrlImgProducto"].ToString()
                        }
                    };

                    venta.DetalleList.Add(detalle);
                }

                return venta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la venta: " + ex.Message, ex);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void ModificarVentaCompleta(Venta venta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.AbrirConexion();
                datos.ComenzarTransaccion();

                // Actualizar venta principal
                datos.SetearConsulta(@"UPDATE VENTA 
                                   SET IdCliente = @IdCliente,
                                       Total = @Total
                                   WHERE Id = @IdVenta");

                datos.SetearParametro("@IdCliente", venta.Cliente.Id);
                datos.SetearParametro("@Total", venta.Total);
                datos.SetearParametro("@IdVenta", venta.Id);

                datos.EjecutarAccion();

                // 1. Obtener los productos que tenés que revertir
                datos.SetearConsulta("SELECT IdProducto, Cantidad FROM DETALLEVENTA WHERE IdVenta = @IdVenta AND Activo = 1");
                datos.SetearParametro("@IdVenta", venta.Id);
                datos.EjecutarLectura();

                var stockARevertir = new List<(int IdProducto, int Cantidad)>();

                while (datos.Lector.Read())
                {
                    stockARevertir.Add((
                        (int)datos.Lector["IdProducto"],
                        (int)datos.Lector["Cantidad"]
                    ));
                }

                // 2. Cerrar el lector antes de seguir
                datos.CerrarLector();

                // 3. Ahora sí: revertir el stock
                foreach (var item in stockARevertir)
                {
                    datos.SetearConsulta("UPDATE PRODUCTO SET Stock = Stock + @Cantidad WHERE Id = @IdProducto");
                    datos.SetearParametro("@Cantidad", item.Cantidad);
                    datos.SetearParametro("@IdProducto", item.IdProducto);
                    datos.EjecutarAccion();
                }


                // Eliminar detalle anterior
                datos.SetearConsulta("DELETE DETALLEVENTA WHERE IdVenta = @IdVenta");
                datos.SetearParametro("@IdVenta", venta.Id);
                datos.EjecutarAccion();

                // Insertar nuevos detalles y actualizar stock
                foreach (var d in venta.DetalleList)
                {
                    datos.SetearConsulta(@"INSERT INTO DETALLEVENTA (IdVenta, IdProducto, Cantidad, PrecioUnitario, Activo)
                                   VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario, 1)");
                    datos.SetearParametro("@IdVenta", venta.Id);
                    datos.SetearParametro("@IdProducto", d.Producto.Id);
                    datos.SetearParametro("@Cantidad", d.Cantidad);
                    datos.SetearParametro("@PrecioUnitario", d.PrecioUnitario);
                    datos.EjecutarAccion();

                    datos.SetearConsulta("UPDATE PRODUCTO SET Stock = Stock - @Cantidad WHERE Id = @IdProducto");
                    datos.SetearParametro("@Cantidad", d.Cantidad);
                    datos.SetearParametro("@IdProducto", d.Producto.Id);
                    datos.EjecutarAccion();
                }

                datos.ConfirmarTransaccion();
            }
            catch (Exception ex)
            {
                datos.RollbackTransaccion();
                throw new Exception("Error al modificar la venta: " + ex.Message, ex);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }



    }
}