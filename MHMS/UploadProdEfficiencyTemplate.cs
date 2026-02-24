using ClosedXML.Excel;
using ExcelDataReader;
using MHMS.Class;
using MHMS.Class_Efficiency;
using MHMS.Connection;
using MHMS.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z.Dapper.Plus;

namespace MHMS
{
    public partial class UploadProdEfficiencyTemplate : Form
    {
        //Connection String
      
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2_ACTUAL"].ConnectionString;
        //static string MHMS_Conn2 = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);
        SqlConnection con2 = new SqlConnection(SQLControl.MHMS_Conn);
        private string connectionString = "Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;";

        public UploadProdEfficiencyTemplate()
        {
            InitializeComponent();
        }

        private void UploadProdEfficiencyTemplate_Load(object sender, EventArgs e)
        {
            if (TargetSettingForm.Category == "Production Efficiency")
            {
                TemplateDropdownList.Items.Remove("MH Annual Target");
                TemplateDropdownList.Items.Remove("ST Annual Target");
                TemplateDropdownList.Items.Add("Direct Efficiency Graph PR1");
                TemplateDropdownList.Items.Add("Direct Efficiency Graph PR2");
                TemplateDropdownList.Items.Add("Direct Efficiency Graph TC");
                TemplateDropdownList.Items.Add("Direct Efficiency Graph IC");
                TemplateDropdownList.Items.Add("Direct Efficiency Graph PT");
                TemplateDropdownList.Items.Add("Direct Efficiency Graph IH");
                TemplateDropdownList.Items.Add("Direct Efficiency Graph TN");
                TemplateDropdownList.Items.Add("Efficiency Summary");
                TemplateDropdownList.Items.Add("Total Efficiency");
                TemplateDropdownList.Items.Add("Direct Efficiency");

                TemplateDropdownList.Items.Add("Semi-Direct Rate");
                TemplateDropdownList.Items.Add("Total Loss Rate");

                TemplateDropdownList.Items.Add("Efficiency Summary Graph");
                TemplateDropdownList.Items.Add("Total Efficiency Graph");
                TemplateDropdownList.Items.Add("Direct Efficiency Graph");
                TemplateDropdownList.Items.Add("Semi-Direct Rate Graph");
                TemplateDropdownList.Items.Add("Total Loss Rate Graph");

                TemplateDropdownList.Items.Add("Daily Top 3 Contributor");
                TemplateDropdownList.Items.Add("Monthly Top 3 Contributor");

                MonthDropdwn.Enabled = true;
            }
            else if (TargetSettingForm.Category == "Factory Efficiency")
            {
                TemplateDropdownList.Items.Remove("Efficiency Summary");
                TemplateDropdownList.Items.Remove("Total Efficiency");
                TemplateDropdownList.Items.Remove("Direct Efficiency");
                TemplateDropdownList.Items.Remove("Semi-Direct Rate");
                TemplateDropdownList.Items.Remove("Total Loss Rate");

                TemplateDropdownList.Items.Remove("Efficiency Summary Graph");
                TemplateDropdownList.Items.Remove("Total Efficiency Graph");
                TemplateDropdownList.Items.Remove("Direct Efficiency Graph PR1");
                TemplateDropdownList.Items.Remove("Direct Efficiency Graph PR2");
                TemplateDropdownList.Items.Remove("Direct Efficiency Graph TC");
                TemplateDropdownList.Items.Remove("Direct Efficiency Graph IC");
                TemplateDropdownList.Items.Remove("Direct Efficiency Graph PT");
                TemplateDropdownList.Items.Remove("Direct Efficiency Graph IH");
                TemplateDropdownList.Items.Remove("Direct Efficiency Graph TN");
                TemplateDropdownList.Items.Remove("Semi-Direct Rate Graph");
                TemplateDropdownList.Items.Remove("Total Loss Rate Graph");
                TemplateDropdownList.Items.Remove("Daily Top 3 Contributor");
                TemplateDropdownList.Items.Remove("Monthly Top 3 Contributor");

                TemplateDropdownList.Items.Add("MH Monthly");
                TemplateDropdownList.Items.Add("ST Monthly");
                TemplateDropdownList.Items.Add("MH Annual Target");
                TemplateDropdownList.Items.Add("ST Annual Target");
                TemplateDropdownList.Items.Add("FE Monthly Graph");
                TemplateDropdownList.Items.Add("FE Quarterly Graph");
                TemplateDropdownList.Items.Add("Ideal Variance Rate Monthly Graph");
                TemplateDropdownList.Items.Add("Ideal Variance Rate Quarterly Graph");

                MonthDropdwn.Enabled = false;
            }
            else {
                TemplateDropdownList.Text = TargetSettingForm.Category;
            }

            //YearDropdownlist.Text = DateTime.Now.Year.ToString();
            AddYears(); // Add years to dropdown list


        }

        private void AddYears()
        {

            var currentYear = DateTime.Today.Year;
            for (int i = 3; i >= 0; i--)
            {
                // Now just add an entry that's the current year minus the counter
                //YearDropdownlist.Items.Add((currentYear - i).ToString());
                YearDropdown.Items.Add((currentYear - i).ToString());
            }
        }


        string fileName = string.Empty;
        string fileNameWithExt = string.Empty;
        string fileExt = string.Empty;
        string filePath = string.Empty;

        //Table collection
        DataTableCollection tableCollection;

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //string filePath = string.Empty;

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

        string UpdateCount;

        private void SheetDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = tableCollection[SheetDropdownList.SelectedItem.ToString()];

