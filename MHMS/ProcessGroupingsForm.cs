using MHMS.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class ProcessGroupingsForm : Form
    {

        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2_ACTUAL"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);

        public ProcessGroupingsForm()
        {
            InitializeComponent();
        }

        private void ProcessGroupingsForm_Load(object sender, EventArgs e)
        {

        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            if (ProcessName.Text == "")
            {
                MessageBox.Show("Please type process name.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                // Check connection status -> if close connection will open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // -> SQL query to insert Section to Approver setting
                SqlCommand AddSectionToApproverSetting = new SqlCommand("SP_InsertProcessGroupingDetails", con);
                AddSectionToApproverSetting.CommandType = CommandType.StoredProcedure;
                AddSectionToApproverSetting.Parameters.AddWithValue("@ProcessName", ProcessName.Text);
                AddSectionToApproverSetting.Parameters.AddWithValue("@WorkCenter", "Work center");
                AddSectionToApproverSetting.Parameters.AddWithValue("@Action", "❌");
                AddSectionToApproverSetting.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Process name added Successfuly!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SelectProcessGroupingDetails();
            }
        }


        private void SelectProcessGroupingDetails()
        {
            // Check connection status -> if close connection will open
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
          
            SqlCommand SelectProcessGroupingDetails = new SqlCommand("SP_SelectProcessGroupingDetails", con);
            SelectProcessGroupingDetails.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(SelectProcessGroupingDetails);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ProcessGroupingDatagrid.DataSource = dt;
            con.Close();
        }








    }
}
