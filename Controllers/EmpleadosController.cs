using GestionProyectos.config;
using GestionProyectos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionProyectos.Controllers
{
    internal class EmpleadosController
    {

        ConexionBDD conexion = new ConexionBDD();

        public List<EmpleadosModelos> ObtenerEmpleados()
        {
            List<EmpleadosModelos> empleadosLista = new List<EmpleadosModelos>();

            try
            {
                String consulta = "SELECT * FROM Empleados";
                SqlDataAdapter lector = new SqlDataAdapter(consulta, conexion.AbrirConexion());
                DataTable tablaEmpleados = new DataTable();
                lector.Fill(tablaEmpleados);

                foreach (DataRow fila in tablaEmpleados.Rows)
                {
                    EmpleadosModelos empleados = new EmpleadosModelos
                    {
                        EmpleadoId = Convert.ToInt32(fila["empleado_id"]),
                        Nombre = fila["nombre"].ToString(),
                        Apellido = fila["apellido"].ToString(),
                        Email = fila["email"].ToString(),
                        Posicion = fila["posicion"].ToString()

                    };

                    empleadosLista.Add(empleados);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
            finally
            {
                conexion.CerrarConexion();
            }

            return empleadosLista;


        }



        public void ModificarEmpleado(EmpleadosModelos empleado)
        {

            try
            {
                string consulta = "UPDATE Empleados SET nombre = @Nombre, apellido = @Apellido, email = @Email, posicion = @Posicion WHERE empleado_id = @EmpleadoId";
                SqlCommand cmd = new SqlCommand(consulta,conexion.AbrirConexion());

               cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", empleado.Apellido);

                cmd.Parameters.AddWithValue("@Email",empleado.Email);
                cmd.Parameters.AddWithValue("@Posicion", empleado.Posicion);
                cmd.Parameters.AddWithValue("@EmpleadoId", empleado.EmpleadoId);

             int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    MessageBox.Show("Empleado modificado correctamente ");

                }
                else
                {
                    MessageBox.Show("No se pudo modificar el empleado ");

                }

              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
            finally
            {
                conexion.CerrarConexion();
            }


        }


    }
}
