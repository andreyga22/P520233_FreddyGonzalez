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
    public partial class FrmPrincipal : Form {
        public FrmPrincipal() {
            InitializeComponent();
        }

        private void movimimentosPorRangoDeFechasToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void gestionDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e) {
            //En este caso quiero que la ventana se muestre solo una vez 
            //en la aplicacion (que no se abra varias veces). Para esto
            //hay que revisar si la ventana esta o no visible
            if (!Globales.ObjetosGlobales.MiFormularioDeGestionDeUsuarios.Visible) {
                //reinstancia para limpiar
                Globales.ObjetosGlobales.MiFormularioDeGestionDeUsuarios = new FrmUsuariosGestion();
                Globales.ObjetosGlobales.MiFormularioDeGestionDeUsuarios.Show();
            }
            
        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}
