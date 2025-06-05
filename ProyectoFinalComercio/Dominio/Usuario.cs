using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Usuario
    {
        public int id { get; set; }

        public int dni { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string rol { get; set; } // Puede ser "Administrador", "Vendedor" o "Cliente"
    }
}   