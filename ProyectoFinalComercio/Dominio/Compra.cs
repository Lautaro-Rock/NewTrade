using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Compra
    {
        int id { get; set; }
        Proveedor Proveedor { get; set; }
        DateTime Fecha { get; set; }

        // tener lista de detalle de compra
    }
}