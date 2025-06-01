using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        Marca Marca { get; set; }
        TipoProducto TipoProducto { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        List<Proveedor> Proveedores { get; set; }
    }
}