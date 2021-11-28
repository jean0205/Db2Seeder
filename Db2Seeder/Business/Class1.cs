//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Db2Seeder.Business
//{
//    internal class Class1
//    {

//        private async Task EmployeeRegistrationRequest()
//        {
//            AddTreeViewLogLevel0("Employee Registration Requests");
//            AddTreeViewLogLevel1Info("Getting Employee Requests Completed");
//            try
//            {
//                var requests = await EmployeeRegistration.GetEmployeeRegistrationSupportRequestCompleted();
//                if (requests.Any())
//                {
//                    AddTreeViewLogLevel1(requests.Count + " Requests Completed Found", true);
//                    foreach (var request in requests)
//                    {
//                        var document = new Document_Employee();
//                        AddTreeViewLogLevel1Info("Getting Employee Details");
//                        try
//                        {
//                            document = await EmployeeRegistration.EmployeeRegistrationRequestDetail(request);
//                            if (document != null)
//                            {
//                                AddTreeViewLogLevel1("Employee details successfully loaded", true);
//                                document.nisNo = await as400Empe.InsertEmployees(document);
//                                if (document.nisNo == 0)
//                                {
//                                    AddTreeViewLogLevel1("Error inserting employee to the  DB2 database.", false);
//                                }
//                                else
//                                {
//                                    AddTreeViewLogLevel1("Employee with NIS number: " + document.nisNo + " successfully saved to the DB2 database", true);
//                                    //updating worflow state
//                                    //var responseA = await EmployeeRegistration.UpdateWorkFlowStateEmployee(7083, request.supportRequestId, 161);
//                                    //if (responseA.IsSuccess)
//                                    //{
//                                    //    AddTreeViewLogLevel1("WorkFlow updated to DB2 Posted", true);
//                                    //}
//                                    //else
//                                    //{
//                                    //    AddTreeViewLogLevel1("Error updating WorkFlow to DB2 Posted. " + responseA.Message, false);
//                                    //}
//                                    try
//                                    {
//                                        AddTreeViewLogLevel2Info("Saving Employee Documents.");
//                                        int savedAtt = await EmployeeRegistration.RequestAttachmentToScannedDocuments(request, document);
//                                        AddTreeViewLogLevel2(savedAtt + " Document(s) Succesfully Saved.", true);
//                                    }
//                                    catch (Exception ex)
//                                    {
//                                        Crashes.TrackError(ex);
//                                        AddTreeViewLogLevel2("Error " + ex.Message, false);
//                                        SaveLOG(ex.Message, request, document.employeeRegistrationFormId, document.CompletedTime);
//                                    }
//                                    //si no es employer y no tiene  employee link entonces hago el link automatico
//                                    if (!await ApiRequest.IsEmployer(request.ownerId) || !await ApiRequest.IsEmployee(request.ownerId))
//                                    {
//                                        try
//                                        {
//                                            AddTreeViewLogLevel2Info("Mapping NIS Number to Web Portal Account.");
//                                            var response = await EmployeeRegistration.AddNISMapping(request, document);
//                                            if (response.IsSuccess)
//                                            {
//                                                AddTreeViewLogLevel2(document.nisNo + " Successfully Mapped.", true);
//                                            }
//                                            else
//                                            {
//                                                AddTreeViewLogLevel2("Error mapping NIS number: " + document.nisNo + " " + response.Message, false);
//                                            }
//                                        }
//                                        catch (Exception ex)
//                                        {
//                                            Crashes.TrackError(ex);
//                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
//                                            SaveLOG(ex.Message, request, document.employeeRegistrationFormId, document.CompletedTime);
//                                        }
//                                        try
//                                        {

//                                            AddTreeViewLogLevel2Info("Assigning Employee Rol.");
//                                            if (await EmployeeRegistration.AddEmployeeRole(request))
//                                            {
//                                                AddTreeViewLogLevel2("Employee Role successufully added", true);
//                                            }
//                                            else
//                                            {
//                                                AddTreeViewLogLevel2("Error Adding Employee Role.", false);
//                                            }
//                                        }
//                                        catch (Exception ex)
//                                        {
//                                            Crashes.TrackError(ex);
//                                            AddTreeViewLogLevel2("Error " + ex.Message, false);
//                                            SaveLOG(ex.Message, request, document.employeeRegistrationFormId, document.CompletedTime);
//                                        }
//                                    }
//                                }
//                            }
//                            else
//                            {
//                                AddTreeViewLogLevel1("Error Getting Employee Details.", false);
//                            }
//                        }
//                        catch (Exception ex)
//                        {
//                            Crashes.TrackError(ex);
//                            AddTreeViewLogLevel1("Error " + ex.Message, false);
//                            SaveLOG(ex.Message, request, document.employeeRegistrationFormId, document.CompletedTime);
//                        }
//                        SaveLOG(String.Empty, request, document.employeeRegistrationFormId, document.CompletedTime);
//                    }
//                }
//                else
//                {
//                    AddTreeViewLogLevel1Info("No Completed Requests were Found.");
//                }
//            }
//            catch (Exception ex)
//            {
//                Crashes.TrackError(ex);
//                AddTreeViewLogLevel1("Error " + ex.Message, false);
//                SaveLOG(ex.Message, null, null, null);
//            }
//        }
//    }
//}
