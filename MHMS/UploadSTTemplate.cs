using ExcelDataReader;
using MHMS.Class;
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
    public partial class UploadSTTemplate : Form
    {

        //Connection String
        static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(MHMS2_Conn);

        public UploadSTTemplate()
        {
            InitializeComponent();
        }

        //Table collection
        DataTableCollection tableCollection;

        private void UploadSTTemplate_Load(object sender, EventArgs e)
        {
            AddCategoryPerApplication();
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
                
                InsertSTApplication();
            }
        }

        
        //string MD_Section;
        //string MD_MassProduction;
        //string MD_Plant;
        //string MD_ItemCodeSAP;
        //string MD_ItemNameSAP;
        //string MD_SAPBeforeST;
        //string MD_SAPAfterST;
        //string MD_SAPBeforeTT;
        //string MD_SAPAfterTT;
        //string MD_ItemCodeMH;
        //string MD_ItemNameMH;
        //string MD_MHBeforeST;
        //string MD_MHAfterST;
        //string MD_MHBeforeTT;
        //string MD_MHAfterTT;
        string ApplicationFormNumber;
    
        private void InsertSTApplication()
        {

            //int Number = 0;

            if (CategoryDropdown.Text == "Annual ST Change")
            {

                DapperPlusManager.Entity<ST_Class>().Table("TBL_AnnualChangeST");
                List<ST_Class> UploadSTApplication = UploadSTTemplateDatagrid.DataSource as List<ST_Class>;
                if (UploadSTApplication != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPH1131;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(UploadSTApplication);

                    }
                }

                SendSTApplicationEmail();

                
                //Insert ST Application For approval
                con.Open();
                SqlCommand InsertSTApplicationApproval = new SqlCommand("SP_InsertSTApplicationApproval", con);
                InsertSTApplicationApproval.CommandType = CommandType.StoredProcedure;
                InsertSTApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                InsertSTApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                InsertSTApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                InsertSTApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                InsertSTApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                InsertSTApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertSTApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section MGR");
                InsertSTApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                InsertSTApplicationApproval.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Annual ST Change uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);


                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadSTTemplateDatagrid.DataSource = null;
                this.Close();

            }
            else if (CategoryDropdown.Text == "MH Change ST Model List Form - No BIL Approval")
            {
                DapperPlusManager.Entity<ST_Class>().Table("TBL_MHChangeSTModelListForm-NoBILApproval");
                List<ST_Class> UploadSTApplication = UploadSTTemplateDatagrid.DataSource as List<ST_Class>;
                if (UploadSTApplication != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPH1131;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(UploadSTApplication);

                        MessageBox.Show("MH Change ST Model List Form - No BIL Approval uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                SendSTApplicationEmail();


                //Insert ST Application For approval
                con.Open();
                SqlCommand InsertSTApplicationApproval = new SqlCommand("SP_InsertSTApplicationApproval", con);
                InsertSTApplicationApproval.CommandType = CommandType.StoredProcedure;
                InsertSTApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                InsertSTApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                InsertSTApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                InsertSTApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                InsertSTApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                InsertSTApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertSTApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section MGR");
                InsertSTApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                InsertSTApplicationApproval.ExecuteNonQuery();
                con.Close();

                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadSTTemplateDatagrid.DataSource = null;
                this.Close();

            }
            else if (CategoryDropdown.Text == "MH Change ST Model List Form")
            {
                DapperPlusManager.Entity<ST_Class>().Table("TBL_MHChangeSTModelListForm");
                List<ST_Class> UploadSTApplication = UploadSTTemplateDatagrid.DataSource as List<ST_Class>;
                if (UploadSTApplication != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPH1131;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(UploadSTApplication);

                        MessageBox.Show("MH Change ST Model List Form uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                SendSTApplicationEmail();


                //Insert ST Application For approval
                con.Open();
                SqlCommand InsertSTApplicationApproval = new SqlCommand("SP_InsertSTApplicationApproval", con);
                InsertSTApplicationApproval.CommandType = CommandType.StoredProcedure;
                InsertSTApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                InsertSTApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                InsertSTApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                InsertSTApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                InsertSTApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                InsertSTApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertSTApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section MGR");
                InsertSTApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                InsertSTApplicationApproval.ExecuteNonQuery();
                con.Close();

                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadSTTemplateDatagrid.DataSource = null;
                this.Close();
            }
            else if (CategoryDropdown.Text == "MH New ST Model List Form")
            {
                DapperPlusManager.Entity<ST_Class>().Table("TBL_MHNewSTModelListForm");
                List<ST_Class> UploadSTApplication = UploadSTTemplateDatagrid.DataSource as List<ST_Class>;
                if (UploadSTApplication != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPH1131;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(UploadSTApplication);

                        MessageBox.Show("MH New ST Model List Form uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                SendSTApplicationEmail();


                //Insert ST Application For approval
                con.Open();
                SqlCommand InsertSTApplicationApproval = new SqlCommand("SP_InsertSTApplicationApproval", con);
                InsertSTApplicationApproval.CommandType = CommandType.StoredProcedure;
                InsertSTApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                InsertSTApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                InsertSTApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                InsertSTApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                InsertSTApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                InsertSTApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertSTApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section MGR");
                InsertSTApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                InsertSTApplicationApproval.ExecuteNonQuery();
                con.Close();

                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadSTTemplateDatagrid.DataSource = null;
                this.Close();
            }

        }

        //================================================================<BreakLine>======================================================>>>

        private void SendSTApplicationEmail()
        {
            STApplicationEmailMessage();

        }

        string innerString;
        string FirstName;
        string LastName;
        string Email;
        string Addresses;
        private void STApplicationEmailMessage()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());

                foreach (DataRow row in dTable.Rows)
                {
                    FirstName = row["First Name"].ToString();
                    LastName = row["Last Name"].ToString();
                    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();
                    builder.Append("<h2 style='color: red'>TESTING ONLY!</h2>"); //For deletion after trial
                    builder.Append("<br>");

                    //builder.Append("Dear " + LastNameList + " san,");
                    builder.Append("Dear Section MGR");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's " + CategoryDropdown.Text + " Application form");
                    builder.Append("<br>");

                    //====================>>>>>>>>
                    if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                    {
                        builder.Append("<font color=blue>については、下記リンクを参照くださいインクカートリッジ年次変更ST機種一覧申請書</font>");
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "Type section here")
                    {
                        //Type other section translation here...
                    }

                    //====================>>>>>>>>




                    builder.Append("<br>");

                    builder.Append("For your checking and approval");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>確認と承認をお願いします</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(ReferenceNo);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                    {
                        builder.Append("<a href= " + @"\\APBIPHSH04\B1_BIPHCommon\08_PE\03_Policy Group\01_Man Hour\00_ST Application Form\Output\Ink Cartridge>" + "Ink Cartrdige Application Form" + "</a>"); //This is the link of approval form module
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "")
                    {
                        //Type code here for other section.
                    }

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
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

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", "arvin.caparros@brother-biph.com.ph");
                        //mail.Bcc.Add(new MailAddress("donnalie.balba@brother-biph.com.ph"));
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "FY." + CurrentYear + ": " + Dashboard.SectionText.Replace("BIPH-", "") + " section's " + CategoryDropdown .Text + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

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

        //string Plant;
        //string ItemCode;
        //string ItemName;
        //string Section;
        //string SAPBeforeST;
        //string SAPBeforeTT;
        //string MHBeforeST;
        //string MHBeforeTT;
        string ReferenceNo;
        private void SheetDropdownList_SelectedIndexChanged(object sender, EventArgs e)
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
                SqlCommand SelectSTApplicationFormNo = new SqlCommand("SP_SelectSTApplicationFormNo", con);
                SelectSTApplicationFormNo.CommandType = CommandType.StoredProcedure;
                SelectSTApplicationFormNo.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                SelectSTApplicationFormNo.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter da = new SqlDataAdapter(SelectSTApplicationFormNo);
                DataTable dTable = new DataTable();
                da.Fill(dTable);
                con.Close();

                if (dTable.Rows.Count > 0)
                {
                    con.Open();
                    SqlDataReader reader = SelectSTApplicationFormNo.ExecuteReader();
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

                    ST_Class obj = new ST_Class();


                    if (CategoryDropdown.Text == "MH New ST Model List Form")
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
                    else
                    {
                        obj.ItemCodeSAP = dt.Rows[i]["Item Code (SAP)"].ToString();
                        obj.ReferenceNo = ReferenceNo;
                        obj.ApplicationFormNo = (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString();
                        obj.No = (Number += 1).ToString();

                        if (obj.ItemCodeSAP != "")
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            //select SAP ST from SAP master data
                            SqlCommand SelectSTItemCode = new SqlCommand("SP_SelectSTItemCodeFromMasterData", con);
                            SelectSTItemCode.CommandType = CommandType.StoredProcedure;
                            SelectSTItemCode.Parameters.AddWithValue("@Procedure", "SAP");
                            SelectSTItemCode.Parameters.AddWithValue("@ItemCode", obj.ItemCodeSAP);
                            SelectSTItemCode.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter da2 = new SqlDataAdapter(SelectSTItemCode);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);
                            con.Close();

                            if (dt2.Rows.Count > 0)
                            {
                                con.Open();

                                SqlDataReader reader = SelectSTItemCode.ExecuteReader();
                                if (reader.Read())
                                {
                                    ItemCode = reader["ItemCodeSAP"].ToString();
                                    ItemName = reader["ItemNameSAP"].ToString();
                                    Plant = reader["Plant"].ToString();
                                    Section = reader["Section"].ToString();
                                    SAPBeforeST = reader["SAPBeforeST"].ToString();
                                    SAPBeforeTT = reader["SAPBeforeTT"].ToString();

                                    reader.Close();
                                }
                            }

                            //obj.ApplicationFormNo = (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString();
                            //obj.No = (Number += 1).ToString();
                            obj.MassProduction = dt.Rows[i]["Mass Production (Month Start)"].ToString();
                            obj.Plant = Plant;

                            obj.ItemNameSAP = ItemName;
                            obj.SAP_BeforeST = SAPBeforeST;
                            obj.SAP_BeforeTT = SAPBeforeTT;
                            obj.SAP_AfterST = dt.Rows[i]["SAP After ST(min)"].ToString(); ;
                            obj.SAP_AfterTT = dt.Rows[i]["SAP After TT(min)"].ToString(); ;
                            obj.ItemCodeMH = dt.Rows[i]["Item Code (MH)"].ToString();
                            //obj.ItemNameMH = dt.Rows[i]["Item Name (MH)"].ToString();
                            //obj.MH_BeforeST = dt.Rows[i]["MH Before ST(min)"].ToString();
                            //obj.MH_BeforeTT = dt.Rows[i]["MH Before TT(min)"].ToString();
                            obj.MH_AfterST = dt.Rows[i]["MH After ST(min)"].ToString();
                            obj.MH_AfterTT = dt.Rows[i]["MH After TT(min)"].ToString();
                            //obj.EffectivityDate = dt.Rows[i]["Effectivity Date"].ToString();
                            //obj.Reason = dt.Rows[i]["Reason"].ToString();
                            //obj.Remarks = dt.Rows[i]["Remarks"].ToString();
                            //obj.Section = Dashboard.SectionText.Replace("BIPH-", "");

                        }

                        obj.ItemCodeMH = dt.Rows[i]["Item Code (MH)"].ToString();

                        if (obj.ItemCodeMH != "")
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            //select MH ST from SAP master data
                            SqlCommand SelectSTItemCode = new SqlCommand("SP_SelectSTItemCodeFromMasterData", con);
                            SelectSTItemCode.CommandType = CommandType.StoredProcedure;
                            SelectSTItemCode.Parameters.AddWithValue("@Procedure", "MH");
                            SelectSTItemCode.Parameters.AddWithValue("@ItemCode", obj.ItemCodeMH);
                            SelectSTItemCode.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter da3 = new SqlDataAdapter(SelectSTItemCode);
                            DataTable dt3 = new DataTable();
                            da3.Fill(dt3);
                            con.Close();

                            if (dt3.Rows.Count > 0)
                            {
                                con.Open();

                                SqlDataReader reader = SelectSTItemCode.ExecuteReader();
                                if (reader.Read())
                                {
                                    ItemCode = reader["ItemCodeMH"].ToString();
                                    ItemName = reader["ItemNameMH"].ToString();
                                    Plant = reader["Plant"].ToString();
                                    Section = reader["Section"].ToString();
                                    MHBeforeST = reader["MHBeforeST"].ToString();
                                    MHBeforeTT = reader["MHBeforeTT"].ToString();

                                    reader.Close();
                                }

                            }

                            //obj.ApplicationFormNo = (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString();
                            //obj.No = (Number += 1).ToString();
                            obj.MassProduction = dt.Rows[i]["Mass Production (Month Start)"].ToString();
                            obj.Plant = Plant;

                            //obj.ItemNameSAP = ItemName;
                            //obj.SAP_BeforeST = SAPBeforeST;
                            //obj.SAP_BeforeST = SAPBeforeTT;
                            obj.SAP_AfterST = dt.Rows[i]["SAP After ST(min)"].ToString();
                            obj.SAP_AfterTT = dt.Rows[i]["SAP After TT(min)"].ToString();
                            //obj.ItemCodeMH = dt.Rows[i]["Item Code (MH)"].ToString();
                            obj.ItemNameMH = dt.Rows[i]["Item Name (MH)"].ToString();
                            obj.MH_BeforeST = MHBeforeST;
                            obj.MH_BeforeTT = MHBeforeTT;
                            obj.MH_AfterST = dt.Rows[i]["MH After ST(min)"].ToString();
                            obj.MH_AfterTT = dt.Rows[i]["MH After TT(min)"].ToString();
                            
                        }

                        obj.EffectivityDate = dt.Rows[i]["Effectivity Date"].ToString();
                        obj.Reason = dt.Rows[i]["Reason"].ToString();
                        obj.Remarks = dt.Rows[i]["Remarks"].ToString();
                        obj.Section = Dashboard.SectionText.Replace("BIPH-", "");
                        obj.DateApplied = DateTime.Now.ToString();
                        obj.AppliedBy = LoginForm.FirstName + " " + LoginForm.LastName;
                    }

                    list.Add(obj);

                }

                UploadSTTemplateDatagrid.DataSource = list;

            }
        }

        private void CategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilePath.Clear();
            SheetDropdownList.Items.Clear();
            UploadSTTemplateDatagrid.DataSource = null;
        }

        private void UploadSTTemplateDatagrid_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
