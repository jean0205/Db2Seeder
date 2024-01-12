using As400DataAccess;
using Db2Seeder.API.Helpers;
using Db2Seeder.API.Models;
using Db2Seeder.API.Request;
using Db2Seeder.Business;
using Db2Seeder.Business.Benefit_Claims;
using Db2Seeder.Controls;
using Db2Seeder.NIS.SQL.Webportal.DataAccess;
using Db2Seeder.SQL.Alphacard.DataAccess;
using Db2Seeder.SQL.Logs.DataAccess;
using Db2Seeder.SQL.Logs.Helpers;
using Microsoft.AppCenter.Crashes;
using Microsoft.EntityFrameworkCore.Internal;
using ShareModels.Models;
using ShareModels.Models.Benefit_Claims;
using ShareModels.Models.Sickness_Claim;
using System;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Application = System.Windows.Forms.Application;

namespace Db2Seeder
{
    public partial class Form1 : Form
    {
        readonly EmployeeDB2 as400Empe = new EmployeeDB2();
        readonly EmployerDB2 as400Empr = new EmployerDB2();
        readonly AgePensionDB2 as400AgeBenefit = new AgePensionDB2();
        readonly DeathBenefitDB2 as400DeathBenefit = new DeathBenefitDB2();
        readonly FuneralBenefitDB2 as400FuneralBenefit = new FuneralBenefitDB2();
        readonly InvalidityDB2 as400InvalidityBenefit = new InvalidityDB2();
        readonly SicknessDB2 as400sicknessBenefit = new SicknessDB2();
        readonly SurvivorBenefitDB2 as400survivorBenefit = new SurvivorBenefitDB2();
        readonly EmpInjDisableDB2 as400DisablementBenefit = new EmpInjDisableDB2();
        readonly MaternityDB2 as400maternityBenefit = new MaternityDB2();
        readonly EmpInjuryBenefitDB2 as400EmploymentInjurBenefit = new EmpInjuryBenefitDB2();
        readonly Covid19DB2 as400CovidBenefit = new Covid19DB2();
        readonly UnemploymentBenefitDB2 as400UnemploymentBenefit = new UnemploymentBenefitDB2();
        readonly UnemploymentSEPBenefitDB2 as400UnemploymentSEPBenefit = new UnemploymentSEPBenefitDB2();
        readonly ElectRemittanceDB2 as400remittances = new ElectRemittanceDB2();
        readonly WebPortalSQLDB webPortalSQLDB = new WebPortalSQLDB();

        AlphacardDB alphacardDB = new AlphacardDB();


        bool working = false;
        bool cancelRequest = false;


