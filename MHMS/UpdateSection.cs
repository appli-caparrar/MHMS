using MHMS.Connection;
using MHMS.Forms;
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
    public partial class UpdateSection : Form
    {
        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public UpdateSection()
        {
            InitializeComponent();
        }

        string SectionID;
        string SectionName;

        //====================================================================================================================>>>>>>>>>>>>

        private void UpdateSection_Load(object sender, EventArgs e)
        {
            SectionName = ApproverSettingForm.userSection;
            Section.Text = SectionName;
            TypeDropdown.Text = ApproverSettingForm.TypeValue;
            SectionID = ApproverSettingForm.ID;

            
        }

        //====================================================================================================================>>>>>>>>>>>>

        // Update or Edit Section function
        private void UpdateSectionName()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // ---> Update query
            SqlCommand UpdateSection = new SqlCommand("SP_UpdateSectionInApproverSetting", con);
            UpdateSection.CommandType = CommandType.StoredProcedure;
            UpdateSection.Parameters.AddWithValue("@ID", SectionID);
            UpdateSection.Parameters.AddWithValue("@Section", Section.Text);
            UpdateSection.ExecuteNonQuery();
            con.Close();

            UpdateUserSection(); //When update the section in approver setting the section of user in user account also updated

            MessageBox.Show("Section name updated successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void UpdateUserSection()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // ---> Update query
            SqlCommand UpdateUserSection = new SqlCommand("SP_UpdateUserSection", con);
            UpdateUserSection.CommandType = CommandType.StoredProcedure;
            UpdateUserSection.Parameters.AddWithValue("@OldSectionName", SectionName);
            UpdateUserSection.Parameters.AddWithValue("@NewSection", Section.Text);
            UpdateUserSection.ExecuteNonQuery();
            con.Close();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            UpdateSectionName();
            this.Close();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void Section_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                UpdateSectionName();
                this.Close();
            }
        }

        //====================================================================================================================>>>>>>>>>>>>
    }
}
