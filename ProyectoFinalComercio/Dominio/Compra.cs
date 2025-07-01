using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Compra
    {
        public int Id { get; set; }
        public Proveedor Proveedor { get; set; }

        public Usuario Usuario { get; set; }

        public List<DetalleCompra> DetalleList { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }

        public bool Activo { get; set; }
    }
}