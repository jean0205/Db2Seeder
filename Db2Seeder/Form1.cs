using As400DataAccess;
using Db2Seeder.API.Request;
using Db2Seeder.Business;
using Db2Seeder.Business.Benefit_Claims;
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
        readonly EmployeeDB2 as400Empe = new EmployeeDB2();


        readonly Employee Employee = new Employee();
        readonly Employer Employer = new Employer();
        readonly Remittance Remittance = new Remittance();
        readonly ComplianceCertificate ComplianceCertificate = new ComplianceCertificate();
        readonly AgeBenefit AgeBenefit = new AgeBenefit();


        List<SupportRequestType> RequestTypeList;



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
            // await Employee.GetEmployeeRegistrationSupportRequestCompleted();           

            tViewEvents.Nodes.Add(new TreeNode("Employee Registration Requests", 1, 1));
            tViewEvents.ExpandAll();

            var lastNode = tViewEvents.Nodes[tViewEvents.Nodes.Count - 1];
            lastNode.Nodes.Add(new TreeNode("Getting Employee Requests Completed", 2, 2));
            var requests = await EmployeeRegistration.GetEmployeeRegistrationSupportRequestCompleted();
            tViewEvents.ExpandAll();

            if (requests.Any())
            {
                lastNode.Nodes.Add(new TreeNode(requests.Count + " request found", 2, 2));
                tViewEvents.ExpandAll();
                foreach (var request in requests)
                {
                    lastNode.Nodes.Add(new TreeNode("Getting Employee Details", 2, 2));
                    var Document_Employee = await EmployeeRegistration.EmployeeRegistrationRequestDetail(request);
                    if (Document_Employee != null)
                    {
                        lastNode.Nodes.Add(new TreeNode("Employee details successfully loaded", 2, 2));
                        Document_Employee.nisNo = await as400Empe.InsertEmployees(Document_Employee);
                        if (Document_Employee.nisNo == 0)
                        {
                            lastNode.Nodes.Add(new TreeNode("Error inserting employee to the  details successfully loaded", 2, 2));
                        }
                        else
                        {
                            lastNode.Nodes.Add(new TreeNode("Employee with NIS number:" + Document_Employee.nisNo + " successfully saved to the DB2 database.", 2, 2));
                        }
                    }
                    else
                    {
                        lastNode.Nodes.Add(new TreeNode("Error getting employee details", 2, 2));
                    }
                }

            }
            else
            {
                lastNode.Nodes.Add(new TreeNode("No requests found", 2, 2));
                tViewEvents.ExpandAll();
            }




        }
        private async void button2_Click(object sender, EventArgs e)
        {
            await Employer.GetEmployerCompleted();
        }
        private async void button4_Click(object sender, EventArgs e)
        {
            await ComplianceCertificate.GetComplianceCrtCompleted();
        }
        private async void button5_Click(object sender, EventArgs e)
        {
            await AgeBenefit.GetAgeBenefitCompleted();
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
        private async void button3_Click(object sender, EventArgs e)
        {
            await Remittance.GetRemittancePendingReview();
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
