using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class LossRateForm : Form
    {
        public LossRateForm()
        {
            InitializeComponent();
        }

        private void LossRateFormTimer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateAndTimeLabel.Text = dateTime.ToString("dddd , MMM dd yyyy, hh : mm : ss");
        }

        private void LossRateForm_Load(object sender, EventArgs e)
        {

        }
    }
}
