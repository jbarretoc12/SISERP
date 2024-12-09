using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using CAPA_NEGOCIOS.CONTABILIDAD;

namespace CAPA_DATOS.CONTABILIDAD
{
    public static class DAT_CON_PLAN_CONTABLE_TIPO_CUENTA
    {
        public static DataTable sp_tb_con_plan_contable_tipo_cuenta_ls(NEG_CON_PLAN_CONTABLE_TIPO_CUENTA neg)
        {
            MySqlConnection cn = new MySqlConnection(conexion.cadena);
            MySqlCommand cmd = new MySqlCommand("sp_tb_con_plan_contable_tipo_cuenta_ls", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_opcion", MySqlDbType.Int32).Value = neg.Opcion;
            cmd.Parameters.Add("@p_criterio", MySqlDbType.VarChar).Value = neg.Criterio;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static int sp_tb_con_plan_contable_tipo_cuenta_gr(NEG_CON_PLAN_CONTABLE_TIPO_CUENTA neg)
        {
            MySqlConnection cn = new MySqlConnection(conexion.cadena);
            MySqlCommand cmd = new MySqlCommand("sp_tb_con_plan_contable_tipo_cuenta_gr", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_opc", MySqlDbType.Int32).Value = neg.Opc;
            cmd.Parameters.Add("@p_coTipo", MySqlDbType.Int32).Value = neg.CoTipo;
            cmd.Parameters.Add("@p_deTipo", MySqlDbType.VarChar).Value = neg.DeTipo;
            cmd.Parameters.Add("@p_naturaleza", MySqlDbType.VarChar).Value = neg.Naturaleza;
            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            return i;
        }
    }
}
