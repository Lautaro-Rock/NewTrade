using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class ProductoProveedor
    {
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
        public int IdProveedor { get; set; }
        public Proveedor Proveedor { get; set; }
    }
}
