using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models {
    public class Producto {
        public int ProductoID { get; set; }
        public string CodigoBarras { get; set; }
        public string NombreProducto { get; set; }
        public decimal Costo { get; set; }
        public decimal Utilidad { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TasaImpuesto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal CantidadStock { get; set; }
        public bool Activo { get; set; }
        public ProductoCategoria MiCategoria { get; set; }
        public Producto()
        {
            MiCategoria = new ProductoCategoria();
        }
        //funciones
        public bool Agregar() {
            bool ret = false;
            return ret;
        }

        public bool Actualizar() {
            bool ret = false;
            return ret;
        }

        public bool Eliminar() {
            bool ret = false;
            return ret;
        }

        public bool ConsultarPorID() {
            bool ret = false;
            return ret;
        }

        public bool ConsultarPorCodigoBarras(string CodigoBarras) {
            bool ret = false;
            return ret;
        }

        public DataTable Listar(bool VerActivos = true) {
            DataTable ret = new DataTable();
            return ret;
        }
    }
}
