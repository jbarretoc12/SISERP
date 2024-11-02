using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS.ADMINISTRADOR
{
    public class NEG_ADMINISTRADOR
    {
        private int opcion;
        private string criterio;
        private int opc;
        private string coEmp;
        private string razSocial;
        private string nomComercial;
        private string dirFiscal;
        private string telefono;
        private string paginaWeb;
        private string co_usua_crea;

        public int Opcion { get => opcion; set => opcion = value; }
        public string Criterio { get => criterio; set => criterio = value; }
        public int Opc { get => opc; set => opc = value; }
        public string CoEmp { get => coEmp; set => coEmp = value; }
        public string RazSocial { get => razSocial; set => razSocial = value; }
        public string NomComercial { get => nomComercial; set => nomComercial = value; }
        public string DirFiscal { get => dirFiscal; set => dirFiscal = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string PaginaWeb { get => paginaWeb; set => paginaWeb = value; }
        public string Co_usua_crea { get => co_usua_crea; set => co_usua_crea = value; }
    }
}
