using CAPA_NEGOCIOS.CONTABILIDAD;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_DATOS.CONTABILIDAD
{
    public static class DAT_CON_PLAN_CONTABLE
    {
        public static DataTable sp_tb_con_plan_contable_ls(NEG_CON_PLAN_CONTABLE neg)
        {
            MySqlConnection cn = new MySqlConnection(conexion.cadena);
            MySqlCommand cmd = new MySqlCommand("sp_tb_con_plan_contable_ls", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_opcion", MySqlDbType.Int32).Value = neg.Opcion;
            cmd.Parameters.Add("@p_criterio", MySqlDbType.VarChar).Value = neg.Criterio;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static int sp_tb_con_plan_contable_gr(NEG_CON_PLAN_CONTABLE neg)
        {
            MySqlConnection cn = new MySqlConnection(conexion.cadena);
            MySqlCommand cmd = new MySqlCommand("sp_tb_con_plan_contable_gr", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_opc", MySqlDbType.Int32).Value = neg.Opc;
            cmd.Parameters.Add("@p_coCuenta", MySqlDbType.Int32).Value = neg.CoCuenta;
            cmd.Parameters.Add("@p_noCuenta", MySqlDbType.VarChar).Value = neg.NoCuenta;
            cmd.Parameters.Add("@p_deCuenta", MySqlDbType.VarChar).Value = neg.DeCuenta;
            cmd.Parameters.Add("@p_coTipo", MySqlDbType.Int32).Value = neg.CoTipo;
            cmd.Parameters.Add("@p_nivel", MySqlDbType.Int32).Value = neg.Nivel;
            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            return i;
        }
    }
}
