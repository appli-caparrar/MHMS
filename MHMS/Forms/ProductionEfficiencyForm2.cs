using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS.Forms
{
    public partial class ProductionEfficiencyForm2 : Form
    {
        public ProductionEfficiencyForm2()
        {
            InitializeComponent();
        }

        private void ProductionEfficiencyForm2_Load(object sender, EventArgs e)
        {
            TTEfficiencyDateFrom();
            TTEfficiencyDateTo();

            DirectEfficiencyDateFrom();
            DirectEfficiencyDateTo();

            SemiDirectRate_DateFrom();
            SemiDirectRate_DateTo();

            TotalLossRate_DateFrom();
            TotalLossRate_DateTo();
        }

        // ---> Set the datetime picker value to first day of the current month
        private void TTEfficiencyDateFrom()
        {
            DateTime now = DateTime.Now;
            TotalEffDateFrom.Value = new DateTime(now.Year, now.Month, 1);
        }// <---- end

        private void TTEfficiencyDateTo()
        {
            DateTime datenow = DateTime.Now;
            TotalEffDateTo.Value = datenow;
        }// <---- end

        private void DirectEfficiencyDateFrom()
        {
            DateTime now = DateTime.Now;
            DirectEffDateFrom.Value = new DateTime(now.Year, now.Month, 1);
        }// <---- end

        private void DirectEfficiencyDateTo()
        {
            DateTime datenow = DateTime.Now;
            DirectEffDateTo.Value = datenow;
        }// <---- end

        private void SemiDirectRate_DateFrom()
        {
            DateTime now = DateTime.Now;
            SemiDirectRateDateFrom.Value = new DateTime(now.Year, now.Month, 1);
        }// <---- end

        private void SemiDirectRate_DateTo()
        {
            DateTime datenow = DateTime.Now;
            SemiDirectRateDateTo.Value = datenow;
        }// <---- end

        private void TotalLossRate_DateFrom()
        {
            DateTime now = DateTime.Now;
            TotalLossRateDateFrom.Value = new DateTime(now.Year, now.Month, 1);
        }// <---- end

        private void TotalLossRate_DateTo()
        {
            DateTime datenow = DateTime.Now;
            TotalLossRateDateTo.Value = datenow;
        }// <---- end

        private void ProcessGroupingBtn_Click(object sender, EventArgs e)
        {
            ProcessGroupingsForm ProcessGroupingsForm = new ProcessGroupingsForm();
            ProcessGroupingsForm.ShowDialog();
        }
    }
}
