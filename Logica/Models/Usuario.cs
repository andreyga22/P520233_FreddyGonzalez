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

        public bool ConsultarPorCedula(string Cedula) {
            bool ret = false;
            return ret;
        }

        public bool ConsultarPorEmail(string Correo) {
            bool ret = false;
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
