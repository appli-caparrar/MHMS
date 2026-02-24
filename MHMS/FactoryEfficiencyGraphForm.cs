using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class FactoryEfficiencyGraphForm : Form
    {
        public FactoryEfficiencyGraphForm()
        {
            InitializeComponent();
        }

        private void FactoryEfficiencyGraphForm_Load(object sender, EventArgs e)
        {

        }

        private void FEMonthlyResultGraphBtn_Click(object sender, EventArgs e)
        {
            Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/FE_MonthlyResult/MonthlyResult?:iid=1");
            Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/FE_QuarterlyResult/QuarterlyResult?:iid=2");
           
        }

        private void FEIdealVarianceRateGraphBtn_Click(object sender, EventArgs e)
        {
           
            Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_IdealVarianceRate/IdealVarianceRateMonthly?:iid=1");
        }

        private void CumulativeManhourGraphBtn_Click(object sender, EventArgs e)
        {
            Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_CumulativeManhour/CumulativeActualFCSTManhour?:iid=3");
        }

        private void FEMonthlyResultGraphBtn_MouseEnter(object sender, EventArgs e)
        {
            GraphName.Text = "Factory Efficiency Monhtly and Quarterly Result";
        }

        private void FEMonthlyResultGraphBtn_MouseLeave(object sender, EventArgs e)
        {
            GraphName.Text = "";
        }

        private void FEIdealVarianceRateGraphBtn_MouseEnter(object sender, EventArgs e)
        {
            GraphName.Text = "Factory Efficiency Ideal Variance Result";
        }

        private void FEIdealVarianceRateGraphBtn_MouseLeave(object sender, EventArgs e)
        {
            GraphName.Text = "";
        }

        private void CumulativeManhourGraphBtn_MouseEnter(object sender, EventArgs e)
        {
            GraphName.Text = "Factory Efficiency Cumulative Manhour";
        }

        private void CumulativeManhourGraphBtn_MouseLeave(object sender, EventArgs e)
        {
            GraphName.Text = "";
        }

    }
}
