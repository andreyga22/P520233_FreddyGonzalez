using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            Conexion MiCnn = new Conexion();
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Fecha", this.Fecha));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Anotaciones", this.Anotaciones));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@TipoMovimiento", this.MiTipo.MovimientoTipoID));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@UsuarioID", this.MiUsuario.UsuarioID));
            Object RetornoSPAgregar = MiCnn.EjecutarSELECTEscalar("SPMovimientosAgregarEncabezado");

            int IDMovimientoRecienCreado;
            if (RetornoSPAgregar != null) {
                IDMovimientoRecienCreado = Convert.ToInt32(RetornoSPAgregar.ToString());

                foreach (MovimientoDetalle item in this.Detalles) {
                    Conexion MyCnnDetalle = new Conexion();

                    MyCnnDetalle.ListaDeParametros.Add(new SqlParameter("@IDMovimiento", IDMovimientoRecienCreado));
                    MyCnnDetalle.ListaDeParametros.Add(new SqlParameter("@IDProducto",item.miProducto.ProductoID));
                    MyCnnDetalle.ListaDeParametros.Add(new SqlParameter("@Cantidad", item.CantidadMovimiento));
                    MyCnnDetalle.ListaDeParametros.Add(new SqlParameter("@Costo", item.Costo));
                    MyCnnDetalle.ListaDeParametros.Add(new SqlParameter("@SubTotal", item.SubTotal));
                    MyCnnDetalle.ListaDeParametros.Add(new SqlParameter("@TotalIVA", item.TotalIVA));
                    MyCnnDetalle.ListaDeParametros.Add(new SqlParameter("@PrecioUnitario", item.PrecioUnitario));

                    MyCnnDetalle.EjecutarDML("SPMovimientosAgregarDetalle");
                }
                ret = true;
            }
            
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
        public DataTable AsignarEsquemaDelDetalle() {
            Conexion MiCnn = new Conexion();
            DataTable ret = MiCnn.EjecutarSelect("SPMovimientoCargarDetalle", true);
            ret.PrimaryKey = null;
            return ret;
        }
    }
}
