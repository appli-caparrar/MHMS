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
    public partial class ProdEfficiencyGraphForm : Form
    {
        public ProdEfficiencyGraphForm()
        {
            InitializeComponent();
        }

        private void TotalEffGraphBtn_Click(object sender, EventArgs e)
        {
            Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_TotalEfficiencyGraph/TotalEfficiencyGraph?:iid=2");
        }

        private void DirectEffGraphBtn_Click(object sender, EventArgs e)
        {
            DirectEfficiencyGraphForm DirectEfficiencyGraphForm = new DirectEfficiencyGraphForm();
            DirectEfficiencyGraphForm.ShowDialog();
        }

        private void SemiDirectGraphBtn_Click(object sender, EventArgs e)
        {
            Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_Semi-directEfficiencyGraph/Semi-directEfficiencyGraph?:iid=8");
        }

        private void TotalLossRateGraphBtn_Click(object sender, EventArgs e)
        {
            Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_TotalLossRate/TotalLossRate");
        }
        
    }
}
