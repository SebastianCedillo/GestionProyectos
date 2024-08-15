using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionProyectos.Controllers;
using GestionProyectos.Models;

namespace GestionProyectos.Views
{
    public partial class frmProyectos : Form
    {

        ProyectosController controller;

        public frmProyectos()
        {
            InitializeComponent();
            controller = new ProyectosController();
        }

        private void frmProyectos_Load(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            var proyectos = controller.ObtenerProyectos();
            dgvProyectos.DataSource = proyectos;

        }

        private void LimpiarCajas()
        {
            txtid.Text = "";
            txtnombre.Text = "";
            txtdescripcion.Text = "";
            dtpfechaInicio.Value = DateTime.Now;
            dtpfechaFin.Checked = false;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {


            int proyectoId = 0;
            if (!string.IsNullOrEmpty(txtid.Text))
            {
                proyectoId = Convert.ToInt32(txtid.Text);
            }

            string nombre = txtnombre.Text;
            string descripcion = txtdescripcion.Text;
            DateTime fechaInicio = dtpfechaInicio.Value;
            DateTime fechaFin = dtpfechaFin.Value;


                ProyectosModelos proyecto = new ProyectosModelos
                {
                    ProyectoId = proyectoId,
                    Nombre = nombre,
                    Descripcion = descripcion,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin

                };

            controller.GuardarProyecto(proyecto);


            Actualizar();

           LimpiarCajas();



        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (dgvProyectos.SelectedRows.Count > 0)
            {
                var proyectoSeleccionado = (ProyectosModelos)dgvProyectos.SelectedRows[0].DataBoundItem;


                txtid.Text = proyectoSeleccionado.ProyectoId.ToString();
                txtnombre.Text = proyectoSeleccionado.Nombre;
                txtdescripcion.Text = proyectoSeleccionado.Descripcion;
                dtpfechaInicio.Value = proyectoSeleccionado.FechaInicio;
                dtpfechaFin.Value = proyectoSeleccionado.FechaFin;


                txtid.Enabled = false;
                txtnombre.ReadOnly = false;
                txtdescripcion.ReadOnly = false;
                dtpfechaInicio.Enabled = true;
                dtpfechaFin.Enabled = true;


            }
            else
            {
                MessageBox.Show("Selecciona un proyecto para modificar.");
            }







        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProyectos.SelectedRows.Count > 0)
            {
                int proyectoId = Convert.ToInt32(dgvProyectos.SelectedRows[0].Cells[0].Value);


                DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar este proyecto?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    controller.EliminarProyecto(proyectoId);
                    Actualizar(); 
                }
            }
            else
            {
                MessageBox.Show("Selecciona un empleado para eliminar.");
            }


        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            LimpiarCajas();
            txtid.Enabled = false;
            txtnombre.ReadOnly = false;
            txtdescripcion.ReadOnly = false;


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            LimpiarCajas();
            txtid.Enabled = true;
            txtnombre.ReadOnly = true;
            txtdescripcion.ReadOnly = true;

        }
    }
}
