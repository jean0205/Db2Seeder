using Db2Seeder.Business.Benefit_Claims;
using Db2Seeder.SQL.Logs.Helpers;
using Microsoft.AppCenter.Crashes;
using ShareModels.Models.Benefit_Claims;
using ShareModels.Models.Sickness_Claim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder
{
    internal class testcode
    {
        //private async Task SicknessBenefitClaimCompleted()
        //{
        //    try
        //    {
        //        AddTreeViewLogLevel0("Sickness Benefit");
        //        AddTreeViewLogLevel1Info("Getting Sickness Benefit Claims Ready For Processing");
        //        try
        //        {
        //            var requests = await SicknessBenefit.GetClaimsCompleted();
        //            if (requests.Any())
        //            {
        //                AddTreeViewLogLevel1(requests.Count + " Claims Ready For Processing Found", true);
        //                foreach (var request in requests)
        //                {
        //                    PlayExclamation();
        //                    if (cancelRequest) return;
        //                    var document = new Document_Sickness();
        //                    AddTreeViewLogLevel1Info("Getting Claim Details");
        //                    try
        //                    {
        //                        document = await SicknessBenefit.ClaimDetail(request);
        //                        if (document != null)
        //                        {
        //                            AddTreeViewLogLevel1("Claim details successfully loaded", true);

        //                            as400sicknessBenefit.As400_lib = "NI";
        //                            document.ClaimNumber = await as400sicknessBenefit.InsertSickness(document, null);
        //                            if (document.ClaimNumber == 0)
        //                            {
        //                                AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
        //                            }
        //                            else
        //                            {
        //                                AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
        //                                //updating worflow state
        //                                var responseA = await SicknessBenefit.UpdateWorkFlowState(3, request.supportRequestId, 242);
        //                                if (responseA.IsSuccess)
        //                                {
        //                                    AddTreeViewLogLevel1("WorkFlow updated to Processing", true);
        //                                }
        //                                else
        //                                {
        //                                    AddTreeViewLogLevel1("Error updating WorkFlow to Processing " + responseA.Message, false);
        //                                }
        //                                try
        //                                {
        //                                    AddTreeViewLogLevel2Info("Saving  Documents.");

        //                                    int savedAtt = await SicknessBenefit.RequestAttachmentToScannedDocuments(request, document);



        //                                    //posting in testing
        //                                    as400sicknessBenefit.As400_lib = "TT";

        //                                    await as400sicknessBenefit.InsertSickness(document, document.ClaimNumber);
        //                                    int savedAt = await SicknessBenefit.RequestAttachmentToScannedDocumentsTest(request, document);

        //                                    AddTreeViewLogLevel2(savedAt + " Document(s) Succesfully Saved.", true);
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    Crashes.TrackError(ex);
        //                                    AddTreeViewLogLevel2("Error " + ex.Message, false);
        //                                    await LogsHelper.SaveErrorLOG(ex.Message, request, document.sicknessBenefitFormId, document.CompletedTime);
        //                                }
        //                                //save log send email
        //                                await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "SICKNESS", document.employerNis, document.nisNo, document.firstName + document.otherName, (DateTime)document.CompletedTime, document.CompletedBy);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            AddTreeViewLogLevel1("Error Getting Claim Details.", false);
        //                        }

        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Crashes.TrackError(ex);
        //                        AddTreeViewLogLevel2("Error " + ex.Message, false);
        //                        await LogsHelper.SaveErrorLOG(ex.Message, request, document.sicknessBenefitFormId, document.CompletedTime);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                AddTreeViewLogLevel1Info("No Completed Claims were Found.");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Crashes.TrackError(ex);
        //            AddTreeViewLogLevel1("Error " + ex.Message, false);
        //            await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Crashes.TrackError(ex);
        //    }
        //}

        //private async Task UEB_EmployeesClaimPendingProcessing()
        //{
        //    try
        //    {
        //        AddTreeViewLogLevel0("Unemployment Benefit EMPLOYEES");
        //        AddTreeViewLogLevel1Info("Getting UEB EMPLOYEES Benefit Claims Pending  Processing.");
        //        try
        //        {
        //            var requests = await UEB_EmpeBenefit.GetClaimsCompleted();
        //            if (requests.Any())
        //            {
        //                AddTreeViewLogLevel1(requests.Count + " Claims Pending Processing Found", true);
        //                foreach (var request in requests)
        //                {
        //                    PlayExclamation();
        //                    if (cancelRequest) return;
        //                    var document = new Document_UEB_Empe();
        //                    AddTreeViewLogLevel1Info("Getting Claim Details");
        //                    try
        //                    {
        //                        document = await UEB_EmpeBenefit.ClaimDetail(request);
        //                        if (document != null)
        //                        {
        //                            AddTreeViewLogLevel1("Claim details successfully loaded", true);

        //                            as400UnemploymentBenefit.As400_lib = "NI";
        //                            document.ClaimNumber = await as400UnemploymentBenefit.InsertUnemployment(document, null);

        //                            await UEB_EmpeBenefit.SaveRequestClaimMapping(request.supportRequestId, (int)document.ClaimNumber);
        //                            //salvar el mapping para el termination certificate tambien
        //                            if (document.certificateTerminationLayoffFormSupportRequestId != null)
        //                            {
        //                                await UEB_EmpeBenefit.UpdateTerminationCertificateLinkedClaim((long)document.certificateTerminationLayoffFormSupportRequestId, request.supportRequestId, (int)document.ClaimNumber);
        //                            }
        //                            if (document.ClaimNumber == 0)
        //                            {
        //                                AddTreeViewLogLevel1("Error inserting claim to the  DB2 database.", false);
        //                            }
        //                            else
        //                            {
        //                                AddTreeViewLogLevel1("Claim with number: " + document.ClaimNumber + " successfully saved to the DB2 database.", true);
        //                                //updating worflow state
        //                                var responseA = await UEB_EmpeBenefit.UpdateWorkFlowState(3, request.supportRequestId, 287);
        //                                if (responseA.IsSuccess)
        //                                {
        //                                    AddTreeViewLogLevel1("WorkFlow updated to Processing", true);
        //                                }
        //                                else
        //                                {
        //                                    AddTreeViewLogLevel1("Error updating WorkFlow to Processing. " + responseA.Message, false);
        //                                }
        //                                try
        //                                {
        //                                    AddTreeViewLogLevel2Info("Saving Claim Documents.");
        //                                    int savedAtt = await UEB_EmpeBenefit.RequestAttachmentToScannedDocuments(request, document);
        //                                    AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);


        //                                    //saving in testing
        //                                    as400UnemploymentBenefit.As400_lib = "TT";
        //                                    await as400UnemploymentBenefit.InsertUnemployment(document, document.ClaimNumber);
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    Crashes.TrackError(ex);
        //                                    AddTreeViewLogLevel2("Error " + ex.Message, false);
        //                                    await LogsHelper.SaveErrorLOG(ex.Message, request, document.unemploymentRegularEmployeeClaimFormId, document.CompletedTime);
        //                                }
        //                                //save log send email
        //                                await LogsHelper.SaveClaimLOG(request, (int)document.ClaimNumber, "UEB_Employees", document.ClaimNumber.ToString(), long.Parse(document.nisNo), document.firstName + document.surname, (DateTime)document.CompletedTime, document.CompletedBy);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            AddTreeViewLogLevel1("Error Getting Claim Details.", false);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Crashes.TrackError(ex);
        //                        AddTreeViewLogLevel2("Error " + ex.Message, false);
        //                        await LogsHelper.SaveErrorLOG(ex.Message, request, document.unemploymentRegularEmployeeClaimFormId, document.CompletedTime);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                AddTreeViewLogLevel1Info("No Completed Claims were Found.");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Crashes.TrackError(ex);
        //            AddTreeViewLogLevel1("Error " + ex.Message, false);
        //            await LogsHelper.SaveErrorLOG(ex.Message, null, null, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Crashes.TrackError(ex);
        //    }
        //}
    }
}
