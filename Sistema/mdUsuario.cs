using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Sistema.Modelo;
using Sistema.Logica;

namespace Sistema
{
    public partial class mdUsuario : Form
    {
        public Persona oPersona { get; set; }
        public bool personaNueva { get; set; }
        public mdUsuario(bool nuevo = true, Persona opersona = null)
        {
            personaNueva = nuevo;
            oPersona = opersona;
            InitializeComponent();
        }

        private void mdUsuario_Load(object sender, EventArgs e)
        {
            cbotiposusuario.Items.Add(new ComboBoxItem() { Value = "ADMINISTRADOR", Text = "Administrador" });
            cbotiposusuario.Items.Add(new ComboBoxItem() { Value = "EMPLEADO", Text = "Empleado" });
            cbotiposusuario.DisplayMember = "Text";
            cbotiposusuario.ValueMember = "Value";
            cbotiposusuario.SelectedIndex = 0;


            if (oPersona != null) {
                txtdocumento.Text = oPersona.Usuario;
                txtnombres.Text = oPersona.Nombres;
                txtcorreo.Text = oPersona.Correo;
                txtclave.Text = oPersona.Contraseña;
                txtconfirmarclave.Text = oPersona.Contraseña;
                foreach (ComboBoxItem valor in cbotiposusuario.Items) {
                    if (valor.Value.ToString().ToUpper() == oPersona.TipoPermiso.ToUpper()) {
                        int item_index = cbotiposusuario.Items.IndexOf(valor);
                        cbotiposusuario.SelectedIndex = item_index;
                        break;
                    }
                }

            }
        }

        private void btnaceptar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;


            if (string.IsNullOrEmpty(txtdocumento.Text.Trim()) || string.IsNullOrEmpty(txtnombres.Text.Trim()) || string.IsNullOrEmpty(txtclave.Text.Trim())) {
                MessageBox.Show("Debe ingresar todos los datos obligatorios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtclave.Text != txtconfirmarclave.Text) {
                MessageBox.Show("Las contraseñas no coinciden", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Persona obj = new Persona()
            {
                IdUsuario = oPersona == null ? 0 : oPersona.IdUsuario,
                Usuario = txtdocumento.Text,
                Nombres = txtnombres.Text,
                Correo = txtcorreo.Text,
                Contraseña = txtclave.Text,
                TipoPermiso = ((ComboBoxItem)cbotiposusuario.SelectedItem).Value.ToString()
            };

            int respuesta = 0;
            if (personaNueva) {
                respuesta = PersonaLogica.Instancia.Guardar(obj, out mensaje);
            }
            else {
                if(PersonaLogica.Instancia.Editar(obj, out mensaje) > 0)
                    respuesta = oPersona.IdUsuario;
            }
            

            if (respuesta > 0)
            {
                obj.IdUsuario = respuesta;
                oPersona = obj;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtdocumento_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbotiposusuario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
