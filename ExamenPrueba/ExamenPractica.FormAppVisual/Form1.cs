using ExamenPractica.ApplicationService.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenPractica.FormAppVisual
{
    public partial class Form1 : Form
    {
        private ServiceProducto serviceProducto = new ServiceProducto();
        private string idProducto = "0";
        private bool Editar = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarTodos();
        }



        private void LimpiarForm()
        {
            txtNombre.Clear();
            txtIdProveedor.Clear();
            txtIdTipo.Clear();
            txtCantidad.Clear();
            txtModelo.Clear();
            txtMarca.Clear();
        }

        private void HabilitarDeshabilitar(bool hab)
        {
            txtNombre.Enabled = hab;
            txtMarca.Enabled = hab;
            txtModelo.Enabled = hab;
            txtIdProveedor.Enabled = hab;
            txtIdTipo.Enabled = hab;
        }

        private void MostrarTodos()
        {            
            dataGridView1.DataSource = serviceProducto.MostrarTodos();
            cbCantidad.Items.Clear();
            cbCantidad.Items.Add("Todos");
            cbCantidad.Items.Add("Con existencia");
            cbCantidad.Items.Add("Sin existencia");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //INSERTAR
            if (Editar == false)
            {
                try
                {
                    serviceProducto.Insertar(txtNombre.Text,
                        txtIdProveedor.Text,
                        txtIdTipo.Text,
                        txtCantidad.Text,
                        txtModelo.Text,
                        txtMarca.Text);
                    LimpiarForm();
                    MostrarTodos();
                    MessageBox.Show("Insertado correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error inesperado: {ex.Message}");
                }                
            } else
            { //EDITAR
                try
                {
                    serviceProducto.EditarCantidad(idProducto, txtCantidad.Text);
                    LimpiarForm();
                    MostrarTodos();
                    HabilitarDeshabilitar(true);
                    MessageBox.Show("Actualizado correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error inesperado: {ex.Message}");
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Editar = true;
                txtNombre.Text = dataGridView1.CurrentRow.Cells["NombreProducto"].Value.ToString();
                txtMarca.Text = dataGridView1.CurrentRow.Cells["Marca"].Value.ToString();
                txtModelo.Text = dataGridView1.CurrentRow.Cells["Modelo"].Value.ToString();
                txtIdProveedor.Text = dataGridView1.CurrentRow.Cells["Proveedor"].Value.ToString();
                txtIdTipo.Text = dataGridView1.CurrentRow.Cells["TipoProducto"].Value.ToString();
                txtCantidad.Text = dataGridView1.CurrentRow.Cells["Cantidad"].Value.ToString();

                HabilitarDeshabilitar(false);

                idProducto = dataGridView1.CurrentRow.Cells["IDProducto"].Value.ToString();
            }
            else
            {
                MessageBox.Show("seleccione una fila por favor");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                idProducto = dataGridView1.CurrentRow.Cells["IDProducto"].Value.ToString();
                serviceProducto.EliminarProducto(idProducto);
                MessageBox.Show("Eliminado correctamente");
                MostrarTodos();
            }
            else
            {
                MessageBox.Show("seleccione una fila por favor");
            }
                
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminarFiltro_Click(object sender, EventArgs e)
        {
            MostrarTodos();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            DateTime FechaInicio = dtpInicio.Value;
            DateTime FechaFin = dtpFinal.Value;
            int opc = cbCantidad.SelectedIndex;
            int cantidad;
            if (opc == 1)
            {
                cantidad = 1;
            } else if (opc == 2)
            {
                cantidad = 0;
            }
            else
            {
                cantidad = 2;
            }

            dataGridView1.DataSource = serviceProducto.MostrarFiltro(FechaInicio, FechaFin, cantidad);
        }
    }
}
