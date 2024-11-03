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
using CAPA_NEGOCIOS.ADMINISTRADOR;
using CAPA_DATOS.ADMINISTRADOR;
using System.Net.Http;
using System.Drawing.Drawing2D;

namespace SISERP.ADMINISTRADOR
{
    public partial class ERP_ADM_EMPRESAS : DevComponents.DotNetBar.Office2007Form/*Form*/
    {
        public ERP_ADM_EMPRESAS()
        {
            InitializeComponent();
        }
        NEG_ADMINISTRADOR neg=new NEG_ADMINISTRADOR();
        Clases.ERP_FUNCIONES fun=new Clases.ERP_FUNCIONES();
        string headerText = "Sistema ERP";
        int opc;
        private void ERP_ADM_EMPRESAS_Load(object sender, EventArgs e)
        {
            cargarForm();
        }

        private void cargarForm()
        {
            try
            {
                //fun.ERP_Habilitar_Botones(this, BTN_NUEVO, BTN_GRABAR, BTN_ELIMINAR, BTN_EDITAR, BTN_CANCELAR, BTN_LISTADO, BTN_AUDITORIA, true, Clases.Globales.Co_usuario, Clases.Globales.Co_sucu, Clases.Globales.Co_empresa, Clases.ERP_GLOBALES.formpermisos);
                fun.Limpiar_Controles(this);
                fun.Habilitar_Controles(this, false);
                gbxListado.Enabled = true;
                listar(1, "");
                superTabControl1.SelectedTabIndex = 1;                
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void listar(int opcion, string criterio)
        {
            try
            {
                neg.Opcion=opcion;
                neg.Criterio=criterio;
                System.Data.DataTable dt = DAT_ADMINISTRADOR.sp_tb_adm_empresas_ls(neg);
                dgvDatos.Rows.Clear();
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    dgvDatos.Rows.Add();
                    dgvDatos.Rows[x].Cells["coEmp"].Value = dt.Rows[x]["coEmp"].ToString().Trim();
                    dgvDatos.Rows[x].Cells["razSocial"].Value = dt.Rows[x]["razSocial"].ToString().Trim();
                    dgvDatos.Rows[x].Cells["nomComercial"].Value = dt.Rows[x]["nomComercial"].ToString().Trim();
                    dgvDatos.Rows[x].Cells["dirFiscal"].Value = dt.Rows[x]["dirFiscal"].ToString().Trim();
                    dgvDatos.Rows[x].Cells["telefono"].Value = dt.Rows[x]["telefono"].ToString().Trim();
                    dgvDatos.Rows[x].Cells["paginaWeb"].Value = dt.Rows[x]["paginaWeb"].ToString().Trim();
                    dgvDatos.Rows[x].Cells["estado"].Value = dt.Rows[x]["estado"].ToString().Trim();
                    dgvDatos.Rows[x].Cells["st_registro"].Value = Convert.ToBoolean(dt.Rows[x]["st_registro"]);
                    dgvDatos.Rows[x].Cells["co_usua_crea"].Value = dt.Rows[x]["co_usua_crea"].ToString().Trim();
                    dgvDatos.Rows[x].Cells["fe_usua_crea"].Value = dt.Rows[x]["fe_usua_crea"].ToString().Trim();
                    dgvDatos.Rows[x].Cells["co_usua_modi"].Value = dt.Rows[x]["co_usua_modi"].ToString().Trim();
                    dgvDatos.Rows[x].Cells["fe_usua_modi"].Value = dt.Rows[x]["fe_usua_modi"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void dgvDatos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                var grid = sender as DataGridView;
                var rowIdx = (e.RowIndex + 1).ToString();
                var centerFormat = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
                e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                filtrar();
            }
        }

        private void filtrar()
        {
            listar(1, txtFiltro.Text.Trim());
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            filtrar();
        }

        private void cargarDatos()
        {
            try
            {
                txtcoEmp.Text = dgvDatos.CurrentRow.Cells["coEmp"].Value.ToString().Trim();
                txtrazSocial.Text = dgvDatos.CurrentRow.Cells["razSocial"].Value.ToString().Trim();
                txtnomComercial.Text = dgvDatos.CurrentRow.Cells["nomComercial"].Value.ToString().Trim();
                txtdirFiscal.Text = dgvDatos.CurrentRow.Cells["dirFiscal"].Value.ToString().Trim();
                txttelefono.Text = dgvDatos.CurrentRow.Cells["telefono"].Value.ToString().Trim();
                txtpaginaWeb.Text = dgvDatos.CurrentRow.Cells["paginaWeb"].Value.ToString().Trim();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                //btnNuevo.Checked = true;
                //btnGuardar.Checked = false;
                //btnEditar.Checked = false;
                //btnEliminar.Checked = false;
                //btnCancelar.Checked = false;
                //btnAuditoria.Checked = false;

                opc = 1;
                //fun.ERP_Habilitar_Botones(this, BTN_NUEVO, BTN_GRABAR, BTN_ELIMINAR, BTN_EDITAR, BTN_CANCELAR, BTN_LISTADO, BTN_AUDITORIA, false, Clases.Globales.Co_usuario, Clases.Globales.Co_sucu, Clases.Globales.Co_empresa, Clases.ERP_GLOBALES.formpermisos);
                fun.Limpiar_Controles(this);
                fun.Habilitar_Controles(this, true);
                gbxListado.Enabled = false;
                txtcoEmp.Enabled = true;
                txtcoEmp.Select();
                superTabControl1.SelectedTabIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //btnNuevo.Checked = false;
            //btnGuardar.Checked = true;
            //btnEditar.Checked = false;
            //btnEliminar.Checked = false;
            //btnCancelar.Checked = false;
            //btnAuditoria.Checked = false;

            #region VALIDACIONES
            if (String.IsNullOrEmpty(txtcoEmp.Text.Trim()))
            {
                MessageBoxEx.Show("* Ingresar el ruc de la empresa.",headerText, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtcoEmp.Select();
                return;
            }
            if (String.IsNullOrEmpty(txtrazSocial.Text.Trim()))
            {
                MessageBoxEx.Show("* Ingresar la razón social de la empresa.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtrazSocial.Select();
                return;
            }
            if (String.IsNullOrEmpty(txtnomComercial.Text.Trim()))
            {
                MessageBoxEx.Show("* Ingresar el nombre comercial de la empresa.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnomComercial.Select();
                return;
            }
            if (String.IsNullOrEmpty(txtdirFiscal.Text.Trim()))
            {
                MessageBoxEx.Show("* Ingresar la direcciónfiscal de la empresa.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdirFiscal.Select();
                return;
            }
            if (String.IsNullOrEmpty(txttelefono.Text.Trim()))
            {
                MessageBoxEx.Show("* Ingresar el N° de teléfono/celular de la empresa.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txttelefono.Select();
                return;
            }
            if (String.IsNullOrEmpty(txtpaginaWeb.Text.Trim()))
            {
                MessageBoxEx.Show("* Ingresar la página web de la empresa.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtpaginaWeb.Select();
                return;
            }
            #endregion
            grabar();
        }

        private void grabar()
        {
            try
            {
                neg.Opc = opc;
                neg.CoEmp = txtcoEmp.Text.Trim();
                neg.RazSocial = txtrazSocial.Text.Trim();
                neg.NomComercial = txtnomComercial.Text.Trim();
                neg.DirFiscal = txtdirFiscal.Text.Trim();
                neg.Telefono = txttelefono.Text.Trim();
                neg.PaginaWeb = txtpaginaWeb.Text.Trim();
                neg.Co_usua_crea = "DCONDORI";
                int i = DAT_ADMINISTRADOR.sp_tb_adm_empresas_gr(neg);
                cargarForm();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                //btnNuevo.Checked = false;
                //btnGuardar.Checked = false;
                //btnEditar.Checked = true;
                //btnEliminar.Checked = false;
                //btnCancelar.Checked = false;
                //btnAuditoria.Checked = false;

                opc = 2;
                //fun.ERP_Habilitar_Botones(this, BTN_NUEVO, BTN_GRABAR, BTN_ELIMINAR, BTN_EDITAR, BTN_CANCELAR, BTN_LISTADO, BTN_AUDITORIA, false, Clases.Globales.Co_usuario, Clases.Globales.Co_sucu, Clases.Globales.Co_empresa, Clases.ERP_GLOBALES.formpermisos);
                fun.Habilitar_Controles(this, true);
                gbxListado.Enabled = false;
                txtcoEmp.Enabled = false;
                cargarDatos();
                superTabControl1.SelectedTabIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                //btnNuevo.Checked = false;
                //btnGuardar.Checked = false;
                //btnEditar.Checked = false;
                //btnEliminar.Checked = true;
                //btnCancelar.Checked = false;
                //btnAuditoria.Checked = false;

                cargarDatos();
                if (MessageBoxEx.Show("¿Está seguro de eliminar esta empresa?", headerText, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    opc = 3;
                    grabar();
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //btnNuevo.Checked = false;
            //btnGuardar.Checked = false;
            //btnEditar.Checked = false;
            //btnEliminar.Checked = false;
            //btnCancelar.Checked = false;
            //btnAuditoria.Checked = false;

            cargarForm();
        }

        private void btnAuditoria_Click(object sender, EventArgs e)
        {
            //btnNuevo.Checked = false;
            //btnGuardar.Checked = false;
            //btnEditar.Checked = false;
            //btnEliminar.Checked = false;
            //btnCancelar.Checked = false;
            //btnAuditoria.Checked = true;
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cargarDatos();
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cargarDatos();
            superTabControl1.SelectedTabIndex = 0;
        }
    }
}
