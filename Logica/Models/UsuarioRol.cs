﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models {
    public class UsuarioRol {
        //Primero digitamos las propiedades de la clase


        public int UsuarioRolID { get; set; }
        //esta forma de escribir una propiedad es autoimplementada, es mas sencilla
        //pero se pierde control en las funciones de get y set.

        public string Rol { get; set; }

        //luego de escribir las propiedades se digitan las funciones

        public DataTable Listar() {
            Conexion MiCnn = new Conexion();
            DataTable ret = MiCnn.EjecutarSelect("SPUsuariosRolListar");
            return ret;
        }
    }
}
