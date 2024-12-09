using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS.CONTABILIDAD
{
    public class NEG_CON_PLAN_CONTABLE_SUB_CUENTAS
    {
        private int opcion;
        private string criterio;
        private int opc;
        private int coCuenta;
        private int coSubCuenta;
        private string noSubCuenta;

        public int Opcion { get => opcion; set => opcion = value; }
        public string Criterio { get => criterio; set => criterio = value; }
        public int Opc { get => opc; set => opc = value; }
        public int CoCuenta { get => coCuenta; set => coCuenta = value; }
        public int CoSubCuenta { get => coSubCuenta; set => coSubCuenta = value; }
        public string NoSubCuenta { get => noSubCuenta; set => noSubCuenta = value; }
    }
}
