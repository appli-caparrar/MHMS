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
    public partial class AddCOPQForm : Form
    {
        public AddCOPQForm()
        {
            InitializeComponent();
        }

        private void AddCOPQTimer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateAndTimeLabel.Text = dateTime.ToString("dddd , MMM dd yyyy, hh : mm : ss");
        }

        private void AddCOPQForm_Load(object sender, EventArgs e)
        {

        }
    }
}
