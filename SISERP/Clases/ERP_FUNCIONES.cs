using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public void Limpiar_Controles(Form frm)
        {
            foreach (Control ctrl in frm.Controls)
            {
                if (ctrl is TabControl)
                {
                    foreach (TabPage tbc in ctrl.Controls)
                    {
                        if (tbc is TabPage)
                        {
                            foreach (GroupBox grp in tbc.Controls)
                            {
                                if (grp is GroupBox)
                                {
                                    foreach (Control txt in grp.Controls)
                                    {
                                        if (txt is TextBox)
                                        {
                                            txt.Text = "";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void Habilitar_Controles(Form frm, bool bolSw)
        {
            foreach (Control ctrl in frm.Controls)
            {
                if (ctrl is TabControl)
                {
                    foreach (TabPage tbc in ctrl.Controls)
                    {
                        if (tbc is TabPage)
                        {
                            foreach (GroupBox grp in tbc.Controls)
                            {
                                if (grp is GroupBox)
                                {
                                    //foreach (Control ctrl1 in grp.Controls)
                                    //{
                                    //    //if (txt is TextBox)
                                    //    //{
                                    grp.Enabled = bolSw;
                                    //    //}

                                    //    //if (txt is DateTimePicker)
                                    //    //{
                                    //    //    txt.Enabled = sw;
                                    //    //}
                                    //}
                                }
                            }
                        }
                    }
                }
            }
        }
        public DataTable Retorna_Tabla(string strSQLConsulta)
        {

            SqlConnection cnx = new SqlConnection(conexion.cadena);
            cnx.Open();
            SqlCommand comando = new SqlCommand(strSQLConsulta, cnx);
            comando.CommandTimeout = 200;
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tbl = new DataTable();
            adaptador.Fill(tbl);
            cnx.Close();
            return tbl;
        }
    }
}
