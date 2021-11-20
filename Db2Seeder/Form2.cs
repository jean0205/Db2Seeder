using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Db2Seeder
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            webBrowser1 = new WebBrowser();
            webBrowser1.Navigate("http://my-nis-uat.loteklabs.com/SupportRequest/Detail/638");
        }
    }
}
