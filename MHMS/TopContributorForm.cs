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

namespace MHMS
{
    public partial class TopContributorForm : Form
    {
        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);
        SqlConnection con2 = new SqlConnection(SQLControl.MHMS_Conn);

        public TopContributorForm()
        {
            InitializeComponent();
        }

        private void TopContributorForm_Load(object sender, EventArgs e)
        {
            SectionDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");
            MonthDropdown.Text = DateTime.Now.ToString("MMMM");
        }



        private void GetProductionEfficiencyTop3ContributorDaily()
        {
            if (Procedure == "GetTop3DirectEfficiency")
            {
                con.Open();
                SqlCommand SelectTop3ContributorDaily = new SqlCommand("SP_SelectTop3ContributorDaily", con);
                SelectTop3ContributorDaily.CommandType = CommandType.StoredProcedure;
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Procedure", "GetTop3DirectEfficiency");
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Date", dateTimePicker.Value.Date);
                SqlDataAdapter sda = new SqlDataAdapter(SelectTop3ContributorDaily);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Top3ContributorDaily.DataSource = dt;
                con.Close();
            }
            else if (Procedure == "GetTop3SemiDirect")
            {
                con.Open();
                SqlCommand SelectTop3ContributorDaily = new SqlCommand("SP_SelectTop3ContributorDaily", con);
                SelectTop3ContributorDaily.CommandType = CommandType.StoredProcedure;
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Procedure", "GetTop3SemiDirect");
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Date", dateTimePicker.Value.Date);
                SqlDataAdapter sda = new SqlDataAdapter(SelectTop3ContributorDaily);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Top3ContributorDaily.DataSource = dt;
                con.Close();
            }
            else if (Procedure == "GetTop3TotalLossRate")
            {
                con.Open();
                SqlCommand SelectTop3ContributorDaily = new SqlCommand("SP_SelectTop3ContributorDaily", con);
                SelectTop3ContributorDaily.CommandType = CommandType.StoredProcedure;
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Procedure", "GetTop3TotalLossRate");
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Date", dateTimePicker.Value.Date);
                SqlDataAdapter sda = new SqlDataAdapter(SelectTop3ContributorDaily);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Top3ContributorDaily.DataSource = dt;
                con.Close();
            }
        }


        private void GetProductionEfficiencyTop3ContributorMonthly()
        {
            if (Procedure == "GetTop3DirectEfficiency")
            {
                con.Open();
                SqlCommand SelectTop3ContributorDaily = new SqlCommand("SP_SelectTop3ContributorMonthly", con);
                SelectTop3ContributorDaily.CommandType = CommandType.StoredProcedure;
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Procedure", "GetTop3DirectEfficiency");
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Month", MonthDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectTop3ContributorDaily);
                DataTable dt = new DataTable(); 
                sda.Fill(dt);
                Top3ContributorMonthly.DataSource = dt;
                con.Close();
            }
            else if (Procedure == "GetTop3SemiDirect")
            {
                con.Open();
                SqlCommand SelectTop3ContributorDaily = new SqlCommand("SP_SelectTop3ContributorMonthly", con);
                SelectTop3ContributorDaily.CommandType = CommandType.StoredProcedure;
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Procedure", "GetTop3SemiDirect");
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Month", MonthDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectTop3ContributorDaily);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Top3ContributorMonthly.DataSource = dt;
                con.Close();
            }
            else if (Procedure == "GetTop3TotalLossRate")
            {
                con.Open();
                SqlCommand SelectTop3ContributorDaily = new SqlCommand("SP_SelectTop3ContributorMonthly", con);
                SelectTop3ContributorDaily.CommandType = CommandType.StoredProcedure;
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Procedure", "GetTop3TotalLossRate");
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SelectTop3ContributorDaily.Parameters.AddWithValue("@Month", MonthDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectTop3ContributorDaily);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Top3ContributorMonthly.DataSource = dt;
                con.Close();
            }
        }

     
        string Procedure;

        private void ShowTop3Contributor()
        {
            if (MonthDropdown.Text == "- - Select Month - -")
            {
                MessageBox.Show("Please select month", "MHMS information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MonthDropdown.Select();
            }
            else if (SectionDropdown.Text == "- - Select Section - -")
            {
                MessageBox.Show("Please select section", "MHMS information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                SectionDropdown.Select();
            }
            else
            {
                Top3ContributorLabel.Text = "Top 3 Contributor Daily " + "[" + CategoryDropdown.Text + "]";
                Top3ContributorLabelMonthly.Text = "Top 3 Contributor Monthly " + "[" + CategoryDropdown.Text + "]";

                if (CategoryDropdown.Text == "Direct Efficiency")
                {
                    Procedure = "GetTop3DirectEfficiency";
                }
                else if (CategoryDropdown.Text == "Semi-direct Efficiency")
                {
                    Procedure = "GetTop3SemiDirect";
                }
                else if (CategoryDropdown.Text == "Total Loss Rate")
                {
                    Procedure = "GetTop3TotalLossRate";
                }

                //Get top 3 contributor daily
                GetProductionEfficiencyTop3ContributorDaily();

                //Get top 3 contributor monthly
                GetProductionEfficiencyTop3ContributorMonthly();
            }
        }

        private void CategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTop3Contributor();
        }

        private void MonthDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SectionDropdown_DropDown(object sender, EventArgs e)
        {
            LoadSection();
        }

        public void LoadSection()
        {
            try
            {
                // Ensure connection is open
                con2.Open();

                using (SqlCommand loadSectionCmd = new SqlCommand("SP_LoadSection", con2))
                {
                    loadSectionCmd.CommandType = CommandType.StoredProcedure;
                    loadSectionCmd.Parameters.AddWithValue("@Procedure", "SelectAllProdSections");

                    using (SqlDataAdapter sda = new SqlDataAdapter(loadSectionCmd))
                    {
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                         
                        // Check if dataset contains data
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            SectionDropdown.DataSource = ds.Tables[0];
                            SectionDropdown.DisplayMember = "Section"; // Use explicit column name
                            SectionDropdown.ValueMember = "Section"; // Use explicit column name
                        }
                        else
                        {
                            SectionDropdown.DataSource = null;
                            MessageBox.Show("No sections available.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sections: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure connection is closed
                con2.Close();
            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ShowTop3Contributor();
        }

        private void ShowBtn_Click(object sender, EventArgs e)
        {
            ShowTop3Contributor();
        }
    }
}
