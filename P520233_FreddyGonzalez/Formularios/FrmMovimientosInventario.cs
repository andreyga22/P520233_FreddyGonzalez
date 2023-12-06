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
                DgvListaDetalle.DataSource = DtListaDetalleProductos;
                Totalizar();
            }
        }

        private void Totalizar() {
            decimal TotalCosto = 0;
            decimal TotalSubtotal = 0;
            decimal TotalImpuestos = 0;
            decimal Total = 0;

            if (DtListaDetalleProductos != null && DtListaDetalleProductos.Rows.Count > 0) {
                foreach (DataRow item in DtListaDetalleProductos.Rows) {
                    decimal Cantidad = Convert.ToDecimal(item["CantidadMovimiento"]);
                    TotalCosto += Convert.ToDecimal(item["Costo"]) * Cantidad;
                    TotalSubtotal += Convert.ToDecimal(item["SubTotal"]) * Cantidad;
                    TotalImpuestos += Convert.ToDecimal(item["TotalIVA"]) * Cantidad;
                    Total += TotalSubtotal + TotalImpuestos;
                }
            }

            LblTotalCosto.Text = string.Format("{0:C2}", TotalCosto);
            LblTotalSubtotal.Text = string.Format("{0:C2}", TotalSubtotal);
            LblTotalImpuestos.Text = string.Format("{0:C2}", TotalImpuestos);
            LblTotalGranTotal.Text = string.Format("{0:C2}", Total);
        }

        private void BtnAplicar_Click(object sender, EventArgs e) {

            if (ValidarMovimiento()) {
                DialogResult respuesta = MessageBox.Show("Desea continuar?", "???", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes) {
                    MiMovimientoLocal.Fecha = DtpFecha.Value.Date;
                    MiMovimientoLocal.Anotaciones = TxtAnotaciones.Text.Trim();
                    MiMovimientoLocal.MiTipo.MovimientoTipoID = Convert.ToInt32(CboxTipo.SelectedValue);
                    MiMovimientoLocal.MiUsuario = Globales.ObjetosGlobales.MiUsuarioGlobal;
                    TrasladarDetalles();
                    if (MiMovimientoLocal.Agregar()) {
                        MessageBox.Show("El movimiento se ha agreagado correctamente.", ":)", MessageBoxButtons.OK);
                        CrystalDecisions.CrystalReports.Engine.ReportDocument MiReporte = new Reportes.RptMovimiento();
                        MiReporte = MiMovimientoLocal.Imprimir(MiReporte);
                        FrmVisualizadorReportes MiVisualizador = new FrmVisualizadorReportes();
                        MiVisualizador.CrvVisualizador.ReportSource = MiReporte;
                        MiVisualizador.Show();

                    }
                }
            }
        }

        private bool ValidarMovimiento() {
            bool ret = false;
            if (DtpFecha.Value.Date <= DateTime.Now.Date && CboxTipo.SelectedIndex > -1 && DtListaDetalleProductos.Rows.Count > 0) {
                ret = true;
            } else {
                if (DtpFecha.Value.Date > DateTime.Now.Date) {
                    MessageBox.Show("La fecha del movimiento no puede ser superior a la fecha actual", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
                if (CboxTipo.SelectedIndex == -1) {
                    MessageBox.Show("Debe seleccionar un tipo de movimiento", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
                if (DtListaDetalleProductos == null || DtListaDetalleProductos.Rows.Count == 0) {
                    MessageBox.Show("No se puede procesar un movimiento sin detalles", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
            }
            return ret;
        }

        private void TrasladarDetalles() {
            foreach (DataRow item in DtListaDetalleProductos.Rows) {
                Logica.Models.MovimientoDetalle NuevoDetalle = new Logica.Models.MovimientoDetalle();
                NuevoDetalle.CantidadMovimiento = Convert.ToDecimal(item["CantidadMovimiento"]);
                NuevoDetalle.Costo = Convert.ToDecimal(item["Costo"]);
                NuevoDetalle.PrecioUnitario = Convert.ToDecimal(item["PrecioUnitario"]);
                NuevoDetalle.SubTotal = Convert.ToDecimal(item["SubTotal"]);
                NuevoDetalle.TotalIVA = Convert.ToDecimal(item["TotalIVA"]);

                NuevoDetalle.miProducto.ProductoID = Convert.ToInt32(item["ProductoID"]);
                MiMovimientoLocal.Detalles.Add(NuevoDetalle);
            }
        }
    }
}
