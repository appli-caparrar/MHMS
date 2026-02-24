using ExcelDataReader;
using MHMS.Class;
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
    public partial class UploadSTMasterData : Form
    {

        //Connection String
        static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(MHMS2_Conn);

        public UploadSTMasterData()
        {
            InitializeComponent();
        }

        //Table collection
        DataTableCollection tableCollection;

        private void UploadButton_Click(object sender, EventArgs e)
        {
            if (ApplicationFormDropdown.Text == "")
            {
                MessageBox.Show("Please select master data type.", "Master data type is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ApplicationFormDropdown.Select();
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
            
        }

        //===========================================================<BREAK>================================================================

        private void InsertMasterData()
        {
            if (ApplicationFormDropdown.Text == "ST Master Data")
            {
                DapperPlusManager.Entity<STMasterData_Class>().Table("TBL_STMasterData");
                List<STMasterData_Class> UploadSTMasterData = UploadMasterDataDatagrid.DataSource as List<STMasterData_Class>;

                if (UploadSTMasterData != null)
                {
                    //change this connection if the database is migrated to other server
                    using (IDbConnection db = new SqlConnection("Server=APBIPH1131;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
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
            else if (ApplicationFormDropdown.Text == "WC/CC Master Data")
            {
                //Insert code here...
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateAndTimeLabel.Text = dateTime.ToString("dddd , MMM dd yyyy, hh : mm : ss");
        }


        //-------------->>>>end
    }
}
