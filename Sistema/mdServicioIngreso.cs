using Sistema.Logica;
using Sistema.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema
{
    public partial class mdServicioIngreso : Form
    {
        public Servicio oServicio { get; set; }
        public bool servicioNuevo { get; set; }
        public string _tiposervicio { get; set; }
        public mdServicioIngreso(bool nuevo = true, Servicio oservicio = null,string tiposervicio = "LICORES")
        {
            servicioNuevo = nuevo;
            oServicio = oservicio;
            _tiposervicio = tiposervicio;
            InitializeComponent();
        }

        private void mdServicioIngreso_Load(object sender, EventArgs e)
        {
            if (oServicio != null)
            {
                txtdescripcion.Text = oServicio.Descripcion;
                txtcantidad.Text = oServicio.Cantidad;
            }
        }

        private void btnaceptar_Click(object sender, EventArgs e)
        {

            string mensaje = string.Empty;


            if (string.IsNullOrEmpty(txtdescripcion.Text.Trim()))
            {
                MessageBox.Show("Debe ingresar todos los datos obligatorios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            Servicio obj = new Servicio()
            {
                Id_L = oServicio == null ? 0 : oServicio.Id_L,
                Descripcion = txtdescripcion.Text,
                TipoServicio = _tiposervicio,
                Cantidad = txtcantidad.Text,
                
            };

            int respuesta = 0;
            if (servicioNuevo)
            {
                respuesta = ServicioLogica.Instancia.Guardar(obj, out mensaje);
            }
            else
            {
                if (ServicioLogica.Instancia.Editar(obj, out mensaje) > 0)
                    respuesta = oServicio.Id_L;
            }


            if (respuesta > 0)
            {
                obj.Id_L = respuesta;
                oServicio = obj;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtdescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
