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
using CAPA_DATOS.CONTABILIDAD;
using CAPA_NEGOCIOS.CONTABILIDAD;
using CAPA_DATOS.ADMINISTRADOR;
using DevComponents.DotNetBar.Controls;

namespace SISERP.CONTABILIDAD
{
    public partial class ERP_CON_PLAN_CONTABLE_SUB_CUENTAS : DevComponents.DotNetBar.Office2007Form/*Form*/
    {
        public ERP_CON_PLAN_CONTABLE_SUB_CUENTAS()
        {
            InitializeComponent();
        }
        NEG_CON_PLAN_CONTABLE_SUB_CUENTAS neg = new NEG_CON_PLAN_CONTABLE_SUB_CUENTAS();
        Clases.ERP_FUNCIONES fun = new Clases.ERP_FUNCIONES();
        string headerText = "Sistema ERP";
        int opc;
        private void ERP_CON_PLAN_CONTABLE_SUB_CUENTAS_Load(object sender, EventArgs e)
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
                neg.Opcion = opcion;
                neg.Criterio = criterio;
                System.Data.DataTable dt = DAT_CON_PLAN_CONTABLE_SUB_CUENTAS.sp_tb_con_plan_contable_sub_cuentas_ls(neg);
                dgvDatos.Rows.Clear();
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    dgvDatos.Rows.Add();
                    dgvDatos.Rows[x].Cells["coCuenta"].Value = dt.Rows[x]["coCuenta"].ToString().Trim();
                    dgvDatos.Rows[x].Cells["coSubCuenta"].Value = dt.Rows[x]["coSubCuenta"].ToString().Trim();
                    dgvDatos.Rows[x].Cells["noSubCuenta"].Value = dt.Rows[x]["noSubCuenta"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                buscarEnDataGridView(txtFiltro.Text.Trim());
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            buscarEnDataGridView(txtFiltro.Text.Trim());
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

        private void cargarDatos()
        {
            try
            {
                txtcoCuenta.Text = dgvDatos.CurrentRow.Cells["coCuenta"].Value.ToString().Trim();
                txtcoSubCuenta.Text = dgvDatos.CurrentRow.Cells["coSubCuenta"].Value.ToString().Trim();
                txtnoSubCuenta.Text = dgvDatos.CurrentRow.Cells["noSubCuenta"].Value.ToString().Trim();
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
                txtcoCuenta.Enabled = true;
                txtcoCuenta.Select();
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
            if (String.IsNullOrEmpty(txtcoCuenta.Text.Trim()))
            {
                MessageBoxEx.Show("* Ingresar código.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtcoCuenta.Select();
                return;
            }
            if (String.IsNullOrEmpty(txtcoSubCuenta.Text.Trim()))
            {
                MessageBoxEx.Show("* Ingresar la descripción.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtcoSubCuenta.Select();
                return;
            }
            if (String.IsNullOrEmpty(txtnoSubCuenta.Text.Trim()))
            {
                MessageBoxEx.Show("* Ingresar la naturaleza.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnoSubCuenta.Select();
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
                neg.CoCuenta = Convert.ToInt32(txtcoCuenta.Text.Trim());
                neg.CoSubCuenta = Convert.ToInt32(txtcoSubCuenta.Text.Trim());
                neg.NoSubCuenta = txtnoSubCuenta.Text.Trim();
                int i = DAT_CON_PLAN_CONTABLE_SUB_CUENTAS.sp_tb_con_plan_contable_sub_cuentas_gr(neg);
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
                txtcoCuenta.Enabled = false;
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

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cargarDatos();
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cargarDatos();
            superTabControl1.SelectedTabIndex = 0;
        }

        private void buscarEnDataGridView(string textoBusqueda)
        {
            textoBusqueda = textoBusqueda.Trim().ToLower(); // Normalizar texto de búsqueda

            foreach (DataGridViewRow fila in dgvDatos.Rows)
            {
                if (fila.IsNewRow) continue; // Ignorar la fila nueva

                bool coincide = false;

                foreach (DataGridViewCell celda in fila.Cells)
                {
                    if (celda.Value != null && celda.Value.ToString().ToLower().Contains(textoBusqueda))
                    {
                        coincide = true;
                        break;
                    }
                }
                fila.Visible = coincide; // Mostrar u ocultar según la coincidencia
            }
        }
    }
}
