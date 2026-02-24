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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z.Dapper.Plus;

namespace MHMS
{
    public partial class UpdatePartsLossData2 : Form
    {

        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public UpdatePartsLossData2()
        {
            InitializeComponent();
        }

        //Table collection
        DataTableCollection tableCollection;

        //======================================================================================================================>>>>>>>>>>

        private void DataUpdateTimer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateAndTimeLabel.Text = dateTime.ToString("dddd , MMM dd yyyy, hh : mm : ss");
        }

        //======================================================================================================================>>>>>>>>>>

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
            //SectionDropdown.ValueMember = "Section";
        }

        //======================================================================================================================>>>>>>>>>>

        private void UpdatePartsLossData2_Load(object sender, EventArgs e)
        {
            LoadSection(); //load section in dropdown list

            //MonthDropdwn.Text = DateTime.Now.ToString("MMMM");
            //FiscalYearDropdown.Text = DateTime.Now.ToString("yyyy");

            AddYears();

            SectionDropdown.Text = LoginForm.UserSection;//When this form loaded the dropdown list value automatically set equal to user section

            if (SectionLabel.Text == "BPS")
            {
                SectionDropdown.Enabled = true;
            }
            else
            {
                SectionDropdown.Enabled = false;
            }


            SelectLastInsertedDefectDate(); //Load the last upload date of defect data

            SelectGMMSAndSAPLastUpdated(); //Load the last upload date of Parts loss data
            
            if (LoginForm.UserSection == "Tape Cassette")
            {
                IDFTemplateButton.Visible = true;
            }
            else
            {
                IDFTemplateButton.Visible = false;
            }
        }

        //======================================================================================================================>>>>>>>>>>

        private void AddYears()
        {
            var currentYear = DateTime.Today.Year;
            for (int i = 3; i >= 0; i--)
            {
                // Now just add an entry that's the current year minus the counter
                FiscalYearDropdown.Items.Add((currentYear - i).ToString());
            }
        }

        //======================================================================================================================>>>>>>>>>>

        private void SelectLastInsertedDefectDate()
        {
            // -> SQL query to select User Account
            SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUpdatedDate = new SqlCommand("SP_SelectDefectLatestUpdatedDate", con);
            SelectUpdatedDate.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(SelectUpdatedDate);
            DataTable dt = new DataTable();
            da.Fill(dt);

            SqlDataReader reader = SelectUpdatedDate.ExecuteReader();

            while (reader.Read())
            {
                DefectLastUpdateDateLabel.Text = "Defect: " + reader["UploadDate"].ToString();
            }
        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void DownloadTemplateButton_Click(object sender, EventArgs e)
        {

            //SectionLabel.Text = Dashboard.SectionText.Replace("BIPH-", "");

            if (SectionDropdown.Text == "Tape Cassette")
            {
                MessageBox.Show("The template is preparing to open, Please click OK to continue.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\GMMS&SAP Template\Revised_010523\TC_GMMS & SAP Template.v4.xlsm");
               
            }
            else if (SectionDropdown.Text == "Ink Cartridge")
            {
                MessageBox.Show("The template is preparing to open, Please click OK to continue.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\GMMS&SAP Template\Revised_010523\IC_GMMS & SAP Template.v4.xlsm");
               
            }
            else if (SectionDropdown.Text == "Ink Head")
            {
                MessageBox.Show("The template is preparing to open, Please click OK to continue.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\GMMS&SAP Template\Revised_010523\IH_GMMS & SAP Template.v4.xlsm");
            }
            else if (SectionDropdown.Text == "Printer 1")
            {
                MessageBox.Show("The template is preparing to open, Please click OK to continue.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\GMMS&SAP Template\Revised_010523\PRT_GMMS & SAP Template.v4.xlsm");
            }
            else if (SectionDropdown.Text == "Printer 2")
            {
                MessageBox.Show("The template is preparing to open, Please click OK to continue.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\GMMS&SAP Template\Revised_010523\PRT_GMMS & SAP Template.v4.xlsm");
            }
            else if (SectionDropdown.Text == "P-Touch")
            {
                MessageBox.Show("The template is preparing to open, Please click OK to continue.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\GMMS&SAP Template\Revised_010523\PT_GMMS & SAP Template.v4.xlsm");
            }
        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void SelectDataTypeWhenSectionChange()
        {
            SectionLabel.Text = Dashboard.SectionText.Replace("BIPH-", "");

            if (SectionDropdown.Text == "Tape Cassette")
            {
                DataTypeDropdown.Enabled = true;
            }
            else
            {
                DataTypeDropdown.Enabled = false;
                DataTypeDropdown.Text = "GMMS & SAP";
            }

        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void SectionDropdown_TextChanged(object sender, EventArgs e)
        {
            SelectDataTypeWhenSectionChange();
        }

        //==================================================================================================================>>>>>>>>>>>>>
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

        //==================================================================================================================>>>>>>>>>>>>>

        private void SheetDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SheetDropdownList.ForeColor = Color.Black;

            DataTable dt = tableCollection[SheetDropdownList.SelectedItem.ToString()];

            if (DataTypeDropdown.Text == "Defect")
            {
                if (dt != null)
                {
                    List<Defect_Class> list = new List<Defect_Class>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Defect_Class obj = new Defect_Class();
                        
                        if (string.IsNullOrEmpty(dt.Rows[i]["Quality Issue No."].ToString()))
                            obj.QualityIssueNo = null; //--- this had to be set to null, not empty
                        else
                            obj.QualityIssueNo = dt.Rows[i]["Quality Issue No."].ToString();

                        //-----------------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Date"].ToString()))
                            obj.Date = null; //--- this had to be set to null, not empty
                        else
                            obj.Date = dt.Rows[i]["Date"].ToString();

                        //-----------------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Shift"].ToString()))
                            obj.Shift = null; //--- this had to be set to null, not empty
                        else
                            obj.Shift = dt.Rows[i]["Shift"].ToString();

                        //-----------------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Time"].ToString()))
                            obj.Time = null; //--- this had to be set to null, not empty
                        else
                            obj.Time = dt.Rows[i]["Time"].ToString();

                        //-----------------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Defect Category"].ToString()))
                            obj.DefectCategory = null; //--- this had to be set to null, not empty
                        else
                            obj.DefectCategory = dt.Rows[i]["Defect Category"].ToString();

                        //-----------------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Defect type"].ToString()))
                            obj.DefectType = null; //--- this had to be set to null, not empty
                        else
                            obj.DefectType = dt.Rows[i]["Defect type"].ToString();

                        //-----------------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Issue"].ToString()))
                            obj.Issue = null; //--- this had to be set to null, not empty
                        else
                            obj.Issue = dt.Rows[i]["Issue"].ToString();

                        //-----------------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Total number of defects [r]"].ToString()))
                            obj.TotalNumberOfDefect = null; //--- this had to be set to null, not empty
                        else
                            obj.TotalNumberOfDefect = dt.Rows[i]["Total number of defects [r]"].ToString();

                        //-----------------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Quantity Affected [n]"].ToString()))
                            obj.QuantityAffected = null; //--- this had to be set to null, not empty
                        else
                            obj.QuantityAffected = dt.Rows[i]["Quantity Affected [n]"].ToString();

                        //-----------------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Defect Rate [%]"].ToString()))
                            obj.DefectRate = null; //--- this had to be set to null, not empty
                        else
                            obj.DefectRate = dt.Rows[i]["Defect Rate [%]"].ToString();

                        //-----------------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Affected Qty. to be Sort"].ToString()))
                            obj.AffectedQtyToBeSort = null; //--- this had to be set to null, not empty
                        else
                            obj.AffectedQtyToBeSort = dt.Rows[i]["Affected Qty. to be Sort"].ToString();

                        //-----------------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Part Code"].ToString()))
                            obj.PartCode = null; //--- this had to be set to null, not empty
                        else
                            obj.PartCode = dt.Rows[i]["Part Code"].ToString();

                        //-----------------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Part Name"].ToString()))
                            obj.PartName = null; //--- this had to be set to null, not empty
                        else
                            obj.PartName = dt.Rows[i]["Part Name"].ToString();

                        //-----------------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Parts Lot Number"].ToString()))
                            obj.PartsLotNumber = null; //--- this had to be set to null, not empty
                        else
                            obj.PartsLotNumber = dt.Rows[i]["Parts Lot Number"].ToString();

                        //----------------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Affected Cassette PO Number"].ToString()))
                            obj.AffectedCassettePONumber = null; //--- this had to be set to null, not empty
                        else
                            obj.AffectedCassettePONumber = dt.Rows[i]["Affected Cassette PO Number"].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["PO Quantity"].ToString()))
                            obj.POQuantity = null; //--- this had to be set to null, not empty
                        else
                            obj.POQuantity = dt.Rows[i]["PO Quantity"].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Process Detected"].ToString()))
                            obj.ProcessDetected = null; //--- this had to be set to null, not empty
                        else
                            obj.ProcessDetected = dt.Rows[i]["Process Detected"].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Line #"].ToString()))
                            obj.Line = null; //--- this had to be set to null, not empty
                        else
                            obj.Line = dt.Rows[i]["Line #"].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Detected By:"].ToString()))
                            obj.DetectedBy = null; //--- this had to be set to null, not empty
                        else
                            obj.DetectedBy = dt.Rows[i]["Detected By:"].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Liable Area"].ToString()))
                            obj.LiableArea = null; //--- this had to be set to null, not empty
                        else
                            obj.LiableArea = dt.Rows[i]["Liable Area"].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Liable Person"].ToString()))
                            obj.LiablePerson = null; //--- this had to be set to null, not empty
                        else
                            obj.LiablePerson = dt.Rows[i]["Liable Person"].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Liable Line"].ToString()))
                            obj.LiableLine = null; //--- this had to be set to null, not empty
                        else
                            obj.LiableLine = dt.Rows[i]["Liable Line"].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Immediate Action"].ToString()))
                            obj.ImmediateAction = null; //--- this had to be set to null, not empty
                        else
                            obj.ImmediateAction = dt.Rows[i]["Immediate Action"].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Production PIC"].ToString()))
                            obj.ProductionPIC = null; //--- this had to be set to null, not empty
                        else
                            obj.ProductionPIC = dt.Rows[i]["Production PIC"].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["PQA In charge"].ToString()))
                            obj.PQAIncharge = null; //--- this had to be set to null, not empty
                        else
                            obj.PQAIncharge = dt.Rows[i]["PQA In charge"].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Technician PIC"].ToString()))
                            obj.TechnicianPIC = null; //--- this had to be set to null, not empty
                        else
                            obj.TechnicianPIC = dt.Rows[i]["Technician PIC"].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["QG PIC"].ToString()))
                            obj.QGPIC = null; //--- this had to be set to null, not empty
                        else
                            obj.QGPIC = dt.Rows[i]["QG PIC"].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Other Details"].ToString()))
                            obj.OtherDetails = null; //--- this had to be set to null, not empty
                        else
                            obj.OtherDetails = dt.Rows[i]["Other Details"].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Investigation"].ToString()))
                            obj.Investigation = null; //--- this had to be set to null, not empty
                        else
                            obj.Investigation = dt.Rows[i]["Investigation"].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(dt.Rows[i]["Details how they detect the N.G."].ToString()))
                            obj.DetailsHowTheyDetectTheNG = null; //--- this had to be set to null, not empty
                        else
                            obj.DetailsHowTheyDetectTheNG = dt.Rows[i]["Details how they detect the N.G."].ToString();

                        //-------------------------------------------------------------

                        if (string.IsNullOrEmpty(DateTime.Now.ToString("MM/dd/yyyy")))
                            obj.UploadDate = null; //--- this had to be set to null, not empty
                        else
                            obj.UploadDate = DateTime.Now.ToString("MM/dd/yyyy");


                        list.Add(obj);
                    }

                   
                    UploadPartsLossDatagrid.DataSource = list;
                    SheetDropdownList.ForeColor = Color.Black;
                }
            }
            else
            {
                if (dt != null)
                {
                    List<PartsLossData_Class> list = new List<PartsLossData_Class>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PartsLossData_Class obj = new PartsLossData_Class();
                        obj.OrderNo = dt.Rows[i]["Order No."].ToString();
                        obj.MaterialCode = dt.Rows[i]["Material Code"].ToString();
                        obj.MaterialText = dt.Rows[i]["Material Text"].ToString();
                        obj.Quantity = dt.Rows[i]["Quantity"].ToString();
                        obj.Amount = dt.Rows[i]["Amount"].ToString();
                        obj.MoveReason = dt.Rows[i]["MoveReason"].ToString();
                        obj.CreateDate = dt.Rows[i]["CreateDate"].ToString();
                        obj.DetailsReason = dt.Rows[i]["Details Reason"].ToString();
                        obj.ConfirmResult = dt.Rows[i]["Confirm Result"].ToString();
                        obj.Remarks = dt.Rows[i]["Remark"].ToString();
                        obj.ChargeObject = dt.Rows[i]["Charged object"].ToString();
                        obj.Creator = dt.Rows[i]["Creator"].ToString();
                        obj.Department = dt.Rows[i]["Dept"].ToString();
                        obj.AdjustedAmount = dt.Rows[i]["ADJ. AMOUNT"].ToString();
                        obj.UploadDate = DateTime.Now.ToString("MM/dd/yyyy");
                        //obj.Date = dt.Rows[i]["Date"].ToString();
                        //obj.DailyAdjustedAmount = dt.Rows[i]["Daily Adjusted Amount"].ToString();

                        if (SectionDropdown.Text == "P-Touch")
                        {
                            obj.Date = dt.Rows[i]["Date"].ToString();
                            obj.DailyAdjustedAmount = dt.Rows[i]["Daily Adjusted Amount"].ToString();
                            obj.Contributor = dt.Rows[i]["Contributor"].ToString();
                            obj.DailyLimit = dt.Rows[i]["Daily Limit"].ToString();
                            obj.MonthlyTarget = dt.Rows[i]["Monthly Target"].ToString();
                            obj.CumulativeLimit = dt.Rows[i]["Cumulative Limit"].ToString();
                            //obj.CumulativeEstimate = dt.Rows[i]["Cumulative Estimate"].ToString();
                            obj.CumulativeActual = dt.Rows[i]["Cumulative Actual"].ToString();
                            obj.CumulativeCombined = dt.Rows[i]["Cumulative (Combined)"].ToString();
                        }
                        else
                        {
                            obj.Date = dt.Rows[i]["Date"].ToString();
                            obj.DailyAdjustedAmount = dt.Rows[i]["Daily Adjusted Amount"].ToString();
                            obj.Contributor = dt.Rows[i]["Contributor"].ToString();
                            obj.MonthlyTarget = dt.Rows[i]["Monthly Target"].ToString();
                            obj.CumulativeLimit = dt.Rows[i]["Cumulative Limit"].ToString();
                            obj.CumulativeCombined = dt.Rows[i]["Cumulative (Combined)"].ToString();
                            obj.CumulativeType = dt.Rows[i]["Cumulative Type"].ToString();
                            obj.CumulativeActual = dt.Rows[i]["Cumulative Actual"].ToString();
                        }

                       
                        string Remark = dt.Rows[i]["Remark"].ToString();
                        string RemarksLastTwoLetters = Remark.Substring(Math.Max(0, Remark.Length - 2));
                        //string Remarks10thLetters = Remark.Substring(Math.Max(12, Remark.Length - 2));

                        if (dt.Rows[i]["Order No."].ToString() != "")
                        {
                            obj.Section = LoginForm.UserSection;
                            obj.UploadDate = DateTime.Now.ToShortDateString();

                            if (RemarksLastTwoLetters == "IC")
                            {
                                obj.SectionRemarks = "IC";
                            }
                            else if (RemarksLastTwoLetters == "TC")
                            {
                                obj.SectionRemarks = "TC";
                            }
                            else if (RemarksLastTwoLetters == "PE")
                            {
                                obj.SectionRemarks = "PE";
                            }
                            else if (RemarksLastTwoLetters == "EE")
                            {
                                obj.SectionRemarks = "EE";
                            }
                            else
                            {
                                obj.SectionRemarks = "NA";
                            }
                        }

                        list.Add(obj);
                    }

                    UploadPartsLossDatagrid.DataSource = list;
                    SheetDropdownList.ForeColor = Color.Black;
                }
            }
        }

        //==================================================================================================================>>>>>>>>>>>>>
        string SaveDirectory;
        string NewFileName;

        private void UploadButton_Click(object sender, EventArgs e)
        {
            if (SectionDropdown.Text == "Tape Cassette")
            {
                if (DataTypeDropdown.Text == "Defect")
                {
                    //File destination
                    SaveDirectory = @"\\apbiphbpswb01\RELEASE\COPQ Files\Defect Uploaded Files\";
                }
                else
                { 
                    //File destination
                    SaveDirectory = @"\\apbiphbpswb01\RELEASE\COPQ Files\Parst Loss Uploaded Files\Tape Cassette\";
                }
            }
            else if (SectionDropdown.Text == "Ink Cartridge")
            {
                //File destination
                SaveDirectory = @"\\apbiphbpswb01\RELEASE\COPQ Files\Parst Loss Uploaded Files\Ink Cartridge\";
            }
            else if (SectionDropdown.Text == "Printer")
            {
                //File destination
                SaveDirectory = @"\\apbiphbpswb01\RELEASE\COPQ Files\Parst Loss Uploaded Files\Printer\";
            }
            else if (SectionDropdown.Text == "P-Touch")
            {
                //File destination
                SaveDirectory = @"\\apbiphbpswb01\RELEASE\COPQ Files\Parst Loss Uploaded Files\P-Touch\";
            }
            else if (SectionDropdown.Text == "Ink Head")
            {
                //File destination
                SaveDirectory = @"\\apbiphbpswb01\RELEASE\COPQ Files\Parst Loss Uploaded Files\Ink Head\";
            }

            
            if (SectionDropdown.Text == "")
            {
                MessageBox.Show("Please select section.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (DataTypeDropdown.Text == "")
            {
                MessageBox.Show("Please select the data type.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (MonthDropdwn.Text == "")
            {
                MessageBox.Show("Please select the month based on create date of data to upload.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (FiscalYearDropdown.Text == "")
            {
                MessageBox.Show("Please select the year based on create date of data to upload.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (FilePath.Text == "")
            {
                MessageBox.Show("Please select file.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FilePath.Select();
            }
            else if (SheetDropdownList.Text == "Select sheet")
            {
                MessageBox.Show("Please select Sheet!", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SheetDropdownList.Select();
            }
            else
            {
                if (DataTypeDropdown.Text == "Defect")
                {
                    
                    try
                    {
                        if (File.Exists(SaveDirectory + fileNameWithExt)) // if the file to upload is already exists this function will rename the file and insert the new file to the database
                        {
                            NewFileName = "Copy_" + fileName + "_" + DateTime.Now.ToString("MMddyyyyhhmmss") + fileExt; // Create new filename if already exist
                            string newFileSavePath = Path.Combine(SaveDirectory, NewFileName); // combine the path of new folder to filename
                            File.Copy(FilePath.Text, newFileSavePath, true);

                            InsertDefectData(); // insert defect Data
                            SelectLastInsertedDefectDate(); // Update the upload date
                        }
                        else
                        {
                            string FileDestination = Path.Combine(SaveDirectory, fileNameWithExt); // combine the path of new folder and filename
                            File.Copy(FilePath.Text, FileDestination, true);

                            InsertDefectData(); // insert defect Data
                            SelectLastInsertedDefectDate(); // Update the upload date
                        }
                        
                        COPQPartsLossForm.HaveNewUploadedData = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    //try
                    //{
                        if (File.Exists(SaveDirectory + fileNameWithExt)) // if the file to upload is already exists this function will rename the file and insert the new file to the database
                        {
                            NewFileName = "Copy_" + fileName + "_" + DateTime.Now.ToString("MMddyyyyhhmmss") + fileExt; // Create new filename if already exist
                            string newFileSavePath = Path.Combine(SaveDirectory, NewFileName); // combine the path of new folder to filename
                            File.Copy(FilePath.Text, newFileSavePath, true);

                            InsertPartsLossReport();
                            InsertPartsLossData();
                            SelectGMMSAndSAPLastUpdated();
                        }
                        else
                        {
                            string FileDestination = Path.Combine(SaveDirectory, fileNameWithExt); // combine the path of new folder and filename
                            File.Copy(FilePath.Text, FileDestination, true);

                            InsertPartsLossReport();
                            InsertPartsLossData();
                            SelectGMMSAndSAPLastUpdated();

                        }

                        //Refresh
                        COPQPartsLossForm.HaveNewUploadedData = true;

                    //}
                    //catch (Exception)
                    //{
                    //    throw;
                    //}
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>

        //private void InsertPartsLossReport()
        //{
        //    try
        //    {
        //        foreach (DataGridViewRow row in UploadPartsLossDatagrid.Rows)
        //        {
        //            // FUNCTION FOR CHECKING IF ALREADY EXIST IN DB.
        //            if (!row.IsNewRow)
        //            {

        //                // -> SQL query to insert Section to Approver setting
        //                SqlConnection con = new SqlConnection(MHMS_Conn);

        //                if (con.State == ConnectionState.Closed)
        //                {
        //                    con.Open();
        //                }

        //                SqlCommand InsertPartsLossReport = new SqlCommand("SP_InsertPartsLossReport", con);
        //                InsertPartsLossReport.CommandType = CommandType.StoredProcedure;
        //                InsertPartsLossReport.Parameters.AddWithValue("@Date", row.Cells["Date"].Value.ToString());
        //                InsertPartsLossReport.Parameters.AddWithValue("@Section", SectionDropdown.Text);
        //                InsertPartsLossReport.Parameters.AddWithValue("@DailyAdjustedAmount", row.Cells["DailyAdjustedAmount"].Value.ToString());
        //                InsertPartsLossReport.Parameters.AddWithValue("@Contributor", row.Cells["Contributor"].Value.ToString());
        //                InsertPartsLossReport.Parameters.AddWithValue("@MonthlyTarget", row.Cells["MonthlyTarget"].Value.ToString());
        //                InsertPartsLossReport.Parameters.AddWithValue("@DailyLimit", row.Cells["DailyLimit"].Value.ToString());
        //                InsertPartsLossReport.Parameters.AddWithValue("@ComulativeLimit", row.Cells["CumulativeLimit"].Value.ToString());
        //                InsertPartsLossReport.Parameters.AddWithValue("@Type", row.Cells["Type"].Value.ToString());
        //                InsertPartsLossReport.Parameters.AddWithValue("@CumulativeActual", "CumulativeActual");
        //                InsertPartsLossReport.Parameters.AddWithValue("@CumulativeEstimate", row.Cells["CumulativeEstimate"].Value.ToString());
        //                con.Close();
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //==================================================================================================================>>>>>>>>>>>>>

        private void GetTop5PartsLossData() //not tested 010/09/2023
        {
            try
            {
                foreach (DataGridViewRow row in UploadPartsLossDatagrid.Rows)
                {
                    // FUNCTION FOR CHECKING IF MH LOSS ALREADY EXIST IN DB.

                    SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand SelectTop5PartsLoss = new SqlCommand("SP_SelectTop5PartsLoss", con);
                    SelectTop5PartsLoss.CommandType = CommandType.StoredProcedure;
                    SelectTop5PartsLoss.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                    SqlDataReader reader = SelectTop5PartsLoss.ExecuteReader();
                    reader.Read();

                    //TO EDIT PA IF OKAY NA ANG SERVER ====>>>>>>>>>>>>>
                    if (reader.HasRows == true)
                    {
                        //MessageBox.Show("Have existing data!");
                        //Update the status of existing data in COPQ Aprroval table
                        SqlCommand UpdateTop5PartsLoss = new SqlCommand("SP_UpdateTop5PartsLoss", con);
                        UpdateTop5PartsLoss.CommandType = CommandType.StoredProcedure;
                        UpdateTop5PartsLoss.Parameters.AddWithValue("@DateEncountered", row.Cells[0].Value.ToString());
                        UpdateTop5PartsLoss.Parameters.AddWithValue("@Section", row.Cells[1].Value.ToString());
                        UpdateTop5PartsLoss.Parameters.AddWithValue("@CostCenter", row.Cells[2].Value.ToString());
                        UpdateTop5PartsLoss.Parameters.AddWithValue("@LineStopDetail", row.Cells[6].Value.ToString());
                        UpdateTop5PartsLoss.Parameters.AddWithValue("@StopTime", row.Cells[7].Value.ToString());
                        UpdateTop5PartsLoss.Parameters.AddWithValue("@DirectMP", row.Cells[8].Value.ToString());
                        UpdateTop5PartsLoss.Parameters.AddWithValue("@SemiDirectMP", row.Cells[9].Value.ToString());
                        UpdateTop5PartsLoss.Parameters.AddWithValue("@LossManhour", row.Cells[10].Value.ToString());
                        UpdateTop5PartsLoss.ExecuteNonQuery();
                        reader.Close();
                    }
                    else
                    {
                        //Insert new top 5 parts loss
                        SqlCommand InsertTop5PartsLoss = new SqlCommand("SP_InsertTop5PartsLoss", con);
                        InsertTop5PartsLoss.CommandType = CommandType.StoredProcedure;
                        InsertTop5PartsLoss.Parameters.AddWithValue("@DateEncountered", row.Cells[0].Value.ToString());
                        InsertTop5PartsLoss.Parameters.AddWithValue("@Section", row.Cells[1].Value.ToString());
                        InsertTop5PartsLoss.Parameters.AddWithValue("@CostCenter", row.Cells[2].Value.ToString());
                        InsertTop5PartsLoss.Parameters.AddWithValue("@LineStopDetail", row.Cells[6].Value.ToString());
                        InsertTop5PartsLoss.Parameters.AddWithValue("@StopTime", row.Cells[7].Value.ToString());
                        InsertTop5PartsLoss.Parameters.AddWithValue("@DirectMP", row.Cells[8].Value.ToString());
                        InsertTop5PartsLoss.Parameters.AddWithValue("@SemiDirectMP", row.Cells[9].Value.ToString());
                        InsertTop5PartsLoss.Parameters.AddWithValue("@LossManhour", row.Cells[10].Value.ToString());
                        InsertTop5PartsLoss.ExecuteNonQuery();
                        reader.Close();
                    }

                    con.Close();
                }

                MessageBox.Show("Some of MH loss data deleted successfully.", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FilePath.Clear();
                SectionDropdown.Items.Clear();
                SheetDropdownList.Items.Clear();
                DataTypeDropdown.Items.Clear();
                UploadPartsLossDatagrid.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        //==================================================================================================================>>>>>>>>>>>>>

        private void InsertPartsLossReport()
        {
            DeletePartsLossReportPreviousUpload();

            DapperPlusManager.Entity<PartsLossData_Class>().Table("PartsLossReport");
            List<PartsLossData_Class> PartsLossReportData = UploadPartsLossDatagrid.DataSource as List<PartsLossData_Class>;
            if (PartsLossReportData != null)
            {
                using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
                {
                    db.BulkInsert(PartsLossReportData);

                    //MessageBox.Show("Production engineering parts loss data inserted successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            DeletePartsLossReportNullValues();
        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void InsertDefectData()
        {
            DapperPlusManager.Entity<Defect_Class>().Table("Defect");
            List<Defect_Class> DefectData = UploadPartsLossDatagrid.DataSource as List<Defect_Class>;
            if (DefectData != null)
            {
                using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
                {
                    db.BulkInsert(DefectData);
                }
            }

            MessageBox.Show("Defect Data inserted successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Clear fields after upload
            FilePath.Text = "";
            SheetDropdownList.Text = "";
            UploadPartsLossDatagrid.DataSource = null;
            this.Close();
        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void InsertPartsLossData_TEST()
        {

            //DeletePartsLossPreviousUpload();

            //try
            //{
            //    foreach (DataGridViewRow row in UploadPartsLossDatagrid.Rows)
            //    {
            //        // FUNCTION FOR CHECKING IF PO ALREADY EXIST IN DB.
            //        if (!row.IsNewRow)
            //        {

            //            SqlConnection con = new SqlConnection(MHMS_Conn);

            //            if (con.State == ConnectionState.Closed)
            //            {
            //                con.Open();
            //            }

            //            //if (row.Cells["OrderNo"].Value.ToString() != "")
            //            //{
            //                SqlCommand InsertUsers = new SqlCommand("SP_InsertPartsLossData_TEST", con);
            //                InsertUsers.CommandType = CommandType.StoredProcedure;
            //                InsertUsers.Parameters.AddWithValue("@OrderNo", row.Cells["OrderNo"].Value.ToString());
            //                InsertUsers.Parameters.AddWithValue("@Section", LoginForm.UserSection);
            //                InsertUsers.Parameters.AddWithValue("@MaterialCode", row.Cells["MaterialCode"].Value.ToString());
            //                InsertUsers.Parameters.AddWithValue("@MaterialText", row.Cells["MaterialText"].Value.ToString());
            //                InsertUsers.Parameters.AddWithValue("@Quantity", row.Cells["Quantity"].Value.ToString());
            //                InsertUsers.Parameters.AddWithValue("@Amount", row.Cells["Amount"].Value.ToString());
            //                InsertUsers.Parameters.AddWithValue("@MoveReason", row.Cells["MoveReason"].Value.ToString());
            //                InsertUsers.Parameters.AddWithValue("@CreateDate", row.Cells["CreateDate"].Value.ToString());
            //                InsertUsers.Parameters.AddWithValue("@DetailsReason", row.Cells["DetailsReason"].Value.ToString());
            //                InsertUsers.Parameters.AddWithValue("@ConfirmResult", row.Cells["ConfirmResult"].Value.ToString());
            //                InsertUsers.Parameters.AddWithValue("@Remark", row.Cells["Remarks"].Value.ToString());
            //                InsertUsers.Parameters.AddWithValue("@ChargedObject", row.Cells["ChargeObject"].Value.ToString());
            //                InsertUsers.Parameters.AddWithValue("@Creator", row.Cells["Creator"].Value.ToString());
            //                InsertUsers.Parameters.AddWithValue("@Department", row.Cells["Department"].Value.ToString());
            //                InsertUsers.Parameters.AddWithValue("@UploadDate", DateTime.Now.ToShortDateString());
            //                InsertUsers.Parameters.AddWithValue("@AdjustedAmount", row.Cells["AdjustedAmount"].Value.ToString());
            //                InsertUsers.ExecuteNonQuery();
            //                con.Close();
            //            //}
            //        }
            //    }

            //    MessageBox.Show("Files Uploaded successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    //Clear fields after upload
            //    FilePath.Text = "";
            //    SheetDropdownList.Text = "";
            //    UploadPartsLossDatagrid.DataSource = null;
            //    this.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }
    


        private void InsertPartsLossData()
        {
            if (SectionDropdown.Text == "BPS")
            {
                DeletePartsLossPreviousUpload(); //delete all previous upload in part loss data table 
                //DeleteTop5Recurrence(); //delete previous top 5 in top 5 recurrence table

                DapperPlusManager.Entity<PartsLossData_Class>().Table("PartsLossData_ProductionEngineering");
                List<PartsLossData_Class> PartsLossData = UploadPartsLossDatagrid.DataSource as List<PartsLossData_Class>;
                if (PartsLossData != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(PartsLossData);

                        MessageBox.Show("BPS parts loss data inserted successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadPartsLossDatagrid.DataSource = null;
                this.Close();
            }
            else if (SectionDropdown.Text == "Ink Cartridge")
            {
                DeletePartsLossPreviousUpload(); //delete all previous upload in part loss data table 
                //DeleteTop5Recurrence(); //delete previous top 5 in top 5 recurrence table

                DapperPlusManager.Entity<PartsLossData_Class>().Table("PartsLossData_InkCartridge");
                List<PartsLossData_Class> PartsLossData = UploadPartsLossDatagrid.DataSource as List<PartsLossData_Class>;
                if (PartsLossData != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                       
                        db.BulkInsert(PartsLossData);

                        MessageBox.Show("Ink Cartridge parts loss data inserted successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadPartsLossDatagrid.DataSource = null;
                this.Close();
            }
            else if (SectionDropdown.Text == "Ink Head")
            {
                DeletePartsLossPreviousUpload(); //delete all previous upload in part loss data table 
                //DeleteTop5Recurrence(); //delete previous top 5 in top 5 recurrence table

                DapperPlusManager.Entity<PartsLossData_Class>().Table("PartsLossData_InkHead");
                List<PartsLossData_Class> PartsLossData = UploadPartsLossDatagrid.DataSource as List<PartsLossData_Class>;
                if (PartsLossData != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(PartsLossData);

                        MessageBox.Show("Ink Head parts loss data inserted successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadPartsLossDatagrid.DataSource = null;
                this.Close();
            }
            else if (SectionDropdown.Text == "Molding Production")
            {
                DeletePartsLossPreviousUpload(); //delete all previous upload in part loss data table 
                //DeleteTop5Recurrence(); //delete previous top 5 in top 5 recurrence table

                DapperPlusManager.Entity<PartsLossData_Class>().Table("PartsLossData_Molding");
                List<PartsLossData_Class> PartsLossData = UploadPartsLossDatagrid.DataSource as List<PartsLossData_Class>;
                if (PartsLossData != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(PartsLossData);

                        MessageBox.Show("Molding parts loss data inserted successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadPartsLossDatagrid.DataSource = null;
                this.Close();
            }
            else if (SectionDropdown.Text == "PCBA")
            {
                DeletePartsLossPreviousUpload(); //delete all previous upload in part loss data table 
                //DeleteTop5Recurrence(); //delete previous top 5 in top 5 recurrence table

                DapperPlusManager.Entity<PartsLossData_Class>().Table("PartsLossData_PCBA");
                List<PartsLossData_Class> PartsLossData = UploadPartsLossDatagrid.DataSource as List<PartsLossData_Class>;
                if (PartsLossData != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(PartsLossData);

                        MessageBox.Show("PCBA parts loss data inserted successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadPartsLossDatagrid.DataSource = null;
                this.Close();
            }
            else if (SectionDropdown.Text == "Printer")
            {
                DeletePartsLossPreviousUpload(); //delete all previous upload in part loss data table 
                //DeleteTop5Recurrence(); //delete previous top 5 in top 5 recurrence table

                DapperPlusManager.Entity<PartsLossData_Class>().Table("PartsLossData_Printer");
                List<PartsLossData_Class> PartsLossData = UploadPartsLossDatagrid.DataSource as List<PartsLossData_Class>;
                if (PartsLossData != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(PartsLossData);

                        MessageBox.Show("Printer parts loss data inserted successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadPartsLossDatagrid.DataSource = null;
                this.Close();
            }
            else if (SectionDropdown.Text == "P-Touch")
            {
                DeletePartsLossPreviousUpload(); //delete all previous upload in part loss data table 
                //DeleteTop5Recurrence(); //delete previous top 5 in top 5 recurrence table

                DapperPlusManager.Entity<PartsLossData_Class>().Table("PartsLossData_PTouch");
                List<PartsLossData_Class> PartsLossData = UploadPartsLossDatagrid.DataSource as List<PartsLossData_Class>;
                if (PartsLossData != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(PartsLossData);

                        MessageBox.Show("P-Touch parts loss data inserted successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadPartsLossDatagrid.DataSource = null;
                this.Close();
            }
            else if (SectionDropdown.Text == "Tape Cassette")
            {
                DeletePartsLossPreviousUpload(); //delete all previous upload in part loss data table 
                //DeleteTop5Recurrence(); //delete previous top 5 in top 5 recurrence table

                DapperPlusManager.Entity<PartsLossData_Class>().Table("PartsLossData_TapeCassette");
                List<PartsLossData_Class> PartsLossData = UploadPartsLossDatagrid.DataSource as List<PartsLossData_Class>;
                if (PartsLossData != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(PartsLossData);

                        MessageBox.Show("Tape Cassette parts loss data inserted successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                
                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadPartsLossDatagrid.DataSource = null;
                this.Close();
            }

        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void DeletePartsLossPreviousUpload()
        {
            // -> SQL query to delete factor loss
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand DeletePartsLoss = new SqlCommand("SP_DeletePartsLossPreviousUpload", con);
            DeletePartsLoss.CommandType = CommandType.StoredProcedure;
            DeletePartsLoss.Parameters.AddWithValue("Section", SectionDropdown.Text);
            DeletePartsLoss.Parameters.AddWithValue("Month", MonthDropdwn.Text);
            DeletePartsLoss.Parameters.AddWithValue("FiscalYear", FiscalYearDropdown.Text);
            DeletePartsLoss.ExecuteNonQuery();
            con.Close();
        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void DeletePartsLossReportPreviousUpload()
        {
            // -> SQL query to delete factor loss
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand DeletePartsLoss = new SqlCommand("SP_DeletePartsLossReportPreviousUpload", con);
            DeletePartsLoss.CommandType = CommandType.StoredProcedure;
            DeletePartsLoss.Parameters.AddWithValue("Section", SectionDropdown.Text);
            DeletePartsLoss.Parameters.AddWithValue("Month", MonthDropdwn.Text);
            DeletePartsLoss.Parameters.AddWithValue("FiscalYear", FiscalYearDropdown.Text);
            DeletePartsLoss.ExecuteNonQuery();
            con.Close();
        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void DeletePartsLossReportNullValues()
        {
            // -> SQL query to delete factor loss
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand DeletePartsLoss = new SqlCommand("SP_DeletePartsLossReportNullValues", con);
            DeletePartsLoss.CommandType = CommandType.StoredProcedure;
            DeletePartsLoss.Parameters.AddWithValue("Section", SectionDropdown.Text);
            DeletePartsLoss.ExecuteNonQuery();
            con.Close();
        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void DeleteTop5Recurrence()
        {
            // -> SQL query to delete factor loss
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand DeleteTop5Recurrence = new SqlCommand("SP_DeleteTop5Recurrence", con);
            DeleteTop5Recurrence.CommandType = CommandType.StoredProcedure;
            DeleteTop5Recurrence.Parameters.AddWithValue("Section", SectionDropdown.Text);
            DeleteTop5Recurrence.ExecuteNonQuery();
            con.Close();
        }

        //====================================================================================================================>>>>>>>>>>>

        private void SelectGMMSAndSAPLastUpdated()
        {
            // -> SQL query to select User Account
            SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SectionLabel.Text = Dashboard.SectionText.Replace("BIPH-", "");

            if (SectionDropdown.Text == "Tape Cassette")
            {
                SqlCommand SelectTapeCassetteLastUpdated = new SqlCommand("SP_SelectGMMSAndSAPLastUpdated", con);
                SelectTapeCassetteLastUpdated.CommandType = CommandType.StoredProcedure;
                SelectTapeCassetteLastUpdated.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteLastUpdated");
                SqlDataAdapter da = new SqlDataAdapter(SelectTapeCassetteLastUpdated);
                DataTable dt = new DataTable();
                da.Fill(dt);

                SqlDataReader reader = SelectTapeCassetteLastUpdated.ExecuteReader();

                while (reader.Read())
                {
                    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["UploadDate"].ToString();
                }

            }
            else if (SectionDropdown.Text == "Ink Cartridge")
            {
                SqlCommand SelectInkCartridgeLastUpdated = new SqlCommand("SP_SelectGMMSAndSAPLastUpdated", con);
                SelectInkCartridgeLastUpdated.CommandType = CommandType.StoredProcedure;
                SelectInkCartridgeLastUpdated.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeLastUpdated");
                SqlDataAdapter da = new SqlDataAdapter(SelectInkCartridgeLastUpdated);
                DataTable dt = new DataTable();
                da.Fill(dt);

                SqlDataReader reader = SelectInkCartridgeLastUpdated.ExecuteReader();

                while (reader.Read())
                {
                    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["UploadDate"].ToString();
                }

                DefectLastUpdateDateLabel.Visible = false;
            }
            else if (SectionDropdown.Text == "BPS")
            {
                SqlCommand SelectBPSLastUpdated = new SqlCommand("SP_SelectGMMSAndSAPLastUpdated", con);
                SelectBPSLastUpdated.CommandType = CommandType.StoredProcedure;
                SelectBPSLastUpdated.Parameters.AddWithValue("@Procedure", "SelectBPSLastUpdated");
                SqlDataAdapter da = new SqlDataAdapter(SelectBPSLastUpdated);
                DataTable dt = new DataTable();
                da.Fill(dt);

                SqlDataReader reader = SelectBPSLastUpdated.ExecuteReader();

                while (reader.Read())
                {
                    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["UploadDate"].ToString();
                }

                DefectLastUpdateDateLabel.Visible = false;

            }
            else if (SectionDropdown.Text == "Ink Head")
            {
                SqlCommand SelectInkHeadLastUpdated = new SqlCommand("SP_SelectGMMSAndSAPLastUpdated", con);
                SelectInkHeadLastUpdated.CommandType = CommandType.StoredProcedure;
                SelectInkHeadLastUpdated.Parameters.AddWithValue("@Procedure", "SelectInkHeadLastUpdated");
                SqlDataAdapter da = new SqlDataAdapter(SelectInkHeadLastUpdated);
                DataTable dt = new DataTable();
                da.Fill(dt);

                SqlDataReader reader = SelectInkHeadLastUpdated.ExecuteReader();

                while (reader.Read())
                {
                    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["UploadDate"].ToString();
                }

                DefectLastUpdateDateLabel.Visible = false;

            }
            else if (SectionDropdown.Text == "Molding Production")
            {
                //SqlCommand SelectMoldingLastUpdated = new SqlCommand("SP_SelectGMMSAndSAPLastUpdated", con);
                //SelectMoldingLastUpdated.CommandType = CommandType.StoredProcedure;
                //SelectMoldingLastUpdated.Parameters.AddWithValue("@Procedure", "SelectMoldingLastUpdated");
                //SqlDataAdapter da = new SqlDataAdapter(SelectMoldingLastUpdated);
                //DataTable dt = new DataTable();
                //da.Fill(dt);


                //SqlDataReader reader = SelectMoldingLastUpdated.ExecuteReader();

                //while (reader.Read())
                //{
                //    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["UploadDate"].ToString();
                //}

                //DefectLastUpdateDateLabel.Visible = false;
            }
            else if (SectionDropdown.Text == "PCBA")
            {
                SqlCommand SelectPCBALastUpdated = new SqlCommand("SP_SelectGMMSAndSAPLastUpdated", con);
                SelectPCBALastUpdated.CommandType = CommandType.StoredProcedure;
                SelectPCBALastUpdated.Parameters.AddWithValue("@Procedure", "SelectPCBALastUpdated");
                SqlDataAdapter da = new SqlDataAdapter(SelectPCBALastUpdated);
                DataTable dt = new DataTable();
                da.Fill(dt);


                SqlDataReader reader = SelectPCBALastUpdated.ExecuteReader();

                while (reader.Read())
                {
                    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["UploadDate"].ToString();
                }

                DefectLastUpdateDateLabel.Visible = false;
            }
            else if (SectionDropdown.Text == "Printer")
            {
                SqlCommand SelectPrinterLastUpdated = new SqlCommand("SP_SelectGMMSAndSAPLastUpdated", con);
                SelectPrinterLastUpdated.CommandType = CommandType.StoredProcedure;
                SelectPrinterLastUpdated.Parameters.AddWithValue("@Procedure", "SelectPrinterLastUpdated");
                SqlDataAdapter da = new SqlDataAdapter(SelectPrinterLastUpdated);
                DataTable dt = new DataTable();
                da.Fill(dt);

                SqlDataReader reader = SelectPrinterLastUpdated.ExecuteReader();

                while (reader.Read())
                {
                    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["UploadDate"].ToString();
                }

                DefectLastUpdateDateLabel.Visible = false;
            }
            else if (SectionDropdown.Text == "P-Touch")
            {
                SqlCommand SelectPTouchLastUpdated = new SqlCommand("SP_SelectGMMSAndSAPLastUpdated", con);
                SelectPTouchLastUpdated.CommandType = CommandType.StoredProcedure;
                SelectPTouchLastUpdated.Parameters.AddWithValue("@Procedure", "SelectPTouchLastUpdated");
                SqlDataAdapter da = new SqlDataAdapter(SelectPTouchLastUpdated);
                DataTable dt = new DataTable();
                da.Fill(dt);

                SqlDataReader reader = SelectPTouchLastUpdated.ExecuteReader();

                while (reader.Read())
                {
                    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["UploadDate"].ToString();
                }

                DefectLastUpdateDateLabel.Visible = false;
            }
        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void IDFTemplateButton_Click(object sender, EventArgs e)
        {
            Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\Other Template\Tape Cassette IDF Monitoring Template.xlsx");
        }

        private void SectionDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectGMMSAndSAPLastUpdated();
        }

        //=====================================================================================================================>>>>>>>>>>>
    }
}
