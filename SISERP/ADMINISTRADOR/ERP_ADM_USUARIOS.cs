using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISERP.ADMINISTRADOR
{
    public partial class ERP_ADM_USUARIOS : DevComponents.DotNetBar.Office2007Form
    {
        public ERP_ADM_USUARIOS()
        {
            InitializeComponent();
        }
        Clases.ERP_FUNCIONES fun = new Clases.ERP_FUNCIONES();
        private void ERP_ADM_USUARIOS_Load(object sender, EventArgs e)
        {
            cargarForm();
        }
        private void cargarForm()
        {
            fun.Limpiar_Controles(this);
            //fun.Habilitar_Controles(this, false);
            GBX_LISTADO.Enabled = true;
        }
    }
}
