using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        // [JLS] Posible mejora: Con este atributo podemos calcular el subtotal directamente, sin tener que hacer logica en el código de la web
        public decimal Subtotal => Cantidad * PrecioUnitario;


        public bool Activo { get; set; }
    }
}
