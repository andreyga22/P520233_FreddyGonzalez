using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models {
    public class ProductoCategoria {
        public int ProductoCategoriaID { get; set; }
        public string ProductoCategoriaDescripcion { get; set; }
        public DataTable Listar() {
            Conexion MiCnn = new Conexion();
            DataTable ret = MiCnn.EjecutarSelect("SPProductosCategoriaListar");
            return ret;
        }
    }
}
