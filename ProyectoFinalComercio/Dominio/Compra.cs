using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Compra
    {
        int Id { get; set; }
        Proveedor Proveedor { get; set; }
        
        List<DetalleCompra> DetalleList { get; set; }

        public bool Activo { get; set; }
        DateTime Fecha { get; set; }
    }
}