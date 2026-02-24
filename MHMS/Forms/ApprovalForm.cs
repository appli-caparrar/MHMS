using MHMS.Class;
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
    public partial class ApprovalForm : Form
    {

        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        private LoadingForm loadingForm;

        public ApprovalForm()
        {
            InitializeComponent();
            loadingForm = new LoadingForm(); // Create loading form instance
        }

       

        private void LoadApproverTypeList()
        {
            if (LoginForm.COPQPIC == "✔️" && LoginForm.ProcessInCharge == "✔️" && LoginForm.SectionSPV == "✔️")
            {
                RoleDropDown.Items.Add("COPQ PIC");
                RoleDropDown.Items.Add("COPQ Process In-Charge");
                RoleDropDown.Items.Add("SPV");

                
                RoleDropDown.Text = "COPQ PIC";
            }
            else if (LoginForm.COPQPIC == "✔️" && LoginForm.ProcessInCharge == "✔️")
            {
                RoleDropDown.Items.Add("COPQ PIC");
                RoleDropDown.Items.Add("COPQ Process In-Charge");
               
                RoleDropDown.Text = "COPQ PIC";
            }
            else if (LoginForm.COPQPIC == "✔️" && LoginForm.SectionSPV == "✔️")
            {
                RoleDropDown.Items.Add("COPQ PIC");
                RoleDropDown.Items.Add("SPV");

                RoleDropDown.Text = "COPQ PIC";
            }
            else if (LoginForm.ProcessInCharge == "✔️" && LoginForm.SectionSPV == "✔️")
            {
                RoleDropDown.Items.Add("COPQ Process In-Charge");
                RoleDropDown.Items.Add("SPV");

                
                RoleDropDown.Text = "COPQ Process In-Charge";
            }
            else if (LoginForm.COPQPIC == "✔️")
            {
                RoleDropDown.Items.Add("COPQ PIC");
                
                RoleDropDown.Text = "COPQ PIC";
            }
            else if (LoginForm.ProcessInCharge == "✔️")
            {
                RoleDropDown.Items.Add("COPQ Process In-Charge");
                
                RoleDropDown.Text = "COPQ Process In-Charge";
            }
            else if (LoginForm.SectionSPV == "✔️")
            {
                RoleDropDown.Items.Add("SPV");
                
                RoleDropDown.Text = "SPV";
            }
            else if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
            {
                RoleDropDown.Items.Add("MGR");
                
                RoleDropDown.Text = "MGR";
            }
        }
        //===================================================================================================================>>>>>>>>>>>>

        string FullName;
        private void ApprovalForm_Load(object sender, EventArgs e)
        {
            //if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
            //{
            //    MessageBox.Show("TESSSTTTTT");
            //}
               
            //System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-PH");
            //System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            //Temporary
            if (Dashboard.SectionText.Replace("BIPH-", "") == "Quality Innovation")
            {
                GeneratePreviousQIForConfirmationBtn.Visible = true;
            }
            else
            {
                GeneratePreviousQIForConfirmationBtn.Visible = false;
            }

           
            if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS" && Dashboard.AccountType == "ADMIN")
            {
                ApproveAllPendingBtn.Visible = true;
            }
            else
            {
                ApproveAllPendingBtn.Visible = false;
            }

            FullName = LoginForm.FirstName + " " + LoginForm.LastName;

            LoadApproverTypeList(); //add items in combobox role

            //Set backcolor and fore color to column header
            ApprovalDataGrid.EnableHeadersVisualStyles = true;
            ApprovalDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(86, 119, 157);
            ApprovalDataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //Hide row header
            ApprovalDataGrid.RowHeadersVisible = false;

            //Add checked box column
            AddCheckedBoxColumn();

            /*FormatHeaderText();*/ // Format Column Header Text
            if (DashboardForm2.COPQAcceptanceButtonIsClicked == true)
            {
                CategoryDropdown.Text = "COPQ";
            }
            //else if (DashboardForm2.STButtonIsClicked == true)
            //{
            //    CategoryDropdown.Text = "ST";
            //}
            //else if (DashboardForm2.WCCCButtonIsClicked == true)
            //{
            //    CategoryDropdown.Text = "WC/CC";
            //}


            //LoadApplyingApprovalData(); // Display all request for approval

            //UI 
            if (LoginForm.UserSection == "Quality Innovation")
            {
                TypeofApprovalDropdown.Text = "QI Confirmation";
                TypeofApprovalDropdown.Enabled = false;
                RoleDropDown.Text = "QI";
                RoleDropDown.Enabled = false;
                StatusDropdown.Text = "For QI Confirmation";
                //StatusDropdown.Enabled = false;

                StatusDropdown.Items.Add("For QI Confirmation"); // Add "For QI Confirmation" item in dropdown list when QI user logged in
                StatusDropdown.Items.Remove("Cancelled"); // Removed "Cancelled" item in  dropdown list when QI user logged in

                AcceptButton.Text = "CONFIRM";

                ExcludeCheckBox.Visible = true; // show checkbox
            }

            ////Checked as default to exclude EE when for was loaded
            //ExcludeCheckBox.Checked = true;

        }

        //===================================================================================================================>>>>>>>>>>>>

        private void AddCheckedBoxColumn()
        {
            // Add checkbox column in datagrid
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "Select";
            checkColumn.HeaderText = "Select";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;
            checkColumn.FillWeight = 50; //if the datagridview is resized (on form resize) the checkbox won't take up too much; value is relative to the other columns' fill values
            ApprovalDataGrid.Columns.Add(checkColumn);
            checkColumn.DisplayIndex = 0;
            checkColumn.Frozen = true;
            // <<----------
        }

        //===================================================================================================================>>>>>>>>>>>>

        private void SelectApprovalCount()
        {
            ApprovalCount.Visible = true;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectApprovalCount = new SqlCommand("SP_SelectApprovalCount", con);
            SelectApprovalCount.CommandType = CommandType.StoredProcedure;
            SelectApprovalCount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SelectApprovalCount.Parameters.AddWithValue("@Role", RoleDropDown.Text);
            SelectApprovalCount.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
            SelectApprovalCount.Parameters.AddWithValue("@Status", StatusDropdown.Text);
            SelectApprovalCount.Parameters.AddWithValue("@AssignedSection", LoginForm.EESection);
            SelectApprovalCount.Parameters.AddWithValue("@ProcessInCharge", LoginForm.FirstName + " " + LoginForm.LastName);
            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalCount);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                SqlDataReader reader = SelectApprovalCount.ExecuteReader();

                while (reader.Read())
                {
                    ApprovalCount.Text = reader["ApprovalCount"].ToString() + " For Approval";
                }
            }

            con.Close();
        }

        //===================================================================================================================>>>>>>>>>>>>


        //====================================================================================================================>>>>>>>>>>>>

        private void FormatHeaderText()
        {
            //ApprovalDataGrid.Columns["ReferenceNo"].HeaderText = "Reference No.";
            //ApprovalDataGrid.Columns["DateEncountered"].HeaderText = "Date Encountered";
            //ApprovalDataGrid.Columns["MHLossType"].HeaderText = "MH Loss Type";
            //ApprovalDataGrid.Columns["Section"].HeaderText = "Section";
            //ApprovalDataGrid.Columns["CostCenter"].HeaderText = "Cost Center";
            //ApprovalDataGrid.Columns["ResponsibleSection"].HeaderText = "Responsible Section";
            //ApprovalDataGrid.Columns["LineStopDetail"].HeaderText = "Rease (Line Stop Detail)";
            //ApprovalDataGrid.Columns["StopTime"].HeaderText = "Stop Time";
            //ApprovalDataGrid.Columns["DirectMP"].HeaderText = "Direct MP";
            //ApprovalDataGrid.Columns["SemiDirectMP"].HeaderText = "Semi-Direct MP";
            //ApprovalDataGrid.Columns["LossManhour"].HeaderText = "Loss Manhour";
            //ApprovalDataGrid.Columns["Reason"].HeaderText = "Reason";
            //ApprovalDataGrid.Columns["COPQAmount(USD)"].HeaderText = "COPQ Amount";
            //ApprovalDataGrid.Columns["Cause"].HeaderText = "Cause";
            //ApprovalDataGrid.Columns["ReasonOfRejection"].HeaderText = "Reason of Rejection";
            //ApprovalDataGrid.Columns["Countermeasure"].HeaderText = "Countermeasure (if accepted) / Reason (if rejected)";
            //ApprovalDataGrid.Columns["ApplyingApprovalStatus"].HeaderText = "Applying Approval Status";
            //ApprovalDataGrid.Columns["ReceivingApprovalStatus"].HeaderText = "Receiving Approval Status";
            //ApprovalDataGrid.Columns["QIConfirmation"].HeaderText = "QI Confirmation";
        }

        //====================================================================================================================>>>>>>>>>>>>

        //Select all for approval data where section column is equal to user section
        private void LoadApplyingApprovalData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (Dashboard.SectionText == "BIPH-BPS")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectBPSApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SPV approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectBPSApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectBPSApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

            }
            else if (Dashboard.SectionText == "BPS")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SPV approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Ink Cartridge")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
                else if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
                else if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

            }
            else if (Dashboard.SectionText == "BIPH-PCBA")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPCBAApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPCBAApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPCBAApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-Ink Head")
            {

                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {

                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkHeadApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkHeadApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkHeadApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-Molding Production")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMoldingApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMoldingApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {

                    }
                    // -> SQL query to select approval data for MGR approval
                    SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                    SelectApprovalData.CommandType = CommandType.StoredProcedure;
                    SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMoldingApprovalData");
                    SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                    SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApprovalDataGrid.DataSource = dt;
                    con.Close();
                }
            }
            else if (Dashboard.SectionText == "BIPH-Printer")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPrinterApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPrinterApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPrinterApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-P-Touch")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPTouchApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPTouchApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPTouchApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-Tape Cassette")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Logistics Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectLogisticsControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectLogisticsControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectLogisticsControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Incoming Quality Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectIncomingQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectIncomingQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectIncomingQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Information Technology")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInformationTechnologyApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInformationTechnologyApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInformationTechnologyApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Material Purchasing 2")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMaterialPurchasing2ApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMaterialPurchasing2ApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMaterialPurchasing2ApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Production Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Quality Assurance")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityAssuranceApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityAssuranceApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityAssuranceApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Quality Innovation")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityInnovationApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityInnovationApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityInnovationApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Supplier Quality Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectSupplierQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectSupplierQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectSupplierQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }

        }

        //====================================================================================================================>>>>>>>>>>>>

        //Select all for approval data where responsible section is equal to user section
        private void LoadReceivingApprovalData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (Dashboard.SectionText == "BIPH-BPS")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectBPSApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectBPSApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectBPSApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectBPSApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-BPS")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-Ink Cartridge")
            {

                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-PCBA")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPCBAApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPCBAApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPCBAApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPCBAApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-Ink Head")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkHeadApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkHeadApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkHeadApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkHeadApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-Molding Production")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMoldingApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMoldingApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMoldingApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMoldingApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Printer")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPrinterApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPrinterApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPrinterApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPrinterApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-P-Touch")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPTouchApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPTouchApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPTouchApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPTouchApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-Tape Cassette")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Logistics Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectLogisticsControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectLogisticsControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectLogisticsControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectLogisticsControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Incoming Quality Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectIncomingQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectIncomingQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectIncomingQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectIncomingQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Information Technology")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInformationTechnologyApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInformationTechnologyApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInformationTechnologyApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInformationTechnologyApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Material Purchasing 2")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMaterialPurchasing2ApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMaterialPurchasing2ApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }


                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMaterialPurchasing2ApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMaterialPurchasing2ApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Production Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Quality Assurance")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityAssuranceApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityAssuranceApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityAssuranceApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityAssuranceApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Quality Innovation")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityInnovationApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityInnovationApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityInnovationApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityInnovationApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Supplier Quality Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectSupplierQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectSupplierQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectSupplierQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectSupplierQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
                else if (Dashboard.SectionText == "BIPH-Development Engineering")
                {
                    if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                    {
                        if (RoleDropDown.Text == "COPQ PIC")
                        {
                            // -> SQL query to select approval data for COPQ PIC approval
                            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                            SelectApprovalData.CommandType = CommandType.StoredProcedure;
                            SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectDevelopmentEngineeringApprovalData");
                            SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                            SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            ApprovalDataGrid.DataSource = dt;
                            con.Close();
                        }
                    }

                    if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                    {
                        if (RoleDropDown.Text == "COPQ Process In-Charge")
                        {
                            // -> SQL query to select approval data for COPQ process in-charge approval
                            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                            SelectApprovalData.CommandType = CommandType.StoredProcedure;
                            SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectDevelopmentEngineeringApprovalData");
                            SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                            SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            ApprovalDataGrid.DataSource = dt;
                            con.Close();
                        }
                    }

                    if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                    {
                        if (RoleDropDown.Text == "SPV")
                        {
                            // -> SQL query to select approval data for SVP approval
                            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                            SelectApprovalData.CommandType = CommandType.StoredProcedure;
                            SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectDevelopmentEngineeringApprovalData");
                            SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                            SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            ApprovalDataGrid.DataSource = dt;
                            con.Close();
                        }

                    }

                    if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                    {
                        if (RoleDropDown.Text == "MGR")
                        {
                            // -> SQL query to select approval data for MGR approval
                            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                            SelectApprovalData.CommandType = CommandType.StoredProcedure;
                            SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectDevelopmentEngineeringApprovalData");
                            SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                            SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            ApprovalDataGrid.DataSource = dt;
                            con.Close();
                        }
                    }
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        //Check each checkbox
        private void SelectAllChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectAllChkBox.Checked == true)
            {
                foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                {
                    row.Cells["Select"].Value = true;
                }
            }
            else if (SelectAllChkBox.Checked == false)
            {
                foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                {
                    row.Cells["Select"].Value = false;
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchRequestForApprovalData();
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void SearchRequestForApprovalData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (TypeofApprovalDropdown.Text == "QI Confirmation")
            {
                if (ExcludeCheckBox.Checked == true)
                {
                    SqlCommand SearchMHLossData = new SqlCommand("SP_SearchRequestForApproval", con);
                    SearchMHLossData.CommandType = CommandType.StoredProcedure;
                    SearchMHLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
                    SearchMHLossData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SearchMHLossData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                    SearchMHLossData.Parameters.AddWithValue("@Role", "");
                    SearchMHLossData.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                    SearchMHLossData.Parameters.AddWithValue("@ExcludeEE", "true");
                    SearchMHLossData.Parameters.AddWithValue("@AssignedSection", "");
                    SqlDataAdapter sda = new SqlDataAdapter(SearchMHLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApprovalDataGrid.DataSource = dt;
                    con.Close();

                    if (dt.Rows.Count < 1)
                    {
                        MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    SqlCommand SearchMHLossData = new SqlCommand("SP_SearchRequestForApproval", con);
                    SearchMHLossData.CommandType = CommandType.StoredProcedure;
                    SearchMHLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
                    SearchMHLossData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SearchMHLossData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                    SearchMHLossData.Parameters.AddWithValue("@Role", "");
                    SearchMHLossData.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                    SearchMHLossData.Parameters.AddWithValue("@ExcludeEE", "");
                    SearchMHLossData.Parameters.AddWithValue("@AssignedSection", "");
                    SqlDataAdapter sda = new SqlDataAdapter(SearchMHLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApprovalDataGrid.DataSource = dt;
                    con.Close();

                    if (dt.Rows.Count < 1)
                    {
                        MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                //ExcludeEEData();

            }
            else
            {
                SqlCommand SearchMHLossData = new SqlCommand("SP_SearchRequestForApproval", con);
                SearchMHLossData.CommandType = CommandType.StoredProcedure;
                SearchMHLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
                SearchMHLossData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SearchMHLossData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                SearchMHLossData.Parameters.AddWithValue("@Role", RoleDropDown.Text);
                SearchMHLossData.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                SearchMHLossData.Parameters.AddWithValue("@ExcludeEE", "");
                SearchMHLossData.Parameters.AddWithValue("@AssignedSection", LoginForm.EESection);
                SqlDataAdapter sda = new SqlDataAdapter(SearchMHLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);                                                                                                                                                                                                                                    
                ApprovalDataGrid.DataSource = dt;
                con.Close();

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            /*FormatHeaderText();*/ // Format header text
        }

        //===================================================================================================================>>>>>>>>>>>>
        private void copyAlltoClipboardsss()
        {
            //dgvComponentList.SelectAll();
            //DataObject dataObj = dgvComponentList.GetClipboardContent();
            //if (dataObj != null)
            //    Clipboard.SetDataObject(dataObj);
            ApprovalDataGrid.SelectAll();


            //Copy to clipboard
            ApprovalDataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = ApprovalDataGrid.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (ApprovalDataGrid.DataSource == null)
            {
                MessageBox.Show("No data found, Please generate the data before clicking export button!",
                                "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Change button text to "Exporting..."
            var btn = sender as Button;
            btn.Text = "     Exporting...";
            btn.Enabled = false; // disable to prevent double-click
            btn.BackColor = Color.Gray;     // background white
            btn.ForeColor = Color.Black;
            btn.Refresh();       // force UI update

            try
            {
                var xlexcel = new Microsoft.Office.Interop.Excel.Application();
                var xlWorkBook = xlexcel.Workbooks.Add();
                var xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Sheets[1];

                // Write column headers
                for (int i = 0; i < ApprovalDataGrid.Columns.Count; i++)
                {
                    xlWorkSheet.Cells[1, i + 1] = ApprovalDataGrid.Columns[i].HeaderText;
                }

                // Write rows
                for (int i = 0; i < ApprovalDataGrid.Rows.Count; i++)
                {
                    for (int j = 0; j < ApprovalDataGrid.Columns.Count; j++)
                    {
                        xlWorkSheet.Cells[i + 2, j + 1] = ApprovalDataGrid.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                xlWorkSheet.Columns.AutoFit();
                xlexcel.Visible = true;

                MessageBox.Show("Exported successfully", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Restore button
                btn.Text = "Export";
                btn.Enabled = true;
                btn.BackColor = Color.FromArgb(47, 69, 180);   // keep white if you always want white
                btn.ForeColor = Color.White;
            }
        }




        //====================================================================================================================>>>>>>>>>>

        public static string SelectedRowReferenceNo;
        public static string SelectedLineStopDetail;
        public static string ApprovalType;

        private async void RejectButton_Click(object sender, EventArgs e)
        {

            List<DataGridViewRow> selectedRows = (from row in ApprovalDataGrid.Rows.Cast<DataGridViewRow>()
                                                  where Convert.ToBoolean(row.Cells["Select"].Value) == true
                                                  select row).ToList();

            if (selectedRows.Count < 1 || selectedRows.Count == 0)
            {
                MessageBox.Show("Please select the item you want to reject!", "Reminders!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //else if (selectedRows.Count > 1)
            //{
            //    MessageBox.Show("Cannot process multiple selected data, Please select one item to reject request!", "Reminders!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
            else
            {
                foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                {
                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                    {
                        //SelectedRowReferenceNo = row.Cells["Reference No."].Value.ToString();
                        DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                        LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                        PartCode = row.Cells["Part Code"].Value.ToString();
                        SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                        ApprovalType = TypeofApprovalDropdown.Text;
                        DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

                        if (MessageBox.Show("Are you sure you want to reject item with line stop details of " + "'" + SelectedLineStopDetail + "'?", "MHMS Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            RejectionForm rejectionForm = new RejectionForm();
                            rejectionForm.ShowDialog();
                        }
                    }
                }

                await GenerateMHData();
            }

           
        }

        //===================================================================================================================>>>>>>>>>>>>

        private void ApprovalDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in ApprovalDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //set color red to  rejected status
            if (ApprovalDataGrid.Rows[e.RowIndex].Cells["Over All Status"].Value.ToString() == "Rejected")
            {
                DataGridViewRow row = ApprovalDataGrid.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 142, 150);
            }

            //set color yellow to cancelled status
            if (ApprovalDataGrid.Rows[e.RowIndex].Cells["Over All Status"].Value.ToString() == "Cancelled")
            {
                DataGridViewRow row = ApprovalDataGrid.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = Color.FromArgb(241, 225, 119);
            }


        }

        //===================================================================================================================>>>>>>>>>>>>


        public static bool ContinueButtonIsClicked = false;
        public static bool ApproveButtonIsClicked = false;
        public static bool AcceptButtonIsClicked = false;
        private async void FrefreshDatagridTimer_Tick(object sender, EventArgs e)
        {
            if (ContinueButtonIsClicked == true)
            {
                await GenerateMHData();

                ContinueButtonIsClicked = false;
            }

            if (ApproveButtonIsClicked == true)
            {
                await GenerateMHData();

                ApproveButtonIsClicked = false;
            }

            if (AcceptButtonIsClicked == true)
            {
                await GenerateMHData();

                AcceptButtonIsClicked = false;
            }
        }

        //=================================================================================================================>>>>>>>>>>>>

        private async Task GenerateMHData()
        {
            await con.OpenAsync();

            try
            {
                if (CategoryDropdown.Text == "COPQ")
                {
                    if (StatusDropdown.Text == "For Approval")
                    {
                        SelectAllChkBox.Visible = true; //Show select all checkbox
                        ExcludeCheckBox.Location = new Point(120, 110); //Set location to new point

                        if (RoleDropDown.Text == "COPQ Process In-Charge")
                        {
                            //Select all process in-charge user
                            SqlCommand SelectProcessInchargeUser = new SqlCommand("SP_SelectProcessInchargeUsers", con);
                            SelectProcessInchargeUser.CommandType = CommandType.StoredProcedure;
                            SelectProcessInchargeUser.Parameters.AddWithValue("@UserSection", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter da = new SqlDataAdapter(SelectProcessInchargeUser);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            //if process in-charge count is greater than 1 select the full name of user 
                            if (dt.Rows.Count > 1)
                            {
                                //show all data where the receiving status is for approval by copq process in-charge and fullname is equal to user login

                                SqlCommand SelectApprovalData = new SqlCommand("SP_SelectCOPQProcessInChargeData", con);
                                SelectApprovalData.CommandType = CommandType.StoredProcedure;
                                SelectApprovalData.Parameters.AddWithValue("@Status", "For Approval");
                                SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                                SelectApprovalData.Parameters.AddWithValue("@Role", RoleDropDown.Text);
                                SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SelectApprovalData.Parameters.AddWithValue("@FullName", FullName);
                                SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                                DataTable dataTable = new DataTable();
                                sda.Fill(dataTable);
                                ApprovalDataGrid.DataSource = dataTable;
                                //con.Close();

                                if (IsGenerateBtnClick == true)
                                {
                                    if (dataTable.Rows.Count < 1)
                                    {
                                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    IsGenerateBtnClick = false;
                                }

                            }
                            else if (dt.Rows.Count == 1)
                            {

                                //if process in-charge count is equal to 1
                                //show all data where the receiving status is for approval by copq process in-charge
                                SqlCommand SelectApprovalData = new SqlCommand("SP_SelectFilteredMHData", con);
                                SelectApprovalData.CommandType = CommandType.StoredProcedure;
                                SelectApprovalData.Parameters.AddWithValue("@Status", "For Approval");
                                SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                                SelectApprovalData.Parameters.AddWithValue("@Role", RoleDropDown.Text);
                                SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SelectApprovalData.Parameters.AddWithValue("@AssignedSection", "");
                                SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                                DataTable dataTable = new DataTable();
                                sda.Fill(dataTable);
                                ApprovalDataGrid.DataSource = dataTable;
                                //con.Close();

                                if (IsGenerateBtnClick == true)
                                {
                                    if (dataTable.Rows.Count < 1)
                                    {
                                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    IsGenerateBtnClick = false;
                                }
                            }
                        }
                        else
                        {
                            if (Dashboard.SectionText.Replace("BIPH-", "") == "Equipment Engineering")
                            {
                                //-> SQL query to select approval data based on status
                                SqlCommand SelectApprovalData = new SqlCommand("SP_SelectFilteredMHData", con);
                                SelectApprovalData.CommandType = CommandType.StoredProcedure;
                                SelectApprovalData.Parameters.AddWithValue("@Status", "For Approval");
                                SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                                SelectApprovalData.Parameters.AddWithValue("@Role", RoleDropDown.Text);
                                SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SelectApprovalData.Parameters.AddWithValue("@AssignedSection", Dashboard.EEAssignedSection);
                                SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                                DataTable dataTable = new DataTable();
                                sda.Fill(dataTable);
                                ApprovalDataGrid.DataSource = dataTable;
                                //con.Close();

                                if (IsGenerateBtnClick == true)
                                {
                                    if (dataTable.Rows.Count < 1)
                                    {
                                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    IsGenerateBtnClick = false;
                                }
                            }
                            else
                            {
                                //-> SQL query to select approval data based on status
                                SqlCommand SelectApprovalData = new SqlCommand("SP_SelectFilteredMHData", con);
                                SelectApprovalData.CommandType = CommandType.StoredProcedure;
                                SelectApprovalData.Parameters.AddWithValue("@Status", "For Approval");
                                SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                                SelectApprovalData.Parameters.AddWithValue("@Role", RoleDropDown.Text);
                                SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SelectApprovalData.Parameters.AddWithValue("@AssignedSection", "");
                                SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                                DataTable dataTable = new DataTable();
                                sda.Fill(dataTable);
                                ApprovalDataGrid.DataSource = dataTable;
                                //con.Close();

                                if (IsGenerateBtnClick == true)
                                {
                                    if (dataTable.Rows.Count < 1)
                                    {
                                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    IsGenerateBtnClick = false;
                                }
                            }
                        }


                        ApprovalDataGrid.Columns["Select"].Visible = true; //Show select checkbox column

                        SelectApprovalCount();

                    }
                    else if (StatusDropdown.Text == "Approved")
                    {
                        SelectAllChkBox.Visible = false; //Hide this checkbox when status selected was "Approved"
                        ExcludeCheckBox.Location = new Point(0, 110);


                        // -> SQL query to select approval data based on status
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectFilteredMHData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Status", "Approved");
                        SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                        SelectApprovalData.Parameters.AddWithValue("@Role", ""); //this an empty parameter to prevent error 
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SelectApprovalData.Parameters.AddWithValue("@AssignedSection", "");
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        //con.Close();


                        ApprovalDataGrid.Columns["Select"].Visible = false; //Hide check box

                        ApprovalCount.Visible = false; // hide for approval count

                    }
                    else if (StatusDropdown.Text == "Rejected")
                    {
                        SelectAllChkBox.Visible = false; //Hide this checkbox when status selected was "Approved"
                        ExcludeCheckBox.Location = new Point(0, 110);

                        // -> SQL query to select approval data based on status
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectFilteredMHData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Status", "Rejected");
                        SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                        SelectApprovalData.Parameters.AddWithValue("@Role", ""); //this an empty parameter to prevent error 
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SelectApprovalData.Parameters.AddWithValue("@AssignedSection", "");
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        //con.Close();


                        ApprovalDataGrid.Columns["Select"].Visible = false;//Hide check box

                        ApprovalCount.Visible = false; // hide for approval count
                    }
                    else if (StatusDropdown.Text == "Cancelled")
                    {
                        SelectAllChkBox.Visible = false; //Hide this checkbox when status selected was "Approved"
                        ExcludeCheckBox.Location = new Point(0, 110);

                        // -> SQL query to select approval data based on status
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectFilteredMHData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Status", "Cancelled");
                        SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                        SelectApprovalData.Parameters.AddWithValue("@Role", ""); //this an empty parameter to prevent error 
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SelectApprovalData.Parameters.AddWithValue("@AssignedSection", "");
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        //con.Close();

                        ApprovalDataGrid.Columns["Select"].Visible = false; //Hide check box

                        ApprovalCount.Visible = false; // hide for approval count

                    }
                    else if (TypeofApprovalDropdown.Text == "QI Confirmation")
                    {

                        //con.Open();
                        //SqlCommand SelectApprovalData = new SqlCommand("SP_SelectFilteredMHData", con);
                        //SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        //SelectApprovalData.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                        //SelectApprovalData.Parameters.AddWithValue("@Type", "QI Confirmation");
                        //SelectApprovalData.Parameters.AddWithValue("@Role", "");
                        //SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        //SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        //DataTable dt = new DataTable();
                        //sda.Fill(dt);
                        //ApprovalDataGrid.DataSource = dt;
                        //con.Close();

                        SelectAllChkBox.Visible = true; //Show select all checkbox
                        ExcludeCheckBox.Location = new Point(120, 110); //Set location to new point
                        ApprovalDataGrid.Columns["Select"].Visible = true; //Show checkbox colum

                        ExcludeEEData();
                        ApprovalCount.Visible = false; // hide for approval count
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            { 
                con.Close();
            }
            
        }

        public static bool IsGenerateBtnClick = false;

        private async void GenerateButton_Click(object sender, EventArgs e)
        {
            IsGenerateBtnClick = true;

            LoadingForm LoadingForm = new LoadingForm();
            LoadingForm.Show();

            // Disable the button or other controls if needed to prevent user interaction
            GenerateButton.Enabled = false;

            try
            {
                await Task.Delay(4000);

                // Call your async method to load data
                await GenerateMHData();

                if (ApprovalDataGrid.DataSource != null)
                {
                    ApprovalDataGrid.Columns["DistinctionCode"].Visible = false;
                }

            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the async operation
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Hide the loading image when the operation is complete
                LoadingForm.Close();

                // Re-enable the button or controls
                GenerateButton.Enabled = true;
            }

            
            
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void TypeofApprovalDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

            //FilterApprovalDataBasedOnSelectedIndex();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void FilterApprovalDataBasedOnSelectedIndex()
        {
            if (TypeofApprovalDropdown.Text == "Applying")
            {
                LoadApplyingApprovalData();
            }
            else if (TypeofApprovalDropdown.Text == "Receiving")
            {
                LoadReceivingApprovalData();
            }
        }

        public static string SelectedReferenceNo;
        public static string LineStopDetail;
        public static string PartCode;
        public static string COPQAmount;
        public static string DateEncountered;
        public static string DistinctionCode;
        public static string ResponsibleSection;

        private void ApprovalDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ApprovalDataGrid.Columns[e.ColumnIndex].Name == "Select")
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)ApprovalDataGrid.Rows[e.RowIndex].Cells["Select"];

                if ((bool)checkCell.Value == true)
                {
                    int i = ApprovalDataGrid.CurrentRow.Index;
                    LineStopDetail = ApprovalDataGrid.Rows[e.RowIndex].Cells["Line Stop Detail"].Value.ToString();

                    if ((bool)checkCell.Value == false)
                    {
                        checkCell.EditingCellValueChanged = false;
                    }
                }
            }
        }

        public static string SelectedProcessIncharge;
        public static string SelectedApprovalType;

        private async void AcceptButton_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> selectedRows = (from row in ApprovalDataGrid.Rows.Cast<DataGridViewRow>()
                                                  where Convert.ToBoolean(row.Cells["Select"].Value) == true
                                                  select row).ToList();

            if (selectedRows.Count < 1 || selectedRows.Count == 0)
            {
                MessageBox.Show("Please select data you want to approve!", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (TypeofApprovalDropdown.Text == "Applying")
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn))
                        {
                            await con.OpenAsync();


                            if (RoleDropDown.Text == "COPQ PIC")
                            {
                                foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                                {
                                    DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                    LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                    PartCode = row.Cells["Part Code"].Value.ToString();
                                    ApprovalType = TypeofApprovalDropdown.Text;
                                    DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

                                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                                    {
                                        COPQConfirmationForm copqConfirmationForm = new COPQConfirmationForm();
                                        copqConfirmationForm.ShowDialog();
                                    }
                                }

                                MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                await GenerateMHData();

                                SelectAllChkBox.Checked = false;
                                //LoadApplyingApprovalData();

                                //Send email to Applying section SPV
                                COPQ_SendEmailToApprover COPQ_SendEmail = new COPQ_SendEmailToApprover(); //class instance
                                await COPQ_SendEmail.SendEmailToSPV(Dashboard.SectionText.Replace("BIPH-", ""));
                            }

                            if (RoleDropDown.Text == "SPV")
                            {
                                foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                                {
                                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                                    {
                                        DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                        LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                        SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                        PartCode = row.Cells["Part Code"].Value.ToString();
                                        ApprovalType = TypeofApprovalDropdown.Text;
                                        DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

                                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionSPV");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by MGR");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                                        await UpdateApprovalStatus.ExecuteNonQueryAsync();
                                    }
                                }

                                MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                await GenerateMHData();
                                SelectAllChkBox.Checked = false;
                                //LoadApplyingApprovalData();

                                //Send email to MGR
                                COPQ_SendEmailToApprover COPQ_SendEmail = new COPQ_SendEmailToApprover(); //class instance
                                await COPQ_SendEmail.SendEmailToApplyingMGR(Dashboard.SectionText.Replace("BIPH-", ""));
                            }

                            if (RoleDropDown.Text == "MGR")
                            {
                                foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                                {
                                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                                    {
                                        DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                        LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                        SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                        PartCode = row.Cells["Part Code"].Value.ToString();
                                        ApprovalType = TypeofApprovalDropdown.Text;
                                        DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();
                                        ResponsibleSection = row.Cells["Responsible Section"].Value.ToString();

                                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMGR");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", ApprovalForm.DistinctionCode);
                                        //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                                        //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
                                        //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                                        await UpdateApprovalStatus.ExecuteNonQueryAsync();
                                    }
                                }


                                MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                COPQ_ApproveByMGR approveByMGR = new COPQ_ApproveByMGR();//instance
                                await approveByMGR.ApproveByApplyingMGR(ApprovalDataGrid);

                                await GenerateMHData();
                                SelectAllChkBox.Checked = false;
                                //LoadApplyingApprovalData();

                                //Send email to Receiving section COPQ PIC
                                COPQ_SendEmailToApprover COPQ_SendEmail = new COPQ_SendEmailToApprover(); //class instance
                                await COPQ_SendEmail.SendEmailToReceivingCOPQPIC(ResponsibleSection);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    finally
                    {
                        con.Close();  // Ensure the connection is closed
                    }

                }

                if (TypeofApprovalDropdown.Text == "Receiving")
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn))
                        {
                            await con.OpenAsync();

                            if (RoleDropDown.Text == "COPQ PIC")
                            {
                                // Asynchronous query using ExecuteReaderAsync
                                SqlCommand SelectProcessInchargeUser = new SqlCommand("SP_SelectProcessInchargeUsers", con);
                                SelectProcessInchargeUser.CommandType = CommandType.StoredProcedure;
                                SelectProcessInchargeUser.Parameters.AddWithValue("@UserSection", Dashboard.SectionText.Replace("BIPH-", ""));

                                SqlDataReader reader = await SelectProcessInchargeUser.ExecuteReaderAsync();

                                // Assuming the data returned will be loaded into a DataTable
                                DataTable dt = new DataTable();
                                dt.Load(reader); // Load the data asynchronously into the DataTable

                                reader.Close(); // Close the reader when done

                                if (dt.Rows.Count > 1)
                                {
                                    // Show the Process In-charge form if more than one record found
                                    ProcessInchargeForm processInChanrge = new ProcessInchargeForm();
                                    processInChanrge.ShowDialog();

                                    // After form submission
                                    SelectedProcessIncharge = ProcessInchargeForm.ProcessInCharge;
                                    SelectedApprovalType = TypeofApprovalDropdown.Text;

                                    if (!string.IsNullOrEmpty(SelectedProcessIncharge))
                                    {

                                        foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                                        {
                                            if (Convert.ToBoolean(row.Cells[0].Value) == true)
                                            {
                                                DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                                LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                                PartCode = row.Cells["Part Code"].Value.ToString();
                                                ApprovalType = TypeofApprovalDropdown.Text;
                                                DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();
                                                ResponsibleSection = row.Cells["Responsible Section"].Value.ToString();

                                                COPQ_ApproveReceivingCOPQPIC approveByReceivingCOPQPIC = new COPQ_ApproveReceivingCOPQPIC();
                                                await approveByReceivingCOPQPIC.ApproveByReceivingCOPQPIC(SelectedProcessIncharge);

                                            }
                                        }
                                    }

                                    // Generate the MH Data asynchronously
                                    await GenerateMHData();
                                    SelectAllChkBox.Checked = false;

                                    //Send email to Receiving section Process In - charge
                                    COPQ_SendEmailToApprover COPQ_SendEmail = new COPQ_SendEmailToApprover(); //class instance
                                    await COPQ_SendEmail.SendEmailToReceivingCOPQProcessInCharge(ResponsibleSection);

                                }

                                if (dt.Rows.Count == 1)
                                {
                                    // Only one result, handle directly
                                    SelectedApprovalType = TypeofApprovalDropdown.Text;

                                    foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                                    {
                                        if (Convert.ToBoolean(row.Cells[0].Value) == true)
                                        {
                                            DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                            LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                            PartCode = row.Cells["Part Code"].Value.ToString();
                                            ApprovalType = TypeofApprovalDropdown.Text;
                                            DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();
                                            ResponsibleSection = row.Cells["Responsible Section"].Value.ToString();

                                            COPQ_ApproveReceivingCOPQPIC approveByReceivingCOPQPIC = new COPQ_ApproveReceivingCOPQPIC();
                                            await approveByReceivingCOPQPIC.ApproveByReceivingCOPQPIC("Pending Approval");
                                        }
                                    }

                                    // Generate MH Data after approval
                                    await GenerateMHData();
                                    SelectAllChkBox.Checked = false;


                                    //Send email to Receiving section Process In - charge
                                    COPQ_SendEmailToApprover COPQ_SendEmail = new COPQ_SendEmailToApprover(); //class instance
                                    await COPQ_SendEmail.SendEmailToReceivingCOPQProcessInCharge(ResponsibleSection);
                                }
                            }


                            if (RoleDropDown.Text == "COPQ Process In-Charge")
                            {
                                SelectedApprovalType = TypeofApprovalDropdown.Text;

                                foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                                {
                                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                                    {
                                        DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                        LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                        SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                        PartCode = row.Cells["Part Code"].Value.ToString();
                                        ApprovalType = TypeofApprovalDropdown.Text;
                                        COPQAmount = row.Cells["COPQ Amount"].Value.ToString();
                                        DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

                                        ResponsibleSection = row.Cells["Responsible Section"].Value.ToString();

                                        Decimal LineStop = Convert.ToDecimal(row.Cells["COPQ Amount"].Value);

                                        try
                                        {
                                            if (Convert.ToDecimal(ApprovalForm.COPQAmount) >= 100 || LineStop >= 90)
                                            {
                                                ProcessInChargeConfirmationForm processInChargeConfirmationForm = new ProcessInChargeConfirmationForm();
                                                processInChargeConfirmationForm.ShowDialog();
                                            }
                                            else
                                            {
                                                // Prepare the SQL command for the stored procedure
                                                SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con)
                                                {
                                                    CommandType = CommandType.StoredProcedure
                                                };

                                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionCOPQProcessInCharge");
                                                UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
                                                UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                                                UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                                                UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalType);
                                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by SPV");
                                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "");
                                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                                // Execute the command asynchronously
                                                await UpdateApprovalStatus.ExecuteNonQueryAsync();
                                            }


                                            //AcceptButtonIsClicked = true;

                                        }
                                        catch (Exception ex)
                                        {
                                            // Handle any errors during the process
                                            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        finally
                                        {
                                            if (Convert.ToDecimal(ApprovalForm.COPQAmount) < 100)
                                            {
                                                MessageBox.Show("Approved Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                    }
                                }

                                //LoadReceivingApprovalData();
                                await GenerateMHData();
                                SelectAllChkBox.Checked = false;


                                //Send email to Receiving section SPV
                                COPQ_SendEmailToApprover COPQ_SendEmail = new COPQ_SendEmailToApprover(); //class instance
                                await COPQ_SendEmail.SendEmailToReceivingSPV(ResponsibleSection);

                            }


                            if (RoleDropDown.Text == "SPV")
                            {
                                try
                                {
                                    foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                                    {
                                        if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                                        {
                                            DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                            LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                            SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                            PartCode = row.Cells["Part Code"].Value.ToString();
                                            ApprovalType = TypeofApprovalDropdown.Text;
                                            DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();
                                            ResponsibleSection = row.Cells["Responsible Section"].Value.ToString();


                                            if (Dashboard.SectionText.Replace("BIPH-", "") == "Equipment Engineering")
                                            {
                                                SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBy_EESPV");
                                                UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
                                                //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);    
                                                UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                                                UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                                                //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
                                                //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
                                                UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalType);
                                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved");
                                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Automatic System Approved" + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                                                await UpdateApprovalStatus.ExecuteNonQueryAsync();
                                            }
                                            else
                                            {
                                                SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionSPV");
                                                UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
                                                //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
                                                UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                                                UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                                                //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
                                                //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
                                                UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalType);
                                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by MGR");
                                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                                                await UpdateApprovalStatus.ExecuteNonQueryAsync();

                                            }
                                        }
                                    }

                                    //AcceptButtonIsClicked = true;
                                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                                catch (Exception ex)
                                {
                                    // Handle any errors during the process
                                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                await GenerateMHData();

                                SelectAllChkBox.Checked = false;

                                if (Dashboard.SectionText.Replace("BIPH-", "") != "Equipment Engineering")
                                {
                                
                                    COPQ_SendEmailToApprover COPQ_SendEmail = new COPQ_SendEmailToApprover(); //class instance
                                    await COPQ_SendEmail.SendEmailToReceivingMGR(ResponsibleSection);
                                }
                            }


                            if (RoleDropDown.Text == "MGR")
                            {
                                try
                                {
                                    foreach (DataGridViewRow row in selectedRows)
                                    {
                                        if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                                        {
                                            DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                            LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                            SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                            PartCode = row.Cells["Part Code"].Value.ToString();
                                            ApprovalType = TypeofApprovalDropdown.Text;
                                            DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();


                                            SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                                            UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                            UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMGR");
                                            UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
                                            //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
                                            UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                                            UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                                            //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
                                            //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
                                            UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                                            UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved");
                                            UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
                                            UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                                            await UpdateApprovalStatus.ExecuteNonQueryAsync();

                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    // Handle any errors during the process
                                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }


                                COPQ_ApproveByMGR approveByMGR = new COPQ_ApproveByMGR();//instance
                                await approveByMGR.ApproveByReceivingMGR(ApprovalDataGrid);

                                await GenerateMHData();
                                SelectAllChkBox.Checked = false;

                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    finally
                    {
                        con.Close();  // Ensure the connection is closed
                    }

                    
                }

                if (TypeofApprovalDropdown.Text == "QI Confirmation")
                {
                    using (SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn))
                    {
                        await con.OpenAsync();

                        //Update QI Confirmation to Confirmed by Username, Date of approval
                        foreach (DataGridViewRow row in selectedRows)
                        {
                            if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                            {
                                DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                PartCode = row.Cells["Part Code"].Value.ToString();
                                ApprovalType = TypeofApprovalDropdown.Text;
                                DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();


                                try
                                {
                                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateQIConfirmationStatus", con);
                                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                    UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
                                    //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
                                    //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
                                    //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@ConfirmedBy", "Confirmed by: " + LoginForm.FirstName + " " + LoginForm.LastName + ", " + DateTime.Now.ToString());
                                    UpdateApprovalStatus.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error: {ex.Message}");
                                }
                                finally
                                {
                                    con.Close();  // Ensure the connection is closed
                                }

                            }
                        }

                        //AcceptButtonIsClicked = true;
                        MessageBox.Show("Confirmed Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //LoadReceivingApprovalData();
                        ExcludeEEData();

                        //GenerateMHData();
                        SelectAllChkBox.Checked = false;
                    }
                }
            }
        }

        private void UpdateProcessInChargeName()
        {
            // -> SQL query to insert user account
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand UpdateApprovalStatus = new SqlCommand("SP_ProcessInChargeName", con);
            UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            UpdateApprovalStatus.Parameters.AddWithValue("@ProcessInChargeName", ProcessInchargeForm.ProcessInCharge);
            UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
            //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetailTextBox.Text);
            //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
            //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
            UpdateApprovalStatus.ExecuteNonQuery();
            con.Close();
        }

        private void ExcludeEEData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (ExcludeCheckBox.Checked == true)
            {
                //Exclude EE data
                SqlCommand SelectForQIConfirmationData = new SqlCommand("SP_SelectForQIConfirmationData", con);
                SelectForQIConfirmationData.CommandType = CommandType.StoredProcedure;
                SelectForQIConfirmationData.Parameters.AddWithValue("@procedure", "ExcludeEEData");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Status", "For Confirmation");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Type", "QI Confirmation");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Role", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectForQIConfirmationData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApprovalDataGrid.DataSource = dt;
                con.Close();
            }
            else
            {
                //Include EE data
                SqlCommand SelectForQIConfirmationData = new SqlCommand("SP_SelectForQIConfirmationData", con);
                SelectForQIConfirmationData.CommandType = CommandType.StoredProcedure;
                // Set the timeout for the command
                SelectForQIConfirmationData.CommandTimeout = 60; // Timeout in seconds
                SelectForQIConfirmationData.Parameters.AddWithValue("@procedure", "IncludeEEData");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Status", "For Confirmation");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Type", "QI Confirmation");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Role", "QI");
                SqlDataAdapter sda = new SqlDataAdapter(SelectForQIConfirmationData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApprovalDataGrid.DataSource = dt;
                con.Close();
            }
        }

        private void ExcludeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //ExcludeEEData();
        }

        private void RoleDropDown_TextChanged(object sender, EventArgs e)
        {
            if (RoleDropDown.Text == "COPQ Process In-Charge")
            {
                TypeofApprovalDropdown.Text = "Receiving";
            }
            else
            {}
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SearchButton_MouseEnter(object sender, EventArgs e)
        {
            //SearchButton.BackColor = Color.FromArgb(21, 35, 53);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchRequestForApprovalData();
        }

        private void CategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (CategoryDropdown.Text == "ST")
            //{
            //    Dashboard.STCategoryIsSelected = true;
            //    CategoryDropdown.Text = "ST";
            //}
        }

        private void ApproveAllPendingBtn_Click(object sender, EventArgs e)
        {
            PendingForApprovalForm COPQPendingForApprovalForm = new PendingForApprovalForm();
            COPQPendingForApprovalForm.ShowDialog();
        }

        private void ApprovalDataGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            
        }

        private void GeneratePreviousQIForConfirmationBtn_Click(object sender, EventArgs e)
        {

            SelectAllChkBox.Visible = true; //Show select all checkbox
            ExcludeCheckBox.Location = new Point(120, 110); //Set location to new point
            ApprovalDataGrid.Columns["Select"].Visible = true; //Show checkbox colum

       
           
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (ExcludeCheckBox.Checked == true)
            {
                //Exclude EE data
                SqlCommand SelectForQIConfirmationData = new SqlCommand("SP_SelectForQIConfirmationData_Previous", con);
                SelectForQIConfirmationData.CommandType = CommandType.StoredProcedure;
                SelectForQIConfirmationData.Parameters.AddWithValue("@procedure", "ExcludeEEData");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Status", "For Confirmation");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Type", "QI Confirmation");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Role", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectForQIConfirmationData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApprovalDataGrid.DataSource = dt;
                con.Close();
            }
            else
            {
                //Include EE data
                SqlCommand SelectForQIConfirmationData = new SqlCommand("SP_SelectForQIConfirmationData_Previous", con);
                SelectForQIConfirmationData.CommandType = CommandType.StoredProcedure;
                SelectForQIConfirmationData.Parameters.AddWithValue("@procedure", "IncludeEEData");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Status", "For Confirmation");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Type", "QI Confirmation");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Role", "QI");
                SqlDataAdapter sda = new SqlDataAdapter(SelectForQIConfirmationData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApprovalDataGrid.DataSource = dt;
                con.Close();
            }

            ApprovalCount.Visible = false; // hide for approval count
        }

        private void RoleDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //====================================================================================================================>>>>>>>
    }
}
    

