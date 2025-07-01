using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CompraNegocio
    {
        public void AgregarCompraCompleta(Compra compra)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.AbrirConexion();
                datos.ComenzarTransaccion();

                // Insertar compra principal
                datos.SetearConsulta(@"
                    INSERT INTO COMPRA (IdProveedor, Fecha, IdUsuario, Total, Activo)
                    VALUES (@IdProveedor, @Fecha, @IdUsuario, @Total, 1);
                    SELECT SCOPE_IDENTITY();");

                datos.SetearParametro("@IdProveedor", compra.Proveedor.Id);
                datos.SetearParametro("@Fecha", compra.Fecha);
                datos.SetearParametro("@IdUsuario", compra.Usuario.Id);
                datos.SetearParametro("@Total", compra.Total);

                int idCompra = Convert.ToInt32(datos.EjecutarScalar());

                // Insertar cada detalle
                foreach (var d in compra.DetalleList)
                {
                    datos.SetearConsulta(@"
                        INSERT INTO DETALLECOMPRA (IdCompra, IdProducto, Cantidad, PrecioUnitario, Activo)
                        VALUES (@IdCompra, @IdProducto, @Cantidad, @PrecioUnitario, 1)");

                    datos.SetearParametro("@IdCompra", idCompra);
                    datos.SetearParametro("@IdProducto", d.Producto.Id);
                    datos.SetearParametro("@Cantidad", d.Cantidad);
                    datos.SetearParametro("@PrecioUnitario", d.PrecioUnitario);

                    datos.EjecutarAccion();

                    // Actualizar stock (se suma)
                    datos.SetearConsulta("UPDATE PRODUCTO SET Stock = Stock + @Cantidad WHERE Id = @IdProducto");
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

        public List<Compra> ListarCompras()
        {
            List<Compra> lista = new List<Compra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(@"
                    SELECT C.Id, C.Fecha, C.Total,
                           P.Id AS IdProveedor, P.RazonSocial AS NombreProveedor,
                           U.Id AS IdUsuario, U.Nombre AS NombreUsuario, U.Apellido AS ApellidoUsuario
                    FROM COMPRA C
                    INNER JOIN PROVEEDOR P ON C.IdProveedor = P.Id
                    INNER JOIN USUARIO U ON C.IdUsuario = U.Id
                    WHERE C.Activo = 1
                    ORDER BY C.Fecha DESC");

                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Compra compra = new Compra
                    {
                        Id = (int)datos.Lector["Id"],
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        Total = (decimal)datos.Lector["Total"],
                        Proveedor = new Proveedor
                        {
                            Id = (int)datos.Lector["IdProveedor"],
                            RazonSocial = datos.Lector["NombreProveedor"].ToString()
                        },
                        Usuario = new Usuario
                        {
                            Id = (int)datos.Lector["IdUsuario"],
                            Nombre = datos.Lector["NombreUsuario"].ToString(),
                            Apellido = datos.Lector["ApellidoUsuario"].ToString()
                        }
                    };

                    lista.Add(compra);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar compras: " + ex.Message, ex);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Compra ObtenerCompraPorId(int id)
        {
            Compra compra = new Compra();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Traer la compra principal
                datos.SetearConsulta(@"
                SELECT C.Id, C.Fecha, C.Total,
                       P.Id AS IdProveedor, P.RazonSocial AS NombreProveedor,
                       U.Id AS IdUsuario, U.Nombre AS NombreUsuario, U.Apellido AS ApellidoUsuario
                FROM COMPRA C
                INNER JOIN PROVEEDOR P ON C.IdProveedor = P.Id
                INNER JOIN USUARIO U ON C.IdUsuario = U.Id
                WHERE C.Id = @IdCompra");

                datos.SetearParametro("@IdCompra", id);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    compra.Id = (int)datos.Lector["Id"];
                    compra.Fecha = (DateTime)datos.Lector["Fecha"];
                    compra.Total = (decimal)datos.Lector["Total"];

                    compra.Proveedor = new Proveedor
                    {
                        Id = (int)datos.Lector["IdProveedor"],
                        RazonSocial = datos.Lector["NombreProveedor"].ToString()
                    };

                    compra.Usuario = new Usuario
                    {
                        Id = (int)datos.Lector["IdUsuario"],
                        Nombre = datos.Lector["NombreUsuario"].ToString(),
                        Apellido = datos.Lector["ApellidoUsuario"].ToString()
                    };
                }

                datos.CerrarConexion();

                // Traer detalles
                AccesoDatos datosDetalle = new AccesoDatos();
                compra.DetalleList = new List<DetalleCompra>();

                datosDetalle.SetearConsulta(@"
                SELECT DC.Cantidad, DC.PrecioUnitario,
                       P.Id AS IdProducto, P.Nombre, P.Stock, P.Precio, P.UrlImgProducto
                FROM DETALLECOMPRA DC
                INNER JOIN PRODUCTO P ON DC.IdProducto = P.Id
                WHERE DC.IdCompra = @IdCompra AND DC.Activo = 1");

                datosDetalle.SetearParametro("@IdCompra", id);
                datosDetalle.EjecutarLectura();

                while (datosDetalle.Lector.Read())
                {
                    DetalleCompra d = new DetalleCompra
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

                    compra.DetalleList.Add(d);
                }

                return compra;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la compra: " + ex.Message, ex);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void ModificarCompraCompleta(Compra compra)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.AbrirConexion();
                datos.ComenzarTransaccion();

                // Actualizar cabecera
                datos.SetearConsulta(@"UPDATE COMPRA 
                               SET IdProveedor = @IdProveedor,
                                   Total = @Total
                               WHERE Id = @IdCompra");

                datos.SetearParametro("@IdProveedor", compra.Proveedor.Id);
                datos.SetearParametro("@Total", compra.Total);
                datos.SetearParametro("@IdCompra", compra.Id);
                datos.EjecutarAccion();

                // Obtener detalle anterior para revertir stock
                datos.SetearConsulta("SELECT IdProducto, Cantidad FROM DETALLECOMPRA WHERE IdCompra = @IdCompra AND Activo = 1");
                datos.SetearParametro("@IdCompra", compra.Id);
                datos.EjecutarLectura();

                List<(int IdProducto, int Cantidad)> stockARevertir = new List<(int, int)>();
                while (datos.Lector.Read())
                {
                    stockARevertir.Add((
                        (int)datos.Lector["IdProducto"],
                        (int)datos.Lector["Cantidad"]
                    ));
                }

                datos.CerrarLector();

                foreach (var item in stockARevertir)
                {
                    datos.SetearConsulta("UPDATE PRODUCTO SET Stock = Stock - @Cantidad WHERE Id = @IdProducto");
                    datos.SetearParametro("@Cantidad", item.Cantidad);
                    datos.SetearParametro("@IdProducto", item.IdProducto);
                    datos.EjecutarAccion();
                }

                // Eliminar detalle viejo
                datos.SetearConsulta("DELETE FROM DETALLECOMPRA WHERE IdCompra = @IdCompra");
                datos.SetearParametro("@IdCompra", compra.Id);
                datos.EjecutarAccion();

                // Insertar detalle nuevo y actualizar stock
                foreach (var d in compra.DetalleList)
                {
                    datos.SetearConsulta(@"
                INSERT INTO DETALLECOMPRA (IdCompra, IdProducto, Cantidad, PrecioUnitario, Activo)
                VALUES (@IdCompra, @IdProducto, @Cantidad, @PrecioUnitario, 1)");

                    datos.SetearParametro("@IdCompra", compra.Id);
                    datos.SetearParametro("@IdProducto", d.Producto.Id);
                    datos.SetearParametro("@Cantidad", d.Cantidad);
                    datos.SetearParametro("@PrecioUnitario", d.PrecioUnitario);
                    datos.EjecutarAccion();

                    datos.SetearConsulta("UPDATE PRODUCTO SET Stock = Stock + @Cantidad WHERE Id = @IdProducto");
                    datos.SetearParametro("@Cantidad", d.Cantidad);
                    datos.SetearParametro("@IdProducto", d.Producto.Id);
                    datos.EjecutarAccion();
                }

                datos.ConfirmarTransaccion();
            }
            catch (Exception ex)
            {
                datos.RollbackTransaccion();
                throw new Exception("Error al modificar la compra: " + ex.Message, ex);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

    }
}
