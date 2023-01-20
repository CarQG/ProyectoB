using Sistema.Logica;
using Sistema.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema
{
    public partial class frmRegistrarSalidas : Form
    {
        private static Persona opersona;
        public frmRegistrarSalidas(Persona obj)
        {
            opersona = obj;
            InitializeComponent();
        }

        private void frmRegistrarEgresos_Load(object sender, EventArgs e)
        {

            //listarMoneda();

            DataGridViewButtonColumn btneliminar = new DataGridViewButtonColumn();
            btneliminar.HeaderText = "Eliminar";
            btneliminar.Width = 58;
            btneliminar.Text = "";
            btneliminar.Name = "btnEliminar";
            btneliminar.UseColumnTextForButtonValue = false;

            dgdata.Columns.Add(btneliminar);
            dgdata.Columns.Add("IdProveedor", "IdProveedor");
            dgdata.Columns.Add("Proveedor", "Proveedor");
            dgdata.Columns.Add("Id_L", "Id_L");
            dgdata.Columns.Add("Bebida", "Bebida");
            dgdata.Columns.Add("Comentario", "Comentario");
            dgdata.Columns.Add("Fecha", "Fecha");
            dgdata.Columns.Add("Cantidad", "Cantidad");


            dgdata.Columns["Fecha"].Width = 80;
            dgdata.Columns["Cantidad"].Width = 65;
            dgdata.Columns["Proveedor"].Width = 110;
            dgdata.Columns["Bebida"].Width = 150;
            dgdata.Columns["Comentario"].Width = 115;

            dgdata.Columns["IdProveedor"].Visible = false;
            dgdata.Columns["Id_L"].Visible = false;
        }


       /* private void listarMoneda()
        {

            string mensaje = string.Empty;
            cbotipomoneda.Items.Clear();
            List<TipoMoneda> olista = TipoMonedaLogica.Instancia.Listar(out mensaje);
            if (olista.Count > 0)
            {
                foreach (TipoMoneda tm in olista)
                {
                    cbotipomoneda.Items.Add(new ComboBoxItem() { Value = tm.IdTipoMoneda, Text = tm.Descripcion });
                }
                cbotipomoneda.DisplayMember = "Text";
                cbotipomoneda.ValueMember = "Value";
                cbotipomoneda.SelectedIndex = 0;
            }
        }*/

        private void txtfecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtdescripcionservicio.Text.Trim()) || string.IsNullOrEmpty(txtmonto.Text.Trim()))
            {
                MessageBox.Show("Debe ingresar los campos obligatorios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            bool error = false;
            try
            {
                decimal monto = Convert.ToDecimal(txtmonto.Text.Trim(), new CultureInfo("es-PE"));
            }
            catch
            {
                error = true;
            }

            if (error)
            {
                MessageBox.Show("Error al convertir el valor del monto ingresado\nEjemplo Formato ##.##", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int rowId = dgdata.Rows.Add();
            DataGridViewRow row = dgdata.Rows[rowId];

            row.Cells["IdProveedor"].Value = txtidpersona.Text;
            row.Cells["Proveedor"].Value = txtnombrecliente.Text;
            row.Cells["Id_L"].Value = txtidservicio.Text;
            row.Cells["Bebida"].Value = txtdescripcionservicio.Text;
            row.Cells["Comentario"].Value = txtcomentario.Text;
            row.Cells["Fecha"].Value = txtfecha.Value.ToString("dd/MM/yyyy");
            row.Cells["Cantidad"].Value = txtmonto.Text.Trim();

            txtidpersona.Text = "0";
            txtnombrecliente.Text = "";
            txtidservicio.Text = "0";
            txtdescripcionservicio.Text = "";
            txtcomentario.Text = "";
            txtfecha.Value = DateTime.Now;
            //cbotipomoneda.SelectedIndex = 0;
            txtmonto.Text = "";


        }

        private void btnterminar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            List<Registro> olista = new List<Registro>();

            if (dgdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgdata.Rows)
                {
                    DateTime dt = Convert.ToDateTime(row.Cells["Fecha"].Value.ToString(), new CultureInfo("es-PE"));
                    decimal Cantidad = Convert.ToDecimal(row.Cells["Cantidad"].Value.ToString(), new CultureInfo("es-PE"));

                    olista.Add(new Registro()
                    {
                        IdProveedor  = int.Parse(row.Cells["IdProveedor"].Value.ToString()),
                        UsuarioCreacion = opersona.Nombres,
                        NombreCliente = row.Cells["Proveedor"].Value.ToString(),
                        Id_L = int.Parse(row.Cells["Id_L"].Value.ToString()),
                        Descripcion_en = row.Cells["Bebida"].Value.ToString(),
                        Comentario = row.Cells["Comentario"].Value.ToString(),
                        Fecha = dt.ToString("yyyy-MM-dd", new CultureInfo("en-US")),
                        Cantidad = Cantidad.ToString("0.00", new CultureInfo("es-PE")),
                        TipoRegistro = "Cervezas"
                    });
                }

                int respuesta = RegistroLogica.Instancia.Guardar(olista, out mensaje);

                if (respuesta > 0)
                {
                    dgdata.Rows.Clear();
                    MessageBox.Show("Los egresos fueron registrados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo registrar\nMayor Detalle:" + mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void txtmonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtmonto.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }

            }
        }

        private void dgdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.trash16.Width;
                var h = Properties.Resources.trash16.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.trash16, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int index = e.RowIndex;
            if (dgdata.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                if (index >= 0)
                {
                    dgdata.Rows.RemoveAt(index);
                }
            }
        }

        private void btnverclientes_Click(object sender, EventArgs e)
        {
            using (var form = new mdListaCliente())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtidpersona.Text = form.IdPersona;
                    txtnombrecliente.Text = form.NombreCliente;
                }
            }
        }

        private void btnverservicios_Click(object sender, EventArgs e)
        {
            using (var form = new mdListaServicios("CERVEZAS"))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtidservicio.Text = form.IdServicio;
                    txtdescripcionservicio.Text = form.Descripcion;
                }
            }
        }

       /* private void button4_Click(object sender, EventArgs e)
        {
            using (var form = new mdTipoMoneda())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    listarMoneda();
                }
            }
        }*/

        private void txtmonto_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
