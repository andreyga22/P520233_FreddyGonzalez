using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public Producto() {
            MiCategoria = new ProductoCategoria();
        }
        //funciones
        public bool Agregar() {
            bool ret = false;
            Conexion MiCnn = new Conexion();
            MiCnn.ListaDeParametros.Add(new SqlParameter("@CodigoBarras", this.CodigoBarras));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@NombreProducto", this.NombreProducto));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Costo", this.Costo));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Utilidad", this.Utilidad));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@SubTotal", this.SubTotal));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@TasaImpuesto", this.TasaImpuesto));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@PrecioUnitario", this.PrecioUnitario));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@CantidadStock", this.CantidadStock));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Activo", this.Activo));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@ProductoCategoriaID", this.MiCategoria.ProductoCategoriaID));

            int resultado = MiCnn.EjecutarDML("SPProductosAgregar");
            if (resultado > 0) ret = true;

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
            Conexion MiCnn = new Conexion();
            MiCnn.ListaDeParametros.Add(new SqlParameter("@CodigoBarras", CodigoBarras));
            DataTable dt = MiCnn.EjecutarSelect("SPProductosConsultarPorCodigoBarras");
            bool ret = false;
            if (dt != null && dt.Rows.Count > 0) ret = true;
            return ret;
        }

        public DataTable Listar(bool VerActivos = true) {
            DataTable ret = new DataTable();
            return ret;
        }

        public DataTable ListarEnMovimientoDetalleProducto(bool VerActivos = true, string Filtro = "") {
            Conexion MiCnn = new Conexion();
            MiCnn.ListaDeParametros.Add(new SqlParameter("@VerActivos", VerActivos));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Filtro", Filtro));
            DataTable ret = MiCnn.EjecutarSelect("SPProductosListar");
            return ret;
        }

        public DataTable ListarActivos(string pFiltro = "") {
            Conexion MiCnn = new Conexion();
            MiCnn.ListaDeParametros.Add(new SqlParameter("@VerActivos", true));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Filtro", pFiltro));
            DataTable ret = MiCnn.EjecutarSelect("SPProductosListarFull");
            return ret;
        }

        public DataTable ListarInactivos(string pFiltro = "") {
            Conexion MiCnn = new Conexion();
            MiCnn.ListaDeParametros.Add(new SqlParameter("@VerActivos", false));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Filtro", pFiltro));
            DataTable ret = MiCnn.EjecutarSelect("SPProductosListarFull");
            return ret;
        }
    }
}
