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

        private Usuario MiUsuarioLocal { get; set; }

        public FrmUsuariosGestion() {
            InitializeComponent();
            MiUsuarioLocal = new Usuario();
        }

        private void FrmUsuariosGestion_Load(object sender, EventArgs e) {
            MdiParent = Globales.ObjetosGlobales.MiFormularioPrincipal;
            CargarComboRoles();
            CargarListaUsuarios();

            ActivarBotonAgregar();
        }

        private void CargarComboRoles() {
            UsuarioRol MiRol = new UsuarioRol();
            DataTable dt = new DataTable();
            dt = MiRol.Listar();
            if (dt != null && dt.Rows.Count > 0) {
                CboxUsuarioTipoRol.ValueMember = "id"; //lo que me interesa para programador
                CboxUsuarioTipoRol.DisplayMember = "descripcion"; //lo que el usuario ve 
                CboxUsuarioTipoRol.DataSource = dt;
                CboxUsuarioTipoRol.SelectedIndex = -1; //no mostrar nada seleccionado al inicio
            }
        }

        private void CargarListaUsuarios() {
            Usuario miUsuario = new Usuario();
            DataTable lista = new DataTable();
            lista = miUsuario.ListarActivos();
            DgvListaUsuarios.DataSource = lista;
        }

        private bool ValidarDatosRequeridos(bool OmitirContrasennia = false) {
            bool ret = false;
            if (!string.IsNullOrEmpty(TxtUsuarioCedula.Text.Trim()) && !string.IsNullOrEmpty(TxtUsuarioNombre.Text.Trim()) && !string.IsNullOrEmpty(TxtUsuarioCorreo.Text.Trim()) && CboxUsuarioTipoRol.SelectedIndex > -1) {

                if (OmitirContrasennia) {
                    ret = true;
                } else {
                    if (!string.IsNullOrEmpty(TxtUsuarioContrasena.Text.Trim())) {
                        ret = true;
                    } else {
                        if (string.IsNullOrEmpty(TxtUsuarioContrasena.Text.Trim())) {
                            MessageBox.Show("Debe digitar la contrasena.", "Error de validacion", MessageBoxButtons.OK);
                            return false;
                        }
                    }
                }



            } else {
                if (string.IsNullOrEmpty(TxtUsuarioCedula.Text.Trim())) {
                    MessageBox.Show("Debe digitar la cedula.", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
                if (string.IsNullOrEmpty(TxtUsuarioNombre.Text.Trim())) {
                    MessageBox.Show("Debe digitar el nombre.", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
                if (string.IsNullOrEmpty(TxtUsuarioCorreo.Text.Trim())) {
                    MessageBox.Show("Debe digitar el correo.", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
                if (CboxUsuarioTipoRol.SelectedIndex == -1) {
                    MessageBox.Show("Debe seleccionar un rol", "Error de validacion", MessageBoxButtons.OK);
                    return false;
                }
            }
            return ret;
        }

        private void BtnAgregar_Click(object sender, EventArgs e) {

            if (ValidarDatosRequeridos()) {
                MiUsuarioLocal = new Usuario();
                MiUsuarioLocal.Cedula = TxtUsuarioCedula.Text.Trim();
                MiUsuarioLocal.Nombre = TxtUsuarioNombre.Text.Trim();
                MiUsuarioLocal.Correo = TxtUsuarioCorreo.Text.Trim();
                MiUsuarioLocal.Telefono = TxtUsuarioTelefono.Text.Trim();
                MiUsuarioLocal.MiUsuarioRol.UsuarioRolID = Convert.ToInt32(CboxUsuarioTipoRol.SelectedValue);
                MiUsuarioLocal.Contrasena = TxtUsuarioContrasena.Text.Trim();
                MiUsuarioLocal.Direccion = TxtUsuarioDireccion.Text.Trim();
                bool CedulaOk = MiUsuarioLocal.ConsultarPorCedula(MiUsuarioLocal.Cedula);
                bool CorreoOk = MiUsuarioLocal.ConsultarPorEmail(MiUsuarioLocal.Correo);
                if (CedulaOk == false && CorreoOk == false) {
                    string Pregunta = string.Format("Esta seguro de agregar al usuario {0}?", MiUsuarioLocal.Nombre);
                    DialogResult respuesta = MessageBox.Show(Pregunta, "???", MessageBoxButtons.YesNo);

                    if (respuesta == DialogResult.Yes) {
                        bool ok = MiUsuarioLocal.Agregar();
                        if (ok) {
                            MessageBox.Show("Usuario ingresado correctamente.", ":)", MessageBoxButtons.OK);
                            LimpiarForm();
                            CargarListaUsuarios();
                        } else {
                            MessageBox.Show("El usuario no se pudo agregar.", ":(", MessageBoxButtons.OK);
                        }
                    }
                }
            }
        }

        private void LimpiarForm() {
            TxtUsuarioCedula.Clear();
            TxtUsuarioCodigo.Clear();
            TxtUsuarioNombre.Clear();
            TxtUsuarioCorreo.Clear();
            TxtUsuarioTelefono.Clear();
            TxtUsuarioContrasena.Clear();
            TxtUsuarioDireccion.Clear();
            CboxUsuarioTipoRol.SelectedIndex = -1;
            CbUsuarioActivo.Checked = false;
        }

        private void DgvListaUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e) {
        }

        private void DgvListaUsuarios_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (DgvListaUsuarios.SelectedRows.Count == 1) {
                LimpiarForm();
                DataGridViewRow MiDgvFila = DgvListaUsuarios.SelectedRows[0];
                int IDUsuario = Convert.ToInt32(MiDgvFila.Cells["ColUsuarioID"].Value);

                MiUsuarioLocal = new Usuario();
                MiUsuarioLocal = MiUsuarioLocal.ConsultarPorID(IDUsuario);

                if (MiUsuarioLocal != null && MiUsuarioLocal.UsuarioID > 0) {
                    TxtUsuarioCodigo.Text = MiUsuarioLocal.UsuarioID.ToString();
                    TxtUsuarioCedula.Text = MiUsuarioLocal.Cedula;
                    TxtUsuarioNombre.Text = MiUsuarioLocal.Nombre;
                    TxtUsuarioCorreo.Text = MiUsuarioLocal.Correo;
                    TxtUsuarioTelefono.Text = MiUsuarioLocal.Telefono;
                    TxtUsuarioDireccion.Text = MiUsuarioLocal.Direccion;
                    CboxUsuarioTipoRol.SelectedValue = MiUsuarioLocal.MiUsuarioRol.UsuarioRolID;
                    CbUsuarioActivo.Checked = MiUsuarioLocal.Activo;

                    ActivarBotonesModificarYEliminar();
                }
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e) {
            LimpiarForm();
            ActivarBotonAgregar();
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

        private void DgvListaUsuarios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            DgvListaUsuarios.ClearSelection();
        }

        private void BtnModificar_Click(object sender, EventArgs e) {
            if (ValidarDatosRequeridos(true)) {
                MiUsuarioLocal.Nombre = TxtUsuarioNombre.Text.Trim();
                MiUsuarioLocal.Cedula = TxtUsuarioCedula.Text.Trim();
                MiUsuarioLocal.Correo = TxtUsuarioCorreo.Text.Trim();
                MiUsuarioLocal.Telefono = TxtUsuarioTelefono.Text.Trim();
                MiUsuarioLocal.Direccion = TxtUsuarioDireccion.Text.Trim();
                MiUsuarioLocal.MiUsuarioRol.UsuarioRolID = Convert.ToInt32(CboxUsuarioTipoRol.SelectedValue);
                MiUsuarioLocal.Contrasena = TxtUsuarioContrasena.Text.Trim();

                if (MiUsuarioLocal.ConsultarPorID()) {
                    DialogResult resp = MessageBox.Show("Desea modificar el usuario?", "???", MessageBoxButtons.YesNo);
                    if (resp == DialogResult.Yes) {
                        if (MiUsuarioLocal.Actualizar()) {
                            MessageBox.Show("Usuario modificado correctamente!", ":)", MessageBoxButtons.OK);
                            CargarListaUsuarios();
                            ActivarBotonAgregar();
                        }
                    }
                }
            }
        }
    }
}
