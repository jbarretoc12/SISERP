using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS.CONTABILIDAD
{
    public class NEG_CON_PLAN_CONTABLE
    {
        private int opcion;
        private string criterio;
        private int opc;
        private int coCuenta;
        private string noCuenta;
        private string deCuenta;
        private int coTipo;
        private int nivel;

        public int Opcion { get => opcion; set => opcion = value; }
        public string Criterio { get => criterio; set => criterio = value; }
        public int Opc { get => opc; set => opc = value; }
        public int CoCuenta { get => coCuenta; set => coCuenta = value; }
        public string NoCuenta { get => noCuenta; set => noCuenta = value; }
        public string DeCuenta { get => deCuenta; set => deCuenta = value; }
        public int CoTipo { get => coTipo; set => coTipo = value; }
        public int Nivel { get => nivel; set => nivel = value; }
    }
}
