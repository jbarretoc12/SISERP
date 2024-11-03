using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPA_NEGOCIOS.ADMINISTRADOR;
using MySql.Data.MySqlClient;

namespace CAPA_DATOS.ADMINISTRADOR
{
    public static class DAT_ADMINISTRADOR
    {
        public static DataTable sp_tb_adm_empresas_ls(NEG_ADMINISTRADOR neg)
        {
            MySqlConnection cn = new MySqlConnection(conexion.cadena);
            MySqlCommand cmd = new MySqlCommand("sp_tb_adm_empresas_ls", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_opcion", MySqlDbType.Int32).Value = neg.Opcion;
            cmd.Parameters.Add("@p_criterio", MySqlDbType.VarChar).Value = neg.Criterio;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static int sp_tb_adm_empresas_gr(NEG_ADMINISTRADOR neg)
        {
            MySqlConnection cn = new MySqlConnection(conexion.cadena);
            MySqlCommand cmd = new MySqlCommand("sp_tb_adm_empresas_gr", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_opc", MySqlDbType.Int32).Value = neg.Opc;
            cmd.Parameters.Add("@p_coEmp", MySqlDbType.VarChar).Value = neg.CoEmp;
            cmd.Parameters.Add("@p_razSocial", MySqlDbType.VarChar).Value = neg.RazSocial;
            cmd.Parameters.Add("@p_nomComercial", MySqlDbType.VarChar).Value = neg.NomComercial;
            cmd.Parameters.Add("@p_dirFiscal", MySqlDbType.VarChar).Value = neg.DirFiscal;
            cmd.Parameters.Add("@p_telefono", MySqlDbType.VarChar).Value = neg.Telefono;
            cmd.Parameters.Add("@p_paginaWeb", MySqlDbType.VarChar).Value = neg.PaginaWeb;
            cmd.Parameters.Add("@p_co_usua_crea", MySqlDbType.VarChar).Value = neg.Co_usua_crea;
            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            return i;
        }
    }
}
