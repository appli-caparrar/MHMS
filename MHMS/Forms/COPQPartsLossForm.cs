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
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace MHMS.Forms
{
    public partial class COPQPartsLossForm : Form
    {
        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public COPQPartsLossForm()
        {
            InitializeComponent();

            
        }

        //===================================================================================================================================

        private void COPQPartsLossForm_Load(object sender, EventArgs e)
        {
            //Change column header back color and fore color
            PartsLossDataGridView.EnableHeadersVisualStyles = false;
            //PartsLossDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(72, 141, 218);
            PartsLossDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(86, 119, 157);
            PartsLossDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //Hide Row header in datagrid
            PartsLossDataGridView.RowHeadersVisible = false;

            ////Set column size to filled the full width of datagridview
            //PartsLossDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            //disable sorting a column in datagrid
            foreach (DataGridViewColumn column in PartsLossDataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            DateFrom(); // set the date always to the first day of the month

            DateTo(); // set date to current date

            LoadSection(); // Load section list from db to combobox

            if (LoginForm.UserSection == "BPS")
            {
                SectionDropdown.Text = "";
            }
            else
            {
                SectionDropdown.Text = LoginForm.UserSection;
                SectionDropdown.Enabled = false;
            }
            
            //LoadPartLossData();

            if (Dashboard.SectionText == "BIPH-Tape Cassette")
            {
                DefectButton.Visible = true;
                PartsLossButton.Visible = true;
                ShowTopDefectButton.Visible = true;
            }
            else
            {
                DefectButton.Visible = false;
                PartsLossButton.Visible = false;
                ViewGraphButton.Dock = DockStyle.Fill;
                ShowTopDefectButton.Visible = false;
            }

            //if (Dashboard.SectionText == "BIPH-Tape Cassette")
            //{
            //    ShowTopDefectButton.Visible = true;
            //}
            //else
            //{
            //    ShowTopDefectButton.Visible = false;
            //}

        } // <---- end

        //===================================================================================================================================

        private void DisableSectionDropdown()
        {
            SectionDropdown.Enabled = false;
            SectionDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");
        }

        //===================================================================================================================================

        private void LoadPartLossData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (Dashboard.SectionText == "BIPH-BPS")
            {
                SelectPartsLossData(); // Load Parts loss data based on selected section -> this is for admin section user

                SectionDropdown.Text = LoginForm.UserSection;
            }
            else if (Dashboard.SectionText == "BIPH-Ink Cartridge")
            {
                //Disable Section Dropdown
                DisableSectionDropdown();

                // -> SQL query to select ink cartridge parts loss data
                SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgePartsLossData");
                SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

                GetTotalAdjustedAmount();
                FormatHeaderText();

            }
            else if (Dashboard.SectionText == "BIPH-Tape Cassette")
            {
                //Disable Section Dropdown
                DisableSectionDropdown();

                // -> SQL query to select tape cassette parts loss data
                SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectTapeCassettePartsLossData");
                SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

                GetTotalAdjustedAmount();
                FormatHeaderText();

            }
            else if (Dashboard.SectionText == "BIPH-Ink Head")
            {
                //Disable Section Dropdown
                DisableSectionDropdown();

                // -> SQL query to select tape cassette parts loss data
                SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectInkHeadPartsLossData");
                SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

                GetTotalAdjustedAmount();
                FormatHeaderText();

            }
            else if (Dashboard.SectionText == "BIPH-Molding Production")
            {
                //Disable Section Dropdown
                DisableSectionDropdown();

                // -> SQL query to select tape cassette parts loss data
                SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectMoldingPartsLossData");
                SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

                GetTotalAdjustedAmount();
                FormatHeaderText();

            }
            else if (Dashboard.SectionText == "BIPH-PCBA")
            {
                //Disable Section Dropdown
                DisableSectionDropdown();

                // -> SQL query to select tape cassette parts loss data
                SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectPCBAPartsLossData");
                SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

                GetTotalAdjustedAmount();
                FormatHeaderText();

            }
            else if (Dashboard.SectionText == "BIPH-Printer")
            {
                //Disable Section Dropdown
                DisableSectionDropdown();

                // -> SQL query to select tape cassette parts loss data
                SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectPrinterPartsLossData");
                SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

                GetTotalAdjustedAmount();
                FormatHeaderText();
            }
            else if (Dashboard.SectionText == "BIPH-P-Touch")
            {
                //Disable Section Dropdown
                DisableSectionDropdown();

                // -> SQL query to select tape cassette parts loss data
                SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectPTouchPartsLossData");
                SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

                GetTotalAdjustedAmount();
                FormatHeaderText();

            }

        }// <---- end

        //===================================================================================================================================

        private void LoadDefectData()
        {
            if (Dashboard.SectionText == "BIPH-Tape Cassette")
            {
                //Disable Section Dropdown
                DisableSectionDropdown();

                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (ShowEntriesDropdown.Text == "All")
                {
                    // -> SQL query to select Section Approver setting
                    SqlCommand SelectDefectData = new SqlCommand("SP_LoadDefectData", con);
                    SelectDefectData.CommandType = CommandType.StoredProcedure;
                    SelectDefectData.Parameters.AddWithValue("@Procedure", "SelectAll");
                    SelectDefectData.Parameters.AddWithValue("@Entries", "");
                    SqlDataAdapter sda = new SqlDataAdapter(SelectDefectData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();
                }
                else
                {
                    // -> SQL query to select Section Approver setting
                    SqlCommand SelectDefectData = new SqlCommand("SP_LoadDefectData", con);
                    SelectDefectData.CommandType = CommandType.StoredProcedure;
                    SelectDefectData.Parameters.AddWithValue("@Procedure", "SelectEntries");
                    SelectDefectData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectDefectData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();
                }
               
                //Hide column ID in datagridview
                PartsLossDataGridView.Columns["ID"].Visible = false;

            }

        }

        //===================================================================================================================================

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
            LoadSection.Parameters.AddWithValue("@Procedure", "SelectAllProdSections");
            SqlDataAdapter sda = new SqlDataAdapter(LoadSection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadSection.ExecuteNonQuery();
            con.Close();

            SectionDropdown.DataSource = ds.Tables[0];
            SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            SectionDropdown.ValueMember = "Section";
        }// <---- end

        //===================================================================================================================================

        // ---> Set the datetime picker value to first day of the current month
        private void DateFrom()
        {
            DateTime now = DateTime.Now;
            FromDateTimePicker.Value = new DateTime(now.Year, now.Month, 1);
        }// <---- end

        //===============================================================================================================================

        private void DateTo()
        {
            DateTime datenow = DateTime.Now;
            ToDateTimePicker.Value = datenow;
        }// <---- end

        //===================================================================================================================================

        private void ViewGraph_Click(object sender, EventArgs e)
        {
          
            OpenGraphForm openGraph = new OpenGraphForm();
            openGraph.ShowDialog();

        }// <---- end

        //===================================================================================================================================

        private void UpdateDataButton_Click(object sender, EventArgs e)
        {

            UpdatePartsLossData2 UpdateData = new UpdatePartsLossData2();
            UpdateData.ShowDialog();

        } 

        //===================================================================================================================================

        bool DefectBtnClicked;
        bool PartLossBtnClicked;
        private void DefectButton_Click(object sender, EventArgs e)
        {
            DefectBtnClicked = true;
            PartLossBtnClicked = false;

            DefectButton.BackColor = Color.FromArgb(69, 185, 175);
            DefectButton.ForeColor = Color.FromArgb(21, 35, 53);

            PartsLossButton.BackColor = Color.FromArgb(113, 147, 144);
            PartsLossButton.ForeColor = Color.White;

            //Set column size based on content value
            PartsLossDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            //Set section dropdown text to tape cassette
            SectionDropdown.Text = "Tape Cassette";

            //Load all defect data to datagrid
            LoadDefectData();

        } // <---- end

        //===================================================================================================================================

        private void PartsLossButton_Click(object sender, EventArgs e)
        {
            DefectBtnClicked = false;
            PartLossBtnClicked = true;

            PartsLossButton.BackColor = Color.FromArgb(69, 185, 175);
            PartsLossButton.ForeColor = Color.FromArgb(21, 35, 53);

            DefectButton.BackColor = Color.FromArgb(113, 147, 144);
            DefectButton.ForeColor = Color.White;

            //Set section dropdown text to tape cassette
            SectionDropdown.Text = "Tape Cassette";

            SelectPartsLossDataBaseOnDropdownEntries();
        } // <---- end

        //===================================================================================================================================

        private void FormatHeaderText()
        {
            PartsLossDataGridView.Columns["OrderNo"].HeaderText = "Order No";
            PartsLossDataGridView.Columns["MaterialCode"].HeaderText = "Material Code";
            PartsLossDataGridView.Columns["MaterialText"].HeaderText = "Material Text";
            PartsLossDataGridView.Columns["Quantity"].HeaderText = "Quantity";
            PartsLossDataGridView.Columns["Amount"].HeaderText = "Amount";
            PartsLossDataGridView.Columns["MoveReason"].HeaderText = "Move Reason";
            PartsLossDataGridView.Columns["CreateDate"].HeaderText = "Create Date";
            PartsLossDataGridView.Columns["DetailsReason"].HeaderText = "Details Reason";
            PartsLossDataGridView.Columns["ConfirmResult"].HeaderText = "Confirm Result";
            PartsLossDataGridView.Columns["Remarks"].HeaderText = "Remarks";
            PartsLossDataGridView.Columns["AdjustedAmount"].HeaderText = "Adjusted Amount";
            PartsLossDataGridView.Columns["UploadDate"].HeaderText = "Upload Date";
        }

        private void SelectPartsLossData()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (Dashboard.SectionText == "BIPH-BPS")
            {
                if (SectionDropdown.Text == "Ink Cartridge")
                {
                    // -> SQL query to select ink cartridge parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgePartsLossData"); 
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();

                }
                else if (SectionDropdown.Text == "Tape Cassette")
                {
                    // -> SQL query to select tape cassette parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectTapeCassettePartsLossData");
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Ink Head")
                {
                    // -> SQL query to select ink head parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectInkHeadPartsLossData");
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Molding Production")
                {
                    // -> SQL query to select molding parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectMoldingPartsLossData");
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();
                }
                else if (SectionDropdown.Text == "PCBA")
                {
                    // -> SQL query to select PCBA parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectPCBAPartsLossData");
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Printer")
                {
                    // -> SQL query to select Printer parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectPrinterPartsLossData");
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();
                }
                else if (SectionDropdown.Text == "BPS")
                {
                    // -> SQL query to select Production Engineering parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectBPSPartsLossData");
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();
                }
                else if (SectionDropdown.Text == "P-Touch")
                {
                    // -> SQL query to select P-touch parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectPTouchPartsLossData");
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();
                }
            }
            else if (Dashboard.SectionText == "BIPH-BPS")
            {
                if (SectionDropdown.Text == "Ink Cartridge")
                {
                    // -> SQL query to select ink cartridge parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgePartsLossData");
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Tape Cassette")
                {
                    // -> SQL query to select tape cassette parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectTapeCassettePartsLossData");
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Ink Head")
                {
                    // -> SQL query to select ink head parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectInkHeadPartsLossData");
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Molding Production")
                {
                    // -> SQL query to select molding parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectMoldingPartsLossData");
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();
                }
                else if (SectionDropdown.Text == "PCBA")
                {
                    // -> SQL query to select PCBA parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectPCBAPartsLossData");
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Printer")
                {
                    // -> SQL query to select Printer parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectPrinterPartsLossData");
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();
                }
                else if (SectionDropdown.Text == "BPS")
                {
                    // -> SQL query to select Production Engineering parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectBPSPartsLossData");
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();
                }
                else if (SectionDropdown.Text == "P-Touch")
                {
                    // -> SQL query to select P-touch parts loss data
                    SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                    SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectPTouchPartsLossData");
                    SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                    FormatHeaderText();
                }
            }
            else if (Dashboard.SectionText == "BIPH-Tape Cassette")
            {
                // -> SQL query to select tape cassette parts loss data
                SqlCommand SelectPartsLossData = new SqlCommand("SP_SelectPartsLossData", con);
                SelectPartsLossData.CommandType = CommandType.StoredProcedure;
                SelectPartsLossData.Parameters.AddWithValue("@Procedure", "SelectTapeCassettePartsLossData");
                SelectPartsLossData.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

                GetTotalAdjustedAmount();
                FormatHeaderText();
            }
        }

        //===================================================================================================================================

        private void SectionDropdown_TextChanged(object sender, EventArgs e)
        {
            //SelectPartsLossData(); // load data from db
        }

        //===================================================================================================================================

        private void FilterDataByDate()
        {

            if (SectionDropdown.Text == "")
            {
                MessageBox.Show("Please Select section");
            }
            else if (SectionDropdown.Text == "Ink Cartridge")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
           
                // -> SQL query to select Ink cartridge gmms data by date selected 
                SqlCommand SelectPartsLossDataByDate = new SqlCommand("SP_SelectPartsLossDataByDate", con);
                SelectPartsLossDataByDate.CommandType = CommandType.StoredProcedure;
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Procedure", "SelectInkCartridgePartsLossDataByDate");
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossDataByDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();
              
            }
            else if (SectionDropdown.Text == "Ink Head")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // -> SQL query to select Ink cartridge gmms data by date selected 
                SqlCommand SelectPartsLossDataByDate = new SqlCommand("SP_SelectPartsLossDataByDate", con);
                SelectPartsLossDataByDate.CommandType = CommandType.StoredProcedure;
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Procedure", "SelectInkHeadPartsLossDataByDate");
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossDataByDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

            }
            else if (SectionDropdown.Text == "Molding Production")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // -> SQL query to select Ink cartridge gmms data by date selected 
                SqlCommand SelectPartsLossDataByDate = new SqlCommand("SP_SelectPartsLossDataByDate", con);
                SelectPartsLossDataByDate.CommandType = CommandType.StoredProcedure;
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Procedure", "SelectMoldingPartsLossDataByDate");
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossDataByDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

            }
            else if (SectionDropdown.Text == "PCBA")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // -> SQL query to select Ink cartridge gmms data by date selected 
                SqlCommand SelectPartsLossDataByDate = new SqlCommand("SP_SelectPartsLossDataByDate", con);
                SelectPartsLossDataByDate.CommandType = CommandType.StoredProcedure;
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Procedure", "SelectPCBAPartsLossDataByDate");
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossDataByDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

            }
            else if (SectionDropdown.Text == "Printer")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // -> SQL query to select Ink cartridge gmms data by date selected 
                SqlCommand SelectPartsLossDataByDate = new SqlCommand("SP_SelectPartsLossDataByDate", con);
                SelectPartsLossDataByDate.CommandType = CommandType.StoredProcedure;
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Procedure", "SelectPrinterPartsLossDataByDate");
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossDataByDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

            }
            else if (SectionDropdown.Text == "BPS")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // -> SQL query to select Ink cartridge gmms data by date selected 
                SqlCommand SelectPartsLossDataByDate = new SqlCommand("SP_SelectPartsLossDataByDate", con);
                SelectPartsLossDataByDate.CommandType = CommandType.StoredProcedure;
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Procedure", "SelectBPSPartsLossDataByDate");
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossDataByDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

            }
            else if (SectionDropdown.Text == "P-Touch")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // -> SQL query to select Ink cartridge gmms data by date selected 
                SqlCommand SelectPartsLossDataByDate = new SqlCommand("SP_SelectPartsLossDataByDate", con);
                SelectPartsLossDataByDate.CommandType = CommandType.StoredProcedure;
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Procedure", "SelectPTouchPartsLossDataByDate");
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossDataByDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

            }
            else if (SectionDropdown.Text == "Tape Cassette")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // -> SQL query to select Ink cartridge gmms data by date selected 
                SqlCommand SelectPartsLossDataByDate = new SqlCommand("SP_SelectPartsLossDataByDate", con);
                SelectPartsLossDataByDate.CommandType = CommandType.StoredProcedure;
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Procedure", "SelectTapeCassettePartsLossDataByDate");
                SelectPartsLossDataByDate.Parameters.AddWithValue("@Entries", ShowEntriesDropdown.Text);
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                SelectPartsLossDataByDate.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossDataByDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

            }
        }

        //==============================================================================================================================

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            if (SectionDropdown.Text == "")
            {
                MessageBox.Show("Please Select section.", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SectionDropdown.Focus();
            }
            else
            {
                //FilterDataByDate();.
                SelectPartsLossDataBaseOnDropdownEntries();
                GetTotalAdjustedAmount();
            }
        }

        //===================================================================================================================================

        private void LoadDataButton_Click(object sender, EventArgs e)
        {
            //LoadPartLossData();
        }

        //==================================================================================================================>>>>>>

        private void GetTotalAdjustedAmountBySection()
        {
            if (SectionDropdown.Text == "Ink Cartridge")
            {
                // -> SQL query to select Total Adjusted Amount
                SqlCommand InkCartridgeTotalAdjustedAmount = new SqlCommand("SP_SelectTotalAdjustedAmount", con);
                InkCartridgeTotalAdjustedAmount.CommandType = CommandType.StoredProcedure;
                InkCartridgeTotalAdjustedAmount.Parameters.AddWithValue("@Procedure", "InkCartridgeTotalAdjustedAmount");
                InkCartridgeTotalAdjustedAmount.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                InkCartridgeTotalAdjustedAmount.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter da = new SqlDataAdapter(InkCartridgeTotalAdjustedAmount);
                DataTable dt = new DataTable();
                da.Fill(dt);
               

                if (dt.Rows.Count > 0)
                {
                    SqlDataReader reader = InkCartridgeTotalAdjustedAmount.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["TotalAmount"].ToString() == "" || reader["TotalAmount"].ToString() == null)
                        {
                            //walang gagawin
                        }
                        else
                        {
                            TotalAdjAmount = Convert.ToDecimal(reader["TotalAmount"].ToString());
                        }
                    }
                }

                con.Close();

                TotalAdjustedAmount.Text = Math.Round(TotalAdjAmount, 4, MidpointRounding.ToEven).ToString();
            }
            else if (SectionDropdown.Text == "Ink Head")
            {
                // -> SQL query to select Total Adjusted Amount
                SqlCommand InkHeadTotalAdjustedAmount = new SqlCommand("SP_SelectTotalAdjustedAmount", con);
                InkHeadTotalAdjustedAmount.CommandType = CommandType.StoredProcedure;
                InkHeadTotalAdjustedAmount.Parameters.AddWithValue("@Procedure", "InkHeadTotalAdjustedAmount");
                InkHeadTotalAdjustedAmount.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                InkHeadTotalAdjustedAmount.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter da = new SqlDataAdapter(InkHeadTotalAdjustedAmount);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    SqlDataReader reader = InkHeadTotalAdjustedAmount.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["TotalAmount"].ToString() == "" || reader["TotalAmount"].ToString() == null)
                        {
                            //walang gagawin
                        }
                        else
                        {
                            TotalAdjAmount = Convert.ToDecimal(reader["TotalAmount"].ToString());
                        }
                    }
                }

                con.Close();

                TotalAdjustedAmount.Text = Math.Round(TotalAdjAmount, 4, MidpointRounding.ToEven).ToString();
            }
            else if (SectionDropdown.Text == "Molding Production")
            {
                //// -> SQL query to select Total Adjusted Amount
                //SqlCommand MoldingTotalAdjustedAmount = new SqlCommand("SP_SelectTotalAdjustedAmount", con);
                //MoldingTotalAdjustedAmount.CommandType = CommandType.StoredProcedure;
                //MoldingTotalAdjustedAmount.Parameters.AddWithValue("@Procedure", "MoldingTotalAdjustedAmount");
                //MoldingTotalAdjustedAmount.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                //MoldingTotalAdjustedAmount.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                //SqlDataAdapter da = new SqlDataAdapter(MoldingTotalAdjustedAmount);
                //DataTable dt = new DataTable();
                //da.Fill(dt);

                //if (dt.Rows.Count > 0)
                //{
                //    SqlDataReader reader = MoldingTotalAdjustedAmount.ExecuteReader();
                //    if (reader.Read())
                //    {
                //        if (reader["TotalAmount"].ToString() == "" || reader["TotalAmount"].ToString() == null)
                //        {
                //            //no action
                //        }
                //        else
                //        {
                //            TotalAdjAmount = Convert.ToDecimal(reader["TotalAmount"].ToString());
                //        }
                //    }
                //}
                //con.Close();

                //TotalAdjustedAmount.Text = Math.Round(TotalAdjAmount, 4, MidpointRounding.ToEven).ToString();
            }
            else if (SectionDropdown.Text == "PCBA")
            {
                // -> SQL query to select Total Adjusted Amount
                SqlCommand PCBATotalAdjustedAmount = new SqlCommand("SP_SelectTotalAdjustedAmount", con);
                PCBATotalAdjustedAmount.CommandType = CommandType.StoredProcedure;
                PCBATotalAdjustedAmount.Parameters.AddWithValue("@Procedure", "PCBATotalAdjustedAmount");
                PCBATotalAdjustedAmount.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                PCBATotalAdjustedAmount.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter da = new SqlDataAdapter(PCBATotalAdjustedAmount);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    SqlDataReader reader = PCBATotalAdjustedAmount.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["TotalAmount"].ToString() == "" || reader["TotalAmount"].ToString() == null)
                        {
                            //walang gagawin
                        }
                        else
                        {
                            TotalAdjAmount = Convert.ToDecimal(reader["TotalAmount"].ToString());
                        }
                    }
                }
                con.Close();

                TotalAdjustedAmount.Text = Math.Round(TotalAdjAmount, 4, MidpointRounding.ToEven).ToString();
            }
            else if (SectionDropdown.Text == "Printer")
            {
                // -> SQL query to select Total Adjusted Amount
                SqlCommand PrinterTotalAdjustedAmount = new SqlCommand("SP_SelectTotalAdjustedAmount", con);
                PrinterTotalAdjustedAmount.CommandType = CommandType.StoredProcedure;
                PrinterTotalAdjustedAmount.Parameters.AddWithValue("@Procedure", "PrinterTotalAdjustedAmount");
                PrinterTotalAdjustedAmount.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                PrinterTotalAdjustedAmount.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter da = new SqlDataAdapter(PrinterTotalAdjustedAmount);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    SqlDataReader reader = PrinterTotalAdjustedAmount.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["TotalAmount"].ToString() == "" || reader["TotalAmount"].ToString() == null)
                        {
                            //no action
                        }
                        else
                        {
                            TotalAdjAmount = Convert.ToDecimal(reader["TotalAmount"].ToString());
                        }
                    }
                }
                con.Close();

                TotalAdjustedAmount.Text = Math.Round(TotalAdjAmount, 4, MidpointRounding.ToEven).ToString();
            }
            else if (SectionDropdown.Text == "BPS")
            {
                // -> SQL query to select Total Adjusted Amount
                SqlCommand BPSTotalAdjustedAmount = new SqlCommand("SP_SelectTotalAdjustedAmount", con);
                BPSTotalAdjustedAmount.CommandType = CommandType.StoredProcedure;
                BPSTotalAdjustedAmount.Parameters.AddWithValue("@Procedure", "BPSTotalAdjustedAmount");
                BPSTotalAdjustedAmount.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                BPSTotalAdjustedAmount.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter da = new SqlDataAdapter(BPSTotalAdjustedAmount);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    SqlDataReader reader = BPSTotalAdjustedAmount.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["TotalAmount"].ToString() == "" || reader["TotalAmount"].ToString() == null)
                        {
                            //walang gagawin
                        }
                        else
                        {
                            TotalAdjAmount = Convert.ToDecimal(reader["TotalAmount"].ToString());
                        }
                    }
                }
                con.Close();

                TotalAdjustedAmount.Text = Math.Round(TotalAdjAmount, 4, MidpointRounding.ToEven).ToString();
            }
            else if (SectionDropdown.Text == "P-Touch")
            {
                // -> SQL query to select Total Adjusted Amount
                SqlCommand PTouchTotalAdjustedAmount = new SqlCommand("SP_SelectTotalAdjustedAmount", con);
                PTouchTotalAdjustedAmount.CommandType = CommandType.StoredProcedure;
                PTouchTotalAdjustedAmount.Parameters.AddWithValue("@Procedure", "PTouchTotalAdjustedAmount");
                PTouchTotalAdjustedAmount.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                PTouchTotalAdjustedAmount.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter da = new SqlDataAdapter(PTouchTotalAdjustedAmount);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    SqlDataReader reader = PTouchTotalAdjustedAmount.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["TotalAmount"].ToString() == "" || reader["TotalAmount"].ToString() == null)
                        {
                            //walang gagawin
                        }
                        else
                        {
                            TotalAdjAmount = Convert.ToDecimal(reader["TotalAmount"].ToString());
                        }
                    }
                }
                con.Close();

                TotalAdjustedAmount.Text = Math.Round(TotalAdjAmount, 4, MidpointRounding.ToEven).ToString();
            }
            else if (SectionDropdown.Text == "Tape Cassette")
            {
                // -> SQL query to select Total Adjusted Amount
                SqlCommand TapeCassetteTotalAdjustedAmount = new SqlCommand("SP_SelectTotalAdjustedAmount", con);
                TapeCassetteTotalAdjustedAmount.CommandType = CommandType.StoredProcedure;
                TapeCassetteTotalAdjustedAmount.Parameters.AddWithValue("@Procedure", "TapeCassetteTotalAdjustedAmount");
                TapeCassetteTotalAdjustedAmount.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                TapeCassetteTotalAdjustedAmount.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter da = new SqlDataAdapter(TapeCassetteTotalAdjustedAmount);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    SqlDataReader reader = TapeCassetteTotalAdjustedAmount.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["TotalAmount"].ToString() == "" || reader["TotalAmount"].ToString() == null)
                        {
                            //walang gagawin
                        }
                        else
                        {
                            TotalAdjAmount = Convert.ToDecimal(reader["TotalAmount"].ToString());
                        }
                    }
                }
                con.Close();

                TotalAdjustedAmount.Text = Math.Round(TotalAdjAmount, 4, MidpointRounding.ToEven).ToString();
            }
        }


        //===================================================================================================================================

        decimal TotalAdjAmount;
        private void GetTotalAdjustedAmount()
        {
         
            //Check Connection status->Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (Dashboard.SectionText == "BIPH-BPS")
            {
                GetTotalAdjustedAmountBySection();
            }
            else if (Dashboard.SectionText == "BIPH-Ink Cartridge")
            {
                GetTotalAdjustedAmountBySection();
            }
            else if (Dashboard.SectionText == "BIPH-Tape Cassette")
            {
                GetTotalAdjustedAmountBySection();
            }
            else if (Dashboard.SectionText == "BIPH-Ink Head")
            {
                GetTotalAdjustedAmountBySection();
            }
             else if (Dashboard.SectionText == "BIPH-Molding Production")
            {
                GetTotalAdjustedAmountBySection();
            }
            else if (Dashboard.SectionText == "BIPH-PCBA")
            {
                GetTotalAdjustedAmountBySection();
            }
            else if (Dashboard.SectionText == "BIPH-Printer")
            {
                GetTotalAdjustedAmountBySection();
            }
            else if (Dashboard.SectionText == "BIPH-P-Touch")
            {
                GetTotalAdjustedAmountBySection();
            }
        }

        //===================================================================================================================================

        private void MonthlyBudgetButton_Click(object sender, EventArgs e)
        {
            MonthlyDisposalBudget DisposaBudgetForm = new MonthlyDisposalBudget();
            DisposaBudgetForm.ShowDialog();
        }

        //===================================================================================================================================


        //===================================================================================================================================
        private void copyAlltoClipboardsss()
        {

            //dgvComponentList.SelectAll();
            //DataObject dataObj = dgvComponentList.GetClipboardContent();
            //if (dataObj != null)
            //    Clipboard.SetDataObject(dataObj);
            PartsLossDataGridView.SelectAll();
            //Copy to clipboard
            PartsLossDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = PartsLossDataGridView.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        // Export data from datagrid to excell
        private void ExportButton_Click(object sender, EventArgs e)
        {
            string pathsss = @"C:\Users\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name.Replace("AP\\", "") + @"\Desktop\COPQ_Exported_Data";
            System.IO.Directory.CreateDirectory(pathsss);

            copyAlltoClipboardsss();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            // xlWorkSheet.Cells[3, "XL"].Cells.NumberFormat = "@";
            CR.Select();
            xlWorkSheet.Cells.NumberFormat = "@";
            //string DateNowVal = DateTime.Now.ToString("yyyyMMdd_hhmmss");
            //string folderPath = "C:\\Users\\manalojo\\Desktop\\Export\\";
            //    xlWorkBook.SaveAs(folderPath + "ViewExport_ " + DateNowVal + ".xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
            //false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
            //Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            xlWorkSheet.Columns.AutoFit();


            MessageBox.Show("Exported successfully", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //try
            //{
            //    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            //    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            //    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            //    app.Visible = true;
            //    worksheet = workbook.Sheets["Sheet1"];
            //    worksheet = workbook.ActiveSheet;
            //    worksheet.Name = "MH Loss";

            //    try
            //    {
            //        for (int i = 0; i < PartsLossDataGridView.Columns.Count; i++)
            //        {
            //            worksheet.Cells[1, i + 1] = PartsLossDataGridView.Columns[i].HeaderText;
            //        }

            //        for (int i = 0; i < PartsLossDataGridView.Rows.Count; i++)
            //        {
            //            for (int j = 0; j < PartsLossDataGridView.Columns.Count; j++)
            //            {
            //                if (PartsLossDataGridView.Rows[i].Cells[j].Value != null)
            //                {
            //                    worksheet.Cells[i + 2, j + 1] = PartsLossDataGridView.Rows[i].Cells[j].Value.ToString();
            //                }
            //                else
            //                {
            //                    worksheet.Cells[i + 2, j + 1] = "";
            //                }
            //            }
            //        }

            //        //Get the location and file name of the excel to save from user. 
            //        SaveFileDialog saveDialog = new SaveFileDialog();
            //        saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            //        saveDialog.FilterIndex = 2;

            //        if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //        {
            //            workbook.SaveAs(saveDialog.FileName);
            //            MessageBox.Show("Export Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }
            //    catch (System.Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }

            //    finally
            //    {
            //        app.Quit();
            //        workbook = null;
            //        worksheet = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        //===========================================================================================================================

        private void SearchPartsLossData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (SectionDropdown.Text == "Ink Cartridge")
            {
                // -> SQL query to search ink cartridge Parts loss data
                SqlCommand SearchInkCartridgePartLossData = new SqlCommand("SP_SearchPartLossData", con);
                SearchInkCartridgePartLossData.CommandType = CommandType.StoredProcedure;
                SearchInkCartridgePartLossData.Parameters.AddWithValue("@Procedure", "SearchInkCartridgePartLossData");
                SearchInkCartridgePartLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SearchInkCartridgePartLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();
                
                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                    
            }
            else if (SectionDropdown.Text == "Tape Cassette")
            {
                // -> SQL query to search tape casstte Parts loss data
                SqlCommand SearchTapeCassettePartLossData = new SqlCommand("SP_SearchPartLossData", con);
                SearchTapeCassettePartLossData.CommandType = CommandType.StoredProcedure;
                SearchTapeCassettePartLossData.Parameters.AddWithValue("@Procedure", "SearchTapeCassettePartLossData");
                SearchTapeCassettePartLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SearchTapeCassettePartLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (SectionDropdown.Text == "Ink Head")
            {
                // -> SQL query to search ink head Parts loss data
                SqlCommand SearchTapeCassettePartLossData = new SqlCommand("SP_SearchPartLossData", con);
                SearchTapeCassettePartLossData.CommandType = CommandType.StoredProcedure;
                SearchTapeCassettePartLossData.Parameters.AddWithValue("@Procedure", "SearchInkHeadPartLossData");
                SearchTapeCassettePartLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SearchTapeCassettePartLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (SectionDropdown.Text == "Molding Production")
            {
                // -> SQL query to search Molding Parts loss data
                SqlCommand SearchTapeCassettePartLossData = new SqlCommand("SP_SearchPartLossData", con);
                SearchTapeCassettePartLossData.CommandType = CommandType.StoredProcedure;
                SearchTapeCassettePartLossData.Parameters.AddWithValue("@Procedure", "SearchMoldingPartLossData");
                SearchTapeCassettePartLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SearchTapeCassettePartLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (SectionDropdown.Text == "PCBA")
            {
                // -> SQL query to search PCBA Parts loss data
                SqlCommand SearchTapeCassettePartLossData = new SqlCommand("SP_SearchPartLossData", con);
                SearchTapeCassettePartLossData.CommandType = CommandType.StoredProcedure;
                SearchTapeCassettePartLossData.Parameters.AddWithValue("@Procedure", "SearchPCBAPartLossData");
                SearchTapeCassettePartLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SearchTapeCassettePartLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (SectionDropdown.Text == "Printer")
            {
                // -> SQL query to search Printer Parts loss data
                SqlCommand SearchTapeCassettePartLossData = new SqlCommand("SP_SearchPartLossData", con);
                SearchTapeCassettePartLossData.CommandType = CommandType.StoredProcedure;
                SearchTapeCassettePartLossData.Parameters.AddWithValue("@Procedure", "SearchPrinterPartLossData");
                SearchTapeCassettePartLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SearchTapeCassettePartLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (SectionDropdown.Text == "P-Touch")
            {
                // -> SQL query to search Ptouch Parts loss data
                SqlCommand SearchTapeCassettePartLossData = new SqlCommand("SP_SearchPartLossData", con);
                SearchTapeCassettePartLossData.CommandType = CommandType.StoredProcedure;
                SearchTapeCassettePartLossData.Parameters.AddWithValue("@Procedure", "SearchPTouchPartLossData");
                SearchTapeCassettePartLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SearchTapeCassettePartLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (SectionDropdown.Text == "BPS")
            {
                // -> SQL query to search PE Parts loss data
                SqlCommand SearchTapeCassettePartLossData = new SqlCommand("SP_SearchPartLossData", con);
                SearchTapeCassettePartLossData.CommandType = CommandType.StoredProcedure;
                SearchTapeCassettePartLossData.Parameters.AddWithValue("@Procedure", "SearchBPSPartLossData");
                SearchTapeCassettePartLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SearchTapeCassettePartLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PartsLossDataGridView.DataSource = dt;
                con.Close();

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        //===================================================================================================================================

        // -->> Search part loss data
        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchPartsLossData();
            }
        }

        //===================================================================================================================================

        private void DailyEmailTimer_Tick(object sender, EventArgs e)
        {
            //SendEmail();
        }

        //===================================================================================================================================

        private void SelectPartsLossDataBaseOnDropdownEntries()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (SectionDropdown.Text == "Ink Cartridge")
            {
                if (ShowEntriesDropdown.Text == "All")
                {
                    // -> SQL query to select parts loss data setting
                    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "InkCartridge");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectAll");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", "");
                    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();

                }
                else
                {
                    // -> SQL query to select parts loss data setting
                    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "InkCartridge");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectBasedOnEntriesValue");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                }
            }
            else if (SectionDropdown.Text == "Tape Cassette")
            {
                if (ShowEntriesDropdown.Text == "All")
                {
                    // -> SQL query to select parts loss data setting
                    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "TapeCassette");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectAll");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", "");
                    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();

                }
                else
                {
                    // -> SQL query to select parts loss data setting
                    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "TapeCassette");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectBasedOnEntriesValue");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                }
            }
            else if (SectionDropdown.Text == "Ink Head")
            {
                if (ShowEntriesDropdown.Text == "All")
                {
                    // -> SQL query to select parts loss data setting
                    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "InkHead");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectAll");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", "");
                    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();

                }
                else
                {
                    // -> SQL query to select parts loss data setting
                    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "InkHead");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectBasedOnEntriesValue");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                }
            }
            else if (SectionDropdown.Text == "Molding Production")
            {
                //if (ShowEntriesDropdown.Text == "All")
                //{
                //    // -> SQL query to select parts loss data setting
                //    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                //    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                //    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "Molding");
                //    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectAll");
                //    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                //    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                //    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", "");
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    PartsLossDataGridView.DataSource = dt;
                //    con.Close();

                //    GetTotalAdjustedAmount();

                //}
                //else
                //{
                //    // -> SQL query to select parts loss data setting
                //    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                //    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                //    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "Molding");
                //    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectBasedOnEntriesValue");
                //    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                //    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                //    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", ShowEntriesDropdown.Text);
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    PartsLossDataGridView.DataSource = dt;
                //    con.Close();

                //    GetTotalAdjustedAmount();
                //}
            }
            else if (SectionDropdown.Text == "PCBA")
            {
                if (ShowEntriesDropdown.Text == "All")
                {
                    // -> SQL query to select parts loss data setting
                    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "PCBA");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectAll");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", "");
                    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();

                }
                else
                {
                    // -> SQL query to select parts loss data setting
                    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "PCBA");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectBasedOnEntriesValue");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                }
            }
            else if (SectionDropdown.Text == "Printer")
            {
                if (ShowEntriesDropdown.Text == "All")
                {
                    // -> SQL query to select parts loss data setting
                    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "Printer");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectAll");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", "");
                    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();

                }
                else
                {
                    // -> SQL query to select parts loss data setting
                    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "Printer");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectBasedOnEntriesValue");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                }
            }
            else if (SectionDropdown.Text == "BPS")
            {
                if (ShowEntriesDropdown.Text == "All")
                {
                    // -> SQL query to select parts loss data setting
                    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "BPS");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectAll");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", "");
                    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();

                }
                else
                {
                    // -> SQL query to select parts loss data setting
                    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "BPS");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectBasedOnEntriesValue");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                }
            }
            else if (SectionDropdown.Text == "P-Touch")
            {
                if (ShowEntriesDropdown.Text == "All")
                {
                    // -> SQL query to select parts loss data setting
                    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "PTouch");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectAll");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", "");
                    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();

                }
                else
                {
                    // -> SQL query to select parts loss data setting
                    SqlCommand SelectInkCartridgePartsLossData = new SqlCommand("SP_SelectPartsLossDataBaseOnDropdownEntries", con);
                    SelectInkCartridgePartsLossData.CommandType = CommandType.StoredProcedure;
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Section", "PTouch");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Procedure", "SelectBasedOnEntriesValue");
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SelectInkCartridgePartsLossData.Parameters.AddWithValue("@Value", ShowEntriesDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectInkCartridgePartsLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    PartsLossDataGridView.DataSource = dt;
                    con.Close();

                    GetTotalAdjustedAmount();
                }
            }
        }

        //===================================================================================================================================

   

        //===================================================================================================================================

        private void PartsLossDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in PartsLossDataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        //===================================================================================================================================

        private void ShowTopDefectButton_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://bi.datalake.brother.co.jp/#/site/biph/views/Top5DefectRecurrence/Top5DefectRecurrence?:iid=6");
            //if (Dashboard.SectionText == "BIPH-Tape Cassette")
            //{
            //    //Disable Section Dropdown
            //    DisableSectionDropdown();

            //    // Check Connection status -> Open connection if the connection is closed
            //    if (con.State == ConnectionState.Closed)
            //    {
            //        con.Open();
            //    }

            //    SqlCommand SelectDefectData = new SqlCommand("SP_SelectTop5DefectRecurrence", con);
            //    SelectDefectData.CommandType = CommandType.StoredProcedure;
            //    SqlDataAdapter sda = new SqlDataAdapter(SelectDefectData);
            //    DataTable dt = new DataTable();
            //    sda.Fill(dt);
            //    PartsLossDataGridView.DataSource = dt;
            //    con.Close();

            //    PartsLossDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //    PartsLossDataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //    ////Hide column ID in datagridview
            //    //PartsLossDataGridView.Columns["ID"].Visible = false;

            //}
        }


        public static bool HaveNewUploadedData = false;
        private void RefreshDatagridTimer_Tick(object sender, EventArgs e)
        {
            if (HaveNewUploadedData == true)
            {
                //LoadPartLossData(); --old

                SelectPartsLossDataBaseOnDropdownEntries(); // -- new
                GetTotalAdjustedAmount();

                HaveNewUploadedData = false;
            }
        }

        private void ShowEntriesDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
            {
                if (DefectBtnClicked == true)
                {
                    PartLossBtnClicked = false;

                    //Load all defect data to datagrid
                    LoadDefectData();
                }
                else if (PartLossBtnClicked == true)
                {
                    DefectBtnClicked = false;
                    SelectPartsLossDataBaseOnDropdownEntries();
                }
                else
                {
                    SelectPartsLossDataBaseOnDropdownEntries();
                }
            }
            else
            {
                SelectPartsLossDataBaseOnDropdownEntries();
            }
            
            ShowEntriesDropdown.ForeColor = Color.FromArgb(21, 35, 53);
        }

        private void SectionDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SelectPartsLossData(); // load data from db
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchPartsLossData();
        }




        //=============================================================================================================================>>>>>>>>>>>

    }
}
