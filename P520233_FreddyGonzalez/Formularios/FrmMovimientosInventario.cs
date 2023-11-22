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
    public partial class FrmMovimientosInventario : Form {
        public Logica.Models.Movimiento MiMovimientoLocal {  get; set; }
        public DataTable DtListaDetalleProductos {  get; set; }
        
        public FrmMovimientosInventario() {
            InitializeComponent();
            MiMovimientoLocal = new Logica.Models.Movimiento();
            DtListaDetalleProductos = new DataTable();
        }

        private void BtnCancelar_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void FrmMovimientosInventario_Load(object sender, EventArgs e) {
            MdiParent = Globales.ObjetosGlobales.MiFormularioPrincipal;
            cargarMovimientosTipo();
            limpiarFormulario();
        }

        private void limpiarFormulario() {
            DtpFecha.Value = DateTime.Now.Date;
            CboxTipo.SelectedIndex = -1;
            TxtAnotaciones.Clear();
            DtListaDetalleProductos = MiMovimientoLocal.AsignarEsquemaDelDetalle();
            DgvListaDetalle.DataSource = DtListaDetalleProductos;
            LblTotalCosto.Text = "0";
            LblTotalGranTotal.Text = "0";
            LblTotalImpuestos.Text = "0";
            LblTotalSubtotal.Text = "0";
        }

        private void cargarMovimientosTipo() {
            MovimientoTipo miMovimientoTipo = new MovimientoTipo();
            DataTable lista = new DataTable();
            lista = miMovimientoTipo.Listar();

            if (lista != null && lista.Rows.Count > 0) {
                CboxTipo.ValueMember = "MovimientoTipoID"; //lo que me interesa para programador
                CboxTipo.DisplayMember = "MovimientoTipoDescripcion"; //lo que el usuario ve 
                CboxTipo.DataSource = lista;
                CboxTipo.SelectedIndex = -1; //no mostrar nada seleccionado al inicio
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e) {
            Form FormDetalleProducto = new Formularios.FrmMovimientosInventarioDetalleProducto();
            DialogResult resp = FormDetalleProducto.ShowDialog();
            if (resp == DialogResult.OK) {

            }
        }
    }
}
