using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using MHMS.Connection;

namespace MHMS.Forms
{
    public partial class ApplicationForm : Form
    {
        
        //Connection String
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2_ACTUAL"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);

        public ApplicationForm()
        {
            InitializeComponent();
        }

        //===================================================<break>======================================================//

        private void ApplicationForm_Load(object sender, EventArgs e)
        {
            //Change column header back color and fore color
            ApplicationDataGrid.EnableHeadersVisualStyles = false;
            //PartsLossDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(72, 141, 218);
            ApplicationDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(86, 119, 157);
            ApplicationDataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            EffectivityDate();

            // Initialize date pickers as blank
            InitDatePicker(EffectivityDateTimePicker);


            AddCategoryPerApplication();

            SubmitButton.Enabled = false;

          
            if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
            {
                UploadMasterDataBtn.Visible = true;
            }
            else
            {
                UploadMasterDataBtn.Visible = false;
            }

            //string test = "January " + DateTime.Now.Year;

            //MessageBox.Show(Regex.Replace(test, @"[\d-]", string.Empty));
        }

        // Helper to initialize blank date picker
        private void InitDatePicker(DateTimePicker picker)
        {
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = " ";

        }

        private void EffectivityDate()
        {
            EffectivityDateTimePicker.Value = DateTime.Today;
            MPFEffectivityDateTimePicker.Value = DateTime.Now;
        }

        //===================================================<break>======================================================//
        private void AddMonthYear()
        {
            // Clear existing items in ComboBox (if any)
            MonthToOpenDropdown.Items.Clear();

            // Add months from April to December
            MonthToOpenDropdown.Items.Add("April");
            MonthToOpenDropdown.Items.Add("May");
            MonthToOpenDropdown.Items.Add("June");
            MonthToOpenDropdown.Items.Add("July");
            MonthToOpenDropdown.Items.Add("August");
            MonthToOpenDropdown.Items.Add("September");
            MonthToOpenDropdown.Items.Add("October");
            MonthToOpenDropdown.Items.Add("November");
            MonthToOpenDropdown.Items.Add("December");

            // Add months from January to March
            MonthToOpenDropdown.Items.Add("January");
            MonthToOpenDropdown.Items.Add("February");
            MonthToOpenDropdown.Items.Add("March");
        }

        private void RemoveMonthYear()
        {

            //Remove
            MonthToOpenDropdown.Items.Remove("January " + DateTime.Now.Year);
            MonthToOpenDropdown.Items.Remove("February " + DateTime.Now.Year);
            MonthToOpenDropdown.Items.Remove("March " + DateTime.Now.Year);
            MonthToOpenDropdown.Items.Remove("April " + DateTime.Now.Year);
            MonthToOpenDropdown.Items.Remove("May " + DateTime.Now.Year);
            MonthToOpenDropdown.Items.Remove("June " + DateTime.Now.Year);
            MonthToOpenDropdown.Items.Remove("July " + DateTime.Now.Year);
            MonthToOpenDropdown.Items.Remove("August " + DateTime.Now.Year);
            MonthToOpenDropdown.Items.Remove("September " + DateTime.Now.Year);
            MonthToOpenDropdown.Items.Remove("October " + DateTime.Now.Year);
            MonthToOpenDropdown.Items.Remove("November " + DateTime.Now.Year);
            MonthToOpenDropdown.Items.Remove("December " + DateTime.Now.Year);

        }


        private void AddCategoryPerApplication()
        {
            if (ApplicationFormTypeDropdown.Text == "COPQ")
            {
                
            }
            else if (ApplicationFormTypeDropdown.Text == "ST")
            {
                //Remove
                CategoryDropdown.Items.Remove("Annual ST Change");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form - No BIL Approval");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form");
                CategoryDropdown.Items.Remove("MH New ST Model List Form");

                //Remove
                CategoryDropdown.Items.Remove("Work Center New");
                CategoryDropdown.Items.Remove("Work Center Revision");
                CategoryDropdown.Items.Remove("Work Center Deletion");
                CategoryDropdown.Items.Remove("Cost Center New");
                CategoryDropdown.Items.Remove("Cost Center Revision");
                CategoryDropdown.Items.Remove("Cost Center Deletion");

                CategoryDropdown.Items.Remove("Manpower/Man-hour");
                CategoryDropdown.Items.Remove("Standard Time (ST mins)");
                CategoryDropdown.Items.Remove("Linestop/Loss Man-hour/Loss Factor");

                //Add
                CategoryDropdown.Items.Add("Annual ST Change");
                CategoryDropdown.Items.Add("MH Change ST Model List Form - No BIL Approval");
                CategoryDropdown.Items.Add("MH Change ST Model List Form");
                CategoryDropdown.Items.Add("MH New ST Model List Form");

            }
            else if (ApplicationFormTypeDropdown.Text == "WC/CC")
            {
                //Remove
                CategoryDropdown.Items.Remove("Work Center New");
                CategoryDropdown.Items.Remove("Work Center Revision");
                CategoryDropdown.Items.Remove("Work Center Deletion");
                CategoryDropdown.Items.Remove("Cost Center New");
                CategoryDropdown.Items.Remove("Cost Center Revision");
                CategoryDropdown.Items.Remove("Cost Center Deletion");

                //Remove
                CategoryDropdown.Items.Remove("Annual ST Change");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form - No BIL Approval");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form");
                CategoryDropdown.Items.Remove("MH New ST Model List Form");

                CategoryDropdown.Items.Remove("Manpower/Man-hour");
                CategoryDropdown.Items.Remove("Standard Time (ST mins)");
                CategoryDropdown.Items.Remove("Linestop/Loss Man-hour/Loss Factor");

                //Add
                CategoryDropdown.Items.Add("Work Center New");
                CategoryDropdown.Items.Add("Work Center Revision");
                CategoryDropdown.Items.Add("Work Center Deletion");
                CategoryDropdown.Items.Add("Cost Center New");
                CategoryDropdown.Items.Add("Cost Center Revision");
                CategoryDropdown.Items.Add("Cost Center Deletion");
            }
            else if (ApplicationFormTypeDropdown.Text == "Open MH System")
            {
                //Remove
                CategoryDropdown.Items.Remove("Manpower/Man-hour");
                CategoryDropdown.Items.Remove("Standard Time (ST mins)");
                CategoryDropdown.Items.Remove("Linestop/Loss Man-hour/Loss Factor");

                //Remove
                CategoryDropdown.Items.Remove("Annual ST Change");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form - No BIL Approval");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form");
                CategoryDropdown.Items.Remove("MH New ST Model List Form");

                CategoryDropdown.Items.Remove("Work Center New");
                CategoryDropdown.Items.Remove("Work Center Revision");
                CategoryDropdown.Items.Remove("Work Center Deletion");
                CategoryDropdown.Items.Remove("Cost Center New");
                CategoryDropdown.Items.Remove("Cost Center Revision");
                CategoryDropdown.Items.Remove("Cost Center Deletion");

