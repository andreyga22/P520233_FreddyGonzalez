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
    public partial class FrmProductosGestion : Form {

        private Producto MiProductoLocal { get; set; }
        public FrmProductosGestion() {
            InitializeComponent();
            MiProductoLocal = new Producto();
        }

        private void FrmProductosGestion_Load(object sender, EventArgs e) {
            MdiParent = Globales.ObjetosGlobales.MiFormularioPrincipal;
            CargarComboCategorias();
            CargarListaProductos(CbVerActivos.Checked);
            ActivarBotonAgregar();
        }

        private void BtnCerrar_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void BtnLimpiar_Click(object sender, EventArgs e) {
            LimpiarForm();
            ActivarBotonAgregar();
        }

        private void LimpiarForm() {
            TxtCodigoBarras.Clear();
            TxtProductoCodigo.Clear();
            TxtNombreProducto.Clear();
            TxtCosto.Clear();
            TxtUtilidad.Clear();
            TxtSubTotal.Clear();
            TxtTasaImpuesto.Clear();
            TxtPrecioUnitario.Clear();
            TxtCantidadStock.Clear();
            CboxCategoria.SelectedIndex = -1;
            CbProductoActivo.Checked = false;
        }

        private void ActivarBotonAgregar() {
            BtnAgregar.Enabled = true;
            BtnModificar.Enabled = false;
            BtnEliminar.Enabled = false;
        }


        private void ActivarBotonesModificarYEliminar() {
            BtnAgregar.Enabled = false;
            BtnModificar.Enabled = true;
            BtnEliminar.Enabled = true;
        }

        private void label5_Click(object sender, EventArgs e) {

        }

        private void label7_Click(object sender, EventArgs e) {

        }

        private void groupBox1_Enter(object sender, EventArgs e) {

        }

        private void CargarComboCategorias() {
            ProductoCategoria miCategoria = new ProductoCategoria();
            DataTable dt = new DataTable();
            dt = miCategoria.Listar();
            if (dt != null && dt.Rows.Count > 0) {
                CboxCategoria.ValueMember = "id"; //lo que me interesa para programador
                CboxCategoria.DisplayMember = "descripcion"; //lo que el usuario ve 
                CboxCategoria.DataSource = dt;
                CboxCategoria.SelectedIndex = -1; //no mostrar nada seleccionado al inicio
            }
        }

        private void CargarListaProductos(bool VerActivos, string FiltroBusqueda = "") {
            Producto miProducto= new Producto();
            DataTable lista = new DataTable();
            if (VerActivos) {
                lista = miProducto.ListarActivos(FiltroBusqueda);
                DgvListaProductos.DataSource = lista;
            } else {
                lista = miProducto.ListarInactivos(FiltroBusqueda);
                DgvListaProductos.DataSource = lista;
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e) {
            if (ValidarDatosRequeridos()) {
                MiProductoLocal = new Producto();
                MiProductoLocal.CodigoBarras = TxtCodigoBarras.Text.Trim();
                MiProductoLocal.NombreProducto = TxtNombreProducto.Text.Trim();
                MiProductoLocal.Costo = Convert.ToInt32(TxtCosto.Text.Trim());
                MiProductoLocal.Utilidad = Convert.ToInt32(TxtUtilidad.Text.Trim());
                MiProductoLocal.SubTotal = Convert.ToInt32(TxtSubTotal.Text.Trim());
                MiProductoLocal.TasaImpuesto = Convert.ToInt32(TxtTasaImpuesto.Text.Trim());
                MiProductoLocal.PrecioUnitario = Convert.ToInt32(TxtPrecioUnitario.Text.Trim());
                MiProductoLocal.CantidadStock = Convert.ToInt32(TxtCantidadStock.Text.Trim());
                MiProductoLocal.Activo = true;//CbProductoActivo.Checked;
                MiProductoLocal.MiCategoria.ProductoCategoriaID = Convert.ToInt32(CboxCategoria.SelectedValue);
                
                bool CodigoBarrasOk = MiProductoLocal.ConsultarPorCodigoBarras(MiProductoLocal.CodigoBarras);
                if (CodigoBarrasOk == false) {
                    string Pregunta = string.Format("Esta seguro de agregar el producto {0}?", MiProductoLocal.NombreProducto);
                    DialogResult respuesta = MessageBox.Show(Pregunta, "???", MessageBoxButtons.YesNo);

                    if (respuesta == DialogResult.Yes) {
                        bool ok = MiProductoLocal.Agregar();
                        if (ok) {
                            MessageBox.Show("Producto ingresado correctamente.", ":)", MessageBoxButtons.OK);
                            LimpiarForm();
                            CargarListaProductos(CbVerActivos.Checked);
                        } else {
                            MessageBox.Show("El producto no se pudo agregar.", ":(", MessageBoxButtons.OK);
                        }
                    }
                } else {
                    MessageBox.Show("El producto no se pudo agregar. Ya existe el codigo de barras", ":(", MessageBoxButtons.OK);
                }
            }
        }

        private bool ValidarDatosRequeridos(bool OmitirContrasennia = false) {
            bool ret = false;
            if (!string.IsNullOrEmpty(TxtCodigoBarras.Text.Trim()) && !string.IsNullOrEmpty(TxtNombreProducto.Text.Trim()) && !string.IsNullOrEmpty(TxtCosto.Text.Trim()) && !string.IsNullOrEmpty(TxtUtilidad.Text.Trim()) && !string.IsNullOrEmpty(TxtSubTotal.Text.Trim()) && !string.IsNullOrEmpty(TxtTasaImpuesto.Text.Trim()) && !string.IsNullOrEmpty(TxtPrecioUnitario.Text.Trim()) && !string.IsNullOrEmpty(TxtCantidadStock.Text.Trim()) && CboxCategoria.SelectedIndex > -1) {

                ret = true;
            } else {
                if (string.IsNullOrEmpty(TxtCodigoBarras.Text.Trim())) {
                    MessageBox.Show("Debe digitar el codigo de barras.", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
                if (string.IsNullOrEmpty(TxtNombreProducto.Text.Trim())) {
                    MessageBox.Show("Debe digitar el nombre.", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
                if (string.IsNullOrEmpty(TxtCosto.Text.Trim())) {
                    MessageBox.Show("Debe digitar el costo.", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
                if (string.IsNullOrEmpty(TxtUtilidad.Text.Trim())) {
                    MessageBox.Show("Debe digitar la utilidad.", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
                if (string.IsNullOrEmpty(TxtSubTotal.Text.Trim())) {
                    MessageBox.Show("Debe digitar el subtotal.", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
                if (string.IsNullOrEmpty(TxtTasaImpuesto.Text.Trim())) {
                    MessageBox.Show("Debe digitar la tasa impuesto.", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
                if (string.IsNullOrEmpty(TxtPrecioUnitario.Text.Trim())) {
                    MessageBox.Show("Debe digitar el precio unitario.", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
                if (string.IsNullOrEmpty(TxtCantidadStock.Text.Trim())) {
                    MessageBox.Show("Debe digitar la cantidad stock.", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
                if (CboxCategoria.SelectedIndex == -1) {
                    MessageBox.Show("Debe seleccionar una categoria", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
            }
            return ret;
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(TxtBuscar.Text.Trim()) && TxtBuscar.Text.Count() >= 3) {
                CargarListaProductos(CbVerActivos.Checked, TxtBuscar.Text.Trim());
            } else {
                CargarListaProductos(CbVerActivos.Checked);
            }
        }

        private void CbVerActivos_CheckedChanged(object sender, EventArgs e) {

            CargarListaProductos(CbVerActivos.Checked);

            if (CbVerActivos.Checked) {
                BtnEliminar.Text = "Eliminar";
            } else {
                BtnEliminar.Text = "Activar";
            }
        }
    }
}
