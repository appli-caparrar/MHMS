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
using MHMS.Connection;

namespace MHMS
{
    public partial class AddLeadTime : Form
    {
        public AddLeadTime()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (LossFactor.Text == "Select Loss Factor")
            {
                MessageBox.Show("Please select loss factor!", "Required.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (LossFactor.Text == "")
            {
                MessageBox.Show("Please input leadtime!", "Required.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                // Example usage: Insert a new LossFactor entry
                InsertLossFactor(LossFactor.Text, Leadtime.Text, LossFactorForm.section);
            }
        }

        // Function to insert data into the LossFactor table
        public static void InsertLossFactor(string lossFactor, string leadTime, string section)
        {
            // Define the SQL query for insertion
            string query = "INSERT INTO LossFactor ([Loss Factor], LeadTime, Section) VALUES (@LossFactor, @LeadTime, @Section)";

            // Using block to ensure that the connection is disposed of properly
            using (SqlConnection conn = new SqlConnection(SQLControl.MHMS_Conn))
            {
                try
                {
                    // Open the connection
                    conn.Open();

                    // Create a SqlCommand object to execute the query
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@LossFactor", lossFactor);
                        cmd.Parameters.AddWithValue("@LeadTime", leadTime);
                        cmd.Parameters.AddWithValue("@Section", section);

                        // Execute the command
                        cmd.ExecuteNonQuery();

                        // Output the number of affected rows
                        MessageBox.Show($"Inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    // Output any errors
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddLossFactorAndLeadTime_Load(object sender, EventArgs e)
        {
            LoadLossFactorData();
        }

        private void LoadLossFactorData()
        {
            // SQL query to select Loss Factor from the LossFactor table
            string query = "SELECT [Loss Factor] FROM LossFactor";

            // Using block to ensure that resources are cleaned up properly
            using (SqlConnection conn = new SqlConnection(SQLControl.MHMS_Conn))
            {
                try
                {
                    // Open the database connection
                    conn.Open();

                    // Create a SqlDataAdapter to execute the query and fill a DataTable
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Clear any previous data in the ComboBox
                    LossFactor.Items.Clear();

                    // Add the default text as the first item
                    LossFactor.Items.Add("Select Loss Factor");

                    // Loop through each row in the DataTable and add to the ComboBox
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Add the Loss Factor value to the ComboBox
                        LossFactor.Items.Add(row["Loss Factor"].ToString());
                    }

                    // Optionally, set the default selection to the "Select Loss Factor" item
                    LossFactor.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    // Show any errors in a message box
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
