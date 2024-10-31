using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISERP.PRINCIPAL
{
    public partial class ERP_ACCESO : Form
    {
        public ERP_ACCESO()
        {
            InitializeComponent();
        }

        private void ERP_ACCESO_Load(object sender, EventArgs e)
        {
            cargarForm();
        }

        private void cargarForm()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }
    }
}
