using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPA_NEGOCIOS.ADMINISTRADOR;

namespace CAPA_DATOS.ADMINISTRADOR
{
    public static class DAT_ERP_ADM_USUARIOS
    {
        public static DataTable SP_ERP_ADM_MODULO_LISTAR(NEG_ERP_ADM_USUARIOS neg)
        {
            SqlConnection cn = new SqlConnection(conexion.cadena);
            SqlCommand cmd = new SqlCommand("tb_adm_usuarios_ls", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@opcion", SqlDbType.Int).Value = neg.Opcion;
            cmd.Parameters.Add("@criterio", SqlDbType.VarChar).Value = neg.Criterio;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
