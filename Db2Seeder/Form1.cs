using As400DataAccess;
using Db2Seeder.API.Request;
using Db2Seeder.Business;
using Db2Seeder.Business.Benefit_Claims;
using Microsoft.AppCenter.Crashes;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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


        public Form1()
        {
            InitializeComponent();
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
            await AgeBenefitClaimCompleted();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            // await Remittance.GetRemittancePendingReview();
            await GetRemittancePendingReview();
        }
        private async void button7_Click(object sender, EventArgs e)
        {
            await DeathBenefitClaimCompleted();
        }
        private async void button8_Click(object sender, EventArgs e)
        {
            await FuneralBenefitClaimCompleted();
        }
        private async void button6_Click(object sender, EventArgs e)
        {
            await InvalidityBenefitClaimCompleted();
        }
        private async  void button9_Click(object sender, EventArgs e)
        {
            await SicknessBenefitClaimCompleted();
        }
        private async void button10_Click(object sender, EventArgs e)
        {
            await SurvivorBenefitClaimCompleted();
        }
        private async void button11_Click(object sender, EventArgs e)
        {
            await DisablemetBenefitClaimCompleted();
        }
        private async void button12_Click(object sender, EventArgs e)
        {
            await MaternityBenefitClaimCompleted();
        }
        private async void button13_Click(object sender, EventArgs e)
        {
            await EmploymentInjuryBenefitClaimCompleted();
        }

        #region Employee-Employer
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
                            var document = await EmployeeRegistration.EmployeeRegistrationRequestDetail(request);
                            if (document != null)
                            {
                                AddTreeViewLogLevel1("Employee details successfully loaded", true);
                                document.nisNo = await as400Empe.InsertEmployees(document);
                                if (document.nisNo == 0)
                                {
                                    AddTreeViewLogLevel1("Error inserting employee to the  DB2 database.", false);
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Employee with NIS number: " + document.nisNo + " successfully saved to the DB2 database", true);
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
                                        int savedAtt = await EmployeeRegistration.RequestAttachmentToScannedDocuments(request, document);
                                        AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
                                    }
                                    catch (Exception ex)
                                    {
                                        Crashes.TrackError(ex);
                                        AddTreeViewLogLevel2("Error " + ex.Message, false);
                                    }
                                    //si no es employer y no tiene  employee link entonces hago el link automatico
                                    if (!await ApiRequest.IsEmployer(request.ownerId) || !await ApiRequest.IsEmployee(request.ownerId))
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
                            var document = await EmployerRegistration.EmployerRegistrationRequestDetail(request);
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
                                        int savedAtt = await EmployerRegistration.RequestAttachmentToScannedDocuments(request, document);
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
                                        var response = await EmployerRegistration.AddNISMapping(request, document);
                                        if (response.IsSuccess)
                                        {
                                            AddTreeViewLogLevel2(document.employerNo + " Successfully Mapped.", true);
                                        }
                                        else
                                        {
                                            AddTreeViewLogLevel2("Error mapping NIS number: " + document.employerNo + " " + response.Message, false);
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

        #endregion
        #region Compliance Certificate
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
                            var document = await ComplianceCertificate.ComplianceCertRequestDetail(request);
                            if (document != null)
                            {
                                AddTreeViewLogLevel1("Request details successfully loaded", true);
                                AddTreeViewLogLevel1Info("Inserting Compliance Certificate in SQL.");

                                if (await ComplianceCertificate.InsertComplianceCertificateSQL(request, document))
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
        #endregion

        #region Remittance
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
                            AddTreeViewLogLevel1Info("Getting Remittance Pending Details");
                            var document = await Remittance.RemittanceDetail(request);
                            if (document != null)
                            {
                                AddTreeViewLogLevel1("Remittance details successfully loaded", true);
                                try
                                {
                                    AddTreeViewLogLevel2Info("Posting Remittance to As400.");
                                    await Remittance.PostRemittanceToAs400(request, document);
                                    AddTreeViewLogLevel2("Remittance Succesfully Posted.", true);
                                }
                                catch (Exception ex)
                                {
                                    Crashes.TrackError(ex);
                                    AddTreeViewLogLevel2("Error " + ex.Message, false);
                                }
                                //var responseA = await EmployerRegistration.UpdateWorkFlowStateEmployee(7083, request.supportRequestId, 171);
                                //if (responseA.IsSuccess)
                                //{
                                //    AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                //}
                                //else
                                //{
                                //    AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                //}
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
        #endregion

        #region Benefit Claims
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
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            var document = await AgeBenefit.ClaimDetail(request);
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
                                    int savedAtt = await AgeBenefit.RequestAttachmentToScannedDocuments(request, document);
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
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            var document = await DeathBenefit.ClaimDetail(request);
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
                                    // var responseA = await DeathBenefit.UpdateWorkFlowState(7083, request.supportRequestId, 164);
                                    //if (responseA.IsSuccess)
                                    //{
                                    //    AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                    //}
                                    //else
                                    //{
                                    //    AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                    //}
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
                                    }
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
        private async Task FuneralBenefitClaimCompleted()
        {
            try
            {
                AddTreeViewLogLevel0("Funeral Benefit");
                AddTreeViewLogLevel1Info("Getting Funeral Benefit Claims Completed");
                try
                {
                    var requests = await FuneralBenefit.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Completed Found", true);
                        foreach (var request in requests)
                        {
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            var document = await FuneralBenefit.ClaimDetail(request);
                            if (document != null)
                            {
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
                                    // var responseA = await FuneralBenefit.UpdateWorkFlowState(7083, request.supportRequestId, 165);
                                    //if (responseA.IsSuccess)
                                    //{
                                    //    AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                    //}
                                    //else
                                    //{
                                    //    AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                    //}
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
                                    }
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
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            var document = await InvalidityBenefit.ClaimDetail(request);
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
                                    var responseA = await InvalidityBenefit.UpdateWorkFlowState(7083, request.supportRequestId, 166);
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
                                    }
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

        private async Task SicknessBenefitClaimCompleted()
        {
            try
            {
                AddTreeViewLogLevel0("Sickness Benefit");
                AddTreeViewLogLevel1Info("Getting Sickness Benefit Claims Completed");
                try
                {
                    var requests = await SicknessBenefit.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Completed Found", true);
                        foreach (var request in requests)
                        {
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            var document = await SicknessBenefit.ClaimDetail(request);
                            if (document != null)
                            {
                                AddTreeViewLogLevel1("Claim details successfully loaded", true);
                                document.ClaimNumber = await as400sicknessBenefit.InsertSickness(document);
                                if (document.ClaimNumber == 0)
                                {
                                    AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                    //updating worflow state
                                    //var responseA = await SicknessBenefit.UpdateWorkFlowState(7083, request.supportRequestId, 169);
                                    //if (responseA.IsSuccess)
                                    //{
                                    //    AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                    //}
                                    //else
                                    //{
                                    //    AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                    //}
                                    try
                                    {
                                        AddTreeViewLogLevel2Info("Saving Employee Documents.");
                                        int savedAtt = await SicknessBenefit.RequestAttachmentToScannedDocuments(request, document);
                                        AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
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
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            var document = await SurvivorBenefit.ClaimDetail(request);
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
                                    //updating worflow state
                                    //var responseA = await InvalidityBenefit.UpdateWorkFlowState(7083, request.supportRequestId, 159);
                                    //if (responseA.IsSuccess)
                                    //{
                                    //    AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                    //}
                                    //else
                                    //{
                                    //    AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                    //}
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
                                    }
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
                        AddTreeViewLogLevel1(requests.Count + " Claims Completed Found", true);
                        foreach (var request in requests)
                        {
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            var document = await DisablementBenefit.ClaimDetail(request);
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
                                    //var responseA = await DisablementBenefit.UpdateWorkFlowState(7083, request.supportRequestId, 168);
                                    //if (responseA.IsSuccess)
                                    //{
                                    //    AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                    //}
                                    //else
                                    //{
                                    //    AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                    //}
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
                                    }
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
        private async Task MaternityBenefitClaimCompleted()
        {
            try
            {
                AddTreeViewLogLevel0("Maternity Benefit");
                AddTreeViewLogLevel1Info("Getting Maternity Benefit Claims Completed");
                try
                {
                    var requests = await MaternityBenefit.GetClaimsCompleted();
                    if (requests.Any())
                    {
                        AddTreeViewLogLevel1(requests.Count + " Claims Completed Found", true);
                        foreach (var request in requests)
                        {
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            var document = await MaternityBenefit.ClaimDetail(request);
                            if (document != null)
                            {
                                AddTreeViewLogLevel1("Claim details successfully loaded", true);
                                document.ClaimNumber = await as400maternityBenefit.InsertMaternity(document);
                                if (document.ClaimNumber == 0)
                                {
                                    AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                    //updating worflow state
                                    //var responseA = await MaternityBenefit.UpdateWorkFlowState(7083, request.supportRequestId, 167);
                                    //if (responseA.IsSuccess)
                                    //{
                                    //    AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                    //}
                                    //else
                                    //{
                                    //    AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                    //}
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
                                    }
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
                            AddTreeViewLogLevel1Info("Getting Claim Details");
                            var document = await EmployInjuryBenefit.ClaimDetail(request);
                            if (document != null)
                            {
                                AddTreeViewLogLevel1("Claim details successfully loaded", true);
                                //document.ClaimNumber = await as400DisablementBenefit.InsertEmpInjDisable(document);
                                if (document.ClaimNumber == 0)
                                {
                                    AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
                                }
                                else
                                {
                                    AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
                                    //updating worflow state
                                    //var responseA = await EmployInjuryBenefit.UpdateWorkFlowState(7083, request.supportRequestId, 172);
                                    //if (responseA.IsSuccess)
                                    //{
                                    //    AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
                                    //}
                                    //else
                                    //{
                                    //    AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
                                    //}
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
                                    }
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

        #endregion






        void AddTreeViewLogLevel0(string text)
        {
            //Thread.Sleep(2000);
            //Application.DoEvents();
            tViewEvents.Nodes.Add(new TreeNode(text + " [" + DateTime.Now + "]", 0, 0));
            tViewEvents.ExpandAll();
            tViewEvents.SelectedNode = tViewEvents.Nodes[tViewEvents.Nodes.Count - 1];
        }
        void AddTreeViewLogLevel1(string text, bool successful)
        {
            //Thread.Sleep(2000);
            //Application.DoEvents();
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
            //Thread.Sleep(2000);
            //Application.DoEvents();
            var firstLevelNode = tViewEvents.Nodes[tViewEvents.Nodes.Count - 1];
            firstLevelNode.Nodes.Add(new TreeNode(text + " [" + DateTime.Now + "]", 4, 4));
            tViewEvents.ExpandAll();
            tViewEvents.SelectedNode = firstLevelNode;
        }
        void AddTreeViewLogLevel2(string text, bool successful)
        {
            //Thread.Sleep(2000);
            //Application.DoEvents();
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
            // Thread.Sleep(2000);
            //Application.DoEvents();
            var firstLevelNode = tViewEvents.Nodes[tViewEvents.Nodes.Count - 1];
            var secondLevelNode = firstLevelNode.Nodes[firstLevelNode.Nodes.Count - 1];
            secondLevelNode.Nodes.Add(new TreeNode(text + " [" + DateTime.Now + "]", 4, 4));
            tViewEvents.ExpandAll();
            tViewEvents.SelectedNode = secondLevelNode;
        }

       
    }
}
