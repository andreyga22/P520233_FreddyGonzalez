using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Logica.Models {
    public class Usuario {

        public int UsuarioID { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }
        public UsuarioRol MiUsuarioRol{ get; set; }
        public Usuario()
        {
            MiUsuarioRol = new UsuarioRol();
        }

        //funciones
        public bool Agregar() {
            bool ret = false;
            Conexion MiCnn = new Conexion();
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Cedula", this.Cedula));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Nombre", this.Nombre));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Correo", this.Correo));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Contrasennia", this.Contrasena));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Telefono", this.Telefono));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Direccion", this.Direccion));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@UsuarioRolID", this.MiUsuarioRol.UsuarioRolID));

            int resultado = MiCnn.EjecutarDML("SPUsuariosAgregar");
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

        public bool ConsultarPorCedula(string pCedula) {
            Conexion MiCnn = new Conexion();
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Cedula", pCedula));
            DataTable dt = MiCnn.EjecutarSelect("SPUsuariosConsultarPorCedula");
            bool ret = false;
            if (dt != null && dt.Rows.Count > 0) ret = true;
            return ret;
        }

        public bool ConsultarPorEmail(string pCorreo) {
            Conexion MiCnn = new Conexion();
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Correo", pCorreo));
            DataTable dt = MiCnn.EjecutarSelect("SPUsuariosConsultarPorCorreo");
            bool ret = false;
            if (dt != null && dt.Rows.Count > 0) ret = true;
            return ret;
        }

        public DataTable ListarActivos() {
            Conexion MiCnn = new Conexion();
            MiCnn.ListaDeParametros.Add(new SqlParameter("@VerActivos", true));
            DataTable ret = MiCnn.EjecutarSelect("SPUsuariosListar");
            return ret;
        }

        public DataTable ListarInactivos() {
            DataTable ret = new DataTable();
            return ret;
        }

    }
}
