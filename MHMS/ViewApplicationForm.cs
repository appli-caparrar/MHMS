using ClosedXML.Excel;
using MHMS.Connection;
using MHMS.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class ViewApplicationForm : Form
    {
        //Connection String
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2_ACTUAL"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);

        public ViewApplicationForm()
        {
            InitializeComponent();
        }

        private void ViewApplicationForm_Load(object sender, EventArgs e)
        {
            ReferenceNoLabel.Text = "<" + MHApproval.ReferenceNumber + ">";

            CategoryTxtBox.Text = MHApproval.Category;
            DateTimeAppliedTxtBox.Text = MHApproval.DateTimeApplied;
            AppliedByTxtBox.Text = MHApproval.AppliedBy;
            SectionTxtBox.Text = MHApproval.Section;

            SelectApplicationFormByReference();


            if (MHApproval.ApplicationFormType == "ST")
            {
                if (MHApproval.Category == "MH New ST Model List Form")
                {
                    //ViewApplicationDataGrid.Columns[1].Frozen = false;
                    ViewApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
                else
                {
                    //ViewApplicationDataGrid.Columns[1].Frozen = true;
                    ViewApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
            else if (MHApproval.ApplicationFormType == "WC/CC")
            {
                ViewApplicationDataGrid.Columns[1].Frozen = false;
                ViewApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else if (MHApproval.ApplicationFormType == "Open MH System")
            {
                //ViewApplicationDataGrid.Columns[2].Frozen = false;
                ViewApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void SelectApplicationFormByReference()
        {
            //try
            //{
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectApplicationFormByReference = new SqlCommand("SP_SelectApplicationFormByReference", con);
                SelectApplicationFormByReference.CommandType = CommandType.StoredProcedure;
                SelectApplicationFormByReference.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                SelectApplicationFormByReference.Parameters.AddWithValue("@Category", CategoryTxtBox.Text);
                SelectApplicationFormByReference.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                SqlDataAdapter da = new SqlDataAdapter(SelectApplicationFormByReference);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ViewApplicationDataGrid.DataSource = dt;
                con.Close();

                if (ViewApplicationDataGrid.Columns.Contains("ApplicationFormNo"))
                {
                    ViewApplicationDataGrid.Columns["ApplicationFormNo"].Visible = false;
                }
     
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}


         
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateAndTimeLabel.Text = dateTime.ToString("dddd , MMM dd yyyy, hh : mm : ss");

        }

        private void ReferenceNoLabel_Click(object sender, EventArgs e)
        {

        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            if (MHApproval.ApplicationFormType == "ST")
            {
                if (MHApproval.Approver == "Section MGR")
                {
                    if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        con.Open();
                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                        UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMGR");
                        UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                        UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration --> BPS MH PIC");
                        UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                        UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MH PIC");
                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "GM Approval Not Required");
                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                        UpdateApprovalStatus.ExecuteNonQuery();
                        con.Close();

                        //Send email
                        STApplicationEmailMessage_PEMHPIC();

                    }
                    else
                    {
                        con.Open();
                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                        UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMGR");
                        UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                        UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "2nd Approval --> Section GM");
                        UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                        UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "Section GM");
                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                        UpdateApprovalStatus.ExecuteNonQuery();
                        con.Close();

                        this.Close();

                        //Send email
                        STApplicationEmailMessage_SectionGM();

                        
                    }

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid

                }
                else if (MHApproval.Approver == "Section GM")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                        UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionGM->BILPIC");
                        UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                        UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "3rd Approval --> BIL PIC");
                        UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                        UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BIL PIC");
                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                        UpdateApprovalStatus.ExecuteNonQuery();
                        con.Close();

                        //Send email
                        STApplicationEmailMessage_BILPIC();

                    }
                    else
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                        UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionGM->BPSMHPIC");
                        UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                        UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration --> BPS MH PIC");
                        UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                        UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MH PIC");
                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                        UpdateApprovalStatus.ExecuteNonQuery();
                        con.Close();

                        //Send email
                        STApplicationEmailMessage_PEMHPIC();
                    }

                    this.Close();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid
                }
                else if (MHApproval.Approver == "BIL PIC")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBILPIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration --> BPS MH PIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MH PIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    this.Close();

                    //Send email
                    STApplicationEmailMessage_PEMHPIC();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid
                }
                else if (MHApproval.Approver == "BPS MH PIC")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMHPIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration Confirmation --> BPS MGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();
                      
                    this.Close();

                    //Send email
                    STApplicationEmailMessage_PEMGR();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid
                }
                else if (MHApproval.Approver == "BPS MGR")
                {
                    if (MHApproval.WithSAP == "Yes")
                    {
                        if (MHApproval.Section == "PCBA" || MHApproval.Section == "Molding Production")
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                            UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                            UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                            UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMGR");
                            UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                            UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                            UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "SAP Registration --> PProd PIC");
                            UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                            UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "PProd PIC");
                            UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                            UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                            UpdateApprovalStatus.ExecuteNonQuery();
                            con.Close();

                            //Send email
                            STApplicationEmailMessage_PProdPIC();
                        }
                        else
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                            UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                            UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                            UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMGR");
                            UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                            UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                            UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "SAP Registration --> PC PIC");
                            UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                            UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "PC PIC");
                            UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                            UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                            UpdateApprovalStatus.ExecuteNonQuery();
                            con.Close();

                            //Send email
                            STApplicationEmailMessage_PCPIC();
                        }

                    }
                    else
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                        UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMGR");
                        UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                        UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                        UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "APPROVED");
                        UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "APPROVED");
                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                        UpdateApprovalStatus.ExecuteNonQuery();
                        con.Close();

                        //Send email
                        STApplicationEmailMessage_SendToRequestor();

                    }

                    this.Close();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid
                }
                else if (MHApproval.Approver == "PC PIC")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByPCPIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "SAP Registration Confirmation --> PC MGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "PC MGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    this.Close();

                    //Send email
                    STApplicationEmailMessage_PCMGR();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid
                }
                else if (MHApproval.Approver == "PC MGR")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByPCMGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "APPROVED");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "APPROVED");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    this.Close();

                    //Send email
                    STApplicationEmailMessage_SendToRequestor();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid
                }
                else if (MHApproval.Approver == "PProd PIC")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByPProdPIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "SAP Registration Confirmation --> PProd MGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "PProd MGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    //Send email
                    STApplicationEmailMessage_PProdMGR();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid
                }
                else if (MHApproval.Approver == "PProd MGR")
                {
                   
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByPProdMGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "APPROVED");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "APPROVED");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    //Send email
                    STApplicationEmailMessage_SendToRequestor();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid
                }

            }
            else if (MHApproval.ApplicationFormType == "WC/CC")
            {
                if (MHApproval.Approver == "Section SPV")
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionSPV");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "2nd Approval --> Section MGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "Section MGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    //Send email
                    SendWCCCApplicationEmailMessage();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid


                }
                else if (MHApproval.Approver == "Section MGR")
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration --> BPS MH PIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MH PIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    //Send email
                    SendWCCCApplicationEmailMessage();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid

                }
                else if (MHApproval.Approver == "BPS MH PIC")
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMHPIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration Confirmation --> BPS MGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    //Send email
                    SendWCCCApplicationEmailMessage();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid

                }
                else if (MHApproval.Approver == "BPS MGR")
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "APPROVED");
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "APPROVED");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "APPROVED");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    //Send email
                    SendWCCCApplicationEmailMessage();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid
                }
            }
            else if (MHApproval.ApplicationFormType == "Open MH System")
            {
                if (MHApproval.Approver == "Section MGR")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "2nd Approval --> Section GM");
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "Section GM");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    //Send email
                    OpenMHApplicationEmailMessage();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid
                }
                else if (MHApproval.Approver == "Section GM")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionGM");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration --> BPS MH PIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MH PIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    //Send email
                    OpenMHApplicationEmailMessage();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid
                }
                else if (MHApproval.Approver == "BPS MH PIC Registration")
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMHPIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration Confirmation --> BPS MGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    //Send email
                    OpenMHApplicationEmailMessage();


                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    this.Close();

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid
                }
                else if (MHApproval.Approver == "BPS MGR")
                {
                    

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMGR");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Closing Information --> Section MH PIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "Section MH PIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    //Send email
                    OpenMHApplicationEmailMessage();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid
                }
                else if (MHApproval.Approver == "Section MH PIC")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMHPIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");  
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Closing Confirmation --> BPS MH PIC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MH PIC Confirmation");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    //Send email
                    OpenMHApplicationEmailMessage();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid
                }
                else if (MHApproval.Approver == "BPS MH PIC Confirmation")
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMHPICConfirmation");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved " + DateTime.Now.ToString());
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "APPROVED");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "APPROVED");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    //Send email
                    OpenMHApplicationEmailMessage();

                    MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();

                    MHApproval.IsApproveSuccess = true; //Refresh MH approval datagrid
                }
            }
        }

        private void ViewApplicationDataGrid_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in ViewApplicationDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (MHApproval.ApplicationFormType == "ST")
            {
                if (CategoryTxtBox.Text == "MH New ST Model List Form")
                {
                    ViewApplicationDataGrid.Columns["SAP ST(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ViewApplicationDataGrid.Columns["SAP TT(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ViewApplicationDataGrid.Columns["MH ST(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ViewApplicationDataGrid.Columns["MH TT(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            /*    ExportMHData();*/
            ExportCurrenMonthData();
        }

        private void ExportCurrenMonthData()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SP_SelectApplicationFormByReference", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 100;
                cmd.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                cmd.Parameters.AddWithValue("@Category", CategoryTxtBox.Text);
                cmd.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

            }

            if (dt.Rows.Count > 0)
            {
                DateTime currentMonth = DateTime.Now;
                string currentMonthName = currentMonth.ToString("MMMM yyyy");
                string currentMonthFile = currentMonth.ToString("yyyyMM");

                using (SaveFileDialog sfd = new SaveFileDialog()
                {
                    Filter = "Excel Workbook|*.xlsx",
                    Title = $"{CategoryTxtBox.Text} - {currentMonthName}",
                    FileName = $"{SectionTxtBox.Text}_{CategoryTxtBox.Text}_{currentMonthFile}.xlsx"
                })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string sheetName = CategoryTxtBox.Text;

                        // Replace invalid characters
                        foreach (char c in new[] { '\\', '/', '?', '*', '[', ']', ':' })
                        {
                            sheetName = sheetName.Replace(c, '-');
                        }

                        // Limit to 31 characters (Excel max)
                        if (sheetName.Length > 31)
                            sheetName = sheetName.Substring(0, 31);

                        // Ensure not empty
                        if (string.IsNullOrWhiteSpace(sheetName))
                            sheetName = "Sheet1";

                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt, sheetName); // ✅ use sanitized sheet name here
                            wb.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Export successful!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("No data found to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void copyAlltoClipboardsss()
        {
            ViewApplicationDataGrid.SelectAll();
            //Copy to clipboard 
            ViewApplicationDataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = ViewApplicationDataGrid.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void ExportMHData()
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
            CR.Select();
            xlWorkSheet.Cells.NumberFormat = "@";


            xlWorkSheet.PasteSpecial(CR, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

        }

        string innerString;
        //string FirstName;
        //string LastName;
        //string Email;

        //===================================================<break>======================================================//

        private void SendWCCCApplicationEmailMessage()
        {
            if (MHApproval.Approver == "Section SPV")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();


                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                    //foreach (DataRow row in dTable.Rows)
                    //{
                    //    FirstName = row["First Name"].ToString();
                    //    LastName = row["Last Name"].ToString();
                    //    Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();

                        builder.Append("Dear " + Dashboard.SectionText.Replace("BIPH-", "").Replace("BIPH-", "") + " Section MGR,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        if (MHApproval.Category == "Work Center New")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is a request for Deletion of Cost Center for " + MHApproval.Section + ".");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is a request for Deletion of Cost Center for " + MHApproval.Section + ".");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is a request for Deletion of Cost Center for " + MHApproval.Section + ".");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is a request for Deletion of Cost Center for " + MHApproval.Section + ".");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is a request for Deletion of Cost Center for " + MHApproval.Section + ".");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is a request for Deletion of Cost Center for " + MHApproval.Section + ".");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is a request for Deletion of Cost Center for " + MHApproval.Section + ".");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is a request for Deletion of Cost Center for " + MHApproval.Section + ".");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Work Center Revision")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Work Center Deletion")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                            
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Cost Center New")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                            
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Cost Center Revision")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                               
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Cost Center Deletion")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                          
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }


                        builder.Append("<b>For your approval.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>承認待ち</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + MHApproval.ReferenceNumber + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");

                        builder.Append("In case a problem occurred in the application file, kindly inform the mailing list below.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>申請ファイルに問題が生じた場合は、下記のメーリングリストに連絡下さい</font>");
                        builder.Append("<br><br>");

                        builder.Append("<b>PM Group Mailing List</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>PM Gメーリングリスト</font>");
                        builder.Append("<br>");
                        builder.Append("Bautista, Princess (BIPH-BPS) <princess.bautista@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Tan, Lina (BIPH-BPS) <lina.tan@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Mateo, Bradly (BIPH-BPS) <bradly.mateo@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Dimayuga, Jeancy (BIPH-BPS) <jeancy.dimayuga@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Estacio, Dianelle Yasdane (BIPH-BPS) <dianelleyasdane.estacio@brother-biph.com.ph>");
                        builder.Append("<br><br>");

                        builder.Append("Thanks and Best Regards.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>ご尽力頂きありがとうございます。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            mail.CC.Add(EmailListCC);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "FY." + CurrentYear + ": " + MHApproval.Section + " section's " + MHApproval.Category + " Application form.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    //}

                }
            }
            else if (MHApproval.Approver == "Section MGR")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                    //foreach (DataRow row in dTable.Rows)
                    //{
                    //    FirstName = row["First Name"].ToString();
                    //    LastName = row["Last Name"].ToString();
                    //    Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();
                       

                        builder.Append("Dear BPS MH PIC,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        if (MHApproval.Category == "Work Center New")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                               
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課の新規ワークセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課の新規ワークセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課の新規ワークセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課の新規ワークセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }

                        }
                        else if (MHApproval.Category == "Work Center Revision")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                           
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }

                        }
                        else if (MHApproval.Category == "Work Center Deletion")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のワークセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のワークセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");

                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のワークセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                             
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のワークセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のワークセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のワークセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のワークセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Cost Center New")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課の新規コストセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課の新規コストセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");

                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課の新規コストセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課の新規コストセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課の新規コストセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課の新規コストセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Cost Center Revision")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のコストセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のコストセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");

                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のコストセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                               
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のコストセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のコストセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のコストセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のコストセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }

                        }
                        else if (MHApproval.Category == "Cost Center Deletion")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のコストセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");

                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のコストセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                             
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のコストセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のコストセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のコストセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のコストセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }

                        builder.Append("<b>For your approval.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>承認をお願い致します。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + MHApproval.ReferenceNumber + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");

                        builder.Append("In case a problem occurred in the application file, kindly inform the mailing list below.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>申請ファイルに不具合が発生した場合には、下記のメーリングリストにご連絡下さい。</font>");
                        builder.Append("<br><br>");

                        builder.Append("<b>PM Group Mailing List</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>PM Gメーリングリスト</font>");
                        builder.Append("<br>");
                        builder.Append("Bautista, Princess (BIPH-BPS) <princess.bautista@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Tan, Lina (BIPH-BPS) <lina.tan@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Mateo, Bradly (BIPH-BPS) <bradly.mateo@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Dimayuga, Jeancy (BIPH-BPS) <jeancy.dimayuga@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Estacio, Dianelle Yasdane (BIPH-BPS) <dianelleyasdane.estacio@brother-biph.com.ph>");
                        builder.Append("<br><br>");

                        builder.Append("Thanks and Best Regards.");
                        builder.Append("<font color=blue>宜しくお願い致します。</font>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動配信されています]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            mail.CC.Add(EmailListCC);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "FY." + CurrentYear + ": " + MHApproval.Section + " section's " + MHApproval.Category + " Application form.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    //}

                }
            }
            else if (MHApproval.Approver == "BPS MH PIC")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BPSMGR");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string FirstNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["First Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                    //foreach (DataRow row in dTable.Rows)
                    //{
                    //    FirstName = row["First Name"].ToString();
                    //    LastName = row["Last Name"].ToString();
                    //    Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear BPS MGR,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        if (MHApproval.Category == "Work Center New")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課の新規ワークセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");

                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                           
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課の新規ワークセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課の新規ワークセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課の新規ワークセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課の新規ワークセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Work Center Revision")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");

                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }

                        }
                        else if (MHApproval.Category == "Work Center Deletion")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のワークセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");

                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のワークセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                              
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のワークセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のワークセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のワークセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Cost Center New")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課の新規コストセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                            
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課の新規コストセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課の新規コストセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課の新規コストセンター登録申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Cost Center Revision")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のコストセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");

                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のコストセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                              
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のコストセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のコストセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のコストセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のコストセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }

                        }
                        else if (MHApproval.Category == "Cost Center Deletion")
                        {
                            if (MHApproval.Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のコストセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のコストセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");

                            }
                            else if (MHApproval.Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のコストセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                           
                            }
                            else if (MHApproval.Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のコストセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のコストセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のコストセンターの削除申請が承認された事をお知らせします。</font>"); 
                                builder.Append("<br>");
                            }
                            else if (MHApproval.Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }

                        builder.Append("<b>For your approval.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>承認をお願い致します。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + MHApproval.ReferenceNumber + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");

                        builder.Append("In case a problem occurred in the application file, kindly inform the mailing list below.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>申請ファイルに不具合が発生した場合には、下記のメーリングリストにご連絡下さい。</font>");
                        builder.Append("<br><br>");

                        builder.Append("<b>PM Group Mailing List</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>PM Gメーリングリスト</font>");
                        builder.Append("<br>");
                        builder.Append("Bautista, Princess (BIPH-BPS) <princess.bautista@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Tan, Lina (BIPH-BPS) <lina.tan@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Mateo, Bradly (BIPH-BPS) <bradly.mateo@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Dimayuga, Jeancy (BIPH-BPS) <jeancy.dimayuga@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Estacio, Dianelle Yasdane (BIPH-BPS) <dianelleyasdane.estacio@brother-biph.com.ph>");
                        builder.Append("<br><br>");

                        builder.Append("Thanks and Best Regards.");
                        builder.Append("<font color=blue>宜しくお願い致します。</font>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動配信されています]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            mail.CC.Add(EmailListCC);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "FY." + CurrentYear + ": " + MHApproval.Section + " section's " + MHApproval.Category + " Application form.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    //}

                }
            }
            else if (MHApproval.Approver == "BPS MGR")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                //SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                //SelectUsersAccount.Parameters.AddWithValue("@Procedure", "PEMGR");
                //SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                //SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                //SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                //DataTable dTable = new DataTable();
                //sda.Fill(dTable);
                //con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                if (dTable2.Rows.Count > 0)
                {
                    //string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    //string EmailListTo = String.Join("; ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                    //foreach (DataRow row in dTable2.Rows)
                    //{
                    //    FirstName = row["First Name"].ToString();
                    //    LastName = row["Last Name"].ToString();
                    //    Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();

                        builder.Append("Dear All");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Registration in MH system is already done");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>MHシステム登録が完了しました</font>");
                        builder.Append("<br>");
                        builder.Append("<b>For your checking/verification.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>ご確認・検証お願い致します。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + MHApproval.ReferenceNumber + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");

                        builder.Append("In case a problem occurred in the application file, kindly inform the mailing list below.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>申請ファイルに不具合が発生した場合には、下記のメーリングリストにご連絡下さい。</font>");
                        builder.Append("<br><br>");

                        builder.Append("<b>PM Group Mailing List</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>PM Gメーリングリスト</font>");
                        builder.Append("<br>");
                        builder.Append("Bautista, Princess (BIPH-BPS) <princess.bautista@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Tan, Lina (BIPH-BPS) <lina.tan@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Mateo, Bradly (BIPH-BPS) <bradly.mateo@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Dimayuga, Jeancy (BIPH-BPS) <jeancy.dimayuga@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Estacio, Dianelle Yasdane (BIPH-BPS) <dianelleyasdane.estacio@brother-biph.com.ph>");
                        builder.Append("<br><br>");

                        builder.Append("Thanks and Best Regards.");
                        builder.Append("<font color=blue>宜しくお願い致します。</font>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動配信されています]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            //mail.CC.Add(EmailListCC);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "FY." + CurrentYear + ": " + MHApproval.Section + " section's " + MHApproval.Category + " Application form.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    //}
                }
            }
        }

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SearchApplicationFormByReference = new SqlCommand("SP_SearchApplicationFormByReference", con);
                SearchApplicationFormByReference.CommandType = CommandType.StoredProcedure;
                SearchApplicationFormByReference.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                SearchApplicationFormByReference.Parameters.AddWithValue("@Category", MHApproval.Category);
                SearchApplicationFormByReference.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                SearchApplicationFormByReference.Parameters.AddWithValue("@Search", SearchBox.Text);
                SqlDataAdapter da = new SqlDataAdapter(SearchApplicationFormByReference);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ViewApplicationDataGrid.DataSource = dt;
                con.Close();

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }


        //ST section GM Email
        private void STApplicationEmailMessage_SectionGM()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionGM");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();


            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                //foreach (DataRow row in dTable.Rows)
                //{
                //    FirstName = row["First Name"].ToString();
                //    LastName = row["Last Name"].ToString();
                //    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();

                    builder.Append("Dear " + Dashboard.SectionText.Replace("BIPH-", "").Replace("BIPH-", "") + " Section GM,");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                         
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                    
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                      
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }


                    }


                    //====================>>>>>>>>


                    builder.Append("<br>");

                    builder.Append("For your checking and approval");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>確認と承認をお願いします</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(MHApproval.ReferenceNumber);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + MHApproval.Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                //}

            }

        }

        //ST BIL PIC Email
        private void STApplicationEmailMessage_BILPIC()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BILPIC");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();


            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();


            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                //foreach (DataRow row in dTable.Rows)
                //{
                //    FirstName = row["First Name"].ToString();
                //    LastName = row["Last Name"].ToString();
                //    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();


                    builder.Append("Dear " + MHApproval.Section + " BIL PIC,");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                       
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }



                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                         
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                      
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }


                    }


                    //====================>>>>>>>>


                    builder.Append("<br>");

                    builder.Append("For your checking and approval");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>確認と承認をお願いします</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(MHApproval.ReferenceNumber);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + MHApproval.Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                //}

            }

        }

        //ST PE MH PIC Email
        private void STApplicationEmailMessage_PEMHPIC()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();


            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();


            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                //foreach (DataRow row in dTable.Rows)
                //{
                //    FirstName = row["First Name"].ToString();
                //    LastName = row["Last Name"].ToString();
                //    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();

                    builder.Append("Dear BPS MH PICs,");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>
                    //for revise 06/07/2023
                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                    
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }



                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
    
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }

                    }


                    //====================>>>>>>>>


                    builder.Append("<br>");

                    builder.Append("For your checking and approval");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>確認と承認をお願いします</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(MHApproval.ReferenceNumber);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + MHApproval.Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                //}

            }

        }

        //ST PE MGR Email
        private void STApplicationEmailMessage_PEMGR()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BPSMGR");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();
              
            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                //foreach (DataRow row in dTable.Rows)
                //{
                //    FirstName = row["First Name"].ToString();
                //    LastName = row["Last Name"].ToString();
                //    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();
                  

                    builder.Append("Dear BPS MGR");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (MHApproval.Section == "Printer 1")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        if (MHApproval.Section == "Printer 2")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }



                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }


                    }


                //====================>>>>>>>>

                    builder.Append("<br>");

                    builder.Append("Registration in MH system is already done");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>MHシステムへの登録は完了済です。</font>");
                    builder.Append("<br><br>");


                    builder.Append("For your checking and approval");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>確認と承認をお願いします</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(MHApproval.ReferenceNumber);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + MHApproval.Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                //}

            }

        }

        //ST Requestor
        private void STApplicationEmailMessage_SendToRequestor()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionRequestor");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                //CC to approver -->> Ongoing
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                //foreach (DataRow row in dTable.Rows)
                //{
                //    FirstName = row["First Name"].ToString();
                //    LastName = row["Last Name"].ToString();
                //    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();

                    builder.Append("Dear " + MHApproval.Section + " MH PIC,");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }



                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }

                    }


                    //====================>>>>>>>>


                    builder.Append("<br>");
                    builder.Append("Registration in MH system is already done");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>MHシステム登録が完了しました</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(MHApproval.ReferenceNumber);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        //mail.CC.Add(EmailListCC);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + MHApproval.Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                //}

            }

        }

        //ST PProd PIC Email
        private void STApplicationEmailMessage_PProdPIC()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "PProdPIC");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();


            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                //foreach (DataRow row in dTable.Rows)
                //{
                //    FirstName = row["First Name"].ToString();
                //    LastName = row["Last Name"].ToString();
                //    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();
                   
                    builder.Append("Dear All,");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }



                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }

                    }


                    //====================>>>>>>>>


                    builder.Append("<br>");

                    builder.Append("For SAP system registration");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>SAPシステム登録用</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(MHApproval.ReferenceNumber);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + MHApproval.Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                //}

            }

        }

        //ST PC PIC Email
        private void STApplicationEmailMessage_PCPIC()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "PCPIC");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();


            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                //foreach (DataRow row in dTable.Rows)
                //{
                //    FirstName = row["First Name"].ToString();
                //    LastName = row["Last Name"].ToString();
                //    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();
                    

                    builder.Append("Dear All,");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }


                    }


                    //====================>>>>>>>>


                    builder.Append("<br>");

                    builder.Append("For SAP system registration");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>SAPシステム登録用</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(MHApproval.Section);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + MHApproval.Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                //}

            }

        }

        //ST PC MGR Email
        private void STApplicationEmailMessage_PCMGR()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "PCMGR");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();


            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                //foreach (DataRow row in dTable.Rows)
                //{
                //    FirstName = row["First Name"].ToString();
                //    LastName = row["Last Name"].ToString();
                //    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();
                   

                    builder.Append("Dear All");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }



                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }



                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }


                    }


                    //====================>>>>>>>>


                    builder.Append("<br>");

                    builder.Append("For your checking and approval");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>確認と承認をお願いします</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(MHApproval.ReferenceNumber);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + MHApproval.Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                //}

            }

        }

        //ST PProd MGR Email
        private void STApplicationEmailMessage_PProdMGR()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "PProdMGR");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();



            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                //foreach (DataRow row in dTable.Rows)
                //{
                //    FirstName = row["First Name"].ToString();
                //    LastName = row["Last Name"].ToString();
                //    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();


                    builder.Append("Dear All,");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }


                    }


                    //====================>>>>>>>>


                    builder.Append("<br>");

                    builder.Append("For your checking and approval");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>確認と承認をお願いします</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(MHApproval.ReferenceNumber);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + MHApproval.Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                //}

            }

        }

        //Open MH Email notification
        private void OpenMHApplicationEmailMessage()
        {
            if (MHApproval.Approver == "Section MGR")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionGM");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();

                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();

                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                    //foreach (DataRow row in dTable.Rows)
                    //{
                    //    FirstName = row["First Name"].ToString();
                    //    LastName = row["Last Name"].ToString();
                    //    Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();
                       

                        builder.Append("Dear " + Dashboard.SectionText.Replace("BIPH - ", "").Replace("BIPH-", "") + " Section GM,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");



                        if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジセクションの承認済みOpen MHシステムリクエストフォームについては、以下のリンクを参照してください。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。/font>");
                            builder.Append("<br>");

                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP-タッチ課のMHシステム編集解除許可（OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                     
                        }
                        else if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてプリンター課のMHシステム編集解除許可 (OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            //builder.Append("<br>");
                        }


                        builder.Append("<b>For your checking and approval.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>確認・承認待ち</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + MHApproval.ReferenceNumber + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");


                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            mail.CC.Add(EmailListCC);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + MHApproval.Section + " section's Open MH request.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    //}

                }
            }
            else if (MHApproval.Approver == "Section GM")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();

                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();

                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());


                    //foreach (DataRow row in dTable.Rows)
                    //{
                    //    FirstName = row["First Name"].ToString();
                    //    LastName = row["Last Name"].ToString();
                    //    Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();
                       

                        builder.Append("Dear BPS MH PIC,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");


                        if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジセクションの承認済みOpen MHシステムリクエストフォームについては、以下のリンクを参照してください。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。/font>");
                            builder.Append("<br>");

                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP-タッチ課のMHシステム編集解除許可（OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                          
                        }
                        else if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてプリンター課のMHシステム編集解除許可 (OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            //builder.Append("<br>");
                        }


                        builder.Append("<b>Kindly proceed to Open MH system.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>MHシステムを開き、編集に進んで下さい。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + MHApproval.ReferenceNumber + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");


                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            mail.CC.Add(EmailListCC);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + MHApproval.Section + " section's Open MH request.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    //}

                }
            }
            else if (MHApproval.Approver == "BPS MH PIC Registration")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BPSMGR");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();


                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();

                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();

                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                    //foreach (DataRow row in dTable.Rows)
                    //{
                    //    FirstName = row["First Name"].ToString();
                    //    LastName = row["Last Name"].ToString();
                    //    Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();
                       

                        builder.Append("Dear BPS MGR,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");


                        if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジセクションの承認済みOpen MHシステムリクエストフォームについては、以下のリンクを参照してください。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。/font>");
                            builder.Append("<br>");

                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP-タッチ課のMHシステム編集解除許可（OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                         
                        }
                        else if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてプリンター課のMHシステム編集解除許可 (OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            //builder.Append("<br>");
                        }


                        builder.Append("<b>For your checking and approval.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>確認・承認待ち</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + MHApproval.ReferenceNumber + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");


                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            mail.CC.Add(EmailListCC);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + MHApproval.Section + " section's Open MH request.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    //}
                }
            }
            else if (MHApproval.Approver == "BPS MGR")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                //SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                //SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                //SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                //SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                //SelectUsersAccount2.Parameters.AddWithValue("@Section", Section);
                //SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                //DataTable dTable2 = new DataTable();
                //sda2.Fill(dTable2);
                //con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();


                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();

                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    //string EmailListCC = String.Join("; ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                    //foreach (DataRow row in dTable.Rows)
                    //{
                    //    FirstName = row["First Name"].ToString();
                    //    LastName = row["Last Name"].ToString();
                    //    Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();

                        builder.Append("Dear " + MHApproval.Section + " MH PIC,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("MH system for the month of " + MHApproval.MonthToOpen + " is already open.");
                        builder.Append("<br>");
                        //builder.Append("<font color=blue>インクカートリッジ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                        builder.Append("<br>");

                        builder.Append("<b>For your checking and approval.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>確認・承認待ち</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + MHApproval.ReferenceNumber + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            //mail.CC.Add(EmailListCC);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + MHApproval.Section + " section's Open MH request.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    //}
                }
            }
            else if (MHApproval.Approver == "Section MH PIC")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();

                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();

                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                    //foreach (DataRow row in dTable.Rows)
                    //{
                    //    FirstName = row["First Name"].ToString();
                    //    LastName = row["Last Name"].ToString();
                    //    Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();
                       

                        builder.Append("Dear BPS MH PIC,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");


                        builder.Append("Revision in MH system is already done.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>MHシステムでの改訂が完了しました。</font>");
                        builder.Append("<br>");

                        builder.Append("<b><font color=green>MH system can now be close.</font></b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>MHシステムは編集締結（クローズ）可能です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + MHApproval.ReferenceNumber + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            mail.CC.Add(EmailListCC);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + MHApproval.Section + " section's Open MH request.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    //}

                }
            }
            else if (MHApproval.Approver == "BPS MH PIC Confirmation")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                //SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                //SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                //SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                //SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                //SelectUsersAccount2.Parameters.AddWithValue("@Section", Section);
                //SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                //DataTable dTable2 = new DataTable();
                //sda2.Fill(dTable2);
                //con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();

                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();


                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    //string EmailListCC = String.Join("; ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                    //foreach (DataRow row in dTable.Rows)
                    //{
                    //    FirstName = row["First Name"].ToString();
                    //    LastName = row["Last Name"].ToString();
                    //    Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();
                      

                        builder.Append("Dear All,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");


                        builder.Append("MH system for the month of " + MHApproval.MonthToOpen + " is already closed.");
                        builder.Append("<br>");
                        //builder.Append("<font color=blue>インクカートリッジ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                        builder.Append("<br>");

                        builder.Append("<b>Kindly start to revise your input</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>入力の改訂を開始して下さい。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + MHApproval.ReferenceNumber + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            //mail.cc.add(emaillistcc);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + MHApproval.Section + " section's Open MH request.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    //}

                }
            }

        }


        private void RejectButton_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to reject this application?", "MHMS Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                MHReasonOfRejection rejectionForm = new MHReasonOfRejection();
                rejectionForm.ShowDialog();
            }
        }

        private void TopPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
