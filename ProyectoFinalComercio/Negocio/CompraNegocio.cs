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
                           P.Id AS IdProveedor, P.Nombre AS NombreProveedor,
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

    }
}
