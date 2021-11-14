using As400DataAccess;
using Db2Seeder.API.Request;
using Db2Seeder.Business;
using Db2Seeder.NIS.SQL.Documents.DataAccess;
using Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments;
using Microsoft.AppCenter.Crashes;
using ShareModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Db2Seeder
{
    public partial class Form1 : Form
    {
        readonly Employee Employee= new Employee();
        readonly Employer Employer = new Employer();


        List<SupportRequestType> RequestTypeList;
        List<SupportRequest> RequestList;
       

        bool finish = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private async void button1_Click(object sender, EventArgs e)
        {
            await Employee.GetEmployeesCompleted();
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            await Employer.GetEmployerCompleted();
        }

        async void GetRequestType()
        {
            try
            {
                RequestTypeList = new List<SupportRequestType>();
                RequestTypeList = await ApiRequest.GetSupportRequestTypes();

                if (RequestTypeList.Any())
                {

                }

            }
            catch (Exception ex)
            {

                Crashes.TrackError(ex);
            }

        }



        private async void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //await GetEmployeesCompleted();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (finish)
            {
               // await GetEmployeesCompleted();
            }
        }

      
    }
}
