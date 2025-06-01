using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Usuario
    {
       int id { get; set; }
        string nombre { get; set; }
        string apellido { get; set; }
        string email { get; set; }
        string password { get; set; }
        string rol { get; set; } // Puede ser "Administrador", "Vendedor" o "Cliente"
    }
}   