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



        public List<EmpleadosModelos> ObtenerEmpleadoPorId(int id)
        {
            List<EmpleadosModelos> empleados = new List<EmpleadosModelos>();

            try
            {
                string consulta = "SELECT * FROM Empleados WHERE empleado_id = @Id";
                SqlCommand cmd = new SqlCommand(consulta, conexion.AbrirConexion());

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader lector = cmd.ExecuteReader();

                if (lector.Read())
                {
                   EmpleadosModelos empleado = new EmpleadosModelos
                    {
                        EmpleadoId = Convert.ToInt32(lector["empleado_id"]),
                        Nombre = lector["nombre"].ToString(),
                        Apellido = lector["apellido"].ToString(),
                        Email = lector["email"].ToString(),
                        Posicion = lector["posicion"].ToString()
                    };
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el empleado: " + ex.Message);

            }
            finally
            {
                conexion.CerrarConexion();
            }
            return empleados;
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



        public void GuardarEmpleado(EmpleadosModelos empleado)
        {

            try
            {
                string consulta = "INSERT INTO Empleados (nombre, apellido, email, posicion) VALUES (@Nombre, @Apellido, @Email, @Posicion)";
                SqlCommand cmd = new SqlCommand(consulta, conexion.AbrirConexion());

                cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", empleado.Apellido);

                cmd.Parameters.AddWithValue("@Email", empleado.Email);
                cmd.Parameters.AddWithValue("@Posicion", empleado.Posicion);
                cmd.Parameters.AddWithValue("@EmpleadoId", empleado.EmpleadoId);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    MessageBox.Show("Empleado guardado correctamente ");

                }
                else
                {
                    MessageBox.Show("No se pudo guardar el empleado ");

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
