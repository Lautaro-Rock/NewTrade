using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio; 

namespace Helpers
{
    public static class VentasHelper
    {
        public static string GenerarNumeroFactura()
        {
            VentaNegocio negocio = new VentaNegocio();
            int ultimoNumero = negocio.ObtenerUltimoNumeroFactura();
            int siguiente = ultimoNumero + 1;

            return $"F0001-{siguiente.ToString("D8")}";
        }

    }
}
