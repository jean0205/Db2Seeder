using As400DataAccess;
using Db2Seeder.API.Request;
using Db2Seeder.Business;
using Db2Seeder.Business.Benefit_Claims;
using Microsoft.AppCenter.Crashes;
using ShareModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Db2Seeder
{
    public partial class Form1 : Form
    {
        readonly EmployeeDB2 as400Empe = new EmployeeDB2();
        readonly EmployerDB2 as400Empr= new EmployerDB2();
        readonly AgePensionDB2 as400AgeBenefit= new AgePensionDB2();

        readonly Employee Employee = new Employee();
        readonly Employer Employer = new Employer();
        readonly Remittance Remittance = new Remittance();      
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

            await EmployeeRegistrationRequest();
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            //await Employer.GetEmployerCompleted();
            await EmployerRegistrationRequest();
        }
        private async void button4_Click(object sender, EventArgs e)
        {
            // await ComplianceCertificate.GetComplianceCrtCompleted();
            await ComplianceCertificateRequest();
        }
        private async void button5_Click(object sender, EventArgs e)
        {
            //await AgeBenefit.GetAgeBenefitCompleted();
            await AgeBenefitRequest();
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
        private async Task EmployeeRegistrationRequest()
        {
            try
            {
                AddTreeViewLogLevel0("Employee Registration Requests");
                AddTreeViewLogLevel1Info("Getting Employee Requests Completed");
                try
                {
                    var requests = await EmployeeRegistration.GetEmployeeRegistrationSupportRequestCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Requests Completed Found", true);
                        foreach (var request in requests)
                        {
                            AddTreeViewLogLevel1Info("Getting Employee Details");
                            var Document_Employee = await EmployeeRegistration.EmployeeRegistrationRequestDetail(request);
                            if (Document_Employee != null)
                            {
                                AddTreeViewLogLevel1("Employee details successfully loaded", true);
                                Document_Employee.nisNo = await as400Empe.InsertEmployees(Document_Employee);
                                if (Document_Employee.nisNo == 0)
                                {
                                    AddTreeViewLogLevel1("Error inserting employee to the  DB2 database.", false);
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Employee with NIS number: " + Document_Employee.nisNo + " successfully saved to the DB2 database", true);
                                    //updating worflow state
                                    var responseA = await EmployeeRegistration.UpdateWorkFlowStateEmployee(7083, request.supportRequestId, 161);
                                    if (responseA.IsSuccess)
                                    {
                                        AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                    }
                                    try
                                    {
                                        AddTreeViewLogLevel2Info("Saving Employee Documents.");
                                        int savedAtt = await EmployeeRegistration.RequestAttachmentToScannedDocuments(request, Document_Employee);
                                        AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                    }
                                    catch (Exception ex)
                                    {
                                        Crashes.TrackError(ex);
                                        AddTreeViewLogLevel2("Error " + ex.Message, false);
                                    }
                                    try
                                    {
                                        AddTreeViewLogLevel2Info("Mapping NIS Number to Web Portal Account.");
                                        var response = await EmployeeRegistration.AddNISMapping(request, Document_Employee);
                                        if (response.IsSuccess)
                                        {
                                            AddTreeViewLogLevel2(Document_Employee.nisNo + " Successfully Mapped.", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel2("Error mapping NIS number: " + Document_Employee.nisNo + " " + response.Message, false);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Crashes.TrackError(ex);
                                        AddTreeViewLogLevel2("Error " + ex.Message, false);                                       
                                    }
                                    try
                                    {
                                        AddTreeViewLogLevel2Info("Assigning Employee Rol.");                                       
                                        if (await EmployeeRegistration.AddEmployeeRole(request))
                                        {
                                            AddTreeViewLogLevel2("Employee Role successufully added", true);                                            
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel2("Error Adding Employee Role.", false);                                            
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Crashes.TrackError(ex);
                                        AddTreeViewLogLevel2("Error " + ex.Message, false);                                       
                                    }                                  
                                }
                            }
                            else
                            {
                                AddTreeViewLogLevel1("Error Getting Employee Details.", false);                                
                            }
                        }
                    }
                    else
                    {
                        AddTreeViewLogLevel1Info("No Completed Requests were Found.");                       
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    AddTreeViewLogLevel1("Error " + ex.Message, false);                    
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async Task EmployerRegistrationRequest()
        {
            try
            {
                AddTreeViewLogLevel0("Employer Registration Requests");
                AddTreeViewLogLevel1Info("Getting Employer Requests Completed");
                try
                {
                    var requests = await EmployerRegistration.GetEmployerRegistrationSupportRequestCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Requests Completed Found", true);
                        foreach (var request in requests)
                        {
                            AddTreeViewLogLevel1Info("Getting Employer Details");
                            var Document_Employer = await EmployerRegistration.EmployerRegistrationRequestDetail(request);
                            if (Document_Employer != null)
                            {
                                AddTreeViewLogLevel1("Employer details successfully loaded", true);
                                Document_Employer.employerNo = await as400Empr.InsertEmployers(Document_Employer);
                                if (Document_Employer.employerNo == 0)
                                {
                                    AddTreeViewLogLevel1("Error inserting employer to the  DB2 database.", false);
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Employer with number: " + Document_Employer.employerNo + " successfully saved to the DB2 database.", true);
                                    //updating worflow state
                                    var responseA = await EmployerRegistration.UpdateWorkFlowStateEmployee(7083, request.supportRequestId, 162);
                                    if (responseA.IsSuccess)
                                    {
                                        AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                    }
                                    try
                                    {
                                        AddTreeViewLogLevel2Info("Saving Employer Documents.");
                                        int savedAtt = await EmployerRegistration.RequestAttachmentToScannedDocuments(request, Document_Employer);
                                        AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                    }
                                    catch (Exception ex)
                                    {
                                        Crashes.TrackError(ex);
                                        AddTreeViewLogLevel2("Error " + ex.Message, false);
                                    }
                                    try
                                    {
                                        AddTreeViewLogLevel2Info("Mapping Employer Number to Web Portal Account.");
                                        var response = await EmployerRegistration.AddNISMapping(request, Document_Employer);
                                        if (response.IsSuccess)
                                        {
                                            AddTreeViewLogLevel2(Document_Employer.employerNo + " Successfully Mapped.", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel2("Error mapping NIS number: " + Document_Employer.employerNo + " " + response.Message, false);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Crashes.TrackError(ex);
                                        AddTreeViewLogLevel2("Error " + ex.Message, false);
                                    }
                                    try
                                    {
                                        AddTreeViewLogLevel2Info("Assigning Employer Rol.");
                                        if (await EmployerRegistration.AddEmployerRole(request))
                                        {
                                            AddTreeViewLogLevel2("Employer Role successufully added", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel2("Error Adding Employer Role.", false);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Crashes.TrackError(ex);
                                        AddTreeViewLogLevel2("Error " + ex.Message, false);
                                    }
                                }
                            }
                            else
                            {
                                AddTreeViewLogLevel1("Error Getting Employer Details.", false);
                            }
                        }
                    }
                    else
                    {
                        AddTreeViewLogLevel1Info("No Completed Requests were Found.");
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    AddTreeViewLogLevel1("Error " + ex.Message, false);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async Task AgeBenefitRequest()
        {
            try
            {
                AddTreeViewLogLevel0("Age Benefit");
                AddTreeViewLogLevel1Info(" Getting Age Benefit Claims Completed");
                try
                {
                    var requests = await AgeBenefit.GetAgeBenefitClaimCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Completed Found", true);
                        foreach (var request in requests)
                        {
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            var Document_AgeBenefit = await AgeBenefit.AgeBenefitClaimDetail(request);
                            if (Document_AgeBenefit != null)
                            {
                                AddTreeViewLogLevel1("Claim details successfully loaded", true);
                                Document_AgeBenefit.ClaimNumber = await as400AgeBenefit.InsertAgePension(Document_AgeBenefit);
                                if (Document_AgeBenefit.ClaimNumber == 0)
                                {
                                    AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Claim with number: " + Document_AgeBenefit.ClaimNumber + " successfully saved to the DB2 database.", true);
                                    //updating worflow state
                                   // var responseA = await EmployerRegistration.UpdateWorkFlowStateEmployee(7083, request.supportRequestId, 163);
                                    //if (responseA.IsSuccess)
                                    //{
                                    //    AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                    //}
                                    //else
                                    //{
                                    //    AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                    //}                                   
                                }
                                //comentar si no se quieren salvar los documentos nuevamente
                                try
                                {
                                    AddTreeViewLogLevel2Info("Saving Employee Documents.");
                                    int savedAtt = await AgeBenefit.RequestAttachmentToScannedDocuments(request, Document_AgeBenefit);
                                    AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                }
                                catch (Exception ex)
                                {
                                    Crashes.TrackError(ex);
                                    AddTreeViewLogLevel2("Error " + ex.Message, false);
                                }
                            }
                            else
                            {
                                AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                            }
                        }
                    }
                    else
                    {
                        AddTreeViewLogLevel1Info("No Completed Claims were Found.");
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    AddTreeViewLogLevel1("Error " + ex.Message, false);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async Task ComplianceCertificateRequest()
        {
            try
            {
                AddTreeViewLogLevel0("Compliance Certificate");
                AddTreeViewLogLevel1Info("Getting Compliance Certificate Request Completed");
                try
                {
                    var requests = await ComplianceCertificate.GetComplianceCertfCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Request Completed Found", true);
                        foreach (var request in requests)
                        {
                            AddTreeViewLogLevel1Info("Getting Request Details");
                            var Document_ComplianceCert = await ComplianceCertificate.ComplianceCertRequestDetail(request);
                            if (Document_ComplianceCert != null)
                            {
                                AddTreeViewLogLevel1("Request details successfully loaded", true);
                                AddTreeViewLogLevel1Info("Inserting Compliance Certificate in SQL.");

                                if (await ComplianceCertificate.InsertComplianceCertificateSQL(request,Document_ComplianceCert))
                                {
                                    AddTreeViewLogLevel2("Compliance Certificate successfully saved to SQL.", true);
                                }
                                else
                                {
                                    AddTreeViewLogLevel2("Error Saving Compliance Certificate to SQL.", false);

                                    //updating worflow state
                                    //var responseA = await ComplianceCertificate.UpdateWorkFlowStateEmployee(7083, request.supportRequestId, 160);
                                    //if (responseA.IsSuccess)
                                    //{
                                    //    AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                    //}
                                    //else
                                    //{
                                    //    AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                    //}
                                }
                                
                                
                            }
                            else
                            {
                                AddTreeViewLogLevel1("Error Getting Compliance Details.", false);
                            }
                        }
                    }
                    else
                    {
                        AddTreeViewLogLevel1Info("No Completed Claims were Found.");
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    AddTreeViewLogLevel1("Error " + ex.Message, false);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }



        void AddTreeViewLogLevel0(string text)
        {
            Thread.Sleep(2000);
            Application.DoEvents();
            tViewEvents.Nodes.Add(new TreeNode(text + " [" + DateTime.Now + "]", 0, 0));
            tViewEvents.ExpandAll();
            tViewEvents.SelectedNode = tViewEvents.Nodes[tViewEvents.Nodes.Count-1];
        }
        void AddTreeViewLogLevel1(string text, bool successful)
        {
            Thread.Sleep(2000);
            Application.DoEvents();
            var firstLevelNode = tViewEvents.Nodes[tViewEvents.Nodes.Count - 1];
            if (successful)
            {
                firstLevelNode.Nodes.Add(new TreeNode(text + " [" + DateTime.Now + "]", 2, 2));
            }
            else
            {
                firstLevelNode.Nodes.Add(new TreeNode(text + " [" + DateTime.Now + "]", 3, 3));
            }
            tViewEvents.ExpandAll();
            tViewEvents.SelectedNode = firstLevelNode;
        }
        void AddTreeViewLogLevel1Info(string text)
        {
            Thread.Sleep(2000);
            Application.DoEvents();
            var firstLevelNode = tViewEvents.Nodes[tViewEvents.Nodes.Count - 1];
            firstLevelNode.Nodes.Add(new TreeNode(text + " [" + DateTime.Now + "]", 4, 4));
            tViewEvents.ExpandAll();
            tViewEvents.SelectedNode = firstLevelNode;
        }
        void AddTreeViewLogLevel2(string text, bool successful)
        {
            Thread.Sleep(2000);
            Application.DoEvents();
            var firstLevelNode = tViewEvents.Nodes[tViewEvents.Nodes.Count - 1];
            var secondLevelNode = firstLevelNode.Nodes[firstLevelNode.Nodes.Count - 1];
            if (successful)
            {
                secondLevelNode.Nodes.Add(new TreeNode(text + " [" + DateTime.Now + "]", 2, 2));
            }
            else
            {
                secondLevelNode.Nodes.Add(new TreeNode(text + " [" + DateTime.Now + "]", 3, 3));
            }
            tViewEvents.ExpandAll();
            tViewEvents.SelectedNode = secondLevelNode;
        }
        void AddTreeViewLogLevel2Info(string text)
        {
            Thread.Sleep(2000);
            Application.DoEvents();
            var firstLevelNode = tViewEvents.Nodes[tViewEvents.Nodes.Count - 1];
            var secondLevelNode = firstLevelNode.Nodes[firstLevelNode.Nodes.Count - 1];
            secondLevelNode.Nodes.Add(new TreeNode(text + " [" + DateTime.Now + "]", 4, 4));
            tViewEvents.ExpandAll();
            tViewEvents.SelectedNode = secondLevelNode;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Form2 frm= new Form2();
            frm.Show();
        }
    }
}
