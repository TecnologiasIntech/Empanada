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
    public partial class Control_acceso : Form
    {
        public Control_acceso(string ds,string ds2)
        {
            //---------
            Thread t = new Thread(new ThreadStart(splashtart));
            t.Start();
            Thread.Sleep(5000);
            InitializeComponent();
            t.Abort();
            //---------
            this.ds = ds;
            this.ds2 = ds2;
        }

        string comboBox1 = "SELECCIONAR";

        private void splashtart()
        {
            Application.Run(new Form2(ds));
        }

        private void LIMPIAR()
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1 = "SELECCIONAR";
        }
        
        string ds,ds2,operador;
        int band;              

        int veces = 0;
        private const int intentos = 2;


        //para ingresar
        private void CONECTAR()
        {
            //checamos la encriptacion
            string var1;
            var1 = Encriptado.Encriptar(textBox2.Text);
            //--------               

            OleDbConnection conexion = new OleDbConnection(ds2);
            conexion.Open();

            string select2 = "SELECT * FROM USUARIOS where USUARIOS.nombre='" + textBox1.Text + "'and USUARIOS.clave='" + var1 + "'";
            OleDbCommand cmd6 = new OleDbCommand(select2, conexion);
            try
            {
                OleDbDataReader reader = cmd6.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CHECAR2();
                    }
                }
                else
                {
                    if (veces == 2)
                    {
                        MessageBox.Show("Has excedido el limite permitido ", "conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Su Usuario o Contraseña o Tipo NO Coinciden o son Erroneas \n \n                        Le Quedan " + (intentos - veces) + " Intento(s)", "Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LIMPIAR();
                        veces = veces + 1;
                        textBox1.Focus();
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error orden" + ex, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private void CHECAR2()
        {
            if ((comboBox1 == "ROOT") || (comboBox1 == "ADMINISTRADOR") || (comboBox1 == "OPERADOR"))
            {
                textBox1.Focus();
                TIPO();
            }
            else if ((comboBox1 != "ROOT") || (comboBox1 != "ADMINISTRADOR") || (comboBox1 != "OPERADOR"))
            {
                if (veces == 3)
                {
                    MessageBox.Show("Has excedido el limite permitido ", "conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Su Usuario o Contraseña o Tipo NO Coinciden o son Erroneas \n \n                        Le Quedan " + (intentos - veces) + " Intento(s)", "Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LIMPIAR();
                    veces = veces + 1;
                    textBox1.Focus();
                }
            }
        }

        private void TIPO()
        {
            if (comboBox1 == "ROOT")
            {
                band = 0;
                Users corre = new Users(ds,ds2, band,operador);
                corre.Show();                
                LIMPIAR();
                this.Hide();
            }
            else if (comboBox1 == "ADMINISTRADOR")
            {
                band = 1;
                Users corre = new Users(ds,ds2, band,operador);
                corre.usuariosToolStripMenuItem.Visible = false;                
                corre.Show();                
                LIMPIAR();
                this.Hide();
            }
            else if(comboBox1 =="OPERADOR")
            {
                operador = textBox1.Text;
                band = 3;
                Inicio form = new Inicio(ds,band,operador);                
                LIMPIAR();
                form.Show();
                this.Hide();
            }
        }

        private void CHECAR()
        {
            do
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Ingrese su Nombre para Continuar", "conexion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox1.Focus();
                    return;
                }
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Ingrese su Clave para Continuar", "conexion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox2.Focus();
                    return;
                }
                if ((textBox1.Text != "") && (textBox2.Text != ""))
                {
                    CONECTAR();
                    break;
                }
            } while (true);

        }

        private void Control_acceso_Load(object sender, EventArgs e)
        {           
        }      

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection conexion = new OleDbConnection(ds2);
            conexion.Open();

            //verificar el tipo de usuario-------------------------
            string select = "SELECT tipo_usuario FROM USUARIOS where USUARIOS.nombre='" + textBox1.Text + "'";
            OleDbCommand cmd = new OleDbCommand(select, conexion);
            try
            {
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comboBox1 = reader.GetString(0);
                    }
                }
                else
                {
                    //MessageBox.Show("No se pudo", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error carga" + ex, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexion.Close();
            ///----------------------------------------------------

            CHECAR();
        }

    }
}