        public Form1()
        {
            InitializeComponent();
            dataGridView1.DefaultCellStyle.BackColor = Color.Beige;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque;
            timer1.Stop();
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                var xx = (int)DateTime.Now.DayOfWeek;

                if (((dtpDFrom.Value.TimeOfDay < DateTime.Now.TimeOfDay && dtpDTo.Value.TimeOfDay > DateTime.Now.TimeOfDay) || (dtpNFrom.Value.TimeOfDay < DateTime.Now.TimeOfDay && dtpNTo.Value.TimeOfDay > DateTime.Now.TimeOfDay)) && UtilRecurrent.FindAllControlsIterative(tpanelDays, "RJToggleButton").Cast<RJToggleButton>().Where(x => x.Checked && int.Parse(x.Tag.ToString().Split(',')[1]) == (int)DateTime.Now.DayOfWeek).ToList().Any())
                {
                    label1.Text = "Back-up Time";
                    return;
                }
                if (!working && !cancelRequest)
                {
                    await Task.Run(() => DoWork());
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        private void button15_Click(object sender, EventArgs e)
        {
            if (working)
            {
                cancelRequest = true;
                label1.Text = "Waiting for the current process finish excecution.";
                timer1.Stop();
                button15.Enabled = false;
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                cancelRequest = false;
                working = false;
                timer1.Start();
                button16.Enabled = false;
                button15.Enabled = true;
            }
        }
        private async void DoWork()
        {
            working = true;
            BeginInvoke(new Action(() => label1.Text = "Process running."));
            if (!cancelRequest && rja.Checked) await EmployeeRegistrationRequest();
            if (!cancelRequest && rjb.Checked) await EmployerRegistrationRequest();
            if (!cancelRequest && rjc.Checked) await ComplianceCertificateRequest();
            if (!cancelRequest && rjd.Checked) await GetRemittancePendingReview();
            if (!cancelRequest && rje.Checked) await AgeBenefitClaimCompleted();
            if (!cancelRequest && rjf.Checked) await DeathBenefitClaimCompleted();
            if (!cancelRequest && rjg.Checked) await FuneralBenefitClaimCompleted();
            if (!cancelRequest && rjh.Checked) await InvalidityBenefitClaimCompleted();
            if (!cancelRequest && rji.Checked) await SicknessBenefitClaimCompleted();
            if (!cancelRequest && rji.Checked) await SicknessBenefitClaimCompletedSEForms();
            if (!cancelRequest && rjj.Checked) await MaternityBenefitClaimCompleted();
            if (!cancelRequest && rjj.Checked) await MaternityBenefitClaimCompletedSEP();
            if (!cancelRequest && rjk.Checked) await EmploymentInjuryBenefitClaimCompleted();
            if (!cancelRequest && rjk.Checked) await EmploymentInjuryBenefitClaimCompletedSEP();
            if (!cancelRequest && rjl.Checked) await CovidBenefitClaimCompleted();
            if (!cancelRequest && rjm.Checked) await SurvivorBenefitClaimCompleted();
            if (!cancelRequest && rjn.Checked) await DisablemetBenefitClaimCompleted();
            if (!cancelRequest && rjo.Checked) await UEB_EmployeesClaimPendingProcessing();
            if (!cancelRequest && rjp.Checked) await UEB_SEPClaimPendingProcessing();
            if (!cancelRequest && rjq.Checked) await UEB_TerminationCertificatePendingProcessing();
            if (!cancelRequest && rjr.Checked) await UEB_DeclarationsPendingProcessing();
            if (!cancelRequest && rjs.Checked) await UEB_DeclarationsSEPPendingProcessing();


            working = false;
            if (cancelRequest) BeginInvoke(new Action(() =>
            {
                button16.Enabled = true;
                label1.Text = "No Process Running.";
            }));
        }

        #region Buttoms

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await EmployeeRegistrationRequest();
                working = false;



            }
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await EmployerRegistrationRequest();
                working = false;
            }
        }
        private async void button4_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await ComplianceCertificateRequest();
                working = false;
            }
        }
        private async void button5_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await AgeBenefitClaimCompleted();
                //await AgeBenefitClaimTesting();
                working = false;
            }
        }
        private async void button3_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await GetRemittancePendingReview();
                working = false;
            }
        }
        private async void button7_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await DeathBenefitClaimCompleted();
                working = false;
            }
        }
        private async void button8_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await FuneralBenefitClaimCompleted();
                working = false;
            }
        }
        private async void button6_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await InvalidityBenefitClaimCompleted();
                working = false;
            }
        }
        private async void button9_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await SicknessBenefitClaimCompleted();
                await SicknessBenefitClaimCompletedSEForms();
                working = false;
            }
        }
        private async void button10_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await SurvivorBenefitClaimCompleted();
                working = false;
            }
        }
        private async void button11_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await DisablemetBenefitClaimCompleted();
                working = false;
            }
        }
        private async void button12_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                //TODO: actializar para q lea y postee los maternity grant, otro support request y otro workflow tambien, no employer number. Esperar a q aprueben el q esta pending. 
                await MaternityBenefitClaimCompleted();
                await MaternityBenefitClaimCompletedSEP();
                working = false;
            }
        }
        private async void button13_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await EmploymentInjuryBenefitClaimCompleted();
                await EmploymentInjuryBenefitClaimCompletedSEP();
                working = false;
            }
        }
        private async void button14_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await CovidBenefitClaimCompleted();
                working = false;
            }
        }
        private async void button21_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await UEB_EmployeesClaimPendingProcessing();

                working = false;
            }
        }
        private async void button20_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await UEB_DeclarationsPendingProcessing();

                working = false;
            }
        }
        private async void button24_Click(object sender, EventArgs e)
        {

            if (!working)
            {
                await UEB_DeclarationsSEPPendingProcessing();

                working = false;
            }

        }
        private async void button22_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await UEB_SEPClaimPendingProcessing();

                working = false;
            }




        }
        private async void button23_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                await UEB_TerminationCertificatePendingProcessing();

                working = false;
            }
        }
        private async void button25_Click(object sender, EventArgs e)
        {

            if (!working)
            {
                await GetRemittanceSEPPendingReview();
                working = false;
            }
        }

        #endregion

        #region Employee-Employer
        private async Task EmployeeRegistrationRequest()
        {
            AddTreeViewLogLevel0("Employee Registration Requests");
            AddTreeViewLogLevel1Info("Getting Employee Requests Completed");
            try
            {
                var requests = await EmployeeRegistration.GetEmployeeRegistrationSupportRequestCompleted();
                requests.AddRange(await EmployeeRegistration.GetSEPSupportRequestCompleted());
                requests.AddRange(await EmployeeRegistration.GetVoluntarySupportRequestCompleted());


                if (requests.Any())
                {
                    AddTreeViewLogLevel1(requests.Count + " Requests Completed Found", true);
                    foreach (var request in requests)
                    {
                        PlayExclamation();
                        if (cancelRequest) return;
                        var document = new Document_Employee();
                        AddTreeViewLogLevel1Info("Getting Employee Details");
                        try
                        {
                            document = await EmployeeRegistration.EmployeeRegistrationRequestDetail(request);
                            if (document != null)
                            {
                                AddTreeViewLogLevel1("Employee details successfully loaded", true);
                                document.EmployerNo = 0;
                                if (document.registrationType == 0)
                                {
                                    AddTreeViewLogLevel1("Posting Employee", true);
                                    document.nisNo = await as400Empe.InsertEmployees(document);
                                    //insert nixpert
                                    await alphacardDB.InsertXportrecord(document, document.nisNo.ToString());

                                    if (document.nisNo != 0)
                                    {
                                        await MappAndAssingEmployeeRol(request, document);
                                        await CreateCommentToPost(request.supportRequestId, 3, "Employee with NIS number: " + document.nisNo + " successfully created.");
                                        await LogsHelper.SaveEmployeeLOG(request, document);
                                    }
                                }
                                if (document.registrationType == 2)
                                {
                                    if (document.nisNo == 0)
                                    {
                                        AddTreeViewLogLevel1("Posting Self-Employee (Employee)", true);
                                        document.nisNo = await as400Empe.InsertEmployees(document);

                                        //insert nixpert
                                        await alphacardDB.InsertXportrecord(document, document.nisNo.ToString());

                                        if (document.nisNo != 0)
                                        {
                                            await MappAndAssingEmployeeRol(request, document);
                                            await CreateCommentToPost(request.supportRequestId, 3, "Employee with NIS number: " + document.nisNo + " successfully created.");
                                            await LogsHelper.SaveEmployeeLOG(request, document);
                                        }
                                    }
                                    AddTreeViewLogLevel1("Posting Self-Employee (Employer)", true);

                                    document.EmployerNo = await as400Empr.InsertSelfEmployers(document);

                                    if (document.EmployerNo != 0)
                                    {
                                        await MappAndAssingSelfEmployer(request, document);
                                        await CreateCommentToPost(request.supportRequestId, 3, "Employer with number: " + document.EmployerNo + " successfully created.");
                                        await LogsHelper.SaveSelfEmployeeLOG(request, document);
                                    }
                                }
                                if (document.registrationType == 3)
                                {
                                    AddTreeViewLogLevel1("Posting Voluntary (Employer)", true);
                                    document.EmployerNo = await as400Empr.InsertSelfEmployers(document);
                                    if (document.EmployerNo != 0)
                                    {
                                        await MappAndAssingSelfEmployer(request, document);
                                        await CreateCommentToPost(request.supportRequestId, 3, "Employer (Voluntary Contributor) with number: " + document.EmployerNo + " successfully created.");
                                        await LogsHelper.SaveVoluntaryLOG(request, document);
                                    }
                                }

                                if (document.nisNo == 0 && document.EmployerNo == 0)
                                {
                                    AddTreeViewLogLevel1("Error inserting employee to the  DB2 database.", false);
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Employee with NIS number: " + document.nisNo + " successfully saved to the DB2 database", true);

                                    //updating worflow state
                                    var responseA = await EmployeeRegistration.UpdateWorkFlowStateEmployee(3, request.supportRequestId, 161);
                                    if (responseA.IsSuccess)
                                    {
                                        AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                    }

                                    //###SAVING DOCUMENTS
                                    try
                                    {
                                        AddTreeViewLogLevel2Info("Saving Employee Documents.");
                                        int savedAtt = await EmployeeRegistration.RequestAttachmentToScannedDocuments(request, document);
                                        AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                    }
                                    catch (Exception ex)
                                    {
                                        Crashes.TrackError(ex);
                                        AddTreeViewLogLevel2("Error " + ex.Message, false);
                                        await LogsHelper.SaveErrorLOG(ex.Message, request, document.employeeRegistrationFormId, document.CompletedTime);
                                    }
                                }
                            }
                            else
                            {
                                AddTreeViewLogLevel1("Error Getting Employee Details.", false);
                            }
                        }
                        catch (Exception ex)
                        {
                            Crashes.TrackError(ex);
                            AddTreeViewLogLevel1("Error " + ex.Message, false);
                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.employeeRegistrationFormId, document.CompletedTime);
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
                await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
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
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_Employer();
                            AddTreeViewLogLevel1Info("Getting Employer Details");
                            try
                            {
                                document = await EmployerRegistration.EmployerRegistrationRequestDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Employer details successfully loaded", true);
                                    document.employerNo = await as400Empr.InsertEmployers(document);
                                    if (document.employerNo == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting employer to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Employer with number: " + document.employerNo + " successfully saved to the DB2 database.", true);

                                        await CreateCommentToPost(request.supportRequestId, 3, "Employer with number: " + document.employerNo + " successfully created.");
                                        await LogsHelper.SaveEmployerLOG(request, document);

                                        //updating worflow state
                                        var responseA = await EmployerRegistration.UpdateWorkFlowStateEmployee(3, request.supportRequestId, 162);
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
                                            int savedAtt = await EmployerRegistration.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.employerRegistrationFormId, document.CompletedTime);
                                        }
                                        await MappAndAssingEmployerRol(request, document);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Employer Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.employerRegistrationFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        private async Task MappAndAssingEmployerRol(SupportRequest request, Document_Employer document)
        {
            try
            {
                AddTreeViewLogLevel2Info("Mapping Employer Number to Web Portal Account.");
                var response = await EmployerRegistration.AddNISMapping(request, (int)document.employerNo);
                if (response.IsSuccess)
                {
                    AddTreeViewLogLevel2(document.employerNo + " Successfully Mapped.", true);
                }
                else
                {
                    AddTreeViewLogLevel2("Error mapping Employer number: " + document.employerNo + " " + response.Message, false);
                    await LogsHelper.SaveErrorLOG("Error mapping Employer number: " + document.employerNo, request, document.employerRegistrationFormId, document.CompletedTime);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                AddTreeViewLogLevel2("Error " + ex.Message, false);
                await LogsHelper.SaveErrorLOG(ex.Message, request, document.employerRegistrationFormId, document.CompletedTime);
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
                await LogsHelper.SaveErrorLOG(ex.Message, request, document.employerRegistrationFormId, document.CompletedTime);
            }
        }
        private async Task MappAndAssingSelfEmployer(SupportRequest request, Document_Employee document)
        {
            try
            {
                AddTreeViewLogLevel2Info("Mapping Employer Number to Web Portal Account.");
                var response = await EmployerRegistration.AddNISMapping(request, (int)document.EmployerNo);
                if (response.IsSuccess)
                {
                    AddTreeViewLogLevel2(document.EmployerNo + " Successfully Mapped.", true);
                }
                else
                {
                    AddTreeViewLogLevel2("Error mapping Employer number: " + document.EmployerNo + " " + response.Message, false);
                    await LogsHelper.SaveErrorLOG("Error mapping Employer number: " + document.EmployerNo, request, document.employeeRegistrationFormId, document.CompletedTime);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                AddTreeViewLogLevel2("Error " + ex.Message, false);
                await LogsHelper.SaveErrorLOG(ex.Message, request, document.employeeRegistrationFormId, document.CompletedTime);
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
                await LogsHelper.SaveErrorLOG(ex.Message, request, document.employeeRegistrationFormId, document.CompletedTime);
            }
        }
        private async Task MappAndAssingEmployeeRol(SupportRequest request, Document_Employee document)
        {
            //TODO: validar tambien q no sea sub account since voy a pedir q el employee registration sea agregado a las opciones de los sub accounts
            //si no es employer y no tiene  employee link entonces hago el link automatico
            if (!await ApiRequest.IsEmployer(request.ownerId) && !await ApiRequest.IsEmployee(request.ownerId))
            {
                try
                {
                    AddTreeViewLogLevel2Info("Mapping NIS Number to Web Portal Account.");
                    var response = await EmployeeRegistration.AddNISMapping(request, document);
                    if (response.IsSuccess)
                    {
                        AddTreeViewLogLevel2(document.nisNo + " Successfully Mapped.", true);
                    }
                    else
                    {
                        AddTreeViewLogLevel2("Error mapping NIS number: " + document.nisNo + " " + response.Message, false);
                        await LogsHelper.SaveErrorLOG("Error mapping NIS number: " + document.nisNo, request, document.employeeRegistrationFormId, document.CompletedTime);
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    AddTreeViewLogLevel2("Error " + ex.Message, false);
                    await LogsHelper.SaveErrorLOG(ex.Message, request, document.employeeRegistrationFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, request, document.employeeRegistrationFormId, document.CompletedTime);
                }
            }
        }
        private async Task<bool> CreateCommentToPost(int supportRequesId, int userAccountId, string commentText)
        {
            try
            {
                SupportRequestComment comment = new SupportRequestComment
                {
                    supportRequestId = supportRequesId,
                    userAccountId = userAccountId,
                    comment = commentText
                };
                return await ApiRequest.AddSupportRequestComment(comment);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return false;
            }
        }

        #endregion

        #region Compliance Certificate
        private async Task ComplianceCertificateRequest()
        {
            try
            {
                AddTreeViewLogLevel0("Compliance Certificate");
                AddTreeViewLogLevel1Info("Getting Compliance Certificate Request Approved");
                try
                {
                    var requests = await ComplianceCertificate.GetComplianceCertfCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Request Completed Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_ComplianceCert();
                            AddTreeViewLogLevel1Info("Getting Request Details");
                            try
                            {
                                document = await ComplianceCertificate.ComplianceCertRequestDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Request details successfully loaded", true);
                                    AddTreeViewLogLevel1Info("Inserting Compliance Certificate in SQL.");

                                    if (await ComplianceCertificate.InsertComplianceCertificateSQL(request, document))
                                    {
                                        AddTreeViewLogLevel2("Compliance Certificate successfully saved to SQL.", true);
                                        await CreateCommentToPost(request.supportRequestId, 3,
                                            @"Dear Employer,
                                        Your application for a Compliance Certificate has been received.
                                         Please note, for your application to be approved, all remittances and                  payments must be submitted. Once your records are up to date, your                     Compliance Certificate would be provided within 24 hours.
                                           For any comments, questions or feedback, please reply to                               compliance@nisgrenada.org
                                            Kindest regards,
                                            Franca Belle
                                            Compliance Manager");
                                        try
                                        {
                                            await LogsHelper.SaveComplianceLOG(request, document);
                                        }
                                        catch (Exception)
                                        {
                                        }

                                        //updating worflow state
                                        var responseA = await ComplianceCertificate.UpdateWorkFlowStateEmployee(3, request.supportRequestId, 3);

                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                        }
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel2("Error Saving Compliance Certificate to SQL.", false);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Compliance Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.documentId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Remittance

        //TODO 
        //PONERLE LOGS A LOS REMITTANCE TAMBIEN
        private async Task GetRemittancePendingReview()
        {
            try
            {
                AddTreeViewLogLevel0("Electronic Remittance");
                AddTreeViewLogLevel1Info("Getting Electronic Remittance Pending Review");
                try
                {
                    var requests = await Remittance.GetRemittancePending();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Remittance Pending Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            AddTreeViewLogLevel1Info("Getting Remittance Pending Details");
                            try
                            {
                                var document = await Remittance.RemittanceDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Remittance details successfully loaded", true);
                                    try
                                    {

                                        AddTreeViewLogLevel2Info("Posting Remittance to As400.");
                                        await Remittance.PostRemittanceToAs400(request, document);
                                        AddTreeViewLogLevel2("Remittance Succesfully Posted.", true);

                                        //agregar el mensage cuando me den el text.
                                        // await CreateCommentToPost(request.supportRequestId, 3, "this is a test.");


                                        var responseA = await EmployerRegistration.UpdateWorkFlowStateEmployee(3, request.supportRequestId, 7);

                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Crashes.TrackError(ex);
                                        AddTreeViewLogLevel2("Error " + ex.Message, false);
                                        await LogsHelper.SaveErrorLOG(ex.Message, request, document.remittanceFormId, DateTime.Now);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Remittance Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, null, DateTime.Now);
                            }
                        }
                    }
                    else
                    {
                        AddTreeViewLogLevel1Info("No Pending Remittance were Found.");
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    AddTreeViewLogLevel1("Error " + ex.Message, false);
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        private async Task GetRemittanceSEPPendingReview()
        {
            try
            {
                AddTreeViewLogLevel0("Electronic Remittance SEP");
                AddTreeViewLogLevel1Info("Getting Electronic Remittance SEP");
                try
                {
                    var requests = await Remittance.GetRemittancePendingSEP();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Remittance Pending Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            AddTreeViewLogLevel1Info("Getting Remittance Pending Details");
                            try
                            {
                                var document = await Remittance.RemittanceDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Remittance details successfully loaded", true);
                                    try
                                    {

                                        AddTreeViewLogLevel2Info("Posting Remittance to As400.");
                                        await Remittance.PostRemittanceToAs400(request, document);
                                        AddTreeViewLogLevel2("Remittance Succesfully Posted.", true);

                                        //agregar el mensage cuando me den el text.
                                        // await CreateCommentToPost(request.supportRequestId, 3, "this is a test.");


                                        var responseA = await EmployerRegistration.UpdateWorkFlowStateEmployee(3, request.supportRequestId, 2340);

                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Crashes.TrackError(ex);
                                        AddTreeViewLogLevel2("Error " + ex.Message, false);
                                        await LogsHelper.SaveErrorLOG(ex.Message, request, document.remittanceFormId, DateTime.Now);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Remittance Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, null, DateTime.Now);
                            }
                        }
                    }
                    else
                    {
                        AddTreeViewLogLevel1Info("No Pending Remittance were Found.");
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    AddTreeViewLogLevel1("Error " + ex.Message, false);
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Benefit Claims

        //Completed,modificarla cuando lotek agrege el NIs del survivor si esta cobrando alguno
        private async Task AgeBenefitClaimCompleted()
        {
            try
            {
                AddTreeViewLogLevel0("Age Benefit");
                AddTreeViewLogLevel1Info("Getting Age Benefit Claims Completed");
                try
                {
                    var requests = await AgeBenefit.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Completed Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_AgeBenefit();
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            try
                            {
                                document = await AgeBenefit.ClaimDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Claim details successfully loaded", true);
                                    //as400AgeBenefit.As400_lib = "NI";
                                    document.ClaimNumber = await as400AgeBenefit.InsertAgePension(document);


                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state
                                        var responseA = await AgeBenefit.UpdateWorkFlowState(3, request.supportRequestId, 235);
                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "AGE", "0", document.nisNo, document.firstName + document.otherName, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                    //comentar si no se quieren salvar los documentos nuevamente
                                    try
                                    {
                                        AddTreeViewLogLevel2Info("Saving Employee Documents.");
                                        int savedAtt = await AgeBenefit.RequestAttachmentToScannedDocuments(request, document);

                                        // //posting in testing
                                        //as400AgeBenefit.As400_lib = "TT";                                           
                                        //await as400AgeBenefit.InsertAgePension(document);
                                       // int savedAt = await AgeBenefit.RequestAttachmentToScannedDocumentsTest(request, document);

                                        AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                    }
                                    catch (Exception ex)
                                    {
                                        Crashes.TrackError(ex);
                                        AddTreeViewLogLevel2("Error " + ex.Message, false);
                                        await LogsHelper.SaveErrorLOG(ex.Message, request, document.ageBenefitFormId, document.CompletedTime);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }

                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.ageBenefitFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        //va a ser el mismo q Survivor cuando Lotek lo termine
        private async Task DeathBenefitClaimCompleted()
        {
            try
            {
                AddTreeViewLogLevel0("Death Benefit");
                AddTreeViewLogLevel1Info("Getting Death Benefit Claims Completed");
                try
                {
                    var requests = await DeathBenefit.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Completed Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_DeathBenefit();
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            try
                            {
                                document = await DeathBenefit.ClaimDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Claim details successfully loaded", true);
                                    document.ClaimNumber = await as400DeathBenefit.InsertDeathBenefit(document);
                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state
                                        var responseA = await DeathBenefit.UpdateWorkFlowState(3, request.supportRequestId, 237);
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
                                            int savedAtt = await DeathBenefit.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.SurvivorBenefitFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "DEATH", "0", document.NisNo, document.FirstName + document.OtherName, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.SurvivorBenefitFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        //lee cuando no hay nulos         
        private async Task FuneralBenefitClaimCompleted()
        {
            try
            {
                AddTreeViewLogLevel0("Funeral Benefit");
                AddTreeViewLogLevel1Info("Getting Funeral Benefit Claims Ready for Processing");
                try
                {
                    var requests = await FuneralBenefit.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Ready for Processing Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_FuneralBenefit();
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            try
                            {
                                document = await FuneralBenefit.ClaimDetail(request);
                                if (document != null)
                                {
                                    if (string.IsNullOrEmpty(document.ClaimantNisNo))
                                    {
                                        AddTreeViewLogLevel1("Claimant NIS not provided.", false);
                                        return;
                                    }
                                    AddTreeViewLogLevel1("Claim details successfully loaded", true);
                                    document.ClaimNumber = await as400FuneralBenefit.InsertFuneral(document);
                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state
                                        var responseA = await FuneralBenefit.UpdateWorkFlowState(3, request.supportRequestId, 239);
                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to Processing", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to Processing. " + responseA.Message, false);
                                        }
                                        try
                                        {
                                            AddTreeViewLogLevel2Info("Saving Employee Documents.");
                                            int savedAtt = await FuneralBenefit.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.FuneralGrantBenefitFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "FUNERAL", "0", document.NisNo, document.FirstName + document.OtherName, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.FuneralGrantBenefitFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async Task InvalidityBenefitClaimCompleted()
        {
            try
            {
                AddTreeViewLogLevel0("Invalidity Benefit");
                AddTreeViewLogLevel1Info("Getting Invalidity Benefit Claims Completed");
                try
                {
                    var requests = await InvalidityBenefit.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Completed Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_Invalidity();
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            try
                            {
                                document = await InvalidityBenefit.ClaimDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Claim details successfully loaded", true);
                                    document.ClaimNumber = await as400InvalidityBenefit.InsertInvalidity(document);
                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state
                                        var responseA = await InvalidityBenefit.UpdateWorkFlowState(3, request.supportRequestId, 166);
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
                                            int savedAtt = await InvalidityBenefit.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.InvalidityBenefitFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "INVALIDITY", "0", document.NisNo, document.FirstName + document.OtherName, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.InvalidityBenefitFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        //Completed
        private async Task SicknessBenefitClaimCompleted()
        {
            try
            {
                AddTreeViewLogLevel0("Sickness Benefit");
                AddTreeViewLogLevel1Info("Getting Sickness Benefit Claims Ready For Processing");
                try
                {
                    var requests = await SicknessBenefit.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Ready For Processing Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_Sickness();
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            try
                            {
                                document = await SicknessBenefit.ClaimDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Claim details successfully loaded", true);

                                    // as400sicknessBenefit.As400_lib = "NI";
                                    document.ClaimNumber = await as400sicknessBenefit.InsertSickness(document, null);
                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state
                                        var responseA = await SicknessBenefit.UpdateWorkFlowState(3, request.supportRequestId, 242);
                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to Processing", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to Processing " + responseA.Message, false);
                                        }
                                        try
                                        {
                                            AddTreeViewLogLevel2Info("Saving  Documents.");

                                            int savedAt = await SicknessBenefit.RequestAttachmentToScannedDocuments(request, document);

                                            // //posting in testing
                                            //  as400sicknessBenefit.As400_lib = "TT";                                           
                                            //await as400sicknessBenefit.InsertSickness(document, document.ClaimNumber);
                                            // int savedAt = await SicknessBenefit.RequestAttachmentToScannedDocumentsTest(request, document);

                                            AddTreeViewLogLevel2(savedAt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.sicknessBenefitFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "SICKNESS", document.employerNis, document.nisNo, document.firstName + document.otherName, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.sicknessBenefitFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async Task SicknessBenefitClaimCompletedSEForms()
        {
            try
            {
                AddTreeViewLogLevel0("Sickness Benefit-SEP Forms");
                AddTreeViewLogLevel1Info("Getting Sickness Benefit SEP Claims Ready For Processing");
                try
                {
                    var requests = await SicknessBenefit.GetClaimsCompletedSEP();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Ready For Processing Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_Sickness();
                            AddTreeViewLogLevel1Info("Getting Claim Details SEP");
                            try
                            {
                                document = await SicknessBenefit.ClaimDetailSEP(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Claim details successfully loaded SEP", true);


                                    document.ClaimNumber = await as400sicknessBenefit.InsertSicknessSEP(document, null);
                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state
                                        var responseA = await SicknessBenefit.UpdateWorkFlowStateSEP(3, request.supportRequestId, 1339);
                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to Processing", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to Processing " + responseA.Message, false);
                                        }
                                        try
                                        {
                                            AddTreeViewLogLevel2Info("Saving  Documents.");

                                            int savedAt = await SicknessBenefit.RequestAttachmentToScannedDocuments(request, document);

                                            AddTreeViewLogLevel2(savedAt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.sicknessBenefitFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "SICKNESS-SEP", document.employerNis, document.nisNo, document.firstName + document.otherName, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }

                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.sicknessBenefitFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async Task SurvivorBenefitClaimCompleted()
        {
            try
            {
                AddTreeViewLogLevel0("Survivor Benefit");
                AddTreeViewLogLevel1Info("Getting Survivor Benefit Claims Completed");
                try
                {
                    var requests = await SurvivorBenefit.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Completed Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_SurvivorBenefit();
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            try
                            {
                                document = await SurvivorBenefit.ClaimDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Claim details successfully loaded", true);
                                    document.ClaimNumber = await as400survivorBenefit.InsertSurvivorBenefit(document);
                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        // updating worflow state
                                        var responseA = await SurvivorBenefit.UpdateWorkFlowState(11, request.supportRequestId, 243);
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
                                            int savedAtt = await SurvivorBenefit.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.SurvivorBenefitFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "survivor", "0", document.NisNo, document.FirstName + document.OtherName, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }

                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.SurvivorBenefitFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        //Completed
        private async Task DisablemetBenefitClaimCompleted()
        {
            try
            {
                AddTreeViewLogLevel0("Disablemet Benefit");
                AddTreeViewLogLevel1Info("Getting Disablemet Benefit Claims Completed");
                try
                {
                    var requests = await DisablementBenefit.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Ready For Processing Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_Disablemet();
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            try
                            {
                                document = await DisablementBenefit.ClaimDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Claim details successfully loaded", true);
                                    document.ClaimNumber = await as400DisablementBenefit.InsertEmpInjDisable(document);
                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state
                                        var responseA = await DisablementBenefit.UpdateWorkFlowState(3, request.supportRequestId, 238);
                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to Processing", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to Processing. " + responseA.Message, false);
                                        }
                                        try
                                        {
                                            AddTreeViewLogLevel2Info("Saving Employee Documents.");
                                            int savedAtt = await DisablementBenefit.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.DisablementBenefitFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "DISABLEMENT", "0", document.NisNo, document.FirstName + document.Name, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }

                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.DisablementBenefitFormId, document.CompletedTime);
                            }
                        }
                    }
                    else
                    {
                        AddTreeViewLogLevel1Info("No Ready For Processing Claims were Found.");
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    AddTreeViewLogLevel1("Error " + ex.Message, false);
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        //funciona bien cuando el last date worked no es nulo
        private async Task MaternityBenefitClaimCompleted()
        {
            try
            {
                AddTreeViewLogLevel0("Maternity Benefit");
                AddTreeViewLogLevel1Info("Getting Maternity Benefit Claims Ready For Processing");
                try
                {
                    var requests = await MaternityBenefit.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Ready For Processing Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_Maternity();
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            try
                            {
                                document = await MaternityBenefit.ClaimDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Claim details successfully loaded", true);
                                    //find if the person is SEP
                                    bool SEP = false;
                                    if (document.EmployerNis != null)
                                    {
                                        if (document.EmployerNis.Split('-')[0] == document.NisNo.ToString())
                                        {
                                            SEP = true;
                                        }
                                    }
                                    else
                                    {
                                        SEP = await webPortalSQLDB.IsSelfEmployedPerson(document.NisNo.ToString());
                                    }
                                    if (request.supportRequestTypeId == 10 && document.Gender == 2)
                                    {
                                        document.BenefitApply = "Grant & Allowance";
                                    }
                                    else
                                    {
                                        document.BenefitApply = "Maternity Grant";
                                    }
                                    document.ClaimNumber = await as400maternityBenefit.InsertMaternity(document, SEP);
                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state for Grant & Allowance and Maternity Allowance is one, for Grant allong is other
                                        var responseA = document.BenefitApply == "Maternity Grant" ? await MaternityBenefit.UpdateWorkFlowState(3, request.supportRequestId, 313) : await MaternityBenefit.UpdateWorkFlowState(3, request.supportRequestId, 241);

                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to Processing", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to Processing. " + responseA.Message, false);
                                        }
                                        try
                                        {
                                            AddTreeViewLogLevel2Info("Saving Employee Documents.");
                                            int savedAtt = await MaternityBenefit.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.MaternityBenefitFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "MATERNITY", document.EmployerNis, document.NisNo, document.FirstName + document.OtherName, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.MaternityBenefitFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        private async Task MaternityBenefitClaimCompletedSEP()
        {
            try
            {
                AddTreeViewLogLevel0("Maternity Benefit SEP");
                AddTreeViewLogLevel1Info("Getting Maternity Benefit SEP Claims Ready For Processing");
                try
                {
                    var requests = await MaternityBenefit.GetClaimsCompletedSEP();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Ready For Processing Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_Maternity();
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            try
                            {
                                document = await MaternityBenefit.ClaimDetailSEP(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Claim details successfully loaded", true);

                                    if (document.Gender == 2)
                                    {
                                        document.BenefitApply = "Grant & Allowance";
                                    }
                                    else
                                    {
                                        document.BenefitApply = "Maternity Grant";
                                    }

                                    document.ClaimNumber = await as400maternityBenefit.InsertMaternity(document, true);

                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);

                                        var responseA = await MaternityBenefit.UpdateWorkFlowStateSEP(3, request.supportRequestId, 1366);

                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to Processing", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to Processing. " + responseA.Message, false);
                                        }
                                        try
                                        {
                                            AddTreeViewLogLevel2Info("Saving Employee Documents.");
                                            int savedAtt = await MaternityBenefit.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.MaternityBenefitFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "MATERNITY", document.EmployerNis, document.NisNo, document.FirstName + document.OtherName, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.MaternityBenefitFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        //esperando respuesta del correo a nigel
        private async Task EmploymentInjuryBenefitClaimCompleted()
        {
            try
            {
                AddTreeViewLogLevel0("Employment Injury Benefit");
                AddTreeViewLogLevel1Info("Getting Employment Injury Benefit Claims Completed");
                try
                {
                    var requests = await EmployInjuryBenefit.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Completed Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_EmploymentInjury();
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            try
                            {
                                document = await EmployInjuryBenefit.ClaimDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Claim details successfully loaded", true);
                                    document.ClaimNumber = await as400EmploymentInjurBenefit.InsertEmpInjuryBenefit(document, false);
                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state
                                        var responseA = await EmployInjuryBenefit.UpdateWorkFlowState(3, request.supportRequestId, 244);
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
                                            int savedAtt = await EmployInjuryBenefit.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.InjuryBenefitFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "EMPLOYMENTINJ", document.EmployerNis, document.NisNo, document.FirstName + document.OtherName, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.InjuryBenefitFormId, document.CompletedTime);
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
                await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
            }
        }
        private async Task EmploymentInjuryBenefitClaimCompletedSEP()
        {
            try
            {
                AddTreeViewLogLevel0("Employment Injury Benefit SEP");
                AddTreeViewLogLevel1Info("Getting Employment Injury Benefit SEP Claims Completed");
                try
                {
                    var requests = await EmployInjuryBenefit.GetClaimsCompletedSEP();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Completed Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_EmploymentInjury();
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            try
                            {
                                document = await EmployInjuryBenefit.ClaimDetailSEP(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Claim details successfully loaded", true);
                                    document.ClaimNumber = await as400EmploymentInjurBenefit.InsertEmpInjuryBenefit(document, true);
                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state
                                        var responseA = await EmployInjuryBenefit.UpdateWorkFlowStateSEP(3, request.supportRequestId, 1358);
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
                                            int savedAtt = await EmployInjuryBenefit.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.InjuryBenefitFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "EMPLOYMENTINJ", document.EmployerNis, document.NisNo, document.FirstName + document.OtherName, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.InjuryBenefitFormId, document.CompletedTime);
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
                await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
            }
        }

        //Completed
        private async Task CovidBenefitClaimCompleted()
        {
            try
            {
                AddTreeViewLogLevel0("Covid Benefit");
                AddTreeViewLogLevel1Info("Getting Covid Benefit Claims Ready for Processing.");
                try
                {
                    var requests = await CovidBenefit.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Ready for Processing Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_Covid19();
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            try
                            {
                                document = await CovidBenefit.ClaimDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Claim details successfully loaded", true);
                                    document.ClaimNumber = await as400CovidBenefit.InsertCovid19(document);
                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state
                                        var responseA = await CovidBenefit.UpdateWorkFlowState(3, request.supportRequestId, 236);
                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to Processing", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to Processing. " + responseA.Message, false);
                                        }
                                        try
                                        {
                                            AddTreeViewLogLevel2Info("Saving Employee Documents.");
                                            int savedAtt = await CovidBenefit.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.CovidBenefitFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "COVID", document.EmployerNis, document.NisNo, document.FirstName + document.OtherName, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.CovidBenefitFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        private async Task UEB_EmployeesClaimPendingProcessing()
        {
            try
            {
                AddTreeViewLogLevel0("Unemployment Benefit EMPLOYEES");
                AddTreeViewLogLevel1Info("Getting UEB EMPLOYEES Benefit Claims Pending  Processing.");
                try
                {
                    var requests = await UEB_EmpeBenefit.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Pending Processing Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_UEB_Empe();
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            try
                            {
                                document = await UEB_EmpeBenefit.ClaimDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Claim details successfully loaded", true);

                                    // as400UnemploymentBenefit.As400_lib = "NI";
                                    document.ClaimNumber = await as400UnemploymentBenefit.InsertUnemployment(document, null);

                                    await UEB_EmpeBenefit.SaveRequestClaimMapping(request.supportRequestId, (int)document.ClaimNumber);
                                    //salvar el mapping para el termination certificate tambien
                                    if (document.certificateTerminationLayoffFormSupportRequestId != null)
                                    {
                                        await UEB_EmpeBenefit.UpdateTerminationCertificateLinkedClaim((long)document.certificateTerminationLayoffFormSupportRequestId, request.supportRequestId, (int)document.ClaimNumber);
                                    }
                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state
                                        var responseA = await UEB_EmpeBenefit.UpdateWorkFlowState(3, request.supportRequestId, 286);
                                        await UEB_EmpeBenefit.UpdateWorkFlowState(3, request.supportRequestId, 287);

                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to Processing", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to Processing. " + responseA.Message, false);
                                        }
                                        try
                                        {
                                            AddTreeViewLogLevel2Info("Saving Claim Documents.");
                                            int savedAtt = await UEB_EmpeBenefit.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);


                                            // //saving in testing
                                            // as400UnemploymentBenefit.As400_lib = "TT";
                                            //await as400UnemploymentBenefit.InsertUnemployment(document,document.ClaimNumber);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.unemploymentRegularEmployeeClaimFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "UEB_Employees", document.ClaimNumber.ToString(), long.Parse(document.nisNo), document.firstName + document.surname, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.unemploymentRegularEmployeeClaimFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async Task UEB_SEPClaimPendingProcessing()
        {
            try
            {
                AddTreeViewLogLevel0("Unemployment Benefit SEP");
                AddTreeViewLogLevel1Info("Getting UEB SEP Benefit Claims Pending  Processing.");
                try
                {
                    var requests = await UEB_SEPBenefit.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Pending Processing Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_UEB_SEP();
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            try
                            {
                                //as400UnemploymentSEPBenefit.As400_lib = "NI";
                                document = await UEB_SEPBenefit.ClaimDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Claim details successfully loaded", true);

                                    document.ClaimNumber = await as400UnemploymentSEPBenefit.InsertUnemployment(document, null);

                                    await UEB_EmpeBenefit.SaveRequestClaimMapping(request.supportRequestId, (int)document.ClaimNumber);

                                    //salvar el mapping para el termination certificate tambien
                                    if (document.certificateTerminationLayoffFormSupportRequestId != null)
                                    {
                                        await UEB_EmpeBenefit.UpdateTerminationCertificateLinkedClaim((long)document.certificateTerminationLayoffFormSupportRequestId, request.supportRequestId, (int)document.ClaimNumber);
                                    }
                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state
                                        //TODO si los voy a coger en pending processing tengo q moverlos dos veces hasta posted, o cambiar los actions para q mueva para el stae 246 de una sola vez
                                        var responseA = await UEB_SEPBenefit.UpdateWorkFlowState(3, request.supportRequestId, 295);
                                        await UEB_SEPBenefit.UpdateWorkFlowState(3, request.supportRequestId, 298);
                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to Processing", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to Processing. " + responseA.Message, false);
                                        }
                                        try
                                        {
                                            AddTreeViewLogLevel2Info("Saving Claim Documents.");
                                            int savedAtt = await UEB_SEPBenefit.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);

                                            ////saving in testing
                                            //as400UnemploymentSEPBenefit.As400_lib = "TT";
                                            //await as400UnemploymentSEPBenefit.InsertUnemployment(document, document.ClaimNumber);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.unemploymentSelfEmployedClaimFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "UEB_SEP", document.ClaimNumber.ToString(), long.Parse(document.nisNo), document.firstName + document.surname, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.unemploymentSelfEmployedClaimFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async Task UEB_DeclarationsPendingProcessing()
        {
            try
            {
                AddTreeViewLogLevel0("Unemployment DECLARATIONS");
                AddTreeViewLogLevel1Info("Getting UEB DECLARATIONS Pending  Processing.");
                try
                {
                    var requests = await UEB_Declaration.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + "DECLARATIONS Pending Processing Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_UEB_Declaration();
                            AddTreeViewLogLevel1Info("Getting DECLARATION Details");
                            try
                            {
                                document = await UEB_Declaration.ClaimDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("DECLARATION details successfully loaded", true);

                                    var claimMapping = await UEB_Declaration.GetClaimRequestMappingByRequestId((long)document.unemploymentRegularEmployeeClaimFormId);
                                    if (claimMapping != null)
                                    {
                                        document.ClaimNumber = int.Parse(claimMapping.ClaimNumber.ToString());
                                    }
                                    else
                                    {
                                        document.ClaimNumber = 0;
                                    }


                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error getting claim mapping.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("DECLARATION for claim number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state
                                        var responseA = await UEB_Declaration.UpdateWorkFlowState(3, request.supportRequestId, 305);
                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to Posted", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to Posted. " + responseA.Message, false);
                                        }
                                        try
                                        {
                                            AddTreeViewLogLevel2Info("Saving Termination Certificates Documents.");
                                            if (await UEB_Declaration.SaveJsoninNISDataBase(request, document, (long)document.unemploymentRegularEmployeeClaimFormId))
                                            {
                                                AddTreeViewLogLevel2("Json Succesfully Saved.", true);
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.unemploymentRegularEmployeedeclarationFormId, document.CompletedTime);
                                        }
                                        try
                                        {
                                            AddTreeViewLogLevel2Info("Saving DECLARATION Documents.");
                                            int savedAtt = await UEB_Declaration.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.unemploymentRegularEmployeedeclarationFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "UEB_DECLARATION", document.ClaimNumber.ToString(), long.Parse(document.nisNo), document.firstName + document.surname, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting DECLARATION Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.unemploymentRegularEmployeedeclarationFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        //se estan salvando en la misma tabla de employees, pero con el formid en null, no es possible mappearla.
        private async Task UEB_DeclarationsSEPPendingProcessing()
        {
            try
            {
                AddTreeViewLogLevel0("Unemployment DECLARATIONS SEP");
                AddTreeViewLogLevel1Info("Getting UEB DECLARATIONS SEP Pending  Processing.");
                try
                {
                    var requests = await UEB_Declaration_SEP.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " DECLARATIONS SEP Pending Processing Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_UEB_Declaration();
                            AddTreeViewLogLevel1Info("Getting DECLARATIONS SEP Details");
                            try
                            {
                                document = await UEB_Declaration_SEP.ClaimDetail(request);
                                if (document != null)
                                {
                                    var claimMapping = await UEB_Declaration_SEP.GetClaimRequestMappingByRequestId((long)document.UnemploymentSelfEmployedClaimFormId);

                                    AddTreeViewLogLevel1("DECLARATIONS SEP details successfully loaded", true);
                                    if (claimMapping != null)
                                    {
                                        document.ClaimNumber = int.Parse(claimMapping.ClaimNumber.ToString());
                                    }
                                    else
                                    {
                                        document.ClaimNumber = 0;
                                    }

                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error getting DECLARATIONS SEP mapping.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("DECLARATION for claim number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state
                                        var responseA = await UEB_Declaration_SEP.UpdateWorkFlowState(3, request.supportRequestId, 304);
                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to Posted", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to Posted. " + responseA.Message, false);
                                        }
                                        try
                                        {
                                            AddTreeViewLogLevel2Info("Saving DECLARATIONS SEP Documents.");
                                            if (await UEB_Declaration_SEP.SaveJsoninNISDataBase(request, document, (long)document.UnemploymentSelfEmployedClaimFormId))
                                            {
                                                AddTreeViewLogLevel2("Json Succesfully Saved.", true);
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.UnemploymentSelfEmployedClaimFormId, document.CompletedTime);
                                        }
                                        try
                                        {
                                            AddTreeViewLogLevel2Info("Saving DECLARATION Documents.");
                                            int savedAtt = await UEB_Declaration_SEP.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.UnemploymentSelfEmployedClaimFormId, document.CompletedTime);
                                        }
                                        //save log send email
                                        await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "UEB_DECLARATION", document.ClaimNumber.ToString(), long.Parse(document.nisNo), document.firstName + document.surname, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting DECLARATION Details.", false);
                                }

                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.UnemploymentSelfEmployedClaimFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async Task UEB_TerminationCertificatePendingProcessing()
        {
            try
            {
                AddTreeViewLogLevel0("Termination Certificates");
                AddTreeViewLogLevel1Info("Getting Termination Certificates Pending  Processing.");
                try
                {
                    var requests = await UEB_TerminationCert.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Termination Certificates Pending Processing Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_TerminationCertificate();
                            AddTreeViewLogLevel1Info("Getting Termination Certificates Details");
                            try
                            {
                                document = await UEB_TerminationCert.ClaimDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Termination Certificates details successfully loaded", true);

                                    document.ClaimNumber = 5;// await as400UnemploymentSEPBenefit.InsertUnemployment(document, null);

                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting Termination Certificates to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Termination Certificates with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                        //updating worflow state

                                        //var responseA = await UEB_SEPBenefit.UpdateWorkFlowState(3, request.supportRequestId, 295);
                                        var responseA = await UEB_SEPBenefit.UpdateWorkFlowState(3, request.supportRequestId, 307);
                                        if (responseA.IsSuccess)
                                        {
                                            AddTreeViewLogLevel1("WorkFlow updated to Processing", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel1("Error updating WorkFlow to Processing. " + responseA.Message, false);
                                        }
                                        try
                                        {
                                            AddTreeViewLogLevel2Info("Saving Termination Certificates Documents.");
                                            if (await UEB_TerminationCert.SaveJsoninNISDataBase(request, document))
                                            {
                                                AddTreeViewLogLevel2("Json Succesfully Saved.", true);
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.certificateTerminationLayoffFormId, document.CompletedTime);
                                        }
                                        try
                                        {
                                            AddTreeViewLogLevel2Info("Saving Claim Documents.");
                                            int savedAtt = await UEB_TerminationCert.RequestAttachmentToScannedDocuments(request, document);
                                            AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                        }
                                        catch (Exception ex)
                                        {
                                            Crashes.TrackError(ex);
                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
                                            await LogsHelper.SaveErrorLOG(ex.Message, request, document.certificateTerminationLayoffFormId, document.CompletedTime);
                                        }

                                        //save log send email
                                        //await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "UEB_Termination Certificates", document.ClaimNumber.ToString(), long.Parse(document.nisNo), document.firstName + document.surname, (DateTime)document.CompletedTime, document.CompletedBy);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Termination Certificates Details.", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.certificateTerminationLayoffFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        #endregion

        #region TreeView
        void AddTreeViewLogLevel0(string text)
        {
            working = true;
            BeginInvoke(new Action(() =>
            {
                if (tViewEvents.Nodes.Count >= 500) tViewEvents.Nodes.Clear();
                tViewEvents.Nodes.Add(new TreeNode(text + " [" + DateTime.Now + "]", 0, 0));
                tViewEvents.ExpandAll();
                var lastNode = tViewEvents.Nodes.Cast<TreeNode>().Last().Nodes.Cast<TreeNode>().Any() ? tViewEvents.Nodes.Cast<TreeNode>().Last().Nodes.Cast<TreeNode>().Last() : tViewEvents.Nodes.Cast<TreeNode>().Last();
                lastNode.EnsureVisible();
            }
            ));
            Application.DoEvents();
            Thread.Sleep(1000);
        }
        void AddTreeViewLogLevel1(string text, bool successful)
        {
            BeginInvoke(new Action(() =>
            {
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
                var lastNode = tViewEvents.Nodes.Cast<TreeNode>().Last().Nodes.Cast<TreeNode>().Any() ? tViewEvents.Nodes.Cast<TreeNode>().Last().Nodes.Cast<TreeNode>().Last() : tViewEvents.Nodes.Cast<TreeNode>().Last();
                lastNode.EnsureVisible();
            }
            ));
            Application.DoEvents();
            Thread.Sleep(1000);
        }
        void AddTreeViewLogLevel1Info(string text)
        {
            BeginInvoke(new Action(() =>
            {
                var firstLevelNode = tViewEvents.Nodes[tViewEvents.Nodes.Count - 1];
                firstLevelNode.Nodes.Add(new TreeNode(text + " [" + DateTime.Now + "]", 4, 4));
                tViewEvents.ExpandAll();
                var lastNode = tViewEvents.Nodes.Cast<TreeNode>().Last().Nodes.Cast<TreeNode>().Any() ? tViewEvents.Nodes.Cast<TreeNode>().Last().Nodes.Cast<TreeNode>().Last() : tViewEvents.Nodes.Cast<TreeNode>().Last();
                lastNode.EnsureVisible();
            }
              ));
            Application.DoEvents();
            Thread.Sleep(1000);
        }
        void AddTreeViewLogLevel2(string text, bool successful)
        {
            BeginInvoke(new Action(() =>
            {
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
                var lastNode = secondLevelNode.Nodes.Cast<TreeNode>().Any() ? secondLevelNode.Nodes.Cast<TreeNode>().Last() : secondLevelNode;
                lastNode.EnsureVisible();
            }
              ));
            Application.DoEvents();
            Thread.Sleep(1000);
        }
        void AddTreeViewLogLevel2Info(string text)
        {
            BeginInvoke(new Action(() =>
            {
                var firstLevelNode = tViewEvents.Nodes[tViewEvents.Nodes.Count - 1];
                var secondLevelNode = firstLevelNode.Nodes[firstLevelNode.Nodes.Count - 1];
                secondLevelNode.Nodes.Add(new TreeNode(text + " [" + DateTime.Now + "]", 4, 4));
                tViewEvents.ExpandAll();
                var lastNode = secondLevelNode.Nodes.Cast<TreeNode>().Any() ? secondLevelNode.Nodes.Cast<TreeNode>().Last() : secondLevelNode;
                lastNode.EnsureVisible();
            }
             ));
            Application.DoEvents();
            Thread.Sleep(1000);
        }
        #endregion

        #region Settings
        private void dtpDFrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var doc = XDocument.Load("As400Backup.xml");
                var time = ((DateTimePicker)sender).Value;
                var value = doc.Descendants().Where(o => o.Name == ((DateTimePicker)sender).Name).First();
                XElement hour = value.Descendants("Hour").FirstOrDefault();
                XElement minutes = value.Descendants("Minutes").FirstOrDefault();
                hour.Value = time.Hour.ToString();
                minutes.Value = time.Minute.ToString();
                doc.Save("As400Backup.xml");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void rjToggleButton3_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var doc = XDocument.Load("As400Backup.xml");
                XElement day = doc.Descendants().Where(o => o.Name == "Days").Descendants().Where(o => o.Name == ((RJToggleButton)sender).Tag.ToString().Split(',')[0]).FirstOrDefault();
                day.Value = ((RJToggleButton)sender).Checked ? "1" : "0";
                doc.Save("As400Backup.xml");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void rjToggleButton8_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var doc = XDocument.Load("As400Backup.xml");
                XElement request = doc.Descendants().Where(o => o.Name == "Requests").Descendants().Where(o => o.Name == ((RJToggleButton)sender).Tag.ToString().Split(',')[0]).FirstOrDefault();
                request.Value = ((RJToggleButton)sender).Checked ? "1" : "0";
                doc.Save("As400Backup.xml");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex == 1)
                {
                    LogsDB logsDB = new LogsDB();
                    dataGridView1.DataSource = await logsDB.GetErrorLogsListAsync();
                }
                if (tabControl1.SelectedIndex == 2)
                {
                    dgv2.DataSource = null;
                    UtilRecurrent.FindAllControlsIterative(tableLayoutPanel8, "DateTimePicker").Cast<DateTimePicker>().Where(o => o.Tag.ToString() == "0").ToList().ForEach(o => o.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
                    UtilRecurrent.FindAllControlsIterative(tableLayoutPanel8, "DateTimePicker").Cast<DateTimePicker>().Where(o => o.Tag.ToString() == "1").ToList().ForEach(o => o.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)));
                }

                if (tabControl1.SelectedIndex == 3)
                {
                    var doc = XDocument.Load("As400Backup.xml");
                    var days = doc.Descendants().Where(o => o.Name == "Days").Descendants();
                    UtilRecurrent.FindAllControlsIterative(tpanelDays, "CheckBox").Cast<CheckBox>().ToList().ForEach(o => o.Checked = days.Where(x => x.Name == o.Tag.ToString().Split(',')[0] && x.Value == "1").Any());

                    UtilRecurrent.FindAllControlsIterative(tableLayoutPanel4, "DateTimePicker").Cast<DateTimePicker>().ToList().ForEach(o => o.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(doc.Descendants().Where(x => x.Name == o.Name).FirstOrDefault().Descendants("Hour").FirstOrDefault().Value.ToString()), int.Parse(doc.Descendants().Where(x => x.Name == o.Name).FirstOrDefault().Descendants("Minutes").FirstOrDefault().Value.ToString()), 00));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (working)
            {
                e.Cancel = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var doc = XDocument.Load("As400Backup.xml");
            var days = doc.Descendants().Where(o => o.Name == "Days").Descendants();
            UtilRecurrent.FindAllControlsIterative(tpanelDays, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(o => o.Checked = days.Where(x => x.Name == o.Tag.ToString().Split(',')[0] && x.Value == "1").Any());

            var requests = doc.Descendants().Where(o => o.Name == "Requests").Descendants();
            UtilRecurrent.FindAllControlsIterative(tRquestPanel, "RJToggleButton").Cast<RJToggleButton>().ToList().ForEach(o => o.Checked = requests.Where(x => x.Name == o.Tag.ToString().Split(',')[0] && x.Value == "1").Any());

            UtilRecurrent.FindAllControlsIterative(tableLayoutPanel4, "DateTimePicker").Cast<DateTimePicker>().ToList().ForEach(o => o.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(doc.Descendants().Where(x => x.Name == o.Name).FirstOrDefault().Descendants("Hour").FirstOrDefault().Value.ToString()), int.Parse(doc.Descendants().Where(x => x.Name == o.Name).FirstOrDefault().Descendants("Minutes").FirstOrDefault().Value.ToString()), 00));
        }

        #region PostedLogs
        private async void rbtnRequest_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {


                if (((RadioButton)sender).Tag.ToString() == "0")
                {
                    cmbRequest.Enabled = true;
                    tpanel0.Enabled = true;
                    cmbClaims.Enabled = false;
                    tpanel1.Enabled = false;
                    tpanel2.Enabled = false;
                    dgv2.DataSource = null;

                    LogsDB logsDB = new LogsDB();
                    dgv2.DataSource = await logsDB.GetEmployeeLogsListAsync();
                }
                if (((RadioButton)sender).Tag.ToString() == "1")
                {
                    cmbRequest.Enabled = false;
                    tpanel0.Enabled = false;
                    cmbClaims.Enabled = true;
                    tpanel1.Enabled = true;
                    tpanel2.Enabled = false;
                    dgv2.DataSource = null;


                }
                if (((RadioButton)sender).Tag.ToString() == "2")
                {
                    cmbRequest.Enabled = false;
                    tpanel0.Enabled = false;
                    cmbClaims.Enabled = false;
                    tpanel1.Enabled = false;
                    tpanel2.Enabled = true;

                    LogsDB logsDB = new LogsDB();
                    dgv2.DataSource = await logsDB.GetRemittanceLogsListAsync();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button17_Click(object sender, EventArgs e)
        {

        }


        #endregion

        public void PlayExclamation()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
            simpleSound.Play();
        }

        private async Task AgeBenefitClaimTesting()
        {
            try
            {
                AddTreeViewLogLevel0("Age Benefit");
                AddTreeViewLogLevel1Info("Getting Age Benefit Claims Completed");
                try
                {
                    var requests = await AgeBenefit.GetPensionToTesting(6, 124);
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Completed Found", true);
                        foreach (var request in requests)
                        {
                            PlayExclamation();
                            if (cancelRequest) return;
                            var document = new Document_AgeBenefit();
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            try
                            {
                                document = await AgeBenefit.ClaimDetail(request);
                                if (document != null)
                                {
                                    AddTreeViewLogLevel1("Claim details successfully loaded", true);

                                    document.ClaimNumber = await as400AgeBenefit.InsertAgePension(document);


                                    if (document.ClaimNumber == 0)
                                    {
                                        AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                    }
                                    else
                                    {
                                        AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                    }
                                    //comentar si no se quieren salvar los documentos nuevamente
                                    try
                                    {
                                        // //posting in testing                                      
                                        int savedAt = await AgeBenefit.RequestAttachmentToScannedDocumentsTest(request, document);
                                        AddTreeViewLogLevel2(savedAt + " Document(s) Succesfully Saved.", true);
                                    }
                                    catch (Exception ex)
                                    {
                                        Crashes.TrackError(ex);
                                        AddTreeViewLogLevel2("Error " + ex.Message, false);
                                        await LogsHelper.SaveErrorLOG(ex.Message, request, document.ageBenefitFormId, document.CompletedTime);
                                    }
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Error Getting Claim Details.", false);
                                }

                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                                AddTreeViewLogLevel2("Error " + ex.Message, false);
                                await LogsHelper.SaveErrorLOG(ex.Message, request, document.ageBenefitFormId, document.CompletedTime);
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
                    await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }


    }
}
