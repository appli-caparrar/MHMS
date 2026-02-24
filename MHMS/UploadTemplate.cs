using ExcelDataReader;
using MHMS.Class;
using MHMS.Connection;
using MHMS.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z.Dapper.Plus;

namespace MHMS
{
    public partial class UploadTemplate : Form
    {

        //Connection String
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2_ACTUAL"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);
        string conn = "Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;";

        public UploadTemplate()
        {
            InitializeComponent();
        }

        //Table collection
        DataTableCollection tableCollection;

        private void UploadSTTemplate_Load(object sender, EventArgs e)
        {
            AddCategoryPerApplication();
            CategoryDropdown.Text = ApplicationForm.Category;

            if (ApplicationForm.ApplicationFormType == "ST")
            {
                WithSAPRadioButton.Enabled = true;
                NoSAPRadioButton.Enabled = true;
            }
            else
            {
                WithSAPRadioButton.Enabled = false;
                NoSAPRadioButton.Enabled = false;
            }

           
        }

        private void AddCategoryPerApplication()
        {
            if (ApplicationForm.ApplicationFormType == "COPQ")
            {

            }
            else if (ApplicationForm.ApplicationFormType == "ST")
            {
                //Remove
                CategoryDropdown.Items.Remove("Annual ST Change");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form - No BIL Approval");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form");
                CategoryDropdown.Items.Remove("MH New ST Model List Form");

                //Remove
                CategoryDropdown.Items.Remove("Work Center New");
                CategoryDropdown.Items.Remove("Work Center Revision");
                CategoryDropdown.Items.Remove("Work Center Deletion");
                CategoryDropdown.Items.Remove("Cost Center New");
                CategoryDropdown.Items.Remove("Cost Center Revision");
                CategoryDropdown.Items.Remove("Cost Center Deletion");

                CategoryDropdown.Items.Remove("Manpower/Man-hour");
                CategoryDropdown.Items.Remove("Standard Time (ST mins)");
                CategoryDropdown.Items.Remove("Linestop/Loss Man-hour/Loss Factor");

                //Add
                CategoryDropdown.Items.Add("Annual ST Change");
                CategoryDropdown.Items.Add("MH Change ST Model List Form - No BIL Approval");
                CategoryDropdown.Items.Add("MH Change ST Model List Form");
                CategoryDropdown.Items.Add("MH New ST Model List Form");
            }
            else if (ApplicationForm.ApplicationFormType == "WC/CC")
            {
                //Remove
                CategoryDropdown.Items.Remove("Work Center New");
                CategoryDropdown.Items.Remove("Work Center Revision");
                CategoryDropdown.Items.Remove("Work Center Deletion");
                CategoryDropdown.Items.Remove("Cost Center New");
                CategoryDropdown.Items.Remove("Cost Center Revision");
                CategoryDropdown.Items.Remove("Cost Center Deletion");

                //Remove
                CategoryDropdown.Items.Remove("Annual ST Change");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form - No BIL Approval");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form");
                CategoryDropdown.Items.Remove("MH New ST Model List Form");

                CategoryDropdown.Items.Remove("Manpower/Man-hour");
                CategoryDropdown.Items.Remove("Standard Time (ST mins)");
                CategoryDropdown.Items.Remove("Linestop/Loss Man-hour/Loss Factor");

                //Add
                CategoryDropdown.Items.Add("Work Center New");
                CategoryDropdown.Items.Add("Work Center Revision");
                CategoryDropdown.Items.Add("Work Center Deletion");
                CategoryDropdown.Items.Add("Cost Center New");
                CategoryDropdown.Items.Add("Cost Center Revision");
                CategoryDropdown.Items.Add("Cost Center Deletion");
            }
            else if (ApplicationForm.ApplicationFormType == "Open MH System")
            {
                //Remove
                CategoryDropdown.Items.Remove("Manpower/Man-hour");
                CategoryDropdown.Items.Remove("Standard Time (ST mins)");
                CategoryDropdown.Items.Remove("Linestop/Loss Man-hour/Loss Factor");

                //Remove
                CategoryDropdown.Items.Remove("Annual ST Change");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form - No BIL Approval");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form");
                CategoryDropdown.Items.Remove("MH New ST Model List Form");

                CategoryDropdown.Items.Remove("Work Center New");
                CategoryDropdown.Items.Remove("Work Center Revision");
                CategoryDropdown.Items.Remove("Work Center Deletion");
                CategoryDropdown.Items.Remove("Cost Center New");
                CategoryDropdown.Items.Remove("Cost Center Revision");
                CategoryDropdown.Items.Remove("Cost Center Deletion");

