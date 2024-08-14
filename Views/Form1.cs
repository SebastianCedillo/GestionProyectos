using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionProyectos.config;
using System.Data.SqlClient;
using System.Data;


namespace GestionProyectos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ConexionBDD conex = new ConexionBDD();
            conex.AbrirConexion();
            
           



        }
    }
}
