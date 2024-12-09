using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS.CONTABILIDAD
{
    public class NEG_CON_PLAN_CONTABLE_TIPO_CUENTA
    {
        private int opcion;
        private string criterio;
        private int opc;
        private string coTipo;
        private string deTipo;
        private string naturaleza;

        public int Opcion { get => opcion; set => opcion = value; }
        public string Criterio { get => criterio; set => criterio = value; }
        public int Opc { get => opc; set => opc = value; }
        public string CoTipo { get => coTipo; set => coTipo = value; }
        public string DeTipo { get => deTipo; set => deTipo = value; }
        public string Naturaleza { get => naturaleza; set => naturaleza = value; }
    }
}
