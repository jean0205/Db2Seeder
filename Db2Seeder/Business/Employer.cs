using As400DataAccess;
using Db2Seeder.API.Helpers;
using Db2Seeder.API.Models;
using Db2Seeder.API.Request;
using Db2Seeder.NIS.SQL.Documents.DataAccess;
using Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments;
using Microsoft.AppCenter.Crashes;
using ShareModels.Models;
using ShareModels.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder.Business
{
    public class Employer
    {
        //AS400DATACCESS
        EmployerDB2 as400Empr;

        List<SupportRequest> RequestList;
        Document_Employer Document_Employer;

        //SupportRequest/GetByState/Type/5/State/14
        public async Task GetEmployerCompleted()
        {
            try
            {
                RequestList = new List<SupportRequest>();
                RequestList = await ApiRequest.GetSupportRequestTypeByState(5, 14);
                if (RequestList.Any())
                {
                    foreach (var request in RequestList)
                    {
                        DocumentGuid guid = new DocumentGuid();
                        guid = await ApiRequest.GetRequestGUID(request.supportRequestId);

                        if (guid.message != Guid.Empty)
                        {
                            Document_Employer = new Document_Employer();
                            Document_Employer = await GetRequestDetailsEmployer(guid);
                            
                            //pasar el objeto a as400 para que se registre el employee y recibir el NIS
                            as400Empr = new EmployerDB2();

                            //si inserta debe retornar el numero de employer mayor que 0
                            int employerNumber = await as400Empr.InsertEmployers(Document_Employer);
                            if (employerNumber==0)
                            {
                                return;
                            }
                            List<DocumentGuid> attachmentsGuid = await ApiRequest.GetAttachmentsGuid(request.supportRequestId);
                            if (attachmentsGuid.Any())
                            {
                                List<Document_MetaData> document_MetaDataList = new List<Document_MetaData>();
                                document_MetaDataList = await ApiRequest.GetDocument_MetaData(attachmentsGuid);
                                if (document_MetaDataList.Any())
                                {
                                    List<RequestHistory> requestHistory = new List<RequestHistory>();
                                    requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", request.supportRequestId);
                                    ImportLog importLog = new ImportLog
                                    {
                                        ImportedBy = requestHistory.Last().modifiedBy.ToUpper(),
                                        ImportDatetime = DateTime.Now
                                    };
                                    ScannedDocumentsDB scannedDocumentsDB = new ScannedDocumentsDB();
                                    int importId = 0;
                                    importId = await scannedDocumentsDB.InsertImportLog(importLog);
                                    //buscar el pdf Data
                                    foreach (var item in document_MetaDataList)
                                    {
                                        //TODO                                       
                                        //poner el document type correcto una vez que venga del portal web
                                        Documents documents = new Documents();
                                        documents.ActiveCode = "A";
                                        documents.RegistrantTypeId = 2;
                                        documents.DocTypeId = item.code;
                                        documents.ImportId = importId;
                                        documents.NisNumber = employerNumber;
                                        documents.PdfData = await ApiRequest.GetDocument_Data(item.documentImageGuid);
                                        documents.ScannedBy = importLog.ImportedBy;
                                        documents.ScanDatetime = DateTime.Now;
                                        documents.ModifiedDatetime = DateTime.Now;
                                        await scannedDocumentsDB.InsertDocumentforRegistration(documents);
                                    }

                                }
                            }

                            //addNisMapping
                            NisMapping nisMapping = new NisMapping();
                            nisMapping.nisNumberTypeId = 2;
                            nisMapping.nisNumber = employerNumber.ToString();
                            nisMapping.userAccountId = request.ownerId;
                            if (await AddNisMapping(nisMapping))
                            {
                                //se realizo con exito
                                //assigno el rol de employee
                                AssignRoleToUserAccount roleToUserAccount = new AssignRoleToUserAccount();
                                roleToUserAccount.userAccountId = request.ownerId;
                                roleToUserAccount.roleId = 6;
                                roleToUserAccount.createdBy = 7083;
                                if (await AssignRoleToUserAccount(roleToUserAccount))
                                {
                                    //se realizo con xito
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //Document/Get?id=b5a1b323-3adf-4917-b77a-c6fda5f1be5f
        public static async Task<Document_Employer> GetRequestDetailsEmployer(DocumentGuid guid)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByGuid<Document_Employer>("Document/Get?id", guid.message);

                if (!response.IsSuccess)
                {
                    return null;
                }
                return (Document_Employer)response.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }
        private async Task<bool> AddNisMapping(NisMapping nisMapping)
        {
            try
            {
                Response response = await ApiServices.PostAsync("Account/AddNisMapping", nisMapping);

                if (!response.IsSuccess)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
        }
        private async Task<bool> AssignRoleToUserAccount(AssignRoleToUserAccount roleToUserAccount)
        {
            try
            {
                Response response = await ApiServices.PostAsync("Account/AssignRoleToUserAccount", roleToUserAccount);

                if (!response.IsSuccess)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
        }
    }
}
