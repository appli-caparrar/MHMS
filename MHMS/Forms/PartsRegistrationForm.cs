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
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace MHMS.Forms
{
    public partial class PartsRegistrationForm : Form
    {
        // SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public PartsRegistrationForm()
        {
            InitializeComponent();
        }

        private void PartsRegistrationForm_Load(object sender, EventArgs e)
        {
            
        }

        private void UploadBtn_Click(object sender, EventArgs e)
        {
            UploadPCData uploadPCData = new UploadPCData();
            uploadPCData.ShowDialog();
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            LoadPartsRegistrationData();
        }

        string SelectQuery;
        private void LoadPartsRegistrationData()
        {
            

            if (NoOfEntries.Text == "All")
            {
                // Create the SQL query to select all records from PartsRegistration
                SelectQuery = "SELECT Plant, Material AS 'Part Code', Description AS 'Part Name', VendorName AS 'Supplier Name' FROM PartsRegistration";
            }
            else
            {
                // Create the SQL query to select all records from PartsRegistration
                SelectQuery = "SELECT TOP(@Entries)Plant, Material AS 'Part Code', Description AS 'Part Name', VendorName AS 'Supplier Name' FROM PartsRegistration";
            }
          

            // Initialize a DataTable to hold the fetched data
            DataTable dt = new DataTable();


            try
            {
                con.Open();

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(SelectQuery, con))
                {
                    if (NoOfEntries.Text != "All")
                    {
                        // Add the @Entries parameter to the SQL command
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@Entries", Convert.ToInt32(NoOfEntries.Text)); // 'entries' is the number of rows to fetch
                    }
                    else
                    {}

                    // Fill the DataTable with the data from PartsRegistration table
                    dataAdapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally 
            {
                con.Close();
            }

            // Set the DataSource of the DataGridView to the DataTable
            PartsDetailDatagrid.DataSource = dt;
        }

        private void SearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Check if the Enter key is pressed
            {
                // Get the search term from the TextBox
                string searchTerm = SearchText.Text.Trim();

                // Call the method to search the data and update the DataGridView
                SearchPartsRegistrationData(searchTerm);
            }
        }

        // Method to search PartsRegistration table based on the search term
        private void SearchPartsRegistrationData(string searchTerm)
        {
            // Construct the SQL query to filter records by VendorName, Material, or Description
            string query = @"
            SELECT Plant, Material AS 'Part Code', Description AS 'Part Name', VendorName AS 'Supplier Name'
            FROM PartsRegistration
            WHERE VendorName LIKE @SearchTerm 
            OR Material LIKE @SearchTerm
            OR Description LIKE @SearchTerm";

            // Initialize a DataTable to hold the search results
            DataTable dt = new DataTable();

            
            try
            {
                con.Open();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con))
                {
                    // Add the search term as a parameter to prevent SQL injection
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                    // Fill the DataTable with the filtered data
                    dataAdapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            finally { con.Close(); }

            // Set the DataSource of the DataGridView to the filtered DataTable
            PartsDetailDatagrid.DataSource = dt;
        }

        private void SearchText_MouseLeave(object sender, EventArgs e)
        {
            // Restore placeholder if the TextBox is empty when mouse leaves
            if (string.IsNullOrEmpty(SearchText.Text))
            {
                SearchText.Text = "Enter a keyword";
                SearchText.ForeColor = Color.Gray; // Set the placeholder text color to gray
            }
        }

        private void SearchText_MouseEnter(object sender, EventArgs e)
        {
            // Clear placeholder text when the TextBox gains focus
            if (SearchText.Text == "Enter a keyword")
            {
                SearchText.Text = "";
                SearchText.ForeColor = Color.Black;
            }
        }

        private void SearchText_Leave(object sender, EventArgs e)
        {
            // Restore placeholder text when the TextBox loses focus
            if (string.IsNullOrEmpty(SearchText.Text))
            {
                SearchText.Text = "Enter a keyword";
                SearchText.ForeColor = Color.Gray;
            }
        }

        private void NoOfEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPartsRegistrationData();
        }

        private void SearchFile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Check if the Enter key is pressed
            {
                // Get the search term from the TextBox
                string searchTerm = SearchFile.Text.Trim();

                // Call the method to search the data and update the DataGridView
                SearchPartsRegistrationHistoryData(searchTerm);
            }
        }

        // Method to search PartsRegistration table based on the search term
        private void SearchPartsRegistrationHistoryData(string searchTerm)
        {
            // Construct the SQL query to filter records by VendorName, Material, or Description
            string query = @"
            SELECT FileName, Date, Time, UploadedByPIC FROM PartsRegistrationHistory
            WHERE FileName LIKE @SearchTerm 
            OR Date LIKE @SearchTerm
            OR UploadedByPIC LIKE @SearchTerm";

            // Initialize a DataTable to hold the search results
            DataTable dt = new DataTable();


            try
            {
                con.Open();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con))
                {
                    // Add the search term as a parameter to prevent SQL injection
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                    // Fill the DataTable with the filtered data
                    dataAdapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            finally { con.Close(); }

            // Set the DataSource of the DataGridView to the filtered DataTable
            UploadHistoryDatagrid.DataSource = dt;
        }

        string SelectFileHistory;
        private void LoadPartsRegistrationHistory()
        {

            if (NoOfEntries.Text == "All")
            {
                // Create the SQL query to select all records from PartsRegistration
                SelectFileHistory = "SELECT FileName AS 'File Name', Date, Time, UploadedByPIC AS 'Upload By PIC' FROM PartsRegistrationHistory ORDER BY Date, Time";
            }
            else
            {
                // Create the SQL query to select all records from PartsRegistration
                SelectFileHistory = "SELECT TOP (@Entries)FileName AS 'File Name', Date, Time, UploadedByPIC AS 'Upload By PIC' FROM PartsRegistrationHistory ORDER BY Date, Time";
            }


            // Initialize a DataTable to hold the fetched data
            DataTable dt = new DataTable();


            try
            {
                con.Open();

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(SelectFileHistory, con))
                {
                    if (NoOfEntries.Text != "All")
                    {
                        // Add the @Entries parameter to the SQL command
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@Entries", Convert.ToInt32(ShowEntriesForFile.Text)); // 'entries' is the number of rows to fetch
                    }
                    else
                    { }

                    // Fill the DataTable with the data from PartsRegistration table
                    dataAdapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                con.Close();
            }

            // Set the DataSource of the DataGridView to the DataTable
            UploadHistoryDatagrid.DataSource = dt;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check which tab is selected
            if (tabControl1.SelectedTab == PartUploadHistoryTab)
            {
                // Load data when the History tab is selected
                LoadPartsRegistrationHistory();
            }
        }

        private void SearchFile_MouseLeave(object sender, EventArgs e)
        {
            // Clear placeholder text when the TextBox gains focus
            if (SearchFile.Text == "Enter a keyword")
            {
                SearchFile.Text = "";
                SearchFile.ForeColor = Color.Black;
            }
        }

        private void SearchFile_MouseEnter(object sender, EventArgs e)
        {
            // Clear placeholder text when the TextBox gains focus
            if (SearchFile.Text == "Enter a keyword")
            {
                SearchFile.Text = "";
                SearchFile.ForeColor = Color.Black;
            }
        }

        private void SearchFile_Leave(object sender, EventArgs e)
        {
            // Restore placeholder text when the TextBox loses focus
            if (string.IsNullOrEmpty(SearchFile.Text))
            {
                SearchFile.Text = "Enter a keyword";
                SearchFile.ForeColor = Color.Gray;
            }
        }
    }
}
