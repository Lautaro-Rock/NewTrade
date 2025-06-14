using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Marca
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public bool Activo { get; set; }
    }
}