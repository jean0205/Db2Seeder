using As400DataAccess;
using Db2Seeder.API.Request;
using ShareModels.Models;
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
    public partial class Form1 : Form
    {
        EmployeeDB2 empe = new EmployeeDB2();
        SupportRequestType requestType=
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            test();
        }

        async void test()
        {
            await ApiRequest.GetSupportRequestTypes();

          
        }
    }
}