                //Add
                CategoryDropdown.Items.Add("Manpower/Man-hour");
                CategoryDropdown.Items.Add("Standard Time (ST mins)");
                CategoryDropdown.Items.Add("Linestop/Loss Man-hour/Loss Factor");
            }

        }

        //===================================================<break>======================================================//

        public static bool AutoFillIsDone = false;
        public static bool IsSubmitClicked = false;
        //public static bool IsSTBatchApplicationIsDone = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (AutoFillIsDone == true)
            {

                SelectNewApplicationFormPerCategory();


                AutoFillIsDone = false;
            }

            if (IsSubmitClicked == true)
            {
                SelectRejectedPerApplicationForm();

                IsSubmitClicked = false;
            }
            //if (IsSTBatchApplicationIsDone == true)
            //{
            //    STApplicationAutoFill();

            //    IsSTBatchApplicationIsDone = false;
            //}
        }

        //===================================================<break>======================================================//

        private void OpenMHMinimizeDesign()
        {
            if (CategoryDropdown.Text == "Manpower/Man-hour")
            {
                //Category
                panel8.Size = new Size(315, 35);
                panel8.Location = new Point(12, 0);
                CategoryDropdown.Size = new Size(213, 25);
                CategoryDropdown.Location = new Point(97, 4);

                //Cost Center
                CostCenterPanel.Size = new Size(315, 35);
                CostCenterPanel.Location = new Point(12, 41);
                CostCenterDropdown.Size = new Size(213, 25);
                CostCenterDropdown.Location = new Point(97, 4);

                //Work Center
                WorkCenterPanel.Size = new Size(315, 35);
                WorkCenterPanel.Location = new Point(339, 41);
                WorkCenterDropdown.Size = new Size(190, 25);
                WorkCenterDropdown.Location = new Point(120, 4);

                //Month to open
                MonthToOpenPanel.Size = new Size(315, 35);
                MonthToOpenPanel.Location = new Point(339, 0);
                MonthToOpenDropdown.Size = new Size(190, 25);
                MonthToOpenDropdown.Location = new Point(120, 4);
                label9.Size = new Size(115, 33);
                panel15.Size = new Size(115, 33);

                //Reason of application
                ReasonOfApplicationPanel.Size = new Size(384, 35);
                ReasonOfApplicationPanel.Location = new Point(668, 0);
                ReasonOfApplicationTextBox.Size = new Size(216, 18);

                EffectiveDatePanel.Location = new Point(668, 41);
                EffectiveDatePanel.Size = new Size(384, 35);
                EffectivityDateTimePicker.Location = new Point(161, 5);
                EffectivityDateTimePicker.Size = new Size(216, 18);
                panel11.Size = new Size(158, 33);

                //submit button
                SubmitButton.Location = new Point(668, 82);
                SubmitButton.Size = new Size(384, 35);
            }
            else
            {

                //Category
                panel8.Size = new Size(331, 35);
                panel8.Location = new Point(12, 0);
                CategoryDropdown.Size = new Size(228, 25);
                CategoryDropdown.Location = new Point(97, 4);

                //Month to open
                MonthToOpenPanel.Size = new Size(315, 35);
                MonthToOpenPanel.Location = new Point(699, 0);
                MonthToOpenDropdown.Size = new Size(190, 25);
                MonthToOpenDropdown.Location = new Point(120, 4);
                label9.Size = new Size(115, 33);
                panel15.Size = new Size(115, 33);

                //Reason of application
                ReasonOfApplicationPanel.Size = new Size(384, 35);
                ReasonOfApplicationPanel.Location = new Point(12, 44);
                ReasonOfApplicationTextBox.Size = new Size(216, 18);


                //EffectiveDatePanel.Location = new Point(668, 41);
                //EffectiveDatePanel.Size = new Size(384, 35);
                //EffectivityDateTimePicker.Location = new Point(161, 5);
                //EffectivityDateTimePicker.Size = new Size(216, 18);

                EffectiveDatePanel.Location = new Point(355, 0);
                EffectiveDatePanel.Size = new Size(331, 35);
                EffectivityDateTimePicker.Location = new Point(120, 5);
                EffectivityDateTimePicker.Size = new Size(205, 23);

                panel11.Size = new Size(115, 33);

                //submit button
                SubmitButton.Location = new Point(408, 44);
                SubmitButton.Size = new Size(157, 35);

               
            }


        }

        private void OpenMHMaximizeDesign()
        {
            ////Work Center
            //WorkCenterPanel.Size = new Size(331, 35);
            //WorkCenterPanel.Location = new Point(355, 41);
            //WorkCenterDropdown.Size = new Size(258, 25);

            ////Month to open
            //MonthToOpenPanel.Size = new Size(384, 35);
            //MonthToOpenPanel.Location = new Point(699, 41);
            //MonthToOpenDropdown.Size = new Size(215, 25);
            //MonthToOpenDropdown.Location = new Point(163, 4);
            //label9.Size = new Size(158, 33);
            //panel15.Size = new Size(158, 33);

            ////Reason of application
            //ReasonOfApplicationPanel.Size = new Size(384, 35);
            //ReasonOfApplicationPanel.Location = new Point(699, 0);
            //ReasonOfApplicationTextBox.Size = new Size(290, 18);

            ////submit button
            //SubmitButton.Location = new Point(1096, 0);
            ////SubmitButton.Size = new Size(157, 35);

            if (CategoryDropdown.Text == "Manpower/Man-hour")
            {
                EffectiveDatePanel.Visible = true;
                WorkCenterPanel.Visible = true;
                CostCenterPanel.Visible = true;

                //Category
                panel8.Size = new Size(331, 35);
                panel8.Location = new Point(12, 0);
                CategoryDropdown.Size = new Size(228, 25);
                CategoryDropdown.Location = new Point(97, 4);

                //Cost Center
                CostCenterPanel.Size = new Size(331, 35);
                CostCenterPanel.Location = new Point(12, 41);
                CostCenterDropdown.Size = new Size(228, 25);
                CostCenterDropdown.Location = new Point(97, 4);

                //Work Center
                WorkCenterPanel.Size = new Size(331, 35);
                WorkCenterPanel.Location = new Point(355, 41);
                WorkCenterDropdown.Size = new Size(205, 25);
                WorkCenterDropdown.Location = new Point(120, 4);

                EffectiveDatePanel.Location = new Point(355, 0);
                EffectiveDatePanel.Size = new Size(331, 35);
                EffectivityDateTimePicker.Location = new Point(120, 5);
                EffectivityDateTimePicker.Size = new Size(205, 23);

                panel11.Size = new Size(115, 33);

                //Reason of application
                ReasonOfApplicationPanel.Size = new Size(384, 35);
                ReasonOfApplicationPanel.Location = new Point(699, 0);
                ReasonOfApplicationTextBox.Size = new Size(290, 18);
                //SubmitButton.Location = new Point(1220, 0);

                //Month to open
                MonthToOpenPanel.Size = new Size(384, 35);
                MonthToOpenPanel.Location = new Point(699, 41);
                MonthToOpenDropdown.Size = new Size(215, 25);
                MonthToOpenDropdown.Location = new Point(163, 4);
                label9.Size = new Size(158, 33);
                panel15.Size = new Size(158, 33);

                SubmitButton.Location = new Point(1095, 0);
                SubmitButton.Size = new Size(157, 35);

            }
            else
            {
                EffectiveDatePanel.Visible = true;
                WorkCenterPanel.Visible = false;
                MonthToOpenPanel.Visible = true;
                CostCenterPanel.Visible = false;

                //Month to open
                MonthToOpenPanel.Location = new Point(699, 0);
                MonthToOpenPanel.Size = new Size(384, 35);
                MonthToOpenDropdown.Size = new Size(215, 25);
                MonthToOpenDropdown.Location = new Point(163, 4);
                label9.Size = new Size(158, 33);
                panel15.Size = new Size(158, 33);

                ReasonOfApplicationPanel.Location = new Point(12, 44);
                SubmitButton.Location = new Point(1095, 0);
            }

        }


        private void ApplicationForm_Resize(object sender, EventArgs e)
        {
            
            if (ApplicationFormTypeDropdown.Text == "ST")
            {
                if (this.Width <= 1400)
                {

                    HeaderPanel.Size = new Size(HeaderPanel.Width, 134);
                    EffectiveDatePanel.Location = new Point(355, 0);
                    ReasonOfApplicationPanel.Location = new Point(356, 44);
                    SubmitButton.Location = new Point(753, 44);
                    ShowEntriesPanel.Location = new Point(12, 93);
                    //DownloadSTButton.Location = new Point(213, 93);
                    //UploadSTButton.Location = new Point(376, 93);
                }
            }
            else if (ApplicationFormTypeDropdown.Text == "WC/CC")
            {
                if (this.Width <= 1400)
                {
                    HeaderPanel.Size = new Size(HeaderPanel.Width, 134);
                    EffectiveDatePanel.Location = new Point(356, 0);
                    ReasonOfApplicationPanel.Location = new Point(12, 41);
                    SubmitButton.Location = new Point(699, 0);
                    ShowEntriesPanel.Location = new Point(12, 93);
                    //DownloadSTButton.Location = new Point(213, 93);
                    //UploadSTButton.Location = new Point(376, 93);
                }
                else
                {
                    HeaderPanel.Size = new Size(1279, 134);
                    EffectiveDatePanel.Location = new Point(356, 0);
                    ReasonOfApplicationPanel.Location = new Point(699, 0);
                    SubmitButton.Location = new Point(1096, 0);
                    ShowEntriesPanel.Location = new Point(12, 93);
                    //DownloadSTButton.Location = new Point(12, 45);
                    //UploadSTButton.Location = new Point(186, 45);

                    //if (ApplicationFormTypeDropdown.Text == "Open MH System")
                    //{
                    //    if (CategoryDropdown.Text == "Manpower/Man-hour")
                    //    {
                    //        CostCenterPanel.Visible = true;
                    //        WorkCenterPanel.Visible = true;
                    //        MonthToOpenPanel.Visible = true;
                    //        EffectiveDatePanel.Visible = false;
                    //    }
                    //    else
                    //    {
                    //        CostCenterPanel.Visible = false;
                    //        WorkCenterPanel.Visible = false;
                    //        EffectiveDatePanel.Visible = false;
                    //    }


                    //    //Category
                    //    panel8.Size = new Size(331, 35);
                    //    //panel8.Location = new Point(12, 0);
                    //    CategoryDropdown.Size = new Size(228, 25);
                    //    //CategoryDropdown.Location = new Point(97, 4);

                    //    //Cost Center
                    //    CostCenterPanel.Size = new Size(331, 35);
                    //    //CostCenterPanel.Location = new Point(12, 41);
                    //    CostCenterDropdown.Size = new Size(228, 25);
                    //    //comboBox1.Location = new Point(97, 4);

                    //    OpenMHMaximizeDesign();

                    //}

                    //Category
                    panel8.Size = new Size(331, 35);
                    //panel8.Location = new Point(12, 0);
                    CategoryDropdown.Size = new Size(228, 25);
                }

            }
            else if (ApplicationFormTypeDropdown.Text == "Open MH System")
            {
                if (this.Width <= 1400)
                {
                    OpenMHMinimizeDesign();
                }
                else
                {
                    OpenMHMaximizeDesign();
                }
            }
                

            //}
            //else
            //{
            //    HeaderPanel.Size = new Size(1279, 134);
            //    EffectiveDatePanel.Location = new Point(356, 0);
            //    ReasonOfApplicationPanel.Location = new Point(699, 0);
            //    SubmitButton.Location = new Point(1096, 0);
            //    ShowEntriesPanel.Location = new Point(12, 93);
            //    //DownloadSTButton.Location = new Point(12, 45);
            //    //UploadSTButton.Location = new Point(186, 45);

            //    if (ApplicationFormTypeDropdown.Text == "Open MH System")
            //    {
            //        if (CategoryDropdown.Text == "Manpower/Man-hour")
            //        {
            //            CostCenterPanel.Visible = true;
            //            WorkCenterPanel.Visible = true;
            //            MonthToOpenPanel.Visible = true;
            //            EffectiveDatePanel.Visible = false;
            //        }
            //        else
            //        {
            //            CostCenterPanel.Visible = false;
            //            WorkCenterPanel.Visible = false;
            //            EffectiveDatePanel.Visible = false;
            //        }


            //        //Category
            //        panel8.Size = new Size(331, 35);
            //        //panel8.Location = new Point(12, 0);
            //        CategoryDropdown.Size = new Size(228, 25);
            //        //CategoryDropdown.Location = new Point(97, 4);

            //        //Cost Center
            //        CostCenterPanel.Size = new Size(331, 35);
            //        //CostCenterPanel.Location = new Point(12, 41);
            //        CostCenterDropdown.Size = new Size(228, 25);
            //        //comboBox1.Location = new Point(97, 4);

            //        OpenMHMaximizeDesign();

            //    }

            //    //Category
            //    panel8.Size = new Size(331, 35);
            //    //panel8.Location = new Point(12, 0);
            //    CategoryDropdown.Size = new Size(228, 25);
            //}
        }

        //===================================================<break>======================================================//

        public void LoadWorkCenter()
        {
           
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

           
            SqlCommand LoadWorkCenter = new SqlCommand("SP_LoadWorkCenterDropdwonList", con);
            LoadWorkCenter.CommandType = CommandType.StoredProcedure;
            LoadWorkCenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            LoadWorkCenter.Parameters.AddWithValue("@CostCenterCode", CostCenterDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(LoadWorkCenter);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadWorkCenter.ExecuteNonQuery();
            con.Close();

            WorkCenterDropdown.DataSource = ds.Tables[0];
            WorkCenterDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            WorkCenterDropdown.ValueMember = "Work Center";

        }// <---- end

        public void LoadCostCenter()
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            SqlCommand LoadCostCenter = new SqlCommand("SP_LoadCostCenter", con);
            LoadCostCenter.CommandType = CommandType.StoredProcedure;
            LoadCostCenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(LoadCostCenter);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadCostCenter.ExecuteNonQuery();
            con.Close();

            CostCenterDropdown.DataSource = ds.Tables[0];
            CostCenterDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            CostCenterDropdown.ValueMember = "Cost Center";

        }// <---- end

        private void ApplicationFormTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            CategoryHeaderType.Visible = false; //Hide Header when category is empty or no selected index

            if (ApplicationFormTypeDropdown.Text == "ST")
            {
                HeaderPanel.Visible = true;

                if (this.Width < 1400)
                {
                    EffectiveDatePanel.Visible = false;
                    ReasonOfApplicationPanel.Visible = false;
                    SubmitButton.Visible = false;
                }
                else
                {

                }
                    
            }
            else if (ApplicationFormTypeDropdown.Text == "WC/CC")
            {
                HeaderPanel.Visible = true;

                if (this.Width < 1400)
                {
                    HeaderPanel.Size = new Size(HeaderPanel.Width, 134);
                    EffectiveDatePanel.Location = new Point(356, 0);
                    ReasonOfApplicationPanel.Location = new Point(12, 41);
                    SubmitButton.Location = new Point(699, 0);
                    ShowEntriesPanel.Location = new Point(12, 93);
                }
                else
                {
                    HeaderPanel.Size = new Size(1279, 134);
                    EffectiveDatePanel.Location = new Point(356, 0);
                    ReasonOfApplicationPanel.Location = new Point(699, 0);
                    SubmitButton.Location = new Point(1096, 0);
                    ShowEntriesPanel.Location = new Point(12, 93);
                }

                
            }
            else if (ApplicationFormTypeDropdown.Text == "Open MH System")
            {
                if (this.Width < 1400)
                {
                    OpenMHMinimizeDesign();
                }
            }

            //if (ApplicationFormTypeDropdown.Text != "")
            //{
            //    if (ApplicationTypeDropdown.Text != "")
            //    {
            //        HeaderPanel.Visible = true;

            //        if (this.Width < 1400)
            //        {
            //            if (ApplicationFormTypeDropdown.Text == "ST")
            //            {
            //                EffectiveDatePanel.Visible = false;
            //                ReasonOfApplicationPanel.Visible = false;
            //                SubmitButton.Visible = false;
            //            }
            //            else if (ApplicationFormTypeDropdown.Text == "WCC/CC")
            //            {

            //            }
            //        }
            //    }

            //}
            //else
            //{
            //    HeaderPanel.Visible = false;
            //}
            //if (ApplicationFormTypeDropdown.Text == "Open MH System")
            //{
                
            //    CostCenterPanel.Visible = true;
            //    WorkCenterPanel.Visible = true;
            //    MonthToOpenPanel.Visible = true;
            //    EffectiveDatePanel.Visible = true;

            //    //OpenMHMaximizeDesign();

            //    if (this.Width < 1400)
            //    {
            //        OpenMHMinimizeDesign();
            //    }
            //    else
            //    {
            //        //Cost Center
            //        CostCenterPanel.Size = new Size(331, 35);
            //        //CostCenterPanel.Location = new Point(12, 41);
            //        CostCenterDropdown.Size = new Size(228, 25);

            //        OpenMHMaximizeDesign();
            //    }

            //}
            //else
            //{
            //    CostCenterPanel.Visible = false;
            //    WorkCenterPanel.Visible = false;
            //    MonthToOpenPanel.Visible = false;
            //    EffectiveDatePanel.Visible = true;

            //    if (this.Width < 1400)
            //    {
            //        HeaderPanel.Size = new Size(HeaderPanel.Width, 134);
            //        EffectiveDatePanel.Location = new Point(12, 44);
            //        ReasonOfApplicationPanel.Location = new Point(356, 44);
            //        SubmitButton.Location = new Point(753, 44);
            //        SubmitButton.Size = new Size(157, 35);
            //        ShowEntriesPanel.Location = new Point(12, 93);
            //    }
            //    else
            //    {
            //        HeaderPanel.Size = new Size(1279, 134);
            //        EffectiveDatePanel.Location = new Point(356, 0);
            //        ReasonOfApplicationPanel.Location = new Point(699, 0);
            //        ReasonOfApplicationPanel.Size = new Size(384, 35);
            //        ReasonOfApplicationTextBox.Size = new Size(216, 18);

            //        SubmitButton.Location = new Point(1096, 0);
            //        SubmitButton.Size = new Size(157, 35);
            //        ShowEntriesPanel.Location = new Point(12, 93);

            //        //Category
            //        panel8.Size = new Size(331, 35);
            //        //panel8.Location = new Point(12, 0);
            //        CategoryDropdown.Size = new Size(228, 25);
            //    }
               
            //}

            if (ApplicationFormTypeDropdown.Text == "Manpower Forecasting")
            {
                ManpowerForecastingHeaderPanel.Visible = true;
                HeaderPanel.Visible = false;
            }
            else
            {
                ManpowerForecastingHeaderPanel.Visible = false;
                HeaderPanel.Visible = true;
                AddCategoryPerApplication();
            }
        }

        //===================================================<break>======================================================//

        private void CategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ApplicationFormTypeDropdown.Text == "ST")
            {
                CreateFormButton.Enabled = false;

                if (ApplicationTypeDropdown.Text == "")
                {
                    MessageBox.Show("Please select application type", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ApplicationTypeDropdown.Select();
                }
                else
                {
                    if (ApplicationTypeDropdown.Text == "New Application")
                    {
                        if (CategoryHeaderType.Text == "")
                        {
                            TableHeaderPanel.Visible = false;
                            ApplicationDataGrid.DataSource = null;
                        }
                        else
                        {
                            ApplicationDataGrid.DataSource = null;
                            TableHeaderPanel.Visible = true;
                            CategoryHeaderType.Visible = true;
                            CategoryHeaderType.Text = CategoryDropdown.Text;
                            
                            DLULBtnPanel.Visible = true;
                            EffectiveDatePanel.Visible = true;
                            ReasonOfApplicationPanel.Visible = false;
                            SubmitButton.Visible = false;
                            RowCount.Visible = false;
                            RefreshBtn.Visible = false;
                           

                            MonthToOpenPanel.Visible = false;
                            WorkCenterPanel.Visible = false;
                            CostCenterPanel.Visible = false;

                            //SelectApplicationFormData();

                            //ApplicationDataGrid.Columns[1].Visible = false;

                            //if (CategoryDropdown.Text == "MH New ST Model List Form")
                            //{

                            //    ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                            //}
                            //else
                            //{
                            //    ApplicationDataGrid.Columns[1].Frozen = true;
                            //    ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            //}

                        }
                    }
                    else if (ApplicationTypeDropdown.Text == "Edit Application")
                    {
                        if (CategoryHeaderType.Text == "")
                        {
                            TableHeaderPanel.Visible = false;
                            ApplicationDataGrid.DataSource = null;
                        }
                        else
                        {
                            TableHeaderPanel.Visible = true;
                            CategoryHeaderType.Visible = true;
                            CategoryHeaderType.Text = CategoryDropdown.Text;
                            DLULBtnPanel.Visible = false;
                            EffectiveDatePanel.Visible = false;
                            ReasonOfApplicationPanel.Visible = false;
                            SubmitButton.Visible = false;
                            ApplicationDataGrid.ReadOnly = true;

                            SelectForApprovalPerApplicationForm();


                            ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                    else if (ApplicationTypeDropdown.Text == "Resubmit Application")
                    {
                    
                        if (CategoryHeaderType.Text == "")
                        {
                            TableHeaderPanel.Visible = false;
                            ApplicationDataGrid.DataSource = null;
                        }
                        else
                        {
                            TableHeaderPanel.Visible = true;
                            CategoryHeaderType.Visible = true;
                            CategoryHeaderType.Text = CategoryDropdown.Text;
                            DLULBtnPanel.Visible = false;
                            EffectiveDatePanel.Visible = false;
                            ReasonOfApplicationPanel.Visible = false;
                            SubmitButton.Visible = false;

                            SelectRejectedPerApplicationForm();

                            ApplicationDataGrid.Columns["WithSAP"].Visible = false;

                            ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            else if (ApplicationFormTypeDropdown.Text == "WC/CC")
            {
                CreateFormButton.Enabled = true;

                if (ApplicationTypeDropdown.Text == "")
                {
                    MessageBox.Show("Please select application type", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ApplicationTypeDropdown.Select();
                }
                else
                {
                    if (ApplicationTypeDropdown.Text == "New Application")
                    {
                        if (CategoryHeaderType.Text == "")
                        {
                            TableHeaderPanel.Visible = false;
                            ApplicationDataGrid.DataSource = null;
                        }
                        else
                        {
                            ApplicationDataGrid.DataSource = null;
                            TableHeaderPanel.Visible = true;
                            CategoryHeaderType.Visible = true;
                            CategoryHeaderType.Text = CategoryDropdown.Text;
                            DLULBtnPanel.Visible = true;
                            EffectiveDatePanel.Visible = true;
                            ReasonOfApplicationPanel.Visible = true;
                            SubmitButton.Visible = true;
                            CreateFormButton.Visible = true;
                            RowCount.Visible = true;

                            MonthToOpenPanel.Visible = false;
                            WorkCenterPanel.Visible = false;
                            CostCenterPanel.Visible = false;

                        }
                    }
                    else if (ApplicationTypeDropdown.Text == "Edit Application")
                    {
                        if (CategoryHeaderType.Text == "")
                        {
                            TableHeaderPanel.Visible = false;
                            ApplicationDataGrid.DataSource = null;
                        }
                        else
                        {
                            TableHeaderPanel.Visible = true;
                            CategoryHeaderType.Visible = true;
                            CategoryHeaderType.Text = CategoryDropdown.Text;
                            DLULBtnPanel.Visible = false;
                            EffectiveDatePanel.Visible = false;
                            ReasonOfApplicationPanel.Visible = false;
                            SubmitButton.Visible = false;
                            ApplicationDataGrid.ReadOnly = true;


                            SelectForApprovalPerApplicationForm();


                            ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }



                    }
                    else if (ApplicationTypeDropdown.Text == "Resubmit Application")
                    {
                        //Type code here...
                        if (CategoryHeaderType.Text == "")
                        {
                            TableHeaderPanel.Visible = false;
                            ApplicationDataGrid.DataSource = null;
                        }
                        else
                        {
                            TableHeaderPanel.Visible = true;
                            CategoryHeaderType.Visible = true;
                            CategoryHeaderType.Text = CategoryDropdown.Text;
                            DLULBtnPanel.Visible = false;
                            EffectiveDatePanel.Visible = false;
                            ReasonOfApplicationPanel.Visible = false;
                            SubmitButton.Visible = false;

                            SelectRejectedPerApplicationForm();

                            ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            else if (ApplicationFormTypeDropdown.Text == "Open MH System")
            {
                InitDatePicker(MPFEffectivityDateTimePicker);

                CreateFormButton.Enabled = true;
                ApplicationDataGrid.DataSource = null;

                if (ApplicationTypeDropdown.Text == "")
                {
                    MessageBox.Show("Please select application type", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ApplicationTypeDropdown.Select();
                }
                else
                {
                    if (ApplicationTypeDropdown.Text == "New Application")
                    {
                        if (CategoryHeaderType.Text == "")
                        {
                            TableHeaderPanel.Visible = false;
                            ApplicationDataGrid.DataSource = null;
                        }
                        else
                        {

                           

                            ApplicationDataGrid.DataSource = null;
                            TableHeaderPanel.Visible = true;
                            CategoryHeaderType.Visible = true;
                            CategoryHeaderType.Text = CategoryDropdown.Text;
                            EffectiveDatePanel.Visible = false;
                            DLULBtnPanel.Visible = true;
                            ReasonOfApplicationPanel.Visible = true;
                            SubmitButton.Visible = true;
                            CreateFormButton.Visible = true;
                            RowCount.Visible = true;
                            MonthToOpenPanel.Visible = true;
                          

                            if (CategoryDropdown.Text == "Manpower/Man-hour")
                            {
                                EffectiveDatePanel.Visible = true;
                                WorkCenterPanel.Visible = true;
                                CostCenterPanel.Visible = true;

                                //Reason of application
                                ReasonOfApplicationPanel.Size = new Size(384, 35);
                                ReasonOfApplicationPanel.Location = new Point(699, 0);
                                ReasonOfApplicationTextBox.Size = new Size(290, 18);
                                //SubmitButton.Location = new Point(1220, 0);

                                //Month to open
                                MonthToOpenPanel.Size = new Size(384, 35);
                                MonthToOpenPanel.Location = new Point(699, 41);
                                MonthToOpenDropdown.Size = new Size(215, 25);
                                MonthToOpenDropdown.Location = new Point(163, 4);
                                label9.Size = new Size(158, 33);
                                panel15.Size = new Size(158, 33);

                            }
                            else
                            {
                                EffectiveDatePanel.Visible = true;
                                WorkCenterPanel.Visible = false;
                                MonthToOpenPanel.Visible = true;
                                CostCenterPanel.Visible = false;

                                MonthToOpenPanel.Location = new Point(699, 0);
                                ReasonOfApplicationPanel.Location = new Point(12, 44);
                                SubmitButton.Location = new Point(1095, 0);
                            }

                            if (this.Width < 1400)
                            {
                                OpenMHMinimizeDesign();
                            }
                            else
                            {
                                OpenMHMaximizeDesign();
                            }

                        }
                    }
                    else if (ApplicationTypeDropdown.Text == "Edit Application")
                    {
                        if (CategoryHeaderType.Text == "")
                        {
                            TableHeaderPanel.Visible = false;
                            ApplicationDataGrid.DataSource = null;
                        }
                        else
                        {
                            TableHeaderPanel.Visible = true;
                            CategoryHeaderType.Visible = true;
                            CategoryHeaderType.Text = CategoryDropdown.Text;
                            DLULBtnPanel.Visible = false;
                            EffectiveDatePanel.Visible = false;
                            ReasonOfApplicationPanel.Visible = false;
                            SubmitButton.Visible = false;
                            MonthToOpenPanel.Visible = false;
                            ApplicationDataGrid.ReadOnly = true;

                            SelectForApprovalPerApplicationForm();


                            ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }



                    }
                    else if (ApplicationTypeDropdown.Text == "Resubmit Application")
                    {
                        if (CategoryHeaderType.Text == "")
                        {
                            TableHeaderPanel.Visible = false;
                            ApplicationDataGrid.DataSource = null;
                        }
                        else
                        {
                            TableHeaderPanel.Visible = true;
                            CategoryHeaderType.Visible = true;
                            CategoryHeaderType.Text = CategoryDropdown.Text;
                            DLULBtnPanel.Visible = false;
                            EffectiveDatePanel.Visible = false;
                            ReasonOfApplicationPanel.Visible = false;
                            SubmitButton.Visible = false;
                            MonthToOpenPanel.Visible = false;

                            SelectRejectedPerApplicationForm();

                            ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }


                }
            }
        }

        //===================================================<break>======================================================//

        private void SelectApplicationFormData()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (DropdownEntriesValue.Text == "All")
            {
                SqlCommand SelectApplication = new SqlCommand("SP_SelectApplicationDataByCategory", con);
                SelectApplication.CommandType = CommandType.StoredProcedure;
                SelectApplication.Parameters.AddWithValue("@Procedure", "SelectAll");
                SelectApplication.Parameters.AddWithValue("@ApplicationType", ApplicationFormTypeDropdown.Text);
                SelectApplication.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                SelectApplication.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectApplication.Parameters.AddWithValue("@Entries", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectApplication);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApplicationDataGrid.DataSource = dt;
                con.Close();
            }
            else
            {
                SqlCommand SelectApplication = new SqlCommand("SP_SelectApplicationDataByCategory", con);
                SelectApplication.CommandType = CommandType.StoredProcedure;
                SelectApplication.Parameters.AddWithValue("@Procedure", "SelectByEntries");
                SelectApplication.Parameters.AddWithValue("@ApplicationType", ApplicationFormTypeDropdown.Text);
                SelectApplication.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                SelectApplication.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectApplication.Parameters.AddWithValue("@Entries", Convert.ToInt32(DropdownEntriesValue.Text));
                SqlDataAdapter sda = new SqlDataAdapter(SelectApplication);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApplicationDataGrid.DataSource = dt;
                con.Close();
            }
           

            ApplicationDataGrid.Columns["ApplicationFormNo"].Visible = false;

            
        }

        //===================================================<break>======================================================//

        private void SelectForApprovalPerApplicationForm()
        {
            if (DropdownEntriesValue.Text == "All")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectSTDataForEdit = new SqlCommand("SP_SelectForApprovalPerApplicationFormCategory", con);
                SelectSTDataForEdit.CommandType = CommandType.StoredProcedure;
                SelectSTDataForEdit.Parameters.AddWithValue("@Procedure", "SelectAll");
                SelectSTDataForEdit.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                SelectSTDataForEdit.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                SelectSTDataForEdit.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectSTDataForEdit.Parameters.AddWithValue("@Entries", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectSTDataForEdit);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApplicationDataGrid.DataSource = dt;
                con.Close();
            }
            else
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectSTDataForEdit = new SqlCommand("SP_SelectForApprovalPerApplicationFormCategory", con);
                SelectSTDataForEdit.CommandType = CommandType.StoredProcedure;
                SelectSTDataForEdit.Parameters.AddWithValue("@Procedure", "SelectByEntries");
                SelectSTDataForEdit.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                SelectSTDataForEdit.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                SelectSTDataForEdit.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectSTDataForEdit.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectSTDataForEdit);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApplicationDataGrid.DataSource = dt;
                con.Close();
            }
            
        }

        //===================================================<break>======================================================//

        private void SelectRejectedPerApplicationForm()
        {
            if (DropdownEntriesValue.Text == "All")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectRejectedApplication = new SqlCommand("SP_SelectRejectedPerApplicationFormCategory", con);
                SelectRejectedApplication.CommandType = CommandType.StoredProcedure;
                SelectRejectedApplication.Parameters.AddWithValue("@Procedure", "SelectAll");
                SelectRejectedApplication.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                SelectRejectedApplication.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                SelectRejectedApplication.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectRejectedApplication.Parameters.AddWithValue("@Entries", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectRejectedApplication);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApplicationDataGrid.DataSource = dt;
                con.Close();
            }
            else
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectRejectedApplication = new SqlCommand("SP_SelectRejectedPerApplicationFormCategory", con);
                SelectRejectedApplication.CommandType = CommandType.StoredProcedure;
                SelectRejectedApplication.Parameters.AddWithValue("@Procedure", "SelectByEntries");
                SelectRejectedApplication.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                SelectRejectedApplication.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                SelectRejectedApplication.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectRejectedApplication.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectRejectedApplication);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApplicationDataGrid.DataSource = dt;
                con.Close();
            }

            
        }

        //===================================================<break>======================================================//

        private void SelectAppliedSTPerCategory()
        {
            if (ApplicationFormTypeDropdown.Text == "ST")
            {
                if (ApplicationTypeDropdown.Text == "New Application")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand SelectSTData = new SqlCommand("SP_SelectAppliedSTPerCategory", con);
                    SelectSTData.CommandType = CommandType.StoredProcedure;
                    SelectSTData.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                    SelectSTData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SqlDataAdapter sda = new SqlDataAdapter(SelectSTData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApplicationDataGrid.DataSource = dt;
                    con.Close();

                    ApplicationDataGrid.Columns["No."].Visible = true;
                }
                else if (ApplicationTypeDropdown.Text == "Edit Application")
                {
                    //Type Code here...
                    MessageBox.Show("ST edit");
                }
                else if (ApplicationTypeDropdown.Text == "Resubmit Application")
                {
                    //Type Code here...
                    MessageBox.Show("ST Resubmit");
                }
            }
            else
            {
                //TYpe Code here
            }
           
        }

        //===================================================<break>======================================================//

        private void SelectNewApplicationFormPerCategory()
        {
            if (ApplicationFormTypeDropdown.Text == "ST")
            {
                //if (ApplicationTypeDropdown.Text == "New Application")
                //{
                //    if (con.State == ConnectionState.Closed)
                //    {
                //        con.Open();
                //    }

                //    SqlCommand SelectSTData = new SqlCommand("SP_SelectNewApplicationFormPerCategory", con);
                //    SelectSTData.CommandType = CommandType.StoredProcedure;
                //    SelectSTData.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                //    SelectSTData.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                //    SelectSTData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectSTData);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    ApplicationDataGrid.DataSource = dt;
                //    con.Close();

                //    ApplicationDataGrid.Columns["ApplicationFormNo"].Visible = false;

                //    if (CategoryDropdown.Text == "MH New ST Model List Form")
                //    {
                //        ApplicationDataGrid.Columns[1].Frozen = false;
                //        ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                //    }
                //    else
                //    {
                //        ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                //    }

                //}
                
            }
            else if (ApplicationFormTypeDropdown.Text == "WC/CC")
            {
                if (ApplicationTypeDropdown.Text == "New Application")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand SelectSTData = new SqlCommand("SP_SelectNewApplicationFormPerCategory", con);
                    SelectSTData.CommandType = CommandType.StoredProcedure;
                    SelectSTData.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                    SelectSTData.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                    SelectSTData.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                    SelectSTData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SqlDataAdapter sda = new SqlDataAdapter(SelectSTData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApplicationDataGrid.DataSource = dt;
                    con.Close();

                    ApplicationDataGrid.Columns["ApplicationFormNo"].Visible = false;
                    ApplicationDataGrid.Columns[1].Frozen = false;

                    ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                }
            }
            else if (ApplicationFormTypeDropdown.Text == "Open MH System")
            {
                if (ApplicationTypeDropdown.Text == "New Application")
                {
                    ApplicationDataGrid.DataSource = null;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand SelectSTData = new SqlCommand("SP_SelectNewApplicationFormPerCategory", con);
                    SelectSTData.CommandType = CommandType.StoredProcedure;
                    SelectSTData.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                    SelectSTData.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                    SelectSTData.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                    SelectSTData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SqlDataAdapter sda = new SqlDataAdapter(SelectSTData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApplicationDataGrid.DataSource = dt;
                    con.Close();


                    ApplicationDataGrid.Columns["ApplicationFormNo"].Visible = false;


                    if (CategoryDropdown.Text == "Manpower/Man-hour")
                    {
                        ApplicationDataGrid.Columns[1].Frozen = true;
                        ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    }
                    else if (CategoryDropdown.Text == "Standard Time (ST mins)")
                    {
                        ApplicationDataGrid.Columns[1].Frozen = false;
                        ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                    else if (CategoryDropdown.Text == "Linestop/Loss Man-hour/Loss Factor")
                    {
                        ApplicationDataGrid.Columns[1].Frozen = true;
                        ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    }

                    

                }
            }
        }

        //===================================================<break>======================================================//

        public static string MonthToOpen;

        public static string SelectedEffectivityDate;
      

        private void UploadSTTemplateBtn_Click(object sender, EventArgs e)
        {
            ApplicationFormType = ApplicationFormTypeDropdown.Text;
            Category = CategoryDropdown.Text;

            if (ApplicationFormTypeDropdown.Text == "Open MH System")
            {
                if (EffectivityDateTimePicker.CustomFormat == " ")
                {
                    MessageBox.Show("Please select effectivity date", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if(string.IsNullOrWhiteSpace(MonthToOpenDropdown.Text))
                {
                    MessageBox.Show("Please select month to open to proceed.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                { 
                    MonthToOpen = MonthToOpenDropdown.Text;
                    SelectedEffectivityDate = EffectivityDateTimePicker.Value.ToString();

                    UploadTemplate uploadSTTemplate = new UploadTemplate();
                    uploadSTTemplate.ShowDialog();

                    InitDatePicker(EffectivityDateTimePicker);
                }
            }
            else
            {
                if (EffectivityDateTimePicker.CustomFormat == " ")
                {
                    MessageBox.Show("Please select effectivity date", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    SelectedEffectivityDate = EffectivityDateTimePicker.Value.ToString();

                    UploadTemplate uploadSTTemplate = new UploadTemplate();
                    uploadSTTemplate.ShowDialog();

                    InitDatePicker(EffectivityDateTimePicker);
                }
            }
        }

        //===================================================<break>======================================================//

        private void DownloadSTTemplateBtn_Click(object sender, EventArgs e)
        {

        }

        //===================================================<break>======================================================//

        //private void EffectivityDateText_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    EffectivityDateText.Text = EffectivityDateText.Text;
        //    EffectivityDateText.ForeColor = Color.FromName("WindowText");
        //}

        //===================================================<break>======================================================//


        //===================================================<break>======================================================//

        //private void EffectivityDateText_MouseEnter(object sender, EventArgs e)
        //{
        //    if (EffectivityDateText.Text == "Ex: 04/01/2023")
        //    {
        //        EffectivityDateText.Text = "";
        //    }

        //}

        //===================================================<break>======================================================//

        //private void EffectivityDateText_MouseLeave(object sender, EventArgs e)
        //{
        //    if (EffectivityDateText.Text == "")
        //    {
        //        EffectivityDateText.Text = "Ex: 04/01/2023";
        //        EffectivityDateText.ForeColor = Color.FromName("WindowFrame");
        //    }
        //}

        //===================================================<break>======================================================//

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (CategoryDropdown.Text == "")
            {
                MessageBox.Show("Please select category.", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CategoryDropdown.Select();
            }
            if (EffectivityDateTimePicker.Text == "")
            {
                MessageBox.Show("Please select effectivity date.", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                EffectivityDateTimePicker.Select();
            }
            else if (ReasonOfApplicationTextBox.Text == "")
            {
                MessageBox.Show("Please type reason of application.", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ReasonOfApplicationTextBox.Select();
            }
            else
            {
                if (ApplicationFormTypeDropdown.Text == "Open MH System")
                {
                    if (MonthToOpenDropdown.Text == "")
                    {
                        MessageBox.Show("Please select month to open.", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        MonthToOpenDropdown.Select();
                    }
                    else
                    {
                        SubmitAppliactionForm();
                    }
                }
                else
                {
                    SubmitAppliactionForm();
                }
                
            }

            //Then send email....
            //if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
            //{
            //SelectAppliedSTPerCategory();
            //STApplicationAutoFill();
            
                
            //}
            //else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
            //{
            //    SendInkCartridgeSTApplicationEmail();
            //}
            

            //if (ApplicationFormTypeDropdown.Text == "")
            //{
            //    MessageBox.Show("Please select application form.", "Application form is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
            //else if (ApplicationTypeDropdown.Text == "")
            //{
            //    MessageBox.Show("Please select application type.", "Application type is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
            //else
            //{
            //    if (ApplicationFormTypeDropdown.Text == "ST")
            //    {
            //        if (CategoryDropdown.Text == "")
            //        {
            //            MessageBox.Show("Please select category.", "Category is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            CategoryDropdown.Select();
            //        }
            //        if (EffectivityDateTimePicker.Text == "")
            //        {
            //            MessageBox.Show("Please type effectivity date.", "Effectivity date is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            EffectivityDateTimePicker.Select();
            //        }
            //        else if (ReasonOfFilingTextBox.Text == "")
            //        {
            //            MessageBox.Show("Please type reason of filing.", "Reason is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            ReasonOfFilingTextBox.Select();
            //        }
            //        else
            //        {
            //            if (ApplicationTypeDropdown.Text == "New Application")
            //            {
            //                //InsertSTApplication();
            //                SubmitNewApplication();
            //            }
            //            else if (ApplicationTypeDropdown.Text == "Edit Application")
            //            {

            //            }
            //            else if (ApplicationTypeDropdown.Text == "Resubmit Application")
            //            {

            //            }

            //        }
            //    }
            //    else if (ApplicationFormTypeDropdown.Text == "WC/CC")
            //    {
            //        if (CategoryDropdown.Text == "")
            //        {
            //            MessageBox.Show("Please select category.", "Category is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            CategoryDropdown.Select();
            //        }
            //        else if (EffectivityDateTimePicker.Text == "")
            //        {
            //            MessageBox.Show("Please type effectivity date.", "Effectivity date is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            EffectivityDateTimePicker.Select();
            //        }
            //        else if (ReasonOfFilingTextBox.Text == "")
            //        {
            //            MessageBox.Show("Please type reason of filing.", "Reason is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            ReasonOfFilingTextBox.Select();
            //        }
            //        else
            //        {
            //            if (ApplicationTypeDropdown.Text == "New Application")
            //            {

            //            }
            //            else if (ApplicationTypeDropdown.Text == "Edit Application")
            //            {

            //            }
            //            else if (ApplicationTypeDropdown.Text == "Resubmit Application")
            //            {

            //            }
            //        }
            //    }
            //    else if (ApplicationFormTypeDropdown.Text == "Open MH System")
            //    {
            //        if (CategoryDropdown.Text == "")
            //        {
            //            MessageBox.Show("Please select category.", "Category is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            CategoryDropdown.Select();
            //        }
            //        else if (CostCenterDropdown.Text == "")
            //        {
            //            MessageBox.Show("Please select cost center.", "Cost center is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            CostCenterDropdown.Select();
            //        }
            //        else if (WorkCenterDropdown.Text == "")
            //        {
            //            MessageBox.Show("Please select work center.", "Work center is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            WorkCenterDropdown.Select();
            //        }
            //        else if (MonthToOpenDropdown.Text == "")
            //        {
            //            MessageBox.Show("Please select month.", "Month is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            MonthToOpenDropdown.Select();
            //        }
            //        else if (ReasonOfFilingTextBox.Text == "")
            //        {
            //            MessageBox.Show("Please type reason of filing.", "Reason is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            ReasonOfFilingTextBox.Select();
            //        }
            //        else
            //        {
            //            //Insert code here...
            //        }
            //    }
            //    else if (ApplicationFormTypeDropdown.Text == "Manpower Forecasting")
            //    {
            //        if (MPFCategoryDropdown.Text == "")
            //        {
            //            MessageBox.Show("Please select category.", "Category is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            MPFCategoryDropdown.Select();
            //        }
            //        else if (MPFTargetTypeDropdown.Text == "")
            //        {
            //            MessageBox.Show("Please select target type.", "Target type is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            MPFTargetTypeDropdown.Select();
            //        }
            //        else if (MPFMonthDropdown.Text == "")
            //        {
            //            MessageBox.Show("Please select month.", "Month is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            MPFMonthDropdown.Select();
            //        }
            //        else if (MPFCostCenterDropdown.Text == "")
            //        {
            //            MessageBox.Show("Please select cost center.", "Cost center is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            MPFCostCenterDropdown.Select();
            //        }
            //        else if (MPFEffectivityDateTimePicker.Value.ToString() == "")
            //        {
            //            MessageBox.Show("Please select effectivity date.", "Effectivity date is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            MPFEffectivityDateTimePicker.Select();
            //        }
            //        else
            //        {
            //            //Insert code here...
            //        }
            //    }

            //}

        }

        private void SelectNewlyAppliedST()
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectSTData = new SqlCommand("SP_SelectNewlyAppliedSTPerCategory", con);
            SelectSTData.CommandType = CommandType.StoredProcedure;
            SelectSTData.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
            SelectSTData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectSTData);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ApplicationDataGrid.DataSource = dt;
            con.Close();

            ApplicationDataGrid.Columns["ApplicationFormNo"].Visible = false;

            if (CategoryDropdown.Text == "MH New ST Model List Form")
            {
                ApplicationDataGrid.Columns[1].Frozen = false;
                ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                ApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        string ReferenceNo;
        private void SubmitAppliactionForm()
        {
            //ST-Ink Cartridge_2023040112
            //ReferenceNo = ApplicationFormTypeDropdown.Text + "-" + CategoryDropdown.Text + "-" + Dashboard.SectionText.Replace("BIPH-", "") + "_" + DateTime.Now.ToString("yyyyMMddhhmm");

            if (ApplicationFormTypeDropdown.Text == "ST")
            {
               
            }
            else if (ApplicationFormTypeDropdown.Text == "WC/CC")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //select no.
                SqlCommand SelectApplicationFormNo = new SqlCommand("SP_SelectApplicationFormNo", con);
                SelectApplicationFormNo.CommandType = CommandType.StoredProcedure;
                SelectApplicationFormNo.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                SelectApplicationFormNo.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                SelectApplicationFormNo.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter da = new SqlDataAdapter(SelectApplicationFormNo);
                DataTable dTable = new DataTable();
                da.Fill(dTable);
                con.Close();

                if (dTable.Rows.Count > 0)
                {
                    con.Open();
                    SqlDataReader reader = SelectApplicationFormNo.ExecuteReader();
                    if (reader.Read())
                    {
                        ApplicationFormNo = reader[0].ToString(); //Application form No.
                        ReferenceNo = reader[1].ToString(); //ReferenceNo column

                        reader.Close();
                    }
                }



                //Insert application for approval 
                con.Close();
                con.Open();
                SqlCommand InsertApplicationApproval = new SqlCommand("SP_InsertApplicationApproval", con);
                InsertApplicationApproval.CommandType = CommandType.StoredProcedure;
                InsertApplicationApproval.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                InsertApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                InsertApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                InsertApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                InsertApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                InsertApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                InsertApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertApplicationApproval.Parameters.AddWithValue("@MonthToOpen", ""); //Empty parameter -> for Open MH category only
                InsertApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section SPV");
                InsertApplicationApproval.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                InsertApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                InsertApplicationApproval.Parameters.AddWithValue("@Approver", "Section SPV");
                InsertApplicationApproval.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                InsertApplicationApproval.Parameters.AddWithValue("@WithSAP", ""); //Null parameter->Not applicable for WC/CC application
                InsertApplicationApproval.ExecuteNonQuery();
                con.Close();

                //Send email notification
                SendWCCCApplicationEmailMessage();


                MessageBox.Show("Uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Show newly applied MH application if user click yes button in dialog box
                if (MessageBox.Show("Do you want to view your newly applied " + ApplicationFormTypeDropdown.Text + " application?", "MHMS Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ApplicationFormType = ApplicationFormTypeDropdown.Text;
                    Category = CategoryDropdown.Text;

                    ViewNewlyAppliedForm viewNewlyAppliedForm = new ViewNewlyAppliedForm();
                    viewNewlyAppliedForm.ShowDialog();

                }

                Dashboard.ApplicationIsSubmitted = true; //Refresh application form

                //Clear data table
                ApplicationDataGrid.DataSource = null;

                //Clear Fields
                ReasonOfApplicationTextBox.Clear();
                ApplicationFormTypeDropdown.Text = "";
                ApplicationTypeDropdown.Text = "";
                CategoryDropdown.Text = "";

                //SelectNewApplicationFormPerCategory();

                CreateFormButton.Enabled = true;
                RowCount.Enabled = true;

            }
            else if (ApplicationFormTypeDropdown.Text == "Open MH System")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //select no.
                SqlCommand SelectApplicationFormNo = new SqlCommand("SP_SelectApplicationFormNo", con);
                SelectApplicationFormNo.CommandType = CommandType.StoredProcedure;
                SelectApplicationFormNo.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                SelectApplicationFormNo.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                SelectApplicationFormNo.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter da = new SqlDataAdapter(SelectApplicationFormNo);
                DataTable dTable = new DataTable();
                da.Fill(dTable);
                con.Close();

                if (dTable.Rows.Count > 0)
                {
                    con.Open();
                    SqlDataReader reader = SelectApplicationFormNo.ExecuteReader();
                    if (reader.Read())
                    {

                        ReferenceNo = reader[1].ToString(); //ReferenceNo column

                        reader.Close();
                    }
                }


                //Insert application for approval 
                con.Close();
                con.Open();
                SqlCommand InsertApplicationApproval = new SqlCommand("SP_InsertApplicationApproval", con);
                InsertApplicationApproval.CommandType = CommandType.StoredProcedure;
                InsertApplicationApproval.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                InsertApplicationApproval.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                InsertApplicationApproval.Parameters.AddWithValue("@ApplicationCategory", CategoryDropdown.Text);
                InsertApplicationApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                InsertApplicationApproval.Parameters.AddWithValue("@ReasonOfApplication", ReasonOfApplicationTextBox.Text);
                InsertApplicationApproval.Parameters.AddWithValue("@DateTimeApplied", DateTime.Now.ToString());
                InsertApplicationApproval.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertApplicationApproval.Parameters.AddWithValue("@MonthToOpen", MonthToOpenDropdown.Text); //New added
                InsertApplicationApproval.Parameters.AddWithValue("@ApprovalStatus", "1st Approval --> Section MGR");
                InsertApplicationApproval.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                InsertApplicationApproval.Parameters.AddWithValue("@Action", "❌");
                InsertApplicationApproval.Parameters.AddWithValue("@Approver", "Section MGR");
                InsertApplicationApproval.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                InsertApplicationApproval.Parameters.AddWithValue("@WithSAP", ""); //Null parameter->Not applicable for Open MH application
                InsertApplicationApproval.ExecuteNonQuery();
                con.Close();



                //Send email notification
                SendOpenMHApplicationEmailMessage();

                MessageBox.Show("Uploaded successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Show newly applied MH application if user click yes button in dialog box
                if (MessageBox.Show("Do you want to view your newly applied " + ApplicationFormTypeDropdown.Text + " application?", "MHMS Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ApplicationFormType = ApplicationFormTypeDropdown.Text;
                    Category = CategoryDropdown.Text;

                    ViewNewlyAppliedForm viewNewlyAppliedForm = new ViewNewlyAppliedForm();
                    viewNewlyAppliedForm.ShowDialog();

                }

                Dashboard.ApplicationIsSubmitted = true; //Refresh application form

                ApplicationDataGrid.DataSource = null;
                ReasonOfApplicationTextBox.Clear();
                ApplicationFormTypeDropdown.Text = "";
                ApplicationTypeDropdown.Text = "";
                CategoryDropdown.Text = "";

                SelectNewApplicationFormPerCategory();

                CreateFormButton.Enabled = true;
                RowCount.Enabled = true;
            }
        }

        //===================================================<break>======================================================//

      

        string innerString;
        string FirstName;
        string LastName;
        string Email;

        //string Addresses;
        private void STApplicationEmailMessage()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationFormTypeDropdown.Text);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationFormTypeDropdown.Text);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join("; ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join("; ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join("; ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                foreach (DataRow row in dTable.Rows)
                {
                    FirstName = row["First Name"].ToString();
                    LastName = row["Last Name"].ToString();
                    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();
                  

                    builder.Append("Dear " + Dashboard.SectionText.Replace("BIPH-", "") + " Section MGR,");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Annual Change ST Model List Application form");
                    builder.Append("<br>");

                    //====================>>>>>>>>
                    if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                    {
                        builder.Append("<font color=blue>については、下記リンクを参照くださいインクカートリッジ年次変更ST機種一覧申請書</font>");
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "Type section here")
                    {
                        //Type other section translation here...
                    }

                    //====================>>>>>>>>

                    

                    builder.Append("<br>");

                    builder.Append("For your checking and approval");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>確認と承認をお願いします</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");

                  
                    builder.Append(ReferenceNo);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                    {
                        builder.Append("<a href= " + @"\\APBIPHSH04\B1_BIPHCommon\08_PE\03_Policy Group\01_Man Hour\00_ST Application Form\Output\Ink Cartridge>" + "Ink Cartrdige Application Form" + "</a>"); //This is the link of approval form module
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "")
                    {
                        //Type code here for other section.
                    }

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動配信されています]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>


                    //SqlCommand SelectNewForApprovalSTApplication = new SqlCommand("SP_SelectNewForApprovalSTApplication", con);
                    //SelectNewForApprovalSTApplication.CommandType = CommandType.StoredProcedure;
                    //SelectNewForApprovalSTApplication.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                    //SelectNewForApprovalSTApplication.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                    //SqlDataAdapter da = new SqlDataAdapter(SelectNewForApprovalSTApplication);
                    //DataTable dt = new DataTable();
                    //da.Fill(dt);

                    //if (dt.Rows.Count > 0)
                    //{
                    //    con.Open();
                    //    SqlDataReader reader = SelectNewForApprovalSTApplication.ExecuteReader();
                    //    if (reader.Read())
                    //    {
                    //        //Reference No
                    //        builder.Append(ReferenceNo);

                    //        reader.Close();
                    //    }
                    //}


                    //if (dt.Rows.Count > 0)
                    //{
                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", "arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "FY." + CurrentYear + ":" + " Ink Cartridge section's Annual Change ST Model List Application form.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                }

            }

        } 



        //===================================================<break>======================================================//

        string Number = "0";
        private void InsertSTApplication()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            //select no.
            SqlCommand SelectLastNo = new SqlCommand("SP_SelectSTLastNo", con);
            SelectLastNo.CommandType = CommandType.StoredProcedure;
            SelectLastNo.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
            SqlDataAdapter da = new SqlDataAdapter(SelectLastNo);
            DataTable dt = new DataTable();
            da.Fill(dt);


            if (dt.Rows.Count > 0)
            {

                SqlDataReader reader = SelectLastNo.ExecuteReader();
                if (reader.Read())
                {
                    
                    if (reader["No"].ToString() != "" || reader["No"].ToString() != null)
                    {
                        Number = reader["No"].ToString();
                    }

                    reader.Close();
                }
            }


            SqlCommand InsertSTApplication = new SqlCommand("SP_InsertSTApplication", con);
            InsertSTApplication.CommandType = CommandType.StoredProcedure;
            InsertSTApplication.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
            InsertSTApplication.Parameters.AddWithValue("@EffectivityDate", EffectivityDateTimePicker.Value.ToString("MM/dd/yyyy"));
            InsertSTApplication.Parameters.AddWithValue("@ReasonOfFiling", ReasonOfApplicationTextBox.Text);
            InsertSTApplication.Parameters.AddWithValue("@No", (Convert.ToInt32(Number) + 1).ToString());
            InsertSTApplication.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("ST application submitted successflly!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SelectAppliedSTPerCategory();
        }

        //===================================================<break>======================================================//


     

        //===================================================<break>======================================================//

        private void InsertWCCCApplication()
        {
            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //}

            //SqlCommand InsertWCCCApplication = new SqlCommand("SP_InsertWCCCApplication", con);
            //InsertWCCCApplication.CommandType = CommandType.StoredProcedure;
            //InsertWCCCApplication.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
            //InsertWCCCApplication.Parameters.AddWithValue("@EffectivityDate", EffectivityDateText.Text);
            //InsertWCCCApplication.Parameters.AddWithValue("@ReasonOfFiling", ReasonOfFilingTextBox.Text);
            //InsertWCCCApplication.ExecuteNonQuery();
            //con.Close();

            //MessageBox.Show("ST application submitted successflly!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //SelectSTPerCategory();
        }

        private void UploadMasterDataBtn_Click(object sender, EventArgs e)
        {
            if (LoginForm.UserSection == "BPS")
            {
                UploadMasterData uploadMasterDataForm = new UploadMasterData();
                uploadMasterDataForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sorry, only admin can update master data!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void ApplicationDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in ApplicationDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (ApplicationFormTypeDropdown.Text == "ST")
            {
                if (ApplicationTypeDropdown.Text == "New Application")
                {
                    ApplicationDataGrid.Columns[1].Frozen = true; //Fixed column
                    ApplicationDataGrid.Columns[1].Width = 50;
                    ApplicationDataGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                    if (CategoryDropdown.Text == "MH New ST Model List Form")
                    {
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[15].ReadOnly = false;

                        //Change back color of particular cell in datagrid
                        for (int i = 0; i < ApplicationDataGrid.Rows.Count; i++)
                        {
                            //ApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[15].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        }
                    }
                    else
                    {
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                        //STDataGrid.CurrentRow.Cells["Item Code (SAP)"].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = true;
                        //STDataGrid.CurrentRow.Cells["SAP After ST(min)"].ReadOnly = true;
                        //STDataGrid.CurrentRow.Cells["SAP After TT(min)"].ReadOnly = true;
                        //STDataGrid.CurrentRow.Cells["Item Code (MH)"].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = true;
                        //STDataGrid.CurrentRow.Cells["SAP After ST(min)"].ReadOnly = true;
                        //STDataGrid.CurrentRow.Cells["SAP After TT(min)"].ReadOnly = true;
                        //STDataGrid.CurrentRow.Cells["Effectivity Date"].ReadOnly = true;
                        //STDataGrid.CurrentRow.Cells["Reason"].ReadOnly = true;
                        //STDataGrid.CurrentRow.Cells["Remarks"].ReadOnly = true;


                        //Change back color of particular cell in datagrid
                        for (int i = 0; i < ApplicationDataGrid.Rows.Count; i++)
                        {
                            ApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(230, 230, 230); //gray

                            ApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow

                            ApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(230, 230, 230); //gray

                            ApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow

                            ApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(230, 230, 230); //gray

                            ApplicationDataGrid.Rows[i].Cells[15].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[16].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[17].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[18].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[19].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow

                        }
                    }
                }
                else if (ApplicationTypeDropdown.Text == "Edit Application")
                {
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = false;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = false;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;

                    if (e.ColumnIndex == 0 && e.Value != null)
                    {
                        //e.CellStyle.BackColor = Color.FromArgb(65, 137, 218);
                        e.CellStyle.ForeColor = Color.FromArgb(27, 88, 245);
                        ApplicationDataGrid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        ApplicationDataGrid.Columns[0].DefaultCellStyle.Font = new Font(ApplicationDataGrid.DefaultCellStyle.Font, FontStyle.Underline);
                    }

                    if (e.ColumnIndex == 6 && e.Value != null)
                    {
                        e.CellStyle.BackColor = Color.FromArgb(210, 52, 74);
                        e.CellStyle.ForeColor = Color.White;

                    }

                    ApplicationDataGrid.Columns["Action"].DefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
                    ApplicationDataGrid.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ApplicationDataGrid.Columns["Action"].Width = 80;
                    //ApplicationDataGrid.Columns["Action"].DefaultCellStyle.SelectionBackColor = Color.FromArgb(65, 137, 218);
                    ApplicationDataGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;

                }
                else if (ApplicationTypeDropdown.Text == "Resubmit Application")
                {
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;

                    if (e.ColumnIndex == 0 && e.Value != null)
                    {
                        //e.CellStyle.BackColor = Color.FromArgb(65, 137, 218);
                        e.CellStyle.ForeColor = Color.FromArgb(27, 88, 245);
                        ApplicationDataGrid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        ApplicationDataGrid.Columns[0].DefaultCellStyle.Font = new Font(ApplicationDataGrid.DefaultCellStyle.Font, FontStyle.Underline);
                    }

                    if (e.ColumnIndex == 6 && e.Value != null)
                    {
                        e.CellStyle.BackColor = Color.FromArgb(210, 52, 74);
                        e.CellStyle.ForeColor = Color.White;

                    }

                    ApplicationDataGrid.Columns["Action"].DefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
                    ApplicationDataGrid.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ApplicationDataGrid.Columns["Action"].Width = 80;
                    //ApplicationDataGrid.Columns["Action"].DefaultCellStyle.SelectionBackColor = Color.FromArgb(65, 137, 218);
                    ApplicationDataGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;

                }
            }
            else if (ApplicationFormTypeDropdown.Text == "WC/CC")
            {
                if (ApplicationTypeDropdown.Text == "New Application")
                {
                    ApplicationDataGrid.Columns[1].Width = 50;
                    ApplicationDataGrid.Columns[1].Frozen = true; //Fixed column
                    ApplicationDataGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //align column content to center

                    if (CategoryDropdown.Text == "Work Center New")
                    {
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;

                        //Change back color of particular cell in datagrid
                        for (int i = 0; i < ApplicationDataGrid.Rows.Count; i++)
                        {
                            ApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        }
                    }
                    else if (CategoryDropdown.Text == "Cost Center New")
                    {
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;

                        //Change back color of particular cell in datagrid
                        for (int i = 0; i < ApplicationDataGrid.Rows.Count; i++)
                        {
                            ApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        }
                    }
                    else if (CategoryDropdown.Text == "Work Center Revision")
                    {
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;

                        //Change back color of particular cell in datagrid
                        for (int i = 0; i < ApplicationDataGrid.Rows.Count; i++)
                        {
                            //ApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        }
                    }
                    else if (CategoryDropdown.Text == "Cost Center Revision")
                    {
                        //Type code here...
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;

                        //Change back color of particular cell in datagrid
                        for (int i = 0; i < ApplicationDataGrid.Rows.Count; i++)
                        {
                            //ApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        }
                    }
                    else if (CategoryDropdown.Text == "Work Center Deletion")
                    {
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;


                        //Change back color of particular cell in datagrid
                        for (int i = 0; i < ApplicationDataGrid.Rows.Count; i++)
                        {
                            ApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow

                        }
                    }
                    else if (CategoryDropdown.Text == "Cost Center Deletion")
                    {
                        //Type code here...
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;



                        //Change back color of particular cell in datagrid
                        for (int i = 0; i < ApplicationDataGrid.Rows.Count; i++)
                        {
                            ApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow

                        }
                    }

                }
                else if (ApplicationTypeDropdown.Text == "Edit Application")
                {
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;

                    if (e.ColumnIndex == 0 && e.Value != null)
                    {
                        //e.CellStyle.BackColor = Color.FromArgb(65, 137, 218);
                        e.CellStyle.ForeColor = Color.FromArgb(27, 88, 245);
                        ApplicationDataGrid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        ApplicationDataGrid.Columns[0].DefaultCellStyle.Font = new Font(ApplicationDataGrid.DefaultCellStyle.Font, FontStyle.Underline);
                    }

                    if (e.ColumnIndex == 6 && e.Value != null)
                    {
                        e.CellStyle.BackColor = Color.FromArgb(210, 52, 74);
                        e.CellStyle.ForeColor = Color.White;

                    }

                    ApplicationDataGrid.Columns["Action"].DefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
                    ApplicationDataGrid.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ApplicationDataGrid.Columns["Action"].Width = 80;
                    //ApplicationDataGrid.Columns["Action"].DefaultCellStyle.SelectionBackColor = Color.FromArgb(65, 137, 218);
                    ApplicationDataGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;

                }
                else if (ApplicationTypeDropdown.Text == "Resubmit Application")
                {
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;

                    if (e.ColumnIndex == 0 && e.Value != null)
                    {
                        //e.CellStyle.BackColor = Color.FromArgb(65, 137, 218);
                        e.CellStyle.ForeColor = Color.FromArgb(27, 88, 245);
                        ApplicationDataGrid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        ApplicationDataGrid.Columns[0].DefaultCellStyle.Font = new Font(ApplicationDataGrid.DefaultCellStyle.Font, FontStyle.Underline);
                    }

                    if (e.ColumnIndex == 6 && e.Value != null)
                    {
                        e.CellStyle.BackColor = Color.FromArgb(210, 52, 74);
                        e.CellStyle.ForeColor = Color.White;

                    }

                    ApplicationDataGrid.Columns["Action"].DefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
                    ApplicationDataGrid.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ApplicationDataGrid.Columns["Action"].Width = 80;
                    //ApplicationDataGrid.Columns["Action"].DefaultCellStyle.SelectionBackColor = Color.FromArgb(65, 137, 218);
                    ApplicationDataGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;

                }
            }
            else if (ApplicationFormTypeDropdown.Text == "Open MH System")
            {
                if (ApplicationTypeDropdown.Text == "New Application")
                {
                    ApplicationDataGrid.Columns[1].Width = 50;
                    ApplicationDataGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    if (CategoryDropdown.Text == "Manpower/Man-hour")
                    {
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[15].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[16].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[17].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[18].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[19].ReadOnly = false;

                        //Change back color of particular cell in datagrid
                        for (int i = 0; i < ApplicationDataGrid.Rows.Count; i++)
                        {
                            ApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[15].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[16].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[17].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[18].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[19].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        }

                    }
                    else if (CategoryDropdown.Text == "Standard Time (ST mins)")
                    {
                        ////ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells["Old"].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = true; //Difference column
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                        ////ApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                        ////ApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                        ////ApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                        ////ApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;
                        ////ApplicationDataGrid.Rows[e.RowIndex].Cells[15].ReadOnly = false;
                        ////ApplicationDataGrid.Rows[e.RowIndex].Cells[16].ReadOnly = false;
                        ////ApplicationDataGrid.Rows[e.RowIndex].Cells[17].ReadOnly = false;
                        ////ApplicationDataGrid.Rows[e.RowIndex].Cells[18].ReadOnly = false;
                        ////ApplicationDataGrid.Rows[e.RowIndex].Cells[19].ReadOnly = false;

                        //Change back color of particular cell in datagrid
                        for (int i = 0; i < ApplicationDataGrid.Rows.Count; i++)
                        {
                            ApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells["Old"].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(230, 230, 230); //Difference column //gray
                            ApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[15].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[16].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[17].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[18].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[19].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        }
                    }
                    else if (CategoryDropdown.Text == "Linestop/Loss Man-hour/Loss Factor")
                    {
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                        ApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[15].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[16].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[17].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[18].ReadOnly = false;
                        //ApplicationDataGrid.Rows[e.RowIndex].Cells[19].ReadOnly = false;

                        //Change back color of particular cell in datagrid
                        for (int i = 0; i < ApplicationDataGrid.Rows.Count; i++)
                        {
                            ApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                            ApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[15].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[16].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[17].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[18].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            //ApplicationDataGrid.Rows[i].Cells[19].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        }
                    }
                }
                else if (ApplicationTypeDropdown.Text == "Edit Application")
                {
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;

                    if (e.ColumnIndex == 0 && e.Value != null)
                    {
                        //e.CellStyle.BackColor = Color.FromArgb(65, 137, 218);
                        e.CellStyle.ForeColor = Color.FromArgb(27, 88, 245);
                        ApplicationDataGrid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        ApplicationDataGrid.Columns[0].DefaultCellStyle.Font = new Font(ApplicationDataGrid.DefaultCellStyle.Font, FontStyle.Underline);
                    }

                    if (e.ColumnIndex == 6 && e.Value != null)
                    {
                        e.CellStyle.BackColor = Color.FromArgb(210, 52, 74);
                        e.CellStyle.ForeColor = Color.White;

                    }

                    ApplicationDataGrid.Columns["Action"].DefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
                    ApplicationDataGrid.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ApplicationDataGrid.Columns["Action"].Width = 80;
                    //ApplicationDataGrid.Columns["Action"].DefaultCellStyle.SelectionBackColor = Color.FromArgb(65, 137, 218);
                    ApplicationDataGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;

                }
                else if (ApplicationTypeDropdown.Text == "Resubmit Application")
                {
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                    ApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;

                    if (e.ColumnIndex == 0 && e.Value != null)
                    {
                        //e.CellStyle.BackColor = Color.FromArgb(65, 137, 218);
                        e.CellStyle.ForeColor = Color.FromArgb(27, 88, 245);
                        ApplicationDataGrid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        ApplicationDataGrid.Columns[0].DefaultCellStyle.Font = new Font(ApplicationDataGrid.DefaultCellStyle.Font, FontStyle.Underline);
                    }

                    if (e.ColumnIndex == 6 && e.Value != null)
                    {
                        e.CellStyle.BackColor = Color.FromArgb(210, 52, 74);
                        e.CellStyle.ForeColor = Color.White;

                    }

                    ApplicationDataGrid.Columns["Action"].DefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
                    ApplicationDataGrid.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ApplicationDataGrid.Columns["Action"].Width = 80;
                    //ApplicationDataGrid.Columns["Action"].DefaultCellStyle.SelectionBackColor = Color.FromArgb(65, 137, 218);
                    ApplicationDataGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;

                }
            }
        }


        private void SubmitNewApplication()
        {
            if (ItemCodeSAP != "")
            {
                if (SAPAfterST == "")
                {
                    MessageBox.Show("Please input SAP after ST.", "Incompete input!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (SAPAfterTT == "")
                {
                    MessageBox.Show("Please input SAP after TT.", "Incompete input!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (ItemCodeMH != "")
                {
                    if (MHAfterST == "")
                    {
                        MessageBox.Show("Please input MH after ST.", "Incompete input!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (MHAfterTT == "")
                    {
                        MessageBox.Show("Please input MH after TT.", "Incompete input!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        if (EffectivityDateTimePicker.Text == "")
                        {
                            MessageBox.Show("Please type effectivity date.", "Effectivity date is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            EffectivityDateTimePicker.Select();
                        }
                        else if (ReasonOfApplicationTextBox.Text == "")
                        {
                            MessageBox.Show("Please type reason of filing.", "Reason is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            ReasonOfApplicationTextBox.Select();
                        }
                        else
                        {
                            //MessageBox.Show("All Goods na boss!");
                            SaveChanges();
                        }
                    }
                }
                else
                {
                    //MessageBox.Show("Goods na boss!");
                    SaveChanges();
                }
            }
            else if (ItemCodeMH != "")
            {
                if (MHAfterST == "")
                {
                    MessageBox.Show("Please input MH after ST.", "Incompete input!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (MHAfterTT == "")
                {
                    MessageBox.Show("Please input MH after TT.", "Incompete input!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (ItemCodeSAP != "")
                {
                    if (SAPAfterST == "")
                    {
                        MessageBox.Show("Please input SAP after ST.", "Incompete input!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (SAPAfterTT == "")
                    {
                        MessageBox.Show("Please input SAP after TT.", "Incompete input!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        if (EffectivityDateTimePicker.Text == "")
                        {
                            MessageBox.Show("Please type effectivity date.", "Effectivity date is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            EffectivityDateTimePicker.Select();
                        }
                        else if (ReasonOfApplicationTextBox.Text == "")
                        {
                            MessageBox.Show("Please type reason of filing.", "Reason is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            ReasonOfApplicationTextBox.Select();
                        }
                        else
                        {
                            //MessageBox.Show("All Goods na boss!");
                            SaveChanges();
                        }

                    }
                }
                else
                {
                    //MessageBox.Show("Goods na boss!");
                    SaveChanges();
                }
            }
        }
        

       

        private void ApplicationDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //No = ApplicationDataGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            //ItemCodeSAP = ApplicationDataGrid.Rows[e.RowIndex].Cells[3].Value.ToString(); //SAP Item Code
            //ItemCodeMH = ApplicationDataGrid.Rows[e.RowIndex].Cells[9].Value.ToString(); //MH Item Code
            //SAPAfterST = ApplicationDataGrid.Rows[e.RowIndex].Cells[7].Value.ToString();

            STCategory = CategoryDropdown.Text;

            //if (ItemCodeSAP == "" && ApplicationDataGrid.Columns[3].Name == "Item Code (SAP)")
            //{
            //    ItemCodeType = "SAP"; 

            //    ItemCodeForm itemCodeForm = new ItemCodeForm();
            //    itemCodeForm.ShowDialog();
            //}
                
            //else if (ApplicationDataGrid.Columns[9].ToString() == "Item Code (MH)")
            //{
            //    if (ItemCodeMH == "")
            //    {
            //        ItemCodeType = "MH";

            //        ItemCodeForm itemCodeForm = new ItemCodeForm();
            //        itemCodeForm.ShowDialog();
            //    }
            //}
        }

        //===================================================<break>======================================================//

        public static string ApplicationFormNo;
        public static string No;

        //ST
        public static string ItemCodeSAP;
        public static string ItemCodeMH;
        public static string Plant;
        public static string STCategory;
        public static string ItemCodeType;
        public static string SAPAfterST;
        public static string SAPAfterTT;
        public static string MHAfterST;
        public static string MHAfterTT;

        //WC/CC - New
        string WorkCenterCode;
        string WorkCenterName;
        string Shift;
        string CostCenterCode;
        string CostCenterName;
        string CostCenterGrouping;

        //WC - Revision
        string WorkCenterCodeOld;
        string WorkCenterNameOld;
        string ShiftOld;
        string PlantOld;
        string CostCenterGroupingOld;

        string WorkCenterCodeNew;
        string WorkCenterNameNew;
        string ShiftNew;
        string PlantNew;
        string CostCenterGroupingNew;

        //CC - Revision
        string CostCenterCodeOld;
        string CostCenterNameOld;
        string CostCenterCodeNew;
        string CostCenterNameNew;

        //Open MH - MP/MH
        string Date;
        decimal OperationTimeOld;
        decimal DirectOperatorOld;
        string DirectOperator_Old;
        decimal SemiDirectOperatorOld;
        decimal SemiIndirectOperatorOld;
        decimal TotalManpowerOld;
        decimal TotalManhourOld;
        decimal OperationTimeNew;
        decimal DirectOperatorNew;
        string DirectOperator_New;
        decimal SemiDirectOperatorNew;
        decimal SemiIndirectOperatorNew;
        decimal TotalManpowerNew;
        decimal TotalManhourNew;
        string ReasonOfRevision;
        //Open MH - Standard Time
        double OldST;
        double New;
        double Difference;
        //Open MH - Line Stop/ Loss MH / Loss factor
        string LinestopContentDetailOld;
        string LossFactorOld;
        string StopTimeOld;
        string SemiDirectEmployeeOld;
        string LossManhourOld;
        string LinestopContentDetailNew;
        string LossFactorNew;
        string StopTimeNew;
        string SemiDirectEmployeeNew;
        string LossManhourNew;

        string Effectivity;
        public static string Reason;
        public static string Remarks;

        private void ApplicationDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (ApplicationFormTypeDropdown.Text == "ST")
            {
                //if (ApplicationTypeDropdown.Text == "New Application")
                //{
                //    if (CategoryDropdown.Text == "MH New ST Model List Form")
                //    {
                //        //Type code here...
                //    }
                //    else
                //    {
                //        ApplicationFormNo = ApplicationDataGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                //        No = ApplicationDataGrid.Rows[e.RowIndex].Cells[1].Value.ToString();

                //        //SAP
                //        ItemCodeSAP = ApplicationDataGrid.Rows[e.RowIndex].Cells[5].Value.ToString(); //SAP Item Code column
                //        SAPAfterST = ApplicationDataGrid.Rows[e.RowIndex].Cells[9].Value.ToString(); //SAP after ST column
                //        SAPAfterTT = ApplicationDataGrid.Rows[e.RowIndex].Cells[10].Value.ToString(); //SAP after TT column

                //        //MH
                //        ItemCodeMH = ApplicationDataGrid.Rows[e.RowIndex].Cells[11].Value.ToString(); //MH Item Code column
                //        MHAfterST = ApplicationDataGrid.Rows[e.RowIndex].Cells[15].Value.ToString(); //MH after ST column
                //        MHAfterTT = ApplicationDataGrid.Rows[e.RowIndex].Cells[16].Value.ToString(); //MH after TT column

                //        Reason = ApplicationDataGrid.Rows[e.RowIndex].Cells[18].Value.ToString();
                //        Remarks = ApplicationDataGrid.Rows[e.RowIndex].Cells[19].Value.ToString(); //Remarks column

                //        //SAP_STAutoFill(); // SAP ST Auto Fill and table content update

                //        //MH_STautoFill(); // MH ST Auto Fill and table content update

                //        STApplicationAutoFill();
                //    }
                //}
            }
            else if (ApplicationFormTypeDropdown.Text == "WC/CC")
            {
                if (ApplicationTypeDropdown.Text == "New Application")
                {
                    if (CategoryDropdown.Text == "Work Center New")
                    {
                        ApplicationFormNo = ApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                        No = ApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();
                        WorkCenterCode = ApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Code"].Value.ToString();
                        WorkCenterName = ApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Name"].Value.ToString();
                        Shift = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["Shift"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                        CostCenterCode = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Code"].Value.ToString();
                        CostCenterName = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Name"].Value.ToString();
                        CostCenterGrouping = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping"].Value.ToString();
                        Plant = ApplicationDataGrid.Rows[e.RowIndex].Cells["Plant"].Value.ToString();
                        Effectivity = ApplicationDataGrid.Rows[e.RowIndex].Cells["Effectivity"].Value.ToString();
                        Remarks = ApplicationDataGrid.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();

                        //Update application
                        con.Open();
                        SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateWCCCNewApplicationForm", con);
                        FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Update WC New");
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterName", WorkCenterName);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift", Shift);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode", CostCenterCode);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterName", CostCenterName);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping", CostCenterGrouping);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant", Plant);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", EffectivityDateTimePicker.Value.ToString("MM/dd/yyyy"));
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", "");
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", "");
                        FillOutWCCCApplicationForm.ExecuteNonQuery();
                        con.Close();

                        //FillOutWCCCApplicationForm();

                    }
                    else if (CategoryDropdown.Text == "Cost Center New")
                    {
                        ApplicationFormNo = ApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                        No = ApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();
                        CostCenterCode = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Code"].Value.ToString();
                        CostCenterName = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Name"].Value.ToString();
                        Shift = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["Shift"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                        WorkCenterCode = ApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Code"].Value.ToString();
                        WorkCenterName = ApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Name"].Value.ToString();
                        CostCenterGrouping = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping"].Value.ToString();
                        Plant = ApplicationDataGrid.Rows[e.RowIndex].Cells["Plant"].Value.ToString();
                        Effectivity = ApplicationDataGrid.Rows[e.RowIndex].Cells["Effectivity"].Value.ToString();
                        Remarks = ApplicationDataGrid.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();

                        //ApplicationFormNo = ApplicationDataGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                        //No = ApplicationDataGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                        //CostCenterCode = ApplicationDataGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                        //CostCenterName = ApplicationDataGrid.Rows[e.RowIndex].Cells[3].Value.ToString();
                        //Shift = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["Shift"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                        //WorkCenterCode = ApplicationDataGrid.Rows[e.RowIndex].Cells[5].Value.ToString();
                        //WorkCenterName = ApplicationDataGrid.Rows[e.RowIndex].Cells[6].Value.ToString();
                        //CostCenterGrouping = ApplicationDataGrid.Rows[e.RowIndex].Cells[7].Value.ToString();
                        //Plant = ApplicationDataGrid.Rows[e.RowIndex].Cells[8].Value.ToString();
                        //Effectivity = ApplicationDataGrid.Rows[e.RowIndex].Cells[9].Value.ToString();
                        //Remarks = ApplicationDataGrid.Rows[e.RowIndex].Cells[10].Value.ToString();

                        //Update application
                        con.Open();
                        SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateWCCCNewApplicationForm", con);
                        FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Update WC New");
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode", CostCenterCode);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterName", CostCenterName);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift", Shift);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterName", WorkCenterName);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping", CostCenterGrouping);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant", Plant);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", EffectivityDateTimePicker.Value.ToString("MM/dd/yyyy"));
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", "");
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", "");
                        FillOutWCCCApplicationForm.ExecuteNonQuery();
                        con.Close();

                        //FillOutWCCCApplicationForm();

                    }
                    else if (CategoryDropdown.Text == "Work Center Revision")
                    {
                        ApplicationFormNo = ApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                        No = ApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();

                        WorkCenterCodeOld = ApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Code (Old)"].Value.ToString();
                        WorkCenterNameOld = ApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Name (Old)"].Value.ToString();
                        ShiftOld = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["Shift (Old)"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                        PlantOld = ApplicationDataGrid.Rows[e.RowIndex].Cells["Plant (Old)"].Value.ToString();
                        CostCenterGroupingOld = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping (Old)"].Value.ToString();

                        WorkCenterCodeNew = ApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Code (New)"].Value.ToString();
                        WorkCenterNameNew = ApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Name (New)"].Value.ToString();
                        ShiftNew = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["Shift (New)"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                        PlantNew = ApplicationDataGrid.Rows[e.RowIndex].Cells["Plant (New)"].Value.ToString();
                        CostCenterGroupingNew = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping (New)"].Value.ToString();

                        Effectivity = ApplicationDataGrid.Rows[e.RowIndex].Cells["Effectivity"].Value.ToString();
                        Remarks = ApplicationDataGrid.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();

                        if (WorkCenterCodeOld != "" && ShiftOld != "")
                        {
                            if ((ShiftOld != "B") && (ShiftOld != "Y"))
                            {
                                MessageBox.Show("Shift must be letter 'B' or 'Y' only.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                //select SAP ST from SAP master data
                                SqlCommand SelectWorkcenter = new SqlCommand("SP_SelectWorkCenterCodeFromWCMasterData", con);
                                SelectWorkcenter.CommandType = CommandType.StoredProcedure;
                                SelectWorkcenter.Parameters.AddWithValue("@Procedure", "WorkCenter");
                                SelectWorkcenter.Parameters.AddWithValue("@WorcenterCode", WorkCenterCodeOld);
                                SelectWorkcenter.Parameters.AddWithValue("@Shift", ShiftOld);
                                SelectWorkcenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SqlDataAdapter da2 = new SqlDataAdapter(SelectWorkcenter);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                con.Close();

                                if (dt2.Rows.Count > 0)
                                {
                                    con.Open();

                                    SqlDataReader reader = SelectWorkcenter.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        WorkCenterCodeOld = reader["WorkCenterCode"].ToString();
                                        WorkCenterNameOld = reader["WorkCenterName"].ToString();
                                        PlantOld = reader["Plant"].ToString();
                                        CostcenterGrouping_A = reader["CostCenterGrouping_A"].ToString();
                                        CostcenterGrouping_B = reader["CostCenterGrouping_B"].ToString();

                                        reader.Close();
                                    }

                                    //Update application
                                    con.Close();

                                    SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateWCRevisionApplicationForm", con);
                                    FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Update WC Revision");
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));

                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode_Old", WorkCenterCodeOld);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterName_Old", WorkCenterNameOld);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift_Old", ShiftOld);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant_Old", PlantOld);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping_Old", CostcenterGrouping_A + " and " + CostcenterGrouping_B);

                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode_New", WorkCenterCodeNew);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterName_New", WorkCenterNameNew);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift_New", ShiftNew);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant_New", PlantNew);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping_New", CostCenterGroupingNew);

                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", Effectivity);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", "");
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", "");
                                    con.Open();
                                    FillOutWCCCApplicationForm.ExecuteNonQuery();
                                    con.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Workcenter code is not existing in master data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                        }

                        //FillOutWCCCApplicationForm();
                    }
                    else if (CategoryDropdown.Text == "Cost Center Revision")
                    {

                        ApplicationFormNo = ApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                        No = ApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();
                        CostCenterCodeOld = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Code (Old)"].Value.ToString();
                        CostCenterNameOld = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Name (Old)"].Value.ToString();

                        ShiftOld = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["Shift (Old)"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                        PlantOld = ApplicationDataGrid.Rows[e.RowIndex].Cells["Plant (Old)"].Value.ToString();
                        CostCenterGroupingOld = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping (Old)"].Value.ToString();

                        CostCenterCodeNew = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Code (New)"].Value.ToString();
                        CostCenterNameNew = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Name (New)"].Value.ToString();
                        ShiftNew = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["Shift (New)"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                        PlantNew = ApplicationDataGrid.Rows[e.RowIndex].Cells["Plant (New)"].Value.ToString();
                        CostCenterGroupingNew = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping (New)"].Value.ToString();

                        Effectivity = ApplicationDataGrid.Rows[e.RowIndex].Cells["Effectivity"].Value.ToString();
                        Remarks = ApplicationDataGrid.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();

                        if (CostCenterCodeOld != "" && ShiftOld != "")
                        {
                            if ((ShiftOld != "B") && (ShiftOld != "Y"))
                            {
                                MessageBox.Show("Shift must be letter 'B' or 'Y' only.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                //select SAP ST from SAP master data
                                SqlCommand SelectCostcenter = new SqlCommand("SP_SelectCostCenterCodeFromWCMasterData", con);
                                SelectCostcenter.CommandType = CommandType.StoredProcedure;
                                SelectCostcenter.Parameters.AddWithValue("@Procedure", "CostCenter");
                                SelectCostcenter.Parameters.AddWithValue("@CostcenterCode", CostCenterCodeOld);
                                //SelectCostcenter.Parameters.AddWithValue("@Shift", ShiftOld);
                                SelectCostcenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SqlDataAdapter da2 = new SqlDataAdapter(SelectCostcenter);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                con.Close();

                                if (dt2.Rows.Count > 0)
                                {
                                    con.Open();

                                    SqlDataReader reader = SelectCostcenter.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        CostCenterCodeOld = reader["CostCenterCode"].ToString();
                                        CostCenterNameOld = reader["CostCenterName"].ToString();
                                        PlantOld = reader["Plant"].ToString();
                                        CostcenterGrouping_A = reader["CostCenterGrouping_A"].ToString();
                                        CostcenterGrouping_B = reader["CostCenterGrouping_B"].ToString();

                                        reader.Close();
                                    }

                                    //Update application
                                    con.Close();

                                    SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateCostCenterRevisionApplicationForm", con);
                                    FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Update CC Revision");
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));

                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode_Old", CostCenterCodeOld);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterName_Old", CostCenterNameOld);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift_Old", ShiftOld);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant_Old", PlantOld);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping_Old", CostcenterGrouping_A + " and " + CostcenterGrouping_B);

                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode_New", CostCenterCodeNew);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterName_New", CostCenterNameNew);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift_New", ShiftNew);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant_New", PlantNew);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping_New", CostCenterGroupingNew);

                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", Effectivity);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", "");
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", "");
                                    con.Open();
                                    FillOutWCCCApplicationForm.ExecuteNonQuery();
                                    con.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Costcenter code is not existing in master data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                        }
                    }
                    else if (CategoryDropdown.Text == "Work Center Deletion")
                    {
                        ApplicationFormNo = ApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                        No = ApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();
                        WorkCenterCode = ApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Code"].Value.ToString();
                        WorkCenterName = ApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Name"].Value.ToString();
                        Shift = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["Shift"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                        CostCenterGrouping = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping"].Value.ToString();
                        Plant = ApplicationDataGrid.Rows[e.RowIndex].Cells["Plant"].Value.ToString();
                        Effectivity = ApplicationDataGrid.Rows[e.RowIndex].Cells["Effectivity"].Value.ToString();
                        Remarks = ApplicationDataGrid.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();


                        if (WorkCenterCode != "" && Shift != "")
                        {
                            if ((Shift != "B") && (Shift != "Y"))
                            {
                                MessageBox.Show("Shift must be letter 'B' or 'Y' only.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                //select SAP ST from SAP master data
                                SqlCommand SelectWorkcenter = new SqlCommand("SP_SelectWorkCenterCodeFromWCMasterData", con);
                                SelectWorkcenter.CommandType = CommandType.StoredProcedure;
                                SelectWorkcenter.Parameters.AddWithValue("@Procedure", "WorkCenter");
                                SelectWorkcenter.Parameters.AddWithValue("@WorcenterCode", WorkCenterCode);
                                SelectWorkcenter.Parameters.AddWithValue("@Shift", Shift);
                                SelectWorkcenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SqlDataAdapter da2 = new SqlDataAdapter(SelectWorkcenter);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                con.Close();

                                if (dt2.Rows.Count > 0)
                                {
                                    con.Open();

                                    SqlDataReader reader = SelectWorkcenter.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        WorkCenterName = reader["WorkCenterName"].ToString();
                                        Shift = reader["Shift"].ToString();
                                        Plant = reader["Plant"].ToString();
                                        CostcenterGrouping_A = reader["CostCenterGrouping_A"].ToString();
                                        CostcenterGrouping_B = reader["CostCenterGrouping_B"].ToString();

                                        reader.Close();
                                    }

                                    //Update application
                                    con.Close();
                                    SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateWCDeletionApplicationForm", con);
                                    FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Update WC Deletion");
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));

                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterName", WorkCenterName);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift", Shift);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant", Plant);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping", CostcenterGrouping_A + " and " + CostcenterGrouping_B);

                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", EffectivityDateTimePicker.Value.ToString("MM/dd/yyyy"));
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", "");
                                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", "");
                                    con.Open();
                                    FillOutWCCCApplicationForm.ExecuteNonQuery();
                                    con.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Workcenter code is not existing in master data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }


                        //FillOutWCCCApplicationForm();

                    }
                    else if (CategoryDropdown.Text == "Cost Center Deletion")
                    {
                        ApplicationFormNo = ApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                        No = ApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();
                        CostCenterCode = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Code"].Value.ToString();
                        CostCenterName = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Name"].Value.ToString();
                        CostCenterGrouping = ApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping"].Value.ToString();
                        Plant = ApplicationDataGrid.Rows[e.RowIndex].Cells["Plant"].Value.ToString();
                        Effectivity = ApplicationDataGrid.Rows[e.RowIndex].Cells["Effectivity"].Value.ToString();
                        Remarks = ApplicationDataGrid.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();


                        if (CostCenterCode != "")
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            //select SAP ST from SAP master data
                            SqlCommand SelectCostcenter = new SqlCommand("SP_SelectCostCenterCodeFromWCMasterData", con);
                            SelectCostcenter.CommandType = CommandType.StoredProcedure;
                            SelectCostcenter.Parameters.AddWithValue("@Procedure", "CostCenter");
                            SelectCostcenter.Parameters.AddWithValue("@CostcenterCode", CostCenterCode);
                            SelectCostcenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter da2 = new SqlDataAdapter(SelectCostcenter);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);
                            con.Close();

                            if (dt2.Rows.Count > 0)
                            {
                                con.Open();

                                SqlDataReader reader = SelectCostcenter.ExecuteReader();
                                if (reader.Read())
                                {
                                    CostCenterName = reader["CostCenterName"].ToString();
                                    Plant = reader["Plant"].ToString();
                                    CostcenterGrouping_A = reader["CostCenterGrouping_A"].ToString();
                                    CostcenterGrouping_B = reader["CostCenterGrouping_B"].ToString();

                                    reader.Close();
                                }

                                //Update application
                                con.Close();
                                SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateCostCenterDeletionApplicationForm", con);
                                FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Update CC Deletion");
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));

                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode", CostCenterCode);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterName", CostCenterName);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant", Plant);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping", CostcenterGrouping_A + " and " + CostcenterGrouping_B);

                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", EffectivityDateTimePicker.Value.ToString("MM/dd/yyyy"));
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", "");
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", "");
                                con.Open();
                                FillOutWCCCApplicationForm.ExecuteNonQuery();
                                con.Close();
                            }
                            else
                            {
                                MessageBox.Show("Costcenter code is not existing in master data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        //FillOutWCCCApplicationForm();
                    }
                }
            }
            else if (ApplicationFormTypeDropdown.Text == "Open MH System")
            {
                if (ApplicationTypeDropdown.Text == "New Application")
                {
                    if (CategoryDropdown.Text == "Manpower/Man-hour")
                    {
                        ApplicationFormNo = ApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                        No = ApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();

                        Date = ApplicationDataGrid.Rows[e.RowIndex].Cells["Date"].Value.ToString();
                        Category = ApplicationDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString();
                        WorkCenterCode = ApplicationDataGrid.Rows[e.RowIndex].Cells["WorkCenterCode"].Value.ToString();
                        CostCenterCode = ApplicationDataGrid.Rows[e.RowIndex].Cells["CostCenterCode"].Value.ToString();
                        Shift = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["Shift"] as DataGridViewComboBoxCell).FormattedValue.ToString());

                        if (ApplicationDataGrid.Rows[e.RowIndex].Cells["Operation Time (Old)"].Value.ToString() == "")
                        {
                            OperationTimeOld = 0;
                        }
                        else
                        {
                            OperationTimeOld = Convert.ToDecimal(ApplicationDataGrid.Rows[e.RowIndex].Cells["Operation Time (Old)"].Value);
                        }

                        if (ApplicationDataGrid.Rows[e.RowIndex].Cells["Direct Operator (Old)"].Value.ToString() == "")
                        {
                            DirectOperatorOld = 0;
                        }
                        else
                        {
                            DirectOperatorOld = Convert.ToDecimal(ApplicationDataGrid.Rows[e.RowIndex].Cells["Direct Operator (Old)"].Value);
                        }

                        if (ApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-direct Operator (Old)"].Value.ToString() == "")
                        {
                            SemiDirectOperatorOld = 0;
                        }
                        else
                        {
                            SemiDirectOperatorOld = Convert.ToDecimal(ApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-direct Operator (Old)"].Value);
                        }

                        if (ApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-Indirect Operator (Old)"].Value.ToString() == "")
                        {
                            SemiIndirectOperatorOld = 0;
                        }
                        else
                        {
                            SemiIndirectOperatorOld = Convert.ToDecimal(ApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-Indirect Operator (Old)"].Value);
                        }

                        //Total manpower computation
                        TotalManpowerOld = DirectOperatorOld + SemiDirectOperatorOld + SemiIndirectOperatorOld;

                        //Total manhour computation
                        TotalManhourOld = Math.Round(Convert.ToDecimal((OperationTimeOld / 60) * TotalManpowerOld), 2);

                        if (ApplicationDataGrid.Rows[e.RowIndex].Cells["Operation Time (New)"].Value.ToString() == "")
                        {
                            OperationTimeNew = 0;
                        }
                        else
                        {
                            OperationTimeNew = Convert.ToDecimal(ApplicationDataGrid.Rows[e.RowIndex].Cells["Operation Time (New)"].Value);
                        }


                        if (ApplicationDataGrid.Rows[e.RowIndex].Cells["Direct Operator (New)"].Value.ToString() == "")
                        {
                            DirectOperatorNew = 0;
                        }
                        else
                        {
                            DirectOperatorNew = Convert.ToDecimal(ApplicationDataGrid.Rows[e.RowIndex].Cells["Direct Operator (New)"].Value);
                        }


                        if (ApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-direct Operator (New)"].Value.ToString() == "")
                        {
                            SemiDirectOperatorNew = 0;
                        }
                        else
                        {
                            SemiDirectOperatorNew = Convert.ToDecimal(ApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-direct Operator (New)"].Value);
                        }


                        if (ApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-Indirect Operator (New)"].Value.ToString() == "")
                        {
                            SemiIndirectOperatorNew = 0;
                        }
                        else
                        {
                            SemiIndirectOperatorNew = Convert.ToDecimal(ApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-Indirect Operator (New)"].Value);
                        }
                       

                        TotalManpowerNew = DirectOperatorNew + SemiDirectOperatorNew + SemiIndirectOperatorNew;

                        TotalManhourNew = Math.Round(Convert.ToDecimal((OperationTimeNew / 60) * TotalManpowerNew), 2);

                        ReasonOfRevision = ApplicationDataGrid.Rows[e.RowIndex].Cells["Reason of Revision"].Value.ToString();

                        
                        if (WorkCenterCode != "")
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            //select SAP ST from SAP master data
                            SqlCommand SelectCostcenter = new SqlCommand("SP_SelectWorkCenterCodeFromopenMHMasterData", con);
                            SelectCostcenter.CommandType = CommandType.StoredProcedure;
                            SelectCostcenter.Parameters.AddWithValue("@Procedure", "WorkCenterCode");
                            SelectCostcenter.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                            //SelectCostcenter.Parameters.AddWithValue("@Shift", ShiftOld);
                            SelectCostcenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter da2 = new SqlDataAdapter(SelectCostcenter);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);
                            con.Close();

                            if (dt2.Rows.Count == 0)
                            {
                                MessageBox.Show("Workcenter is not existing in master data.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                SubmitButton.Enabled = false;
                            }
                            else
                            {
                                SubmitButton.Enabled = true;

                                //Update application
                                con.Open();
                                SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateManpowerManhourApplicationForm", con);
                                FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Update ManpowerManhour");

                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Date", Date);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", Category);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode", CostCenterCode);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift", Shift);

                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@OperationTimeOld", OperationTimeOld);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DirectOperatorOld", DirectOperatorOld);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@SemiDirectOperatorOld", SemiDirectOperatorOld);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@SemiIndirectOperatorOld", SemiIndirectOperatorOld);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@TotalManpowerOld", TotalManpowerOld);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@TotalManhourOld", TotalManhourOld);

                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@OperationTimeNew", OperationTimeNew);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DirectOperatorNew", DirectOperatorNew);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@SemiDirectOperatorNew", SemiIndirectOperatorNew);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@SemiIndirectOperatorNew", SemiIndirectOperatorNew);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@TotalManpowerNew", TotalManpowerNew);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@TotalManhourNew", TotalManhourNew);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReasonOfRevision", ReasonOfRevision);

                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", "");
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", "");
                                FillOutWCCCApplicationForm.ExecuteNonQuery();
                                con.Close();
                            }

                        }
                        

                    }
                    else if (CategoryDropdown.Text == "Standard Time (ST mins)")
                    {
                        ApplicationFormNo = ApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();

                        No = ApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();

                        Date = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["Date"] as DataGridViewComboBoxCell).FormattedValue.ToString());

                        CostCenterCode = ApplicationDataGrid.Rows[e.RowIndex].Cells["CostCenter"].Value.ToString();

                        WorkCenterCode = ApplicationDataGrid.Rows[e.RowIndex].Cells["WorkCenter"].Value.ToString();

                        Shift = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["Shift"] as DataGridViewComboBoxCell).FormattedValue.ToString());

                        ItemCode = ApplicationDataGrid.Rows[e.RowIndex].Cells["Item Code"].Value.ToString();

                        if (ApplicationDataGrid.Rows[e.RowIndex].Cells["Old"].Value.ToString() == "")
                        {
                            OldST = 0;
                        }
                        else
                        {
                            OldST = Convert.ToDouble(ApplicationDataGrid.Rows[e.RowIndex].Cells["Old"].Value);
                        }

                        if (ApplicationDataGrid.Rows[e.RowIndex].Cells["New"].Value.ToString() == "")
                        {
                            New = 0;
                        }
                        else
                        {
                            New = Convert.ToDouble(ApplicationDataGrid.Rows[e.RowIndex].Cells["New"].Value);
                        }
                      
                        ReasonOfRevision = ApplicationDataGrid.Rows[e.RowIndex].Cells["Reason of Revision"].Value.ToString();

                        if (ItemCode != "")
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            //select SAP ST from SAP master data
                            SqlCommand SelectItemCode= new SqlCommand("SP_SelectItemCodeFromOpemMHMasterData", con);
                            SelectItemCode.CommandType = CommandType.StoredProcedure;
                            //SelectCostcenter.Parameters.AddWithValue("@Procedure", "CostCenter");
                            SelectItemCode.Parameters.AddWithValue("@ItemCode", ItemCode);
                            SelectItemCode.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter da2 = new SqlDataAdapter(SelectItemCode);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);
                            con.Close();

                            if (dt2.Rows.Count > 0)
                            {
                                con.Open();

                                SqlDataReader reader = SelectItemCode.ExecuteReader();
                                if (reader.Read())
                                {
                                    //ItemCode = reader["ItemCode"].ToString();
                                    OldST = Convert.ToDouble(reader["OldST"].ToString());

                                    reader.Close();
                                }

                                Difference = OldST - New; //Get Difference

                                //Update application
                                con.Close();
                                SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateStandardTimeApplicationForm", con);
                                FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Update Standard Time");

                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Date", Date);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode", CostCenterCode);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift", Shift);

                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ItemCode", ItemCode);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Old", OldST);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@New", New);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Difference", Difference);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReasonOfRevision", ReasonOfRevision);

                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", "");
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", "");
                                con.Open();
                                FillOutWCCCApplicationForm.ExecuteNonQuery();
                                con.Close();

                            }
                            else
                            {
                                MessageBox.Show("Item code is not existing in master data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        
                    }
                    else if (CategoryDropdown.Text == "Linestop/Loss Man-hour/Loss Factor")
                    {
                        ApplicationFormNo = ApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                        No = ApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();

                        Date = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["Date"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                        WorkCenterCode = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["WorkCenterCode"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                        CostCenterCode = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["CostCenterCode"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                        Shift = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["Shift"] as DataGridViewComboBoxCell).FormattedValue.ToString());

                        LinestopContentDetailOld = ApplicationDataGrid.Rows[e.RowIndex].Cells["Line Stop Content Detail (Old)"].Value.ToString();
                        LossFactorOld = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["LossFactorOld"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                        StopTimeOld = ApplicationDataGrid.Rows[e.RowIndex].Cells["Stop Time (Old)"].Value.ToString();
                        DirectOperator_Old= ApplicationDataGrid.Rows[e.RowIndex].Cells["Direct Operator (Old)"].Value.ToString();
                        SemiDirectEmployeeOld = ApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-direct Employee (Old)"].Value.ToString();
                        LossManhourOld = ApplicationDataGrid.Rows[e.RowIndex].Cells["Loss Manhour (Old)"].Value.ToString();

                        LinestopContentDetailNew = ApplicationDataGrid.Rows[e.RowIndex].Cells["Line Stop Content Detail (New)"].Value.ToString();
                        LossFactorNew = Convert.ToString((ApplicationDataGrid.Rows[e.RowIndex].Cells["LossFactorNew"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                        StopTimeNew = ApplicationDataGrid.Rows[e.RowIndex].Cells["Stop Time (New)"].Value.ToString();
                        DirectOperator_New = ApplicationDataGrid.Rows[e.RowIndex].Cells["Direct Operator (New)"].Value.ToString();
                        SemiDirectEmployeeNew = ApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-direct Employee (New)"].Value.ToString();
                        LossManhourNew = ApplicationDataGrid.Rows[e.RowIndex].Cells["Loss Manhour (New)"].Value.ToString();

                        ReasonOfRevision = ApplicationDataGrid.Rows[e.RowIndex].Cells["Reason of Revision"].Value.ToString();

                        //Update application
                        con.Open();
                        SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateLinestop_LossManhour_LossFactorApplicationForm", con);
                        FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Update Linestop_LossManhour_LossFactor");

                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));

                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Date", Date);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode", CostCenterCode);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift", Shift);

                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@LinestopContentDetailOld", LinestopContentDetailOld);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@LossFactorOld", LossFactorOld);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@StopTimeOld", StopTimeOld);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@DirectOperatorOld", DirectOperator_Old);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@SemiDirectEmployeeOld", SemiDirectEmployeeOld);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@LossManhourOld", LossManhourOld);

                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@LinestopContentDetailNew", LinestopContentDetailNew);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@LossFactorNew", LossFactorNew);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@StopTimeNew", StopTimeNew);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@DirectOperatorNew", DirectOperator_New);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@SemiDirectEmployeeNew", SemiDirectEmployeeNew);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@LossManhourNew", LossManhourNew);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReasonOfRevision", ReasonOfRevision);

                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", "");
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", "");
                        FillOutWCCCApplicationForm.ExecuteNonQuery();
                        con.Close();

                    }
                }
            }

            //AutoFillIsDone = true;
        }

        //===================================================<break>======================================================//

        private void SaveChanges()
        {
            //SAP_STAutoFill();
            //MH_STautoFill();
        }

        //===================================================<break>======================================================//

        string ItemCode;
        string ItemName;
        string Section;
        string SAPBeforeST;
        string SAPBeforeTT;
        string MHBeforeST;
        string MHBeforeTT;


        private void STApplicationAutoFill()
        {
            //if (CategoryDropdown.Text == "Annual ST Change")
            //{
                
                if (ItemCodeSAP != "")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    //select SAP ST from SAP master data
                    SqlCommand SelectSTItemCode = new SqlCommand("SP_SelectSTItemCodeFromMasterData", con);
                    SelectSTItemCode.CommandType = CommandType.StoredProcedure;
                    SelectSTItemCode.Parameters.AddWithValue("@Procedure", "SAP");
                    SelectSTItemCode.Parameters.AddWithValue("@ItemCode", ItemCodeSAP);
                    SelectSTItemCode.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SqlDataAdapter da = new SqlDataAdapter(SelectSTItemCode);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        con.Open();

                        SqlDataReader reader = SelectSTItemCode.ExecuteReader();
                        if (reader.Read())
                        {
                            ItemCode = reader["ItemCodeSAP"].ToString();
                            ItemName = reader["ItemNameSAP"].ToString();
                            Plant = reader["Plant"].ToString();
                            Section = reader["Section"].ToString();
                            SAPBeforeST = reader["SAPBeforeST"].ToString();
                            SAPBeforeTT = reader["SAPBeforeTT"].ToString();

                            reader.Close();
                        }

                    }



                    con.Close();
                    //Update application
                    SqlCommand AutoFillSTApplication = new SqlCommand("SP_UpdateSAPSTApplication", con);
                    AutoFillSTApplication.CommandType = CommandType.StoredProcedure;
                    AutoFillSTApplication.Parameters.AddWithValue("@STcategory", STCategory);
                    AutoFillSTApplication.Parameters.AddWithValue("@ApplicationFormNo", ApplicationFormNo);
                    AutoFillSTApplication.Parameters.AddWithValue("@No", No);
                    AutoFillSTApplication.Parameters.AddWithValue("@Section", Section);
                    AutoFillSTApplication.Parameters.AddWithValue("@Plant", Plant);
                    AutoFillSTApplication.Parameters.AddWithValue("@ItemCode", ItemCode);
                    AutoFillSTApplication.Parameters.AddWithValue("@ItemName", ItemName);
                    AutoFillSTApplication.Parameters.AddWithValue("@SAPBeforeST", SAPBeforeST);
                    AutoFillSTApplication.Parameters.AddWithValue("@SAPBeforeTT", SAPBeforeTT);
                    AutoFillSTApplication.Parameters.AddWithValue("@SAPAfterST", SAPAfterST);
                    AutoFillSTApplication.Parameters.AddWithValue("@SAPAfterTT", SAPAfterTT);
                    AutoFillSTApplication.Parameters.AddWithValue("@EffectivityDate", EffectivityDateTimePicker.Value.ToString("MM/dd/yyyy"));
                    AutoFillSTApplication.Parameters.AddWithValue("@Reason", Reason);
                    AutoFillSTApplication.Parameters.AddWithValue("@Remarks", Remarks);
                    AutoFillSTApplication.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                    AutoFillSTApplication.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                    con.Open();
                    AutoFillSTApplication.ExecuteNonQuery();
                    con.Close();

                    //FOR NEXT WEEK TASK
                    //Select Newly Applied ST application to datagrid


                    //UpdateAnnualChangeST();


                }


                if (ItemCodeMH != "")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    //select MH ST from SAP master data
                    SqlCommand SelectSTItemCode = new SqlCommand("SP_SelectSTItemCodeFromMasterData", con);
                    SelectSTItemCode.CommandType = CommandType.StoredProcedure;
                    SelectSTItemCode.Parameters.AddWithValue("@Procedure", "MH");
                    SelectSTItemCode.Parameters.AddWithValue("@ItemCode", ItemCodeMH);
                    SelectSTItemCode.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SqlDataAdapter da = new SqlDataAdapter(SelectSTItemCode);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        con.Open();

                        SqlDataReader reader = SelectSTItemCode.ExecuteReader();
                        if (reader.Read())
                        {
                            ItemCode = reader["ItemCodeMH"].ToString();
                            ItemName = reader["ItemNameMH"].ToString();
                            Plant = reader["Plant"].ToString();
                            Section = reader["Section"].ToString();
                            MHBeforeST = reader["MHBeforeST"].ToString();
                            MHBeforeTT = reader["MHBeforeTT"].ToString();

                            reader.Close();
                        }

                    }

                    con.Close();
                    //Update application
                    SqlCommand AutoFillSTApplication = new SqlCommand("SP_UpdateMHSTApplication", con);
                    AutoFillSTApplication.CommandType = CommandType.StoredProcedure;
                    AutoFillSTApplication.Parameters.AddWithValue("@STcategory", STCategory);
                    AutoFillSTApplication.Parameters.AddWithValue("@No", No);
                    AutoFillSTApplication.Parameters.AddWithValue("@Section", Section);
                    AutoFillSTApplication.Parameters.AddWithValue("@Plant", Plant);
                    AutoFillSTApplication.Parameters.AddWithValue("@ItemCode", ItemCode);
                    AutoFillSTApplication.Parameters.AddWithValue("@ItemName", ItemName);
                    AutoFillSTApplication.Parameters.AddWithValue("@MHBeforeST", MHBeforeST);
                    AutoFillSTApplication.Parameters.AddWithValue("@MHBeforeTT", MHBeforeTT);
                    AutoFillSTApplication.Parameters.AddWithValue("@MHAfterST", MHAfterST);
                    AutoFillSTApplication.Parameters.AddWithValue("@MHAfterTT", MHAfterTT);
                    AutoFillSTApplication.Parameters.AddWithValue("@EffectivityDate", EffectivityDateTimePicker.Value.ToString("MM/dd/yyyy"));
                    AutoFillSTApplication.Parameters.AddWithValue("@Reason", Reason);
                    AutoFillSTApplication.Parameters.AddWithValue("@Remarks", Remarks);
                    AutoFillSTApplication.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                    AutoFillSTApplication.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                    con.Open();
                    AutoFillSTApplication.ExecuteNonQuery();
                    con.Close();
                }
                
                
            //}
            //else if (CategoryDropdown.Text == "Annual ST Change")
            //{
            //    //Type code here...
            //}
            
           
        }

        //================================================================<BreakLine>======================================================>>> 

        //string CostCenterCodeOld;
        //string CostCenterNameOld;
        //string WorkCenterCodeOld;
        //string WorkCenterNameOld;
        //string PlantOld;
        string CostcenterGrouping_A;
        string CostcenterGrouping_B;

        private void FillOutWCCCApplicationForm()
        {
            if (CategoryDropdown.Text == "Work Center New")
            {
                //Update application
                con.Open();
                SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateWCCCNewApplicationForm", con);
                FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Update WC New");
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterName", WorkCenterName);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift", Shift);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode", CostCenterCode);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterName", CostCenterName);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping", CostCenterGrouping);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant", Plant);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", EffectivityDateTimePicker.Value.ToString("MM/dd/yyyy"));
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", "");
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", "");
                FillOutWCCCApplicationForm.ExecuteNonQuery();
                con.Close();

            }
            else if (CategoryDropdown.Text == "Cost Center New")
            {
                //Update application
                con.Open();
                SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateWCCCNewApplicationForm", con);
                FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Update WC New");
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode", CostCenterCode);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterName", CostCenterName);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift", Shift);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterName", WorkCenterName);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping", CostCenterGrouping);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant", Plant);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", EffectivityDateTimePicker.Value.ToString("MM/dd/yyyy"));
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", "");
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", "");
                FillOutWCCCApplicationForm.ExecuteNonQuery();
                con.Close();
            }
            else if (CategoryDropdown.Text == "Work Center Revision")
            {

                if (WorkCenterCodeOld != "" && ShiftOld != "")
                {
                    if ((ShiftOld != "B") && (ShiftOld != "Y"))
                    {
                        MessageBox.Show("Shift must be letter 'B' or 'Y' only.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        //select SAP ST from SAP master data
                        SqlCommand SelectWorkcenter = new SqlCommand("SP_SelectWorkCenterCodeFromWCMasterData", con);
                        SelectWorkcenter.CommandType = CommandType.StoredProcedure;
                        SelectWorkcenter.Parameters.AddWithValue("@Procedure", "WorkCenter");
                        SelectWorkcenter.Parameters.AddWithValue("@WorcenterCode", WorkCenterCodeOld);
                        SelectWorkcenter.Parameters.AddWithValue("@Shift", ShiftOld);
                        SelectWorkcenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter da2 = new SqlDataAdapter(SelectWorkcenter);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        con.Close();

                        if (dt2.Rows.Count > 0)
                        {
                            con.Open();

                            SqlDataReader reader = SelectWorkcenter.ExecuteReader();
                            if (reader.Read())
                            {
                                WorkCenterCodeOld = reader["WorkCenterCode"].ToString();
                                WorkCenterNameOld = reader["WorkCenterName"].ToString();
                                PlantOld = reader["Plant"].ToString();
                                CostcenterGrouping_A = reader["CostCenterGrouping_A"].ToString();
                                CostcenterGrouping_B = reader["CostCenterGrouping_B"].ToString();

                                reader.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Workcenter code is not existing in master data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    
                }


                //Update application
                con.Close();

                SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateWCCCRevisionApplicationForm", con);
                FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Update WC Revision");
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));

                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode_Old", WorkCenterCodeOld);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterName_Old", WorkCenterNameOld);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift_Old", ShiftOld);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant_Old", PlantOld);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping_Old", CostcenterGrouping_A + " " + CostcenterGrouping_B);

                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode_New", WorkCenterCodeNew);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterName_New", WorkCenterNameNew);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift_New", ShiftNew);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant_New", PlantNew);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping_New", CostCenterGroupingNew);

                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", Effectivity);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", "");
                FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", "");
                con.Open();
                FillOutWCCCApplicationForm.ExecuteNonQuery();
                con.Close();
            }
            else if (CategoryDropdown.Text == "Cost Center Revision")
            {

            }
            else if (CategoryDropdown.Text == "Work Center Deletion")
            {
                if (WorkCenterCode != "" && Shift != "")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    //select SAP ST from SAP master data
                    SqlCommand SelectWorkcenter = new SqlCommand("SP_SelectWorkCenterCodeFromWCMasterData", con);
                    SelectWorkcenter.CommandType = CommandType.StoredProcedure;
                    SelectWorkcenter.Parameters.AddWithValue("@Procedure", "WorkCenter");
                    SelectWorkcenter.Parameters.AddWithValue("@WorcenterCode", WorkCenterCode);
                    SelectWorkcenter.Parameters.AddWithValue("@Shift", Shift);
                    SelectWorkcenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SqlDataAdapter da2 = new SqlDataAdapter(SelectWorkcenter);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    con.Close();

                    if (dt2.Rows.Count > 0)
                    {
                        con.Open();

                        SqlDataReader reader = SelectWorkcenter.ExecuteReader();
                        if (reader.Read())
                        {
                            WorkCenterName = reader["WorkCenterName"].ToString();
                            Shift = reader["Shift"].ToString();
                            Plant = reader["Plant"].ToString();
                            CostcenterGrouping_A = reader["CostCenterGrouping_A"].ToString();
                            CostcenterGrouping_B = reader["CostCenterGrouping_B"].ToString();

                            reader.Close();
                        }

                        //Update application
                        con.Close();
                        SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateWCCCDeletionApplicationForm", con);
                        FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Update WC Deletion");
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));

                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterName", WorkCenterName);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift", Shift);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant", Plant);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping", CostcenterGrouping_A + " " + CostcenterGrouping_B);

                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", EffectivityDateTimePicker.Value.ToString("MM/dd/yyyy"));
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", "");
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", "");
                        con.Open();
                        FillOutWCCCApplicationForm.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Workcenter code or shift is not existing in master data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }
            else if (CategoryDropdown.Text == "Cost Center Deletion")
            {
                //Type code here...
                if (CostCenterCode != "")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    //select SAP ST from SAP master data
                    SqlCommand SelectWorkcenter = new SqlCommand("SP_SelectCostCenterCodeFromWCMasterData", con);
                    SelectWorkcenter.CommandType = CommandType.StoredProcedure;
                    SelectWorkcenter.Parameters.AddWithValue("@Procedure", "CostCenter");
                    SelectWorkcenter.Parameters.AddWithValue("@CostcenterCode", CostCenterCode);
                    SelectWorkcenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SqlDataAdapter da2 = new SqlDataAdapter(SelectWorkcenter);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    con.Close();

                    if (dt2.Rows.Count > 0)
                    {
                        con.Open();

                        SqlDataReader reader = SelectWorkcenter.ExecuteReader();
                        if (reader.Read())
                        {
                            CostCenterName = reader["CostCenterName"].ToString();
                            Plant = reader["Plant"].ToString();
                            CostcenterGrouping_A = reader["CostCenterGrouping_A"].ToString();
                            CostcenterGrouping_B = reader["CostCenterGrouping_B"].ToString();

                            reader.Close();
                        }

                        //Update application
                        con.Close();
                        SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateCostCenterDeletionApplicationForm", con);
                        FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Update CC Deletion");
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));

                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode", CostCenterCode);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterName", CostCenterName);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant", Plant);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping", CostcenterGrouping_A + " " + CostcenterGrouping_B);

                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", EffectivityDateTimePicker.Value.ToString("MM/dd/yyyy"));
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", DateTime.Now.ToString());
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", "");
                        FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", "");
                        con.Open();
                        FillOutWCCCApplicationForm.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Costcenter code is not existing in master data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //================================================================<BreakLine>======================================================>>> 

        private void SendWCCCApplicationEmailMessage()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionSPV");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationFormTypeDropdown.Text);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationFormTypeDropdown.Text);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationFormTypeDropdown.Text);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                foreach (DataRow row in dTable.Rows)
                {
                    FirstName = row["First Name"].ToString();
                    LastName = row["Last Name"].ToString();
                    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();
                   

                    builder.Append("Dear " + Dashboard.SectionText.Replace("BIPH-", "") + " Section SPV,");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>お疲れ様です。</font>");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    if (CategoryDropdown.Text == "Work Center New")
                    {
                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課の新規ワークセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課の新規ワークセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");

                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課の新規ワークセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課の新規ワークセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課の新規ワークセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課の新規ワークセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課の新規ワークセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課の新規ワークセンター登録申請の連絡です。</font>");
                            //builder.Append("<br>");
                        }

                    }
                    else if (CategoryDropdown.Text == "Work Center Revision")
                    {
                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課のワークセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課のワークセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");

                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課のワークセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課のワークセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課のワークセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")  
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課のワークセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課のワークセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("This is a request for Revision of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課のワークセンターの改訂申請の連絡です。</font>");
                            //builder.Append("<br>");
                        }


                    }
                    else if (CategoryDropdown.Text == "Work Center Deletion")
                    {
                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課のワークセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課のワークセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");

                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課のワークセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課のワークセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課のワークセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課のワークセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課のワークセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("This is a request for Deletion of Work Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課のワークセンターの削除申請の連絡です。</font>");
                            //builder.Append("<br>");
                        }


                    }
                    else if (CategoryDropdown.Text == "Cost Center New")
                    {
                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課の新規コストセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課の新規コストセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");

                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課の新規コストセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課の新規コストセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課の新規コストセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課の新規コストセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課の新規コストセンター登録申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("This is a request for New Cost Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課の新規コストセンター登録申請の連絡です。</font>");
                            //builder.Append("<br>");
                        }


                    }
                    else if (CategoryDropdown.Text == "Cost Center Revision")
                    {
                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課のコストセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課のコストセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");

                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課のコストセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課のコストセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課のコストセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課のコストセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課のコストセンターの改訂申請の連絡です。</font>");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("This is a request for Revision of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課のコストセンターの改訂申請の連絡です。</font>");
                            //builder.Append("<br>");
                        }


                    }
                    else if (CategoryDropdown.Text == "Cost Center Deletion")
                    {
                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課のコストセンターの削除申請の連絡です。</font>"); 
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課のコストセンターの削除申請の連絡です。</font>");
                            builder.Append("<br>");

                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課のコストセンターの削除申請の連絡です。</font>"); 
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課のコストセンターの削除申請の連絡です。</font>"); 
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課のコストセンターの削除申請の連絡です。</font>"); 
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課のコストセンターの削除申請の連絡です。</font>"); 
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課のコストセンターの削除申請の連絡です。</font>"); 
                            builder.Append("<br>");
                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                        {
                            builder.Append("This is a request for Deletion of Cost Center for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課のコストセンターの削除申請の連絡です。</font>");
                            //builder.Append("<br>");
                        }

                    }


                   
                    builder.Append("<b>For your approval.</b>");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>承認待ち</font>");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                    builder.Append("<br>");
                    builder.Append("<b><i>" + ReferenceNo + "</i></b>");
                    builder.Append("<br><br><br>");

                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>"); //This is the link of approval form module

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("In case a problem occurred in the application file, kindly inform the mailing list below.");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>申請ファイルに問題が生じた場合は、下記のメーリングリストに連絡下さい</font>");
                    builder.Append("<br><br>");

                    builder.Append("<b>PM Group Mailing List</b>");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>PM Gメーリングリスト</font>");
                    builder.Append("<br>");
                    builder.Append("Bautista, Princess (BIPH-PE) <princess.bautista@brother-biph.com.ph>");
                    builder.Append("<br>");
                    builder.Append("Tan, Lina (BIPH-PE)  <lina.tan@brother-biph.com.ph>");
                    builder.Append("<br>");
                    builder.Append("Mateo, Bradly (BIPH-PE) <bradly.mateo@brother-biph.com.ph>");
                    builder.Append("<br>");
                    builder.Append("Dimayuga, Jeancy (BIPH-PE) <jeancy.dimayuga@brother-biph.com.ph>");
                    builder.Append("<br>");
                    builder.Append("Estacio, Dianelle Yasdane (BIPH-PE) <dianelleyasdane.estacio@brother-biph.com.ph>");
                    builder.Append("<br><br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>ご尽力頂きありがとうございます。</font>");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + Dashboard.SectionText.Replace("BIPH-", "") + " section's " + CategoryDropdown.Text + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

            }

        }

        //================================================================<BreakLine>======================================================>>> 

        private void SendOpenMHApplicationEmailMessage()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationFormTypeDropdown.Text);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationFormTypeDropdown.Text);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationFormTypeDropdown.Text);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                foreach (DataRow row in dTable.Rows)
                {
                    FirstName = row["First Name"].ToString();
                    LastName = row["Last Name"].ToString();
                    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();
                  

                    builder.Append("Dear " + Dashboard.SectionText.Replace("BIPH-", "") + " Section MGR,");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>お疲れ様です。</font>");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
                    {
                        builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH system request form.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>インクカートリッジセクション(Open MH)のシステムリクエストフォームについては、以下のリンクを参照してください。</ font>");
                        builder.Append("<br>");
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                    {
                        builder.Append("This is a request for New Work Center Registration for " + Dashboard.SectionText.Replace("BIPH-", "") + ".");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>以下のリンクにてインクヘッド課のMHシステム編集解除許可(OPEN MH)申請書ご覧下さい。</font>");
                        builder.Append("<br>");

                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
                    {
                        builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH system request form.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>以下のリンクにてP-タッチ課のMHシステム編集解除許可（OPEN MH)申請書ご覧下さい。</font>");
                        builder.Append("<br>");
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                    {
                        builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH system request form.");
                        builder.Append("<br>");
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
                    {
                        builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH system request form.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>以下のリンクにてプリンター課のMHシステム編集解除許可(OPEN MH)申請書ご覧下さい。</font>");
                        builder.Append("<br>");
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
                    {
                        builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH system request form.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>以下のリンクにてテープカセット課のMHシステム編集解除許可(OPEN MH)申請書ご覧下さい。</font>");
                        builder.Append("<br>");
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                    {
                        builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH system request form.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>以下のリンクにて基板組立課のMHシステム編集解除許可(OPEN MH)申請書ご覧下さい。</font>");
                        builder.Append("<br>");
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                    {
                        builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH system request form.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>以下のリンクにて成形課のMHシステム編集解除許可(OPEN MH)申請書ご覧下さい。</font>");
                        builder.Append("<br>");
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "Toner")
                    {
                        builder.Append("Please see link below for " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH system request form.");
                        builder.Append("<br>");
                        //builder.Append("<font color=blue>以下のリンクにて成形課のMHシステム編集解除許可(OPEN MH)申請書ご覧下さい。</font>");
                        //builder.Append("<br>");
                    }


                    builder.Append("<b>For your approval.</b>");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>確認・承認待ち</font>");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                    builder.Append("<br>");
                    builder.Append("<b><i>" + ReferenceNo + "</i></b>");
                    builder.Append("<br><br><br>");

                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>"); //This is the link of approval form module

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + Dashboard.SectionText.Replace("BIPH-", "") + " section's Open MH request.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        //================================================================<BreakLine>======================================================>>> 

        private void UpdateAnnualChangeST()
        {
            con.Close();
            //Update application
            SqlCommand UpdateAnnualChangeST = new SqlCommand("SP_UpdateAnnualChangeST", con);
            UpdateAnnualChangeST.CommandType = CommandType.StoredProcedure;
            con.Open();
            UpdateAnnualChangeST.ExecuteNonQuery();
            con.Close();
        }

        //================================================================<BreakLine>======================================================>>> 

        //private void SAP_STAutoFill()
        //{
        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }

        //    //select SAP ST from SAP master data
        //    SqlCommand SelectSTItemCode = new SqlCommand("SP_SelectSTItemCodeFromMasterData", con);
        //    SelectSTItemCode.CommandType = CommandType.StoredProcedure;
        //    SelectSTItemCode.Parameters.AddWithValue("@Procedure", "SAP");
        //    SelectSTItemCode.Parameters.AddWithValue("@ItemCode", ItemCodeSAP);
        //    SqlDataAdapter da = new SqlDataAdapter(SelectSTItemCode);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    con.Close();

        //    if (dt.Rows.Count > 0)
        //    {
        //        con.Open();

        //        SqlDataReader reader = SelectSTItemCode.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            ItemCode = reader["ItemCodeSAP"].ToString();
        //            ItemName = reader["ItemNameSAP"].ToString();
        //            Plant = reader["Plant"].ToString();
        //            Section = reader["Section"].ToString();
        //            SAPBeforeST = reader["SAPBeforeST"].ToString();
        //            SAPBeforeTT = reader["SAPBeforeTT"].ToString();

        //            reader.Close();
        //        }

        //    }


        //    con.Close();
        //    //Update application
        //    SqlCommand AutoFillSTApplication = new SqlCommand("SP_UpdateSAPSTApplication", con);
        //    AutoFillSTApplication.CommandType = CommandType.StoredProcedure;
        //    AutoFillSTApplication.Parameters.AddWithValue("@STcategory", STCategory);
        //    AutoFillSTApplication.Parameters.AddWithValue("@No", No);
        //    AutoFillSTApplication.Parameters.AddWithValue("@Section", Section);
        //    AutoFillSTApplication.Parameters.AddWithValue("@Plant", Plant);
        //    AutoFillSTApplication.Parameters.AddWithValue("@ItemCode", ItemCode);
        //    AutoFillSTApplication.Parameters.AddWithValue("@ItemName", ItemName);
        //    AutoFillSTApplication.Parameters.AddWithValue("@SAPBeforeST", SAPBeforeST);
        //    AutoFillSTApplication.Parameters.AddWithValue("@SAPBeforeTT", SAPBeforeTT);
        //    AutoFillSTApplication.Parameters.AddWithValue("@SAPAfterST", SAPAfterST);
        //    AutoFillSTApplication.Parameters.AddWithValue("@SAPAfterTT", SAPAfterTT);
        //    AutoFillSTApplication.Parameters.AddWithValue("@EffectivityDate", EffectivityDateTimePicker.Value.ToString("MM/dd/yyyy"));
        //    AutoFillSTApplication.Parameters.AddWithValue("@Reason", Reason);
        //    AutoFillSTApplication.Parameters.AddWithValue("@Remarks", Remarks);
        //    con.Open();
        //    AutoFillSTApplication.ExecuteNonQuery();
        //    con.Close();
        //}

        ////===================================================<break>======================================================//
        //private void MH_STautoFill()
        //{

        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }

        //    //select MH ST from SAP master data
        //    SqlCommand SelectSTItemCode = new SqlCommand("SP_SelectSTItemCodeFromMasterData", con);
        //    SelectSTItemCode.CommandType = CommandType.StoredProcedure;
        //    SelectSTItemCode.Parameters.AddWithValue("@Procedure", "MH");
        //    SelectSTItemCode.Parameters.AddWithValue("@ItemCode", ItemCodeMH);
        //    SqlDataAdapter da = new SqlDataAdapter(SelectSTItemCode);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    con.Close();

        //    if (dt.Rows.Count > 0)
        //    {
        //        con.Open();

        //        SqlDataReader reader = SelectSTItemCode.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            ItemCode = reader["ItemCodeMH"].ToString();
        //            ItemName = reader["ItemNameMH"].ToString();
        //            Plant = reader["Plant"].ToString();
        //            Section = reader["Section"].ToString();
        //            MHBeforeST = reader["MHBeforeST"].ToString();
        //            MHBeforeTT = reader["MHBeforeST"].ToString();

        //            reader.Close();
        //        }

        //    }


        //    con.Close();
        //    //Update application
        //    SqlCommand AutoFillSTApplication = new SqlCommand("SP_UpdateMHSTApplication", con);
        //    AutoFillSTApplication.CommandType = CommandType.StoredProcedure;
        //    AutoFillSTApplication.Parameters.AddWithValue("@STcategory", STCategory);
        //    AutoFillSTApplication.Parameters.AddWithValue("@No", No);
        //    AutoFillSTApplication.Parameters.AddWithValue("@Section", Section);
        //    AutoFillSTApplication.Parameters.AddWithValue("@Plant", Plant);
        //    AutoFillSTApplication.Parameters.AddWithValue("@ItemCode", ItemCode);
        //    AutoFillSTApplication.Parameters.AddWithValue("@ItemName", ItemName);
        //    AutoFillSTApplication.Parameters.AddWithValue("@MHBeforeST", MHBeforeST);
        //    AutoFillSTApplication.Parameters.AddWithValue("@MHBeforeTT", MHBeforeTT);
        //    AutoFillSTApplication.Parameters.AddWithValue("@MHAfterST", MHAfterST);
        //    AutoFillSTApplication.Parameters.AddWithValue("@MHAfterTT", MHAfterTT);
        //    AutoFillSTApplication.Parameters.AddWithValue("@EffectivityDate", EffectivityDateTimePicker.Value.ToString("MM/dd/yyyy"));
        //    AutoFillSTApplication.Parameters.AddWithValue("@Reason", Reason);
        //    AutoFillSTApplication.Parameters.AddWithValue("@Remarks", Remarks);
        //    con.Open();
        //    AutoFillSTApplication.ExecuteNonQuery();
        //    con.Close();
        //}


       


        

         
        private void MPFCategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MPFCategoryDropdown.Text == "")
            {
                TableHeaderPanel.Visible = false;
                ApplicationDataGrid.DataSource = null;
            }
            else
            {
                TableHeaderPanel.Visible = true;
                CategoryHeaderType.Visible = true;
                MPFCategoryDropdown.Text = MPFCategoryDropdown.Text;
                SelectNewApplicationFormPerCategory();
            }
        }

        private void DropdownEntriesValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ApplicationTypeDropdown.Text == "Edit Application")
            {
                SelectForApprovalPerApplicationForm();
            }
            else if (ApplicationTypeDropdown.Text == "Resubmit Application")
            {
                SelectRejectedPerApplicationForm();
            }
            //SelectApplicationFormData();

            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //}

            //if (DropdownEntriesValue.Text == "All")
            //{
            //    SqlCommand SelectTopEntriesSTData = new SqlCommand("SP_SelectTopEntriesPerSTCategory", con);
            //    SelectTopEntriesSTData.CommandType = CommandType.StoredProcedure;
            //    SelectTopEntriesSTData.Parameters.AddWithValue("@Procedure", "AllEntries");
            //    SelectTopEntriesSTData.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
            //    SelectTopEntriesSTData.Parameters.AddWithValue("@Entries", "");
            //    SqlDataAdapter sda = new SqlDataAdapter(SelectTopEntriesSTData);
            //    DataTable dt = new DataTable();
            //    sda.Fill(dt);
            //    ApplicationDataGrid.DataSource = dt;
            //    con.Close();
            //}
            //else
            //{
            //    SqlCommand SelectTopEntriesSTData = new SqlCommand("SP_SelectTopEntriesPerSTCategory", con);
            //    SelectTopEntriesSTData.CommandType = CommandType.StoredProcedure;
            //    SelectTopEntriesSTData.Parameters.AddWithValue("@Procedure", "BySelectedEntries");
            //    SelectTopEntriesSTData.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
            //    SelectTopEntriesSTData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
            //    SqlDataAdapter sda = new SqlDataAdapter(SelectTopEntriesSTData);
            //    DataTable dt = new DataTable();
            //    sda.Fill(dt);
            //    ApplicationDataGrid.DataSource = dt;
            //    con.Close();
            //}


        }

        private void ApplicationTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            CategoryDropdown.Text = "";
            ApplicationDataGrid.DataSource = null;
            AddCategoryPerApplication();

            if (ApplicationTypeDropdown.Text == "New Application")
            {
                ShowEntriesPanel.Visible = false;
            }
            else
            {
                ShowEntriesPanel.Visible = true;
            }
            
        }

        bool SeeAppliedBtnIsClick = false;
        private void SeeAppliedSTBtn_Click(object sender, EventArgs e)
        {
            SelectNewlyAppliedST();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            if (SeeAppliedSTBtn.Text == "       View Applied ST") //Space in codition is required due to button text
            {
                SelectNewApplicationFormPerCategory();
            }
            else if (SeeAppliedSTBtn.Text == "       New Application Form") //Space in codition is required due to button text
            {
                SelectAppliedSTPerCategory();
            }
        }


        string ApplicationFormNumber;
       
        private void CreateFormButton_Click(object sender, EventArgs e)
        {
            SubmitButton.Enabled = true;

            ReferenceNo = ApplicationFormTypeDropdown.Text + "-" + CategoryDropdown.Text + "-" + Dashboard.SectionText.Replace("BIPH-", "") + "_" + DateTime.Now.ToString("yyyyMMddhhmm");

            if (ApplicationFormTypeDropdown.Text == "ST")
            {
                if (ApplicationTypeDropdown.Text == "New Application")
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    //select no.
                    SqlCommand SelectApplicationFormNo = new SqlCommand("SP_SelectApplicationFormNo", con);
                    SelectApplicationFormNo.CommandType = CommandType.StoredProcedure;
                    SelectApplicationFormNo.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                    SelectApplicationFormNo.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                    SelectApplicationFormNo.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SqlDataAdapter da = new SqlDataAdapter(SelectApplicationFormNo);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        con.Open();
                        SqlDataReader reader = SelectApplicationFormNo.ExecuteReader();
                        if (reader.Read())
                        {
                            ApplicationFormNumber = reader[0].ToString(); //ApplicationFormNo Column

                            reader.Close();
                        }

                    }
                    else
                    {
                        ApplicationFormNumber = "0";
                    }

                    int num = 0;

                    for (int i = 0; i < Convert.ToInt32(RowCount.Text); i++)
                    {
                        con.Close();
                        SqlCommand InsertBlankSTApplication = new SqlCommand("SP_InsertNewApplicationForm", con);
                        InsertBlankSTApplication.CommandType = CommandType.StoredProcedure;
                        InsertBlankSTApplication.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                        InsertBlankSTApplication.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                        InsertBlankSTApplication.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                        InsertBlankSTApplication.Parameters.AddWithValue("@ApplicationFormNo", (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString());
                        InsertBlankSTApplication.Parameters.AddWithValue("@No", (num += 1).ToString());
                        InsertBlankSTApplication.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        InsertBlankSTApplication.Parameters.AddWithValue("@EffectivityDate", ""); //Blank parameter -> not use in this query
                        con.Open();
                        InsertBlankSTApplication.ExecuteNonQuery();
                        con.Close();

                    }

                    SelectNewApplicationFormPerCategory();

                    ApplicationDataGrid.Columns[1].Visible = true;

                    CreateFormButton.Enabled = false;
                    RowCount.Enabled = false;
                    CancelApplicationBtn.Visible = true;
                }
            }
            else if (ApplicationFormTypeDropdown.Text == "WC/CC")
            {
                if (ApplicationTypeDropdown.Text == "New Application")
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    //select no.
                    SqlCommand SelectApplicationFormNo = new SqlCommand("SP_SelectApplicationFormNo", con);
                    SelectApplicationFormNo.CommandType = CommandType.StoredProcedure;
                    SelectApplicationFormNo.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                    SelectApplicationFormNo.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                    SelectApplicationFormNo.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SqlDataAdapter da = new SqlDataAdapter(SelectApplicationFormNo);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        con.Open();
                        SqlDataReader reader = SelectApplicationFormNo.ExecuteReader();
                        if (reader.Read())
                        {
                            ApplicationFormNumber = reader[0].ToString(); //ApplicationFormNo Column

                            reader.Close();
                        }

                    }
                    else
                    {
                        ApplicationFormNumber = "0";
                    }

                    int num = 0;

                    for (int i = 0; i < Convert.ToInt32(RowCount.Text); i++)
                    {
                        con.Close();
                        SqlCommand InsertBlankSTApplication = new SqlCommand("SP_InsertNewApplicationForm", con);
                        InsertBlankSTApplication.CommandType = CommandType.StoredProcedure;
                        InsertBlankSTApplication.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                        InsertBlankSTApplication.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                        InsertBlankSTApplication.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                        InsertBlankSTApplication.Parameters.AddWithValue("@ApplicationFormNo", (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString());
                        InsertBlankSTApplication.Parameters.AddWithValue("@No", (num += 1).ToString());
                        InsertBlankSTApplication.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        InsertBlankSTApplication.Parameters.AddWithValue("@EffectivityDate", EffectivityDateTimePicker.Value.ToString());
                        con.Open();
                        InsertBlankSTApplication.ExecuteNonQuery();
                        con.Close();

                    }



                    //DataGridViewComboBoxColumn DropdownColumn2 = new DataGridViewComboBoxColumn();
                    //DropdownColumn2.DataSource = new string[] { "B", "Y" };


                    //DropdownColumn2.Name = "Shift (Old)";
                    //DropdownColumn2.DisplayIndex = 4;
                    //ApplicationDataGrid.Columns.Add(DropdownColumn2);


                    SelectNewApplicationFormPerCategory();

                    if (CategoryDropdown.Text == "Work Center New")
                    {
                        DataGridViewComboBoxColumn cb_Shift = new DataGridViewComboBoxColumn();
                        cb_Shift.Items.AddRange("B", "Y");

                        cb_Shift.Name = "Shift";
                        cb_Shift.HeaderText = "Shift";
                        cb_Shift.DisplayIndex = 4;
                        //cb_Month.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        ApplicationDataGrid.Columns.Add(cb_Shift);
                    }
                    else if (CategoryDropdown.Text == "Cost Center New")
                    {
                        DataGridViewComboBoxColumn cb_Shift = new DataGridViewComboBoxColumn();
                        cb_Shift.Items.AddRange("B", "Y");

                        cb_Shift.Name = "Shift";
                        cb_Shift.HeaderText = "Shift";
                        cb_Shift.DisplayIndex = 7;
                        //cb_Month.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        ApplicationDataGrid.Columns.Add(cb_Shift);
                    }
                    else if (CategoryDropdown.Text == "Work Center Revision")
                    {
                        DataGridViewComboBoxColumn cb_ShiftOld = new DataGridViewComboBoxColumn();
                        cb_ShiftOld.Items.AddRange("B", "Y");

                        cb_ShiftOld.Name = "Shift (Old)";
                        cb_ShiftOld.HeaderText = "Shift (Old)";
                        cb_ShiftOld.DisplayIndex = 4;
                        //cb_Month.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        ApplicationDataGrid.Columns.Add(cb_ShiftOld);


                        DataGridViewComboBoxColumn cb_ShiftNew = new DataGridViewComboBoxColumn();
                        cb_ShiftNew.Items.AddRange("B", "Y");

                        cb_ShiftNew.Name = "Shift (New)";
                        cb_ShiftNew.HeaderText = "Shift (New)";
                        cb_ShiftNew.DisplayIndex = 9;
                        //cb_Month.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        ApplicationDataGrid.Columns.Add(cb_ShiftNew);
                    }
                    else if (CategoryDropdown.Text == "Cost Center Revision")
                    {
                        DataGridViewComboBoxColumn cb_ShiftOld = new DataGridViewComboBoxColumn();
                        cb_ShiftOld.Items.AddRange("B", "Y");

                        cb_ShiftOld.Name = "Shift (Old)";
                        cb_ShiftOld.HeaderText = "Shift (Old)";
                        cb_ShiftOld.DisplayIndex = 4;
                        //cb_Month.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        ApplicationDataGrid.Columns.Add(cb_ShiftOld);


                        DataGridViewComboBoxColumn cb_ShiftNew = new DataGridViewComboBoxColumn();
                        cb_ShiftNew.Items.AddRange("B", "Y");

                        cb_ShiftNew.Name = "Shift (New)";
                        cb_ShiftNew.HeaderText = "Shift (New)";
                        cb_ShiftNew.DisplayIndex = 9;
                        //cb_Month.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        ApplicationDataGrid.Columns.Add(cb_ShiftNew);
                        
                    }
                    else if (CategoryDropdown.Text == "Work Center Deletion")
                    {
                        DataGridViewComboBoxColumn cb_Shift = new DataGridViewComboBoxColumn();
                        cb_Shift.Items.AddRange("B", "Y");

                        cb_Shift.Name = "Shift";
                        cb_Shift.HeaderText = "Shift";
                        cb_Shift.DisplayIndex = 4;
                        //cb_Month.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        ApplicationDataGrid.Columns.Add(cb_Shift);
                    }


                    ApplicationDataGrid.Columns[1].Visible = true;

                    CreateFormButton.Enabled = false;
                    RowCount.Enabled = false;
                    CancelApplicationBtn.Visible = true;
                }
            }
            else if (ApplicationFormTypeDropdown.Text == "Open MH System")
            {
                //Type code here...
                if (ApplicationTypeDropdown.Text == "New Application")
                {
                    if (CategoryDropdown.Text == "Manpower/Man-hour")
                    {
                        if (MonthToOpenDropdown.Text == "")
                        {
                            MessageBox.Show("Please select month.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (CostCenterDropdown.Text == "")
                        {
                            MessageBox.Show("Please select cost center.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (WorkCenterDropdown.Text == "")
                        {
                            MessageBox.Show("Please select work center.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            //select no.
                            SqlCommand SelectApplicationFormNo = new SqlCommand("SP_SelectApplicationFormNo", con);
                            SelectApplicationFormNo.CommandType = CommandType.StoredProcedure;
                            SelectApplicationFormNo.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                            SelectApplicationFormNo.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                            SelectApplicationFormNo.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter da = new SqlDataAdapter(SelectApplicationFormNo);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            con.Close();

                            if (dt.Rows.Count > 0)
                            {
                                con.Open();
                                SqlDataReader reader = SelectApplicationFormNo.ExecuteReader();
                                if (reader.Read())
                                {
                                    ApplicationFormNumber = reader[0].ToString(); //ApplicationFormNo Column

                                    reader.Close();
                                }
                            }
                            else
                            {
                                ApplicationFormNumber = "0";
                            }

                            int num = 0;

                            for (int i = 0; i < Convert.ToInt32(RowCount.Text); i++)
                            {
                                con.Close();
                                SqlCommand InsertBlankSTApplication = new SqlCommand("SP_InsertNewOpenMHApplicationForm", con);
                                InsertBlankSTApplication.CommandType = CommandType.StoredProcedure;
                                InsertBlankSTApplication.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                InsertBlankSTApplication.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                                InsertBlankSTApplication.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                InsertBlankSTApplication.Parameters.AddWithValue("@ApplicationFormNo", (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString());
                                InsertBlankSTApplication.Parameters.AddWithValue("@No", (num += 1).ToString());
                                InsertBlankSTApplication.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                InsertBlankSTApplication.Parameters.AddWithValue("@Date", MonthToOpenDropdown.Text);
                                InsertBlankSTApplication.Parameters.AddWithValue("@CostCenterCode", CostCenterDropdown.Text);
                                InsertBlankSTApplication.Parameters.AddWithValue("@WorkCenterCode", WorkCenterDropdown.Text);
                                con.Open();
                                InsertBlankSTApplication.ExecuteNonQuery();
                                con.Close();

                            }


                            //AddCostCenterDropdownColumn();

                            //AddCWorkCenterDropdownColumn();

                            SelectNewApplicationFormPerCategory();

                            ApplicationDataGrid.Columns[1].Visible = true;


                            ////Date------------------------>>
                            //AddMonthColumn();

                            ////CosteCenter------------------------>>
                            //con.Open();
                            //SqlCommand LoadCostCenter = new SqlCommand("SP_LoadCostCenter", con);
                            //LoadCostCenter.CommandType = CommandType.StoredProcedure;
                            //LoadCostCenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            //SqlDataAdapter sda = new SqlDataAdapter(LoadCostCenter);
                            //DataSet ds = new DataSet();
                            //sda.Fill(ds);
                            //LoadCostCenter.ExecuteNonQuery();
                            //con.Close();


                            //DataGridViewComboBoxColumn cb_CostCenter = new DataGridViewComboBoxColumn();
                            //cb_CostCenter.Name = "CostCenterCode";
                            //cb_CostCenter.HeaderText = "Costcenter Code";
                            //cb_CostCenter.DisplayIndex = 3;

                            //ArrayList row = new ArrayList();

                            //foreach (DataRow dr in ds.Tables[0].Rows)
                            //{
                            //    row.Add(dr["CostCenterCode"].ToString());
                            //}

                            //cb_CostCenter.Items.AddRange(row.ToArray());

                            //ApplicationDataGrid.Columns.Add(cb_CostCenter);

                            ////WorkCenter------------------------>>
                            //con.Open();
                            //SqlCommand LoadWorkCenter = new SqlCommand("SP_LoadWorkCenter", con);
                            //LoadWorkCenter.CommandType = CommandType.StoredProcedure;
                            //LoadWorkCenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            //SqlDataAdapter sda2 = new SqlDataAdapter(LoadWorkCenter);
                            //DataSet ds2 = new DataSet();
                            //sda2.Fill(ds2);
                            //LoadWorkCenter.ExecuteNonQuery();
                            //con.Close();

                            //DataGridViewComboBoxColumn cb_WorkCenter = new DataGridViewComboBoxColumn();
                            //cb_WorkCenter.Name = "WorkCenterCode";
                            //cb_WorkCenter.HeaderText = "Workcenter Code";
                            //cb_WorkCenter.DisplayIndex = 4;

                            ////ArrayList row = new ArrayList();

                            //foreach (DataRow dr in ds2.Tables[0].Rows)
                            //{
                            //    row.Add(dr["WorkCenterCode"].ToString());
                            //}

                            //cb_WorkCenter.Items.AddRange(row.ToArray());

                            //ApplicationDataGrid.Columns.Add(cb_WorkCenter);

                            //Shift---------------------->>
                            DataGridViewComboBoxColumn cb_Shift = new DataGridViewComboBoxColumn();
                            cb_Shift.Items.AddRange("B", "Y");

                            cb_Shift.Name = "Shift";
                            cb_Shift.HeaderText = "Shift";
                            cb_Shift.DisplayIndex = 5;
                            //cb_Month.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                            ApplicationDataGrid.Columns.Add(cb_Shift);


                            CreateFormButton.Enabled = false;
                            RowCount.Enabled = false;
                            CancelApplicationBtn.Visible = true;
                        }

                    }
                    else if (CategoryDropdown.Text == "Standard Time (ST mins)")
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        //select no.
                        SqlCommand SelectApplicationFormNo = new SqlCommand("SP_SelectApplicationFormNo", con);
                        SelectApplicationFormNo.CommandType = CommandType.StoredProcedure;
                        SelectApplicationFormNo.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                        SelectApplicationFormNo.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                        SelectApplicationFormNo.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter da = new SqlDataAdapter(SelectApplicationFormNo);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();

                        if (dt.Rows.Count > 0)
                        {
                            con.Open();
                            SqlDataReader reader = SelectApplicationFormNo.ExecuteReader();
                            if (reader.Read())
                            {
                                ApplicationFormNumber = reader[0].ToString(); //ApplicationFormNo Column

                                reader.Close();
                            }
                        }
                        else
                        {
                            ApplicationFormNumber = "0";
                        }

                        int num = 0;

                        for (int i = 0; i < Convert.ToInt32(RowCount.Text); i++)
                        {
                            con.Close();
                            SqlCommand InsertBlankSTApplication = new SqlCommand("SP_InsertNewOpenMHApplicationForm", con);
                            InsertBlankSTApplication.CommandType = CommandType.StoredProcedure;
                            InsertBlankSTApplication.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                            InsertBlankSTApplication.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                            InsertBlankSTApplication.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                            InsertBlankSTApplication.Parameters.AddWithValue("@ApplicationFormNo", (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString());
                            InsertBlankSTApplication.Parameters.AddWithValue("@No", (num += 1).ToString());
                            InsertBlankSTApplication.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            InsertBlankSTApplication.Parameters.AddWithValue("@Date", MonthToOpenDropdown.Text);
                            InsertBlankSTApplication.Parameters.AddWithValue("@CostCenterCode", CostCenterDropdown.Text);
                            InsertBlankSTApplication.Parameters.AddWithValue("@WorkCenterCode", WorkCenterDropdown.Text);
                            con.Open();
                            InsertBlankSTApplication.ExecuteNonQuery();
                            con.Close();

                        }


                        //AddCostCenterDropdownColumn();

                        //AddCWorkCenterDropdownColumn();

                        SelectNewApplicationFormPerCategory();

                        ApplicationDataGrid.Columns[1].Visible = true;

                        //Date------------------------>>
                        AddMonthColumn();

                        ////CosteCenter------------------------>>
                        //con.Open();
                        //SqlCommand LoadCostCenter = new SqlCommand("SP_LoadCostCenter", con);
                        //LoadCostCenter.CommandType = CommandType.StoredProcedure;
                        //LoadCostCenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        //SqlDataAdapter sda = new SqlDataAdapter(LoadCostCenter);
                        //DataSet ds = new DataSet();
                        //sda.Fill(ds);
                        //LoadCostCenter.ExecuteNonQuery();
                        //con.Close();


                        //DataGridViewComboBoxColumn cb_CostCenter = new DataGridViewComboBoxColumn();
                        //cb_CostCenter.Name = "CostCenterCode";
                        //cb_CostCenter.HeaderText = "Costcenter Code";
                        //cb_CostCenter.DisplayIndex = 3;

                        //ArrayList row = new ArrayList();

                        //foreach (DataRow dr in ds.Tables[0].Rows)
                        //{
                        //    row.Add(dr["CostCenterCode"].ToString());
                        //}

                        //cb_CostCenter.Items.AddRange(row.ToArray());

                        //ApplicationDataGrid.Columns.Add(cb_CostCenter);

                        ////WorkCenter------------------------>>
                        //con.Open();
                        //SqlCommand LoadWorkCenter = new SqlCommand("SP_LoadWorkCenter", con);
                        //LoadWorkCenter.CommandType = CommandType.StoredProcedure;
                        //LoadWorkCenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        //LoadWorkCenter.Parameters.AddWithValue("@CostCenterCode", );
                        //SqlDataAdapter sda2 = new SqlDataAdapter(LoadWorkCenter);
                        //DataSet ds2 = new DataSet();
                        //sda2.Fill(ds2);
                        //LoadWorkCenter.ExecuteNonQuery();
                        //con.Close();

                        //DataGridViewComboBoxColumn cb_WorkCenter = new DataGridViewComboBoxColumn();
                        //cb_WorkCenter.Name = "WorkCenterCode";
                        //cb_WorkCenter.HeaderText = "Workcenter Code";
                        //cb_WorkCenter.DisplayIndex = 4;

                        ////ArrayList row = new ArrayList();

                        //foreach (DataRow dr in ds2.Tables[0].Rows)
                        //{
                        //    row.Add(dr["WorkCenterCode"].ToString());
                        //}

                        //cb_WorkCenter.Items.AddRange(row.ToArray());

                        //ApplicationDataGrid.Columns.Add(cb_WorkCenter);


                        //Shift---------------------->>
                        DataGridViewComboBoxColumn cb_Shift = new DataGridViewComboBoxColumn();
                        cb_Shift.Items.AddRange("B", "Y");

                        cb_Shift.Name = "Shift";
                        cb_Shift.HeaderText = "Shift";
                        cb_Shift.DisplayIndex = 5;
                        //cb_Month.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        ApplicationDataGrid.Columns.Add(cb_Shift);


                        CreateFormButton.Enabled = false;
                        RowCount.Enabled = false;
                        CancelApplicationBtn.Visible = true;
                    }
                    else if (CategoryDropdown.Text == "Linestop/Loss Man-hour/Loss Factor")
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        //select no.
                        SqlCommand SelectApplicationFormNo = new SqlCommand("SP_SelectApplicationFormNo", con);
                        SelectApplicationFormNo.CommandType = CommandType.StoredProcedure;
                        SelectApplicationFormNo.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                        SelectApplicationFormNo.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                        SelectApplicationFormNo.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter da = new SqlDataAdapter(SelectApplicationFormNo);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();

                        if (dt.Rows.Count > 0)
                        {
                            con.Open();
                            SqlDataReader reader = SelectApplicationFormNo.ExecuteReader();
                            if (reader.Read())
                            {
                                ApplicationFormNumber = reader[0].ToString(); //ApplicationFormNo Column

                                reader.Close();
                            }
                        }
                        else
                        {
                            ApplicationFormNumber = "0";
                        }

                        int num = 0;

                        for (int i = 0; i < Convert.ToInt32(RowCount.Text); i++)
                        {
                            con.Close();
                            SqlCommand InsertBlankSTApplication = new SqlCommand("SP_InsertNewOpenMHApplicationForm", con);
                            InsertBlankSTApplication.CommandType = CommandType.StoredProcedure;
                            InsertBlankSTApplication.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                            InsertBlankSTApplication.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                            InsertBlankSTApplication.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                            InsertBlankSTApplication.Parameters.AddWithValue("@ApplicationFormNo", (Convert.ToUInt32(ApplicationFormNumber) + 1).ToString());
                            InsertBlankSTApplication.Parameters.AddWithValue("@No", (num += 1).ToString());
                            InsertBlankSTApplication.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            InsertBlankSTApplication.Parameters.AddWithValue("@Date", MonthToOpenDropdown.Text);
                            InsertBlankSTApplication.Parameters.AddWithValue("@CostCenterCode", CostCenterDropdown.Text);
                            InsertBlankSTApplication.Parameters.AddWithValue("@WorkCenterCode", WorkCenterDropdown.Text);
                            con.Open();
                            InsertBlankSTApplication.ExecuteNonQuery();
                            con.Close();

                        }


                        //AddCostCenterDropdownColumn();

                        //AddCWorkCenterDropdownColumn();

                        SelectNewApplicationFormPerCategory();

                        ApplicationDataGrid.Columns[1].Visible = true;

                        //Date------------------------>>
                        AddMonthColumn();

                        //CosteCenter------------------------>>
                        con.Open();
                        SqlCommand LoadCostCenter = new SqlCommand("SP_LoadCostCenter", con);
                        LoadCostCenter.CommandType = CommandType.StoredProcedure;
                        LoadCostCenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(LoadCostCenter);
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                        LoadCostCenter.ExecuteNonQuery();
                        con.Close();


                        DataGridViewComboBoxColumn cb_CostCenter = new DataGridViewComboBoxColumn();
                        cb_CostCenter.Name = "CostCenterCode";
                        cb_CostCenter.HeaderText = "Costcenter Code";
                        cb_CostCenter.DisplayIndex = 3;

                        ArrayList row = new ArrayList();

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            row.Add(dr["CostCenterCode"].ToString());
                        }

                        cb_CostCenter.Items.AddRange(row.ToArray());

                        ApplicationDataGrid.Columns.Add(cb_CostCenter);

                        //WorkCenter------------------------>>
                        con.Open();
                        SqlCommand LoadWorkCenter = new SqlCommand("SP_LoadWorkCenter", con);
                        LoadWorkCenter.CommandType = CommandType.StoredProcedure;
                        LoadWorkCenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda2 = new SqlDataAdapter(LoadWorkCenter);
                        DataSet ds2 = new DataSet();
                        sda2.Fill(ds2);
                        LoadWorkCenter.ExecuteNonQuery();
                        con.Close();

                        DataGridViewComboBoxColumn cb_WorkCenter = new DataGridViewComboBoxColumn();
                        cb_WorkCenter.Name = "WorkCenterCode";
                        cb_WorkCenter.HeaderText = "Workcenter Code";
                        cb_WorkCenter.DisplayIndex = 4;

                        //ArrayList row = new ArrayList();

                        foreach (DataRow dr in ds2.Tables[0].Rows)
                        {
                            row.Add(dr["WorkCenterCode"].ToString());
                        }

                        cb_WorkCenter.Items.AddRange(row.ToArray());

                        ApplicationDataGrid.Columns.Add(cb_WorkCenter);


                        //Shift---------------------->>
                        DataGridViewComboBoxColumn cb_Shift = new DataGridViewComboBoxColumn();
                        cb_Shift.Items.AddRange("B", "Y");

                        cb_Shift.Name = "Shift";
                        cb_Shift.HeaderText = "Shift";
                        cb_Shift.DisplayIndex = 5;
                        //cb_Month.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        ApplicationDataGrid.Columns.Add(cb_Shift);

                        CreateFormButton.Enabled = false;
                        RowCount.Enabled = false;
                        CancelApplicationBtn.Visible = true;


                        AddLossFactorOldColumn(); //Loss Factor Old Column 

                        AddLossFactorNewColumn(); //Loss Factor New Column 
                    }

                   
                }
            }
          
        }

        private void AddLossFactorOldColumn()
        {
            con.Open();
            SqlCommand LoadLossFactor = new SqlCommand("SP_LoadLossFactor", con);
            LoadLossFactor.CommandType = CommandType.StoredProcedure;
            LoadLossFactor.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(LoadLossFactor);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadLossFactor.ExecuteNonQuery();
            con.Close();


            DataGridViewComboBoxColumn cb_LossFator = new DataGridViewComboBoxColumn();
            cb_LossFator.Name = "LossFactorOld";
            cb_LossFator.HeaderText = "Loss Factor (Old)";
            cb_LossFator.DisplayIndex = 8;

            ArrayList row = new ArrayList();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                row.Add(dr["LossFactor"].ToString());
            }

            cb_LossFator.Items.AddRange(row.ToArray());

            ApplicationDataGrid.Columns.Add(cb_LossFator);

        }

        private void AddLossFactorNewColumn()
        {
            con.Open();
            SqlCommand LoadLossFactor = new SqlCommand("SP_LoadLossFactor", con);
            LoadLossFactor.CommandType = CommandType.StoredProcedure;
            LoadLossFactor.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(LoadLossFactor);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadLossFactor.ExecuteNonQuery();
            con.Close();


            DataGridViewComboBoxColumn cb_LossFator = new DataGridViewComboBoxColumn();
            cb_LossFator.Name = "LossFactorNew";
            cb_LossFator.HeaderText = "Loss Factor (New)";
            cb_LossFator.DisplayIndex = 14;

            ArrayList row = new ArrayList();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                row.Add(dr["LossFactor"].ToString());
            }

            cb_LossFator.Items.AddRange(row.ToArray());

            ApplicationDataGrid.Columns.Add(cb_LossFator);
        }

        private void AddMonthColumn()
        {
            DataGridViewComboBoxColumn cb_Month = new DataGridViewComboBoxColumn();
            cb_Month.Items.AddRange("January " + DateTime.Now.Year, "February " + DateTime.Now.Year, "March " + DateTime.Now.Year, "April " + DateTime.Now.Year, "May " + DateTime.Now.Year, "June " + DateTime.Now.Year, "July " + DateTime.Now.Year, "August " + DateTime.Now.Year, "September " + DateTime.Now.Year, "October " + DateTime.Now.Year, "November " + DateTime.Now.Year, "December " + DateTime.Now.ToString("YYYY"));

            cb_Month.Name = "Date";
            cb_Month.HeaderText = "Date";
            cb_Month.DisplayIndex = 2;
            //cb_Month.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
            ApplicationDataGrid.Columns.Add(cb_Month);

        }

      
        //private void AddCostCenterDropdownColumn()
        //{
        //    //CosteCenter
        //    con.Open();
        //    SqlCommand LoadCostCenter = new SqlCommand("SP_LoadCostCenter", con);
        //    LoadCostCenter.CommandType = CommandType.StoredProcedure;
        //    LoadCostCenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
        //    SqlDataAdapter sda = new SqlDataAdapter(LoadCostCenter);
        //    DataSet ds = new DataSet();
        //    sda.Fill(ds);
        //    LoadCostCenter.ExecuteNonQuery();
        //    con.Close();

        //    //dt = ds.Tables[0];

        //    DataGridViewComboBoxColumn cb_CostCenter = new DataGridViewComboBoxColumn();

        //    if (CategoryDropdown.Text == "Manpower/Man-hour")
        //    {
        //        cb_CostCenter.Name = "CostcenterCode";
        //        cb_CostCenter.HeaderText = "Costcenter Code";
        //        cb_CostCenter.DisplayIndex = 3;
        //    }
        //    else if (CategoryDropdown.Text == "Standard Time (ST mins")
        //    {
        //        cb_CostCenter.Name = "CostcenterCode";
        //        cb_CostCenter.HeaderText = "Costcenter Code";
        //        cb_CostCenter.DisplayIndex = 3;
        //    }
        //    else if (CategoryDropdown.Text == "Linestop/Loss Man-hour/Loss Factor")
        //    {

        //    }
            

        //    ArrayList row = new ArrayList();

        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        row.Add(dr["CostCenterCode"].ToString());
        //    }

        //    cb_CostCenter.Items.AddRange(row.ToArray());

        //    ApplicationDataGrid.Columns.Add(cb_CostCenter);
        //}

        //private void AddCWorkCenterDropdownColumn()
        //{
        //    //WorkCenter
        //    con.Open();
        //    SqlCommand LoadWorkCenter = new SqlCommand("SP_LoadWorkCenter", con);
        //    LoadWorkCenter.CommandType = CommandType.StoredProcedure;
        //    LoadWorkCenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
        //    SqlDataAdapter sda2 = new SqlDataAdapter(LoadWorkCenter);
        //    DataSet ds2 = new DataSet();
        //    sda2.Fill(ds2);
        //    LoadWorkCenter.ExecuteNonQuery();
        //    con.Close();

        //    DataGridViewComboBoxColumn cb_WorkCenter = new DataGridViewComboBoxColumn();

        //    if (CategoryDropdown.Text == "Manpower/Man-hour")
        //    {
        //        cb_WorkCenter.Name = "WorkCenterCode";
        //        cb_WorkCenter.HeaderText = "Workcenter Code";
        //        cb_WorkCenter.DisplayIndex = 4;
        //    }
        //    else if (CategoryDropdown.Text == "Standard Time (ST mins")
        //    {
        //        cb_WorkCenter.Name = "WorkCenterCode";
        //        cb_WorkCenter.HeaderText = "Workcenter Code";
        //        cb_WorkCenter.DisplayIndex = 4;
        //    }
        //    else if (CategoryDropdown.Text == "Linestop/Loss Man-hour/Loss Factor")
        //    {

        //    }

            

        //    ArrayList row = new ArrayList();

        //    foreach (DataRow dr in ds2.Tables[0].Rows)
        //    {
        //        row.Add(dr["WorkCenterCode"].ToString());
        //    }

        //    cb_WorkCenter.Items.AddRange(row.ToArray());

        //    ApplicationDataGrid.Columns.Add(cb_WorkCenter);
        //}

        public static string ReferenceNumber;
        public static string ApplicationFormType;
        public static string Category;
        public static string WithSAP;

        public DataGridViewColumn cb_ShiftNew { get; private set; }

        private void ApplicationDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            if (ApplicationFormTypeDropdown.Text == "ST")
            {
                if (ApplicationTypeDropdown.Text == "Edit Application")
                {

                    if (e.RowIndex != -1)
                    {
                        ReferenceNumber = ApplicationDataGrid.Rows[e.RowIndex].Cells["Reference No."].Value.ToString();
                        ApplicationFormType = ApplicationFormTypeDropdown.Text;
                        Category = CategoryDropdown.Text;

                        //Edit Application
                        if (ApplicationDataGrid.CurrentCell.ColumnIndex.Equals(0) && e.RowIndex != -1)
                        {
                            EditApplication editApplication = new EditApplication();
                            editApplication.ShowDialog();
                        }

                        //Cancell Application
                        if (ApplicationDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "❌")
                        {
                            DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this application?" + Environment.NewLine + "Reference No. " + ReferenceNumber, "MHMS Infornation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (dialogResult == DialogResult.Yes)
                            {

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                //Deletion of Application in approval table
                                SqlCommand DeleteForApprovalApplicationForm = new SqlCommand("SP_DeleteForApprovalApplicationForm", con);
                                DeleteForApprovalApplicationForm.CommandType = CommandType.StoredProcedure;
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNumber);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                DeleteForApprovalApplicationForm.ExecuteNonQuery();
                                con.Close();

                                //Deletion of Application per category
                                con.Open();
                                SqlCommand DeleteApplicationForm = new SqlCommand("SP_DeleteApplicationFormPerCategory", con);
                                DeleteApplicationForm.CommandType = CommandType.StoredProcedure;
                                DeleteApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                                DeleteApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                DeleteApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNumber);
                                DeleteApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                DeleteApplicationForm.ExecuteNonQuery();
                                con.Close();

                                SelectForApprovalPerApplicationForm();

                                MessageBox.Show("ST Application Deleted successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                    }

                   
                }
                else if (ApplicationTypeDropdown.Text == "Resubmit Application")
                {
                    //Type code here...
                    if (e.RowIndex != -1)
                    {
                        ReferenceNumber = ApplicationDataGrid.Rows[e.RowIndex].Cells["Reference No."].Value.ToString();
                        ApplicationFormType = ApplicationFormTypeDropdown.Text;
                        Category = CategoryDropdown.Text;
                        WithSAP = ApplicationDataGrid.Rows[e.RowIndex].Cells["WithSAP"].Value.ToString();

                        //Edit Application
                        if (ApplicationDataGrid.CurrentCell.ColumnIndex.Equals(0) && e.RowIndex != -1)
                        {
                            ResubmitApplicationForm resubmitApplication = new ResubmitApplicationForm();
                            resubmitApplication.ShowDialog();
                        }

                        //Cancell Application
                        else if (ApplicationDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "❌")
                        {
                            DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this application?" + Environment.NewLine + "Reference No. " + ReferenceNumber, "MHMS Infornation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (dialogResult == DialogResult.Yes)
                            {

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                //Deletion of Application in approval table
                                SqlCommand DeleteForApprovalApplicationForm = new SqlCommand("SP_DeleteForApprovalApplicationForm", con);
                                DeleteForApprovalApplicationForm.CommandType = CommandType.StoredProcedure;
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNumber);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                DeleteForApprovalApplicationForm.ExecuteNonQuery();
                                con.Close();

                                //Deletion of Application per category
                                con.Open();
                                SqlCommand DeleteApplicationForm = new SqlCommand("SP_DeleteApplicationFormPerCategory", con);
                                DeleteApplicationForm.CommandType = CommandType.StoredProcedure;
                                DeleteApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                                DeleteApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                DeleteApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNumber);
                                DeleteApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                DeleteApplicationForm.ExecuteNonQuery();
                                con.Close();

                                SelectRejectedPerApplicationForm();

                                MessageBox.Show("ST Application Deleted successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                    }

                }
            }
            else if (ApplicationFormTypeDropdown.Text == "WC/CC")
            {
                if (ApplicationTypeDropdown.Text == "Edit Application")
                {
                    if (e.RowIndex != -1)
                    {
                        ReferenceNumber = ApplicationDataGrid.Rows[e.RowIndex].Cells["Reference No."].Value.ToString();
                        ApplicationFormType = ApplicationFormTypeDropdown.Text;
                        Category = CategoryDropdown.Text;

                        //Edit Application
                        if (ApplicationDataGrid.CurrentCell.ColumnIndex.Equals(0) && e.RowIndex != -1)
                        {
                            EditApplication editApplication = new EditApplication();
                            editApplication.ShowDialog();
                        }

                        //Cancell Application
                        if (ApplicationDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "❌")
                        {
                            DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this application?" + Environment.NewLine + "Reference No. " + ReferenceNumber, "MHMS Infornation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (dialogResult == DialogResult.Yes)
                            {

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                //Deletion of Application in approval table
                                SqlCommand DeleteForApprovalApplicationForm = new SqlCommand("SP_DeleteForApprovalApplicationForm", con);
                                DeleteForApprovalApplicationForm.CommandType = CommandType.StoredProcedure;
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNumber);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                DeleteForApprovalApplicationForm.ExecuteNonQuery();
                                con.Close();

                                //Deletion of Application per category
                                con.Open();
                                SqlCommand DeleteApplicationForm = new SqlCommand("SP_DeleteApplicationFormPerCategory", con);
                                DeleteApplicationForm.CommandType = CommandType.StoredProcedure;
                                DeleteApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                                DeleteApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                DeleteApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNumber);
                                DeleteApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                DeleteApplicationForm.ExecuteNonQuery();
                                con.Close();

                                SelectForApprovalPerApplicationForm();

                                MessageBox.Show("WC/CC Application Deleted successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                    }
                   

                   
                }
                else if (ApplicationTypeDropdown.Text == "Resubmit Application")
                {
                    if (e.RowIndex != -1)
                    {
                        ReferenceNumber = ApplicationDataGrid.Rows[e.RowIndex].Cells["Reference No."].Value.ToString();
                        ApplicationFormType = ApplicationFormTypeDropdown.Text;
                        Category = CategoryDropdown.Text;

                        //Edit Application
                        if (ApplicationDataGrid.CurrentCell.ColumnIndex.Equals(0) && e.RowIndex != -1)
                        {
                            ResubmitApplicationForm resubmitApplication = new ResubmitApplicationForm();
                            resubmitApplication.ShowDialog();
                        }

                        //Cancell Application
                        else if (ApplicationDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "❌")
                        {
                            DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this application?" + Environment.NewLine + "Reference No. " + ReferenceNumber, "MHMS Infornation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (dialogResult == DialogResult.Yes)
                            {

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                //Deletion of Application in approval table
                                SqlCommand DeleteForApprovalApplicationForm = new SqlCommand("SP_DeleteForApprovalApplicationForm", con);
                                DeleteForApprovalApplicationForm.CommandType = CommandType.StoredProcedure;
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNumber);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                DeleteForApprovalApplicationForm.ExecuteNonQuery();
                                con.Close();

                                //Deletion of Application per category
                                con.Open();
                                SqlCommand DeleteApplicationForm = new SqlCommand("SP_DeleteApplicationFormPerCategory", con);
                                DeleteApplicationForm.CommandType = CommandType.StoredProcedure;
                                DeleteApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                                DeleteApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                DeleteApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNumber);
                                DeleteApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                DeleteApplicationForm.ExecuteNonQuery();
                                con.Close();

                                SelectRejectedPerApplicationForm();

                                MessageBox.Show("WC/CC Application Deleted successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                    }
                   
                }
            }
            else if (ApplicationFormTypeDropdown.Text == "Open MH System")
            {
                if (ApplicationTypeDropdown.Text == "Edit Application")
                {
                    if (e.RowIndex != -1)
                    {
                        ReferenceNumber = ApplicationDataGrid.Rows[e.RowIndex].Cells["Reference No."].Value.ToString();
                        ApplicationFormType = ApplicationFormTypeDropdown.Text;
                        Category = CategoryDropdown.Text;

                        //Edit Application
                        if (ApplicationDataGrid.CurrentCell.ColumnIndex.Equals(0) && e.RowIndex != -1)
                        {
                            EditApplication editApplication = new EditApplication();
                            editApplication.ShowDialog();
                        }

                        //Cancell Application
                        if (ApplicationDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "❌")
                        {
                            DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this application?" + Environment.NewLine + "Reference No. " + ReferenceNumber, "MHMS Infornation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (dialogResult == DialogResult.Yes)
                            {

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                //Deletion of Application in approval table
                                SqlCommand DeleteForApprovalApplicationForm = new SqlCommand("SP_DeleteForApprovalApplicationForm", con);
                                DeleteForApprovalApplicationForm.CommandType = CommandType.StoredProcedure;
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNumber);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                DeleteForApprovalApplicationForm.ExecuteNonQuery();
                                con.Close();

                                //Deletion of Application per category
                                con.Open();
                                SqlCommand DeleteApplicationForm = new SqlCommand("SP_DeleteApplicationFormPerCategory", con);
                                DeleteApplicationForm.CommandType = CommandType.StoredProcedure;
                                DeleteApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                                DeleteApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                DeleteApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNumber);
                                DeleteApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                DeleteApplicationForm.ExecuteNonQuery();
                                con.Close();

                                SelectForApprovalPerApplicationForm();

                                MessageBox.Show("WC/CC Application Deleted successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                    }



                }
                else if (ApplicationTypeDropdown.Text == "Resubmit Application")
                {
                    if (e.RowIndex != -1)
                    {
                        ReferenceNumber = ApplicationDataGrid.Rows[e.RowIndex].Cells["Reference No."].Value.ToString();
                        ApplicationFormType = ApplicationFormTypeDropdown.Text;
                        Category = CategoryDropdown.Text;

                        //Edit Application
                        if (ApplicationDataGrid.CurrentCell.ColumnIndex.Equals(0) && e.RowIndex != -1)
                        {
                            ResubmitApplicationForm resubmitApplication = new ResubmitApplicationForm();
                            resubmitApplication.ShowDialog();
                        }

                        //Cancell Application
                        else if (ApplicationDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "❌")
                        {
                            DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this application?" + Environment.NewLine + "Reference No. " + ReferenceNumber, "MHMS Infornation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (dialogResult == DialogResult.Yes)
                            {

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                //Deletion of Application in approval table
                                SqlCommand DeleteForApprovalApplicationForm = new SqlCommand("SP_DeleteForApprovalApplicationForm", con);
                                DeleteForApprovalApplicationForm.CommandType = CommandType.StoredProcedure;
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNumber);
                                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                DeleteForApprovalApplicationForm.ExecuteNonQuery();
                                con.Close();

                                //Deletion of Application per category
                                con.Open();
                                SqlCommand DeleteApplicationForm = new SqlCommand("SP_DeleteApplicationFormPerCategory", con);
                                DeleteApplicationForm.CommandType = CommandType.StoredProcedure;
                                DeleteApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                                DeleteApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                                DeleteApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNumber);
                                DeleteApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                DeleteApplicationForm.ExecuteNonQuery();
                                con.Close();

                                SelectRejectedPerApplicationForm();

                                MessageBox.Show("Open MH Application Deleted successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                    }
                }
            }
        }

        //private void TemplatePerCategory_DropDown(object sender, EventArgs e)
        //{
        //    TemplatePerCategory.ForeColor = Color.Black;
        //}

        //private void TemplatePerCategory_DropDownClosed(object sender, EventArgs e)
        //{
            
        //     TemplatePerCategory.ForeColor = Color.DarkGray;
        //}

        //private void TemplatePerCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (TemplatePerCategory.Text == "Select ST Template")
        //    {
        //        TemplatePerCategory.ForeColor = Color.DarkGray;
        //    }
        //    else
        //    {
        //        TemplatePerCategory.ForeColor = Color.Black;
        //    }
        //}

        private void ApplicationDataGrid_MouseEnter(object sender, EventArgs e)
        {
            if (ApplicationTypeDropdown.Text == "Edit Application")
            {
                 Cursor = Cursors.Hand;
            }
           
        }

        private void ApplicationDataGrid_MouseLeave(object sender, EventArgs e)
        {
            if (ApplicationTypeDropdown.Text == "Edit Application")
            {
                Cursor = Cursors.Default;
            }
        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DownloadTemplateBtn_Click(object sender, EventArgs e)
        {
            if (ApplicationFormTypeDropdown.Text == "ST")
            {

                if (CategoryDropdown.Text == "")
                {
                    MessageBox.Show("Please select category", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    CategoryDropdown.Select();
                }
                else
                {
                    if (CategoryDropdown.Text == "Annual ST Change")
                    {
                        Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\ST Template\Annual ST Change Template.xlsm");
                    }
                    else if (CategoryDropdown.Text == "MH Change ST Model List Form - No BIL Approval")
                    {
                        Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\ST Template\MH Change ST Model List Form - No BIL Approval Template.xlsm");
                    }
                    else if (CategoryDropdown.Text == "MH Change ST Model List Form")
                    {
                        Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\ST Template\MH Change ST Model List Form Template.xlsm");
                    }
                    else if (CategoryDropdown.Text == "MH New ST Model List Form")
                    {
                        Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\ST Template\MH New ST Model List Form Template.xlsm");
                    }
                }
            }
            else if (ApplicationFormTypeDropdown.Text == "WC/CC")
            {
                if (CategoryDropdown.Text == "")
                {
                    MessageBox.Show("Please select category", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    CategoryDropdown.Select();
                }
                else
                {
                    if (CategoryDropdown.Text == "Work Center New")
                    {
                        Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\WC_CC Template\Work Center New Template.xlsm");
                    }
                    else if (CategoryDropdown.Text == "Work Center Revision")
                    {
                        Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\WC_CC Template\Work Center Revision Template.xlsm");
                    }
                    else if (CategoryDropdown.Text == "Work Center Deletion")
                    {
                        Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\WC_CC Template\Work Center Deletion Template.xlsm");
                    }
                    else if (CategoryDropdown.Text == "Cost Center New")
                    {
                        Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\WC_CC Template\Cost Center New Template.xlsm");
                    }
                    else if (CategoryDropdown.Text == "Cost Center Revision")
                    {
                        Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\WC_CC Template\Cost Center Revision Template.xlsm");
                    }
                    else if (CategoryDropdown.Text == "Cost Center Deletion")
                    {
                        Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\WC_CC Template\Cost Center Deletion Template.xlsm");
                    }
                }
            }
            else if (ApplicationFormTypeDropdown.Text == "Open MH System")
            {
                if (CategoryDropdown.Text == "")
                {
                    MessageBox.Show("Please select category", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    CategoryDropdown.Select();
                }
                else
                {
                    if (CategoryDropdown.Text == "Manpower/Man-hour")
                    {
                        Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\Open MH Template\Manpower-Man-Hour Category.xlsm");
                    }
                    else if (CategoryDropdown.Text == "Standard Time (ST mins)")
                    {
                        Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\Open MH Template\Standard Time (ST mins.) Category.xlsm");
                    }
                    else if (CategoryDropdown.Text == "Linestop/Loss Man-hour/Loss Factor")
                    {
                        Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\Open MH Template\Linestop-Loss Man-Hour-Loss Factor Category.xlsx");
                    }
                }
            }
        }

        private void DropdownEntriesValue_TextChanged(object sender, EventArgs e)
        {
            if (ApplicationTypeDropdown.Text == "Edit Application")
            {
                SelectForApprovalPerApplicationForm();
            }
            else if (ApplicationTypeDropdown.Text == "Resubmit Application")
            {
                SelectRejectedPerApplicationForm();
            }
        }
          
        private void WorkCenterDropdown_DropDown(object sender, EventArgs e)
        {
            LoadWorkCenter();
        }

        private void CostCenterDropdown_DropDown(object sender, EventArgs e)
        {
            LoadCostCenter();
        }

        private void WorkCenterDropdown_DropDownClosed(object sender, EventArgs e)
        {
            //WorkCenterDropdown.DataSource = null;
        }

        private void CostCenterDropdown_DropDownClosed(object sender, EventArgs e)
        {
            //CostCenterDropdown.DataSource = null;
        }

        private void CancelApplicationBtn_Click(object sender, EventArgs e)
        {
            CancelApplicationBtn.Visible = false;
            RowCount.Enabled = true;
            CreateFormButton.Enabled = true;

            //Delete application here...
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this application?", "MHMS Infornation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //Deletion of Application per category
                SqlCommand DeleteApplicationForm = new SqlCommand("SP_DeleteApplicationFormPerCategory", con);
                DeleteApplicationForm.CommandType = CommandType.StoredProcedure;
                DeleteApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
                DeleteApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                DeleteApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNo);
                DeleteApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                DeleteApplicationForm.ExecuteNonQuery();
                con.Close();

                //ApplicationDataGrid.Columns.Remove("Date");
                ////ApplicationDataGrid.Columns.Remove("Costcenter Code");
                ////ApplicationDataGrid.Columns.Remove("Workcenter Code");
                //ApplicationDataGrid.Columns.Remove("Shift");

                ApplicationDataGrid.DataSource = null;

                //SelectForApprovalPerApplicationForm();

                MessageBox.Show("Application was cancelled successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Dashboard.ApplicationIsSubmitted = true; //Refresh application form
            }
        }

        private void DiscardApplication()
        {
            CancelApplicationBtn.Visible = false;
            RowCount.Enabled = true;
            CreateFormButton.Enabled = true;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            //Deletion of Application per category
            SqlCommand DeleteApplicationForm = new SqlCommand("SP_DeleteApplicationFormPerCategory", con);
            DeleteApplicationForm.CommandType = CommandType.StoredProcedure;
            DeleteApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationFormTypeDropdown.Text);
            DeleteApplicationForm.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
            DeleteApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNo);
            DeleteApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            DeleteApplicationForm.ExecuteNonQuery();
            con.Close();

            //ApplicationDataGrid.Columns.Remove("Date");
            ////ApplicationDataGrid.Columns.Remove("Costcenter Code");
            ////ApplicationDataGrid.Columns.Remove("Workcenter Code");
            //ApplicationDataGrid.Columns.Remove("Shift");

            ApplicationDataGrid.DataSource = null;
        }

        private void MonthToOpenDropdown_DropDown(object sender, EventArgs e)
        {
            AddMonthYear();
        }

        private void MonthToOpenDropdown_DropDownClosed(object sender, EventArgs e)
        {
            //RemoveMonthYear();
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            SelectNewApplicationFormPerCategory();
        }


        private void ApplicationForm_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CostCenterDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Show newly applied MH application if user click yes button in dialog box
            if (MessageBox.Show("Do you want to view your newly applied " + ApplicationFormTypeDropdown.Text + " application?", "MHMS Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ApplicationFormType = ApplicationFormTypeDropdown.Text;
                Category = CategoryDropdown.Text;

                ViewNewlyAppliedForm viewNewlyAppliedForm = new ViewNewlyAppliedForm();
                viewNewlyAppliedForm.ShowDialog();
            }
        }

        private void EffectivityDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ShowPickedDate(EffectivityDateTimePicker);
        }

        // Helper to show date after user picks one
        private void ShowPickedDate(DateTimePicker picker)
        {
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = "MM/dd/yyyy"; // or any format you prefer

        }
        //===================================================<break>======================================================//



        //-------------------end
    }
}
