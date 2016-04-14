﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;

namespace empanada_2
{
    public partial class Inicio : Form
    {
        public Inicio(string ds)
        {                        
            //---------
            Thread t = new Thread(new ThreadStart(splashtart));
            t.Start();
            Thread.Sleep(5000);
            InitializeComponent();
            t.Abort();
            //---------
            this.ds = ds;
        }

        private void splashtart()
        {
            Application.Run(new Form2(ds));
        }

        //CONEXION
        string ds;
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fechahoy = DateTime.Now;
            string fecha = fechahoy.ToString("d");
            string consulta;
            try
            {
                OleDbConnection conexion = new OleDbConnection(ds);

                conexion.Open();

                string consultar = "SELECT fecha FROM FECHA WHERE fecha = '" + fecha + "'";
                OleDbCommand con = new OleDbCommand(consultar, conexion);
                consulta = (con.ExecuteScalar()).ToString();

                if (consulta == null)
                {
                    string insertar = "INSERT INTO FECHA (fecha) VALUES (@fecha)";
                    OleDbCommand cmd = new OleDbCommand(insertar, conexion);
                    cmd.Parameters.AddWithValue("@fecha", fecha);

                    cmd.ExecuteNonQuery();

                    conexion.Close();

                    Pantalla2 form = new Pantalla2(fecha, ds);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Ya iniciaste el dia de hoy! \n Vaya a la seccion CONTINUAR DIA para proseguir con su dia de hoy.");
                }
            }
                
                

            catch (DBConcurrencyException ex)
            {
                MessageBox.Show("Error de concurrencia:\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }

            
                       

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Fechas form = new Fechas(ds);
            form.Show();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menu form = new Menu(ds);
            form.Show();

        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
