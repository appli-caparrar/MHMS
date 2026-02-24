using MHMS.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS.Forms
{
    public partial class ManpowerForecastForm : Form
    {
        //Connection String
        //static string MHMS_Conn = System.Configuration.ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public ManpowerForecastForm()
        {
            InitializeComponent();
        }

        private void ManpowerForecastForm_Load(object sender, EventArgs e)
        {
            
        }

        //Load Section in combobox
        public void LoadProdSection()
        {
            // Check Connection status -> Open the connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand LoadSection = new SqlCommand("SP_LoadSection", con);
            LoadSection.CommandType = CommandType.StoredProcedure;
            LoadSection.Parameters.AddWithValue("@Procedure", "SelectAllProdSections");
            SqlDataAdapter sda = new SqlDataAdapter(LoadSection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadSection.ExecuteNonQuery();
            con.Close();

            SummarySectionDropdown.DataSource = ds.Tables[0];
            SummarySectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            SummarySectionDropdown.ValueMember = "Section";
        }// <---- end


        public void LoadAllSection()
        {
            // Check Connection status -> Open the connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand LoadSection = new SqlCommand("SP_LoadSection", con);
            LoadSection.CommandType = CommandType.StoredProcedure;
            LoadSection.Parameters.AddWithValue("@Procedure", "SelectAllSections");
            SqlDataAdapter sda = new SqlDataAdapter(LoadSection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadSection.ExecuteNonQuery();
            con.Close();

            CompResSectionDropdown.DataSource = ds.Tables[0];
            CompResSectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            CompResSectionDropdown.ValueMember = "Section";
        }// <---- end

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (this.Width <= 1295)
            //{
            //    timer1.Start();
            //    SummaryControlPanel.Size = new Size(SummaryControlPanel.Width, 177);
            //    DataTypeDropdownPanel.Location = new Point(130, 7);
            //    SectionDropdownPanel.Location = new Point(130, 48);
            //    TargetTypeDropdownPanel.Location = new Point(532, 7);
            //    MonthDropdownPanel.Location = new Point(532, 48);
            //    FiscalYearDropdownpanel.Location = new Point(130, 89);
            //    ApplicationStatusDropdownPanel.Location = new Point(532, 89);
            //    SummaryGenerateButton.Location = new Point(799, 130);
            //    ShowEntriesPanel.Location = new Point(7, 136);

            //}
            //else
            //{
            //    timer1.Stop();
            //    Default Location
            //    SummaryControlPanel.Size = new Size(1623, 177);
            //    DataTypeDropdownPanel.Location = new Point(7, 6);
            //    SectionDropdownPanel.Location = new Point(7, 47);
            //    TargetTypeDropdownPanel.Location = new Point(409, 6);
            //    MonthDropdownPanel.Location = new Point(409, 47);
            //    FiscalYearDropdownpanel.Location = new Point(812, 6);
            //    ApplicationStatusDropdownPanel.Location = new Point(812, 47);
            //    SummaryGenerateButton.Location = new Point(1215, 47);
            //    ShowEntriesPanel.Location = new Point(7, 136);

            //    Dashboard.MaximizeIsClicked = false;
            //}
        }

        private void SummaryControlPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {

        }

        Color DefaultBgColor = Color.FromArgb(47, 69, 180);
        private void SummaryButton_Click(object sender, EventArgs e)
        {
            SummaryButton.BackColor = Color.FromArgb(21, 147, 149);
            ComparisonResultButton.BackColor = DefaultBgColor;
            BenchmarkSummaryButton.BackColor = DefaultBgColor;

            SummaryControlPanel.Visible = true;
            ComparisonResultControlPanel.Visible = false;
            BenchmarkSummaryControlPanel.Visible = false;


            if (this.Width <= 1295)
            {
                SummaryControlPanel.Size = new Size(SummaryControlPanel.Width, 177);
                DataTypeDropdownPanel.Location = new Point(130, 7);
                SectionDropdownPanel.Location = new Point(130, 48);
                TargetTypeDropdownPanel.Location = new Point(532, 7);
                MonthDropdownPanel.Location = new Point(532, 48);
                FiscalYearDropdownpanel.Location = new Point(130, 89);
                ApplicationStatusDropdownPanel.Location = new Point(532, 89);
                SummaryGenerateButton.Location = new Point(799, 130);
                ShowEntriesPanel.Location = new Point(7, 136);

            }
            else
            {
                //Default Location
                SummaryControlPanel.Size = new Size(1623, 177);
                DataTypeDropdownPanel.Location = new Point(7, 6);
                SectionDropdownPanel.Location = new Point(7, 47);
                TargetTypeDropdownPanel.Location = new Point(404, 6);
                MonthDropdownPanel.Location = new Point(404, 47);
                FiscalYearDropdownpanel.Location = new Point(802, 6);
                ApplicationStatusDropdownPanel.Location = new Point(802, 47);
                SummaryGenerateButton.Location = new Point(1200, 47);
                ShowEntriesPanel.Location = new Point(7, 136);


            }

        }

        private void ComparisonResultButton_Click(object sender, EventArgs e)
        {
            SummaryButton.BackColor = DefaultBgColor;
            ComparisonResultButton.BackColor = Color.FromArgb(21, 147, 149);
            BenchmarkSummaryButton.BackColor = DefaultBgColor;

            SummaryControlPanel.Visible = false;
            ComparisonResultControlPanel.Visible = true;
            BenchmarkSummaryControlPanel.Visible = false;
        }

        private void BenchmarkSummaryButton_Click(object sender, EventArgs e)
        {
            SummaryButton.BackColor = DefaultBgColor;
            ComparisonResultButton.BackColor = DefaultBgColor;
            BenchmarkSummaryButton.BackColor = Color.FromArgb(21, 147, 149);

            SummaryControlPanel.Visible = false;
            ComparisonResultControlPanel.Visible = false;
            BenchmarkSummaryControlPanel.Visible = true;
        }

        private void ManpowerForecastForm_Resize(object sender, EventArgs e)
        {
            if (this.Width <= 1295)
            {
                SummaryControlPanel.Size = new Size(SummaryControlPanel.Width, 177);
                DataTypeDropdownPanel.Location = new Point(130, 7);
                SectionDropdownPanel.Location = new Point(130, 48);
                TargetTypeDropdownPanel.Location = new Point(532, 7);
                MonthDropdownPanel.Location = new Point(532, 48);
                FiscalYearDropdownpanel.Location = new Point(130, 89);
                ApplicationStatusDropdownPanel.Location = new Point(532, 89);
                SummaryGenerateButton.Location = new Point(799, 130);
                ShowEntriesPanel.Location = new Point(7, 136);

            }
            else
            {
                //Default Location
                SummaryControlPanel.Size = new Size(1623, 177);
                DataTypeDropdownPanel.Location = new Point(7, 6);
                SectionDropdownPanel.Location = new Point(7, 47);
                TargetTypeDropdownPanel.Location = new Point(404, 6);
                MonthDropdownPanel.Location = new Point(404, 47);
                FiscalYearDropdownpanel.Location = new Point(802, 6);
                ApplicationStatusDropdownPanel.Location = new Point(802, 47);
                SummaryGenerateButton.Location = new Point(1200, 47);
                ShowEntriesPanel.Location = new Point(7, 136);

                
            }
        }

        private void SummarySectionDropdown_DropDown(object sender, EventArgs e)
        {
            LoadProdSection();
        }

        private void CompResSectionDropdown_DropDown(object sender, EventArgs e)
        {
            LoadAllSection();
        }
    }
}
