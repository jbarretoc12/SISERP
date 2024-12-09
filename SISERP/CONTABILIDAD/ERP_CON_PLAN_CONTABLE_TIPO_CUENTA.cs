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
    public partial class ERP_CON_PLAN_CONTABLE_TIPO_CUENTA : DevComponents.DotNetBar.Office2007Form/*Form*/
    {
        public ERP_CON_PLAN_CONTABLE_TIPO_CUENTA()
        {
            InitializeComponent();
        }
        NEG_CON_PLAN_CONTABLE_TIPO_CUENTA neg=new NEG_CON_PLAN_CONTABLE_TIPO_CUENTA();
        Clases.ERP_FUNCIONES fun = new Clases.ERP_FUNCIONES();
        string headerText = "Sistema ERP";
        int opc;
        private void ERP_CON_PLAN_CONTABLE_Load(object sender, EventArgs e)
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
                System.Data.DataTable dt = DAT_CON_PLAN_CONTABLE_TIPO_CUENTA.sp_tb_con_plan_contable_tipo_cuenta_ls(neg);
                dgvDatos.Rows.Clear();
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    dgvDatos.Rows.Add();
                    dgvDatos.Rows[x].Cells["coTipo"].Value = dt.Rows[x]["coTipo"].ToString().Trim();
                    dgvDatos.Rows[x].Cells["deTipo"].Value = dt.Rows[x]["deTipo"].ToString().Trim();
                    dgvDatos.Rows[x].Cells["naturaleza"].Value = dt.Rows[x]["naturaleza"].ToString().Trim();
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
                txtcoTipo.Text = dgvDatos.CurrentRow.Cells["coTipo"].Value.ToString().Trim();
                txtdeTipo.Text = dgvDatos.CurrentRow.Cells["deTipo"].Value.ToString().Trim();
                txtnaturaleza.Text = dgvDatos.CurrentRow.Cells["naturaleza"].Value.ToString().Trim();
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
                txtcoTipo.Enabled = true;
                txtcoTipo.Select();
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
            if (String.IsNullOrEmpty(txtcoTipo.Text.Trim()))
            {
                MessageBoxEx.Show("* Ingresar código.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtcoTipo.Select();
                return;
            }
            if (String.IsNullOrEmpty(txtdeTipo.Text.Trim()))
            {
                MessageBoxEx.Show("* Ingresar la descripción.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdeTipo.Select();
                return;
            }
            if (String.IsNullOrEmpty(txtnaturaleza.Text.Trim()))
            {
                MessageBoxEx.Show("* Ingresar la naturaleza.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnaturaleza.Select();
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
                neg.CoTipo = txtcoTipo.Text.Trim();
                neg.DeTipo = txtdeTipo.Text.Trim();
                neg.Naturaleza = txtnaturaleza.Text.Trim();
                int i = DAT_CON_PLAN_CONTABLE_TIPO_CUENTA.sp_tb_con_plan_contable_tipo_cuenta_gr(neg);
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
                txtcoTipo.Enabled = false;
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
