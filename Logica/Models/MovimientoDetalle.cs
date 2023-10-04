using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models {
    public class MovimientoDetalle {
        public decimal CantidadMovimiento { get; set; }
        public decimal Costo { get; set; }
        public Producto miProducto { get; set; }
        public MovimientoDetalle() {
            miProducto = new Producto();
        }
        
    }
}
