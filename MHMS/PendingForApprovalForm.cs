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
using System.Windows.Forms;

namespace MHMS
{
    public partial class PendingForApprovalForm : Form
    {
        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public PendingForApprovalForm()
        {
            InitializeComponent();
        }

        private void COPQPendingForApprovalForm_Load(object sender, EventArgs e)
        {
           

            //Add checked box column
            AddCheckedBoxColumn();
        }

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
            LoadSection.Parameters.AddWithValue("@Procedure", "SelectAllSections");
            SqlDataAdapter sda = new SqlDataAdapter(LoadSection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadSection.ExecuteNonQuery();
            con.Close();

            SectionDropdown.DataSource = ds.Tables[0];
            SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            SectionDropdown.ValueMember = "Section";
        }// <---- end


        private void AddCheckedBoxColumn()
        {
            // Add checkbox column in datagrid
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "Select";
            checkColumn.HeaderText = "Select";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;
            checkColumn.FillWeight = 50; //if the datagridview is resized (on form resize) the checkbox won't take up too much; value is relative to the other columns' fill values
            PendingForApprovalDataGrid.Columns.Add(checkColumn);
            checkColumn.DisplayIndex = 0;
            checkColumn.Frozen = true;
            // <<----------
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            if (SectionDropdown.Text == "")
            {
                MessageBox.Show("Please select section.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (TypeDropdown.Text == "")
            {
                MessageBox.Show("Please select type.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {

                GenerateData();

            }

        }

        private void GenerateData()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectAllCOPQPendingApproval", con);
            SelectApprovalData.CommandType = CommandType.StoredProcedure;
            SelectApprovalData.Parameters.AddWithValue("@Type", TypeDropdown.Text);
            SelectApprovalData.Parameters.AddWithValue("@Section", SectionDropdown.Text);
            SelectApprovalData.Parameters.AddWithValue("@DateFrom", FromdateTimePicker.Value.ToString());
            SelectApprovalData.Parameters.AddWithValue("@DateTo", TodateTimePicker.Value.ToString());
            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            PendingForApprovalDataGrid.DataSource = dataTable;
            con.Close();

            if (dataTable.Rows.Count < 1)
            {
                MessageBox.Show("No data has been generated!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //}

            //SqlCommand SelectApprovalData = new SqlCommand("SP_SelectFilteredMHData", con);
            //SelectApprovalData.CommandType = CommandType.StoredProcedure;
            //SelectApprovalData.Parameters.AddWithValue("@Status", "For Approval");
            //SelectApprovalData.Parameters.AddWithValue("@Type", TypeDropdown.Text);
            //SelectApprovalData.Parameters.AddWithValue("@Role", RoleDropDown.Text);
            //SelectApprovalData.Parameters.AddWithValue("@Section", SectionDropdown.Text);
            //SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
            //DataTable dataTable = new DataTable();
            //sda.Fill(dataTable);
            //PendingForApprovalDataGrid.DataSource = dataTable;
            //con.Close();

            //if (dataTable.Rows.Count < 1)
            //{
            //    MessageBox.Show("No data has been generated!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void SectionDropdown_DropDown(object sender, EventArgs e)
        {
            LoadSection();
        }

        private void SectionDropdown_DropDownClosed(object sender, EventArgs e)
        {
            
        }


        private void CheckAllBtn_Click(object sender, EventArgs e)
        {
            if (CheckAllBtn.Text == "CHECK ALL")
            {
                foreach (DataGridViewRow row in PendingForApprovalDataGrid.Rows)
                {
                    row.Cells["Select"].Value = true;
                }

                CheckAllBtn.Text = "UNCHECK ALL";
                CheckAllBtn.BackColor = Color.FromArgb(201, 82, 58);
            }
            else if (CheckAllBtn.Text == "UNCHECK ALL")
            {
                foreach (DataGridViewRow row in PendingForApprovalDataGrid.Rows)
                {
                    row.Cells["Select"].Value = false;
                }

                CheckAllBtn.Text = "CHECK ALL";
                CheckAllBtn.BackColor = Color.FromArgb(46, 190, 118);
                
            }
        }

        public static string SelectedReferenceNo;
        public static string LineStopDetail;
        public static string PartCode;
        public static string COPQAmount;
        public static string DateEncountered;

        public static string SelectedRowReferenceNo;
        public static string SelectedLineStopDetail;
        public static string ApprovalType;
        public static string DistinctionCode;

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            //List<DataGridViewRow> selectedRows = (from row in PendingForApprovalDataGrid.Rows.Cast<DataGridViewRow>()
            //                                      where Convert.ToBoolean(row.Cells["Select"].Value) == true
            //                                      select row).ToList();

            //if (selectedRows.Count < 1 || selectedRows.Count == 0)
            //{
            //    MessageBox.Show("Please select data you want to approve!", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
            //else
            //{
            //    if (TypeDropdown.Text == "Applying")
            //    {
            //        if (con.State == ConnectionState.Closed)
            //        {
            //            con.Open();
            //        }

            //        if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
            //        {
            //            if (RoleDropDown.Text == "COPQ PIC")
            //            {
            //                foreach (DataGridViewRow row in PendingForApprovalDataGrid.Rows)
            //                {
            //                    DateEncountered = row.Cells["Date Encountered"].Value.ToString();
            //                    LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //                    PartCode = row.Cells["Part Code"].Value.ToString();
            //                    ApprovalType = TypeDropdown.Text;
            //                    DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

            //                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
            //                    {
            //                        //COPQConfirmationForm copqConfirmationForm = new COPQConfirmationForm();
            //                        //copqConfirmationForm.ShowDialog();


            //                        if (con.State == ConnectionState.Closed)
            //                        {
            //                            con.Open();
            //                        }

            //                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
            //                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionCOPQPIC");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
            //                        //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "Temporary Accepted");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "Man-Hour (Linestop)");
            //                        //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by SPV");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalType);
            //                        //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            //                        UpdateApprovalStatus.ExecuteNonQuery();
            //                        con.Close();

            //                        //UpdateMHLossType();
            //                        //UpdateReason();

            //                        //MHLossTypeDropdown.Text = "";
            //                        //ReasonTextBox.Text = "";

            //                        MessageBox.Show("Approved Successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //                    }
            //                }

            //                GenerateData();
            //                CheckAllBtn.Text = "CHECK ALL";
            //                CheckAllBtn.BackColor = Color.FromArgb(46, 190, 118);
            //            }
            //        }
            //        else if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
            //        {
            //            if (RoleDropDown.Text == "SPV")
            //            {
            //                foreach (DataGridViewRow row in PendingForApprovalDataGrid.Rows)
            //                {
            //                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
            //                    {
            //                        DateEncountered = row.Cells["Date Encountered"].Value.ToString();
            //                        LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //                        SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //                        PartCode = row.Cells["Part Code"].Value.ToString();
            //                        ApprovalType = TypeDropdown.Text;
            //                        DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

            //                        // -> SQL query to update approval status
            //                        if (con.State == ConnectionState.Closed)
            //                        {
            //                            con.Open();
            //                        }

            //                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
            //                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionSPV");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
            //                        //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
            //                        //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
            //                        //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeDropdown.Text);
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by MGR");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            //                        UpdateApprovalStatus.ExecuteNonQuery();
            //                        con.Close();

            //                    }
            //                }

            //                //AcceptButtonIsClicked = true;
            //                MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //                GenerateData();
            //                CheckAllBtn.Text = "CHECK ALL";
            //                CheckAllBtn.BackColor = Color.FromArgb(46, 190, 118);
            //            }
            //        }
            //        else if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
            //        {
            //            if (RoleDropDown.Text == "MGR")
            //            {
            //                foreach (DataGridViewRow row in PendingForApprovalDataGrid.Rows)
            //                {
            //                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
            //                    {
            //                        DateEncountered = row.Cells["Date Encountered"].Value.ToString();
            //                        LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //                        SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //                        PartCode = row.Cells["Part Code"].Value.ToString();
            //                        ApprovalType = TypeDropdown.Text;
            //                        DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

            //                        // -> SQL query to update approval status
            //                        if (con.State == ConnectionState.Closed)
            //                        {
            //                            con.Open();
            //                        }

            //                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
            //                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMGR");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
            //                        //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
            //                        //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
            //                        //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeDropdown.Text);
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            //                        UpdateApprovalStatus.ExecuteNonQuery();
            //                        con.Close();
            //                    }
            //                }

            //                //AcceptButtonIsClicked = true;
            //                MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //                GenerateData();
            //                CheckAllBtn.Text = "CHECK ALL";
            //                CheckAllBtn.BackColor = Color.FromArgb(46, 190, 118);
            //            }
            //        }
            //    }

            //    if (TypeDropdown.Text == "Receiving")
            //    {
            //        if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
            //        {
            //            if (RoleDropDown.Text == "COPQ PIC")
            //            {
            //                // -> SQL query to select process in-charge pic
            //                if (con.State == ConnectionState.Closed)
            //                {
            //                    con.Open();
            //                }

            //                SqlCommand SelectProcessInchargeUser = new SqlCommand("SP_SelectProcessInchargeUsers", con);
            //                SelectProcessInchargeUser.CommandType = CommandType.StoredProcedure;
            //                SelectProcessInchargeUser.Parameters.AddWithValue("@UserSection", Dashboard.SectionText.Replace("BIPH-", ""));
            //                SqlDataAdapter da = new SqlDataAdapter(SelectProcessInchargeUser);
            //                DataTable dt = new DataTable();
            //                da.Fill(dt);

            //                if (dt.Rows.Count > 1)
            //                {
            //                    foreach (DataGridViewRow row in PendingForApprovalDataGrid.Rows)
            //                    {

            //                        if ((Convert.ToBoolean(row.Cells[0].Value) == true))
            //                        {
            //                            DateEncountered = row.Cells["Date Encountered"].Value.ToString();
            //                            LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //                            PartCode = row.Cells["Part Code"].Value.ToString();
            //                            ApprovalType = TypeDropdown.Text;
            //                            DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

            //                            //Show Process in-charge form
            //                            ProcessInchargeForm processInChanrge = new ProcessInchargeForm();
            //                            processInChanrge.ShowDialog();

            //                            SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
            //                            UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionCOPQPIC");
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
            //                            //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
            //                            //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
            //                            //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@Reason", ""); //this an empty parameter to prevent error 
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");//this an empty parameter to prevent error 
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by COPQ Process In-Charge");
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalType);
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            //                            UpdateApprovalStatus.ExecuteNonQuery();
            //                            con.Close();

            //                            UpdateProcessInChargeName();

            //                            MessageBox.Show("Approved Successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                        }
            //                    }

            //                    GenerateData();
            //                    CheckAllBtn.Text = "CHECK ALL";
            //                    CheckAllBtn.BackColor = Color.FromArgb(46, 190, 118);
            //                }

            //                if (dt.Rows.Count == 1)
            //                {
            //                    foreach (DataGridViewRow row in PendingForApprovalDataGrid.Rows)
            //                    {
            //                        if ((Convert.ToBoolean(row.Cells[0].Value) == true))
            //                        {
            //                            DateEncountered = row.Cells["Date Encountered"].Value.ToString();
            //                            LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //                            SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //                            PartCode = row.Cells["Part Code"].Value.ToString();
            //                            ApprovalType = TypeDropdown.Text;
            //                            DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

            //                            // -> SQL query to update approval status
            //                            if (con.State == ConnectionState.Closed)
            //                            {
            //                                con.Open();
            //                            }

            //                            SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
            //                            UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionCOPQPIC");
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
            //                            //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
            //                            //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
            //                            //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeDropdown.Text);
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by COPQ Process In-Charge");
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            //                            UpdateApprovalStatus.ExecuteNonQuery();
            //                            con.Close();
            //                        }
            //                    }

            //                    UpdateProcessInChargeName();

            //                    //AcceptButtonIsClicked = true;
            //                    MessageBox.Show("Approved Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //                    //LoadReceivingApprovalData();
            //                    GenerateData();
            //                    CheckAllBtn.Text = "CHECK ALL";
            //                    CheckAllBtn.BackColor = Color.FromArgb(46, 190, 118);
            //                }
            //            }
            //        }

            //        if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
            //        {
            //            if (RoleDropDown.Text == "COPQ Process In-Charge")
            //            {
            //                foreach (DataGridViewRow row in PendingForApprovalDataGrid.Rows)
            //                {
            //                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
            //                    {
            //                        DateEncountered = row.Cells["Date Encountered"].Value.ToString();
            //                        LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //                        SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //                        PartCode = row.Cells["Part Code"].Value.ToString();
            //                        ApprovalType = TypeDropdown.Text;
            //                        COPQAmount = row.Cells["COPQ Amount"].Value.ToString();
            //                        DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

            //                        // -> SQL query to update approval status
            //                        if (con.State == ConnectionState.Closed)
            //                        {
            //                            con.Open();
            //                        }

            //                        if (Convert.ToDecimal(COPQAmount) >= 100)
            //                        {
            //                            ProcessInChargeConfirmationForm processInChargeConfirmationForm = new ProcessInChargeConfirmationForm();
            //                            processInChargeConfirmationForm.ShowDialog();
            //                        }
            //                        else
            //                        {
            //                            SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
            //                            UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionCOPQProcessInCharge");
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
            //                            //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
            //                            //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
            //                            //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeDropdown.Text);
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by SPV");
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "");
            //                            UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
            //                            UpdateApprovalStatus.ExecuteNonQuery();
            //                            con.Close();
            //                        }
            //                    }
            //                }

            //                //AcceptButtonIsClicked = true;
            //                MessageBox.Show("Approved Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //                //LoadReceivingApprovalData();
            //                GenerateData();
            //                CheckAllBtn.Text = "CHECK ALL";
            //                CheckAllBtn.BackColor = Color.FromArgb(46, 190, 118);
            //            }
            //        }

            //        if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
            //        {
            //            if (RoleDropDown.Text == "SPV")
            //            {
            //                foreach (DataGridViewRow row in PendingForApprovalDataGrid.Rows)
            //                {
            //                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
            //                    {
            //                        DateEncountered = row.Cells["Date Encountered"].Value.ToString();
            //                        LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //                        SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //                        PartCode = row.Cells["Part Code"].Value.ToString();
            //                        ApprovalType = TypeDropdown.Text;
            //                        DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

            //                        // -> SQL query to update approval status
            //                        if (con.State == ConnectionState.Closed)
            //                        {
            //                            con.Open();
            //                        }

            //                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
            //                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionSPV");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
            //                        //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
            //                        //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
            //                        //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeDropdown.Text);
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by MGR");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            //                        UpdateApprovalStatus.ExecuteNonQuery();
            //                        con.Close();


            //                    }
            //                }

            //                //AcceptButtonIsClicked = true;
            //                MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                //LoadReceivingApprovalData();
            //                GenerateData();
            //                CheckAllBtn.Text = "CHECK ALL";
            //                CheckAllBtn.BackColor = Color.FromArgb(46, 190, 118);
            //            }
            //        }

            //        if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
            //        {
            //            if (RoleDropDown.Text == "MGR")
            //            {
            //                foreach (DataGridViewRow row in selectedRows)
            //                {
            //                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
            //                    {
            //                        DateEncountered = row.Cells["Date Encountered"].Value.ToString();
            //                        LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //                        SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //                        PartCode = row.Cells["Part Code"].Value.ToString();
            //                        ApprovalType = TypeDropdown.Text;
            //                        DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

            //                        // -> SQL query to update approval status
            //                        if (con.State == ConnectionState.Closed)
            //                        {
            //                            con.Open();
            //                        }

            //                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
            //                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMGR");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
            //                        //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
            //                        //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
            //                        //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeDropdown.Text);
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
            //                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            //                        UpdateApprovalStatus.ExecuteNonQuery();
            //                        con.Close();

            //                    }
            //                }

            //                //AcceptButtonIsClicked = true;
            //                MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                //LoadReceivingApprovalData();
            //                GenerateData();
            //                CheckAllBtn.Text = "CHECK ALL";
            //                CheckAllBtn.BackColor = Color.FromArgb(46, 190, 118);
            //            }
            //        }
            //    }
            //    else if (TypeDropdown.Text == "QI Confirmation")
            //    {
            //        ////Update QI Confirmation to Confirmed by Username, Date of approval
            //        //foreach (DataGridViewRow row in selectedRows)
            //        //{
            //        //    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
            //        //    {
            //        //        DateEncountered = row.Cells["Date Encountered"].Value.ToString();
            //        //        LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //        //        SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
            //        //        PartCode = row.Cells["Part Code"].Value.ToString();
            //        //        ApprovalType = TypeDropdown.Text;

            //        //        // -> SQL query to update approval status
            //        //        if (con.State == ConnectionState.Closed)
            //        //        {
            //        //            con.Open();
            //        //        }

            //        //        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateQIConfirmationStatus", con);
            //        //        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            //        //        UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
            //        //        UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
            //        //        UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
            //        //        UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeDropdown.Text);
            //        //        UpdateApprovalStatus.Parameters.AddWithValue("@ConfirmedBy", "Confirmed by: " + LoginForm.FirstName + " " + LoginForm.LastName + ", " + DateTime.Now.ToString());
            //        //        UpdateApprovalStatus.ExecuteNonQuery();
            //        //        con.Close();

            //        //    }
            //        //}

            //        ////AcceptButtonIsClicked = true;
            //        //MessageBox.Show("Confirmed Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        ////LoadReceivingApprovalData();
            //        ////ExcludeEEData();

            //        ////GenerateMHData();
            //        //CheckAllBtn.Text = "CHECK ALL";
            //        //CheckAllBtn.BackColor = Color.FromArgb(46, 190, 118);

            //    }
            //}
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
            UpdateApprovalStatus.Parameters.AddWithValue("@ProcessInChargeName", "test lang muna"); // create standard name for section dummy account 
            UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
            UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
            UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
            UpdateApprovalStatus.ExecuteNonQuery();
            con.Close();
        }

        private void UpdateCOPQAmountIfNoCOPQNeeded()
        {
           
        }












    }
}
