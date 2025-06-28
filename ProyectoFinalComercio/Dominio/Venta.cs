using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Venta
    {
        public int Id { get; set; }
        
        public Cliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public Usuario Usuario { get; set; }
        public List<DetalleVenta> DetalleList { get; set; }
        public string NumeroFactura { get; set; }

        public decimal Total { get; set; }

        public bool Activo { get; set; }

    }
}