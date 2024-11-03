using CAPA_DATOS;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SISERP.Clases
{
    public class ERP_FUNCIONES
    {
        public void Llenar_Combo(DataTable tbl, ComboBox cbo, string strDisplayMember, string strValueMember)
        {
            cbo.DataSource = tbl;
            cbo.DisplayMember = strDisplayMember;
            cbo.ValueMember = strValueMember;
        }
        public void Limpiar_Controles(DevComponents.DotNetBar.Office2007Form frm/*Form frm*/)
        {
            //foreach (Control ctrl in frm.Controls)
            //{
            //    if (ctrl is TabControl)
            //    {
            //        foreach (TabPage tbc in ctrl.Controls)
            //        {
            //            if (tbc is TabPage)
            //            {
            //                foreach (GroupBox grp in tbc.Controls)
            //                {
            //                    if (grp is GroupBox)
            //                    {
            //                        foreach (Control txt in grp.Controls)
            //                        {
            //                            if (txt is TextBox)
            //                            {
            //                                txt.Text = "";
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            foreach (Control ctrl in frm.Controls)
            {
                if (ctrl is SuperTabControl superTabControl)
                {
                    foreach (SuperTabItem tabItem in superTabControl.Tabs)
                    {
                        // Accede al TabPanel de cada SuperTabItem para obtener sus controles
                        if (tabItem.AttachedControl != null)
                        {
                            foreach (Control panelControl in tabItem.AttachedControl.Controls)
                            {
                                if (panelControl is GroupBox groupBox)
                                {
                                    foreach (Control txt in groupBox.Controls)
                                    {
                                        if (txt is TextBoxX textBox)
                                        {
                                            textBox.Text = ""; // Clear TextBoxX
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void Habilitar_Controles(DevComponents.DotNetBar.Office2007Form frm/*Form frm*/, bool bolSw)
        {
            //foreach (Control ctrl in frm.Controls)
            //{
            //    if (ctrl is TabControl)
            //    {
            //        foreach (TabPage tbc in ctrl.Controls)
            //        {
            //            if (tbc is TabPage)
            //            {
            //                foreach (GroupBox grp in tbc.Controls)
            //                {
            //                    if (grp is GroupBox)
            //                    {
            //                        //foreach (Control ctrl1 in grp.Controls)
            //                        //{
            //                        //    //if (txt is TextBox)
            //                        //    //{
            //                        grp.Enabled = bolSw;
            //                        //    //}

            //                        //    //if (txt is DateTimePicker)
            //                        //    //{
            //                        //    //    txt.Enabled = sw;
            //                        //    //}
            //                        //}
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            foreach (Control ctrl in frm.Controls)
            {
                if (ctrl is SuperTabControl superTabControl)
                {
                    foreach (SuperTabItem tabItem in superTabControl.Tabs)
                    {
                        // Accede al TabPanel de cada SuperTabItem para obtener sus controles
                        if (tabItem.AttachedControl != null)
                        {
                            foreach (Control panelControl in tabItem.AttachedControl.Controls)
                            {
                                if (panelControl is GroupBox groupBox)
                                {
                                    groupBox.Enabled = bolSw;
                                }
                            }
                        }
                    }
                }
            }

        }
        public DataTable Retorna_Tabla(string strSQLConsulta)
        {
            //SqlConnection cnx = new SqlConnection(conexion.cadena);
            //cnx.Open();
            //SqlCommand comando = new SqlCommand(strSQLConsulta, cnx);
            //comando.CommandTimeout = 200;
            //SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            //DataTable tbl = new DataTable();
            //adaptador.Fill(tbl);
            //cnx.Close();
            //return tbl;

            MySqlConnection cnx = new MySqlConnection(conexion.cadena);
            cnx.Open();
            MySqlCommand comando = new MySqlCommand(strSQLConsulta, cnx);
            comando.CommandTimeout = 200;
            MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
            DataTable tbl = new DataTable();
            adaptador.Fill(tbl);
            cnx.Close();
            return tbl;
        }
    }
}
