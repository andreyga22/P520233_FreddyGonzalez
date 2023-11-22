using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models {
    public class MovimientoTipo {
        public int MovimientoTipoID { get; set; }
        public string MovimientoTipoDescripcion { get; set; }

        public DataTable Listar() {
            Conexion MiCnn = new Conexion();
            DataTable ret = MiCnn.EjecutarSelect("SPMovimientoTipoListar");
            return ret;
        }
    }
}
