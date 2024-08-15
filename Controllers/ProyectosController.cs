using GestionProyectos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProyectos.config;
using System.Drawing;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace GestionProyectos.Controllers
{
    internal class ProyectosController
    {

        ConexionBDD conexion = new ConexionBDD();

        public List<ProyectosModelos> ObtenerProyectos()
        {
            List<ProyectosModelos> proyectosLista = new List<ProyectosModelos>();

            try
            {
                String consulta = "SELECT * FROM Proyectos";
                SqlDataAdapter lector = new SqlDataAdapter(consulta, conexion.AbrirConexion());
                DataTable tablaProyectos = new DataTable();
                lector.Fill(tablaProyectos);

                foreach (DataRow fila in tablaProyectos.Rows)
                {
                    ProyectosModelos proyecto = new ProyectosModelos
                    {
                        ProyectoId = Convert.ToInt32(fila["proyecto_id"]),
                        Nombre = fila["nombre"].ToString(),
                        Descripcion = fila["descripcion"].ToString(),
                        FechaInicio = Convert.ToDateTime(fila["fecha_inicio"]),
                        FechaFin = Convert.ToDateTime(fila["fecha_fin"])

                    };

                    proyectosLista.Add(proyecto);

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

            return proyectosLista;


        }


        public List<ProyectosModelos> ObtenerProyectoPorId(int id)
        {
            List<ProyectosModelos> proyectosLista = new List<ProyectosModelos>();

            try
            {
                string consulta = "SELECT * FROM Proyectos WHERE proyecto_id = @Id";
                SqlCommand cmd = new SqlCommand(consulta, conexion.AbrirConexion());

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader lector = cmd.ExecuteReader();

                if (lector.Read())
                {
                    ProyectosModelos proyecto = new ProyectosModelos
                    {
                        ProyectoId = Convert.ToInt32(lector["proyecto_id"]),
                        Nombre = lector["nombre"].ToString(),
                        Descripcion = lector["descripcion"].ToString(),
                        FechaInicio = Convert.ToDateTime(lector["fecha_inicio"]),
                        FechaFin = Convert.ToDateTime(lector["fecha_fin"])
                    };

                    proyectosLista.Add(proyecto);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el proyecto: " + ex.Message);

            }
            finally
            {
                conexion.CerrarConexion();
            }
            return proyectosLista;
        }



        public void GuardarProyecto(ProyectosModelos proyecto)
        {

            try
            {
                string consulta = "INSERT INTO Proyectos (nombre, descripcion, fecha_inicio, fecha_fin) VALUES (@Nombre, @Descripcion, @FechaInicio, @FechaFin)";
                SqlCommand cmd = new SqlCommand(consulta, conexion.AbrirConexion());

                cmd.Parameters.AddWithValue("@Nombre", proyecto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", proyecto.Descripcion);
                cmd.Parameters.AddWithValue("@FechaInicio", proyecto.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", proyecto.FechaFin);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    MessageBox.Show("Proyecto guardado correctamente ");

                }
                else
                {
                    MessageBox.Show("No se pudo guardar el proyecto ");

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



        public void ModificarProyecto(ProyectosModelos proyecto)
        {

            try
            {
                string consulta = "UPDATE Proyectos SET nombre = @Nombre, descripcion = @Descripcion, fecha_inicio = @FechaInicio, fecha_fin = @FechaFin WHERE proyecto_id = @ProyectoId";
                SqlCommand cmd = new SqlCommand(consulta, conexion.AbrirConexion());

                cmd.Parameters.AddWithValue("@Nombre", proyecto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", proyecto.Descripcion);
                cmd.Parameters.AddWithValue("@FechaInicio", proyecto.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", proyecto.FechaFin);
                cmd.Parameters.AddWithValue("@ProyectoId", proyecto.ProyectoId);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    MessageBox.Show("Proyecto modificado correctamente ");

                }
                else
                {
                    MessageBox.Show("No se pudo modificar el proyecto ");

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


        public void EliminarProyecto(int id)
        {

            try
            {
                string consulta = "DELETE FROM Proyectos WHERE proyecto_id = @Id";
                SqlCommand cmd = new SqlCommand(consulta, conexion.AbrirConexion());

                cmd.Parameters.AddWithValue("@Id", id);


                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    MessageBox.Show("Proyecto eliminado correctamente ");

                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el proyecto ");

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
