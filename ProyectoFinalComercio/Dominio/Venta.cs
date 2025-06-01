using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Venta
    {
        int id { get; set; }
        Cliente Cliente { get; set; }
        DateTime Fecha { get; set; }
        Usuario Usuario { get; set; }
        List<Producto> Productos { get; set; }
        string numeroFactura { get; set; }


    }
}