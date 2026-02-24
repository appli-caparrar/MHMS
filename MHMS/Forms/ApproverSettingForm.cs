using MHMS.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS.Forms
{
    public partial class ApproverSettingForm : Form
    {
        //Connection String
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public ApproverSettingForm()
        {
            InitializeComponent();

        }

        //====================================================================================================================>>>>>>>>>>>>

        private void GetSectionApproverSetting()
        {
            // Check connection status -> if close connection will open
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select Section Approver setting
            SqlCommand SelectSectionApproverSetting = new SqlCommand("SP_SelectUserApproverSetting", con);
            SelectSectionApproverSetting.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(SelectSectionApproverSetting);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ApproverSettingDataGrid.DataSource = dt;
            con.Close();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ApproverSettingForm_Load(object sender, EventArgs e)
        {
            GetSectionApproverSetting();

            //Change column header back color and fore color
            ApproverSettingDataGrid.EnableHeadersVisualStyles = false;
            ApproverSettingDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(86, 119, 157);
            ApproverSettingDataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //Align to middle and center the cell content of datagridview
            ApproverSettingDataGrid.Columns["Section"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ApproverSettingDataGrid.Columns["User Setting"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ApproverSettingDataGrid.Columns["Delete"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ApproverSettingDataGrid.Columns["Edit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            ApproverSettingDataGrid.Columns["ID"].Visible = false;
            ApproverSettingDataGrid.Columns["Type"].Visible = false;

            foreach (DataGridViewColumn column in ApproverSettingDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        // --> Change cell backcolor in datagrid
        private void ApproverSettingDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.Value != null)
            {
                e.CellStyle.BackColor = Color.FromArgb(65, 137, 218);
                e.CellStyle.ForeColor = Color.White;
            }
            else if (e.ColumnIndex == 2 && e.Value != null)
            {
                e.CellStyle.BackColor = Color.FromArgb(60, 179, 141); 
                e.CellStyle.ForeColor = Color.White;
            }
            else if (e.ColumnIndex == 3 && e.Value != null)
            {
                e.CellStyle.BackColor = Color.FromArgb(210, 52, 74);
                e.CellStyle.ForeColor = Color.White;
            }

          
            //Custom datagrid font
            ApproverSettingDataGrid.Columns["Section"].DefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            ApproverSettingDataGrid.Columns["User Setting"].DefaultCellStyle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            ApproverSettingDataGrid.Columns["Edit"].DefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            ApproverSettingDataGrid.Columns["Delete"].DefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);

            //Custom datagrid header cell backcolor
            //ApproverSettingDataGrid.Columns["Section"].HeaderCell.Style.BackColor = Color.FromArgb(21, 35, 53);
            //ApproverSettingDataGrid.Columns["Setting"].HeaderCell.Style.BackColor = Color.FromArgb(21, 35, 53);
            //ApproverSettingDataGrid.Columns["Status"].HeaderCell.Style.BackColor = Color.FromArgb(21, 35, 53);

            //Set specific width on datagrid column
            ApproverSettingDataGrid.Columns["User Setting"].Width = 150;
            ApproverSettingDataGrid.Columns["Delete"].Width = 150;
            ApproverSettingDataGrid.Columns["Edit"].Width = 150;

        }

        //====================================================================================================================>>>>>>>>>>>>

        // --> Function for adding section in database
        private void AddSection()
        {
            if (Section.Text == "")
            {
                MessageBox.Show("Please type the section!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (TypeDropdown.Text == "")
            {
                MessageBox.Show("Please select section type!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Check connection status -> if close connection will open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // -> SQL query to insert Section to Approver setting
                SqlCommand AddSectionToApproverSetting = new SqlCommand("SP_InsertSectionToApproverSetting", con);
                AddSectionToApproverSetting.CommandType = CommandType.StoredProcedure;
                AddSectionToApproverSetting.Parameters.AddWithValue("@Section", Section.Text);
                AddSectionToApproverSetting.Parameters.AddWithValue("@Setting", "⚙️");
                AddSectionToApproverSetting.Parameters.AddWithValue("@Edit", "✏️");
                AddSectionToApproverSetting.Parameters.AddWithValue("@Delete", "❌");
                AddSectionToApproverSetting.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                AddSectionToApproverSetting.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Section Save Successfuly!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetSectionApproverSetting();
                Section.Text = "";
            }

        } // --> End of Function

        //====================================================================================================================>>>>>>>>>>>>

        // Add or Save section
        private void SaveSectionButton_Click(object sender, EventArgs e)
        {
            AddSection();
        }

        //====================================================================================================================>>>>>>>>>>>>

        public static string userSection = "";
        public static string TypeValue;
        public static string ID;

        private void ApproverSettingDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (ApproverSettingDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "⚙️")
                {
                    if (Dashboard.sections.Replace("BIPH-", "") == "BPS" || Dashboard.AccountType == "ADMIN")
                    {
                        userSection = ApproverSettingDataGrid.Rows[e.RowIndex].Cells["Section"].Value.ToString();
                        UserSetting userSettings = new UserSetting();
                        userSettings.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("You are not authorized to access this setting!");
                    }
                }
                else if (ApproverSettingDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "✏️")
                {
                    ID = ApproverSettingDataGrid.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                    userSection = ApproverSettingDataGrid.Rows[e.RowIndex].Cells["Section"].Value.ToString();
                    TypeValue = ApproverSettingDataGrid.Rows[e.RowIndex].Cells["Type"].Value.ToString();

                    UpdateSection updateSection = new UpdateSection();
                    updateSection.ShowDialog();

                    GetSectionApproverSetting();

                }
                else if (ApproverSettingDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "❌")
                {
                    ID = ApproverSettingDataGrid.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                    userSection = ApproverSettingDataGrid.Rows[e.RowIndex].Cells["Section"].Value.ToString();
                   

                    DialogResult dialogResult = MessageBox.Show("Are you sure do you want delete " + userSection + " section?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        DeleteSection();

                        MessageBox.Show("User Section Successfuly Deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    GetSectionApproverSetting();
                }
            }
            catch
            { }

        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Search function
        private void SearchSection()
        {
            // Check connection status -> if close connection will open
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select User Account
            SqlCommand SearchSection = new SqlCommand("SP_SearchSection", con);
            SearchSection.CommandType = CommandType.StoredProcedure;
            SearchSection.Parameters.AddWithValue("@Section", SearchBox.Text);
            SqlDataAdapter da = new SqlDataAdapter(SearchSection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ApproverSettingDataGrid.DataSource = dt;
            con.Close();

            SearchBox.Clear();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchSection();
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    var uploadTemplate = new UploadTemplateForm();
        //    uploadTemplate.ShowDialog();
        //}

        //Upload Template
        //After clicking the button update template form will be shown
        private void UploadTemplateButton_Click(object sender, EventArgs e)
        {
            var uploadTemplate = new UploadTemplateForm();
            uploadTemplate.ShowDialog();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void DownloadTemplateButton_Click(object sender, EventArgs e)
        {
            Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\Others\NewUserTemplate.xlsx");
        }

        //====================================================================================================================>>>>>>>>>>>>
        private void LossFactorButton_Click(object sender, EventArgs e)
        {
            LossFactorForm lossFactor = new LossFactorForm();
            lossFactor.ShowDialog();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void DeleteSection()
        {
            // -> SQL query to delete Section to user setting
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand DeleteSection = new SqlCommand("SP_DeleteSectionInApproverSetting", con);
            DeleteSection.CommandType = CommandType.StoredProcedure;
            DeleteSection.Parameters.AddWithValue("@ID", ID);
            DeleteSection.ExecuteNonQuery();
            con.Close();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ApproverSettingDataGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (ApproverSettingDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "🗑")
            //{
            //    userSection = ApproverSettingDataGrid.Rows[e.RowIndex].Cells["Section"].Value.ToString();
            //    ID = ApproverSettingDataGrid.Rows[e.RowIndex].Cells["ID"].Value.ToString();

            //    DialogResult dialogResult = MessageBox.Show("Are you sure do you want delete " + userSection + " section?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //    if (dialogResult == DialogResult.Yes)
            //    {
            //        DeleteSection();

            //        MessageBox.Show("User Section Successfuly Deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }

            //    MessageBox.Show("OKAY NA BOSS GUMAGANA ANG DELETE CODE MO HAHA");
            //}

            //GetSectionApproverSetting();
        }


        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchSection();
        }

        //====================================================================================================================>>>>>>>>>>>>
    }
}
