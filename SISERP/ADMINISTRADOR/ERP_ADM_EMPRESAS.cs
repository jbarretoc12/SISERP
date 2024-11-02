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
        string headerText = "Rivera Diesel S.A.";
        int opc;
        private void ERP_ADM_EMPRESAS_Load(object sender, EventArgs e)
        {
            cargarForm();
        }

        private void cargarForm()
        {
            try
            {
                txtcoEmp.Enabled = true;
                txtcoEmp.Select();
                listar(1, "");
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
                System.Data.DataTable dt = DAT_ADMINISTRADOR.sp_tb_empresas_ls(neg);
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

        private void btnAgregarEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                #region VALIDACIONES
                if (String.IsNullOrEmpty(txtcoEmp.Text.Trim()))
                {
                    MessageBoxEx.Show("* Ingresar el RUC.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcoEmp.Select();
                    return;
                }

                if (String.IsNullOrEmpty(txtrazSocial.Text.Trim()))
                {
                    MessageBoxEx.Show("* Ingresar la razón social.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtrazSocial.Select();
                    return;
                }

                if (String.IsNullOrEmpty(txtnomComercial.Text.Trim()))
                {
                    MessageBoxEx.Show("* Ingresar el nombre comercial.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtnomComercial.Select();
                    return;
                }

                if (String.IsNullOrEmpty(txtdirFiscal.Text.Trim()))
                {
                    MessageBoxEx.Show("* Ingresar la dirección fiscal.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtdirFiscal.Select();
                    return;
                }

                if (String.IsNullOrEmpty(txttelefono.Text.Trim()))
                {
                    MessageBoxEx.Show("* Ingresar el teléfono o celular.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txttelefono.Select();
                    return;
                }

                if (String.IsNullOrEmpty(txtpaginaWeb.Text.Trim()))
                {
                    MessageBoxEx.Show("* Ingresar su página web.", headerText, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtpaginaWeb.Select();
                    return;
                }
                #endregion
                dgvDatos.Rows.Add();
                dgvDatos.Rows[dgvDatos.Rows.Count - 1].Cells["coEmp"].Value = txtcoEmp.Text.Trim();
                dgvDatos.Rows[dgvDatos.Rows.Count - 1].Cells["razSocial"].Value = txtrazSocial.Text.Trim();
                dgvDatos.Rows[dgvDatos.Rows.Count - 1].Cells["nomComercial"].Value = txtnomComercial.Text.Trim();
                dgvDatos.Rows[dgvDatos.Rows.Count - 1].Cells["dirFiscal"].Value = txtdirFiscal.Text.Trim();
                dgvDatos.Rows[dgvDatos.Rows.Count - 1].Cells["telefono"].Value = txttelefono.Text.Trim();
                dgvDatos.Rows[dgvDatos.Rows.Count - 1].Cells["paginaWeb"].Value = txtpaginaWeb.Text.Trim();
                dgvDatos.Rows[dgvDatos.Rows.Count - 1].Cells["estado"].Value = "A";
                dgvDatos.Rows[dgvDatos.Rows.Count - 1].Cells["st_registro"].Value = true;
                dgvDatos.Rows[dgvDatos.Rows.Count - 1].Cells["co_usua_crea"].Value = "DCONDORI";
                dgvDatos.Rows[dgvDatos.Rows.Count - 1].Cells["fe_usua_crea"].Value = DateTime.Now;
                dgvDatos.Rows[dgvDatos.Rows.Count - 1].Cells["co_usua_modi"].Value = "";
                dgvDatos.Rows[dgvDatos.Rows.Count - 1].Cells["fe_usua_modi"].Value = "";

                neg.Opc = 1;
                neg.CoEmp = txtcoEmp.Text.Trim();
                neg.RazSocial = txtcoEmp.Text.Trim();
                neg.NomComercial = txtnomComercial.Text.Trim();
                neg.DirFiscal = txtdirFiscal.Text.Trim();
                neg.Telefono = txttelefono.Text.Trim();
                neg.PaginaWeb = txtpaginaWeb.Text.Trim();
                neg.Co_usua_crea = "DCONDORI";
                int i = DAT_ADMINISTRADOR.sp_tb_empresas_gr(neg);

                limpiarControles();
                
                lblAlerta.Visible = true;
                lblAlerta.Text = "Grabado";
                lblAlerta.BackColor = Color.Green;
                lblAlerta.ForeColor = Color.White;

                
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void grabar()
        {
            try
            {
                for (int x = 0; x < dgvDatos.Rows.Count; x++)
                {
                    neg.Opc = opc;
                    neg.CoEmp = dgvDatos.Rows[x].Cells["coEmp"].Value.ToString().Trim();
                    neg.RazSocial = dgvDatos.Rows[x].Cells["razSocial"].Value.ToString().Trim();
                    neg.NomComercial = dgvDatos.Rows[x].Cells["nomComercial"].Value.ToString().Trim();
                    neg.DirFiscal = dgvDatos.Rows[x].Cells["dirFiscal"].Value.ToString().Trim();
                    neg.Telefono = dgvDatos.Rows[x].Cells["telefono"].Value.ToString().Trim();
                    neg.PaginaWeb = dgvDatos.Rows[x].Cells["paginaWeb"].Value.ToString().Trim();
                    neg.Co_usua_crea = "DCONDORI";
                    int i = DAT_ADMINISTRADOR.sp_tb_empresas_gr(neg);
                }
                limpiarControles();                
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }
        int rowIndex;
        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDatos.Columns[e.ColumnIndex].Name == "btnEditar")
                {
                    rowIndex = dgvDatos.CurrentRow.Index;
                    lblAlerta.Visible = true;
                    btnAgregarEmpresa.Enabled = false;
                    btnGrabarEmpresa.Enabled = true;
                    lblAlerta.Text = "Pendiente Grabar";
                    lblAlerta.BackColor = Color.Red;
                    lblAlerta.ForeColor = Color.White;

                    Clases.ERP_GLOBALES.NombreAccion = "EDITAR";
                    txtcoEmp.Text = dgvDatos.Rows[rowIndex].Cells["coEmp"].Value.ToString().Trim();
                    txtrazSocial.Text = dgvDatos.Rows[rowIndex].Cells["razSocial"].Value.ToString().Trim();
                    txtnomComercial.Text = dgvDatos.Rows[rowIndex].Cells["nomComercial"].Value.ToString().Trim();
                    txtdirFiscal.Text = dgvDatos.Rows[rowIndex].Cells["dirFiscal"].Value.ToString().Trim();
                    txttelefono.Text = dgvDatos.Rows[rowIndex].Cells["telefono"].Value.ToString().Trim();
                    txtpaginaWeb.Text = dgvDatos.Rows[rowIndex].Cells["paginaWeb"].Value.ToString().Trim();
                    txtcoEmp.Enabled = false;
                }

                if (dgvDatos.Columns[e.ColumnIndex].Name == "btnEliminar")
                {
                    if (MessageBoxEx.Show("¿Está seguro de eliminar esta empresa?","Rivera Diesel S.A.",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        neg.Opc = 3;
                        neg.CoEmp = dgvDatos.CurrentRow.Cells["coEmp"].Value.ToString().Trim();
                        neg.RazSocial = "";
                        neg.NomComercial = "";
                        neg.DirFiscal = "";
                        neg.Telefono = "";
                        neg.PaginaWeb = "";
                        neg.Co_usua_crea = "";
                        int i = DAT_ADMINISTRADOR.sp_tb_empresas_gr(neg);
                        cargarForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private async void btnTraerClienteSunat_Click(object sender, EventArgs e)
        {
            try
            {
                string ruc = txtcoEmp.Text.Trim();
                if (!string.IsNullOrEmpty(ruc))
                {
                    var datos = await ObtenerDatosDesdeSunatAsync(ruc);
                    if (datos != null)
                    {
                        txtrazSocial.Text = datos.RazonSocial;
                        txtdirFiscal.Text = datos.Direccion;
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron datos para el RUC proporcionado.");
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingresa un RUC válido.");
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private async Task<datosDelCliente> ObtenerDatosDesdeSunatAsync(string ruc)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Asegúrate de que la URL sea la correcta para acceder a SUNAT
                    string url = $"https://api.sunat.gob.pe/v1/consorcio/{ruc}";
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var datosJson = await response.Content.ReadAsStringAsync();
                        // Deserializa el JSON en un objeto de tipo DatosCliente
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<datosDelCliente>(datosJson);
                    }
                    else
                    {
                        // Maneja los errores de acuerdo a tu necesidad
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al obtener datos: {ex.Message}");
                    return null;
                }
            }
        }

        public class datosDelCliente
        {
            public string RazonSocial { get; set; }
            public string Direccion { get; set; }
            // Agrega otros campos según la respuesta de SUNAT
        }

        private void btnGrabarEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                //lblAlerta.Visible = false;
                //lblAlerta.Text = "Grabado";
                //lblAlerta.BackColor = Color.DarkGreen;
                //lblAlerta.ForeColor = Color.White;
                //grabar();
                if (Clases.ERP_GLOBALES.NombreAccion == "EDITAR")
                {
                    opc = 2;
                    dgvDatos.Rows[rowIndex].Cells["razSocial"].Value = txtrazSocial.Text.Trim();
                    dgvDatos.Rows[rowIndex].Cells["nomComercial"].Value = txtnomComercial.Text.Trim();
                    dgvDatos.Rows[rowIndex].Cells["dirFiscal"].Value = txtdirFiscal.Text.Trim();
                    dgvDatos.Rows[rowIndex].Cells["telefono"].Value = txttelefono.Text.Trim();
                    dgvDatos.Rows[rowIndex].Cells["paginaWeb"].Value = txtpaginaWeb.Text.Trim();
                    grabar();
                    txtcoEmp.Enabled = true;
                    btnAgregarEmpresa.Enabled = true;
                    btnGrabarEmpresa.Enabled = false;
                    lblAlerta.Text = "Grabado";
                    lblAlerta.BackColor = Color.Green;
                    lblAlerta.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void btnLimpiarControles_Click(object sender, EventArgs e)
        {
            limpiarControles();
            lblAlerta.Visible = false;
        }

        private void limpiarControles()
        {
            txtcoEmp.Enabled = true;
            txtcoEmp.Text = "";
            txtrazSocial.Text = "";
            txtnomComercial.Text = "";
            txtdirFiscal.Text = "";
            txttelefono.Text = "";
            txtpaginaWeb.Text = "";
            txtcoEmp.Select();    
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarControles();
            cargarForm();
            lblAlerta.Visible = false;

            btnAgregarEmpresa.Enabled = true;
            btnGrabarEmpresa.Enabled = false;

            lblAlerta.Text = "Grabado";
            lblAlerta.BackColor = Color.Green;
            lblAlerta.ForeColor = Color.White;
        }
    }
}
