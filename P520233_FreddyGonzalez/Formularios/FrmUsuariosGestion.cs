using Logica.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P520233_FreddyGonzalez.Formularios {
    public partial class FrmUsuariosGestion : Form {
        public FrmUsuariosGestion() {
            InitializeComponent();
        }

        private void FrmUsuariosGestion_Load(object sender, EventArgs e) {
            MdiParent = Globales.ObjetosGlobales.MiFormularioPrincipal;
            CargarListaUsuarios();
        }

        private void CargarListaUsuarios() {
            Usuario miUsuario = new Usuario();
            DataTable lista = new DataTable();
            lista = miUsuario.ListarActivos();
            DgvListaUsuarios.DataSource = lista;
        }
    }
}
