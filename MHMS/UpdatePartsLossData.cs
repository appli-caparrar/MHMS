using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class UpdatePartsLossData : Form
    {
        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(MHMS_Conn);

        public UpdatePartsLossData()
        {
            InitializeComponent();
        }
        //===================================================================================================================================

        private void DataUpdateTimer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateAndTimeLabel.Text = dateTime.ToString("dddd , MMM dd yyyy, hh : mm : ss");
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
            //SectionDropdown.ValueMember = "Section";
        }
        //===================================================================================================================================

        private void InsertPartsLossData()
        {
            int intRow = 0;

            foreach (DataGridViewRow row in UploadDatagrid.Rows)
            {
                // FUNCTION FOR CHECKING IF ALREADY EXIST IN DB.
                if (!row.IsNewRow)
                {
                    intRow = intRow + 1;

                    // -> SQL statement to insert GMMS data
                    SqlConnection con = new SqlConnection(MHMS_Conn);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    
                    if ((SectionDropdown.Text == "Ink Cartridge") && (DataTypeDropdown.Text == "GMMS"))
                    {
                        SqlCommand InsertPartsLossData = new SqlCommand("SP_InsertPartsLossData", con);
                        InsertPartsLossData.CommandType = CommandType.StoredProcedure;
                        InsertPartsLossData.Parameters.AddWithValue("@Procedure", "PartsLoss_InkCartridge");
                        InsertPartsLossData.Parameters.AddWithValue("@OrderNo", row.Cells["Order No#"].Value);
                        InsertPartsLossData.Parameters.AddWithValue("@MaterialCode", row.Cells["Material Code"].Value.ToString());
                        InsertPartsLossData.Parameters.AddWithValue("@MaterialText", row.Cells["Material Text"].Value.ToString());
                        InsertPartsLossData.Parameters.AddWithValue("@Quantity", row.Cells["Quantity"].Value);
                        InsertPartsLossData.Parameters.AddWithValue("@Amount", row.Cells["Amount"].Value);
                        InsertPartsLossData.Parameters.AddWithValue("@MoveReason", row.Cells["MoveReason"].Value.ToString());
                        InsertPartsLossData.Parameters.AddWithValue("@CreateDate", row.Cells["CreateDate"].Value.ToString());
                        InsertPartsLossData.Parameters.AddWithValue("@DetailsReason", row.Cells["Details Reason"].Value.ToString());
                        InsertPartsLossData.Parameters.AddWithValue("@ConfirmResult", row.Cells["Confirm Result"].Value.ToString());
                        InsertPartsLossData.Parameters.AddWithValue("@Remark", row.Cells["Remark"].Value.ToString());
                        InsertPartsLossData.Parameters.AddWithValue("@ChargedObject", row.Cells["Charged object"].Value.ToString());
                        InsertPartsLossData.Parameters.AddWithValue("@Creator", row.Cells["Creator"].Value.ToString());
                        InsertPartsLossData.Parameters.AddWithValue("@Department", row.Cells["Dept"].Value.ToString());
                        InsertPartsLossData.Parameters.AddWithValue("@UploadDate", DateTime.Now.ToString("MM/dd/yyyy"));
                        InsertPartsLossData.Parameters.AddWithValue("@AdjustedAmount", row.Cells["ADJ# AMOUNT"].Value.ToString());
                        InsertPartsLossData.ExecuteNonQuery();
                        con.Close();
                    }
                    else if ((SectionDropdown.Text == "BPS") && (DataTypeDropdown.Text == "GMMS"))
                    {
                       //Type insert statement here
                       //Create table of GMMS for PE in DB 
                    }
                    else if ((SectionDropdown.Text == "Tape Cassette") && (DataTypeDropdown.Text == "GMMS"))
                    {
                        //Type insert statement here
                        //Create table of GMMS for Tape Cassette in DB 
                    }
                    //Type here other condition per section...
                }
            }

            MessageBox.Show("GMMS data was successfully updated", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SectionDropdown.Text = "";
            DataTypeDropdown.Text = "";
            FilePath.Clear();
            UploadDatagrid.DataSource = null;
            
        }

        //===================================================================================================================================

        private void InsertDefectData()
        {
            int intRow = 0;

            foreach (DataGridViewRow row in UploadDatagrid.Rows)
            {
                // FUNCTION FOR CHECKING IF ALREADY EXIST IN DB.
                if (!row.IsNewRow)
                {
                    intRow = intRow + 1;

                    // -> SQL statement to insert GMMS data
                    SqlConnection con = new SqlConnection(MHMS_Conn);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    if ((SectionDropdown.Text == "Tape Cassette") && (DataTypeDropdown.Text == "Defect"))
                    {
                        SqlCommand InsertDefectData = new SqlCommand("SP_InsertDefectData", con);
                        InsertDefectData.CommandType = CommandType.StoredProcedure;
                        InsertDefectData.Parameters.AddWithValue("@QualityIssueNo", row.Cells["Quality Issue No#"].Value);
                        InsertDefectData.Parameters.AddWithValue("@Date", row.Cells["Date"].Value);
                        InsertDefectData.Parameters.AddWithValue("@Shift", row.Cells["Shift"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@Time", row.Cells["Time"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@DefectCategory", row.Cells["Defect Category"].Value);
                        InsertDefectData.Parameters.AddWithValue("@DefectType", row.Cells["Defect Type"].Value);
                        InsertDefectData.Parameters.AddWithValue("@Issue", row.Cells["Issue"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@TotalNumberofDefect", row.Cells["Total Number of Defects (r)"].Value);
                        InsertDefectData.Parameters.AddWithValue("@QuantityAffected", row.Cells["Quantity Affected (n)"].Value);
                        InsertDefectData.Parameters.AddWithValue("@DefectRate", row.Cells["Defect Rate (%)"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@AffectedQtyToBeSort", row.Cells["Affected Qty# to be Sort"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@PartCode", row.Cells["Part Code"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@PartName", row.Cells["Part Name"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@PartsLotNumber", row.Cells["Parts Lot Number"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@AffectedCassettePONumber", row.Cells["Affected Cassette PO Number"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@POQuantity", row.Cells["PO Quantity"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@ProcessDetected", row.Cells["Process Detected"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@Line#", row.Cells["Line #"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@DetectedBy", row.Cells["Detected By:"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@LiableArea", row.Cells["Liable Area"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@LiablePerson", row.Cells["Liable Person"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@LiableLine", row.Cells["Liable Line"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@ImmediateAction", row.Cells["Immediate Action"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@ProductionPIC", row.Cells["Production PIC"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@PQAIncharge", row.Cells["PQA In charge"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@TechnicianPIC", row.Cells["Technician PIC"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@QGPIC", row.Cells["QG PIC"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@OtherDetails", row.Cells["Other Details"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@Investigation", row.Cells["Investigation"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@DetailsHowTheyDetectTheNG", row.Cells["Details how they detect the N#G#"].Value.ToString());
                        InsertDefectData.Parameters.AddWithValue("@UploadDate", DateTime.Now.ToString());
                        InsertDefectData.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }

            MessageBox.Show("Defect data was successfully Uploaded", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SectionDropdown.Text = "";
            DataTypeDropdown.Text = "";
            FilePath.Clear();
            UploadDatagrid.DataSource = null;
        }

        //===================================================================================================================================

        //string Section = Dashboard.SectionText;
        private void UpdateDataForm_Load(object sender, EventArgs e)
        {
            LoadSection(); //load section in dropdown list

            SectionDropdown.Text = SectionLabel.Text;//When this form loaded the dropdown list value automatically set equal to section label
            
            SelectGMMSAndSAPLastUpdated(); //Load the last upload date of Parts loss data

            SelectLastInsertedDefectDate(); //Load the last upload date of defect data
        }

        //===================================================================================================================================

        private void SelectLastInsertedDefectDate()
        {
            // -> SQL query to select User Account
            SqlConnection con = new SqlConnection(MHMS_Conn);
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
                DefectLastUpdateDateLabel.Text = "Defect: " + reader["Upload Date"].ToString();
            }
           
        }

        //===================================================================================================================================

        private void SelectGMMSAndSAPLastUpdated()
        {
            // -> SQL query to select User Account
            SqlConnection con = new SqlConnection(MHMS_Conn);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SectionLabel.Text = Dashboard.SectionText.Replace("BIPH-", "");

            if (SectionLabel.Text == "Tape Cassette")
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
                    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["Upload Date"].ToString();
                }
                
            }
            else if (SectionLabel.Text == "Ink Cartridge")
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
                    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["Upload Date"].ToString();
                }

                DefectLastUpdateDateLabel.Visible = false;
            }
            else if (SectionLabel.Text == "BPS")
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
                    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["Upload Date"].ToString();
                }

                DefectLastUpdateDateLabel.Visible = false;

            }
            else if (SectionLabel.Text == "Ink Head")
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
                    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["Upload Date"].ToString();
                }

                DefectLastUpdateDateLabel.Visible = false;

            }
            else if (SectionLabel.Text == "Molding Production")
            {
                SqlCommand SelectMoldingLastUpdated = new SqlCommand("SP_SelectGMMSAndSAPLastUpdated", con);
                SelectMoldingLastUpdated.CommandType = CommandType.StoredProcedure;
                SelectMoldingLastUpdated.Parameters.AddWithValue("@Procedure", "SelectMoldingLastUpdated");
                SqlDataAdapter da = new SqlDataAdapter(SelectMoldingLastUpdated);
                DataTable dt = new DataTable();
                da.Fill(dt);


                SqlDataReader reader = SelectMoldingLastUpdated.ExecuteReader();

                while (reader.Read())
                {
                    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["Upload Date"].ToString();
                }

                DefectLastUpdateDateLabel.Visible = false;
            }
            else if (SectionLabel.Text == "PCBA")
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
                    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["Upload Date"].ToString();
                }

                DefectLastUpdateDateLabel.Visible = false;
            }
            else if (SectionLabel.Text == "Printer")
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
                    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["Upload Date"].ToString();
                }

                DefectLastUpdateDateLabel.Visible = false;
            }
            else if (SectionLabel.Text == "P-Touch")
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
                    GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP: " + reader["Upload Date"].ToString();
                }

                DefectLastUpdateDateLabel.Visible = false;
            }
        }

        //===================================================================================================================================

        public DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)//compare the extension of the file
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';";//for below excel 2007
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';";//for above excel 2007
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from ["+ DataTypeDropdown.Text + "$]", con);//here we read data from specified sheet name
                    oleAdpt.Fill(dtexcel);//fill excel data into dataTable
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            return dtexcel;
        }// end ReadExcel

        //===================================================================================================================================

        private void UploadUserButton_Click(object sender, EventArgs e)
        {
            if (SectionDropdown.Text == "")
            {
                MessageBox.Show("Please select the data type!", "Reminders");
            }
            else if (DataTypeDropdown.Text == "")
            {
                MessageBox.Show("Please select the type of data that you want to upload!", "Reminders");
            }
            else if (FilePath.Text == "")
            {
                MessageBox.Show("Please choose the file that you want to upload", "Reminders");
            }
            else
            {
                if (DataTypeDropdown.Text == "Defect")
                {
                    try
                    {
                        InsertDefectData(); // insert GMMS Data
                        SelectLastInsertedDefectDate(); // Update the upload date
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        InsertPartsLossData(); // insert GMMS Data
                        SelectGMMSAndSAPLastUpdated(); // Update the upload date
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        //===================================================================================================================================

        //string percentage = "";

        private void DataTypeDropdown_TextChanged(object sender, EventArgs e)
        {
            if (DataTypeDropdown.Text == "Defect")
            {
                SectionDropdown.Text = "Tape Cassette";
                SectionDropdown.Enabled = false;
            }
            else
            {
                SectionDropdown.Enabled = true;
            }
        }

        //===================================================================================================================================

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            string fileExt = string.Empty;

            OpenFileDialog file = new OpenFileDialog();//open dialog to choose file

            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)//if there is a file choosen by the user
            {
                filePath = file.FileName;//get the path of the file
                fileExt = Path.GetExtension(filePath);//get the file extension
                FilePath.Text = filePath + fileExt;

                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0 || fileExt.CompareTo(".xlsm") == 0)
                {

                    try
                    {
                        DataTable dtExcel = new DataTable();
                        dtExcel = ReadExcel(filePath, fileExt);
                        UploadDatagrid.Visible = true;
                        UploadDatagrid.DataSource = dtExcel;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);//custom messageBox to show error
                }
            }
        }

        //===================================================================================================================================

        private void DownloadTemplateButton_Click(object sender, EventArgs e)
        {
            SectionLabel.Text = Dashboard.SectionText.Replace("BIPH-", "");

            if (SectionLabel.Text == "Tape Cassette")
            {
                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\GMMS&SAP Template\TC_GMMS & SAP Template.xlsm");
                MessageBox.Show(SectionLabel.Text + " Template Downloaded Successfully");
            }
            else if (SectionLabel.Text == "Ink Cartridge")
            {
                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\GMMS&SAP Template\IC_GMMS & SAP Template.xlsm");
                MessageBox.Show(SectionLabel.Text + " Template Downloaded Successfully");
            }
            else if (SectionLabel.Text == "BPS")
            {
                MessageBox.Show(SectionLabel.Text + " Template Downloaded Successfully");
            }
            else if (SectionLabel.Text == "Ink Head")
            {
                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\GMMS&SAP Template\IH_GMMS & SAP Template.xlsm");
                MessageBox.Show(SectionLabel.Text + " Template Downloaded Successfully");
            }
            else if (SectionLabel.Text == "Molding Production")
            {
                MessageBox.Show(SectionLabel.Text + " Template Downloaded Successfully");
            }
            else if (SectionLabel.Text == "PCBA")
            {
                MessageBox.Show(SectionLabel.Text + " Template Downloaded Successfully");
            }
            else if (SectionLabel.Text == "Printer")
            {
                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\GMMS&SAP Template\PRT_GMMS & SAP Template.xlsm");
                MessageBox.Show(SectionLabel.Text + " Template Downloaded Successfully");
            }
            else if (SectionLabel.Text == "P-Touch")
            {
                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\GMMS&SAP Template\PT_GMMS & SAP Template.xlsm");
                MessageBox.Show(SectionLabel.Text + " Template Downloaded Successfully");
            }
        }

        //===================================================================================================================================

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

        //===================================================================================================================================

        private void SectionDropdown_TextChanged(object sender, EventArgs e)
        {
            SelectDataTypeWhenSectionChange();
        }

        private void SheetDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //===================================================================================================================================

    }
}
