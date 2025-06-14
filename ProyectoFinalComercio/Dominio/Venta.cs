using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Venta
    {
        int Id { get; set; }
       // Cliente Cliente { get; set; }
        DateTime Fecha { get; set; }
        Usuario Usuario { get; set; }

        List<DetalleVenta> DetalleList { get; set; }
        string numeroFactura { get; set; }

        public bool Activo { get; set; }

    }
}