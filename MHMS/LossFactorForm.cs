using MHMS.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace MHMS
{
    public partial class LossFactorForm : Form
    {
        // Connection string
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        // SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public LossFactorForm()
        {
            InitializeComponent();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void LossFactorForm_Load(object sender, EventArgs e)
        {
            SectionDropdown.Text = "";

            LoadLossFactor();

            LoadSection();

            SectionDropdown.Text = "Select section";

            //Set the specific column in datagrid to readonly / not editable
            LossFactorDataGrid.Columns["Section"].ReadOnly = true;
            LossFactorDataGrid.Columns["Loss Factor"].ReadOnly = true;

            LossFactorDataGrid.ClearSelection(); // Clear Section

            // Add checkbox column in datagrid
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "Select";
            checkColumn.HeaderText = "Select All";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;
            checkColumn.FillWeight = 50; //if the datagridview is resized (on form resize) the checkbox won't take up too much; value is relative to the other columns' fill values
            LossFactorDataGrid.Columns.Add(checkColumn);
            checkColumn.DisplayIndex = 0;
            // <<----------

            //Hide ID column in datagrid
            LossFactorDataGrid.Columns["ID"].Visible = false;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void LoadLossFactor()
        {
            // Check connection status -> if close connection will open
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select Section Approver setting
            SqlCommand SelectLossFactor = new SqlCommand("SP_SelectLossFactor", con);
            SelectLossFactor.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(SelectLossFactor);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            LossFactorDataGrid.DataSource = dt;
            con.Close();
        }

        //====================================================================================================================>>>>>>>>>>>>

        //Load Section in combobox
        public void LoadSection()
        {
            // Check Connection status -> Open the connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select User Account
            SqlCommand LoadSection = new SqlCommand("SP_LoadSection", con);
            LoadSection.CommandType = CommandType.StoredProcedure;
            LoadSection.Parameters.AddWithValue("@Procedure", "SelectAllSectionsExceptBPS");
            SqlDataAdapter sda = new SqlDataAdapter(LoadSection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadSection.ExecuteNonQuery();
            con.Close();

            SectionDropdown.DataSource = ds.Tables[0];
            SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            //SectionDropdown.ValueMember = "Section";
        }// <---- end

        //====================================================================================================================>>>>>>>>>>>>

        private void LossFactorDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0)
            //{
            //    MessageBox.Show(LossFactorDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            //}
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void LossFactorDataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (LossFactorDataGrid.Columns[e.ColumnIndex].HeaderText == "Select")
            //{
              

            //    foreach (DataGridViewRow row in LossFactorDataGrid.Rows)
            //    {
            //        row.Cells["Select"].Value = row.Cells["Select"].Value == null ? false : !(bool)row.Cells["Select"].Value;
            //    }
            //}
        }

        //====================================================================================================================>>>>>>>>>>>>


        private void AddButton_Click(object sender, EventArgs e)
        {
            AddFactorLoss();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void AddFactorLoss()
        {
            if (SectionDropdown.Text == "Select section")
            {
                MessageBox.Show("Please select section!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (LossFactorDropdown.Text == "")
            {
                MessageBox.Show("Please select loss factor!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Check connection status -> if close connection will open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // -> SQL query to insert Section to Approver setting
                SqlCommand AddFactorLoss = new SqlCommand("SP_InsertFactorLoss", con);
                AddFactorLoss.CommandType = CommandType.StoredProcedure;
                AddFactorLoss.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                AddFactorLoss.Parameters.AddWithValue("@LossFactor", LossFactorDropdown.Text);
                AddFactorLoss.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Added successfully added!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadLossFactor();
                SectionDropdown.Text = "";
                LossFactorDropdown.Text = "";
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void DeleteButton_Click(object sender, EventArgs e)
        {
             

            List<DataGridViewRow> selectedRows = (from row in LossFactorDataGrid.Rows.Cast<DataGridViewRow>()
                                                  where Convert.ToBoolean(row.Cells["Select"].Value) == true
                                                  select row).ToList();

            if (MessageBox.Show(string.Format("Do you want to delete {0} item?", selectedRows.Count), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in selectedRows)
                {
                    // -> SQL query to delete factor loss
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand DeleteFactorLoss = new SqlCommand("SP_DeleteLossFactor", con);
                    DeleteFactorLoss.CommandType = CommandType.StoredProcedure;
                    DeleteFactorLoss.Parameters.AddWithValue("@ID", row.Cells["ID"].Value);
                    DeleteFactorLoss.ExecuteNonQuery();
                    con.Close();
                }

                MessageBox.Show("Deleted Successfully!", "DONE");

                LoadLossFactor();
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void SearchLossFactor()
        {
            // Check connection status -> if close connection will open
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> Search loss factor
            SqlCommand SearchLossFactor = new SqlCommand("SP_SearchLossFactor", con);
            SearchLossFactor.CommandType = CommandType.StoredProcedure;
            SearchLossFactor.Parameters.AddWithValue("@SearchValue", SearchBox.Text);
            SqlDataAdapter da = new SqlDataAdapter(SearchLossFactor);
            DataTable dt = new DataTable();
            da.Fill(dt);
            LossFactorDataGrid.DataSource = dt;
            con.Close();

            //Clear searchbox when hit enter
            SearchBox.Clear();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchLossFactor();
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            UpdateLossFactor();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void UpdateLossFactor()
        {
            List<DataGridViewRow> selectedRows = (from row in LossFactorDataGrid.Rows.Cast<DataGridViewRow>()
                                                  where Convert.ToBoolean(row.Cells["Select"].Value) == true
                                                  select row).ToList();
            if (MessageBox.Show(string.Format("Do you want to delete {0} rows?", selectedRows.Count), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in selectedRows)
                {
                    // -> SQL query to delete factor loss
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateLossFactor = new SqlCommand("SP_UpdateLossFactor", con);
                    UpdateLossFactor.CommandType = CommandType.StoredProcedure;
                    UpdateLossFactor.Parameters.AddWithValue("@ID", row.Cells["ID"].Value);
                    UpdateLossFactor.ExecuteNonQuery();
                    con.Close();
                }

                MessageBox.Show("Deleted Successfully!", "DONE");

                LoadLossFactor();
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectAllCheckBox.Checked == true)
            {
                foreach (DataGridViewRow row in LossFactorDataGrid.Rows)
                {
                    row.Cells["Select"].Value = true;
                }
            }
            else if (SelectAllCheckBox.Checked == false)
            {
                foreach (DataGridViewRow row in LossFactorDataGrid.Rows)
                {
                    row.Cells["Select"].Value = false;
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void LossFactorDataGrid_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LossFactorDataGrid.Columns[e.ColumnIndex].HeaderText == "Select")
            {
                foreach (DataGridViewRow row in LossFactorDataGrid.Rows)
                {
                    if (row.Cells["Select"].Value == null)
                    {
                        row.Cells["Select"].Value = false;
                    }
                    else if (row.Cells["Select"].Value != null)
                    {
                        row.Cells["Select"].Value = true;
                    }
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void LossFactorDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in LossFactorDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void SectionDropdown_DropDown(object sender, EventArgs e)
        {
            
        }

        private void SectionDropdown_DropDownClosed(object sender, EventArgs e)
        {
            //SectionDropdown.Text = "Select section";
        }

        public static string section = "";
        private void AddLossFactorBtn_Click(object sender, EventArgs e)
        {
            section = SectionDropdown.Text;

            if (SectionDropdown.Text == "Select section")
            {
                MessageBox.Show("Please select section.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                SectionDropdown.Select();
            }
            else
            {
                AddLeadTime AddLossFactorAndLeadTime = new AddLeadTime();
                AddLossFactorAndLeadTime.Show();

             
            }
        }

        

        //====================================================================================================================>>>>>>>>>>>>
    }
}