            if (dt != null)
            {
                //======================================================================>>>>>>>>>>>>>>
                if (TargetSettingForm.Category == "Production Efficiency")
                {
                    if (TemplateDropdownList.Text == "Total Efficiency")
                    {
                        try
                        {
                            List<TotalEfficiency_Class> list = new List<TotalEfficiency_Class>();

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                TotalEfficiency_Class obj = new TotalEfficiency_Class();

                                obj.Section = dt.Rows[i]["Section"].ToString();

                                if (dt.Rows[i]["Daily KPI Target"].ToString() == "")
                                {
                                    obj.DailyKPITarget = null;
                                }
                                else
                                {
                                    obj.DailyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Daily KPI Target"]) * 100), 2).ToString();
                                }

                                if (dt.Rows[i]["Daily Challenge Target"].ToString() == "")
                                {
                                    obj.DailyChallengeTarget = null;
                                }
                                else
                                {
                                    obj.DailyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Daily Challenge Target"]) * 100), 2).ToString();
                                }

                                if (dt.Rows[i]["Overall result"].ToString() == "")
                                {
                                    obj.OverallResult = null;
                                }
                                else
                                {
                                    obj.OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Overall result"]) * 100), 2).ToString();
                                }

                                if (dt.Rows[i]["Direct Contributor (%)"].ToString() == "")
                                {
                                    obj.DirectContributor = null;
                                }
                                else
                                {

                                    if (dt.Rows[i]["Direct Contributor (%)"].ToString() == "0.00%")
                                    {
                                        obj.DirectContributor = Math.Round((Convert.ToDecimal(dt.Rows[i]["Direct Contributor (%)"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.DirectContributor = Math.Round((Convert.ToDecimal(dt.Rows[i]["Direct Contributor (%)"]) * 100), 2).ToString();
                                    }

                                    //obj.DirectContributor = Math.Round((Convert.ToDecimal(dt.Rows[i]["Direct Contributor (%)"]) * 100), 2).ToString();
                                }

                                if (dt.Rows[i]["Semi-Direct Contributor (%)"].ToString() == "")
                                {
                                    obj.SemiDirectContributor = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Semi-Direct Contributor (%)"].ToString() == "0.00%")
                                    {
                                        obj.SemiDirectContributor = Math.Round((Convert.ToDecimal(dt.Rows[i]["Semi-Direct Contributor (%)"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.SemiDirectContributor = Math.Round((Convert.ToDecimal(dt.Rows[i]["Semi-Direct Contributor (%)"]) * 100), 2).ToString();
                                    }

                                    //obj.SemiDirectContributor = Math.Round((Convert.ToDecimal(dt.Rows[i]["Semi-Direct Contributor (%)"]) * 100), 2).ToString();
                                }

                                if (dt.Rows[i]["Loss Manhour Contributor (%)"].ToString() == "")
                                {
                                    obj.LossManhourContributor = null;
                                }
                                else
                                {

                                    if (dt.Rows[i]["Loss Manhour Contributor (%)"].ToString() == "0.00%")
                                    {
                                        obj.LossManhourContributor = Math.Round((Convert.ToDecimal(dt.Rows[i]["Loss Manhour Contributor (%)"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.LossManhourContributor = Math.Round((Convert.ToDecimal(dt.Rows[i]["Loss Manhour Contributor (%)"]) * 100), 2).ToString();
                                    }

                                    //obj.LossManhourContributor = Math.Round((Convert.ToDecimal(dt.Rows[i]["Loss Manhour Contributor (%)"]) * 100), 2).ToString();
                                }

                                obj.Date = dt.Rows[i]["Date"].ToString();

                                //----------------------------------------------------------
                                //obj.Monthly_Section = dt.Rows[i]["Section (Monthly)"].ToString();

                                if (dt.Rows[i]["Monthly KPI Target"].ToString() == "")
                                {
                                    obj.MonthlyKPITarget = null;
                                }
                                else
                                {

                                    if (dt.Rows[i]["Monthly KPI Target"].ToString() == "0.00%")
                                    {
                                        obj.MonthlyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly KPI Target"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.MonthlyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly KPI Target"]) * 100), 2).ToString();
                                    }

                                    //obj.MonthlyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly KPI Target"]) * 100), 2).ToString();
                                }

                                if (dt.Rows[i]["Monthly Challenge Target"].ToString() == "")
                                {
                                    obj.MonthlyChallengeTarget = null;
                                }
                                else
                                {

                                    if (dt.Rows[i]["Monthly Challenge Target"].ToString() == "0.00%")
                                    {
                                        obj.MonthlyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Challenge Target"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.MonthlyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Challenge Target"]) * 100), 2).ToString();
                                    }

                                    //obj.MonthlyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Challenge Target"]) * 100), 2).ToString();
                                }

                                if (dt.Rows[i]["Monthly Overall Result"].ToString() == "")
                                {
                                    obj.MonthlyOverallResult = null;
                                }
                                else
                                {

                                    if (dt.Rows[i]["Monthly Overall Result"].ToString() == "0.00%")
                                    {
                                        obj.MonthlyOverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.MonthlyOverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Overall Result"]) * 100), 2).ToString();
                                    }

                                    //obj.MonthlyOverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Overall Result"]) * 100), 2).ToString();

                                }

                                //obj.Month = dt.Rows[i]["Month"].ToString();
                                //obj.Year = dt.Rows[i]["Year"].ToString();

                                //----------------------------------------------------------

                                obj.WC_Section = dt.Rows[i]["WC_Section"].ToString();
                                obj.Workcenter = dt.Rows[i]["Workcenter"].ToString();

                                if (dt.Rows[i]["WC_Daily Result"].ToString() == "")
                                {
                                    obj.WC_DailyResult = null;
                                }
                                else
                                {

                                    if (dt.Rows[i]["WC_Daily Result"].ToString() == "0.00%")
                                    {
                                        obj.WC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Daily Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.WC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Daily Result"]) * 100), 2).ToString();
                                    }

                                    //obj.WC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Daily Result"]) * 100), 2).ToString();
                                }

                                if (dt.Rows[i]["WC_Overall Result"].ToString() == "")
                                {
                                    obj.WC_OverallResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["WC_Overall Result"].ToString() == "0.00%")
                                    {
                                        obj.WC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.WC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Overall Result"]) * 100), 2).ToString();
                                    }

                                    //obj.WC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Overall Result"]) * 100), 2).ToString();
                                }

                                obj.WC_Month = dt.Rows[i]["WC_Month"].ToString();
                                obj.WC_Date = dt.Rows[i]["WC_Date"].ToString();

                                //----------------------------------------------------------

                                obj.CC_Section = dt.Rows[i]["CC_Section"].ToString();
                                obj.Costcenter = dt.Rows[i]["Costcenter"].ToString();

                                if (dt.Rows[i]["CC_Daily Result"].ToString() == "")
                                {
                                    obj.CC_DailyResult = null;
                                }
                                else
                                {

                                    if (dt.Rows[i]["CC_Daily Result"].ToString() == "0.00%")
                                    {
                                        obj.CC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Daily Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.CC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Daily Result"]) * 100), 2).ToString();
                                    }

                                    //obj.CC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Daily Result"]) * 100), 2).ToString();
                                }

                                if (dt.Rows[i]["CC_Overall Result"].ToString() == "")
                                {
                                    obj.CC_OverallResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["CC_Overall Result"].ToString() == "0.00%")
                                    {
                                        obj.CC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.CC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Overall Result"]) * 100), 2).ToString();
                                    }

                                    //obj.CC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Overall Result"]) * 100), 2).ToString();
                                }

                                obj.CC_Month = dt.Rows[i]["CC_Month"].ToString();
                                obj.CC_Date = dt.Rows[i]["CC_Date"].ToString();

                                //----------------------------------------------------------

                                //obj.DailyDate = DateTime.ParseExact(dt.Rows[i]["Daily Date"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                                obj.Process_Section = dt.Rows[i]["Process_Section"].ToString();
                                obj.Process_Item = dt.Rows[i]["Process_Item"].ToString();

                                if (dt.Rows[i]["Process_Daily Result"].ToString() == "")
                                {
                                    obj.Process_DailyResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Process_Daily Result"].ToString() == "0.00%")
                                    {
                                        obj.Process_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Daily Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.Process_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Daily Result"]) * 100), 2).ToString();
                                    }

                                    //obj.Process_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Daily Result"]) * 100), 2).ToString();
                                }

                                if (dt.Rows[i]["Process_Overall Result"].ToString() == "")
                                {
                                    obj.Process_OverallResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Process_Overall Result"].ToString() == "0.00%")
                                    {
                                        obj.Process_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.Process_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Overall Result"]) * 100), 2).ToString();
                                    }

                                    //obj.Process_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Overall Result"]) * 100), 2).ToString();
                                }

                                obj.Process_Month = dt.Rows[i]["Process_Month"].ToString();
                                obj.Process_Date = dt.Rows[i]["Process_Date"].ToString();
                                //obj.WC_Date = DateTime.ParseExact(dt.Rows[i]["WC_Date"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);


                                if (dt.Rows[i]["Process_Date"].ToString() != "" || dt.Rows[i]["WC_Date"].ToString() != "" || dt.Rows[i]["CC_Date"].ToString() != "")
                                {
                                    obj.UploadDate = DateTime.Now.ToString();
                                }


                                list.Add(obj);
                            }

                            EfficiencyDatagrid.DataSource = list;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Please check the file if match to the selected template.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                    }
                    else if (TemplateDropdownList.Text == "Direct Efficiency")
                    {
                        try
                        {


                            List<DirectEfficiency_Class> list = new List<DirectEfficiency_Class>();

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DirectEfficiency_Class obj = new DirectEfficiency_Class();

                                obj.Section = dt.Rows[i]["Section"].ToString();

                                if (dt.Rows[i]["Daily KPI Target"].ToString() == "")
                                {
                                    obj.DailyKPITarget = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Daily KPI Target"].ToString() == "0.00%")
                                    {
                                        obj.DailyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Daily KPI Target"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.DailyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Daily KPI Target"]) * 100), 2).ToString();
                                    }

                                }

                                if (dt.Rows[i]["Daily Challenge Target"].ToString() == "")
                                {
                                    obj.DailyChallengeTarget = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Daily Challenge Target"].ToString() == "0.00%")
                                    {
                                        obj.DailyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Daily Challenge Target"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.DailyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Daily Challenge Target"]) * 100), 2).ToString();
                                    }

                                }

                                if (dt.Rows[i]["Overall result"].ToString() == "")
                                {
                                    obj.OverallResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Overall result"].ToString() == "0.00%")
                                    {
                                        obj.OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Overall result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Overall result"]) * 100), 2).ToString();
                                    }

                                }


                                //obj.Monthly_Section = dt.Rows[i]["Section (Monthly)"].ToString();

                                if (dt.Rows[i]["Monthly KPI Target"].ToString() == "")
                                {
                                    obj.MonthlyKPITarget = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Monthly KPI Target"].ToString() == "0.00%")
                                    {
                                        obj.MonthlyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly KPI Target"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.MonthlyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly KPI Target"]) * 100), 2).ToString();
                                    }

                                }


                                if (dt.Rows[i]["Monthly Challenge Target"].ToString() == "")
                                {
                                    obj.MonthlyChallengeTarget = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Monthly Challenge Target"].ToString() == "0.00%")
                                    {
                                        obj.MonthlyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Challenge Target"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.MonthlyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Challenge Target"]) * 100), 2).ToString();
                                    }

                                }

                                if (dt.Rows[i]["Monthly Overall Result"].ToString() == "")
                                {
                                    obj.MonthlyOverallResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Monthly Overall Result"].ToString() == "0.00%")
                                    {
                                        obj.MonthlyOverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.MonthlyOverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Overall Result"]) * 100), 2).ToString();
                                    }
                                }


                                obj.Date = dt.Rows[i]["Date"].ToString();
                                //obj.Month = dt.Rows[i]["Month"].ToString();
                                //obj.Year = dt.Rows[i]["Year"].ToString();

                                obj.Contributor_Section = dt.Rows[i]["Contributor_Section"].ToString();
                                obj.Contributor_ProcessItem = dt.Rows[i]["Contributor_Process_Item"].ToString();

                                if (dt.Rows[i]["Contributor Rate %"].ToString() == "")
                                {
                                    obj.Contributor_Rate = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Contributor Rate %"].ToString() == "0.00%")
                                    {
                                        obj.Contributor_Rate = Math.Round((Convert.ToDecimal(dt.Rows[i]["Contributor Rate %"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.Contributor_Rate = Math.Round((Convert.ToDecimal(dt.Rows[i]["Contributor Rate %"]) * 100), 2).ToString();
                                    }
                                }


                                obj.Contributor_ProcessDate = dt.Rows[i]["Contributor_Process_Date"].ToString();

                                obj.WC_Section = dt.Rows[i]["WC_Section"].ToString();
                                obj.Workcenter = dt.Rows[i]["Workcenter"].ToString();

                                if (dt.Rows[i]["WC_Daily Result"].ToString() == "")
                                {
                                    obj.WC_DailyResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["WC_Daily Result"].ToString() == "0.00%")
                                    {
                                        obj.WC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Daily Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.WC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Daily Result"]) * 100), 2).ToString();
                                    }
                                }

                                if (dt.Rows[i]["WC_Overall Result"].ToString() == "")
                                {
                                    obj.WC_OverallResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["WC_Overall Result"].ToString() == "0.00%")
                                    {
                                        obj.WC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.WC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Overall Result"]) * 100), 2).ToString();
                                    }
                                }


                                obj.WC_Month = dt.Rows[i]["WC_Month"].ToString();
                                obj.WC_Date = dt.Rows[i]["WC_Date"].ToString();

                                obj.CC_Section = dt.Rows[i]["CC_Section"].ToString();
                                obj.Costcenter = dt.Rows[i]["Costcenter"].ToString();

                                if (dt.Rows[i]["CC_Daily Result"].ToString() == "")
                                {
                                    obj.CC_DailyResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["CC_Daily Result"].ToString() == "0.00%")
                                    {
                                        obj.CC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Daily Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.CC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Daily Result"]) * 100), 2).ToString();
                                    }
                                }

                                if (dt.Rows[i]["CC_Overall Result"].ToString() == "")
                                {
                                    obj.CC_OverallResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["CC_Overall Result"].ToString() == "0.00%")
                                    {
                                        obj.CC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.CC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Overall Result"]) * 100), 2).ToString();
                                    }
                                }


                                obj.CC_Month = dt.Rows[i]["CC_Month"].ToString();
                                obj.CC_Date = dt.Rows[i]["CC_Date"].ToString();

                                obj.Process_Section = dt.Rows[i]["Process_Section"].ToString();
                                obj.Process_Item = dt.Rows[i]["Process_Item"].ToString();

                                if (dt.Rows[i]["Process_Daily Result"].ToString() == "")
                                {
                                    obj.Process_DailyResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Process_Daily Result"].ToString() == "0.00%")
                                    {
                                        obj.Process_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Daily Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.Process_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Daily Result"]) * 100), 2).ToString();
                                    }
                                }


                                if (dt.Rows[i]["Process_Overall Result"].ToString() == "")
                                {
                                    obj.Process_OverallResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Process_Overall Result"].ToString() == "0.00%")
                                    {
                                        obj.Process_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.Process_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Overall Result"]) * 100), 2).ToString();
                                    }
                                }

                                obj.Process_Month = dt.Rows[i]["Process_Month"].ToString();
                                obj.Process_Date = dt.Rows[i]["Process_Date"].ToString();

                                if (dt.Rows[i]["Contributor_Process_Date"].ToString() != "" || dt.Rows[i]["Process_Date"].ToString() != "" || dt.Rows[i]["WC_Date"].ToString() != "" || dt.Rows[i]["CC_Date"].ToString() != "")
                                {
                                    obj.UploadDate = DateTime.Now.ToString();
                                }

                                list.Add(obj);
                            }

                            EfficiencyDatagrid.DataSource = list;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Please check the file if match to the selected template.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                    }
                    else if (TemplateDropdownList.Text == "Semi-Direct Rate")
                    {
                        try
                        {
                            List<SemiDirectEfficiency_Class> list = new List<SemiDirectEfficiency_Class>();

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                SemiDirectEfficiency_Class obj = new SemiDirectEfficiency_Class();

                                obj.Section = dt.Rows[i]["Section"].ToString();

                                if (dt.Rows[i]["Daily KPI Target"].ToString() == "")
                                {
                                    obj.DailyKPITarget = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Daily KPI Target"].ToString() == "0.00%")
                                    {
                                        obj.DailyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Daily KPI Target"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.DailyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Daily KPI Target"]) * 100), 2).ToString();
                                    }

                                }

                                if (dt.Rows[i]["Daily Challenge Target"].ToString() == "")
                                {
                                    obj.DailyChallengeTarget = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Daily Challenge Target"].ToString() == "0.00%")
                                    {
                                        obj.DailyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Daily Challenge Target"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.DailyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Daily Challenge Target"]) * 100), 2).ToString();
                                    }

                                }

                                if (dt.Rows[i]["Overall result"].ToString() == "")
                                {
                                    obj.OverallResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Overall result"].ToString() == "0.00%")
                                    {
                                        obj.OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Overall result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Overall result"]) * 100), 2).ToString();
                                    }

                                }


                                //obj.Monthly_Section = dt.Rows[i]["Section (Monthly)"].ToString();

                                if (dt.Rows[i]["Monthly KPI Target"].ToString() == "")
                                {
                                    obj.MonthlyKPITarget = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Monthly KPI Target"].ToString() == "0.00%")
                                    {
                                        obj.MonthlyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly KPI Target"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.MonthlyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly KPI Target"]) * 100), 2).ToString();
                                    }

                                }


                                if (dt.Rows[i]["Monthly Challenge Target"].ToString() == "")
                                {
                                    obj.MonthlyChallengeTarget = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Monthly Challenge Target"].ToString() == "0.00%")
                                    {
                                        obj.MonthlyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Challenge Target"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.MonthlyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Challenge Target"]) * 100), 2).ToString();
                                    }

                                }

                                if (dt.Rows[i]["Monthly Overall Result"].ToString() == "")
                                {
                                    obj.MonthlyOverallResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Monthly Overall Result"].ToString() == "0.00%")
                                    {
                                        obj.MonthlyOverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.MonthlyOverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Overall Result"]) * 100), 2).ToString();
                                    }
                                }


                                obj.Date = dt.Rows[i]["Date"].ToString();
                                //obj.Month = dt.Rows[i]["Month"].ToString();
                                //obj.Year = dt.Rows[i]["Year"].ToString();

                                obj.Contributor_Section = dt.Rows[i]["Contributor_Section"].ToString();
                                obj.Contributor_ManpowerItem = dt.Rows[i]["Contributor_Manpower_Item"].ToString();

                                if (dt.Rows[i]["Contributor Rate %"].ToString() == "")
                                {
                                    obj.Contributor_Rate = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Contributor Rate %"].ToString() == "0.00%")
                                    {
                                        obj.Contributor_Rate = Math.Round((Convert.ToDecimal(dt.Rows[i]["Contributor Rate %"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.Contributor_Rate = Math.Round((Convert.ToDecimal(dt.Rows[i]["Contributor Rate %"]) * 100), 2).ToString();
                                    }
                                }

                                obj.Contributor_ProcessDate = dt.Rows[i]["Contributor_Process_Date"].ToString();

                                obj.WC_Section = dt.Rows[i]["WC_Section"].ToString();
                                obj.Workcenter = dt.Rows[i]["Workcenter"].ToString();

                                if (dt.Rows[i]["WC_Daily Result"].ToString() == "")
                                {
                                    obj.WC_DailyResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["WC_Daily Result"].ToString() == "0.00%")
                                    {
                                        obj.WC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Daily Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.WC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Daily Result"]) * 100), 2).ToString();
                                    }
                                }

                                if (dt.Rows[i]["WC_Overall Result"].ToString() == "")
                                {
                                    obj.WC_OverallResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["WC_Overall Result"].ToString() == "0.00%")
                                    {
                                        obj.WC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.WC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Overall Result"]) * 100), 2).ToString();
                                    }
                                }


                                obj.WC_Month = dt.Rows[i]["WC_Month"].ToString();
                                obj.WC_Date = dt.Rows[i]["WC_Date"].ToString();

                                obj.CC_Section = dt.Rows[i]["CC_Section"].ToString();
                                obj.Costcenter = dt.Rows[i]["Costcenter"].ToString();

                                if (dt.Rows[i]["CC_Daily Result"].ToString() == "")
                                {
                                    obj.CC_DailyResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["CC_Daily Result"].ToString() == "0.00%")
                                    {
                                        obj.CC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Daily Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.CC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Daily Result"]) * 100), 2).ToString();
                                    }
                                }

                                if (dt.Rows[i]["CC_Overall Result"].ToString() == "")
                                {
                                    obj.CC_OverallResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["CC_Overall Result"].ToString() == "0.00%")
                                    {
                                        obj.CC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.CC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Overall Result"]) * 100), 2).ToString();
                                    }
                                }


                                obj.CC_Month = dt.Rows[i]["CC_Month"].ToString();
                                obj.CC_Date = dt.Rows[i]["CC_Date"].ToString();

                                obj.Process_Section = dt.Rows[i]["Process_Section"].ToString();
                                obj.Process_Item = dt.Rows[i]["Process_Item"].ToString();

                                if (dt.Rows[i]["Process_Daily Result"].ToString() == "")
                                {
                                    obj.Process_DailyResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Process_Daily Result"].ToString() == "0.00%")
                                    {
                                        obj.Process_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Daily Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.Process_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Daily Result"]) * 100), 2).ToString();
                                    }
                                }


                                if (dt.Rows[i]["Process_Overall Result"].ToString() == "")
                                {
                                    obj.Process_OverallResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Process_Overall Result"].ToString() == "0.00%")
                                    {
                                        obj.Process_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.Process_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Overall Result"]) * 100), 2).ToString();
                                    }
                                }

                                obj.Process_Month = dt.Rows[i]["Process_Month"].ToString();
                                obj.Process_Date = dt.Rows[i]["Process_Date"].ToString();

                                obj.Manpower_Section = dt.Rows[i]["Manpower_Section"].ToString();
                                obj.Manpower_Item = dt.Rows[i]["Manpower_Item"].ToString();

                                if (dt.Rows[i]["Manpower_Daily Result"].ToString() == "")
                                {
                                    obj.Manpower_DailyResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Manpower_Daily Result"].ToString() == "0.00%")
                                    {
                                        obj.Manpower_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Manpower_Daily Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.Manpower_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Manpower_Daily Result"]) * 100), 2).ToString();
                                    }
                                }

                                if (dt.Rows[i]["Manpower_Overall Result"].ToString() == "")
                                {
                                    obj.Manpower_OverallResult = null;
                                }
                                else
                                {
                                    if (dt.Rows[i]["Manpower_Overall Result"].ToString() == "0.00%")
                                    {
                                        obj.Manpower_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Manpower_Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                    }
                                    else
                                    {
                                        obj.Manpower_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Manpower_Overall Result"]) * 100), 2).ToString();
                                    }
                                }

                                obj.Manpower_Month = dt.Rows[i]["Manpower_Month"].ToString();
                                obj.Manpower_Date = dt.Rows[i]["Manpower_Date"].ToString();

                                if (dt.Rows[i]["Contributor_Process_Date"].ToString() != "" || dt.Rows[i]["Manpower_Date"].ToString() != "" || dt.Rows[i]["Process_Date"].ToString() != "" || dt.Rows[i]["WC_Date"].ToString() != "" || dt.Rows[i]["CC_Date"].ToString() != "")
                                {
                                    obj.UploadDate = DateTime.Now.ToString();
                                }


                                list.Add(obj);

                            }

                            EfficiencyDatagrid.DataSource = list;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Please check the file if match to the selected template.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else if (TemplateDropdownList.Text == "Total Loss Rate")
                    {
                        //try
                        //{

                        List<LossRate_Class> list = new List<LossRate_Class>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            LossRate_Class obj = new LossRate_Class();

                            obj.Section = dt.Rows[i]["Section"].ToString();

                            if (dt.Rows[i]["Daily KPI Target"].ToString() == "")
                            {
                                obj.DailyKPITarget = null;
                            }
                            else
                            {
                                if (dt.Rows[i]["Daily KPI Target"].ToString() == "0.00%")
                                {
                                    obj.DailyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Daily KPI Target"].ToString().Replace("%", "")) * 100), 2).ToString();
                                }
                                else
                                {
                                    obj.DailyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Daily KPI Target"]) * 100), 2).ToString();
                                }

                            }

                            if (dt.Rows[i]["Daily Challenge Target"].ToString() == "" || dt.Rows[i]["Daily Challenge Target"].ToString() == null)
                            {
                                obj.DailyChallengeTarget = null;
                            }
                            else
                            {
                                if (dt.Rows[i]["Daily Challenge Target"].ToString() == "0.00%")
                                {
                                    obj.DailyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Daily Challenge Target"].ToString().Replace("%", "")) * 100), 2).ToString();
                                }
                                else
                                {
                                    obj.DailyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Daily Challenge Target"]) * 100), 2).ToString();
                                }

                            }

                            if (dt.Rows[i]["Overall result"].ToString() == "")
                            {
                                obj.OverallResult = null;
                            }
                            else
                            {
                                if (dt.Rows[i]["Overall result"].ToString() == "0.00%")
                                {
                                    obj.OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Overall result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                }
                                else
                                {
                                    obj.OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Overall result"]) * 100), 2).ToString();
                                }

                            }


                            //obj.Monthly_Section = dt.Rows[i]["Section (Monthly)"].ToString();

                            if (dt.Rows[i]["Monthly KPI Target"].ToString() == "")
                            {
                                obj.MonthlyKPITarget = null;
                            }
                            else
                            {
                                if (dt.Rows[i]["Monthly KPI Target"].ToString() == "0.00%")
                                {
                                    obj.MonthlyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly KPI Target"].ToString().Replace("%", "")) * 100), 2).ToString();
                                }
                                else
                                {
                                    obj.MonthlyKPITarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly KPI Target"]) * 100), 2).ToString();
                                }

                            }


                            if (dt.Rows[i]["Monthly Challenge Target"].ToString() == "")
                            {
                                obj.MonthlyChallengeTarget = null;
                            }
                            else
                            {
                                if (dt.Rows[i]["Monthly Challenge Target"].ToString() == "0.00%")
                                {
                                    obj.MonthlyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Challenge Target"].ToString().Replace("%", "")) * 100), 2).ToString();
                                }
                                else
                                {
                                    obj.MonthlyChallengeTarget = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Challenge Target"]) * 100), 2).ToString();
                                }

                            }

                            if (dt.Rows[i]["Monthly Overall Result"].ToString() == "")
                            {
                                obj.MonthlyOverallResult = null;
                            }
                            else
                            {
                                if (dt.Rows[i]["Monthly Overall Result"].ToString() == "0.00%")
                                {
                                    obj.MonthlyOverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                }
                                else
                                {
                                    obj.MonthlyOverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Monthly Overall Result"]) * 100), 2).ToString();
                                }
                            }


                            obj.Date = dt.Rows[i]["Date"].ToString();
                            //obj.Month = dt.Rows[i]["Month"].ToString();
                            //obj.Year = dt.Rows[i]["Year"].ToString();

                            obj.Contributor_Section = dt.Rows[i]["Contributor_Section"].ToString();
                            obj.Contributor_ManpowerItem = dt.Rows[i]["Contributor_Manpower_Item"].ToString();

                            if (dt.Rows[i]["Contributor Rate %"].ToString() == "")
                            {
                                obj.Contributor_Rate = null;
                            }
                            else
                            {
                                if (dt.Rows[i]["Contributor Rate %"].ToString() == "0.00%")
                                {
                                    obj.Contributor_Rate = Math.Round((Convert.ToDecimal(dt.Rows[i]["Contributor Rate %"].ToString().Replace("%", "")) * 100), 2).ToString();
                                }
                                else
                                {
                                    obj.Contributor_Rate = Math.Round((Convert.ToDecimal(dt.Rows[i]["Contributor Rate %"]) * 100), 2).ToString();
                                }
                            }

                            obj.Contributor_ProcessDate = dt.Rows[i]["Contributor_Process_Date"].ToString();

                            obj.WC_Section = dt.Rows[i]["WC_Section"].ToString();
                            obj.Workcenter = dt.Rows[i]["Workcenter"].ToString();

                            if (dt.Rows[i]["WC_Daily Result"].ToString() == "")
                            {
                                obj.WC_DailyResult = null;
                            }
                            else
                            {
                                if (dt.Rows[i]["WC_Daily Result"].ToString() == "0.00%")
                                {
                                    obj.WC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Daily Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                }
                                else
                                {
                                    obj.WC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Daily Result"]) * 100), 2).ToString();
                                }
                            }

                            if (dt.Rows[i]["WC_Overall Result"].ToString() == "")
                            {
                                obj.WC_OverallResult = null;
                            }
                            else
                            {
                                if (dt.Rows[i]["WC_Overall Result"].ToString() == "0.00%")
                                {
                                    obj.WC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                }
                                else
                                {
                                    obj.WC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["WC_Overall Result"]) * 100), 2).ToString();
                                }
                            }


                            obj.WC_Month = dt.Rows[i]["WC_Month"].ToString();
                            obj.WC_Date = dt.Rows[i]["WC_Date"].ToString();

                            obj.CC_Section = dt.Rows[i]["CC_Section"].ToString();
                            obj.Costcenter = dt.Rows[i]["Costcenter"].ToString();

                            if (dt.Rows[i]["CC_Daily Result"].ToString() == "")
                            {
                                obj.CC_DailyResult = null;
                            }
                            else
                            {
                                if (dt.Rows[i]["CC_Daily Result"].ToString() == "0.00%")
                                {
                                    obj.CC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Daily Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                }
                                else
                                {
                                    obj.CC_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Daily Result"]) * 100), 2).ToString();
                                }
                            }

                            if (dt.Rows[i]["CC_Overall Result"].ToString() == "")
                            {
                                obj.CC_OverallResult = null;
                            }
                            else
                            {
                                if (dt.Rows[i]["CC_Overall Result"].ToString() == "0.00%")
                                {
                                    obj.CC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                }
                                else
                                {
                                    obj.CC_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["CC_Overall Result"]) * 100), 2).ToString();
                                }
                            }


                            obj.CC_Month = dt.Rows[i]["CC_Month"].ToString();
                            obj.CC_Date = dt.Rows[i]["CC_Date"].ToString();

                            obj.Process_Section = dt.Rows[i]["Process_Section"].ToString();
                            obj.Process_Item = dt.Rows[i]["Process_Item"].ToString();

                            if (dt.Rows[i]["Process_Daily Result"].ToString() == "")
                            {
                                obj.Process_DailyResult = null;
                            }
                            else
                            {
                                if (dt.Rows[i]["Process_Daily Result"].ToString() == "0.00%")
                                {
                                    obj.Process_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Daily Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                }
                                else
                                {
                                    obj.Process_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Daily Result"]) * 100), 2).ToString();
                                }
                            }


                            if (dt.Rows[i]["Process_Overall Result"].ToString() == "")
                            {
                                obj.Process_OverallResult = null;
                            }
                            else
                            {
                                if (dt.Rows[i]["Process_Overall Result"].ToString() == "0.00%")
                                {
                                    obj.Process_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                }
                                else
                                {
                                    obj.Process_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Process_Overall Result"]) * 100), 2).ToString();
                                }
                            }

                            obj.Process_Month = dt.Rows[i]["Process_Month"].ToString();
                            obj.Process_Date = dt.Rows[i]["Process_Date"].ToString();

                            obj.Loss_Section = dt.Rows[i]["Loss_Section"].ToString();
                            obj.Loss_Item = dt.Rows[i]["Loss_Item"].ToString();

                            if (dt.Rows[i]["Loss_Daily Result"].ToString() == "")
                            {
                                obj.Loss_DailyResult = null;
                            }
                            else
                            {
                                if (dt.Rows[i]["Loss_Daily Result"].ToString() == "0.00%")
                                {
                                    obj.Loss_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Loss_Daily Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                }
                                else
                                {
                                    obj.Loss_DailyResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Loss_Daily Result"]) * 100), 2).ToString();
                                }
                            }

                            if (dt.Rows[i]["Loss_Overall Result"].ToString() == "")
                            {
                                obj.Loss_OverallResult = null;
                            }
                            else
                            {
                                if (dt.Rows[i]["Loss_Overall Result"].ToString() == "0.00%")
                                {
                                    obj.Loss_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Loss_Overall Result"].ToString().Replace("%", "")) * 100), 2).ToString();
                                }
                                else
                                {
                                    obj.Loss_OverallResult = Math.Round((Convert.ToDecimal(dt.Rows[i]["Loss_Overall Result"]) * 100), 2).ToString();
                                }
                            }

                            //obj.Loss_Month = dt.Rows[i]["Loss_Month"].ToString();
                            obj.Loss_Date = dt.Rows[i]["Loss_Date"].ToString();

                            if (dt.Rows[i]["Contributor_Process_Date"].ToString() != "" || dt.Rows[i]["WC_Date"].ToString() != "" || dt.Rows[i]["CC_Date"].ToString() != "" || dt.Rows[i]["Process_Date"].ToString() != "" || dt.Rows[i]["Loss_Date"].ToString() != "")
                            {
                                obj.UploadDate = DateTime.Now.ToString();
                            }

                            list.Add(obj);
                        }

                        EfficiencyDatagrid.DataSource = list;
                        //}
                        // catch (Exception ex)
                        //{
                        //    MessageBox.Show("Please check the file if match to the selected template.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //}
                    }
                    else if (TemplateDropdownList.Text == "Efficiency Summary")
                    {
                        try
                        {


                            List<EfficiencySummary_Class> list = new List<EfficiencySummary_Class>();

                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }


                            SqlCommand SelectEffSummaryUpdateCount = new SqlCommand("SP_SelectEffSummaryUpdateCount", con);
                            SelectEffSummaryUpdateCount.CommandType = CommandType.StoredProcedure;
                            //SelectEffSummaryUpdateCount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter da = new SqlDataAdapter(SelectEffSummaryUpdateCount);
                            DataTable data = new DataTable();
                            da.Fill(data);
                            con.Close();

                            if (dt.Rows.Count > 0)
                            {
                                con.Open();
                                SqlDataReader reader = SelectEffSummaryUpdateCount.ExecuteReader();
                                if (reader.Read())
                                {
                                    UpdateCount = reader[0].ToString(); //UpdateCount Column

                                    reader.Close();
                                }
                            }
                            else
                            {
                                UpdateCount = "0";
                            }

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                EfficiencySummary_Class obj = new EfficiencySummary_Class();

                                //obj.TotalEfficiency = dt.Rows[i]["TOTAL EFFICIENCY"].ToString();
                                //obj.MonthlyKPITarget = dt.Rows[i]["Monthly KPI Target"].ToString();
                                //obj.MonthlyChallengeTarget = dt.Rows[i]["Monthly Challenge Target"].ToString();
                                //obj.MonthlyCumulativeKPITarget = dt.Rows[i]["Monthly Cumulative KPI Target"].ToString();
                                //obj.MonthlyCumulativeChallengeTarget = dt.Rows[i]["Monthly Cumulative Challenge Target"].ToString();
                                //obj.AnnualRecoveryTarget = dt.Rows[i]["Annual Recovery Target"].ToString();
                                //obj.MonthlyCumulativeActual = dt.Rows[i]["Monthly Cumulative Actual"].ToString();
                                //obj.YearlyResult = dt.Rows[i]["Yearly Result"].ToString();
                                //obj.MonthlyDate = dt.Rows[i]["Monthly Date"].ToString();
                                //obj.Yearly = dt.Rows[i]["Yearly"].ToString();

                                obj.Section = dt.Rows[i]["Section"].ToString();
                                obj.TotalEfficiency = dt.Rows[i]["TOTAL EFFICIENCY"].ToString();
                                obj.TE_Apr = Math.Round((Convert.ToDecimal(dt.Rows[i]["TE_Apr"]) * 100), 2).ToString();
                                obj.TE_May = Math.Round((Convert.ToDecimal(dt.Rows[i]["TE_May"]) * 100), 2).ToString();
                                obj.TE_Jun = Math.Round((Convert.ToDecimal(dt.Rows[i]["TE_Jun"]) * 100), 2).ToString();
                                obj.TE_Jul = Math.Round((Convert.ToDecimal(dt.Rows[i]["TE_Jul"]) * 100), 2).ToString();
                                obj.TE_Aug = Math.Round((Convert.ToDecimal(dt.Rows[i]["TE_Aug"]) * 100), 2).ToString();
                                obj.TE_Sep = Math.Round((Convert.ToDecimal(dt.Rows[i]["TE_Sep"]) * 100), 2).ToString();
                                obj.TE_Oct = Math.Round((Convert.ToDecimal(dt.Rows[i]["TE_Oct"]) * 100), 2).ToString();
                                obj.TE_Nov = Math.Round((Convert.ToDecimal(dt.Rows[i]["TE_Nov"]) * 100), 2).ToString();
                                obj.TE_Dec = Math.Round((Convert.ToDecimal(dt.Rows[i]["TE_Dec"]) * 100), 2).ToString();
                                obj.TE_Jan = Math.Round((Convert.ToDecimal(dt.Rows[i]["TE_Jan"]) * 100), 2).ToString();
                                obj.TE_Feb = Math.Round((Convert.ToDecimal(dt.Rows[i]["TE_Feb"]) * 100), 2).ToString();
                                obj.TE_Mar = Math.Round((Convert.ToDecimal(dt.Rows[i]["TE_Mar"]) * 100), 2).ToString();
                                obj.TE_Yearly = Math.Round((Convert.ToDecimal(dt.Rows[i]["TE_Yearly"]) * 100), 2).ToString();
                                obj.UploadCount = (Convert.ToInt32(UpdateCount) + 1).ToString();
                                obj.UploadDate = DateTime.Now.ToString();
                                obj.Year = YearDropdown.Text;

                                obj.DirectEfficiency = dt.Rows[i]["DIRECT EFFICIENCY"].ToString();
                                obj.DE_Apr = Math.Round((Convert.ToDecimal(dt.Rows[i]["DE_Apr"]) * 100), 2).ToString();
                                obj.DE_May = Math.Round((Convert.ToDecimal(dt.Rows[i]["DE_May"]) * 100), 2).ToString();
                                obj.DE_Jun = Math.Round((Convert.ToDecimal(dt.Rows[i]["DE_Jun"]) * 100), 2).ToString();
                                obj.DE_Jul = Math.Round((Convert.ToDecimal(dt.Rows[i]["DE_Jul"]) * 100), 2).ToString();
                                obj.DE_Aug = Math.Round((Convert.ToDecimal(dt.Rows[i]["DE_Aug"]) * 100), 2).ToString();
                                obj.DE_Sep = Math.Round((Convert.ToDecimal(dt.Rows[i]["DE_Sep"]) * 100), 2).ToString();
                                obj.DE_Oct = Math.Round((Convert.ToDecimal(dt.Rows[i]["DE_Oct"]) * 100), 2).ToString();
                                obj.DE_Nov = Math.Round((Convert.ToDecimal(dt.Rows[i]["DE_Nov"]) * 100), 2).ToString();
                                obj.DE_Dec = Math.Round((Convert.ToDecimal(dt.Rows[i]["DE_Dec"]) * 100), 2).ToString();
                                obj.DE_Jan = Math.Round((Convert.ToDecimal(dt.Rows[i]["DE_Jan"]) * 100), 2).ToString();
                                obj.DE_Feb = Math.Round((Convert.ToDecimal(dt.Rows[i]["DE_Feb"]) * 100), 2).ToString();
                                obj.DE_Mar = Math.Round((Convert.ToDecimal(dt.Rows[i]["DE_Mar"]) * 100), 2).ToString();
                                obj.DE_Yearly = Math.Round((Convert.ToDecimal(dt.Rows[i]["DE_Yearly"]) * 100), 2).ToString();
                                obj.UploadCount = (Convert.ToInt32(UpdateCount) + 1).ToString();
                                obj.UploadDate = DateTime.Now.ToString();
                                obj.Year = YearDropdown.Text;

                                obj.SemiDirectRate = dt.Rows[i]["SEMI-DIRECT RATE"].ToString();
                                obj.SDR_Apr = Math.Round((Convert.ToDecimal(dt.Rows[i]["SDR_Apr"]) * 100), 2).ToString();
                                obj.SDR_May = Math.Round((Convert.ToDecimal(dt.Rows[i]["SDR_May"]) * 100), 2).ToString();
                                obj.SDR_Jun = Math.Round((Convert.ToDecimal(dt.Rows[i]["SDR_Jun"]) * 100), 2).ToString();
                                obj.SDR_Jul = Math.Round((Convert.ToDecimal(dt.Rows[i]["SDR_Jul"]) * 100), 2).ToString();
                                obj.SDR_Aug = Math.Round((Convert.ToDecimal(dt.Rows[i]["SDR_Aug"]) * 100), 2).ToString();
                                obj.SDR_Sep = Math.Round((Convert.ToDecimal(dt.Rows[i]["SDR_Sep"]) * 100), 2).ToString();
                                obj.SDR_Oct = Math.Round((Convert.ToDecimal(dt.Rows[i]["SDR_Oct"]) * 100), 2).ToString();
                                obj.SDR_Nov = Math.Round((Convert.ToDecimal(dt.Rows[i]["SDR_Nov"]) * 100), 2).ToString();
                                obj.SDR_Dec = Math.Round((Convert.ToDecimal(dt.Rows[i]["SDR_Dec"]) * 100), 2).ToString();
                                obj.SDR_Jan = Math.Round((Convert.ToDecimal(dt.Rows[i]["SDR_Jan"]) * 100), 2).ToString();
                                obj.SDR_Feb = Math.Round((Convert.ToDecimal(dt.Rows[i]["SDR_Feb"]) * 100), 2).ToString();
                                obj.SDR_Mar = Math.Round((Convert.ToDecimal(dt.Rows[i]["SDR_Mar"]) * 100), 2).ToString();
                                obj.SDR_Yearly = Math.Round((Convert.ToDecimal(dt.Rows[i]["SDR_Yearly"]) * 100), 2).ToString();
                                obj.UploadCount = (Convert.ToInt32(UpdateCount) + 1).ToString();
                                obj.UploadDate = DateTime.Now.ToString();
                                obj.Year = YearDropdown.Text;

                                obj.TotalLossRate = dt.Rows[i]["TOTAL LOSS RATE"].ToString();
                                obj.TLR_Apr = Math.Round((Convert.ToDecimal(dt.Rows[i]["TLR_Apr"]) * 100), 2).ToString();
                                obj.TLR_May = Math.Round((Convert.ToDecimal(dt.Rows[i]["TLR_May"]) * 100), 2).ToString();
                                obj.TLR_Jun = Math.Round((Convert.ToDecimal(dt.Rows[i]["TLR_Jun"]) * 100), 2).ToString();
                                obj.TLR_Jul = Math.Round((Convert.ToDecimal(dt.Rows[i]["TLR_Jul"]) * 100), 2).ToString();
                                obj.TLR_Aug = Math.Round((Convert.ToDecimal(dt.Rows[i]["TLR_Aug"]) * 100), 2).ToString();
                                obj.TLR_Sep = Math.Round((Convert.ToDecimal(dt.Rows[i]["TLR_Sep"]) * 100), 2).ToString();
                                obj.TLR_Oct = Math.Round((Convert.ToDecimal(dt.Rows[i]["TLR_Oct"]) * 100), 2).ToString();
                                obj.TLR_Nov = Math.Round((Convert.ToDecimal(dt.Rows[i]["TLR_Nov"]) * 100), 2).ToString();
                                obj.TLR_Dec = Math.Round((Convert.ToDecimal(dt.Rows[i]["TLR_Dec"]) * 100), 2).ToString();
                                obj.TLR_Jan = Math.Round((Convert.ToDecimal(dt.Rows[i]["TLR_Jan"]) * 100), 2).ToString();
                                obj.TLR_Feb = Math.Round((Convert.ToDecimal(dt.Rows[i]["TLR_Feb"]) * 100), 2).ToString();
                                obj.TLR_Mar = Math.Round((Convert.ToDecimal(dt.Rows[i]["TLR_Mar"]) * 100), 2).ToString();
                                obj.TLR_Yearly = Math.Round((Convert.ToDecimal(dt.Rows[i]["TLR_Yearly"]) * 100), 2).ToString();
                                obj.UploadCount = (Convert.ToInt32(UpdateCount) + 1).ToString();
                                obj.UploadDate = DateTime.Now.ToString();
                                obj.Year = YearDropdown.Text;

                                list.Add(obj);
                            }

                            EfficiencyDatagrid.DataSource = list;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Please check the file if match to the selected template.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else if (TemplateDropdownList.Text == "Total Efficiency Graph")
                    {
                        try
                        {
                            List<TotalEff_Graph_Class> list = new List<TotalEff_Graph_Class>();

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                TotalEff_Graph_Class obj = new TotalEff_Graph_Class();

                                //Daily
                                obj.Daily_Section = dt.Rows[i]["Daily_Section"].ToString();
                                obj.Daily_Date = dt.Rows[i]["Daily_Date"].ToString();
                                obj.DirectContributor = dt.Rows[i]["Direct Contributor (%)"].ToString();
                                obj.LossManhourContributor = dt.Rows[i]["Loss Manhour Contributor (%)"].ToString();
                                obj.SemiDirectContributor = dt.Rows[i]["Semi-Direct Contributor (%)"].ToString();
                                obj.Daily_FiscalYear = dt.Rows[i]["Fiscal Year"].ToString();

                                //Graph
                                obj.Graph_Section = dt.Rows[i]["Graph_Section"].ToString();
                                obj.Graph_Value = dt.Rows[i]["Graph_Value"].ToString();
                                obj.Graph_DirectContributor = dt.Rows[i]["Graph_Direct Contributor (%)"].ToString();
                                obj.Graph_SemiDirectContributor = dt.Rows[i]["Graph_Semi-Direct Contributor (%)"].ToString();
                                obj.Graph_LossManhourContributor = dt.Rows[i]["Graph_Loss Manhour Contributor (%)"].ToString();
                                obj.Graph_Type = dt.Rows[i]["Graph_Type"].ToString();
                                obj.Graph_Date = dt.Rows[i]["Graph_Date"].ToString();

                                //Monthly
                                obj.Monthly_Section = dt.Rows[i]["Monthly_Section"].ToString();
                                obj.Monthly_Month = dt.Rows[i]["Monthly_Month"].ToString();
                                obj.Monthly_Value = dt.Rows[i]["Monthly_Value"].ToString();
                                obj.Monthly_DirectContributor = dt.Rows[i]["Monthly_Direct Contributor (%)"].ToString();
                                obj.Monthly_LossManhourContributor = dt.Rows[i]["Monthly_Loss Manhour Contributor (%)"].ToString();
                                obj.Monthly_SemiDirectContributor = dt.Rows[i]["Monthly_Semi-Direct Contributor (%)"].ToString();
                                obj.Monthly_Type = dt.Rows[i]["Monthly_Type"].ToString();
                                obj.Monthly_FiscalYear = dt.Rows[i]["Monthly_FiscalYear"].ToString();

                                //Yearly
                                obj.Yearly_Section = dt.Rows[i]["Yearly_Section"].ToString();
                                obj.Yearly_Value = dt.Rows[i]["Yearly_Value"].ToString();
                                obj.Yearly_Type = dt.Rows[i]["Yearly_Type"].ToString();
                                obj.Yearly_FiscalYear = dt.Rows[i]["Yearly_Fiscal Year"].ToString();


                                if (dt.Rows[i]["Daily_Section"].ToString() != "" || dt.Rows[i]["Yearly_Section"].ToString() != "" || dt.Rows[i]["Graph_Section"].ToString() != "" || dt.Rows[i]["Monthly_Section"].ToString() != "")
                                {
                                    obj.UploadDate = DateTime.Now.ToString();
                                }

                                list.Add(obj);
                            }

                            EfficiencyDatagrid.DataSource = list;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Please check the file if match to the selected template.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                    }
                    else if (TemplateDropdownList.Text == "Direct Efficiency Graph PR1")
                    {


                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }

                        //try
                        //{
                        //List<DirectEff_Graph_PR1> list = new List<DirectEff_Graph_PR1>();

                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    DirectEff_Graph_PR1 obj = new DirectEff_Graph_PR1();

                        //    obj.Monthly_Section = dt.Rows[i]["Monthly_Section"].ToString();
                        //    obj.Monthly_Month = dt.Rows[i]["Monthly_Month"].ToString();
                        //    obj.Monthly_Value = dt.Rows[i]["Monthly_Value"].ToString();
                        //    obj.Monthly_C4130 = dt.Rows[i]["Monthly_C4130"].ToString();
                        //    obj.Monthly_RegNP = dt.Rows[i]["Monthly_RegNP"].ToString();
                        //    obj.Monthly_PrtSmple = dt.Rows[i]["Monthly_PrtSmple"].ToString();
                        //    obj.Monthly_BZNP = dt.Rows[i]["Monthly_BZNP"].ToString();
                        //    obj.Monthly_HTNP = dt.Rows[i]["Monthly_HTNP"].ToString();
                        //    obj.Monthly_W301 = dt.Rows[i]["Monthly_W301"].ToString();
                        //    obj.Monthly_R005DPRT = dt.Rows[i]["Monthly_R005DPRT"].ToString();
                        //    obj.Monthly_W302 = dt.Rows[i]["Monthly_W302"].ToString();
                        //    obj.Monthly_W303 = dt.Rows[i]["Monthly_W303"].ToString();
                        //    obj.Monthly_W304 = dt.Rows[i]["Monthly_W304"].ToString();
                        //    obj.Monthly_WB01 = dt.Rows[i]["Monthly_WB01"].ToString();
                        //    obj.Monthly_WB02 = dt.Rows[i]["Monthly_WB02"].ToString();
                        //    obj.Monthly_W305 = dt.Rows[i]["Monthly_W305"].ToString();
                        //    obj.Monthly_W306 = dt.Rows[i]["Monthly_W306"].ToString();
                        //    obj.Monthly_W307 = dt.Rows[i]["Monthly_W307"].ToString();
                        //    obj.Monthly_W308 = dt.Rows[i]["Monthly_W308"].ToString();
                        //    obj.Monthly_Type = dt.Rows[i]["Monthly_Type"].ToString();
                        //    obj.Monthly_FiscalYear = dt.Rows[i]["Monthly_FiscalYear"].ToString();

                        //    obj.Graph_Section = dt.Rows[i]["Graph_Section"].ToString();
                        //    obj.Graph_Value = dt.Rows[i]["Graph_Value"].ToString();
                        //    obj.Graph_C4130 = dt.Rows[i]["Graph_C4130"].ToString();
                        //    obj.Graph_RegNP = dt.Rows[i]["Graph_RegNP"].ToString();
                        //    obj.Graph_PrtSmple = dt.Rows[i]["Graph_PrtSmple"].ToString();
                        //    obj.Graph_BZNP = dt.Rows[i]["Graph_BZNP"].ToString();
                        //    obj.Graph_HTNP = dt.Rows[i]["Graph_HTNP"].ToString();
                        //    obj.Graph_W301 = dt.Rows[i]["Graph_W301"].ToString();
                        //    obj.Graph_R005DPRT = dt.Rows[i]["Graph_R005DPRT"].ToString();
                        //    obj.Graph_W302 = dt.Rows[i]["Graph_W302"].ToString();
                        //    obj.Graph_W303 = dt.Rows[i]["Graph_W303"].ToString();
                        //    obj.Graph_W304 = dt.Rows[i]["Graph_W304"].ToString();
                        //    obj.Graph_WB01 = dt.Rows[i]["Graph_WB01"].ToString();
                        //    obj.Graph_WB02 = dt.Rows[i]["Graph_WB02"].ToString();
                        //    obj.Graph_W305 = dt.Rows[i]["Graph_W305"].ToString();
                        //    obj.Graph_W306 = dt.Rows[i]["Graph_W306"].ToString();
                        //    obj.Graph_W307 = dt.Rows[i]["Graph_W307"].ToString();
                        //    obj.Graph_W308 = dt.Rows[i]["Graph_W308"].ToString();
                        //    obj.Graph_Type = dt.Rows[i]["Graph_Type"].ToString();
                        //    obj.Graph_Date = dt.Rows[i]["Graph_Date"].ToString();

                        //    obj.Yearly_Section = dt.Rows[i]["Yearly_Section"].ToString();
                        //    obj.Yearly_Value = dt.Rows[i]["Yearly_Value"].ToString();
                        //    obj.Yearly_Type = dt.Rows[i]["Yearly_Type"].ToString();
                        //    obj.Yearly_FiscalYear = dt.Rows[i]["Yearly_Fiscal Year"].ToString();

                        //    if (dt.Rows[i]["Graph_Section"].ToString() != "")
                        //    {
                        //        obj.UploadDate = DateTime.Now.ToString();
                        //    }

                        //    list.Add(obj);
                        //}

                        //EfficiencyDatagrid.DataSource = list;

                    }
                    else if (TemplateDropdownList.Text == "Direct Efficiency Graph PR2")
                    {

                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }

                        //try
                        //{
                        //List<DirectEff_Graph_PR2> list = new List<DirectEff_Graph_PR2>();

                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    DirectEff_Graph_PR2 obj = new DirectEff_Graph_PR2();

                        //    obj.Monthly_Section = dt.Rows[i]["Monthly_Section"].ToString();
                        //    obj.Monthly_Month = dt.Rows[i]["Monthly_Month"].ToString();
                        //    obj.Monthly_Value = dt.Rows[i]["Monthly_Value"].ToString();
                        //    obj.Monthly_W020 = dt.Rows[i]["Monthly_W020"].ToString();
                        //    obj.Monthly_A3NP = dt.Rows[i]["Monthly_A3NP"].ToString();
                        //    obj.Monthly_W401 = dt.Rows[i]["Monthly_W401"].ToString();
                        //    obj.Monthly_W402 = dt.Rows[i]["Monthly_W402"].ToString();
                        //    obj.Monthly_W403 = dt.Rows[i]["Monthly_W403"].ToString();
                        //    obj.Monthly_R005EPRT = dt.Rows[i]["Monthly_R005EPRT"].ToString();
                        //    obj.Monthly_Type = dt.Rows[i]["Monthly_Type"].ToString();
                        //    obj.Monthly_FiscalYear = dt.Rows[i]["Monthly_FiscalYear"].ToString();

                        //    obj.Graph_Section = dt.Rows[i]["Graph_Section"].ToString();
                        //    obj.Graph_Value = dt.Rows[i]["Graph_Value"].ToString();
                        //    obj.Graph_W020 = dt.Rows[i]["Graph_W020"].ToString();
                        //    obj.Graph_A3NP = dt.Rows[i]["Graph_A3NP"].ToString();
                        //    obj.Graph_C4131 = dt.Rows[i]["Graph_C4131"].ToString();
                        //    obj.Graph_W401 = dt.Rows[i]["Graph_W401"].ToString();
                        //    obj.Graph_W402 = dt.Rows[i]["Graph_W402"].ToString();
                        //    obj.Graph_W403 = dt.Rows[i]["Graph_W403"].ToString();
                        //    obj.Graph_R005EPRT = dt.Rows[i]["Graph_R005EPRT"].ToString();
                        //    obj.Graph_Type = dt.Rows[i]["Graph_Type"].ToString();
                        //    obj.Graph_Date = dt.Rows[i]["Graph_Date"].ToString();

                        //    obj.Yearly_Section = dt.Rows[i]["Yearly_Section"].ToString();
                        //    obj.Yearly_Value = dt.Rows[i]["Yearly_Value"].ToString();
                        //    obj.Yearly_Type = dt.Rows[i]["Yearly_Type"].ToString();
                        //    obj.Yearly_FiscalYear = dt.Rows[i]["Yearly_Fiscal Year"].ToString();

                        //    if (dt.Rows[i]["Graph_Section"].ToString() != "")
                        //    {
                        //        obj.UploadDate = DateTime.Now.ToString();
                        //    }

                        //    list.Add(obj);
                        //}

                        //EfficiencyDatagrid.DataSource = list;
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Please check the file if match to the selected template.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //}

                        //try
                        //{
                        //    DataTable dtExcel = new DataTable();
                        //    dtExcel = ReadExcel(filePath, fileExt);//read excel file
                        //    ProdEfficiencyDatagrid.Visible = true;
                        //    ProdEfficiencyDatagrid.DataSource = dtExcel;
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show(ex.Message.ToString());
                        //}
                    }
                    else if (TemplateDropdownList.Text == "Direct Efficiency Graph IC")
                    {
                        //try
                        //{

                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }

                        //List<DirectEff_Graph_IC> list = new List<DirectEff_Graph_IC>();

                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    DirectEff_Graph_IC obj = new DirectEff_Graph_IC();

                        //    obj.Monthly_Section = dt.Rows[i]["Monthly_Section"].ToString();
                        //    obj.Monthly_Month = dt.Rows[i]["Monthly_Month"].ToString();
                        //    obj.Monthly_Value = dt.Rows[i]["Monthly_Value"].ToString();
                        //    obj.Monthly_Support = dt.Rows[i]["Monthly_Support"].ToString();
                        //    obj.Monthly_FinalPacking = dt.Rows[i]["Monthly_Final Packing"].ToString();
                        //    obj.Monthly_IF_old = dt.Rows[i]["Monthly_IF (old)"].ToString();
                        //    obj.Monthly_FW_old = dt.Rows[i]["Monthly_FW (old)"].ToString();
                        //    obj.Monthly_HT = dt.Rows[i]["Monthly_HT"].ToString();
                        //    obj.Monthly_Others = dt.Rows[i]["Monthly_Others"].ToString();
                        //    obj.Monthly_IF_BH17 = dt.Rows[i]["Monthly_IF-BH17"].ToString();
                        //    obj.Monthly_FW_BH17 = dt.Rows[i]["Monthly_FW-BH17"].ToString();
                        //    obj.Monthly_SIM17 = dt.Rows[i]["Monthly_SIM17"].ToString();
                        //    obj.Monthly_FWSIM = dt.Rows[i]["Monthly_FWSIM"].ToString();
                        //    obj.Monthly_SIM19 = dt.Rows[i]["Monthly_SIM19"].ToString();
                        //    obj.Monthly_REG19 = dt.Rows[i]["Monthly_REG19"].ToString();
                        //    obj.Monthly_Blossom = dt.Rows[i]["Monthly_Blossom"].ToString();
                        //    obj.Monthly_CartridgePack = dt.Rows[i]["Monthly_Cartridge Pack"].ToString();
                        //    obj.Monthly_Type = dt.Rows[i]["Monthly_Type"].ToString();
                        //    obj.Monthly_FiscalYear = dt.Rows[i]["Monthly_FiscalYear"].ToString();

                        //    obj.Graph_Section = dt.Rows[i]["Graph_Section"].ToString();
                        //    obj.Graph_Value = dt.Rows[i]["Graph_Value"].ToString();
                        //    obj.Graph_Support = dt.Rows[i]["Graph_Support"].ToString();
                        //    obj.Graph_FinalPacking = dt.Rows[i]["Graph_Final Packing"].ToString();
                        //    obj.Graph_IF_old = dt.Rows[i]["Graph_IF (old)"].ToString();
                        //    obj.Graph_FW_old = dt.Rows[i]["Graph_FW (old)"].ToString();
                        //    obj.Graph_HT = dt.Rows[i]["Graph_HT"].ToString();
                        //    obj.Graph_Others = dt.Rows[i]["Graph_Others"].ToString();
                        //    obj.Graph_IF_BH17 = dt.Rows[i]["Graph_IF-BH17"].ToString();
                        //    obj.Graph_FW_BH17 = dt.Rows[i]["Graph_FW-BH17"].ToString();
                        //    obj.Graph_SIM17 = dt.Rows[i]["Graph_SIM17"].ToString();
                        //    obj.Graph_FWSIM = dt.Rows[i]["Graph_FWSIM"].ToString();
                        //    obj.Graph_SIM19 = dt.Rows[i]["Graph_SIM19"].ToString();
                        //    obj.Graph_REG19 = dt.Rows[i]["Graph_REG19"].ToString();
                        //    obj.Graph_Blossom = dt.Rows[i]["Graph_Blossom"].ToString();
                        //    obj.Graph_CartridgePack = dt.Rows[i]["Graph_Cartridge Pack"].ToString();
                        //    obj.Graph_Type = dt.Rows[i]["Graph_Type"].ToString();
                        //    obj.Graph_Date = dt.Rows[i]["Graph_Date"].ToString();

                        //    obj.Yearly_Section = dt.Rows[i]["Yearly_Section"].ToString();
                        //    obj.Yearly_Value = dt.Rows[i]["Yearly_Value"].ToString();
                        //    obj.Yearly_Type = dt.Rows[i]["Yearly_Type"].ToString();
                        //    obj.Yearly_FiscalYear = dt.Rows[i]["Yearly_Fiscal Year"].ToString();

                        //    if (dt.Rows[i]["Graph_Section"].ToString() != "")
                        //    {
                        //        obj.UploadDate = DateTime.Now.ToString();
                        //    }

                        //    list.Add(obj);
                        //}

                        //EfficiencyDatagrid.DataSource = list;
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Please check the file if match to the selected template.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //}

                        //try
                        //{
                        //    DataTable dtExcel = new DataTable();
                        //    dtExcel = ReadExcel(filePath, fileExt);//read excel file
                        //    ProdEfficiencyDatagrid.Visible = true;
                        //    ProdEfficiencyDatagrid.DataSource = dtExcel;
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show(ex.Message.ToString());
                        //}
                    }
                    else if (TemplateDropdownList.Text == "Direct Efficiency Graph PT")
                    {


                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }

                        //try
                        //{
                        //List<DirectEff_Graph_PT> list = new List<DirectEff_Graph_PT>();

                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    DirectEff_Graph_PT obj = new DirectEff_Graph_PT();

                        //    obj.Monthly_Section = dt.Rows[i]["Monthly_Section"].ToString();
                        //    obj.Monthly_Month = dt.Rows[i]["Monthly_Month"].ToString();
                        //    obj.Monthly_Value = dt.Rows[i]["Monthly_Value"].ToString();
                        //    obj.Monthly_C4150 = dt.Rows[i]["Monthly_C4150"].ToString();
                        //    obj.Monthly_PTClerk = dt.Rows[i]["Monthly_PTClerk"].ToString();
                        //    obj.Monthly_W020 = dt.Rows[i]["Monthly_W020"].ToString();
                        //    obj.Monthly_W022 = dt.Rows[i]["Monthly_W022"].ToString();
                        //    obj.Monthly_W023 = dt.Rows[i]["Monthly_W023"].ToString();
                        //    obj.Monthly_W024 = dt.Rows[i]["Monthly_W024"].ToString();
                        //    obj.Monthly_W025 = dt.Rows[i]["Monthly_W025"].ToString();
                        //    obj.Monthly_W050 = dt.Rows[i]["Monthly_W050"].ToString();
                        //    obj.Monthly_KitProd = dt.Rows[i]["Monthly_KitProd"].ToString();
                        //    obj.Monthly_R006PT = dt.Rows[i]["Monthly_R006PT"].ToString();
                        //    obj.Monthly_PTEngg = dt.Rows[i]["Monthly_PTEngg"].ToString();
                        //    obj.Monthly_PTSub = dt.Rows[i]["Monthly_PTSub"].ToString();
                        //    obj.Monthly_Offline = dt.Rows[i]["Monthly_Offline"].ToString();
                        //    obj.Monthly_ProdSemi = dt.Rows[i]["Monthly_ProdSemi"].ToString();
                        //    obj.Monthly_W026 = dt.Rows[i]["Monthly_W026"].ToString();
                        //    obj.Monthly_W027 = dt.Rows[i]["Monthly_W027"].ToString();
                        //    obj.Monthly_W052 = dt.Rows[i]["Monthly_W052"].ToString();
                        //    obj.Monthly_PTN20NP = dt.Rows[i]["Monthly_PTN20NP"].ToString();
                        //    obj.Monthly_PTN25NP = dt.Rows[i]["Monthly_PTN25NP"].ToString();
                        //    obj.Monthly_PTN10NP = dt.Rows[i]["Monthly_PTN10NP"].ToString();
                        //    obj.Monthly_W021 = dt.Rows[i]["Monthly_W021"].ToString();
                        //    obj.Monthly_W028 = dt.Rows[i]["Monthly_W028"].ToString();
                        //    obj.Monthly_Type = dt.Rows[i]["Monthly_Type"].ToString();
                        //    obj.Monthly_FiscalYear = dt.Rows[i]["Monthly_FiscalYear"].ToString();

                        //    obj.Graph_Section = dt.Rows[i]["Graph_Section"].ToString();
                        //    obj.Graph_Value = dt.Rows[i]["Graph_Value"].ToString();
                        //    obj.Graph_C4150 = dt.Rows[i]["Graph_C4150"].ToString();
                        //    obj.Graph_PTClerk = dt.Rows[i]["Graph_PTClerk"].ToString();
                        //    obj.Graph_W020 = dt.Rows[i]["Graph_W020"].ToString();
                        //    obj.Graph_W022 = dt.Rows[i]["Graph_W022"].ToString();
                        //    obj.Graph_W023 = dt.Rows[i]["Graph_W023"].ToString();
                        //    obj.Graph_W024 = dt.Rows[i]["Graph_W024"].ToString();
                        //    obj.Graph_W025 = dt.Rows[i]["Graph_W025"].ToString();
                        //    obj.Graph_W050 = dt.Rows[i]["Graph_W050"].ToString();
                        //    obj.Graph_KitProd = dt.Rows[i]["Graph_KitProd"].ToString();
                        //    obj.Graph_R006PT = dt.Rows[i]["Graph_R006PT"].ToString();
                        //    obj.Graph_PTEngg = dt.Rows[i]["Graph_PTEngg"].ToString();
                        //    obj.Graph_PTSub = dt.Rows[i]["Graph_PTSub"].ToString();
                        //    obj.Graph_Offline = dt.Rows[i]["Graph_Offline"].ToString();
                        //    obj.Graph_ProdSemi = dt.Rows[i]["Graph_ProdSemi"].ToString();
                        //    obj.Graph_W026 = dt.Rows[i]["Graph_W026"].ToString();
                        //    obj.Graph_W027 = dt.Rows[i]["Graph_W027"].ToString();
                        //    obj.Graph_W052 = dt.Rows[i]["Graph_W052"].ToString();
                        //    obj.Graph_PTN20NP = dt.Rows[i]["Graph_PTN20NP"].ToString();
                        //    obj.Graph_PTN25NP = dt.Rows[i]["Graph_PTN25NP"].ToString();
                        //    obj.Graph_PTN10NP = dt.Rows[i]["Graph_PTN10NP"].ToString();
                        //    obj.Graph_W021 = dt.Rows[i]["Graph_W021"].ToString();
                        //    obj.Graph_W028 = dt.Rows[i]["Graph_W028"].ToString();
                        //    obj.Graph_Type = dt.Rows[i]["Graph_Type"].ToString();
                        //    obj.Graph_Date = dt.Rows[i]["Graph_Date"].ToString();

                        //    obj.Yearly_Section = dt.Rows[i]["Yearly_Section"].ToString();
                        //    obj.Yearly_Value = dt.Rows[i]["Yearly_Value"].ToString();
                        //    obj.Yearly_Type = dt.Rows[i]["Yearly_Type"].ToString();
                        //    obj.Yearly_FiscalYear = dt.Rows[i]["Yearly_Fiscal Year"].ToString();

                        //    if (dt.Rows[i]["Graph_Section"].ToString() != "")
                        //    {
                        //        obj.UploadDate = DateTime.Now.ToString();
                        //    }

                        //    list.Add(obj);
                        //}

                        //EfficiencyDatagrid.DataSource = list;
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Please check the file if match to the selected template.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //}

                        //try
                        //{
                        //    DataTable dtExcel = new DataTable();
                        //    dtExcel = ReadExcel(filePath, fileExt);//read excel file
                        //    ProdEfficiencyDatagrid.Visible = true;
                        //    ProdEfficiencyDatagrid.DataSource = dtExcel;
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show(ex.Message.ToString());
                        //}
                    }
                    else if (TemplateDropdownList.Text == "Direct Efficiency Graph IH")
                    {


                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }

                        //try
                        //{
                        //List<DirectEff_Graph_IH> list = new List<DirectEff_Graph_IH>();

                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    DirectEff_Graph_IH obj = new DirectEff_Graph_IH();

                        //    obj.Monthly_Section = dt.Rows[i]["Monthly_Section"].ToString();
                        //    obj.Monthly_Month = dt.Rows[i]["Monthly_Month"].ToString();
                        //    obj.Monthly_Value = dt.Rows[i]["Monthly_Value"].ToString();
                        //    obj.Monthly_C4140 = dt.Rows[i]["Monthly_C4140"].ToString();
                        //    obj.Monthly_SP13Low = dt.Rows[i]["Monthly_SP13Low"].ToString();
                        //    obj.Monthly_SP15Step = dt.Rows[i]["Monthly_SP15Step"].ToString();
                        //    obj.Monthly_SP15Low = dt.Rows[i]["Monthly_SP15Low"].ToString();
                        //    obj.Monthly_H004HT = dt.Rows[i]["Monthly_H004HT"].ToString();
                        //    obj.Monthly_H004X = dt.Rows[i]["Monthly_H004X"].ToString();
                        //    obj.Monthly_WDC001 = dt.Rows[i]["Monthly_WDC001"].ToString();
                        //    obj.Monthly_WDC002 = dt.Rows[i]["Monthly_WDC002"].ToString();
                        //    obj.Monthly_WBE001 = dt.Rows[i]["Monthly_WBE001"].ToString();
                        //    obj.Monthly_WBE002 = dt.Rows[i]["Monthly_WBE002"].ToString();
                        //    obj.Monthly_WBE003 = dt.Rows[i]["Monthly_WBE003"].ToString();
                        //    obj.Monthly_WBE004 = dt.Rows[i]["Monthly_WBE004"].ToString();
                        //    obj.Monthly_WBE005 = dt.Rows[i]["Monthly_WBE005"].ToString();
                        //    obj.Monthly_WBE006 = dt.Rows[i]["Monthly_WBE006"].ToString();
                        //    obj.Monthly_WSUBH01 = dt.Rows[i]["Monthly_WSUBH01"].ToString();
                        //    obj.Monthly_WSUBH02 = dt.Rows[i]["Monthly_WSUBH02"].ToString();
                        //    obj.Monthly_WSUBH03 = dt.Rows[i]["Monthly_WSUBH03"].ToString();
                        //    obj.Monthly_WSUBH04 = dt.Rows[i]["Monthly_WSUBH04"].ToString();
                        //    obj.Monthly_WSUBH05 = dt.Rows[i]["Monthly_WSUBH05"].ToString();
                        //    obj.Monthly_WSUBD01 = dt.Rows[i]["Monthly_WSUBD01"].ToString();
                        //    obj.Monthly_WSUBD02 = dt.Rows[i]["Monthly_WSUBD02"].ToString();
                        //    obj.Monthly_WSUBD03 = dt.Rows[i]["Monthly_WSUBD03"].ToString();
                        //    obj.Monthly_WSUBD04 = dt.Rows[i]["Monthly_WSUBD04"].ToString();
                        //    obj.Monthly_WBE101 = dt.Rows[i]["Monthly_WBE101"].ToString();
                        //    obj.Monthly_WBE102 = dt.Rows[i]["Monthly_WBE102"].ToString();
                        //    obj.Monthly_WBE103 = dt.Rows[i]["Monthly_WBE103"].ToString();
                        //    obj.Monthly_WBE104 = dt.Rows[i]["Monthly_WBE104"].ToString();
                        //    obj.Monthly_WBE105 = dt.Rows[i]["Monthly_WBE105"].ToString();
                        //    obj.Monthly_WBE106 = dt.Rows[i]["Monthly_WBE106"].ToString();
                        //    obj.Monthly_WSUB101 = dt.Rows[i]["Monthly_WSUB101"].ToString();
                        //    obj.Monthly_WSUB102 = dt.Rows[i]["Monthly_WSUB102"].ToString();
                        //    obj.Monthly_Ihexprod = dt.Rows[i]["Monthly_Ihexprod"].ToString();
                        //    obj.Monthly_Mini19NP = dt.Rows[i]["Monthly_Mini19NP"].ToString();
                        //    obj.Monthly_M19 = dt.Rows[i]["Monthly_M19"].ToString();
                        //    obj.Monthly_WNP001 = dt.Rows[i]["Monthly_WNP001"].ToString();
                        //    obj.Monthly_C4140A = dt.Rows[i]["Monthly_C4140A"].ToString();
                        //    obj.Monthly_WDC003 = dt.Rows[i]["Monthly_WDC003"].ToString();
                        //    obj.Monthly_WNI001 = dt.Rows[i]["Monthly_WNI001"].ToString();
                        //    obj.Monthly_Type = dt.Rows[i]["Monthly_Type"].ToString();
                        //    obj.Monthly_FiscalYear = dt.Rows[i]["Monthly_FiscalYear"].ToString();

                        //    obj.Graph_Section = dt.Rows[i]["Graph_Section"].ToString();
                        //    obj.Graph_Value = dt.Rows[i]["Graph_Value"].ToString();
                        //    obj.Graph_C4140 = dt.Rows[i]["Graph_C4140"].ToString();
                        //    obj.Graph_SP13Low = dt.Rows[i]["Graph_SP13Low"].ToString();
                        //    obj.Graph_SP15Step = dt.Rows[i]["Graph_SP15Step"].ToString();
                        //    obj.Graph_SP15Low = dt.Rows[i]["Graph_SP15Low"].ToString();
                        //    obj.Graph_H004HT = dt.Rows[i]["Graph_H004HT"].ToString();
                        //    obj.Graph_H004X = dt.Rows[i]["Graph_H004X"].ToString();
                        //    obj.Graph_WDC001 = dt.Rows[i]["Graph_WDC001"].ToString();
                        //    obj.Graph_WDC002 = dt.Rows[i]["Graph_WDC002"].ToString();
                        //    obj.Graph_WBE001 = dt.Rows[i]["Graph_WBE001"].ToString();
                        //    obj.Graph_WBE002 = dt.Rows[i]["Graph_WBE002"].ToString();
                        //    obj.Graph_WBE003 = dt.Rows[i]["Graph_WBE003"].ToString();
                        //    obj.Graph_WBE004 = dt.Rows[i]["Graph_WBE004"].ToString();
                        //    obj.Graph_WBE005 = dt.Rows[i]["Graph_WBE005"].ToString();
                        //    obj.Graph_WBE006 = dt.Rows[i]["Graph_WBE006"].ToString();
                        //    obj.Graph_WSUBH01 = dt.Rows[i]["Graph_WSUBH01"].ToString();
                        //    obj.Graph_WSUBH02 = dt.Rows[i]["Graph_WSUBH02"].ToString();
                        //    obj.Graph_WSUBH03 = dt.Rows[i]["Graph_WSUBH03"].ToString();
                        //    obj.Graph_WSUBH04 = dt.Rows[i]["Graph_WSUBH04"].ToString();
                        //    obj.Graph_WSUBH05 = dt.Rows[i]["Graph_WSUBH05"].ToString();
                        //    obj.Graph_WSUBD01 = dt.Rows[i]["Graph_WSUBD01"].ToString();
                        //    obj.Graph_WSUBD02 = dt.Rows[i]["Graph_WSUBD02"].ToString();
                        //    obj.Graph_WSUBD03 = dt.Rows[i]["Graph_WSUBD03"].ToString();
                        //    obj.Graph_WSUBD04 = dt.Rows[i]["Graph_WSUBD04"].ToString();
                        //    obj.Graph_WBE101 = dt.Rows[i]["Graph_WBE101"].ToString();
                        //    obj.Graph_WBE102 = dt.Rows[i]["Graph_WBE102"].ToString();
                        //    obj.Graph_WBE103 = dt.Rows[i]["Graph_WBE103"].ToString();
                        //    obj.Graph_WBE104 = dt.Rows[i]["Graph_WBE104"].ToString();
                        //    obj.Graph_WBE105 = dt.Rows[i]["Graph_WBE105"].ToString();
                        //    obj.Graph_WBE106 = dt.Rows[i]["Graph_WBE106"].ToString();
                        //    obj.Graph_WSUB101 = dt.Rows[i]["Graph_WSUB101"].ToString();
                        //    obj.Graph_WSUB102 = dt.Rows[i]["Graph_WSUB102"].ToString();
                        //    obj.Graph_Ihexprod = dt.Rows[i]["Graph_Ihexprod"].ToString();
                        //    obj.Graph_Mini19NP = dt.Rows[i]["Graph_Mini19NP"].ToString();
                        //    obj.Graph_M19 = dt.Rows[i]["Graph_M19"].ToString();
                        //    obj.Graph_WNP001 = dt.Rows[i]["Graph_WNP001"].ToString();
                        //    obj.Graph_C4140A = dt.Rows[i]["Graph_C4140A"].ToString();
                        //    obj.Graph_WDC003 = dt.Rows[i]["Graph_WDC003"].ToString();
                        //    obj.Graph_WNI001 = dt.Rows[i]["Graph_WNI001"].ToString();
                        //    obj.Graph_Type = dt.Rows[i]["Graph_Type"].ToString();
                        //    obj.Graph_Date = dt.Rows[i]["Graph_Date"].ToString();

                        //    obj.Yearly_Section = dt.Rows[i]["Yearly_Section"].ToString();
                        //    obj.Yearly_Value = dt.Rows[i]["Yearly_Value"].ToString();
                        //    obj.Yearly_Type = dt.Rows[i]["Yearly_Type"].ToString();
                        //    obj.Yearly_FiscalYear = dt.Rows[i]["Yearly_Fiscal Year"].ToString();

                        //    if (dt.Rows[i]["Graph_Section"].ToString() != "")
                        //    {
                        //        obj.UploadDate = DateTime.Now.ToString();
                        //    }

                        //    list.Add(obj);
                        //}

                        //EfficiencyDatagrid.DataSource = list;
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Please check the file if match to the selected template.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //}

                        //try
                        //{
                        //    DataTable dtExcel = new DataTable();
                        //    dtExcel = ReadExcel(filePath, fileExt);//read excel file
                        //    ProdEfficiencyDatagrid.Visible = true;
                        //    ProdEfficiencyDatagrid.DataSource = dtExcel;
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show(ex.Message.ToString());
                        //}
                    }
                    else if (TemplateDropdownList.Text == "Direct Efficiency Graph TC")
                    {
                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }

                        //try
                        //{
                        //List<DirectEff_Graph_TC> list = new List<DirectEff_Graph_TC>();

                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    DirectEff_Graph_TC obj = new DirectEff_Graph_TC();

                        //    obj.Monthly_Section = dt.Rows[i]["Monthly_Section"].ToString();
                        //    obj.Monthly_Month = dt.Rows[i]["Monthly_Month"].ToString();
                        //    obj.Monthly_Value = dt.Rows[i]["Monthly_Value"].ToString();
                        //    obj.Monthly_C4120 = dt.Rows[i]["Monthly_C4120"].ToString();
                        //    obj.Monthly_TCClerk = dt.Rows[i]["Monthly_TCClerk"].ToString();
                        //    obj.Monthly_W21X = dt.Rows[i]["Monthly_W21X"].ToString();
                        //    obj.Monthly_W21XL2 = dt.Rows[i]["Monthly_W21XL2"].ToString();
                        //    obj.Monthly_W21Y = dt.Rows[i]["Monthly_W21Y"].ToString();
                        //    obj.Monthly_W21Z = dt.Rows[i]["Monthly_W21Z"].ToString();
                        //    obj.Monthly_W202 = dt.Rows[i]["Monthly_W202"].ToString();
                        //    obj.Monthly_W202L2 = dt.Rows[i]["Monthly_W202L2"].ToString();
                        //    obj.Monthly_W202L3 = dt.Rows[i]["Monthly_W202L3"].ToString();
                        //    obj.Monthly_W201 = dt.Rows[i]["Monthly_W201"].ToString();
                        //    obj.Monthly_W201L2 = dt.Rows[i]["Monthly_W201L2"].ToString();
                        //    obj.Monthly_W203 = dt.Rows[i]["Monthly_W203"].ToString();
                        //    obj.Monthly_W210 = dt.Rows[i]["Monthly_W210"].ToString();
                        //    obj.Monthly_W213 = dt.Rows[i]["Monthly_W213"].ToString();
                        //    obj.Monthly_W216 = dt.Rows[i]["Monthly_W216"].ToString();
                        //    obj.Monthly_W217 = dt.Rows[i]["Monthly_W217"].ToString();
                        //    obj.Monthly_W21A = dt.Rows[i]["Monthly_W21A"].ToString();
                        //    obj.Monthly_W21B = dt.Rows[i]["Monthly_W21B"].ToString();
                        //    obj.Monthly_W21C = dt.Rows[i]["Monthly_W21C"].ToString();
                        //    obj.Monthly_W21D = dt.Rows[i]["Monthly_W21D"].ToString();
                        //    obj.Monthly_W21E = dt.Rows[i]["Monthly_W21E"].ToString();
                        //    obj.Monthly_W300 = dt.Rows[i]["Monthly_W300"].ToString();
                        //    obj.Monthly_W301 = dt.Rows[i]["Monthly_W301"].ToString();
                        //    obj.Monthly_W302 = dt.Rows[i]["Monthly_W302"].ToString();
                        //    obj.Monthly_W303 = dt.Rows[i]["Monthly_W303"].ToString();
                        //    obj.Monthly_W270 = dt.Rows[i]["Monthly_W270"].ToString();
                        //    obj.Monthly_PET = dt.Rows[i]["Monthly_PET"].ToString();
                        //    obj.Monthly_ADH = dt.Rows[i]["Monthly_ADH"].ToString();
                        //    obj.Monthly_INK = dt.Rows[i]["Monthly_INK"].ToString();
                        //    obj.Monthly_SLIT = dt.Rows[i]["Monthly_SLIT"].ToString();
                        //    obj.Monthly_TCLabel = dt.Rows[i]["Monthly_TCLabel"].ToString();
                        //    obj.Monthly_MLabel = dt.Rows[i]["Monthly_MLabel"].ToString();
                        //    obj.Monthly_R003TC = dt.Rows[i]["Monthly_R003TC"].ToString();
                        //    obj.Monthly_W21F = dt.Rows[i]["Monthly_W21F"].ToString();
                        //    obj.Monthly_TCAssy = dt.Rows[i]["Monthly_TCAssy"].ToString();
                        //    obj.Monthly_TCPack = dt.Rows[i]["Monthly_TCPack"].ToString();
                        //    obj.Monthly_TCWind = dt.Rows[i]["Monthly_TCWind"].ToString();
                        //    obj.Monthly_TCQG = dt.Rows[i]["Monthly_TCQG"].ToString();
                        //    obj.Monthly_W200 = dt.Rows[i]["Monthly_W200"].ToString();
                        //    obj.Monthly_W21G = dt.Rows[i]["Monthly_W21G"].ToString();
                        //    obj.Monthly_AUSLIT = dt.Rows[i]["Monthly_AUSLIT"].ToString();
                        //    obj.Monthly_Type = dt.Rows[i]["Monthly_Type"].ToString();
                        //    obj.Monthly_FiscalYear = dt.Rows[i]["Monthly_FiscalYear"].ToString();

                        //    obj.Graph_Section = dt.Rows[i]["Graph_Section"].ToString();
                        //    obj.Graph_Value = dt.Rows[i]["Graph_Value"].ToString();
                        //    obj.Graph_C4120 = dt.Rows[i]["Graph_C4120"].ToString();
                        //    obj.Graph_TCClerk = dt.Rows[i]["Graph_TCClerk"].ToString();
                        //    obj.Graph_W21X = dt.Rows[i]["Graph_W21X"].ToString();
                        //    obj.Graph_W21XL2 = dt.Rows[i]["Graph_W21XL2"].ToString();
                        //    obj.Graph_W21Y = dt.Rows[i]["Graph_W21Y"].ToString();
                        //    obj.Graph_W21Z = dt.Rows[i]["Graph_W21Z"].ToString();
                        //    obj.Graph_W202 = dt.Rows[i]["Graph_W202"].ToString();
                        //    obj.Graph_W202L2 = dt.Rows[i]["Graph_W202L2"].ToString();
                        //    obj.Graph_W202L3 = dt.Rows[i]["Graph_W202L3"].ToString();
                        //    obj.Graph_W201 = dt.Rows[i]["Graph_W201"].ToString();
                        //    obj.Graph_W201L2 = dt.Rows[i]["Graph_W201L2"].ToString();
                        //    obj.Graph_W203 = dt.Rows[i]["Graph_W203"].ToString();
                        //    obj.Graph_W210 = dt.Rows[i]["Graph_W210"].ToString();
                        //    obj.Graph_W213 = dt.Rows[i]["Graph_W213"].ToString();
                        //    obj.Graph_W216 = dt.Rows[i]["Graph_W216"].ToString();
                        //    obj.Graph_W217 = dt.Rows[i]["Graph_W217"].ToString();
                        //    obj.Graph_W21A = dt.Rows[i]["Graph_W21A"].ToString();
                        //    obj.Graph_W21B = dt.Rows[i]["Graph_W21B"].ToString();
                        //    obj.Graph_W21C = dt.Rows[i]["Graph_W21C"].ToString();
                        //    obj.Graph_W21D = dt.Rows[i]["Graph_W21D"].ToString();
                        //    obj.Graph_W21E = dt.Rows[i]["Graph_W21E"].ToString();
                        //    obj.Graph_W300 = dt.Rows[i]["Graph_W300"].ToString();
                        //    obj.Graph_W301 = dt.Rows[i]["Graph_W301"].ToString();
                        //    obj.Graph_W302 = dt.Rows[i]["Graph_W302"].ToString();
                        //    obj.Graph_W303 = dt.Rows[i]["Graph_W303"].ToString();
                        //    obj.Graph_W270 = dt.Rows[i]["Graph_W270"].ToString();
                        //    obj.Graph_PET = dt.Rows[i]["Graph_PET"].ToString();
                        //    obj.Graph_ADH = dt.Rows[i]["Graph_ADH"].ToString();
                        //    obj.Graph_INK = dt.Rows[i]["Graph_INK"].ToString();
                        //    obj.Graph_SLIT = dt.Rows[i]["Graph_SLIT"].ToString();
                        //    obj.Graph_TCLabel = dt.Rows[i]["Graph_TCLabel"].ToString();
                        //    obj.Graph_MLabel = dt.Rows[i]["Graph_MLabel"].ToString();
                        //    obj.Graph_R003TC = dt.Rows[i]["Graph_R003TC"].ToString();
                        //    obj.Graph_W21F = dt.Rows[i]["Graph_W21F"].ToString();
                        //    obj.Graph_TCAssy = dt.Rows[i]["Graph_TCAssy"].ToString();
                        //    obj.Graph_TCPack = dt.Rows[i]["Graph_TCPack"].ToString();
                        //    obj.Graph_TCWind = dt.Rows[i]["Graph_TCWind"].ToString();
                        //    obj.Graph_TCQG = dt.Rows[i]["Graph_TCQG"].ToString();
                        //    obj.Graph_W200 = dt.Rows[i]["Graph_W200"].ToString();
                        //    obj.Graph_W21G = dt.Rows[i]["Graph_W21G"].ToString();
                        //    obj.Graph_AUSLIT = dt.Rows[i]["Graph_Value"].ToString();

                        //    obj.Graph_Type = dt.Rows[i]["Graph_Type"].ToString();
                        //    obj.Graph_Date = dt.Rows[i]["Graph_Date"].ToString();

                        //    obj.Yearly_Section = dt.Rows[i]["Yearly_Section"].ToString();
                        //    obj.Yearly_Value = dt.Rows[i]["Yearly_Value"].ToString();
                        //    obj.Yearly_Type = dt.Rows[i]["Yearly_Type"].ToString();
                        //    obj.Yearly_FiscalYear = dt.Rows[i]["Yearly_Fiscal Year"].ToString();

                        //    if (dt.Rows[i]["Graph_Section"].ToString() != "")
                        //    {
                        //        obj.UploadDate = DateTime.Now.ToString();
                        //    }

                        //    list.Add(obj);
                        //}

                        //EfficiencyDatagrid.DataSource = list;
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Please check the file if match to the selected template.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //}

                        //try
                        //{
                        //    DataTable dtExcel = new DataTable();
                        //    dtExcel = ReadExcel(filePath, fileExt);//read excel file
                        //    ProdEfficiencyDatagrid.Visible = true;
                        //    ProdEfficiencyDatagrid.DataSource = dtExcel;
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show(ex.Message.ToString());
                        //}
                    }
                    else if (TemplateDropdownList.Text == "Direct Efficiency Graph TN")
                    {
                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    else if (TemplateDropdownList.Text == "Semi-Direct Rate Graph")
                    {
                        //try
                        //{
                            List<SemiDirect_Graph_Class> list = new List<SemiDirect_Graph_Class>();

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                SemiDirect_Graph_Class obj = new SemiDirect_Graph_Class();

                                //Daily
                                obj.Section = dt.Rows[i]["Daily_Section"].ToString();
                                obj.Daily_Date = dt.Rows[i]["Daily_Date"].ToString();
                                obj.Supporter_RepairManHourContributor = dt.Rows[i]["Supporter/Repair man-hour"].ToString();
                                obj.TransportManHourContributor = dt.Rows[i]["Transport man-hour"].ToString();
                                obj.SubLeaderManHourContributor = dt.Rows[i]["(Sub)Leader man-hour"].ToString();
                                obj.Record_OthersManHourContributor = dt.Rows[i]["Record/others man-hour"].ToString();
                                obj.Maintenance_EmployeeManHourContributor = dt.Rows[i]["Maintenance employee man-hour"].ToString();
                                obj.Clerk_EngineerManHourContributor = dt.Rows[i]["Clerk/Engineer man-hour"].ToString();
                                obj.StaffManHourContributor = dt.Rows[i]["Staff man-hour"].ToString();
                                obj.ManagerManHourContributor = dt.Rows[i]["Maneger man-hour"].ToString();
                                obj.OvertimeIncreaseAndDecreaseManHourContributor = dt.Rows[i]["Overtime increase and decrease man-hour"].ToString();
                                obj.Daily_FiscalYear = dt.Rows[i]["Fiscal Year"].ToString();

                                //GRaph
                                obj.Graph_Section = dt.Rows[i]["Graph_Section"].ToString();
                                obj.Graph_Value = dt.Rows[i]["Graph_Value"].ToString();
                                obj.Graph_Supporter_RepairManHourContributor = dt.Rows[i]["Graph_Supporter/Repair man-hour"].ToString();
                                obj.Graph_TransportManHourContributor = dt.Rows[i]["Graph_Transport man-hour"].ToString();
                                obj.Graph_SubLeaderManHourContributor = dt.Rows[i]["Graph_(Sub)Leader man-hour"].ToString();
                                obj.Graph_Record_OthersManHourContributor = dt.Rows[i]["Graph_Record/others man-hour"].ToString();
                                obj.Graph_Maintenance_EmployeeManHourContributor = dt.Rows[i]["Graph_Maintenance employee man-hour"].ToString();
                                obj.Graph_Clerk_EngineerManHourContributor = dt.Rows[i]["Graph_Clerk/Engineer man-hour"].ToString();
                                obj.Graph_StaffManHourContributor = dt.Rows[i]["Graph_Staff man-hour"].ToString();
                                obj.Graph_ManagerManHourContributor = dt.Rows[i]["Graph_Maneger man-hour"].ToString();
                                obj.Graph_OvertimeIncreaseAndDecreaseManHourContributor = dt.Rows[i]["Graph_Overtime increase and decrease man-hour"].ToString();
                                obj.Graph_Type = dt.Rows[i]["Graph_Type"].ToString();
                                obj.Graph_Date = dt.Rows[i]["Graph_Date"].ToString();

                                //Month
                                obj.Monthly_Section = dt.Rows[i]["Monthly_Section"].ToString();
                                obj.Monthly_Value = dt.Rows[i]["Monthly_Value"].ToString();
                                obj.Monthly_Supporter_RepairManHourContributor = dt.Rows[i]["Monthly_Supporter/Repair man-hour"].ToString();
                                obj.Monthly_TransportManHourContributor = dt.Rows[i]["Monthly_Transport man-hour"].ToString();
                                obj.Monthly_SubLeaderManHourContributor = dt.Rows[i]["Monthly_(Sub)Leader man-hour"].ToString();
                                obj.Monthly_Record_OthersManHourContributor = dt.Rows[i]["Monthly_Record/others man-hour"].ToString();
                                obj.Monthly_Maintenance_EmployeeManHourContributor = dt.Rows[i]["Monthly_Maintenance employee man-hour"].ToString();
                                obj.Monthly_Clerk_EngineerManHourContributor = dt.Rows[i]["Monthly_Clerk/Engineer man-hour"].ToString();
                                obj.Monthly_StaffManHourContributor = dt.Rows[i]["Monthly_Staff man-hour"].ToString();
                                obj.Monthly_ManagerManHourContributor = dt.Rows[i]["Monthly_Maneger man-hour"].ToString();
                                obj.Monthly_OvertimeIncreaseAndDecreaseManHourContributor = dt.Rows[i]["Graph_Overtime increase and decrease man-hour"].ToString();
                                obj.Monthly_Type = dt.Rows[i]["Monthly_Type"].ToString();
                                obj.Monthly_Month = dt.Rows[i]["Monthly_Month"].ToString();
                                obj.Monthly_FiscalYear = dt.Rows[i]["Monthly_FiscalYear"].ToString();

                                obj.Yearly_Section = dt.Rows[i]["Yearly_Section"].ToString();
                                obj.Yearly_Value = dt.Rows[i]["Yearly_Value"].ToString();
                                obj.Yearly_Type = dt.Rows[i]["Yearly_Type"].ToString();
                                obj.Yearly_FiscalYear = dt.Rows[i]["Yearly_Fiscal Year"].ToString();

                                if (dt.Rows[i]["Daily_Section"].ToString() != "" || dt.Rows[i]["Yearly_Section"].ToString() != "" || dt.Rows[i]["Graph_Section"].ToString() != "" || dt.Rows[i]["Monthly_Section"].ToString() != "")
                                {
                                    obj.UploadDate = DateTime.Now.ToString();
                                }

                                list.Add(obj);
                            }

                            EfficiencyDatagrid.DataSource = list;
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Please check the file if match to the selected template.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //}

                    }
                    else if (TemplateDropdownList.Text == "Total Loss Rate Graph")
                    {
                        //try
                        //{
                        List<LossRate_Graph_Class> list = new List<LossRate_Graph_Class>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            LossRate_Graph_Class obj = new LossRate_Graph_Class();

                            //Daily
                            obj.Section = dt.Rows[i]["Daily_Section"].ToString();
                            obj.Daily_Date = dt.Rows[i]["Daily_Date"].ToString();
                            obj.PreparationRearrangementManhour = dt.Rows[i]["①preparation/rearrangement man-hour"].ToString();
                            obj.InventoryManhour = dt.Rows[i]["②Inventory man-hour"].ToString();
                            obj.ProductIssue = dt.Rows[i]["③Product issue"].ToString();
                            obj.OperatorTraining = dt.Rows[i]["④Operator Training"].ToString();
                            obj.TraineeManhour = dt.Rows[i]["Trainee man-hour"].ToString();
                            obj.WaitManhour_ModelDifference = dt.Rows[i]["Wait man-hour(model difference)"].ToString();
                            obj.WaitManhour_PregnantWoman = dt.Rows[i]["Wait man-hour(Pregnant woman)"].ToString();
                            obj.WaitManhour_Resignation = dt.Rows[i]["Wait man-hour(Resignation)"].ToString();
                            obj.NewModelPreparationManhour = dt.Rows[i]["New model preparation man-hour(direct)"].ToString();
                            obj.NoWorkManhour = dt.Rows[i]["⑤No work man-hour"].ToString();
                            obj.SupplyIssue = dt.Rows[i]["⑥Supply issue"].ToString();
                            obj.Sales_ManagementIssue = dt.Rows[i]["⑦Sales/management issue"].ToString();
                            obj.DesignIssue = dt.Rows[i]["⑧Design issue"].ToString();
                            obj.OtherCompaniesSupport = dt.Rows[i]["⑨Other companies suport"].ToString();
                            obj.Daily_FiscalYear = dt.Rows[i]["Fiscal Year"].ToString();

                            //Graph
                            obj.Graph_Section = dt.Rows[i]["Graph_Section"].ToString();
                            obj.Graph_Value = dt.Rows[i]["Graph_Value"].ToString();
                            obj.Graph_PreparationRearrangementManour = dt.Rows[i]["Graph_①preparation/rearrangement man-hour"].ToString();
                            obj.Graph_InventoryManHour = dt.Rows[i]["Graph_②Inventory man-hour"].ToString();
                            obj.Graph_ProductIssue = dt.Rows[i]["Graph_③Product issue"].ToString();
                            obj.Graph_OperatorTraining = dt.Rows[i]["Graph_④Operator Training"].ToString();
                            obj.Graph_TraineeManHour = dt.Rows[i]["Graph_Trainee man-hour"].ToString();
                            obj.Graph_WaitManHour_ModelDifference = dt.Rows[i]["Graph_Wait man-hour(model difference)"].ToString();
                            obj.Graph_WaitManHour_PregnantWoman = dt.Rows[i]["Graph_Wait man-hour(Pregnant woman)"].ToString();
                            obj.Graph_WaitManHour_Resignation = dt.Rows[i]["Graph_Wait man-hour(Resignation)"].ToString();
                            obj.Graph_NewModelPreparationManHourDirect = dt.Rows[i]["Graph_New model preparation man-hour(direct)"].ToString();
                            obj.Graph_NoWorkManHour = dt.Rows[i]["Graph_⑤No work man-hour"].ToString();
                            obj.Graph_SupplyIssue = dt.Rows[i]["Graph_⑥Supply issue"].ToString();
                            obj.Graph_SalesManagementIssue = dt.Rows[i]["Graph_⑦Sales/management issue"].ToString();
                            obj.Graph_DesignIssue = dt.Rows[i]["Graph_⑧Design issue"].ToString();
                            obj.Graph_OtherCompaniesSuport = dt.Rows[i]["Graph_⑨Other companies suport"].ToString();
                            obj.Graph_Type = dt.Rows[i]["Graph_Type"].ToString();
                            obj.Graph_Date = dt.Rows[i]["Graph_Date"].ToString();
                            obj.UploadDate = DateTime.Now.ToString();

                            //Monthly
                            obj.Monthly_Section = dt.Rows[i]["Monthly_Section"].ToString();
                            obj.Monthly_Value = dt.Rows[i]["Monthly_Value"].ToString();
                            obj.Monthly_PreparationRearrangementManour = dt.Rows[i]["Monthly_①preparation/rearrangement man-hour"].ToString();
                            obj.Monthly_InventoryManHour = dt.Rows[i]["Monthly_②Inventory man-hour"].ToString();
                            obj.Monthly_ProductIssue = dt.Rows[i]["Monthly_③Product issue"].ToString();
                            obj.Monthly_OperatorTraining = dt.Rows[i]["Monthly_④Operator Training"].ToString();
                            obj.Monthly_TraineeManHour = dt.Rows[i]["Monthly_Trainee man-hour"].ToString();
                            obj.Monthly_WaitManHour_ModelDifference = dt.Rows[i]["Monthly_Wait man-hour(model difference)"].ToString();
                            obj.Monthly_WaitManHour_PregnantWoman = dt.Rows[i]["Monthly_Wait man-hour(Pregnant woman)"].ToString();
                            obj.Monthly_WaitManHour_Resignation = dt.Rows[i]["Monthly_Wait man-hour(Resignation)"].ToString();
                            obj.Monthly_NewModelPreparationManHourDirect = dt.Rows[i]["Monthly_New model preparation man-hour(direct)"].ToString();
                            obj.Monthly_NoWorkManHour = dt.Rows[i]["Monthly_⑤No work man-hour"].ToString();
                            obj.Monthly_SupplyIssue = dt.Rows[i]["Monthly_⑥Supply issue"].ToString();
                            obj.Monthly_SalesManagementIssue = dt.Rows[i]["Monthly_⑦Sales/management issue"].ToString();
                            obj.Monthly_DesignIssue = dt.Rows[i]["Monthly_⑧Design issue"].ToString();
                            obj.Monthly_OtherCompaniesSuport = dt.Rows[i]["Monthly_⑨Other companies suport"].ToString();
                            obj.Monthly_Type = dt.Rows[i]["Monthly_Type"].ToString();
                            obj.Monthly_Month = dt.Rows[i]["Monthly_Month"].ToString();
                            obj.Monthly_FiscalYear = dt.Rows[i]["Monthly_FiscalYear"].ToString();

                            //Yearly
                            obj.Yearly_Section = dt.Rows[i]["Yearly_Section"].ToString();
                            obj.Yearly_Value = dt.Rows[i]["Yearly_Value"].ToString();
                            obj.Yearly_Type = dt.Rows[i]["Yearly_Type"].ToString();
                            obj.Yearly_FiscalYear = dt.Rows[i]["Yearly_Fiscal Year"].ToString();

                            if (dt.Rows[i]["Daily_Section"].ToString() != "" || dt.Rows[i]["Yearly_Section"].ToString() != "" || dt.Rows[i]["Graph_Section"].ToString() != "" || dt.Rows[i]["Monthly_Section"].ToString() != "")
                            {
                                obj.UploadDate = DateTime.Now.ToString();
                            }

                            list.Add(obj);
                        }

                        EfficiencyDatagrid.DataSource = list;
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Please check the file if match to the selected template.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //}
                        //try
                        //{
                        //    DataTable dtExcel = new DataTable();
                        //    dtExcel = ReadExcel(filePath, fileExt);//read excel file
                        //    ProdEfficiencyDatagrid.Visible = true;
                        //    ProdEfficiencyDatagrid.DataSource = dtExcel;
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show(ex.Message.ToString());
                        //}
                    }
                    else if (TemplateDropdownList.Text == "Monthly Top 3 Contributor" || TemplateDropdownList.Text == "Daily Top 3 Contributor")
                    {
                        //try
                        //{
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show(ex.Message.ToString());
                        //}
                    }
                }
                else if (TargetSettingForm.Category == "Factory Efficiency")
                {
                    if (TemplateDropdownList.Text == "MH Annual Target")
                    {
                        //List<FactoryEfficiency_Class> list = new List<FactoryEfficiency_Class>();

                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{

                        //    FactoryEfficiency_Class obj = new FactoryEfficiency_Class();

                        //    obj.Section = dt.Rows[i]["Section Detail"].ToString();
                        //    obj.Apr = dt.Rows[i]["Apr"].ToString();
                        //    obj.May = dt.Rows[i]["May"].ToString();
                        //    obj.Jun = dt.Rows[i]["Jun"].ToString();
                        //    obj.Jul = dt.Rows[i]["Jul"].ToString();
                        //    obj.Aug = dt.Rows[i]["Aug"].ToString();
                        //    obj.Sep = dt.Rows[i]["Sep"].ToString();
                        //    obj.Oct = dt.Rows[i]["Oct"].ToString();
                        //    obj.Nov = dt.Rows[i]["Nov"].ToString();
                        //    obj.Dec = dt.Rows[i]["Dec"].ToString();
                        //    obj.Jan = dt.Rows[i]["Jan"].ToString();
                        //    obj.Feb = dt.Rows[i]["Feb"].ToString();
                        //    obj.Mar = dt.Rows[i]["Mar"].ToString();
                        //    obj.Annual = dt.Rows[i]["Annual"].ToString();


                        //    list.Add(obj);
                        //}

                        //ProdEfficiencyDatagrid.DataSource = list;

                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;

                            EfficiencyDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    else if (TemplateDropdownList.Text == "ST Annual Target")
                    {
                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;

                            EfficiencyDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    else if (TemplateDropdownList.Text == "MH Monthly")
                    {
                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;

                            EfficiencyDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    else if (TemplateDropdownList.Text == "ST Monthly")
                    {
                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;

                            EfficiencyDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    else if (TemplateDropdownList.Text == "FE Monthly Graph")
                    {
                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;

                            EfficiencyDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    else if (TemplateDropdownList.Text == "FE Quarterly Graph")
                    {
                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;

                            EfficiencyDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    else if (TemplateDropdownList.Text == "Ideal Variance Rate Monthly Graph")
                    {
                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;

                            EfficiencyDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    else if (TemplateDropdownList.Text == "Ideal Variance Rate Quarterly Graph")
                    {
                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(filePath);//read excel file
                            EfficiencyDatagrid.Visible = true;
                            EfficiencyDatagrid.DataSource = dtExcel;

                            EfficiencyDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }
                else if (TargetSettingForm.Category == "Disposal Budget")
                {
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        dtExcel = ReadExcel(filePath);//read excel file
                        EfficiencyDatagrid.Visible = true;
                        EfficiencyDatagrid.DataSource = dtExcel;

                        EfficiencyDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }


            }
        }

        //public DataTable ReadExcel(string fileName, string fileExt)
        //{
        //    string conn = string.Empty;

        //    DataTable dtexcel = new DataTable();

        //    if (fileExt.CompareTo(".xls") == 0)//compare the extension of the file
        //        conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';";//for below excel 2007
        //    else
        //        conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';";//for above excel 2007
        //    using (OleDbConnection con = new OleDbConnection(conn))
        //    {
        //        try
        //        {
        //            OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [" + SheetDropdownList.Text + "$]", con);//here we read data from sheet1
        //            oleAdpt.Fill(dtexcel);//fill excel data into dataTable
        //        }
        //        catch (Exception ex)
        //        {
        //            //MessageBox.Show("Make sure the sheet name should be same as category name!", "Reminders");
        //            MessageBox.Show(ex.Message);
        //        }
        //    }

        //    return dtexcel;

        //}// end ReadExcel




        //public DataTable ReadExcel(string filePath)
        //{
        //    DataTable dt = new DataTable();

        //    using (var workbook = new XLWorkbook(filePath))
        //    {
        //        var worksheet = workbook.Worksheet(SheetDropdownList.Text); // Use selected sheet
        //        bool firstRow = true;

        //        foreach (var row in worksheet.RowsUsed())
        //        {
        //            if (firstRow)
        //            {
        //                // Create columns based on the first row (header)
        //                foreach (var cell in row.Cells())
        //                    dt.Columns.Add(cell.Value.ToString());
        //                firstRow = false;
        //            }
        //            else
        //            {
        //                // Ensure the row has enough columns to match the DataTable
        //                dt.Rows.Add();

        //                // For each cell in the row, only assign if it exists
        //                for (int i = 0; i < row.Cells().Count(); i++)
        //                {
        //                    // Ensure you don't try to access an index outside the DataTable's column count
        //                    if (i < dt.Columns.Count)
        //                    {
        //                        dt.Rows[dt.Rows.Count - 1][i] = row.Cell(i + 1).Value.ToString();
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return dt;
        //}

        public DataTable ReadExcel(string filePath)
        {
            DataTable dt = new DataTable();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(SheetDropdownList.Text); // Use selected sheet
                bool firstRow = true;
                List<int> validColumnIndexes = new List<int>(); // para i-track lang yung mga column na may header

                foreach (var row in worksheet.RowsUsed())
                {
                    if (firstRow)
                    {
                        int colCounter = 1;
                        foreach (var cell in row.Cells())
                        {
                            string header = cell.Value.ToString().Trim();

                            if (!string.IsNullOrEmpty(header)) // ✅ only add if not blank
                            {
                                dt.Columns.Add(header);
                                validColumnIndexes.Add(colCounter); // keep the column index
                            }
                            colCounter++;
                        }
                        firstRow = false;
                    }
                    else
                    {
                        DataRow dataRow = dt.NewRow();
                        int colIndex = 0;

                        // ✅ only read cells that belong to valid (non-blank header) columns
                        foreach (var colNumber in validColumnIndexes)
                        {
                            string value = row.Cell(colNumber).Value.ToString();
                            dataRow[colIndex] = value;
                            colIndex++;
                        }

                        dt.Rows.Add(dataRow);
                    }
                }
            }

            return dt;
        }



        //===============================================================================================>>>>>>>>>>>>>>>
        private void InsertFiles()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con2.State == ConnectionState.Closed)
            {
                con2.Open();
            }

            // -> SQL query to insert files in Efficiency files table
            if (TargetSettingForm.Category == "Production Efficiency")
            {
                SqlCommand InsertEfficiencyFiles = new SqlCommand("SP_InsertTargetFiles", con2);
                InsertEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                InsertEfficiencyFiles.Parameters.AddWithValue("@Procedure", "InsertProductionEfficiencyFiles");
                InsertEfficiencyFiles.Parameters.AddWithValue("@FileName", fileNameWithExt);
                InsertEfficiencyFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertEfficiencyFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertEfficiencyFiles.Parameters.AddWithValue("@FilePath", SaveDirectory + fileNameWithExt);
                InsertEfficiencyFiles.ExecuteNonQuery();
                con.Close();

                InsertProductionEfficiencyData();
            }
        }

        //===============================================================================================>>>>>>>>>>>>>>>
        private void InsertRevisedFiles()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con2.State == ConnectionState.Closed)
            {
                con2.Open();
            }

            // -> SQL query to insert files in Efficiency files table
            if (TargetSettingForm.Category == "Production Efficiency")
            {
                SqlCommand InsertEfficiencyFiles = new SqlCommand("SP_InsertTargetFiles", con2);
                InsertEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                InsertEfficiencyFiles.Parameters.AddWithValue("@Procedure", "InsertProductionEfficiencyFiles");
                InsertEfficiencyFiles.Parameters.AddWithValue("@FileName", NewFileName);
                InsertEfficiencyFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertEfficiencyFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertEfficiencyFiles.Parameters.AddWithValue("@FilePath", SaveDirectory + fileNameWithExt);
                InsertEfficiencyFiles.ExecuteNonQuery();
                con.Close();

                InsertProductionEfficiencyData();

                //MessageBox.Show("Production efficiency data uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //this.Close();
            }

        }

        //===============================================================================================>>>>>>>>>>>>>>>
        public void UpdateDateUpload()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con2.State == ConnectionState.Closed)
            {
                con2.Open();
            }

            // ---> Update query
            SqlCommand UpdateDateOfLastUpload = new SqlCommand("SP_UpdateDateOfLastUpload", con2);
            UpdateDateOfLastUpload.CommandType = CommandType.StoredProcedure;
            UpdateDateOfLastUpload.Parameters.AddWithValue("@Category", TargetSettingForm.Category);
            UpdateDateOfLastUpload.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
            UpdateDateOfLastUpload.ExecuteNonQuery();
            con.Close();
        }

        //===============================================================================================>>>>>>>>>>>>>>>

        string NewFileName;
        string SaveDirectory;

        private void UploadButton_Click(object sender, EventArgs e)
        {
            if (TargetSettingForm.Category == "Production Efficiency")
            {
                if (TemplateDropdownList.Text == "")
                {
                    MessageBox.Show("Please select template.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    TemplateDropdownList.Select();
                }
                else if (FilePath.Text == "")
                {
                    MessageBox.Show("Please select the file.", "MHMS Information!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    FilePath.Select();
                }
                else if (MonthDropdwn.Text == "")
                {
                    MessageBox.Show("Please select Month.", "MHMS Information!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    MonthDropdwn.Select();
                }
                else if (YearDropdown.Text == "")
                {
                    MessageBox.Show("Please select Year.", "MHMS Information!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    YearDropdown.Select();
                }
                //else if (SectionDropdownlist.Text == "")
                //{
                //    MessageBox.Show("Please select section.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    SectionDropdownlist.Select();
                //}
                else if (SheetDropdownList.Text == "")
                {
                    MessageBox.Show("Please select sheet.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SheetDropdownList.Select();
                }
                else if (EfficiencyDatagrid.DataSource == null)
                {
                    MessageBox.Show("No data has been detected, please check your file.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    //File destination
                    SaveDirectory = @"\\apbiphbpswb01\RELEASE\COPQ Files\Production Efficiency Files\";

                    if (!Directory.Exists(SaveDirectory))
                    {
                        Directory.CreateDirectory(SaveDirectory);
                    }

                    if (TemplateDropdownList.Text == "Total Efficiency")
                    {
                        if (File.Exists(SaveDirectory + fileNameWithExt)) // if the file to upload already exists, rename it and insert the new file into the database
                        {
                            NewFileName = fileName + "_Copy_" + DateTime.Now.ToString("MMddyyyyhhmmss") + fileExt; // Create new filename if it already exists
                            string newFileSavePath = Path.Combine(SaveDirectory, NewFileName); // Combine the path of the new folder with the filename
                            File.Copy(FilePath.Text, newFileSavePath, true);

                            InsertRevisedFiles(); // Insert the revised files
                            UpdateDateUpload(); // Update the date of the last upload
                        }
                        else
                        {
                            string FileDestination = Path.Combine(SaveDirectory, fileNameWithExt); // Combine the path of the new folder and filename
                            File.Copy(FilePath.Text, FileDestination, true);

                            InsertFiles(); // Insert the file into the database if it doesn't already exist
                            UpdateDateUpload(); // Update the date of the last upload
                        }

                        TargetSettingForm.LoadTargetSettings = true;
                    }
                    else
                    {
                        // List of all template names that require InsertProductionEfficiencyData()
                        HashSet<string> efficiencyTemplates = new HashSet<string>
                        {
                            "Direct Efficiency",
                            "Semi-Direct Rate",
                            "Total Loss Rate",
                            "Total Efficiency Graph",
                            "Direct Efficiency Graph PR1",
                            "Direct Efficiency Graph PR2",
                            "Direct Efficiency Graph IC",
                            "Direct Efficiency Graph TC",
                            "Direct Efficiency Graph PT",
                            "Direct Efficiency Graph IH",
                            "Direct Efficiency Graph TN",
                            "Semi-Direct Rate Graph",
                            "Total Loss Rate Graph",
                            "Efficiency Summary",
                            "Daily Top 3 Contributor",
                            "Monthly Top 3 Contributor"
                        };

                        if (efficiencyTemplates.Contains(TemplateDropdownList.Text))
                        {
                            InsertProductionEfficiencyData();
                        }
                    }


                }
            }
            else
            {
                if (TemplateDropdownList.Text == "")
                {
                    MessageBox.Show("Please select template.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    TemplateDropdownList.Select();
                }
                else if (FilePath.Text == "")
                {
                    MessageBox.Show("Please select the file.", "MHMS Information!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    FilePath.Select();
                }
                else if (YearDropdown.Text == "")
                {
                    MessageBox.Show("Please select Year.", "MHMS Information!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    YearDropdown.Select();
                }
                else if (SheetDropdownList.Text == "")
                {
                    MessageBox.Show("Please select sheet.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SheetDropdownList.Select();
                }
                else if (EfficiencyDatagrid.DataSource == null)
                {
                    MessageBox.Show("No data has been detected, please check your file.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    UploadtFactoryEfficiencyData();
                }
            }
        }

        public static string Department = "";
        public static string Section = "";
        public static string Month = "";
        public static string Costcenter = "";
        public static string FiscalYear = "";

        private void UploadtFactoryEfficiencyData()
        {
            // -> SQL query to select User Account
            con.Close();
            con.Open();
            SqlCommand SelectFERawData = new SqlCommand("SP_SelectFEGraphData", con);
            SelectFERawData.CommandType = CommandType.StoredProcedure;
            SelectFERawData.Parameters.AddWithValue("@Template", TemplateDropdownList.Text);
            SelectFERawData.Parameters.AddWithValue("@FiscalYear", YearDropdown.Text);
            SqlDataAdapter da = new SqlDataAdapter(SelectFERawData);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {

                foreach (DataGridViewRow row in EfficiencyDatagrid.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        //If data exists, perform an update
                        if (TemplateDropdownList.Text == "MH Annual Target")
                        {
                            //If data exists, perform an update
                            con.Close();
                            con.Open();
                            SqlCommand UpdateFERawData = new SqlCommand("SP_UpdateFERawData", con);
                            UpdateFERawData.CommandType = CommandType.StoredProcedure;
                            UpdateFERawData.Parameters.AddWithValue("@Template", TemplateDropdownList.Text);
                            UpdateFERawData.Parameters.AddWithValue("@Value", row.Cells["Value"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Section", row.Cells["Section Detail"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Actual_Forecast", row.Cells["Actual / Forecast"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Costcenter", "");
                            UpdateFERawData.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                            //UpdateFERawData.Parameters.AddWithValue("@Type", "Monthly Target");
                            UpdateFERawData.Parameters.AddWithValue("@FiscalYear", YearDropdown.Text);
                            UpdateFERawData.ExecuteNonQuery();
                            con.Close();

                        }
                        else if (TemplateDropdownList.Text == "ST Annual Target")
                        {
                            con.Close();
                            con.Open();
                            SqlCommand UpdateFERawData = new SqlCommand("SP_UpdateFERawData", con);
                            UpdateFERawData.CommandType = CommandType.StoredProcedure;
                            UpdateFERawData.Parameters.AddWithValue("@Template", TemplateDropdownList.Text);
                            UpdateFERawData.Parameters.AddWithValue("@Value", row.Cells["Value"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Section", row.Cells["Section"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Actual_Forecast", row.Cells["Actual / Forecast"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Costcenter", row.Cells["Cost Center"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@FiscalYear", YearDropdown.Text);
                            UpdateFERawData.ExecuteNonQuery();
                            con.Close();

                        }
                        else if (TemplateDropdownList.Text == "MH Monthly")
                        {
                            con.Close();
                            con.Open();
                            SqlCommand UpdateFERawData = new SqlCommand("SP_UpdateFERawData", con);
                            UpdateFERawData.CommandType = CommandType.StoredProcedure;
                            UpdateFERawData.Parameters.AddWithValue("@Template", TemplateDropdownList.Text);
                            UpdateFERawData.Parameters.AddWithValue("@Value", row.Cells["Value"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Section", row.Cells["Section Detail"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Actual_Forecast", row.Cells["Actual / Forecast"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Costcenter", "");
                            UpdateFERawData.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                            //UpdateFERawData.Parameters.AddWithValue("@Date", row.Cells["Date"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@FiscalYear", YearDropdown.Text);
                            UpdateFERawData.ExecuteNonQuery();
                            con.Close();

                        }
                        else if (TemplateDropdownList.Text == "ST Monthly")
                        {
                            con.Close();
                            con.Open();
                            SqlCommand UpdateFERawData = new SqlCommand("SP_UpdateFERawData", con);
                            UpdateFERawData.CommandType = CommandType.StoredProcedure;
                            UpdateFERawData.Parameters.AddWithValue("@Template", TemplateDropdownList.Text);
                            UpdateFERawData.Parameters.AddWithValue("@Value", row.Cells["Value"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Section", row.Cells["Section"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Actual_Forecast", row.Cells["Actual / Forecast"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Costcenter", row.Cells["Cost Center"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                            //UpdateFERawData.Parameters.AddWithValue("@Date", row.Cells["Date"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@FiscalYear", YearDropdown.Text);
                            UpdateFERawData.ExecuteNonQuery();
                            con.Close();


                        }
                        else if (TemplateDropdownList.Text == "FE Monthly Graph")
                        {
                            con.Close();
                            con.Open();
                            SqlCommand UpdateFERawData = new SqlCommand("SP_UpdateFEGraph", con);
                            UpdateFERawData.CommandType = CommandType.StoredProcedure;
                            UpdateFERawData.Parameters.AddWithValue("@Template", TemplateDropdownList.Text);
                            UpdateFERawData.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Quarter", ""); //Empty parameter not use in this procedure
                            UpdateFERawData.Parameters.AddWithValue("@MonthlyResult", row.Cells["Monthly Result"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@QuarterlyResult", ""); //Empty parameter not use in this procedure
                            UpdateFERawData.Parameters.AddWithValue("@Type", row.Cells["Type"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@CumulativeResult", row.Cells["Cumulative Result"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@CumulativeType", row.Cells["Type Cumulative"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Status", row.Cells["Status"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@FiscalYear", row.Cells["Fiscal Year"].Value);
                            UpdateFERawData.ExecuteNonQuery();
                            con.Close();
                        }
                        else if (TemplateDropdownList.Text == "FE Quarterly Graph")
                        {
                            con.Close();
                            con.Open();
                            SqlCommand UpdateFERawData = new SqlCommand("SP_UpdateFEGraph", con);
                            UpdateFERawData.CommandType = CommandType.StoredProcedure;
                            UpdateFERawData.Parameters.AddWithValue("@Template", TemplateDropdownList.Text);
                            UpdateFERawData.Parameters.AddWithValue("@Month", ""); //Empty parameter not use in this procedure
                            UpdateFERawData.Parameters.AddWithValue("@Quarter", row.Cells["Quarter"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@MonthlyResult", ""); //Empty parameter not use in this procedure
                            UpdateFERawData.Parameters.AddWithValue("@QuarterlyResult", row.Cells["Quarterly Result"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Type", row.Cells["Type"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@CumulativeResult", row.Cells["Cumulative Result"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@CumulativeType", row.Cells["Type Cumulative"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Status", row.Cells["Status"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@FiscalYear", row.Cells["Fiscal Year"].Value);
                            UpdateFERawData.ExecuteNonQuery();
                            con.Close();
                        }
                        else if (TemplateDropdownList.Text == "Ideal Variance Rate Monthly Graph")
                        {
                            con.Close();
                            con.Open();
                            SqlCommand UpdateFERawData = new SqlCommand("SP_Update_IdealVarianceRateGraph", con);
                            UpdateFERawData.CommandType = CommandType.StoredProcedure;
                            UpdateFERawData.Parameters.AddWithValue("@Template", TemplateDropdownList.Text);
                            UpdateFERawData.Parameters.AddWithValue("@Department", row.Cells["Department"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Quarter", "");//Declare but not use
                            UpdateFERawData.Parameters.AddWithValue("@IdealVarianceRate", row.Cells["Ideal Variance Rate"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@FiscalYear", row.Cells["Fiscal Year"].Value);
                            UpdateFERawData.ExecuteNonQuery();
                            con.Close();
                        }
                        else if (TemplateDropdownList.Text == "Ideal Variance Rate Quarterly Graph")
                        {
                            con.Close();
                            con.Open();
                            SqlCommand UpdateFERawData = new SqlCommand("SP_Update_IdealVarianceRateGraph", con);
                            UpdateFERawData.CommandType = CommandType.StoredProcedure;
                            UpdateFERawData.Parameters.AddWithValue("@Template", TemplateDropdownList.Text);
                            UpdateFERawData.Parameters.AddWithValue("@Department", row.Cells["Department"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Month", "");//Declare but not use
                            UpdateFERawData.Parameters.AddWithValue("@Quarter", "Q" + row.Cells["Quarter"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@IdealVarianceRate", row.Cells["Ideal Variance Rate"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@FiscalYear", row.Cells["Fiscal Year"].Value);
                            UpdateFERawData.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

                MessageBox.Show("Factory Efficiency data has been updated!", "MHMS Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                foreach (DataGridViewRow row in EfficiencyDatagrid.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        // If data doesn't exist, perform an insert
                        if (TemplateDropdownList.Text == "MH Annual Target")
                        {
                            con.Close();
                            con.Open();
                            SqlCommand InsertFEMHAnnualTarget = new SqlCommand("SP_Insert_FE_Annual_Target", con);
                            InsertFEMHAnnualTarget.CommandType = CommandType.StoredProcedure;
                            InsertFEMHAnnualTarget.Parameters.AddWithValue("@Procedure", "MH Annual Target");
                            InsertFEMHAnnualTarget.Parameters.AddWithValue("@Department", row.Cells["Department"].Value);
                            InsertFEMHAnnualTarget.Parameters.AddWithValue("@Costcenter", "");
                            InsertFEMHAnnualTarget.Parameters.AddWithValue("@Section", row.Cells["Section Detail"].Value);
                            InsertFEMHAnnualTarget.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                            InsertFEMHAnnualTarget.Parameters.AddWithValue("@Value", row.Cells["Value"].Value);
                            InsertFEMHAnnualTarget.Parameters.AddWithValue("@Actual_Forecast", row.Cells["Actual / Forecast"].Value);
                            InsertFEMHAnnualTarget.Parameters.AddWithValue("@Total", row.Cells["Total"].Value);
                            InsertFEMHAnnualTarget.Parameters.AddWithValue("@FiscalYear", YearDropdown.Text);
                            InsertFEMHAnnualTarget.ExecuteNonQuery();
                            con.Close();

                        }
                        else if (TemplateDropdownList.Text == "ST Annual Target")
                        {
                            con.Close();
                            con.Open();
                            SqlCommand InsertFESTAnnualTarget = new SqlCommand("SP_Insert_FE_Annual_Target", con);
                            InsertFESTAnnualTarget.CommandType = CommandType.StoredProcedure;
                            InsertFESTAnnualTarget.Parameters.AddWithValue("@Procedure", "ST Annual Target");
                            InsertFESTAnnualTarget.Parameters.AddWithValue("@Department", "");
                            InsertFESTAnnualTarget.Parameters.AddWithValue("@Section", row.Cells["Section"].Value);
                            InsertFESTAnnualTarget.Parameters.AddWithValue("@Costcenter", row.Cells["Cost Center"].Value);
                            InsertFESTAnnualTarget.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                            InsertFESTAnnualTarget.Parameters.AddWithValue("@Value", row.Cells["Value"].Value);
                            InsertFESTAnnualTarget.Parameters.AddWithValue("@Actual_Forecast", row.Cells["Actual / Forecast"].Value);
                            InsertFESTAnnualTarget.Parameters.AddWithValue("@Total", row.Cells["Total"].Value);
                            InsertFESTAnnualTarget.Parameters.AddWithValue("@FiscalYear", YearDropdown.Text);
                            InsertFESTAnnualTarget.ExecuteNonQuery();
                            con.Close();

                        }
                        else if (TemplateDropdownList.Text == "MH Monthly")
                        {
                            con.Close();
                            con.Open();
                            SqlCommand InsertFEMHMonthly = new SqlCommand("SP_Insert_FE_Monthly", con);
                            InsertFEMHMonthly.CommandType = CommandType.StoredProcedure;
                            InsertFEMHMonthly.Parameters.AddWithValue("@Procedure", "MH Monthly");
                            InsertFEMHMonthly.Parameters.AddWithValue("@Department", row.Cells["Department"].Value);
                            InsertFEMHMonthly.Parameters.AddWithValue("@Costcenter", "");
                            InsertFEMHMonthly.Parameters.AddWithValue("@Section", row.Cells["Section Detail"].Value);
                            InsertFEMHMonthly.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                            InsertFEMHMonthly.Parameters.AddWithValue("@Date", row.Cells["Date"].Value);
                            InsertFEMHMonthly.Parameters.AddWithValue("@Value", row.Cells["Value"].Value);
                            InsertFEMHMonthly.Parameters.AddWithValue("@Actual_Forecast", row.Cells["Actual / Forecast"].Value);
                            InsertFEMHMonthly.Parameters.AddWithValue("@Total", row.Cells["Total"].Value);
                            InsertFEMHMonthly.Parameters.AddWithValue("@Type", "Monthly Actual");
                            InsertFEMHMonthly.Parameters.AddWithValue("@FiscalYear", YearDropdown.Text);
                            InsertFEMHMonthly.ExecuteNonQuery();
                            con.Close();
                        }
                        else if (TemplateDropdownList.Text == "ST Monthly")
                        {
                            con.Close();
                            con.Open();
                            SqlCommand InsertFESTMonthly = new SqlCommand("SP_Insert_FE_Monthly", con);
                            InsertFESTMonthly.CommandType = CommandType.StoredProcedure;
                            InsertFESTMonthly.Parameters.AddWithValue("@Procedure", "ST Monthly");
                            InsertFESTMonthly.Parameters.AddWithValue("@Department", "");
                            InsertFESTMonthly.Parameters.AddWithValue("@Section", row.Cells["Section"].Value);
                            InsertFESTMonthly.Parameters.AddWithValue("@Costcenter", row.Cells["Cost Center"].Value);
                            InsertFESTMonthly.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                            InsertFESTMonthly.Parameters.AddWithValue("@Date", ""); //Empty parameter
                            InsertFESTMonthly.Parameters.AddWithValue("@Value", row.Cells["Value"].Value);
                            InsertFESTMonthly.Parameters.AddWithValue("@Actual_Forecast", row.Cells["Actual / Forecast"].Value);
                            InsertFESTMonthly.Parameters.AddWithValue("@Total", row.Cells["Total"].Value);
                            InsertFESTMonthly.Parameters.AddWithValue("@Type", "");
                            InsertFESTMonthly.Parameters.AddWithValue("@FiscalYear", YearDropdown.Text);
                            InsertFESTMonthly.ExecuteNonQuery();
                            con.Close();

                        }
                        else if (TemplateDropdownList.Text == "FE Monthly Graph")
                        {
                            con.Close();
                            con.Open();
                            SqlCommand UpdateFERawData = new SqlCommand("SP_InsertFEGraph", con);
                            UpdateFERawData.CommandType = CommandType.StoredProcedure;
                            UpdateFERawData.Parameters.AddWithValue("@Template", TemplateDropdownList.Text);
                            UpdateFERawData.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Quarter", ""); //Empty parameter
                            UpdateFERawData.Parameters.AddWithValue("@MonthlyResult", row.Cells["Monthly Result"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@QuarterlyResult", ""); //Empty parameter
                            UpdateFERawData.Parameters.AddWithValue("@Type", row.Cells["Type"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@CumulativeResult", row.Cells["Cumulative Result"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@CumulativeType", row.Cells["Type Cumulative"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Status", row.Cells["Status"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@FiscalYear", row.Cells["Fiscal Year"].Value);
                            UpdateFERawData.ExecuteNonQuery();
                            con.Close();

                        }
                        else if (TemplateDropdownList.Text == "FE Quarterly Graph")
                        {
                            con.Close();
                            con.Open();
                            SqlCommand UpdateFERawData = new SqlCommand("SP_InsertFEGraph", con);
                            UpdateFERawData.CommandType = CommandType.StoredProcedure;
                            UpdateFERawData.Parameters.AddWithValue("@Template", TemplateDropdownList.Text);
                            UpdateFERawData.Parameters.AddWithValue("@Month", ""); //Empty parameter
                            UpdateFERawData.Parameters.AddWithValue("@Quarter", row.Cells["Quarter"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@MonthlyResult", ""); //Empty parameter
                            UpdateFERawData.Parameters.AddWithValue("@QuarterlyResult", row.Cells["Quarterly Result"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Type", row.Cells["Type"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@CumulativeResult", row.Cells["Cumulative Result"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@CumulativeType", row.Cells["Type Cumulative"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Status", row.Cells["Status"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@FiscalYear", row.Cells["Fiscal Year"].Value);
                            UpdateFERawData.ExecuteNonQuery();
                            con.Close();
                        }
                        else if (TemplateDropdownList.Text == "Ideal Variance Rate Monthly Graph")
                        {
                            con.Close();
                            con.Open();
                            SqlCommand UpdateFERawData = new SqlCommand("SP_Insert_IdealVarianceRateGraph", con);
                            UpdateFERawData.CommandType = CommandType.StoredProcedure;
                            UpdateFERawData.Parameters.AddWithValue("@Template", TemplateDropdownList.Text);
                            UpdateFERawData.Parameters.AddWithValue("@Department", row.Cells["Department"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Quarter", "");//Declare but not use
                            UpdateFERawData.Parameters.AddWithValue("@IdealVarianceRate", row.Cells["Ideal Variance Rate"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@FiscalYear", YearDropdown.Text);
                            UpdateFERawData.ExecuteNonQuery();
                            con.Close();
                        }
                        else if (TemplateDropdownList.Text == "Ideal Variance Rate Quarterly Graph")
                        {
                            con.Close();
                            con.Open();
                            SqlCommand UpdateFERawData = new SqlCommand("SP_Insert_IdealVarianceRateGraph", con);
                            UpdateFERawData.CommandType = CommandType.StoredProcedure;
                            UpdateFERawData.Parameters.AddWithValue("@Template", TemplateDropdownList.Text);
                            UpdateFERawData.Parameters.AddWithValue("@Department", row.Cells["Department"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@Month", "");//Declare but not use
                            UpdateFERawData.Parameters.AddWithValue("@Quarter", "Q" + row.Cells["Quarter"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@IdealVarianceRate", row.Cells["Ideal Variance Rate"].Value);
                            UpdateFERawData.Parameters.AddWithValue("@FiscalYear", YearDropdown.Text);
                            UpdateFERawData.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

                MessageBox.Show("Factory Efficiency data inserted successfully!", "MHMS Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            TemplateDropdownList.Text = "";
            FilePath.Text = "";
            SheetDropdownList.Text = "";
            EfficiencyDatagrid.DataSource = null;
            YearDropdown.Text = "";
        }

        

        private void DeleteFEPreviousUpload()
        {
            con.Open();
            SqlCommand DeleteEfficiencyPreviousUpload = new SqlCommand("SP_DeleteFEPreviousUpload", con);
            DeleteEfficiencyPreviousUpload.CommandType = CommandType.StoredProcedure;
            DeleteEfficiencyPreviousUpload.Parameters.AddWithValue("Template", TemplateDropdownList.Text);
            DeleteEfficiencyPreviousUpload.Parameters.AddWithValue("FiscalYear", YearDropdown.Text);
            DeleteEfficiencyPreviousUpload.ExecuteNonQuery();
            con.Close();
        }

        private void DeleteEfficiencyPreviousUpload()
        {
            // -> SQL query to delete factor loss
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand DeleteEfficiencyPreviousUpload = new SqlCommand("SP_DeleteEfficiencyPreviousUpload", con);
            DeleteEfficiencyPreviousUpload.CommandType = CommandType.StoredProcedure;
            DeleteEfficiencyPreviousUpload.Parameters.AddWithValue("Template", TemplateDropdownList.Text);
            DeleteEfficiencyPreviousUpload.Parameters.AddWithValue("Month", MonthDropdwn.Text);
            DeleteEfficiencyPreviousUpload.Parameters.AddWithValue("FiscalYear", YearDropdown.Text);
            DeleteEfficiencyPreviousUpload.ExecuteNonQuery();
            con.Close();
        }

        private void DeleteEfficiencyTableEmptyRow()
        {
            // -> SQL query to delete factor loss
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand DeleteEfficiencyPreviousUpload = new SqlCommand("SP_DeleteEfficiencyTableEmptyRow", con);
            DeleteEfficiencyPreviousUpload.CommandType = CommandType.StoredProcedure;
            DeleteEfficiencyPreviousUpload.Parameters.AddWithValue("Template", TemplateDropdownList.Text);
            DeleteEfficiencyPreviousUpload.Parameters.AddWithValue("Month", MonthDropdwn.Text);
            DeleteEfficiencyPreviousUpload.Parameters.AddWithValue("FiscalYear", YearDropdown.Text);
            DeleteEfficiencyPreviousUpload.ExecuteNonQuery();
            con.Close();
        }

        private void DeleteEfficiencyGraphPreviousUpload()
        {
            // -> SQL query to delete factor loss
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand DeleteEfficiencyGraph = new SqlCommand("SP_DeleteEfficiencyGraphPreviousUpload", con);
            DeleteEfficiencyGraph.CommandType = CommandType.StoredProcedure;
            DeleteEfficiencyGraph.Parameters.AddWithValue("Template", TemplateDropdownList.Text);
            DeleteEfficiencyGraph.Parameters.AddWithValue("Month", MonthDropdwn.Text);
            DeleteEfficiencyGraph.Parameters.AddWithValue("FiscalYear", YearDropdown.Text);
            DeleteEfficiencyGraph.ExecuteNonQuery();
            con.Close();
        }

        private void DeleteEfficiencyOverallResultGraphPreviousUpload()
        {
            // -> SQL query to delete factor loss
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand DeleteEfficiencyOverallGraph = new SqlCommand("SP_DeleteEfficiencyOverallResultGraphPreviousUpload", con);
            DeleteEfficiencyOverallGraph.CommandType = CommandType.StoredProcedure;
            DeleteEfficiencyOverallGraph.Parameters.AddWithValue("Template", TemplateDropdownList.Text);
            DeleteEfficiencyOverallGraph.Parameters.AddWithValue("Month", MonthDropdwn.Text);
            DeleteEfficiencyOverallGraph.Parameters.AddWithValue("FiscalYear", YearDropdown.Text);
            DeleteEfficiencyOverallGraph.ExecuteNonQuery();
            con.Close();
        }


        private void InsertProductionEfficiencyData()
        {
            if (TemplateDropdownList.Text == "Total Efficiency")
            {
                //Delete the previous upload data within a month
                DeleteEfficiencyPreviousUpload();

                DapperPlusManager.Entity<TotalEfficiency_Class>().Table("TBL_ProdEff_TotalEfficiency");

                List<TotalEfficiency_Class> TotalEfficiency = EfficiencyDatagrid.DataSource as List<TotalEfficiency_Class>;

                if (TotalEfficiency != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(TotalEfficiency);
                    }
                }

                // -----------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<TotalEfficiency_Class>().Table("TBL_ProdEff_TotalEfficiency_WC");

                List<TotalEfficiency_Class> TotalEfficiency_Workcenter = EfficiencyDatagrid.DataSource as List<TotalEfficiency_Class>;

                if (TotalEfficiency_Workcenter != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(TotalEfficiency_Workcenter);
                    }
                }

                //-----------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<TotalEfficiency_Class>().Table("TBL_ProdEff_TotalEfficiency_CC");

                List<TotalEfficiency_Class> TotalEfficiency_Costcenter = EfficiencyDatagrid.DataSource as List<TotalEfficiency_Class>;

                if (TotalEfficiency_Costcenter != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(TotalEfficiency_Costcenter);
                    }
                }

                //-----------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<TotalEfficiency_Class>().Table("TBL_ProdEff_TotalEfficiency_Process");

                List<TotalEfficiency_Class> TotalEfficiency_Process = EfficiencyDatagrid.DataSource as List<TotalEfficiency_Class>;

                if (TotalEfficiency_Process != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(TotalEfficiency_Process);
                    }
                }


                DeleteEfficiencyTableEmptyRow(); //After uploading this function will delete empty row

                MessageBox.Show("Total Efficiency data uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //this.Close();

            }
            else if (TemplateDropdownList.Text == "Total Efficiency Graph")
            {
                //Delete Previous upload
                DeleteEfficiencyGraphPreviousUpload();
                DeleteEfficiencyOverallResultGraphPreviousUpload();

                DapperPlusManager.Entity<TotalEff_Graph_Class>().Table("TBL_ProdEff_Graph_TotalEfficiency");

                List<TotalEff_Graph_Class> TotalEfficiencyGraphData = EfficiencyDatagrid.DataSource as List<TotalEff_Graph_Class>;

                if (TotalEfficiencyGraphData != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(TotalEfficiencyGraphData);
                    }
                }

                DapperPlusManager.Entity<TotalEff_Graph_Class>().Table("TBL_ProdEff_Graph_TotalEfficiency_OverallResult");

                List<TotalEff_Graph_Class> TotalEfficiencyGraphOverallResultData = EfficiencyDatagrid.DataSource as List<TotalEff_Graph_Class>;

                if (TotalEfficiencyGraphOverallResultData != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(TotalEfficiencyGraphOverallResultData);
                    }
                }

                MessageBox.Show("Total Efficiency graph data uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //this.Close();
                TemplateDropdownList.Text = "";
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                EfficiencyDatagrid.DataSource = null;

            }
            else if (TemplateDropdownList.Text == "Direct Efficiency")
            {
                //Delete the previous upload data within a month
                DeleteEfficiencyPreviousUpload();

                //---------------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<DirectEfficiency_Class>().Table("TBL_ProdEff_DirectEfficiency");

                List<DirectEfficiency_Class> DirectEfficiency = EfficiencyDatagrid.DataSource as List<DirectEfficiency_Class>;

                if (DirectEfficiency != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(DirectEfficiency);
                    }
                }

                //---------------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<DirectEfficiency_Class>().Table("TBL_ProdEff_DirectEfficiency_CC");

                List<DirectEfficiency_Class> DirectEfficiency_CC = EfficiencyDatagrid.DataSource as List<DirectEfficiency_Class>;

                if (DirectEfficiency_CC != null)

                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(DirectEfficiency_CC);
                    }
                }

                //---------------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<DirectEfficiency_Class>().Table("TBL_ProdEff_DirectEfficiency_WC");

                List<DirectEfficiency_Class> DirectEfficiency_WC = EfficiencyDatagrid.DataSource as List<DirectEfficiency_Class>;

                if (DirectEfficiency_WC != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(DirectEfficiency_WC);
                    }
                }

                //---------------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<DirectEfficiency_Class>().Table("TBL_ProdEff_DirectEfficiency_Contributor");

                List<DirectEfficiency_Class> DirectEfficiency_Contributor = EfficiencyDatagrid.DataSource as List<DirectEfficiency_Class>;

                if (DirectEfficiency_Contributor != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(DirectEfficiency_Contributor);
                    }
                }

                //---------------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<DirectEfficiency_Class>().Table("TBL_ProdEff_DirectEfficiency_Process");

                List<DirectEfficiency_Class> DirectEfficiency_Process = EfficiencyDatagrid.DataSource as List<DirectEfficiency_Class>;

                if (DirectEfficiency_Process != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(DirectEfficiency_Process);
                    }
                }
                
                //---------------------------------------------------------------------------------------------------

                DeleteEfficiencyTableEmptyRow();//After uploading this function will delete empty row 

                MessageBox.Show("Direct Efficiency data uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //this.Close();
            }
            else if (TemplateDropdownList.Text == "Direct Efficiency Graph PR2")
            {

                try
                {
                    DataTable dt = GetDataTableFromGrid(EfficiencyDatagrid);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Add UploadDate column if not exists
                        if (!dt.Columns.Contains("UploadDate"))
                            dt.Columns.Add("UploadDate", typeof(DateTime));

                        // Fill with current date/time
                        foreach (DataRow row in dt.Rows)
                        {
                            row["UploadDate"] = DateTime.Now;
                        }

                        // 1. Sync schema before bulk insert
                        SyncTableColumns("TBL_ProdEff_Graph_DirectEfficiency_PR2", dt);

                        // 2. Bulk insert into SQL Server
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                            {
                                bulkCopy.DestinationTableName = "TBL_ProdEff_Graph_DirectEfficiency_PR2";

                                // Map columns automatically (column names must match)
                                foreach (DataColumn col in dt.Columns)
                                {
                                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                                }

                                bulkCopy.WriteToServer(dt);
                            }
                        }

                        MessageBox.Show("Tape Cassette direct efficiency data uploaded successfully!",
                                        "Notification",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error uploading: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                TemplateDropdownList.Text = "";
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                EfficiencyDatagrid.DataSource = null;


                //------------------------------------------------------------------->>>>>>

                //DapperPlusManager.Entity<DirectEff_Graph_PR2>().Table("TBL_ProdEff_Graph_DirectEfficiency_PR2");

                //List<DirectEff_Graph_PR2> DirectEfficiencyGraphData = EfficiencyDatagrid.DataSource as List<DirectEff_Graph_PR2>;

                //if (DirectEfficiencyGraphData != null)
                //{
                //    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                //    {
                //        db.BulkInsert(DirectEfficiencyGraphData);
                //    }
                //}

                //MessageBox.Show("Printer 2 direct efficiency data uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ////this.Close();
                //TemplateDropdownList.Text = "";
                //FilePath.Text = "";
                //SheetDropdownList.Text = "";
                //EfficiencyDatagrid.DataSource = null;

            }
            else if (TemplateDropdownList.Text == "Direct Efficiency Graph PR1")
            {
                try
                {
                    DataTable dt = GetDataTableFromGrid(EfficiencyDatagrid);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Add UploadDate column if not exists
                        if (!dt.Columns.Contains("UploadDate"))
                            dt.Columns.Add("UploadDate", typeof(DateTime));

                        // Fill with current date/time
                        foreach (DataRow row in dt.Rows)
                        {
                            row["UploadDate"] = DateTime.Now;
                        }

                        // 1. Sync schema before bulk insert
                        SyncTableColumns("TBL_ProdEff_Graph_DirectEfficiency_PR1", dt);

                        // 2. Bulk insert into SQL Server
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                            {
                                bulkCopy.DestinationTableName = "TBL_ProdEff_Graph_DirectEfficiency_PR1";

                                // Map columns automatically (column names must match)
                                foreach (DataColumn col in dt.Columns)
                                {
                                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                                }

                                bulkCopy.WriteToServer(dt);
                            }
                        }

                        MessageBox.Show("Tape Cassette direct efficiency data uploaded successfully!",
                                        "Notification",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error uploading: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                TemplateDropdownList.Text = "";
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                EfficiencyDatagrid.DataSource = null;

                //DapperPlusManager.Entity<DirectEff_Graph_PR1>().Table("TBL_ProdEff_Graph_DirectEfficiency_PR1");

                //List<DirectEff_Graph_PR1> DirectEfficiencyGraphData = EfficiencyDatagrid.DataSource as List<DirectEff_Graph_PR1>;

                //if (DirectEfficiencyGraphData != null)
                //{
                //    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                //    {
                //        db.BulkInsert(DirectEfficiencyGraphData);
                //    }
                //}

                //MessageBox.Show("Printer 1 direct efficiency data uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ////this.Close();
                //TemplateDropdownList.Text = "";
                //FilePath.Text = "";
                //SheetDropdownList.Text = "";
                //EfficiencyDatagrid.DataSource = null;

            }
            else if (TemplateDropdownList.Text == "Direct Efficiency Graph TC")
            {
                try
                {
                    DataTable dt = GetDataTableFromGrid(EfficiencyDatagrid);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Add UploadDate column if not exists
                        if (!dt.Columns.Contains("UploadDate"))
                            dt.Columns.Add("UploadDate", typeof(DateTime));

                        // Fill with current date/time
                        foreach (DataRow row in dt.Rows)
                        {
                            row["UploadDate"] = DateTime.Now;
                        }

                        // 1. Sync schema before bulk insert
                        SyncTableColumns("TBL_ProdEff_Graph_DirectEfficiency_TC", dt);

                        // 2. Bulk insert into SQL Server
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                            {
                                bulkCopy.DestinationTableName = "TBL_ProdEff_Graph_DirectEfficiency_TC";

                                // Map columns automatically (column names must match)
                                foreach (DataColumn col in dt.Columns)
                                {
                                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                                }

                                bulkCopy.WriteToServer(dt);
                            }
                        }

                        MessageBox.Show("Tape Cassette direct efficiency data uploaded successfully!",
                                        "Notification",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error uploading: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                TemplateDropdownList.Text = "";
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                EfficiencyDatagrid.DataSource = null;

            }
            else if (TemplateDropdownList.Text == "Direct Efficiency Graph IC")
            {
                try
                {
                    DataTable dt = GetDataTableFromGrid(EfficiencyDatagrid);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Add UploadDate column if not exists
                        if (!dt.Columns.Contains("UploadDate"))
                            dt.Columns.Add("UploadDate", typeof(DateTime));

                        // Fill with current date/time
                        foreach (DataRow row in dt.Rows)
                        {
                            row["UploadDate"] = DateTime.Now;
                        }

                        // 1. Sync schema before bulk insert
                        SyncTableColumns("TBL_ProdEff_Graph_DirectEfficiency_IC", dt);

                        // 2. Bulk insert into SQL Server
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                            {
                                bulkCopy.DestinationTableName = "TBL_ProdEff_Graph_DirectEfficiency_IC";

                                // Map columns automatically (column names must match)
                                foreach (DataColumn col in dt.Columns)
                                {
                                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                                }

                                bulkCopy.WriteToServer(dt);
                            }
                        }

                        MessageBox.Show("Ink Cartridge direct efficiency data uploaded successfully!",
                                        "Notification",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error uploading: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                TemplateDropdownList.Text = "";
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                EfficiencyDatagrid.DataSource = null;

                //DapperPlusManager.Entity<DirectEff_Graph_IC>().Table("TBL_ProdEff_Graph_DirectEfficiency_IC");

                //List<DirectEff_Graph_IC> DirectEfficiencyGraphData = EfficiencyDatagrid.DataSource as List<DirectEff_Graph_IC>;

                //try
                //{
                //    if (DirectEfficiencyGraphData != null)
                //    {
                //        // 1. Sync schema before bulk insert
                //        SyncTableColumns("TBL_ProdEff_Graph_DirectEfficiency_IC", DirectEfficiencyGraphData);

                //        using (IDbConnection db = new SqlConnection(connectionString))
                //        {
                //            db.BulkInsert(DirectEfficiencyGraphData);
                //        }
                //    }

                //    MessageBox.Show("Ink Cartridge direct efficiency data uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show($"Error uploading: { ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}

                ////this.Close();
                //TemplateDropdownList.Text = "";
                //FilePath.Text = "";
                //SheetDropdownList.Text = "";
                //EfficiencyDatagrid.DataSource = null;

            }
            else if (TemplateDropdownList.Text == "Direct Efficiency Graph PT")
            {
                try
                {
                    DataTable dt = GetDataTableFromGrid(EfficiencyDatagrid);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Add UploadDate column if not exists
                        if (!dt.Columns.Contains("UploadDate"))
                            dt.Columns.Add("UploadDate", typeof(DateTime));

                        // Fill with current date/time
                        foreach (DataRow row in dt.Rows)
                        {
                            row["UploadDate"] = DateTime.Now;
                        }

                        // 1. Sync schema before bulk insert
                        SyncTableColumns("TBL_ProdEff_Graph_DirectEfficiency_PT", dt);

                        // 2. Bulk insert into SQL Server
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                            {
                                bulkCopy.DestinationTableName = "TBL_ProdEff_Graph_DirectEfficiency_PT";

                                // Map columns automatically (column names must match)
                                foreach (DataColumn col in dt.Columns)
                                {
                                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                                }

                                bulkCopy.WriteToServer(dt);
                            }
                        }

                        MessageBox.Show("P-Touch direct efficiency data uploaded successfully!",
                                        "Notification",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error uploading: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                TemplateDropdownList.Text = "";
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                EfficiencyDatagrid.DataSource = null;

            }
            else if (TemplateDropdownList.Text == "Direct Efficiency Graph IH")
            {
                try
                {
                    DataTable dt = GetDataTableFromGrid(EfficiencyDatagrid);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Add UploadDate column if not exists
                        if (!dt.Columns.Contains("UploadDate"))
                            dt.Columns.Add("UploadDate", typeof(DateTime));

                        // Fill with current date/time
                        foreach (DataRow row in dt.Rows)
                        {
                            row["UploadDate"] = DateTime.Now;
                        }

                        // 1. Sync schema before bulk insert
                        SyncTableColumns("TBL_ProdEff_Graph_DirectEfficiency_IH", dt);

                        // 2. Bulk insert into SQL Server
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                            {
                                bulkCopy.DestinationTableName = "TBL_ProdEff_Graph_DirectEfficiency_IH";

                                // Map columns automatically (column names must match)
                                foreach (DataColumn col in dt.Columns)
                                {
                                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                                }

                                bulkCopy.WriteToServer(dt);
                            }
                        }

                        MessageBox.Show("Ink Head direct efficiency data uploaded successfully!",
                                        "Notification",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error uploading: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                TemplateDropdownList.Text = "";
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                EfficiencyDatagrid.DataSource = null;


            }
            else if (TemplateDropdownList.Text == "Direct Efficiency Graph TN")
            {
                try
                {
                    DataTable dt = GetDataTableFromGrid(EfficiencyDatagrid);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Add UploadDate column if not exists
                        if (!dt.Columns.Contains("UploadDate"))
                            dt.Columns.Add("UploadDate", typeof(DateTime));

                        // Fill with current date/time
                        foreach (DataRow row in dt.Rows)
                        {
                            row["UploadDate"] = DateTime.Now;
                        }

                        // 1. Sync schema before bulk insert
                        SyncTableColumns("TBL_ProdEff_Graph_DirectEfficiency_TN", dt);

                        // 2. Bulk insert into SQL Server
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                            {
                                bulkCopy.DestinationTableName = "TBL_ProdEff_Graph_DirectEfficiency_TN";

                                // Map columns automatically (column names must match)
                                foreach (DataColumn col in dt.Columns)
                                {
                                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                                }

                                bulkCopy.WriteToServer(dt);
                            }
                        }

                        MessageBox.Show("Toner direct efficiency data uploaded successfully!",
                                        "Notification",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error uploading: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                TemplateDropdownList.Text = "";
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                EfficiencyDatagrid.DataSource = null;

            }
            else if (TemplateDropdownList.Text == "Semi-Direct Rate")
            {
                //Delete the previous upload data within a month
                DeleteEfficiencyPreviousUpload();

                //---------------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<SemiDirectEfficiency_Class>().Table("TBL_ProdEff_SemiDirectEfficiency");

                List<SemiDirectEfficiency_Class> SemiDirectEfficiency = EfficiencyDatagrid.DataSource as List<SemiDirectEfficiency_Class>;

                if (SemiDirectEfficiency != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(SemiDirectEfficiency);
                    }
                }

                //---------------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<SemiDirectEfficiency_Class>().Table("TBL_ProdEff_SemiDirectEfficiency_Contributors");

                List<SemiDirectEfficiency_Class> SemiDirectEfficiency_Contributor = EfficiencyDatagrid.DataSource as List<SemiDirectEfficiency_Class>;

                if (SemiDirectEfficiency_Contributor != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(SemiDirectEfficiency_Contributor);
                    }
                }

                //---------------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<SemiDirectEfficiency_Class>().Table("TBL_ProdEff_SemiDirectEfficiency_WC");

                List<SemiDirectEfficiency_Class> SemiDirectEfficiency_WC = EfficiencyDatagrid.DataSource as List<SemiDirectEfficiency_Class>;

                if (SemiDirectEfficiency_WC != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(SemiDirectEfficiency_WC);
                    }
                }

                //---------------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<SemiDirectEfficiency_Class>().Table("TBL_ProdEff_SemiDirectEfficiency_CC");

                List<SemiDirectEfficiency_Class> SemiDirectEfficiency_CC = EfficiencyDatagrid.DataSource as List<SemiDirectEfficiency_Class>;

                if (SemiDirectEfficiency_CC != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(SemiDirectEfficiency_CC);
                    }
                }

                //---------------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<SemiDirectEfficiency_Class>().Table("TBL_ProdEff_SemiDirectEfficiency_Process");

                List<SemiDirectEfficiency_Class> SemiDirectEfficiency_Process = EfficiencyDatagrid.DataSource as List<SemiDirectEfficiency_Class>;

                if (SemiDirectEfficiency_Process != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(SemiDirectEfficiency_Process);
                    }
                }

                //---------------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<SemiDirectEfficiency_Class>().Table("TBL_ProdEff_SemiDirectEfficiency_Manpower");

                List<SemiDirectEfficiency_Class> SemiDirectEfficiency_Manpower = EfficiencyDatagrid.DataSource as List<SemiDirectEfficiency_Class>;

                if (SemiDirectEfficiency_Manpower != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(SemiDirectEfficiency_Manpower);
                    }
                }

                DeleteEfficiencyTableEmptyRow(); //After uploading this function will delete empty row 

                MessageBox.Show("Semi-Direct Rate data uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //this.Close();

            }
            else if (TemplateDropdownList.Text == "Semi-Direct Rate Graph")
            {
                //Delete Previous upload
                DeleteEfficiencyGraphPreviousUpload();
                DeleteEfficiencyOverallResultGraphPreviousUpload();

                DapperPlusManager.Entity<SemiDirect_Graph_Class>().Table("TBL_ProdEff_Graph_SemiDirectRate");

                List<SemiDirect_Graph_Class> SemiDirectEfficiencyGraph = EfficiencyDatagrid.DataSource as List<SemiDirect_Graph_Class>;

                if (SemiDirectEfficiencyGraph != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(SemiDirectEfficiencyGraph);
                    }
                }

                //----------------------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<SemiDirect_Graph_Class>().Table("TBL_ProdEff_Graph_SemiDirectRate_OverallResult");

                List<SemiDirect_Graph_Class> SemiDirectEfficiencyOverall = EfficiencyDatagrid.DataSource as List<SemiDirect_Graph_Class>;

                if (SemiDirectEfficiencyOverall != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(SemiDirectEfficiencyOverall);
                    }
                }


                
                MessageBox.Show("Semi-direct rate graph data uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //this.Close();
                TemplateDropdownList.Text = "";
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                EfficiencyDatagrid.DataSource = null;

            }
            else if (TemplateDropdownList.Text == "Total Loss Rate")
            {
                //Delete the previous upload data within a month
                DeleteEfficiencyPreviousUpload();

                //------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<LossRate_Class>().Table("TBL_ProdEff_LossRate");

                List<LossRate_Class> LossRate = EfficiencyDatagrid.DataSource as List<LossRate_Class>;

                if (LossRate != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(LossRate);
                    }
                }

                //------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<LossRate_Class>().Table("TBL_ProdEff_LossRate_Contributor");

                List<LossRate_Class> LossRate_Contributor = EfficiencyDatagrid.DataSource as List<LossRate_Class>;

                if (LossRate_Contributor != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(LossRate_Contributor);
                    }
                }

                //------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<LossRate_Class>().Table("TBL_ProdEff_LossRate_WC");

                List<LossRate_Class> LossRate_WC= EfficiencyDatagrid.DataSource as List<LossRate_Class>;

                if (LossRate_WC != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(LossRate_WC);
                    }
                }

                //------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<LossRate_Class>().Table("TBL_ProdEff_LossRate_CC");

                List<LossRate_Class> LossRate_CC = EfficiencyDatagrid.DataSource as List<LossRate_Class>;

                if (LossRate_CC != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(LossRate_CC);
                    }
                }

                //------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<LossRate_Class>().Table("TBL_ProdEff_LossRate_Process");

                List<LossRate_Class> LossRate_Process = EfficiencyDatagrid.DataSource as List<LossRate_Class>;

                if (LossRate_Process != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(LossRate_Process);
                    }
                }

                //------------------------------------------------------------------------------------------

                DapperPlusManager.Entity<LossRate_Class>().Table("TBL_ProdEff_LossRate_Loss");

                List<LossRate_Class> LossRate_Loss = EfficiencyDatagrid.DataSource as List<LossRate_Class>;

                if (LossRate_Loss != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(LossRate_Loss);
                    }
                }

                DeleteEfficiencyTableEmptyRow(); //After uploading this function will delete empty row 

                MessageBox.Show("Total Loss Rate data uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //this.Close();
            }
            else if (TemplateDropdownList.Text == "Total Loss Rate Graph")
            {
                //Delete Previous upload
                DeleteEfficiencyGraphPreviousUpload();
                DeleteEfficiencyOverallResultGraphPreviousUpload();


                //Insert Total Loss Rate Overall Result Data 
                DapperPlusManager.Entity<LossRate_Graph_Class>().Table("TBL_ProdEff_Graph_TotalLossRate");

                List<LossRate_Graph_Class> LossRateGraph = EfficiencyDatagrid.DataSource as List<LossRate_Graph_Class>;

                if (LossRateGraph != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(LossRateGraph);
                    }
                }

                //----------------------------------------------------------------------------------------------------------

                //Insert Total Loss Rate Overall Result Data 
                DapperPlusManager.Entity<LossRate_Graph_Class>().Table("TBL_ProdEff_Graph_TotalLossRate_OverallResult");

                List<LossRate_Graph_Class> LossRateGraphOverall = EfficiencyDatagrid.DataSource as List<LossRate_Graph_Class>;

                if (LossRateGraphOverall != null)
                {
                    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                    {
                        db.BulkInsert(LossRateGraphOverall);
                    }
                }

                MessageBox.Show("Total loss rate graph data uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //this.Close();
                TemplateDropdownList.Text = "";
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                EfficiencyDatagrid.DataSource = null;

            }
            else if (TemplateDropdownList.Text == "Efficiency Summary")
            {
                try
                {
                    //Insert Total Efficiency
                    DapperPlusManager.Entity<EfficiencySummary_Class>().Table("TBL_ProdEff_Summary_TotalEfficiency");

                    List<EfficiencySummary_Class> EfficiencySummary = EfficiencyDatagrid.DataSource as List<EfficiencySummary_Class>;

                    if (EfficiencySummary != null)
                    {
                        using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                        {
                            db.BulkInsert(EfficiencySummary);
                        }
                    }

                    //Insert Direct Efficiency --------------------------------------->
                    DapperPlusManager.Entity<EfficiencySummary_Class>().Table("TBL_ProdEff_Summary_DirectEfficiency");

                    List<EfficiencySummary_Class> DirectEfficiencySummary = EfficiencyDatagrid.DataSource as List<EfficiencySummary_Class>;

                    if (DirectEfficiencySummary != null)
                    {
                        using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                        {
                            db.BulkInsert(DirectEfficiencySummary);
                        }
                    }

                    //Insert Semi-Direct Rate --------------------------------------->
                    DapperPlusManager.Entity<EfficiencySummary_Class>().Table("TBL_ProdEff_Summary_SemiDirectRate");

                    List<EfficiencySummary_Class> SemiDirectRateSummary = EfficiencyDatagrid.DataSource as List<EfficiencySummary_Class>;

                    if (SemiDirectRateSummary != null)
                    {
                        using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                        {
                            db.BulkInsert(SemiDirectRateSummary);
                        }
                    }

                    //Insert Total Loss Rate --------------------------------------->
                    DapperPlusManager.Entity<EfficiencySummary_Class>().Table("TBL_ProdEff_Summary_TotalLossRate");

                    List<EfficiencySummary_Class> TotalLossRate = EfficiencyDatagrid.DataSource as List<EfficiencySummary_Class>;

                    if (TotalLossRate != null)
                    {
                        using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MHMS2_DB;User Id=MH_User;Password=P@ssw0rd;"))
                        {
                            db.BulkInsert(TotalLossRate);
                        }
                    }

                    MessageBox.Show("Efficiency Summary data uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //this.Close();

                    //FilePath.Clear();
                    //TemplateDropdownList.Items.Clear();
                    //SheetDropdownList.Items.Clear();
                    //FilePath.Clear();
                    //ProdEfficiencyDatagrid.DataSource = null;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else if (TemplateDropdownList.Text == "Daily Top 3 Contributor")
            {
                foreach (DataGridViewRow row in EfficiencyDatagrid.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                      
                        SqlCommand InsertTop3ContributorMonthly = new SqlCommand("SP_InsertTop3Contributor", con);
                        InsertTop3ContributorMonthly.CommandType = CommandType.StoredProcedure;
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@Procedure", "Top 3 Daily");
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@Section", row.Cells["Section"].Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@DirectEfficiency", row.Cells["Direct Efficiency"].Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@DEResult", row.Cells["DE Result"].Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@SemiDirectEfficiency", row.Cells["Semi Direct Rate"].Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@SDRResult", row.Cells["SDR Result"].Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@TotalLossRate", row.Cells["Total Loss Rate"].Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@TLRResult", row.Cells["TLR Result"].Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@Rank", row.Cells["Rank"].Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@Date", row.Cells["Day"].Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@Month", DBNull.Value);
                        InsertTop3ContributorMonthly.ExecuteNonQuery();
                        con.Close();
                    }
                }

                MessageBox.Show("Top 3 contributor daily uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //this.Close();
                TemplateDropdownList.Text = "";
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                EfficiencyDatagrid.DataSource = null;

            }
            else if (TemplateDropdownList.Text == "Monthly Top 3 Contributor")
            {

                foreach (DataGridViewRow row in EfficiencyDatagrid.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlCommand InsertTop3ContributorMonthly = new SqlCommand("SP_InsertTop3Contributor", con);
                        InsertTop3ContributorMonthly.CommandType = CommandType.StoredProcedure;
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@Procedure", "Top 3 Monthly");
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@Section", row.Cells["Section"].Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@DirectEfficiency", row.Cells["Direct Efficiency"].Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@SemiDirectEfficiency", row.Cells["Semi Direct Rate"].Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@TotalLossRate", row.Cells["Total Loss Rate"].Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@Rank", row.Cells["Rank"].Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@Date", DBNull.Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@DEResult", DBNull.Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@SDRResult", DBNull.Value);
                        InsertTop3ContributorMonthly.Parameters.AddWithValue("@TLRResult", DBNull.Value);
                        InsertTop3ContributorMonthly.ExecuteNonQuery();
                        con.Close();

                    }
                }

                MessageBox.Show("Top 3 contributor monthly uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //this.Close();
                TemplateDropdownList.Text = "";
                FilePath.Text = "";
                SheetDropdownList.Text = "";
                EfficiencyDatagrid.DataSource = null;

            }

        }

        /// <summary>
        /// Convert DataGridView to DataTable
        /// </summary>
        private DataTable GetDataTableFromGrid(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible)
                    dt.Columns.Add(col.HeaderText ?? col.Name);
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow dRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.OwningColumn.HeaderText ?? cell.OwningColumn.Name] = cell.Value ?? DBNull.Value;
                    }
                    dt.Rows.Add(dRow);
                }
            }

            return dt;
        }

        /// <summary>
        /// Ensure SQL table has all DataTable columns (add if missing)
        /// </summary>
        private void SyncTableColumns(string tableName, DataTable dt)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Get existing columns from database
                DataTable schemaTable = con.GetSchema("Columns", new string[] { null, null, tableName, null });
                var existingCols = schemaTable.AsEnumerable()
                                              .Select(r => r.Field<string>("COLUMN_NAME"))
                                              .ToList();

                foreach (DataColumn col in dt.Columns)
                {
                    if (!existingCols.Contains(col.ColumnName))
                    {
                        string sql = $"ALTER TABLE {tableName} ADD [{col.ColumnName}] NVARCHAR(MAX)";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }


        private void SectionDropdownlist_DropDown(object sender, EventArgs e)
        {
            //LoadSection();
        }

        //public void LoadSection()
        //{
        //    // Check Connection status -> Open the connection if the connection is closed
        //    if (con2.State == ConnectionState.Closed)
        //    {
        //        con2.Open();
        //    }

        //    // -> SQL query to select User Account
        //    SqlCommand LoadSection = new SqlCommand("SP_LoadSection", con2);
        //    LoadSection.CommandType = CommandType.StoredProcedure;
        //    LoadSection.Parameters.AddWithValue("@Procedure", "SelectAllSections");
        //    SqlDataAdapter sda = new SqlDataAdapter(LoadSection);
        //    DataSet ds = new DataSet();
        //    sda.Fill(ds);
        //    LoadSection.ExecuteNonQuery();
        //    con.Close();

        //    SectionDropdownlist.DataSource = ds.Tables[0];
        //    SectionDropdownlist.DisplayMember = ds.Tables[0].Columns[0].ToString();
        //    //SectionDropdown.ValueMember = "";
        //}


        private void ProdEfficiencyDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in EfficiencyDatagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }


      
    }
}
