using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models {
    public class Movimiento {
        public int MovimientoID { get; set; }
        public DateTime Fecha { get; set; }
        public int NumeroMovimiento { get; set; }
        public string Anotaciones { get; set; }
        public MovimientoTipo MiTipo { get; set; }
        public Usuario MiUsuario { get; set; }
        public List<MovimientoDetalle> Detalles { get; set; }
        public Movimiento() {
            MiTipo = new MovimientoTipo();
            MiUsuario = new Usuario();
            Detalles = new List<MovimientoDetalle>();
        }
        //funciones
        public bool Agregar() {
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
        public DataTable Listar() {
            DataTable ret = new DataTable();
            return ret;
        }
    }
}
