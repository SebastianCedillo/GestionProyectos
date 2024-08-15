using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionProyectos.Controllers;
using GestionProyectos.Models;

namespace GestionProyectos.Views
{
    public partial class frmEmpleados : Form
    {

        EmpleadosController controller;
        public frmEmpleados()
        {
            InitializeComponent();
           controller = new EmpleadosController();


        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            Actualizar();



        }


        private void Actualizar()
        {
            var empleado = controller.ObtenerEmpleados();
            dgvEmpleados.DataSource = empleado; 
        
        }

        public void limpiarCajas()
        {
            txtid.Text = "";
            txtnombre.Text = "";
            txtapellido.Text = "";
            textemail.Text = "";
            posicion.Text = "";

        }

        private void dgvEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dgvEmpleados_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvEmpleados.SelectedRows.Count > 0)
            {
                var empleadoSeleccionado = (EmpleadosModelos)dgvEmpleados.SelectedRows[0].DataBoundItem;

                txtid.Text = empleadoSeleccionado.EmpleadoId.ToString();
                txtnombre.Text = empleadoSeleccionado.Nombre;
                txtapellido.Text = empleadoSeleccionado.Apellido;
                textemail.Text = empleadoSeleccionado.Email;
                posicion.Text = empleadoSeleccionado.Posicion;
            }
            else
            {

                limpiarCajas();
            }


        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (dgvEmpleados.SelectedRows.Count > 0)
            {
                var empleadoSeleccionado = (EmpleadosModelos)dgvEmpleados.SelectedRows[0].DataBoundItem;

                
                txtid.Text = empleadoSeleccionado.EmpleadoId.ToString();
                txtnombre.Text = empleadoSeleccionado.Nombre;
                txtapellido.Text = empleadoSeleccionado.Apellido;
                textemail.Text = empleadoSeleccionado.Email;
                posicion.Text = empleadoSeleccionado.Posicion;

                Actualizar();

                
            }
            else
            {
                MessageBox.Show("Selecciona un empleado para modificar.");
            }


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            int empleadoId = 0;
            if (!string.IsNullOrEmpty(txtid.Text))
            {
                empleadoId = Convert.ToInt32(txtid.Text);
            }

            string nombre = txtnombre.Text;
            string apellido = txtapellido.Text;
            string email = textemail.Text;
            string posicion = this.posicion.Text;

            
            EmpleadosModelos empleado = new EmpleadosModelos
            {
                EmpleadoId = empleadoId,
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Posicion = posicion
            };

           controller.GuardarEmpleado(empleado);

            
            Actualizar();

            limpiarCajas();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count > 0)
            {
                int empleadoId = Convert.ToInt32(dgvEmpleados.SelectedRows[0].Cells[0].Value);

                
                DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar este empleado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    controller.EliminarEmpleado(empleadoId);
                    Actualizar(); // Actualizar el DataGridView después de eliminar
                }
            }
            else
            {
                MessageBox.Show("Selecciona un empleado para eliminar.");
            }


        }
    }
    }