                //Add
                CategoryDropdown.Items.Add("Manpower/Man-hour");
                CategoryDropdown.Items.Add("Standard Time (ST mins)");
                CategoryDropdown.Items.Add("Linestop/Loss Man-hour/Loss Factor");
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateAndTimeLabel.Text = dateTime.ToString("dddd , MMM dd yyyy, hh : mm : ss");
        }

        string fileName = string.Empty;
        string fileNameWithExt = string.Empty;
        string fileExt = string.Empty;

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string filePath = string.Empty;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;//get the path of the file
                    fileName = Path.GetFileNameWithoutExtension(filePath); // get the file name without extension
                    fileNameWithExt = Path.GetFileName(filePath);
                    fileExt = Path.GetExtension(filePath);//get the file extension
                    FilePath.Text = filePath;

                    FilePath.Text = openFileDialog.FileName;
                    try
                    {
                        using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                });
                                tableCollection = result.Tables;
                                SheetDropdownList.Items.Clear();
                                foreach (DataTable table in tableCollection)
                                    SheetDropdownList.Items.Add(table.TableName);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please close the Excel File!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        FilePath.Text = "";
                    }
                }
            }
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            if (CategoryDropdown.Text == "")
            {
                MessageBox.Show("Please select category.", "Category is required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CategoryDropdown.Select();
            }
            else if (ReasonOfApplicationTextBox.Text == "")
            {
                MessageBox.Show("Please type reason of application.", "Reason is Required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ReasonOfApplicationTextBox.Select();
            }
            else if (FilePath.Text == "")
            {
                MessageBox.Show("Please select the file.", "File is required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FilePath.Select();
            }
            else if (SheetDropdownList.Text == "")
            {
                MessageBox.Show("Please select the sheet.", "Sheet is required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                SheetDropdownList.Select();
            }
            else
            {
                if (ApplicationForm.ApplicationFormType == "ST")
                {
                    if (WithSAPRadioButton.Checked == false && NoSAPRadioButton.Checked == false)
                    {
                        MessageBox.Show("Please select if this application form is \"With SAP\" or \"No SAP\".", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        InsertSTApplication();
                    }
                }
                else if (ApplicationForm.ApplicationFormType == "WC/CC")
                {
                    InsertWCCCApplication();
                }
                else if (ApplicationForm.ApplicationFormType == "Open MH System")
                {
                    InsertOpenMHApplication();
                }
            }
        }

        

        string ApplicationFormNumber;
        string WithSAP;

       
        private void InsertSTApplication()
        {
            if (WithSAPRadioButton.Checked == true)
            {
                WithSAP = "Yes";
            }
            else if (NoSAPRadioButton.Checked == true)
            {
                WithSAP = "No";
            }

            //int Number = 0;

            if (CategoryDropdown.Text == "Annual ST Change")
            {
                try
                {
                    DapperPlusManager.Entity<ST_Class>().Table("TBL_AnnualChangeST");

                    List<ST_Class> UploadSTApplication = UploadTemplateDatagrid.DataSource as List<ST_Class>;

                    if (UploadSTApplication != null)
                    {
                        // Replace "-" with "0" in the specified fields
                        foreach (var st in UploadSTApplication)
                        {
                            st.SAP_BeforeST = ReplaceDashWithZero(st.SAP_BeforeST);
                            st.SAP_BeforeTT = ReplaceDashWithZero(st.SAP_BeforeTT);
                            st.SAP_AfterST = ReplaceDashWithZero(st.SAP_AfterST);
                            st.SAP_AfterTT = ReplaceDashWithZero(st.SAP_AfterTT);
                            st.SAPST = ReplaceDashWithZero(st.SAPST);
                            st.SAPTT = ReplaceDashWithZero(st.SAPTT);
                            st.MH_BeforeST = ReplaceDashWithZero(st.MH_BeforeST);
                            st.MH_BeforeTT = ReplaceDashWithZero(st.MH_BeforeTT);
                            st.MH_AfterST = ReplaceDashWithZero(st.MH_AfterST);
                            st.MH_AfterTT = ReplaceDashWithZero(st.MH_AfterTT);
                            st.MHST = ReplaceDashWithZero(st.MHST);
                            st.MHTT = ReplaceDashWithZero(st.MHTT);
                        }

                        using (IDbConnection db = new SqlConnection(conn))
                        {
                            db.BulkInsert(UploadSTApplication);
                        }


                        SendSTApplicationEmailMessage();

                        //Insert ST Application For approval
                        con.Open();
                        SqlCommand InsertApplicationApproval = new SqlCommand("SP_InsertApplicationApproval", con);
                        InsertApplicationApproval.CommandType = CommandType.StoredProcedure;
                        InsertApplicationApproval.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                        InsertApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                        InsertApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                        InsertApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        InsertApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                        InsertApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                        InsertApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                        InsertApplicationApproval.Parameters.AddWithValue("@MonthToOpen", ""); //Empty parameter -> for Open MH category only
                        InsertApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section MGR");
                        InsertApplicationApproval.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                        InsertApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                        InsertApplicationApproval.Parameters.AddWithValue("@Approver", "Section MGR");
                        InsertApplicationApproval.Parameters.AddWithValue("@WithSAP", WithSAP);
                        InsertApplicationApproval.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                        InsertApplicationApproval.Parameters.AddWithValue("@EffectivityDate", ApplicationForm.SelectedEffectivityDate);
                        InsertApplicationApproval.ExecuteNonQuery();
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during upload:\n" + ex.Message + "\n\nPlease contact the developer for assistance.", "Upload Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
              

                
                MessageBox.Show("Annual ST Change uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);


                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadTemplateDatagrid.DataSource = null;
                this.Close();

            }
            else if (CategoryDropdown.Text == "MH Change ST Model List Form - No BIL Approval")
            {
                try
                {
                    DapperPlusManager.Entity<ST_Class>().Table("TBL_MHChangeSTModelListForm-NoBILApproval");

                    List<ST_Class> UploadSTApplication = UploadTemplateDatagrid.DataSource as List<ST_Class>;

                    if (UploadSTApplication != null)
                    {
                        // Replace "-" with "0" in the specified fields
                        foreach (var st in UploadSTApplication)
                        {
                            st.SAP_BeforeST = ReplaceDashWithZero(st.SAP_BeforeST);
                            st.SAP_BeforeTT = ReplaceDashWithZero(st.SAP_BeforeTT);
                            st.SAP_AfterST = ReplaceDashWithZero(st.SAP_AfterST);
                            st.SAP_AfterTT = ReplaceDashWithZero(st.SAP_AfterTT);
                            st.SAPST = ReplaceDashWithZero(st.SAPST);
                            st.SAPTT = ReplaceDashWithZero(st.SAPTT);
                            st.MH_BeforeST = ReplaceDashWithZero(st.MH_BeforeST);
                            st.MH_BeforeTT = ReplaceDashWithZero(st.MH_BeforeTT);
                            st.MH_AfterST = ReplaceDashWithZero(st.MH_AfterST);
                            st.MH_AfterTT = ReplaceDashWithZero(st.MH_AfterTT);
                            st.MHST = ReplaceDashWithZero(st.MHST);
                            st.MHTT = ReplaceDashWithZero(st.MHTT);
                        }

                        using (IDbConnection db = new SqlConnection(conn))
                        {
                            db.BulkInsert(UploadSTApplication);

                            MessageBox.Show("MH Change ST Model List Form - No BIL Approval uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        SendSTApplicationEmailMessage();

                        //Insert ST Application For approval
                        con.Open();
                        SqlCommand InsertApplicationApproval = new SqlCommand("SP_InsertApplicationApproval", con);
                        InsertApplicationApproval.CommandType = CommandType.StoredProcedure;
                        InsertApplicationApproval.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                        InsertApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                        InsertApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                        InsertApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        InsertApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                        InsertApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                        InsertApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                        InsertApplicationApproval.Parameters.AddWithValue("@MonthToOpen", ""); //Empty parameter -> for Open MH category only
                        InsertApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section MGR");
                        InsertApplicationApproval.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                        InsertApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                        InsertApplicationApproval.Parameters.AddWithValue("@Approver", "Section MGR");
                        InsertApplicationApproval.Parameters.AddWithValue("@WithSAP", WithSAP);
                        InsertApplicationApproval.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                        InsertApplicationApproval.Parameters.AddWithValue("@EffectivityDate", ApplicationForm.SelectedEffectivityDate);
                        InsertApplicationApproval.ExecuteNonQuery();
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during upload:\n" + ex.Message + "\n\nPlease contact the developer for assistance.", "Upload Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                

                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadTemplateDatagrid.DataSource = null;
                this.Close();

            }
            else if (CategoryDropdown.Text == "MH Change ST Model List Form")
            {
                try
                {
                    DapperPlusManager.Entity<ST_Class>().Table("TBL_MHChangeSTModelListForm");

                    List<ST_Class> UploadSTApplication = UploadTemplateDatagrid.DataSource as List<ST_Class>;

                    if (UploadSTApplication != null)
                    {
                        // Replace "-" with "0" in the specified fields
                        foreach (var st in UploadSTApplication)
                        {
                            st.SAP_BeforeST = ReplaceDashWithZero(st.SAP_BeforeST);
                            st.SAP_BeforeTT = ReplaceDashWithZero(st.SAP_BeforeTT);
                            st.SAP_AfterST = ReplaceDashWithZero(st.SAP_AfterST);
                            st.SAP_AfterTT = ReplaceDashWithZero(st.SAP_AfterTT);
                            st.SAPST = ReplaceDashWithZero(st.SAPST);
                            st.SAPTT = ReplaceDashWithZero(st.SAPTT);
                            st.MH_BeforeST = ReplaceDashWithZero(st.MH_BeforeST);
                            st.MH_BeforeTT = ReplaceDashWithZero(st.MH_BeforeTT);
                            st.MH_AfterST = ReplaceDashWithZero(st.MH_AfterST);
                            st.MH_AfterTT = ReplaceDashWithZero(st.MH_AfterTT);
                            st.MHST = ReplaceDashWithZero(st.MHST);
                            st.MHTT = ReplaceDashWithZero(st.MHTT);
                        }

                        using (IDbConnection db = new SqlConnection(conn))
                        {
                            db.BulkInsert(UploadSTApplication);

                            MessageBox.Show("MH Change ST Model List Form uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        SendSTApplicationEmailMessage();

                        //Insert ST Application For approval
                        con.Open();
                        SqlCommand InsertApplicationApproval = new SqlCommand("SP_InsertApplicationApproval", con);
                        InsertApplicationApproval.CommandType = CommandType.StoredProcedure;
                        InsertApplicationApproval.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                        InsertApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                        InsertApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                        InsertApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        InsertApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                        InsertApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                        InsertApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                        InsertApplicationApproval.Parameters.AddWithValue("@MonthToOpen", ""); //Empty parameter -> for Open MH category only
                        InsertApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section MGR");
                        InsertApplicationApproval.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                        InsertApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                        InsertApplicationApproval.Parameters.AddWithValue("@Approver", "Section MGR");
                        InsertApplicationApproval.Parameters.AddWithValue("@WithSAP", WithSAP);
                        InsertApplicationApproval.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                        InsertApplicationApproval.Parameters.AddWithValue("@EffectivityDate", ApplicationForm.SelectedEffectivityDate);
                        InsertApplicationApproval.ExecuteNonQuery();
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during upload:\n" + ex.Message + "\n\nPlease contact the developer for assistance.", "Upload Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                

                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadTemplateDatagrid.DataSource = null;
                this.Close();
            }
            else if (CategoryDropdown.Text == "MH New ST Model List Form")
            {
                try
                {
                    DapperPlusManager.Entity<ST_Class>().Table("TBL_MHNewSTModelListForm");

                    List<ST_Class> UploadSTApplication = UploadTemplateDatagrid.DataSource as List<ST_Class>;

                    if (UploadSTApplication != null)
                    {
                        // Replace "-" with "0" in the specified fields
                        foreach (var st in UploadSTApplication)
                        {
                            st.SAP_BeforeST = ReplaceDashWithZero(st.SAP_BeforeST);
                            st.SAP_BeforeTT = ReplaceDashWithZero(st.SAP_BeforeTT);
                            st.SAP_AfterST = ReplaceDashWithZero(st.SAP_AfterST);
                            st.SAP_AfterTT = ReplaceDashWithZero(st.SAP_AfterTT);
                            st.SAPST = ReplaceDashWithZero(st.SAPST);
                            st.SAPTT = ReplaceDashWithZero(st.SAPTT);
                            st.MH_BeforeST = ReplaceDashWithZero(st.MH_BeforeST);
                            st.MH_BeforeTT = ReplaceDashWithZero(st.MH_BeforeTT);
                            st.MH_AfterST = ReplaceDashWithZero(st.MH_AfterST);
                            st.MH_AfterTT = ReplaceDashWithZero(st.MH_AfterTT);
                            st.MHST = ReplaceDashWithZero(st.MHST);
                            st.MHTT = ReplaceDashWithZero(st.MHTT);
                        }

                        using (IDbConnection db = new SqlConnection(conn))
                        {
                            db.BulkInsert(UploadSTApplication);
                            MessageBox.Show("MH New ST Model List Form uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    SendSTApplicationEmailMessage();


                    //Insert ST Application For approval
                    con.Open();
                    SqlCommand InsertApplicationApproval = new SqlCommand("SP_InsertApplicationApproval", con);
                    InsertApplicationApproval.CommandType = CommandType.StoredProcedure;
                    InsertApplicationApproval.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                    InsertApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                    InsertApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                    InsertApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    InsertApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                    InsertApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                    InsertApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                    InsertApplicationApproval.Parameters.AddWithValue("@MonthToOpen", ""); //Empty parameter -> for Open MH category only
                    InsertApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section MGR");
                    InsertApplicationApproval.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                    InsertApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                    InsertApplicationApproval.Parameters.AddWithValue("@Approver", "Section MGR");
                    InsertApplicationApproval.Parameters.AddWithValue("@WithSAP", WithSAP);
                    InsertApplicationApproval.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                    InsertApplicationApproval.Parameters.AddWithValue("@EffectivityDate", ApplicationForm.SelectedEffectivityDate);
                    InsertApplicationApproval.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("MH New ST Model List Form uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during upload:\n" + ex.Message + "\n\nPlease contact the developer for assistance.", "Upload Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadTemplateDatagrid.DataSource = null;
                this.Close();
            }

        }

        // Helper method to replace "-" with "0"
        private string ReplaceDashWithZero(string value)
        {
            if (value == "-")
            {
                return "0";
            }
            return value;
        }
        //================================================================<BreakLine>======================================================>>>

        private void InsertWCCCApplication()
        {
            if (CategoryDropdown.Text == "Work Center New")
            {
                try
                {
                    DapperPlusManager.Entity<WCCC_New_Class>().Table("TBL_WorkCenterNew");

                    List<WCCC_New_Class> UploadWCCCApplication = UploadTemplateDatagrid.DataSource as List<WCCC_New_Class>;

                    if (UploadWCCCApplication != null)
                    {
                        using (IDbConnection db = new SqlConnection(conn))
                        {
                            db.BulkInsert(UploadWCCCApplication);
                        }
                    }

                    // Send email
                    SendWCCCApplicationEmailMessage();

                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();
                        using (SqlCommand InsertApplicationApproval = new SqlCommand("SP_InsertApplicationApproval", con))
                        {
                            InsertApplicationApproval.CommandType = CommandType.StoredProcedure;
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                            InsertApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            InsertApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                            InsertApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                            InsertApplicationApproval.Parameters.AddWithValue("@MonthToOpen", ""); // Empty parameter -> for Open MH category only
                            InsertApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section SPV");
                            InsertApplicationApproval.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                            InsertApplicationApproval.Parameters.AddWithValue("@Approver", "Section SPV");
                            InsertApplicationApproval.Parameters.AddWithValue("@WithSAP", "");
                            InsertApplicationApproval.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@EffectivityDate", ApplicationForm.SelectedEffectivityDate);

                            InsertApplicationApproval.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear fields after upload
                    FilePath.Text = "";
                    SheetDropdownList.Text = "";
                    UploadTemplateDatagrid.DataSource = null;
                    this.Close();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Database error: " + sqlEx.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (CategoryDropdown.Text == "Work Center Revision")
            {
                try
                {
                    DapperPlusManager.Entity<WC_Revision_Class>().Table("TBL_WorkCenterRevision");
                    List<WC_Revision_Class> UploadWCCCApplication = UploadTemplateDatagrid.DataSource as List<WC_Revision_Class>;

                    if (UploadWCCCApplication != null)
                    {
                        using (IDbConnection db = new SqlConnection(conn))
                        {
                            db.BulkInsert(UploadWCCCApplication);
                        }
                    }

                    // Send email
                    SendWCCCApplicationEmailMessage();

                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();
                        using (SqlCommand InsertApplicationApproval = new SqlCommand("SP_InsertApplicationApproval", con))
                        {
                            InsertApplicationApproval.CommandType = CommandType.StoredProcedure;
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                            InsertApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            InsertApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                            InsertApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                            InsertApplicationApproval.Parameters.AddWithValue("@MonthToOpen", ""); // Empty parameter -> for Open MH category only
                            InsertApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section SPV");
                            InsertApplicationApproval.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                            InsertApplicationApproval.Parameters.AddWithValue("@Approver", "Section SPV");
                            InsertApplicationApproval.Parameters.AddWithValue("@WithSAP", "");
                            InsertApplicationApproval.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@EffectivityDate", ApplicationForm.SelectedEffectivityDate);

                            InsertApplicationApproval.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear fields after upload
                    FilePath.Text = "";
                    SheetDropdownList.Text = "";
                    UploadTemplateDatagrid.DataSource = null;
                    this.Close();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Database error: " + sqlEx.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (CategoryDropdown.Text == "Work Center Deletion")
            {
                try
                {
                    DapperPlusManager.Entity<WCCC_Deletion_Class>().Table("TBL_WorkCenterDeletion");
                    List<WCCC_Deletion_Class> UploadWCCCApplication = UploadTemplateDatagrid.DataSource as List<WCCC_Deletion_Class>;

                    if (UploadWCCCApplication != null)
                    {
                        using (IDbConnection db = new SqlConnection(conn))
                        {
                            db.BulkInsert(UploadWCCCApplication);
                        }
                    }

                    // Send email
                    SendWCCCApplicationEmailMessage();

                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();
                        using (SqlCommand InsertApplicationApproval = new SqlCommand("SP_InsertApplicationApproval", con))
                        {
                            InsertApplicationApproval.CommandType = CommandType.StoredProcedure;
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                            InsertApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            InsertApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                            InsertApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                            InsertApplicationApproval.Parameters.AddWithValue("@MonthToOpen", ""); // Empty parameter -> for Open MH category only
                            InsertApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section SPV");
                            InsertApplicationApproval.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                            InsertApplicationApproval.Parameters.AddWithValue("@Approver", "Section SPV");
                            InsertApplicationApproval.Parameters.AddWithValue("@WithSAP", "");
                            InsertApplicationApproval.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@EffectivityDate", ApplicationForm.SelectedEffectivityDate);

                            InsertApplicationApproval.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear fields after upload
                    FilePath.Text = "";
                    SheetDropdownList.Text = "";
                    UploadTemplateDatagrid.DataSource = null;
                    this.Close();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Database error: " + sqlEx.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (CategoryDropdown.Text == "Cost Center New")
            {
                try
                {
                    DapperPlusManager.Entity<WCCC_New_Class>().Table("TBL_CostCenterNew");
                    List<WCCC_New_Class> UploadWCCCApplication = UploadTemplateDatagrid.DataSource as List<WCCC_New_Class>;
                    if (UploadWCCCApplication != null)
                    {
                        using (IDbConnection db = new SqlConnection(conn))
                        {
                            db.BulkInsert(UploadWCCCApplication);
                        }
                    }

                    //Send email
                    SendWCCCApplicationEmailMessage();

                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();
                        using (SqlCommand InsertApplicationApproval = new SqlCommand("SP_InsertApplicationApproval", con))
                        {
                            InsertApplicationApproval.CommandType = CommandType.StoredProcedure;
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                            InsertApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            InsertApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                            InsertApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                            InsertApplicationApproval.Parameters.AddWithValue("@MonthToOpen", ""); //Empty parameter -> for Open MH category only
                            InsertApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section SPV");
                            InsertApplicationApproval.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                            InsertApplicationApproval.Parameters.AddWithValue("@Approver", "Section SPV");
                            InsertApplicationApproval.Parameters.AddWithValue("@WithSAP", "");
                            InsertApplicationApproval.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@EffectivityDate", ApplicationForm.SelectedEffectivityDate);
                            InsertApplicationApproval.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    //Clear fields after upload
                    FilePath.Text = "";
                    SheetDropdownList.Text = "";
                    UploadTemplateDatagrid.DataSource = null;
                    this.Close();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Database error: " + sqlEx.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (CategoryDropdown.Text == "Cost Center Revision")
            {
                try
                {
                    DapperPlusManager.Entity<CC_Revision_Class>().Table("TBL_CostCenterRevision");
                    List<CC_Revision_Class> UploadWCCCApplication = UploadTemplateDatagrid.DataSource as List<CC_Revision_Class>;
                    if (UploadWCCCApplication != null)
                    {
                        using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                        {
                            db.BulkInsert(UploadWCCCApplication);

                        }
                    }

                    //Send email
                    SendWCCCApplicationEmailMessage();

                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();
                        using (SqlCommand InsertApplicationApproval = new SqlCommand("SP_InsertApplicationApproval", con))
                        {
                            InsertApplicationApproval.CommandType = CommandType.StoredProcedure;
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                            InsertApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            InsertApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                            InsertApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                            InsertApplicationApproval.Parameters.AddWithValue("@MonthToOpen", ""); //Empty parameter -> for Open MH category only
                            InsertApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section SPV");
                            InsertApplicationApproval.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                            InsertApplicationApproval.Parameters.AddWithValue("@Approver", "Section SPV");
                            InsertApplicationApproval.Parameters.AddWithValue("@WithSAP", "");
                            InsertApplicationApproval.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@EffectivityDate", ApplicationForm.SelectedEffectivityDate);
                            InsertApplicationApproval.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Clear fields after upload
                    FilePath.Text = "";
                    SheetDropdownList.Text = "";
                    UploadTemplateDatagrid.DataSource = null;
                    this.Close();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Database error: " + sqlEx.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else if (CategoryDropdown.Text == "Cost Center Deletion")
            {
                try
                {
                    DapperPlusManager.Entity<WCCC_Deletion_Class>().Table("TBL_CostCenterDeletion");
                    List<WCCC_Deletion_Class> UploadWCCCApplication = UploadTemplateDatagrid.DataSource as List<WCCC_Deletion_Class>;
                    if (UploadWCCCApplication != null)
                    {
                        using (IDbConnection db = new SqlConnection(conn))
                        {
                            db.BulkInsert(UploadWCCCApplication);

                        }
                    }

                    //Send email
                    SendWCCCApplicationEmailMessage();

                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();
                        using (SqlCommand InsertApplicationApproval = new SqlCommand("SP_InsertApplicationApproval", con))
                        {
                            InsertApplicationApproval.CommandType = CommandType.StoredProcedure;
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                            InsertApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            InsertApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                            InsertApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                            InsertApplicationApproval.Parameters.AddWithValue("@MonthToOpen", ""); //Empty parameter -> for Open MH category only
                            InsertApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section SPV");
                            InsertApplicationApproval.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                            InsertApplicationApproval.Parameters.AddWithValue("@Approver", "Section SPV");
                            InsertApplicationApproval.Parameters.AddWithValue("@WithSAP", "");
                            InsertApplicationApproval.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@EffectivityDate", ApplicationForm.SelectedEffectivityDate);
                            InsertApplicationApproval.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Clear fields after upload
                    FilePath.Text = "";
                    SheetDropdownList.Text = "";
                    UploadTemplateDatagrid.DataSource = null;
                    this.Close();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Database error: " + sqlEx.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        //================================================================<BreakLine>======================================================>>>

        private void InsertOpenMHApplication()
        {
           
            if (CategoryDropdown.Text == "Manpower/Man-hour")
            {
                try
                {
                    DapperPlusManager.Entity<OpenMH_MH_MPCategory_Class>().Table("TBL_ManpowerManhourCategory");
                    List<OpenMH_MH_MPCategory_Class> UploadOpenMHApplication = UploadTemplateDatagrid.DataSource as List<OpenMH_MH_MPCategory_Class>;
                    if (UploadOpenMHApplication != null)
                    {
                        using (IDbConnection db = new SqlConnection(conn))
                        {
                            db.BulkInsert(UploadOpenMHApplication);
                        }
                    }

                    //Send email
                    SendOpenMHApplicationEmailMessage();

                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();
                        using (SqlCommand InsertApplicationApproval = new SqlCommand("SP_InsertApplicationApproval", con))
                        {
                            InsertApplicationApproval.CommandType = CommandType.StoredProcedure;
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                            InsertApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            InsertApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                            InsertApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                            InsertApplicationApproval.Parameters.AddWithValue("@MonthToOpen", ApplicationForm.MonthToOpen);
                            InsertApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section MGR");
                            InsertApplicationApproval.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                            InsertApplicationApproval.Parameters.AddWithValue("@Approver", "Section MGR");
                            InsertApplicationApproval.Parameters.AddWithValue("@WithSAP", ""); //Null parameter->Not applicable for Open MH application
                            InsertApplicationApproval.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@EffectivityDate", ApplicationForm.SelectedEffectivityDate);
                            InsertApplicationApproval.ExecuteNonQuery();
                        }
                    }

                   
                    MessageBox.Show("Uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    //Clear fields after upload
                    FilePath.Text = "";
                    SheetDropdownList.Text = "";
                    UploadTemplateDatagrid.DataSource = null;
                    this.Close();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Database error: " + sqlEx.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else if (CategoryDropdown.Text == "Standard Time (ST mins)")
            {
                try
                {
                    DapperPlusManager.Entity<OpenMH_StandardTime_Class>().Table("TBL_StandardTime");
                    List<OpenMH_StandardTime_Class> UploadOpenMHApplication = UploadTemplateDatagrid.DataSource as List<OpenMH_StandardTime_Class>;
                    if (UploadOpenMHApplication != null)
                    {
                        using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                        {

                            db.BulkInsert(UploadOpenMHApplication);

                        }
                    }

                    //Send email
                    SendOpenMHApplicationEmailMessage();

                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();
                        using (SqlCommand InsertApplicationApproval = new SqlCommand("SP_InsertApplicationApproval", con))
                        {
                            InsertApplicationApproval.CommandType = CommandType.StoredProcedure;
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                            InsertApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            InsertApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                            InsertApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                            InsertApplicationApproval.Parameters.AddWithValue("@MonthToOpen", ApplicationForm.MonthToOpen);
                            InsertApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section MGR");
                            InsertApplicationApproval.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                            InsertApplicationApproval.Parameters.AddWithValue("@Approver", "Section MGR");
                            InsertApplicationApproval.Parameters.AddWithValue("@WithSAP", ""); //Null parameter->Not applicable for Open MH application
                            InsertApplicationApproval.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@EffectivityDate", ApplicationForm.SelectedEffectivityDate);
                            InsertApplicationApproval.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    //Clear fields after upload
                    FilePath.Text = "";
                    SheetDropdownList.Text = "";
                    UploadTemplateDatagrid.DataSource = null;
                    this.Close();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Database error: " + sqlEx.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else if (CategoryDropdown.Text == "Linestop/Loss Man-hour/Loss Factor")
            {
                try
                {
                    DapperPlusManager.Entity<OpenMH_LineStop_LossMH_LossFactor_Class>().Table("TBL_LineStop_LossManhour_LossFactor");
                    List<OpenMH_LineStop_LossMH_LossFactor_Class> UploadOpenMHApplication = UploadTemplateDatagrid.DataSource as List<OpenMH_LineStop_LossMH_LossFactor_Class>;
                    if (UploadOpenMHApplication != null)
                    {
                        using (IDbConnection db = new SqlConnection(conn))
                        {

                            db.BulkInsert(UploadOpenMHApplication);

                        }
                    }

                    //Send email
                    SendOpenMHApplicationEmailMessage();

                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();
                        using (SqlCommand InsertApplicationApproval = new SqlCommand("SP_InsertApplicationApproval", con))
                        {
                            InsertApplicationApproval.CommandType = CommandType.StoredProcedure;
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                            InsertApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                            InsertApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            InsertApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                            InsertApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                            InsertApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                            InsertApplicationApproval.Parameters.AddWithValue("@MonthToOpen", ApplicationForm.MonthToOpen);
                            InsertApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section MGR");
                            InsertApplicationApproval.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                            InsertApplicationApproval.Parameters.AddWithValue("@Approver", "Section MGR");
                            InsertApplicationApproval.Parameters.AddWithValue("@WithSAP", ""); //Null parameter->Not applicable for Open MH application
                            InsertApplicationApproval.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                            InsertApplicationApproval.Parameters.AddWithValue("@EffectivityDate", ApplicationForm.SelectedEffectivityDate);
                            InsertApplicationApproval.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    //Clear fields after upload
                    FilePath.Text = "";
                    SheetDropdownList.Text = "";
                    UploadTemplateDatagrid.DataSource = null;
                    this.Close();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Database error: " + sqlEx.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        //================================================================<BreakLine>======================================================>>>


        string innerString;
        string FirstName;
        string LastName;
        string Email;
        private void SendSTApplicationEmailMessage()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationForm.ApplicationFormType);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationForm.ApplicationFormType);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationForm.ApplicationFormType);
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
                    //FirstName = row["First Name"].ToString();
                    //LastName = row["Last Name"].ToString();
                    //Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();
                   

                    //builder.Append("Dear " + LastNameList + " san,");
                    builder.Append("Dear " + Dashboard.SectionText.Replace("BIPH-", "") + " Section MGR,");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>
                   
                    if (ApplicationForm.Category == "Annual ST Change")
                    {
                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてプリンター課の年計ST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の年計ST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の年計ST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の年計ST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の年計ST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の年計ST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の年計ST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課の年計ST変更機種一覧申請書をご覧下さい。</font>");
                        }

                    }
                    else if (ApplicationForm.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてプリンター課のST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課のST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課のST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課のST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課のST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課のST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課のST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課のST変更機種一覧申請書をご覧下さい。</font>");
                        }


                    }
                    else if (ApplicationForm.Category == "MH Change ST Model List Form")
                    {
                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてプリンター課のST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課のST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課のST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課のST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課のST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課のST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課のST変更機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課のST変更機種一覧申請書をご覧下さい。</font>");
                        }

                    }
                    else if (ApplicationForm.Category == "MH New ST Model List Form")
                    {
                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてプリンター課の新規ST機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の新規ST機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の新規ST機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の新規ST機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の新規ST機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's New ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の新規ST機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の新規ST機種一覧申請書をご覧下さい。</font>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の新規ST機種一覧申請書をご覧下さい。</font>");
                        }

                    }

                    //====================>>>>>>>>

                    //====================>>>>>>>>


                    builder.Append("<br>");

                    builder.Append("For your checking and approval");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>確認依頼と承認状況の連絡になります。</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");

                    builder.Append(ReferenceNo);
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
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + Dashboard.SectionText.Replace("BIPH-", "") + " section's " + CategoryDropdown.Text + " Application form.";
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

        //================================================================<BreakLine>======================================================>>>   

        private void SendWCCCApplicationEmailMessage()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionSPV");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationForm.ApplicationFormType);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationForm.ApplicationFormType);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select BPS MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationForm.ApplicationFormType);
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
                    //FirstName = row["First Name"].ToString();
                    //LastName = row["Last Name"].ToString();
                    //Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();


                    builder.Append("Dear " + Dashboard.SectionText.Replace("BIPH-", "") + " Section SPV,");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>お疲れ様です。</font>");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    if (CategoryDropdown.Text == "Work Center New")
                    {
                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課の新規ワークセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課の新規ワークセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");

                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課の新規ワークセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課の新規ワークセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課の新規ワークセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課の新規ワークセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課の新規ワークセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課の新規ワークセンター登録申請の連絡です。</font>");
                            //builder.Append("<br>");
                        }

                    }
                    else if (CategoryDropdown.Text == "Work Center Revision")
                    {
                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課のワークセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課のワークセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課のワークセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課のワークセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課のワークセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課のワークセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課のワークセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課の新規ワークセンター登録申請の連絡です。</font>");
                            //builder.Append("<br>");
                        }

                    }
                    else if (CategoryDropdown.Text == "Work Center Deletion")
                    {
                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課のワークセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課のワークセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");

                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課のワークセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {   
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課のワークセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課のワークセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課のワークセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課のワークセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課のワークセンターの削除申請の連絡です。</font>");
                            //builder.Append("<br>");
                        }

                    }
                    else if (CategoryDropdown.Text == "Cost Center New")
                    {
                       

                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課の新規コストセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課の新規コストセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");

                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課の新規コストセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課の新規コストセンター登録申請の連絡です。</ font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課の新規コストセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課の新規コストセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課の新規コストセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>PE課の新作業区の登録依頼です。</font>");
                            //builder.Append("<br>");
                        }
                    }
                    else if (CategoryDropdown.Text == "Cost Center Revision")
                    {
                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課のコストセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課のコストセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");

                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課のコストセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課のコストセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課のコストセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課のコストセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課のコストセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>PE課の新作業区の登録依頼です。</font>");
                            //builder.Append("<br>");
                        }
                    }
                    else if (CategoryDropdown.Text == "Cost Center Deletion")
                    {
                       

                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課のコストセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課のコストセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");

                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課のコストセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課のコストセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課のコストセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課のコストセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課のコストセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>PE課の新作業区の登録依頼です。</font>");
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
                    builder.Append("<b><i>" + ReferenceNo + "</i></b>");
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
                    builder.Append("Bautista, Princess (BIPH-PE) <princess.bautista@brother-biph.com.ph>");
                    builder.Append("<br>");
                    builder.Append("Mateo, Bradly (BIPH-PE) <bradly.mateo@brother-biph.com.ph>");
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
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + Dashboard.SectionText.Replace("BIPH-", "") + " section's " + CategoryDropdown.Text + " Application form.";
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

        private void SendOpenMHApplicationEmailMessage()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationForm.ApplicationFormType);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationForm.ApplicationFormType);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();


            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationForm.ApplicationFormType);
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


                    builder.Append("Dear " + Dashboard.SectionText.Replace("BIPH-", "") + " Section MGR,");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>お疲れ様です。</font>");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                    {
                        builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH system request form.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>インクカートリッジセクション(Open MH)のシステムリクエストフォームについては、以下のリンクを参照してください。</font>");
                        builder.Append("<br>");
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                    {
                        builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>以下のリンクにてインクヘッド課のMHシステム編集解除許可(OPEN MH)申請書ご覧下さい。</font>");
                        builder.Append("<br>");

                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                    {
                        builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH system request form.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>以下のリンクにてP-タッチ課のMHシステム編集解除許可（OPEN MH)申請書ご覧下さい。</font>");
                        builder.Append("<br>");
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                    {
                        builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH system request form.");
                        builder.Append("<br>");
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                    {
                        builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH system request form.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>以下のリンクにてプリンター課のMHシステム編集解除許可(OPEN MH)申請書ご覧下さい。</font>");
                        builder.Append("<br>");
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                    {
                        builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH system request form.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>以下のリンクにてテープカセット課のMHシステム編集解除許可(OPEN MH)申請書ご覧下さい。</font>");
                        builder.Append("<br>");
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                    {
                        builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH system request form.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>以下のリンクにて基板組立課のMHシステム編集解除許可(OPEN MH)申請書ご覧下さい。</font>");
                        builder.Append("<br>");
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                    {
                        builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH system request form.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>以下のリンクにて成形課のMHシステム編集解除許可(OPEN MH)申請書ご覧下さい。</font>");
                        builder.Append("<br>");
                    }


                    builder.Append("<b>For your approval.</b>");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>確認・承認待ち</font>");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                    builder.Append("<br>");
                    builder.Append("<b><i>" + ReferenceNo + "</i></b>");
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
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH request.";
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

        //================================================================<BreakLine>======================================================>>> 

        string ItemCodeSAP;
        string ItemCodeMH;
        string No;
        string Plant;
        string ItemCode;
        string ItemName;
        string Section;
        string SAPBeforeST;
        string SAPBeforeTT;
        string MHBeforeST;
        string MHBeforeTT;
        string MassProduction;

        private void AutoUpdateSTapplication()
        {
            con.Open();
            //SELECT ALL ANNUAL ST APPLICATION WHERE DATE APPLIED IS EQUAL TO DATE TODAY
            SqlCommand SelectSTAppliedToday = new SqlCommand("SP_SelectSTApplicationAppliedToday", con);
            SelectSTAppliedToday.CommandType = CommandType.StoredProcedure;
            SelectSTAppliedToday.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
            //SelectSTAppliedToday.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter da = new SqlDataAdapter(SelectSTAppliedToday);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    con.Open();
                    SqlDataReader reader = SelectSTAppliedToday.ExecuteReader();
                    if (reader.Read())
                    {
                        ItemCodeSAP = reader["ItemCodeSAP"].ToString();
                        ItemCodeMH = reader["ItemCodeSAP"].ToString();
                        No = reader["No"].ToString();

                        reader.Close();
                    }
                   

                    if (ItemCodeSAP != "")
                    {
                        
                        //con.Open();
                        //select SAP ST from SAP master data
                        SqlCommand SelectSTItemCode = new SqlCommand("SP_SelectSTItemCodeFromMasterData", con);
                        SelectSTItemCode.CommandType = CommandType.StoredProcedure;
                        SelectSTItemCode.Parameters.AddWithValue("@Procedure", "SAP");
                        SelectSTItemCode.Parameters.AddWithValue("@ItemCode", ItemCodeSAP);
                        SelectSTItemCode.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter da2 = new SqlDataAdapter(SelectSTItemCode);
                        DataTable dt2 = new DataTable();
                        da.Fill(dt);
                        con.Close();

                       
                        con.Open();

                        SqlDataReader reader2 = SelectSTItemCode.ExecuteReader();

                        if (reader2.Read())
                        {
                            //No = reader2["No"].ToString();
                            ItemCodeSAP = reader2["ItemCodeSAP"].ToString();
                            ItemName = reader2["ItemNameSAP"].ToString();
                            Plant = reader2["Plant"].ToString();
                            Section = reader2["Section"].ToString();
                            SAPBeforeST = reader2["SAPBeforeST"].ToString();
                            SAPBeforeTT = reader2["SAPBeforeTT"].ToString();

                            reader.Close();
                        }

                        con.Close();
                        //Update application
                        SqlCommand AutoFillSTApplication = new SqlCommand("SP_UpdateSAPBatchSTApplication", con);
                        AutoFillSTApplication.CommandType = CommandType.StoredProcedure;
                        AutoFillSTApplication.Parameters.AddWithValue("@STcategory", CategoryDropdown.Text);
                        AutoFillSTApplication.Parameters.AddWithValue("@No", No);
                        AutoFillSTApplication.Parameters.AddWithValue("@Section", Section);
                        AutoFillSTApplication.Parameters.AddWithValue("@Plant", Plant);
                        AutoFillSTApplication.Parameters.AddWithValue("@ItemCode", ItemCodeSAP);
                        AutoFillSTApplication.Parameters.AddWithValue("@ItemName", ItemName);
                        AutoFillSTApplication.Parameters.AddWithValue("@SAPBeforeST", SAPBeforeST);
                        AutoFillSTApplication.Parameters.AddWithValue("@SAPBeforeTT", SAPBeforeTT);
                        //AutoFillSTApplication.Parameters.AddWithValue("@SAPAfterST", SAPAfterST);
                        //AutoFillSTApplication.Parameters.AddWithValue("@SAPAfterTT", SAPAfterTT);
                        //AutoFillSTApplication.Parameters.AddWithValue("@EffectivityDate", EffectivityDateTimePicker.Value.ToString("MM/dd/yyyy"));
                        //AutoFillSTApplication.Parameters.AddWithValue("@Reason", Reason);
                        //AutoFillSTApplication.Parameters.AddWithValue("@Remarks", Remarks);
                        //AutoFillSTApplication.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                        //AutoFillSTApplication.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                        con.Open();
                        AutoFillSTApplication.ExecuteNonQuery();
                        con.Close();

                    }


                }
               
            }
        }

        //================================================================<BreakLine>======================================================>>>   
        //WC/CC Revision
        string CostcenterCodeOld;
        string CostcenterNameOld;
        string WorkcenterCodeOld;
        string WorkcenterNameOld;
        string PlantOld;
        string CostcenterGrouping_A;
        string CostcenterGrouping_B;
        string ReferenceNo;

        //WC/CC Deletion
        string WorkCenterCode;
        string WorkCenterName;
        string CostCenterCode;
        string CostCenterName;
        string Shift;
        string CostCenterGrouping;

        //OpenMH - Standard Time
        string OldST;

        private void SheetDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ApplicationForm.ApplicationFormType == "ST")
            {
                //ST-Ink Cartridge_2023040112
                ReferenceNo = "ST-" + CategoryDropdown.Text + "-" + Dashboard.SectionText.Replace("BIPH-", "") + "_" + DateTime.Now.ToString("yyyyMMddhhmm");

                DataTable dt = tableCollection[SheetDropdownList.SelectedItem.ToString()];

                if (dt != null)
                {
                    List<ST_Class> list = new List<ST_Class>();

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    //select no.
                    SqlCommand SelectApplicationFormNo = new SqlCommand("SP_SelectApplicationFormNo", con);
                    SelectApplicationFormNo.CommandType = CommandType.StoredProcedure;
                    SelectApplicationFormNo.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                    SelectApplicationFormNo.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                    SelectApplicationFormNo.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SqlDataAdapter da = new SqlDataAdapter(SelectApplicationFormNo);
                    DataTable dTable = new DataTable();
                    da.Fill(dTable);
                    con.Close();

                    if (dTable.Rows.Count > 0)
                    {
                        con.Open();
                        SqlDataReader reader = SelectApplicationFormNo.ExecuteReader();
                        if (reader.Read())
                        {
                            ApplicationFormNumber = reader[0].ToString(); //ApplicationFormNo Column

                            reader.Close();
                        }
                    }
                    else
                    {
                        ApplicationFormNumber = "0";
                    }

                    int Number = 0;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        // Skip if Plant column is blank or empty
                        if (string.IsNullOrWhiteSpace(dt.Rows[i]["Plant"]?.ToString()))
                            continue;

                        ST_Class obj = new ST_Class();

                        if (CategoryDropdown.Text == "MH New ST Model List Form")
                        {
                            obj.Plant = dt.Rows[i]["Plant"].ToString();

                            if (obj.Plant != "")
                            {
                                // Column names to validate (assuming we are validating "Column2" and "Column3")
                                string[] columnsToValidate = { "Mass Production (Month Start)", "SAP ST(min)", "SAP TT(min)", "MH ST(min)", "MH TT(min)" };
                                bool containsHyphen = false;

                                // Iterate through each row and check the specified columns for a hyphen
                                foreach (DataGridViewRow row in UploadTemplateDatagrid.Rows)
                                {
                                    // Validate each specified column in the row
                                    foreach (string columnName in columnsToValidate)
                                    {
                                        if (row.Cells[columnName].Value != null &&
                                            row.Cells[columnName].Value.ToString().Contains("-"))
                                        {
                                            containsHyphen = true;
                                            break;
                                        }
                                    }

                                    // If any column contains a hyphen, stop further checks and show the message
                                    if (containsHyphen)
                                    {
                                        break;
                                    }
                                }

                                // If any row contains a hyphen in any of the specified columns, show a message and prevent insertion
                                if (containsHyphen)
                                {
                                    MessageBox.Show("Uploading failed. One or more cells in the specified columns contain a hyphen (-), please check the template.",
                                                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    obj.ReferenceNo = ReferenceNo;
                                    obj.ApplicationFormNo = (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString();
                                    obj.No = (Number += 1).ToString();
                                    obj.MassProduction = dt.Rows[i]["Mass Production (Month Start)"].ToString();
                                    obj.Plant = dt.Rows[i]["Plant"].ToString();
                                    obj.ItemCodeSAP = dt.Rows[i]["Item Code (SAP)"].ToString();
                                    obj.ItemNameSAP = dt.Rows[i]["Item Name (SAP)"].ToString();
                                    obj.SAPST = dt.Rows[i]["SAP ST(min)"].ToString();
                                    obj.SAPTT = dt.Rows[i]["SAP TT(min)"].ToString();
                                    obj.ItemCodeMH = dt.Rows[i]["Item Code (MH)"].ToString();
                                    obj.ItemNameMH = dt.Rows[i]["Item Name (MH)"].ToString();
                                    obj.MHST = dt.Rows[i]["MH ST(min)"].ToString();
                                    obj.MHTT = dt.Rows[i]["MH TT(min)"].ToString();
                                    obj.EffectivityDate = dt.Rows[i]["Effectivity Date"].ToString();
                                    obj.Reason = dt.Rows[i]["Reason"].ToString();
                                    obj.Remarks = dt.Rows[i]["Remarks"].ToString();
                                    obj.Section = Dashboard.SectionText.Replace("BIPH-", "");
                                }
                            }
                        }
                        else
                        {
                            obj.ItemCodeSAP = dt.Rows[i]["Item Code (SAP)"].ToString();
                            obj.ItemCodeMH = dt.Rows[i]["Item Code (MH)"].ToString();
                            obj.ReferenceNo = ReferenceNo;
                            obj.ApplicationFormNo = (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString();
                            obj.No = (Number += 1).ToString();

                            if (obj.ItemCodeSAP != "")
                            {
                                // Column names to validate (assuming we are validating "Column2" and "Column3")
                                string[] columnsToValidate = { "Mass Production (Month Start)", "SAP Before ST(min)", "SAP Before TT(min)", "SAP After ST(min)", "SAP After TT(min)", "MH After ST(min)", "MH After TT(min)" };

                                bool containsHyphen = false;

                                // Iterate through each row and check the specified columns for a hyphen
                                foreach (DataGridViewRow row in UploadTemplateDatagrid.Rows)
                                {
                                    // Validate each specified column in the row
                                    foreach (string columnName in columnsToValidate)
                                    {
                                        if (row.Cells[columnName].Value != null &&
                                            row.Cells[columnName].Value.ToString().Contains("-"))
                                        {
                                            containsHyphen = true;
                                            break;
                                        }
                                    }

                                    // If any column contains a hyphen, stop further checks and show the message
                                    if (containsHyphen)
                                    {
                                        break;
                                    }
                                }

                                // If any row contains a hyphen in any of the specified columns, show a message and prevent insertion
                                if (containsHyphen)
                                {
                                    MessageBox.Show("Uploading failed. One or more cells in the specified columns contain a hyphen (-), please check the template.",
                                                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                obj.MassProduction = dt.Rows[i]["Mass Production (Month Start)"].ToString();
                                    obj.Plant = dt.Rows[i]["Plant"].ToString();
                                    Section = dt.Rows[i]["SECTION"].ToString();


                                    obj.ItemNameSAP = dt.Rows[i]["Item Name (SAP)"].ToString();
                                    obj.SAP_BeforeST = dt.Rows[i]["SAP Before ST(min)"].ToString();
                                    obj.SAP_BeforeTT = dt.Rows[i]["SAP Before TT(min)"].ToString();

                                    obj.SAP_AfterST = dt.Rows[i]["SAP After ST(min)"].ToString();
                                    obj.SAP_AfterTT = dt.Rows[i]["SAP After TT(min)"].ToString();
                                    obj.ItemCodeMH = dt.Rows[i]["Item Code (MH)"].ToString();

                                    obj.MH_AfterST = dt.Rows[i]["MH After ST(min)"].ToString();
                                    obj.MH_AfterTT = dt.Rows[i]["MH After TT(min)"].ToString();

                                }

                            }
                            else { }



                            if (obj.ItemCodeMH != "")
                            {
                                // Column names to validate (assuming we are validating "Column2" and "Column3")
                                string[] columnsToValidate = { "Mass Production (Month Start)", "SAP After ST(min)", "SAP After TT(min)", "MH Before ST(min)", "MH Before TT(min)", "MH After ST(min)", "MH After TT(min)" };
                                bool containsHyphen = false;

                                // Iterate through each row and check the specified columns for a hyphen
                                foreach (DataGridViewRow row in UploadTemplateDatagrid.Rows)
                                {
                                    // Validate each specified column in the row
                                    foreach (string columnName in columnsToValidate)
                                    {
                                        if (row.Cells[columnName].Value != null &&
                                            row.Cells[columnName].Value.ToString().Contains("-"))
                                        {
                                            containsHyphen = true;
                                            break;
                                        }
                                    }

                                    // If any column contains a hyphen, stop further checks and show the message
                                    if (containsHyphen)
                                    {
                                        break;
                                    }
                                }

                                // If any row contains a hyphen in any of the specified columns, show a message and prevent insertion
                                if (containsHyphen)
                                {
                                    MessageBox.Show("Uploading failed. One or more cells in the specified columns contain a hyphen (-), please check the template.",
                                                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    obj.MassProduction = dt.Rows[i]["Mass Production (Month Start)"].ToString();
                                    obj.Plant = dt.Rows[i]["Plant"].ToString();
                                    Section = dt.Rows[i]["SECTION"].ToString();


                                    obj.SAP_AfterST = dt.Rows[i]["SAP After ST(min)"].ToString();
                                    obj.SAP_AfterTT = dt.Rows[i]["SAP After TT(min)"].ToString();
                                    //obj.ItemCodeMH = dt.Rows[i]["Item Code (MH)"].ToString();


                                    obj.ItemNameMH = dt.Rows[i]["Item Name (MH)"].ToString();
                                    obj.MH_BeforeST = dt.Rows[i]["MH Before ST(min)"].ToString();
                                    obj.MH_BeforeTT = dt.Rows[i]["MH Before TT(min)"].ToString();
                                    obj.MH_AfterST = dt.Rows[i]["MH After ST(min)"].ToString();
                                    obj.MH_AfterTT = dt.Rows[i]["MH After TT(min)"].ToString();

                                }
                            }
                            else { }

                            obj.EffectivityDate = dt.Rows[i]["Effectivity Date"].ToString();
                            obj.Reason = dt.Rows[i]["Reason"].ToString();
                            obj.Remarks = dt.Rows[i]["Remarks"].ToString();
                            obj.Section = Dashboard.SectionText.Replace("BIPH-", "");
                            obj.DateApplied = DateTime.Now.ToString();
                            obj.AppliedBy = LoginForm.FirstName + " " + LoginForm.LastName;
                        }

                        list.Add(obj);
                    }

                    UploadTemplateDatagrid.DataSource = list;

                    ////Hide Specific colum that not belong to selected category
                    //if (CategoryDropdown.Text == "MH New ST Model List Form")
                    //{
                    //    UploadTemplateDatagrid.Columns["ReferenceNo"].Visible = false;
                    //    UploadTemplateDatagrid.Columns["ApplicationFormNo"].Visible = false;
                    //    UploadTemplateDatagrid.Columns["SAP_BeforeST"].Visible = false;
                    //    UploadTemplateDatagrid.Columns["SAP_BeforeTT"].Visible = false;
                    //    UploadTemplateDatagrid.Columns["SAP_AfterST"].Visible = false;
                    //    UploadTemplateDatagrid.Columns["SAP_AfterTT"].Visible = false;
                    //    UploadTemplateDatagrid.Columns["MH_BeforeST"].Visible = false;
                    //    UploadTemplateDatagrid.Columns["MH_BeforeTT"].Visible = false;
                    //    UploadTemplateDatagrid.Columns["MH_AfterST"].Visible = false;
                    //    UploadTemplateDatagrid.Columns["MH_AfterTT"].Visible = false;

                    //}
                    //else
                    //{
                    //    UploadTemplateDatagrid.Columns["ReferenceNo"].Visible = false;
                    //    UploadTemplateDatagrid.Columns["ApplicationFormNo"].Visible = false;
                    //    UploadTemplateDatagrid.Columns["SAPST"].Visible = false;
                    //    UploadTemplateDatagrid.Columns["SAPTT"].Visible = false;
                    //    UploadTemplateDatagrid.Columns["MHST"].Visible = false;
                    //    UploadTemplateDatagrid.Columns["MHTT"].Visible = false;
                    //}
                   
                }
            }
            else if (ApplicationForm.ApplicationFormType == "WC/CC")
            {

                ReferenceNo = ApplicationForm.ApplicationFormType + "-" + CategoryDropdown.Text + "-" + Dashboard.SectionText.Replace("BIPH-", "") + "_" + DateTime.Now.ToString("yyyyMMddhhmm");

                DataTable dt = tableCollection[SheetDropdownList.SelectedItem.ToString()];


                //List<WCCC_Class> list = new List<WCCC_Class>();

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //select no.
                SqlCommand SelectApplicationFormNo = new SqlCommand("SP_SelectApplicationFormNo", con);
                SelectApplicationFormNo.CommandType = CommandType.StoredProcedure;
                SelectApplicationFormNo.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                SelectApplicationFormNo.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                SelectApplicationFormNo.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter da = new SqlDataAdapter(SelectApplicationFormNo);
                DataTable dTable = new DataTable();
                da.Fill(dTable);
                con.Close();

                if (dTable.Rows.Count > 0)
                {
                    con.Open();
                    SqlDataReader reader = SelectApplicationFormNo.ExecuteReader();
                    if (reader.Read())
                    {
                        ApplicationFormNumber = reader[0].ToString(); //ApplicationFormNo Column

                        reader.Close();
                    }
                }
                else
                {
                    ApplicationFormNumber = "0";
                }

                int Number = 0;

                if (CategoryDropdown.Text == "Work Center New")
                {
                    if (dt != null)
                    {
                        List<WCCC_New_Class> list = new List<WCCC_New_Class>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //WCCC_Class obj = new WCCC_Class();

                            WCCC_New_Class obj = new WCCC_New_Class();

                            obj.ReferenceNo = ReferenceNo;
                            obj.ApplicationFormNo = (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString();
                            obj.No = (Number += 1).ToString();
                            obj.WorkCenterCode = dt.Rows[i]["Workcenter Code"].ToString();
                            obj.WorkCenterName = dt.Rows[i]["Workcenter Name"].ToString();
                            obj.Shift = dt.Rows[i]["Shift"].ToString();
                            obj.CostCenterCode = dt.Rows[i]["Costcenter Code"].ToString();
                            obj.CostCenterName = dt.Rows[i]["Costcenter Name"].ToString();
                            obj.CostCenterGrouping = dt.Rows[i]["Costcenter Grouping"].ToString();
                            obj.Plant = dt.Rows[i]["Plant"].ToString();
                            obj.ReasonOfApplication = ReasonOfApplicationTextBox.Text;
                            obj.Effectivity = dt.Rows[i]["Effectivity"].ToString();
                            obj.Remarks = dt.Rows[i]["Remarks"].ToString();
                            obj.Section = Dashboard.SectionText.Replace("BIPH-", "");
                            obj.DateApplied = DateTime.Now.ToString();
                            obj.AppliedBy = LoginForm.FirstName + " " + LoginForm.LastName;

                            list.Add(obj);
                        }

                        UploadTemplateDatagrid.DataSource = list;


                        UploadTemplateDatagrid.Columns["ReferenceNo"].Visible = false;
                        UploadTemplateDatagrid.Columns["ApplicationFormNo"].Visible = false;
                        UploadTemplateDatagrid.Columns["ReasonOfApplication"].Visible = false;
                        UploadTemplateDatagrid.Columns["Section"].Visible = false;
                        UploadTemplateDatagrid.Columns["DateApplied"].Visible = false;
                        UploadTemplateDatagrid.Columns["AppliedBy"].Visible = false;

                    }
                }
                else if (CategoryDropdown.Text == "Cost Center New")
                {
                    if (dt != null)
                    {
                        List<WCCC_New_Class> list = new List<WCCC_New_Class>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //WCCC_Class obj = new WCCC_Class();

                            WCCC_New_Class obj = new WCCC_New_Class();

                            obj.ReferenceNo = ReferenceNo;
                            obj.ApplicationFormNo = (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString();
                            obj.No = (Number += 1).ToString();
                            obj.CostCenterCode = dt.Rows[i]["Costcenter Code"].ToString();
                            obj.CostCenterName = dt.Rows[i]["Costcenter Name"].ToString();
                            obj.Plant = dt.Rows[i]["Plant"].ToString();
                            obj.WorkCenterCode = dt.Rows[i]["Workcenter Code"].ToString();
                            obj.WorkCenterName = dt.Rows[i]["Workcenter Name"].ToString();
                            obj.CostCenterGrouping = dt.Rows[i]["Costcenter Grouping"].ToString();
                            obj.Shift = dt.Rows[i]["Shift"].ToString();
                            obj.ReasonOfApplication = ReasonOfApplicationTextBox.Text;
                            obj.Effectivity = dt.Rows[i]["Effectivity"].ToString();
                            obj.Remarks = dt.Rows[i]["Remarks"].ToString();
                            obj.Section = Dashboard.SectionText.Replace("BIPH-", "");
                            obj.DateApplied = DateTime.Now.ToString();
                            obj.AppliedBy = LoginForm.FirstName + " " + LoginForm.LastName;

                            list.Add(obj);
                        }

                        UploadTemplateDatagrid.DataSource = list;

                        UploadTemplateDatagrid.Columns["ReferenceNo"].Visible = false;
                        UploadTemplateDatagrid.Columns["ApplicationFormNo"].Visible = false;
                        UploadTemplateDatagrid.Columns["ReasonOfApplication"].Visible = false;
                        UploadTemplateDatagrid.Columns["Section"].Visible = false;
                        UploadTemplateDatagrid.Columns["DateApplied"].Visible = false;
                        UploadTemplateDatagrid.Columns["AppliedBy"].Visible = false;
                    }
                }
                else if (CategoryDropdown.Text == "Work Center Revision")
                {
                    if (dt != null)
                    {
                        List<WC_Revision_Class> list = new List<WC_Revision_Class>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //WCCC_Class obj = new WCCC_Class();

                            WC_Revision_Class obj = new WC_Revision_Class();

                            obj.ReferenceNo = ReferenceNo;
                            obj.ApplicationFormNo = (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString();
                            obj.No = (Number += 1).ToString();

                            obj.WorkCenterCode_Old = dt.Rows[i]["Workcenter Code (Old)"].ToString();
                            obj.Shift_Old = dt.Rows[i]["Shift (Old)"].ToString();

                            if (obj.WorkCenterCode_Old != "" && obj.Shift_Old != "")
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                //select SAP ST from SAP master data
                                SqlCommand SelectWorkcenter = new SqlCommand("SP_SelectWorkCenterCodeFromWCMasterData", con);
                                SelectWorkcenter.CommandType = CommandType.StoredProcedure;
                                SelectWorkcenter.Parameters.AddWithValue("@Procedure", "WorkCenter");
                                SelectWorkcenter.Parameters.AddWithValue("@WorcenterCode", obj.WorkCenterCode_Old);
                                SelectWorkcenter.Parameters.AddWithValue("@Shift", obj.Shift_Old);
                                SelectWorkcenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SqlDataAdapter da2 = new SqlDataAdapter(SelectWorkcenter);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                con.Close();

                                if (dt2.Rows.Count > 0)
                                {
                                    con.Open();

                                    SqlDataReader reader = SelectWorkcenter.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        WorkcenterCodeOld = reader["WorkCenterCode"].ToString();
                                        WorkcenterNameOld = reader["WorkCenterName"].ToString();
                                        PlantOld = reader["Plant"].ToString();
                                        CostcenterGrouping_A = reader["CostCenterGrouping_A"].ToString();
                                        CostcenterGrouping_B = reader["CostCenterGrouping_B"].ToString();

                                        reader.Close();
                                    }
                                }


                                obj.WorkCenterName_Old = dt.Rows[i]["Workcenter Name (OLD)"].ToString();
                                obj.Plant_Old = dt.Rows[i]["Plant (Old)"].ToString();
                                obj.CostCenterGrouping_Old = dt.Rows[i]["Costcenter Grouping (Old)"].ToString();
                                obj.WorkCenterCode_New = dt.Rows[i]["Workcenter Code (New)"].ToString();
                                obj.WorkCenterName_New = dt.Rows[i]["Workcenter Name (New)"].ToString();
                                obj.Shift_New = dt.Rows[i]["Shift (New)"].ToString();
                                obj.Plant_New = dt.Rows[i]["Plant (New)"].ToString();
                                obj.CostCenterGrouping_New = dt.Rows[i]["Costcenter Grouping (New)"].ToString();
                            }


                            obj.ReasonOfApplication = ReasonOfApplicationTextBox.Text;
                            obj.Effectivity = dt.Rows[i]["Effectivity"].ToString();
                            obj.Remarks = dt.Rows[i]["Remarks"].ToString();
                            obj.Section = Dashboard.SectionText.Replace("BIPH-", "");
                            obj.DateApplied = DateTime.Now.ToString();
                            obj.AppliedBy = LoginForm.FirstName + " " + LoginForm.LastName;

                            list.Add(obj);
                        }

                        UploadTemplateDatagrid.DataSource = list;

                        UploadTemplateDatagrid.Columns["ReferenceNo"].Visible = false;
                        UploadTemplateDatagrid.Columns["ApplicationFormNo"].Visible = false;
                        UploadTemplateDatagrid.Columns["ReasonOfApplication"].Visible = false;
                        UploadTemplateDatagrid.Columns["Section"].Visible = false;
                        UploadTemplateDatagrid.Columns["DateApplied"].Visible = false;
                        UploadTemplateDatagrid.Columns["AppliedBy"].Visible = false;

                    }
                }
                else if (CategoryDropdown.Text == "Cost Center Revision")
                {
                    if (dt != null)
                    {
                        List<CC_Revision_Class> list = new List<CC_Revision_Class>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //WCCC_Class obj = new WCCC_Class();

                            CC_Revision_Class obj = new CC_Revision_Class();

                            obj.ReferenceNo = ReferenceNo;
                            obj.ApplicationFormNo = (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString();
                            obj.No = (Number += 1).ToString();

                            obj.CostCenterCode_Old = dt.Rows[i]["Costcenter Code (Old)"].ToString();
                            obj.Shift_Old = dt.Rows[i]["Shift (Old)"].ToString();

                            if (obj.CostCenterCode_Old != "" && obj.Shift_Old != "")
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                //select SAP ST from SAP master data
                                SqlCommand SelectWorkcenter = new SqlCommand("SP_SelectCostCenterCodeFromWCMasterData", con);
                                SelectWorkcenter.CommandType = CommandType.StoredProcedure;
                                SelectWorkcenter.Parameters.AddWithValue("@Procedure", "CostCenter");
                                SelectWorkcenter.Parameters.AddWithValue("@CostcenterCode", obj.CostCenterCode_Old);
                                //SelectWorkcenter.Parameters.AddWithValue("@Shift", obj.Shift_Old);
                                SelectWorkcenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SqlDataAdapter da2 = new SqlDataAdapter(SelectWorkcenter);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                con.Close();

                                if (dt2.Rows.Count > 0)
                                {
                                    con.Open();

                                    SqlDataReader reader = SelectWorkcenter.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        CostcenterCodeOld = reader["CostCenterCode"].ToString();
                                        CostcenterNameOld = reader["CostCenterName"].ToString();
                                        PlantOld = reader["Plant"].ToString();
                                        CostcenterGrouping_A = reader["CostCenterGrouping_A"].ToString();
                                        CostcenterGrouping_B = reader["CostCenterGrouping_B"].ToString();

                                        reader.Close();
                                    }
                                }

                                obj.CostCenterName_Old = dt.Rows[i]["Costcenter Name (OLD)"].ToString();
                                obj.Plant_Old = dt.Rows[i]["Plant (Old)"].ToString();
                                obj.CostCenterGrouping_Old = dt.Rows[i]["Costcenter Grouping (Old)"].ToString();
                                obj.CostCenterCode_New = dt.Rows[i]["Costcenter Code (New)"].ToString();
                                obj.CostCenterName_New = dt.Rows[i]["Costcenter Name (New)"].ToString();
                                obj.Shift_New = dt.Rows[i]["Shift (New)"].ToString();
                                obj.Plant_New = dt.Rows[i]["Plant (New)"].ToString();
                                obj.CostCenterGrouping_New = dt.Rows[i]["Costcenter Grouping (New)"].ToString();

                            }


                            obj.ReasonOfApplication = ReasonOfApplicationTextBox.Text;
                            obj.Effectivity = dt.Rows[i]["Effectivity"].ToString();
                            obj.Remarks = dt.Rows[i]["Remarks"].ToString();
                            obj.Section = Dashboard.SectionText.Replace("BIPH-", "");
                            obj.DateApplied = DateTime.Now.ToString();
                            obj.AppliedBy = LoginForm.FirstName + " " + LoginForm.LastName;

                            list.Add(obj);
                        }

                        UploadTemplateDatagrid.DataSource = list;

                        UploadTemplateDatagrid.Columns["ReferenceNo"].Visible = false;
                        UploadTemplateDatagrid.Columns["ApplicationFormNo"].Visible = false;
                        UploadTemplateDatagrid.Columns["ReasonOfApplication"].Visible = false;
                        UploadTemplateDatagrid.Columns["Section"].Visible = false;
                        UploadTemplateDatagrid.Columns["DateApplied"].Visible = false;
                        UploadTemplateDatagrid.Columns["AppliedBy"].Visible = false;
                    }
                }
                else if (CategoryDropdown.Text == "Work Center Deletion")
                {
                    if (dt != null)
                    {
                        List<WCCC_Deletion_Class> list = new List<WCCC_Deletion_Class>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //WCCC_Class obj = new WCCC_Class();

                            WCCC_Deletion_Class obj = new WCCC_Deletion_Class();

                            obj.ReferenceNo = ReferenceNo;
                            obj.ApplicationFormNo = (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString();
                            obj.No = (Number += 1).ToString();

                            obj.WorkCenterCode = dt.Rows[i]["Workcenter Code"].ToString();
                            obj.WorkCenterName = dt.Rows[i]["Workcenter Name"].ToString();
                            obj.Shift = dt.Rows[i]["Shift"].ToString();
                            obj.Plant = dt.Rows[i]["Plant"].ToString();

                            obj.CostCenterGrouping = dt.Rows[i]["Costcenter Grouping"].ToString(); ;

                            obj.ReasonOfApplication = ReasonOfApplicationTextBox.Text;
                            obj.Effectivity = dt.Rows[i]["Effectivity"].ToString();
                            obj.Remarks = dt.Rows[i]["Remarks"].ToString();
                            obj.Section = Dashboard.SectionText.Replace("BIPH-", "");
                            obj.DateApplied = DateTime.Now.ToString();
                            obj.AppliedBy = LoginForm.FirstName + " " + LoginForm.LastName;

                            list.Add(obj);
                        }

                        UploadTemplateDatagrid.DataSource = list;

                        UploadTemplateDatagrid.Columns["ReferenceNo"].Visible = false;
                        UploadTemplateDatagrid.Columns["ApplicationFormNo"].Visible = false;
                        UploadTemplateDatagrid.Columns["CostCenterCode"].Visible = false;
                        UploadTemplateDatagrid.Columns["CostCenterName"].Visible = false;
                        UploadTemplateDatagrid.Columns["ReasonOfApplication"].Visible = false;
                        UploadTemplateDatagrid.Columns["Section"].Visible = false;
                        UploadTemplateDatagrid.Columns["DateApplied"].Visible = false;
                        UploadTemplateDatagrid.Columns["AppliedBy"].Visible = false;

                        UploadTemplateDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    }
                }
                else if (CategoryDropdown.Text == "Cost Center Deletion")
                {

                    if (dt != null)
                    {
                        List<WCCC_Deletion_Class> list = new List<WCCC_Deletion_Class>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            WCCC_Deletion_Class obj = new WCCC_Deletion_Class();

                            obj.ReferenceNo = ReferenceNo;
                            obj.ApplicationFormNo = (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString();
                            obj.No = (Number += 1).ToString();

                            obj.CostCenterCode = dt.Rows[i]["Costcenter Code"].ToString();
                            obj.Shift = dt.Rows[i]["Shift"].ToString();
                            obj.CostCenterName = dt.Rows[i]["Costcenter Name"].ToString();
                            obj.Plant = dt.Rows[i]["Plant"].ToString();
                            obj.CostCenterGrouping = dt.Rows[i]["Costcenter Grouping"].ToString();

                            //if (obj.WorkCenterCode != "")
                            //{
                            //    if (con.State == ConnectionState.Closed)
                            //    {
                            //        con.Open();
                            //    }

                            //    //select SAP ST from SAP master data
                            //    SqlCommand SelectWorkcenter = new SqlCommand("SP_SelectCostCenterCodeFromWCMasterData", con);
                            //    SelectWorkcenter.CommandType = CommandType.StoredProcedure;
                            //    SelectWorkcenter.Parameters.AddWithValue("@Procedure", "CostCenter");
                            //    SelectWorkcenter.Parameters.AddWithValue("@CostCenterCode", obj.CostCenterCode);
                            //    SelectWorkcenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            //    SqlDataAdapter da2 = new SqlDataAdapter(SelectWorkcenter);
                            //    DataTable dt2 = new DataTable();
                            //    da2.Fill(dt2);
                            //    con.Close();

                            //    if (dt2.Rows.Count > 0)
                            //    {
                            //        con.Open();

                            //        SqlDataReader reader = SelectWorkcenter.ExecuteReader();
                            //        if (reader.Read())
                            //        {
                            //            CostCenterName = reader["CostCenterName"].ToString();
                            //            Plant = reader["Plant"].ToString();
                            //            CostcenterGrouping_A = reader["CostCenterGrouping_A"].ToString();
                            //            CostcenterGrouping_B = reader["CostCenterGrouping_B"].ToString();

                            //            reader.Close();
                            //        }
                            //    }


                            //    obj.CostCenterName = CostCenterName;
                            //    obj.Shift = Shift;
                            //    obj.Plant = Plant;
                            //    obj.CostCenterGrouping = CostcenterGrouping_A + " " + CostcenterGrouping_B;
                            //}


                            obj.ReasonOfApplication = ReasonOfApplicationTextBox.Text;
                            obj.Effectivity = dt.Rows[i]["Effectivity"].ToString();
                            obj.Remarks = dt.Rows[i]["Remarks"].ToString();
                            obj.Section = Dashboard.SectionText.Replace("BIPH-", "");
                            obj.DateApplied = DateTime.Now.ToString();
                            obj.AppliedBy = LoginForm.FirstName + " " + LoginForm.LastName;

                            list.Add(obj);
                        }

                        UploadTemplateDatagrid.DataSource = list;

                        UploadTemplateDatagrid.Columns["ReferenceNo"].Visible = false;
                        UploadTemplateDatagrid.Columns["ApplicationFormNo"].Visible = false;
                        UploadTemplateDatagrid.Columns["WorkCenterCode"].Visible = false;
                        UploadTemplateDatagrid.Columns["WorkCenterName"].Visible = false;
                        UploadTemplateDatagrid.Columns["Shift"].Visible = false;
                        UploadTemplateDatagrid.Columns["ReasonOfApplication"].Visible = false;
                        UploadTemplateDatagrid.Columns["Section"].Visible = false;
                        UploadTemplateDatagrid.Columns["DateApplied"].Visible = false;
                        UploadTemplateDatagrid.Columns["AppliedBy"].Visible = false;

                        UploadTemplateDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
            }
            else if (ApplicationForm.ApplicationFormType == "Open MH System")
            {

                ReferenceNo = ApplicationForm.ApplicationFormType + "-" + CategoryDropdown.Text + "-" + Dashboard.SectionText.Replace("BIPH-", "") + "_" + DateTime.Now.ToString("yyyyMMddhhmm");

                DataTable dt = tableCollection[SheetDropdownList.SelectedItem.ToString()];


                //List<WCCC_Class> list = new List<WCCC_Class>();

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //select no.
                SqlCommand SelectApplicationFormNo = new SqlCommand("SP_SelectApplicationFormNo", con);
                SelectApplicationFormNo.CommandType = CommandType.StoredProcedure;
                SelectApplicationFormNo.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                SelectApplicationFormNo.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                SelectApplicationFormNo.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter da = new SqlDataAdapter(SelectApplicationFormNo);
                DataTable dTable = new DataTable();
                da.Fill(dTable);
                con.Close();

                if (dTable.Rows.Count > 0)
                {
                    con.Open();
                    SqlDataReader reader = SelectApplicationFormNo.ExecuteReader();
                    if (reader.Read())
                    {
                        ApplicationFormNumber = reader[0].ToString(); //ApplicationFormNo Column

                        reader.Close();
                    }
                }
                else
                {
                    ApplicationFormNumber = "0";
                }

                int Number = 0;

                if (CategoryDropdown.Text == "Manpower/Man-hour")
                {
                    if (dt != null)
                    {
                        List<OpenMH_MH_MPCategory_Class> list = new List<OpenMH_MH_MPCategory_Class>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                          
                            OpenMH_MH_MPCategory_Class obj = new OpenMH_MH_MPCategory_Class();

                            obj.ReferenceNo = ReferenceNo;
                            obj.ApplicationFormNo = (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString();
                            obj.No = (Number += 1).ToString();
                            obj.Section = Dashboard.SectionText.Replace("BIPH-", "");

                            obj.Date = dt.Rows[i]["Date"].ToString();
                            obj.Category = dt.Rows[i]["Category"].ToString();
                            obj.CostCenterCode = dt.Rows[i]["Cost Center"].ToString();
                            obj.WorkCenterCode = dt.Rows[i]["Work Center"].ToString();
                            obj.Shift = dt.Rows[i]["Shift"].ToString();
                            obj.OperationTime_Old = dt.Rows[i]["Operation time (Old)"].ToString();
                            obj.DirectOperator_Old = dt.Rows[i]["Direct operator  (Old)"].ToString();
                            obj.SemiDirectOperator_Old = dt.Rows[i]["Semi-Direct operator  (Old) "].ToString();
                            obj.SemiIndirectOperator_Old = dt.Rows[i]["Semi-Indirect operator  (Old)"].ToString();
                            obj.TotalManpower_Old = dt.Rows[i]["Total Manpower (Old)"].ToString();
                            obj.TotalManhour_Old = dt.Rows[i]["Total Man-hour (Old)"].ToString();
                            obj.OperationTime_New = dt.Rows[i]["Operation time (New)"].ToString();
                            obj.DirectOperator_New = dt.Rows[i]["Direct operator  (New)"].ToString();
                            obj.SemiDirectOperator_New = dt.Rows[i]["Semi-Direct operator  (New)"].ToString();
                            obj.SemiIndirectOperator_New = dt.Rows[i]["Semi-Indirect operator  (New)"].ToString();
                            obj.TotalManpower_New = dt.Rows[i]["Total Manpower (New)"].ToString();
                            obj.TotalManhour_New = dt.Rows[i]["Total Man-hour (New)"].ToString();
                            obj.ReasonOfRevision = dt.Rows[i]["Reason of Revision"].ToString();

                            obj.DateApplied = DateTime.Now.ToString();
                            obj.AppliedBy = LoginForm.FirstName + " " + LoginForm.LastName;

                            list.Add(obj);
                        }

                        UploadTemplateDatagrid.DataSource = list;
                    }
                }
                else if (CategoryDropdown.Text == "Standard Time (ST mins)")
                {
                    if (dt != null)
                    {
                        List<OpenMH_StandardTime_Class> list = new List<OpenMH_StandardTime_Class>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string itemCode = dt.Rows[i]["Item Code"].ToString()?.Trim();

                            // ❗ SKIP EMPTY ROW
                            if (string.IsNullOrWhiteSpace(itemCode))
                                continue;

                            OpenMH_StandardTime_Class obj = new OpenMH_StandardTime_Class();

                            obj.ReferenceNo = ReferenceNo;
                            obj.ApplicationFormNo = (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString();
                            obj.No = (Number += 1).ToString();
                            obj.Section = Dashboard.SectionText.Replace("BIPH-", "");

                            obj.ItemCode = dt.Rows[i]["Item Code"].ToString();

                            if (obj.ItemCode != "")
                            {
                                //if (con.State == ConnectionState.Closed)
                                //{
                                //    con.Open();
                                //}

                                    
                                //SqlCommand SelectItemCode = new SqlCommand("SP_SelectItemCodeFromOpemMHMasterData", con);
                                //SelectItemCode.CommandType = CommandType.StoredProcedure;
                                ////SelectCostcenter.Parameters.AddWithValue("@Procedure", "CostCenter");
                                //SelectItemCode.Parameters.AddWithValue("@ItemCOde", obj.ItemCode);
                                //SelectItemCode.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                //SqlDataAdapter da2 = new SqlDataAdapter(SelectItemCode);
                                //DataTable dt2 = new DataTable();
                                //da2.Fill(dt2);
                                //con.Close();

                                //if (dt2.Rows.Count > 0)
                                //{
                                //    con.Open();

                                //    SqlDataReader reader = SelectItemCode.ExecuteReader();
                                //    if (reader.Read())
                                //    {
                                           
                                //        OldST = reader["OldST"].ToString();

                                //        reader.Close();
                                //    }
                                //}

                                obj.Date = dt.Rows[i]["Date"].ToString();
                                obj.CostCenter = dt.Rows[i]["Cost Center"].ToString();
                                obj.WorkCenter = dt.Rows[i]["Work Center"].ToString();
                                obj.Shift = dt.Rows[i]["Shift"].ToString();

                                obj.Old = dt.Rows[i]["(Old) ST"].ToString();
                                obj.New = dt.Rows[i]["(New) ST"].ToString();
                                obj.Difference = dt.Rows[i]["Difference"].ToString();
                                obj.ReasonOfRevision = dt.Rows[i]["Reason of Revision"].ToString();
                            }
                            

                            obj.DateApplied = DateTime.Now.ToString();
                            obj.AppliedBy = LoginForm.FirstName + " " + LoginForm.LastName;

                            list.Add(obj);
                        }

                        UploadTemplateDatagrid.DataSource = list;
                    }
                }
                else if (CategoryDropdown.Text == "Linestop/Loss Man-hour/Loss Factor")
                {
                  
                    if (dt != null)
                    {
                        List<OpenMH_LineStop_LossMH_LossFactor_Class> list = new List<OpenMH_LineStop_LossMH_LossFactor_Class>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            OpenMH_LineStop_LossMH_LossFactor_Class obj = new OpenMH_LineStop_LossMH_LossFactor_Class();

                            obj.ReferenceNo = ReferenceNo;
                            obj.ApplicationFormNo = (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString();
                            obj.No = (Number += 1).ToString();
                            obj.Section = Dashboard.SectionText.Replace("BIPH-", "");

                            obj.Date = dt.Rows[i]["Date"].ToString();
                            obj.CostCenterCode = dt.Rows[i]["Cost Center"].ToString();
                            obj.WorkCenterCode = dt.Rows[i]["Work Center"].ToString();
                            obj.Shift = dt.Rows[i]["Shift"].ToString();
                            obj.LineStopContentDetail_Old = dt.Rows[i]["Line Stop Content Detail (Old)"].ToString();
                            obj.LossFactor_Old = dt.Rows[i]["Loss Factor (Old)"].ToString();
                            obj.StopTime_Old = dt.Rows[i]["Stop Time (Old)"].ToString();
                            obj.DirectOperator_Old = dt.Rows[i]["Direct Operator (Old)"].ToString();
                            obj.SemiDirectEmployee_Old = dt.Rows[i]["Semi-direct Employee (Old)"].ToString();
                            obj.LossManhour_Old = dt.Rows[i]["Loss Manhour (Old)"].ToString();
                            obj.LineStopContentDetail_New = dt.Rows[i]["Line Stop Content Detail (New)"].ToString();
                            obj.LossFactor_New = dt.Rows[i]["Loss Factor (New)"].ToString();
                            obj.StopTime_New = dt.Rows[i]["Stop Time (New)"].ToString();
                            obj.DirectOperator_New = dt.Rows[i]["Direct Operator (New)"].ToString();
                            obj.SemiDirectEmployee_New = dt.Rows[i]["Semi-direct Employee (New)"].ToString();
                            obj.LossManhour_New = dt.Rows[i]["Loss Manhour (New)"].ToString();

                            obj.ReasonOfRevision = dt.Rows[i]["Reason of Revision"].ToString();

                            obj.DateApplied = DateTime.Now.ToString();
                            obj.AppliedBy = LoginForm.FirstName + " " + LoginForm.LastName;

                            list.Add(obj);
                        }

                        UploadTemplateDatagrid.DataSource = list;
                    }
                }
            }

        }//-->End

 

        private void CategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilePath.Clear();
            SheetDropdownList.Items.Clear();
            UploadTemplateDatagrid.DataSource = null;
        }

        private void UploadSTTemplateDatagrid_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void UploadTemplateDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (ApplicationForm.ApplicationFormType == "ST")
            {
                UploadTemplateDatagrid.Columns["SAP_BeforeST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                UploadTemplateDatagrid.Columns["SAP_BeforeTT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                UploadTemplateDatagrid.Columns["SAP_AfterST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                UploadTemplateDatagrid.Columns["SAP_AfterTT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                UploadTemplateDatagrid.Columns["MH_BeforeST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                UploadTemplateDatagrid.Columns["MH_BeforeTT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                UploadTemplateDatagrid.Columns["MH_AfterST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                UploadTemplateDatagrid.Columns["MH_AfterTT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            
           
        }
    }
}
