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
    public partial class UpdateWorkingHours : Form
    {

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);

        public UpdateWorkingHours()
        {
            InitializeComponent();
        }

        private void UpdateWorkingHours_Load(object sender, EventArgs e)
        {
            AddYears();
        }

        private void AddYears()
        {
            var currentYear = DateTime.Today.Year;
            for (int i = 3; i >= 0; i--)
            {
                // Now just add an entry that's the current year minus the counter
                FiscalYearDropdown.Items.Add((currentYear - i).ToString());
            }
        }

        private void FiscalYearDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectWorkingDaysAndHours();
        }

        private void SelectWorkingDaysAndHours()
        {
            con.Open();
            SqlCommand SelectWorkingDaysAndHours = new SqlCommand("SP_SelectWorkingDaysAndHours", con);
            SelectWorkingDaysAndHours.CommandType = CommandType.StoredProcedure;
            SelectWorkingDaysAndHours.Parameters.AddWithValue("@FiscalYear", FiscalYearDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectWorkingDaysAndHours);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            WorkingDaysHoursDatagrid.DataSource = dt;
            con.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (Month.Text == "")
            {
                MessageBox.Show("Please select month.", "Remiders!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (WorkingDays.Text == "")
            {
                MessageBox.Show("Please type working days.", "Remiders!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (WorkingHours.Text == "")
            {
                MessageBox.Show("Please type working hours.", "Remiders!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // ---> Update query
                SqlCommand UpdateStandardMH = new SqlCommand("SP_UpdateWorkingDaysAndHours", con);
                UpdateStandardMH.CommandType = CommandType.StoredProcedure;
                UpdateStandardMH.Parameters.AddWithValue("@Month", Month.Text);
                UpdateStandardMH.Parameters.AddWithValue("@WorkingDays", WorkingDays.Text);
                UpdateStandardMH.Parameters.AddWithValue("@WorkingHours", WorkingHours.Text);
                UpdateStandardMH.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString());
                UpdateStandardMH.Parameters.AddWithValue("@FiscalYear", FiscalYearDropdown.Text);
                UpdateStandardMH.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Updated successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SelectWorkingDaysAndHours();

                Month.Clear();
                WorkingDays.Clear();
                WorkingHours.Clear();
            }
        }

        private void WorkingDaysHoursDatagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Month.Text = WorkingDaysHoursDatagrid.Rows[e.RowIndex].Cells["Month"].Value.ToString();
            WorkingDays.Text = WorkingDaysHoursDatagrid.Rows[e.RowIndex].Cells["Working Days"].Value.ToString();
            WorkingHours.Text = WorkingDaysHoursDatagrid.Rows[e.RowIndex].Cells["Working Hours"].Value.ToString();
        }
    }
}
