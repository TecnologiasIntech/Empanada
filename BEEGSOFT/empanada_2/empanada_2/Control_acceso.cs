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
    public partial class Control_acceso : Form
    {
        public Control_acceso(string fecha,string ds)
        {
            InitializeComponent();
            this.ds = ds;
            this.fecha = fecha;                       
        }

        private void LIMPIAR()
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Text = "SELECCIONAR";
        }
        
        string ds;
        string fecha;

        string texto;
        int band;      
        Encriptado encri = new Encriptado();

        int veces = 0;
        private const int intentos = 2;

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == "SELECCIONAR")
            {
                MessageBox.Show("Seleccione Tipo de Usuario", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
            }                                 

            if (textBox1.Text == "")
            {
                MessageBox.Show("Digite Usuario para Continuar", "CompuBinario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Digite Clave para Continuar", "CompuBinario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox2.Focus();
            }            
            else
            {
                OleDbConnection conexion = new OleDbConnection(ds);
                conexion.Open();
                string select="SELECT * FROM USUARIOS where USUARIOS.nombre='" + textBox1.Text + "'and USUARIOS.clave='" + textBox2.Text + "'and USUARIOS.tipo_usuario='" + comboBox1.Text + "'";
                OleDbCommand cmd6 = new OleDbCommand(select, conexion);
                try
                {
                    OleDbDataReader reader = cmd6.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            MessageBox.Show("Usuario Aceptado", "Empanada", MessageBoxButtons.OK, MessageBoxIcon.Information);                            
                            Pantalla2 corre = new Pantalla2(fecha, ds);
                            LIMPIAR();
                            corre.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Su Usuario o Contraseña o Tipo NO Coinciden o son Erroneas \n \n                        Le Quedan " + (intentos - veces) + " Intento(s)", "Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LIMPIAR();
                        veces = veces + 1;                        
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error orden" + ex, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (veces == 2)
                {
                    MessageBox.Show("Has excedido el limite permitido ", "Coneccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();                    
                }
            }
        }


        private void registrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            band = 0;
            if (this.comboBox1.Text == "SELECCIONAR")
            {
                MessageBox.Show("Para Agregar un usuario tienes que identificarte", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
            }            

            if (textBox1.Text == "")
            {
                MessageBox.Show("Para Agregar un usuario tienes que identificarte", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Para Agregar un usuario tienes que identificarte", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
            }
            else
            {
                OleDbConnection conexion = new OleDbConnection(ds);
                conexion.Open();
                string select = "SELECT * FROM USUARIOS where nombre='" + textBox1.Text + "'and clave='" + textBox2.Text + "'and tipo_usuario='" + comboBox1.Text + "'";
                OleDbCommand cmd6 = new OleDbCommand(select, conexion);
                try
                {
                    OleDbDataReader reader = cmd6.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {                                                        
                            Nuevo_usuario corre = new Nuevo_usuario(ds,texto,band);
                            LIMPIAR();
                            corre.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Su Usuario o Contraseña o Tipo NO Coinciden o son Erroneas \n \n                        Le Quedan " + (intentos - veces) + " Intento(s)", "CompuBinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LIMPIAR();
                        veces = veces + 1;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error orden" + ex, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (veces == 2)
                {
                    MessageBox.Show("Has excedido el limite permitido ", "Coneccion", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                    this.Close();
                }
            }
        }

        private void UsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {            
          
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            band = 1;
            if (this.comboBox1.Text == "SELECCIONAR")
            {
                MessageBox.Show("Para Agregar un usuario tienes que identificarte", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
            }

            if (textBox1.Text == "")
            {
                MessageBox.Show("Para Agregar un usuario tienes que identificarte", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Para Agregar un usuario tienes que identificarte", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
            }
            else
            {
                OleDbConnection conexion = new OleDbConnection(ds);
                conexion.Open();
                string select = "SELECT * FROM USUARIOS where nombre='" + textBox1.Text + "'and clave='" + textBox2.Text + "'and tipo_usuario='" + comboBox1.Text + "'";
                OleDbCommand cmd6 = new OleDbCommand(select, conexion);
                try
                {
                    OleDbDataReader reader = cmd6.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            texto = textBox1.Text;
                            Nuevo_usuario corre = new Nuevo_usuario(ds, texto, band);
                            LIMPIAR();
                            corre.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Su Usuario o Contraseña o Tipo NO Coinciden o son Erroneas \n \n                        Le Quedan " + (intentos - veces) + " Intento(s)", "CompuBinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LIMPIAR();
                        veces = veces + 1;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error orden" + ex, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (veces == 2)
                {
                    MessageBox.Show("Has excedido el limite permitido ", "Coneccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }
    }
}