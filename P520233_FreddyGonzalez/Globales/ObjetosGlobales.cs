﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P520233_FreddyGonzalez.Globales {
    public static class ObjetosGlobales {
        //definir un objeto global para el formulario principal
        public static Form MiFormularioPrincipal = new Formularios.FrmPrincipal();
        public static Formularios.FrmUsuariosGestion MiFormularioDeGestionDeUsuarios = new Formularios.FrmUsuariosGestion();
        public static Formularios.FrmProductosGestion MiFormularioDeGestionDeProductos = new Formularios.FrmProductosGestion();

        public static Logica.Models.Usuario MiUsuarioGlobal = new Logica.Models.Usuario();
        public static Formularios.FrmMovimientosInventario MiFormularioMovimientos = new Formularios.FrmMovimientosInventario();
    }
}
