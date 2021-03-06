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
    public partial class Exportacion_excel : Form
    {
        public Exportacion_excel(string ds)
        {
            InitializeComponent();
            this.ds = ds;
        }
        string ds;
        int fechaa, fechab;

        private void Exportacion_excel_Load(object sender, EventArgs e)
        {
            SELECT_FECHA();
        }

        private void SELECT_FECHA()
        {
            OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT FECHA.fecha FROM FECHA ORDER BY FECHA.id ASC", ds);

            DataSet dataset = new DataSet();
            DataTable tabla = new DataTable();

            adaptador.Fill(dataset);
            tabla = dataset.Tables[0];
            this.listView_fechas.Items.Clear();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                DataRow filas = tabla.Rows[i];
                ListViewItem elementos = new ListViewItem(filas["fecha"].ToString());
                listView_fechas.Items.Add(elementos);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CARGAR();
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection conexion = new OleDbConnection(ds);
            conexion.Open();

            //SACAR LA FECHA MENOR
            string sql = "SELECT MIN(id) FROM FECHA";

            OleDbCommand cmd = new OleDbCommand(sql, conexion); //Conexion es tu objeto conexion                                

            int fecha_menor = Convert.ToInt32(cmd.ExecuteScalar());

            //SACAR LA FECHA MAYOR
            string sql2 = "SELECT MAX(id) FROM FECHA";

            OleDbCommand cmd2 = new OleDbCommand(sql2, conexion);

            int fecha_mayor = Convert.ToInt32(cmd2.ExecuteScalar());
            
            //SELECCIONAR TODAS LAS FECHAS DESDE EL ORIGEN

            OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT FECHA.fecha, ORDEN.id_orden, PLATILLO.nombre_platillo, PLATILLO.cantidad, PLATILLO.pagar FROM(FECHA INNER JOIN ORDEN ON FECHA.fecha = ORDEN.fecha) INNER JOIN PLATILLO ON ORDEN.id_orden = PLATILLO.id_orden WHERE FECHA.id >= " + fecha_menor + " AND FECHA.id <= " + fecha_mayor + " ORDER BY FECHA.id ASC", ds);

            DataSet dataset = new DataSet();
            DataTable tabla = new DataTable();

            adaptador.Fill(dataset);
            tabla = dataset.Tables[0];
            this.listView_esta.Items.Clear();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                DataRow filas = tabla.Rows[i];
                ListViewItem elemntos = new ListViewItem(filas["fecha"].ToString());
                elemntos.SubItems.Add(filas["id_orden"].ToString());
                elemntos.SubItems.Add(filas["nombre_platillo"].ToString());
                elemntos.SubItems.Add(filas["cantidad"].ToString());
                elemntos.SubItems.Add(filas["pagar"].ToString());

                listView_esta.Items.Add(elemntos);
            }

            //MOSTRAR LOS DATOS DE LOS GASTOS

            OleDbDataAdapter adaptador2 = new OleDbDataAdapter("SELECT GASTOS.Fecha, GASTOS.Descripcion, GASTOS.Gasto FROM FECHA INNER JOIN GASTOS ON FECHA.fecha = GASTOS.Fecha WHERE FECHA.id >= " + fecha_menor + " AND FECHA.id <= " + fecha_mayor + " ORDER BY FECHA.id ASC", ds);

            DataSet dataset2 = new DataSet();
            DataTable tabla2 = new DataTable();

            adaptador2.Fill(dataset2);
            tabla2 = dataset2.Tables[0];
            this.listView_gastos.Items.Clear();
            for (int i = 0; i < tabla2.Rows.Count; i++)
            {
                DataRow filas2 = tabla2.Rows[i];
                ListViewItem elemntos2 = new ListViewItem(filas2["Fecha"].ToString());
                elemntos2.SubItems.Add(filas2["Descripcion"].ToString());
                elemntos2.SubItems.Add(filas2["Gasto"].ToString());

                listView_gastos.Items.Add(elemntos2);
            }
            conexion.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            xla.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook wb = xla.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xla.ActiveSheet;
            
            int i = 3;
            int j = 1;

            foreach (ListViewItem comp in listView_esta.Items)
            {                
                ws.Cells[2, 1] = ("FECHA");
                ws.Cells[2, 2] = ("ID");
                ws.Cells[2, 3] = ("PLATILLO");
                ws.Cells[2, 4] = ("CANTIDA");
                ws.Cells[2, 5] = ("TOTAL");

                ws.Cells[i, j] = comp.Text.ToString();
                //MessageBox.Show(comp.Text.ToString());
                foreach (ListViewItem.ListViewSubItem drv in comp.SubItems)
                {
                    ws.Cells[i, j] = drv.Text.ToString();
                    j++;
                }
                j = 1;
                i++;
            }
            i = 3;
            j = 7;
            foreach (ListViewItem comp in listView_gastos.Items)
            {
                
                ws.Cells[2, 7] = ("FECHA");
                ws.Cells[2, 8] = ("DESCRIPCION");
                ws.Cells[2, 9] = ("GASTO");

                ws.Cells[i, j] = comp.Text.ToString();
                //MessageBox.Show(comp.Text.ToString());
                foreach (ListViewItem.ListViewSubItem drv in comp.SubItems)
                {
                    ws.Cells[i, j] = drv.Text.ToString();
                    j++;
                }
                j = 7;
                i++;
            }
        }

        private void CARGAR()
        {
            //Primera fecha
            string var1 = fechaA.Text;
            var1 = var1.Substring(0, 2);

            string var2 = fechaA.Text;
            var2 = var2.Substring(3, 2);

            string var3 = fechaA.Text;
            var3 = var3.Substring(6, 4);

            //juntando las cadenas
            string FECHAA = string.Concat(var3, var2, var1);
            fechaa = Convert.ToInt32(FECHAA);
            //----------------

            //Segunda fecha
            var1 = fechaB.Text;
            var1 = var1.Substring(0, 2);

            var2 = fechaB.Text;
            var2 = var2.Substring(3, 2);

            var3 = fechaB.Text;
            var3 = var3.Substring(6, 4);

            //juntando las cadenas
            string FECHAB = string.Concat(var3, var2, var1);
            fechab = Convert.ToInt32(FECHAB);


            OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT FECHA.fecha, ORDEN.id_orden, PLATILLO.nombre_platillo, PLATILLO.cantidad, PLATILLO.pagar FROM(FECHA INNER JOIN ORDEN ON FECHA.fecha = ORDEN.fecha) INNER JOIN PLATILLO ON ORDEN.id_orden = PLATILLO.id_orden WHERE FECHA.id >= " + fechaa + " AND FECHA.id <= " + fechab + " ORDER BY FECHA.id ASC", ds);

            DataSet dataset = new DataSet();
            DataTable tabla = new DataTable();

            adaptador.Fill(dataset);
            tabla = dataset.Tables[0];
            this.listView_esta.Items.Clear();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                DataRow filas = tabla.Rows[i];
                ListViewItem elemntos = new ListViewItem(filas["fecha"].ToString());
                elemntos.SubItems.Add(filas["id_orden"].ToString());
                elemntos.SubItems.Add(filas["nombre_platillo"].ToString());
                elemntos.SubItems.Add(filas["cantidad"].ToString());
                elemntos.SubItems.Add(filas["pagar"].ToString());

                listView_esta.Items.Add(elemntos);
            }

            //MOSTRAR LOS DATOS DE LOS GASTOS

            OleDbDataAdapter adaptador2 = new OleDbDataAdapter("SELECT GASTOS.Fecha, GASTOS.Descripcion, GASTOS.Gasto FROM FECHA INNER JOIN GASTOS ON FECHA.fecha = GASTOS.Fecha WHERE FECHA.id >= " + fechaa + " AND FECHA.id <= " + fechab + " ORDER BY FECHA.id ASC", ds);

            DataSet dataset2 = new DataSet();
            DataTable tabla2 = new DataTable();

            adaptador2.Fill(dataset2);
            tabla2 = dataset2.Tables[0];
            this.listView_gastos.Items.Clear();
            for (int i = 0; i < tabla2.Rows.Count; i++)
            {
                DataRow filas2 = tabla2.Rows[i];
                ListViewItem elemntos2 = new ListViewItem(filas2["Fecha"].ToString());
                elemntos2.SubItems.Add(filas2["Descripcion"].ToString());
                elemntos2.SubItems.Add(filas2["Gasto"].ToString());

                listView_gastos.Items.Add(elemntos2);
            }
        }
    }
}
