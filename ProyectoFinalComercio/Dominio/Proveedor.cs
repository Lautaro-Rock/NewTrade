using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Proveedor
    {
        int id { get; set; }
        string RazonSocial { get; set; }
        string Cuit { get; set; }
        string email { get; set; }
        string telefono { get; set; }
        string direccion { get; set; }

    }
}