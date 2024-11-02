using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_DATOS
{
    public class conexion
    {
        public static string cadena
        {
            get
            {
                return "server=localhost;database=bd_siserp;Uid=root;pwd=;";
            }
        }
    }
}
