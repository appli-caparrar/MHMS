using ExcelDataReader;
using MHMS.Class;
using MHMS.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z.Dapper.Plus;

namespace MHMS
{
    public partial class UploadMasterData : Form
    {

        //Connection String
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2_ACTUAL"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);

        public UploadMasterData()
        {
            InitializeComponent();
        }

        //Table collection
        DataTableCollection tableCollection;

        private void UploadButton_Click(object sender, EventArgs e)
        {
            if (MasterDataTypeDropdown.Text == "")
            {
                MessageBox.Show("Please select master data type.", "Master data type is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MasterDataTypeDropdown.Select();
            }
            else if (FilePath.Text == "")
            {
                MessageBox.Show("Please select the file.", "File is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FilePath.Select();
            }
            else if (SheetDropdownList.Text == "")
            {
                MessageBox.Show("Please select the sheet.", "Sheet is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                SheetDropdownList.Select();
            }
            else
            {
                 InsertMasterData();
            }
        }

        //===========================================================<BREAK>================================================================

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

        //===========================================================<BREAK>================================================================

        private void SheetDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = tableCollection[SheetDropdownList.SelectedItem.ToString()];
           
            if (dt != null)
            {
                if (MasterDataTypeDropdown.Text == "ST Master Data")
                {
                    List<STMasterData_Class> list = new List<STMasterData_Class>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        STMasterData_Class obj = new STMasterData_Class();

                        obj.Section = dt.Rows[i]["SECTION"].ToString();
                        obj.MassProduction = dt.Rows[i]["Mass Production (Month Start)"].ToString();
                        obj.Plant = dt.Rows[i]["Plant"].ToString();
                        obj.ItemCodeSAP = dt.Rows[i]["Item Code (SAP)"].ToString();
                        obj.ItemNameSAP = dt.Rows[i]["Item Name (SAP)"].ToString();
                        obj.SAPBeforeST = dt.Rows[i]["SAP Before ST(min)"].ToString();
                        obj.SAPBeforeTT = dt.Rows[i]["SAP Before TT(min)"].ToString();
                        obj.SAPAfterST = dt.Rows[i]["SAP After ST(min)"].ToString();
                        obj.SAPAfterTT = dt.Rows[i]["SAP After TT(min)"].ToString();
                        obj.ItemCodeMH = dt.Rows[i]["Item Code (MH)"].ToString();
                        obj.ItemNameMH = dt.Rows[i]["Item Name (MH)"].ToString();
                        obj.MHBeforeST = dt.Rows[i]["MH Before ST(min)"].ToString();
                        obj.MHBeforeTT = dt.Rows[i]["MH Before TT(min)"].ToString();
                        obj.MHAfterST = dt.Rows[i]["MH After ST(min)"].ToString();
                        obj.MHAfterTT = dt.Rows[i]["MH After TT(min)"].ToString();

                        list.Add(obj);
                    }

                    UploadMasterDataDatagrid.DataSource = list;
                }
                else if (MasterDataTypeDropdown.Text == "WC/CC Master Data")
                {
                    List<WCCCMasterData_Class> list = new List<WCCCMasterData_Class>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        WCCCMasterData_Class obj = new WCCCMasterData_Class();

                        if (WCCCDropdown.Text == "Work Center")
                        {
                            obj.YearMonth = dt.Rows[i]["YEAR_MONTH"].ToString();
                            obj.Factory = dt.Rows[i]["FACTORY"].ToString();
                            obj.CostCenterCode = dt.Rows[i]["Costcenter Code"].ToString();
                            obj.CostCenterName = dt.Rows[i]["Costcenter Name"].ToString();
                            obj.Plant = dt.Rows[i]["PLANT"].ToString();
                            obj.WorkCenterCode = dt.Rows[i]["Work Center"].ToString();
                            obj.Shift = dt.Rows[i]["Shift"].ToString();
                            obj.WorkCenterName = dt.Rows[i]["Workcenter Name"].ToString();
                            obj.BaseSumSign = dt.Rows[i]["BASE SUM SIGN"].ToString();
                            obj.CostCenter = dt.Rows[i]["COST_CENTER"].ToString();
                            obj.CostCenterGrouping_A = dt.Rows[i]["Cost Center Groupings A"].ToString();
                            obj.CostCenterGrouping_B = dt.Rows[i]["Cost Center Groupings B"].ToString();
                            obj.Section = dt.Rows[i]["Section"].ToString();
                        }
                        else if (WCCCDropdown.Text == "Cost Center")
                        {
                            obj.YearMonth = dt.Rows[i]["YEAR_MONTH"].ToString();
                            obj.Factory = dt.Rows[i]["FACTORY"].ToString();
                            obj.CostCenterCode = dt.Rows[i]["Cost Center Code"].ToString();
                            obj.CostCenterName = dt.Rows[i]["Costcenter Name"].ToString();
                            obj.CostCenterSign = dt.Rows[i]["COST_CENTER SIGN"].ToString();
                            obj.Sort = dt.Rows[i]["SORT"].ToString();
                            obj.BaseSumSign = dt.Rows[i]["BASE SUM SIGN"].ToString();
                            obj.Plant = dt.Rows[i]["PLANT"].ToString();
                            //Insert shift here
                            obj.CostCenterGrouping_A = dt.Rows[i]["Cost Center Groupings A"].ToString();
                            obj.CostCenterGrouping_B = dt.Rows[i]["Cost Center Groupings B"].ToString();
                            obj.Section = dt.Rows[i]["Section"].ToString();
                        }

                        list.Add(obj);
                    }

                    UploadMasterDataDatagrid.DataSource = list;
                }
                else if (MasterDataTypeDropdown.Text == "Open MH System Master Data")
                {
                    //Type code here...
                    List<OpenMHMasterData_Class> list = new List<OpenMHMasterData_Class>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        OpenMHMasterData_Class obj = new OpenMHMasterData_Class();

                        obj.Section = dt.Rows[i]["SECTION"].ToString();
                        obj.ItemCode = dt.Rows[i]["Item Code"].ToString();
                        obj.OldST = dt.Rows[i]["Old ST"].ToString();
                        obj.WC_Section = dt.Rows[i]["WC/CC Section"].ToString();
                        obj.WorkCenterCode = dt.Rows[i]["Work Center"].ToString();
                        obj.WorkCenterName = dt.Rows[i]["Workcenter Name"].ToString();
                        obj.CC_Section = dt.Rows[i]["WC/CC Section"].ToString();
                        obj.CostCenterCode = dt.Rows[i]["Costcenter Code"].ToString();
                        //obj.CostCenterName = dt.Rows[i]["Costcenter Name"].ToString();
                        obj.Legend = dt.Rows[i]["Legend"].ToString();
                        obj.LossFactor = dt.Rows[i]["Loss Factor"].ToString();
                        obj.FinalCat = dt.Rows[i]["Final Cat."].ToString();

                        list.Add(obj);
                    }

                    UploadMasterDataDatagrid.DataSource = list;

                }
                else if (MasterDataTypeDropdown.Text == "Manpower Forecasting Master Data")
                {
                    //Type code here...

                }
                

            }
            
        }

