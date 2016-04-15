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

namespace empanada_2
{
    public partial class Pantalla2 : Form, IForm, Terminar_orden
    {
        public Pantalla2(string fecha, string ds)
        {
            InitializeComponent();
            this.ds = ds;
            this.fecha = fecha;
        }
        
        string ds, fecha;
        int id_orden;
        #region IForm Members

        public void ChangeTextBoxText(string text, int id)
        {
            if (Convert.ToInt32(listView_platillos.Items.Count) == 0)
            {
                textBox1.Text = text;

                OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT id_orden, Platillo, Cantidad FROM VISUALIZADO WHERE id_orden = " + id, ds);

                DataSet dataset = new DataSet();
                DataTable tabla = new DataTable();

                adaptador.Fill(dataset);
                tabla = dataset.Tables[0];
                this.listView_platillos.Items.Clear();
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    DataRow filas = tabla.Rows[i];
                    ListViewItem elemntos = new ListViewItem(filas["id_orden"].ToString());
                    elemntos.SubItems.Add(filas["Platillo"].ToString());
                    elemntos.SubItems.Add(filas["Cantidad"].ToString());

                    listView_platillos.Items.Add(elemntos);
                }
            }
            else
            {
                if (Convert.ToInt32(listView1.Items.Count) == 0)
                {
                    textBox2.Text = text;

                    OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT id_orden, Platillo, Cantidad FROM VISUALIZADO WHERE id_orden = " + id, ds);

                    DataSet dataset = new DataSet();
                    DataTable tabla = new DataTable();

                    adaptador.Fill(dataset);
                    tabla = dataset.Tables[0];
                    this.listView1.Items.Clear();
                    for (int i = 0; i < tabla.Rows.Count; i++)
                    {
                        DataRow filas = tabla.Rows[i];
                        ListViewItem elemntos = new ListViewItem(filas["id_orden"].ToString());
                        elemntos.SubItems.Add(filas["Platillo"].ToString());
                        elemntos.SubItems.Add(filas["Cantidad"].ToString());

                        listView1.Items.Add(elemntos);
                    }
                }
                else
                {
                    if (Convert.ToInt32(listView2.Items.Count) == 0)
                    {
                        textBox3.Text = text;

                        OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT id_orden, Platillo, Cantidad FROM VISUALIZADO WHERE id_orden = " + id, ds);

                        DataSet dataset = new DataSet();
                        DataTable tabla = new DataTable();

                        adaptador.Fill(dataset);
                        tabla = dataset.Tables[0];
                        this.listView2.Items.Clear();
                        for (int i = 0; i < tabla.Rows.Count; i++)
                        {
                            DataRow filas = tabla.Rows[i];
                            ListViewItem elemntos = new ListViewItem(filas["id_orden"].ToString());
                            elemntos.SubItems.Add(filas["Platillo"].ToString());
                            elemntos.SubItems.Add(filas["Cantidad"].ToString());

                            listView2.Items.Add(elemntos);
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(listView3.Items.Count) == 0)
                        {
                            textBox4.Text = text;

                            OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT id_orden, Platillo, Cantidad FROM VISUALIZADO WHERE id_orden = " + id, ds);

                            DataSet dataset = new DataSet();
                            DataTable tabla = new DataTable();

                            adaptador.Fill(dataset);
                            tabla = dataset.Tables[0];
                            this.listView3.Items.Clear();
                            for (int i = 0; i < tabla.Rows.Count; i++)
                            {
                                DataRow filas = tabla.Rows[i];
                                ListViewItem elemntos = new ListViewItem(filas["id_orden"].ToString());
                                elemntos.SubItems.Add(filas["Platillo"].ToString());
                                elemntos.SubItems.Add(filas["Cantidad"].ToString());

                                listView3.Items.Add(elemntos);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(listView4.Items.Count) == 0)
                            {
                                textBox5.Text = text;

                                OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT id_orden, Platillo, Cantidad FROM VISUALIZADO WHERE id_orden = " + id, ds);

                                DataSet dataset = new DataSet();
                                DataTable tabla = new DataTable();

                                adaptador.Fill(dataset);
                                tabla = dataset.Tables[0];
                                this.listView4.Items.Clear();
                                for (int i = 0; i < tabla.Rows.Count; i++)
                                {
                                    DataRow filas = tabla.Rows[i];
                                    ListViewItem elemntos = new ListViewItem(filas["id_orden"].ToString());
                                    elemntos.SubItems.Add(filas["Platillo"].ToString());
                                    elemntos.SubItems.Add(filas["Cantidad"].ToString());

                                    listView4.Items.Add(elemntos);
                                }
                            }

                            else
                            {
                                MessageBox.Show("Todos las comandas estan llenas ! ! ", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Terminar_orden Members

        public void orden1()
        {
            id_orden = Convert.ToInt32(listView_platillos.GetItemAt(0, 0));
            MessageBox.Show(id_orden.ToString());
            listView_platillos.Items.Clear();
            textBox1.Text = "";

            OleDbConnection conexion = new OleDbConnection(ds);
            
            conexion.Open();
            string insertar = "DELETE FROM VISUALIZADO WHERE id_orden = " + id_orden;
            OleDbCommand cmd = new OleDbCommand(insertar, conexion);

            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public void orden2()
        {
            listView1.Items.Clear();
            textBox2.Text = "";

            id_orden = Convert.ToInt32(this.listView1.Items[0]);

            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            string insertar = "DELETE FROM VISUALIZADO WHERE id_orden = " + id_orden;
            OleDbCommand cmd = new OleDbCommand(insertar, conexion);

            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public void orden3()
        {
            listView2.Items.Clear();
            textBox3.Text = "";

            id_orden = Convert.ToInt32(this.listView2.Items[0]);

            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            string insertar = "DELETE FROM VISUALIZADO WHERE id_orden = " + id_orden;
            OleDbCommand cmd = new OleDbCommand(insertar, conexion);

            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public void orden4()
        {
            listView3.Items.Clear();
            textBox4.Text = "";

            id_orden = Convert.ToInt32(this.listView3.Items[0]);

            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            string insertar = "DELETE FROM VISUALIZADO WHERE id_orden = " + id_orden;
            OleDbCommand cmd = new OleDbCommand(insertar, conexion);

            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public void orden5()
        {
            listView4.Items.Clear();
            textBox5.Text = "";

            id_orden = Convert.ToInt32(this.listView4.Items[0]);

            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            string insertar = "DELETE FROM VISUALIZADO WHERE id_orden = " + id_orden;
            OleDbCommand cmd = new OleDbCommand(insertar, conexion);

            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        
        #endregion

        private void Pantalla2cs_Load(object sender, EventArgs e)
        {
            Form1 form = new Form1(fecha, ds);
            form.Show(this);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void listView_platillos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            orden1();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            orden2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            orden3();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            orden4();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            orden5();
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
