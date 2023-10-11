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
    public partial class FrmLogin : Form {
        public FrmLogin() {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void BtnVerContrasena_MouseDown(object sender, MouseEventArgs e) {
            TxtContrasena.UseSystemPasswordChar = false;
        }

        private void BtnVerContrasena_MouseUp(object sender, MouseEventArgs e) {
            TxtContrasena.UseSystemPasswordChar = true;
        }

        private void BtnCerrar_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void BtnIngresar_Click(object sender, EventArgs e) {
            Globales.ObjetosGlobales.MiFormularioPrincipal.Show();
            this.Hide();
        }
    }
}