        //===========================================================<BREAK>================================================================

        private void InsertMasterData()
        {
            if (MasterDataTypeDropdown.Text == "ST Master Data")
            {
                //Delete Master Data
                DeleteMasterData();

                DapperPlusManager.Entity<STMasterData_Class>().Table("TBL_STMasterData");
                List<STMasterData_Class> UploadSTMasterData = UploadMasterDataDatagrid.DataSource as List<STMasterData_Class>;

                if (UploadSTMasterData != null)
                {
                    //change this connection if the database is migrated to other server
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(UploadSTMasterData);
                    }

                    MessageBox.Show("ST master data uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadMasterDataDatagrid.DataSource = null;
                this.Close();

            }
            else if (MasterDataTypeDropdown.Text == "WC/CC Master Data")
            {
          
                if (WCCCDropdown.Text == "Select WC or CC")
                {
                    MessageBox.Show("Please select Work center or Cost center", "Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    WCCCDropdown.Select();
                }
                else
                {
                    if (WCCCDropdown.Text == "Work Center")
                    {
                        //Delete Master Data
                        DeleteMasterData();

                        DapperPlusManager.Entity<WCCCMasterData_Class>().Table("TBL_WorkCenterMasterData");
                        List<WCCCMasterData_Class> UploadWCMasterData = UploadMasterDataDatagrid.DataSource as List<WCCCMasterData_Class>;

                        if (UploadWCMasterData != null)
                        {
                            //change this connection if the database is migrated to other server
                            using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                            {
                                db.BulkInsert(UploadWCMasterData);
                            }

                            MessageBox.Show("Work center master data uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                        //Clear fields after upload
                        FilePath.Text = "";
                        SheetDropdownList.Text = "";
                        UploadMasterDataDatagrid.DataSource = null;
                        this.Close();
                    }
                    else if (WCCCDropdown.Text == "Cost Center")
                    {
                        //Delete Master Data
                        DeleteMasterData();

                        DapperPlusManager.Entity<WCCCMasterData_Class>().Table("TBL_CostCenterMasterData");
                        List<WCCCMasterData_Class> UploadCCMasterData = UploadMasterDataDatagrid.DataSource as List<WCCCMasterData_Class>;

                        if (UploadCCMasterData != null)
                        {
                            //change this connection if the database is migrated to other server
                            using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                            {
                                db.BulkInsert(UploadCCMasterData);
                            }

                            MessageBox.Show("Cost center master data uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        //Clear fields after upload
                        FilePath.Text = "";
                        SheetDropdownList.Text = "";
                        UploadMasterDataDatagrid.DataSource = null;
                        this.Close();
                    }
                }

            }
            else if (MasterDataTypeDropdown.Text == "Open MH System Master Data")
            {
                
                //Delete Master Data
                DeleteMasterData();

                DapperPlusManager.Entity<OpenMHMasterData_Class>().Table("TBL_OpenMHMasterData");
                List<OpenMHMasterData_Class> UploadSTMasterData = UploadMasterDataDatagrid.DataSource as List<OpenMHMasterData_Class>;

                if (UploadSTMasterData != null)
                {
                    //change this connection if the database is migrated to other server
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(UploadSTMasterData);
                    }

                    MessageBox.Show("Open MH master data uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                //Clear fields after upload
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                UploadMasterDataDatagrid.DataSource = null;
                this.Close();

            }
            else if (MasterDataTypeDropdown.Text == "Manpower Forecasting Master Data")
            {
                //Type code here...

            }
        }

        //==========================================================<Line Break>=======================================================

        private void DeleteMasterData()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (MasterDataTypeDropdown.Text == "ST Master Data")
            {
                SqlCommand DeleteWCCCMasterData = new SqlCommand("SP_DeleteMasterData", con);
                DeleteWCCCMasterData.CommandType = CommandType.StoredProcedure;
                DeleteWCCCMasterData.Parameters.AddWithValue("MasterDataType", MasterDataTypeDropdown.Text);
                DeleteWCCCMasterData.Parameters.AddWithValue("WCCC", "");
                DeleteWCCCMasterData.ExecuteNonQuery();
                con.Close();
            }
            else if (MasterDataTypeDropdown.Text == "WC/CC Master Data")
            {
                SqlCommand DeleteWCCCMasterData = new SqlCommand("SP_DeleteMasterData", con);
                DeleteWCCCMasterData.CommandType = CommandType.StoredProcedure;
                DeleteWCCCMasterData.Parameters.AddWithValue("MasterDataType", MasterDataTypeDropdown.Text);
                DeleteWCCCMasterData.Parameters.AddWithValue("WCCC", WCCCDropdown.Text);
                DeleteWCCCMasterData.ExecuteNonQuery();
                con.Close();
            }
            else if (MasterDataTypeDropdown.Text == "Open MH System Master Data")
            {
                SqlCommand DeleteWCCCMasterData = new SqlCommand("SP_DeleteMasterData", con);
                DeleteWCCCMasterData.CommandType = CommandType.StoredProcedure;
                DeleteWCCCMasterData.Parameters.AddWithValue("MasterDataType", MasterDataTypeDropdown.Text);
                DeleteWCCCMasterData.Parameters.AddWithValue("WCCC", "");
                DeleteWCCCMasterData.ExecuteNonQuery();
                con.Close();
            }

           
        }

        //==========================================================<Line Break>=======================================================

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateAndTimeLabel.Text = dateTime.ToString("dddd , MMM dd yyyy, hh : mm : ss");
        }

        private void ApplicationFormDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MasterDataTypeDropdown.Text == "WC/CC Master Data")
            {
                WCCCDropdown.Enabled = true;
            }
            else
            {
                WCCCDropdown.Enabled = false;
                WCCCDropdown .Text = "Select WC or CC";
            }
            
        }

        private void UploadMasterData_Load(object sender, EventArgs e)
        {

        }


        //-------------->>>>end
    }
}
