using As400DataAccess;
using Db2Seeder.API.Request;
using Microsoft.AppCenter.Crashes;
using ShareModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Db2Seeder
{
    public partial class Form1 : Form
    {
        EmployeeDB2 empe = new EmployeeDB2();
        List<SupportRequestType> requestTypeList;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        async void GetRequestType()
        {
            try
            {
                requestTypeList = new List<SupportRequestType>();
                requestTypeList = await ApiRequest.GetSupportRequestTypes();

                if (requestTypeList.Any())
                {

                }

            }
            catch (Exception ex)
            {

                Crashes.TrackError(ex);
            }

        }
        async void GetEmployeesAproved()
        {
            try
            {

            }
            catch (Exception)
            {

                Crashes.TrackError(ex);
            }

        }
    }
}
