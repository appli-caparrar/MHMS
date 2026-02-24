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
    public partial class TableauGraphForm : Form
    {
        public TableauGraphForm()
        {
            InitializeComponent();
        }

        private void TableauGraphForm_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.google.com/");
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //this.Text = e.Url.ToString() + " is loading...";
        }
    }
}
