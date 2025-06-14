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
        public Marca Marca { get; set; }
        public TipoProducto TipoProducto { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        
        public int StockMin { get; set; }
        
        public string UrlImgProducto { get; set; }
        public List<Proveedor> Proveedores { get; set; }
    }
}