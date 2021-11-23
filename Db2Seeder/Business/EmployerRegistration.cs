using Db2Seeder.API.Helpers;
using Db2Seeder.API.Models;
using Db2Seeder.API.Request;
using Db2Seeder.NIS.SQL.Documents.DataAccess;
using Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments;
using ShareModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder.Business
{
    public class EmployerRegistration
    {
        public static async Task<List<SupportRequest>> GetEmployerRegistrationSupportRequestCompleted()
        {
            try
            {
                List<SupportRequest> RequestList = new List<SupportRequest>();
                return RequestList = await ApiRequest.GetSupportRequestTypeByState(5, 14);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_Employer> EmployerRegistrationRequestDetail(SupportRequest Request)
        {
            try
            {
                DocumentGuid guid = new DocumentGuid();
                guid = await ApiRequest.GetRequestGUID(Request.supportRequestId);
                if (guid.message != Guid.Empty)
                {
                    Document_Employer Document_Employer = new Document_Employer();
                    return Document_Employer = await GetRequestDetailsEmployer(guid);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<int> RequestAttachmentToScannedDocuments(SupportRequest Request, Document_Employer Document_Employer)
        {
            try
            {
                var cant = 0;
                List<DocumentGuid> attachmentsGuid = await ApiRequest.GetAttachmentsGuid(Request.supportRequestId);
                if (attachmentsGuid.Any())
                {
                    List<Document_MetaData> document_MetaDataList = new List<Document_MetaData>();
                    document_MetaDataList = await ApiRequest.GetDocument_MetaData(attachmentsGuid);
                    if (document_MetaDataList.Any())
                    {
                        List<RequestHistory> requestHistory = new List<RequestHistory>();
                        requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", Request.supportRequestId);
                        ImportLog importLog = new ImportLog
                        {
                            ImportedBy = requestHistory.Last().UserName,
                            ImportDatetime = DateTime.Now
                        };
                        ScannedDocumentsDB scannedDocumentsDB = new ScannedDocumentsDB();
                        int importId = 0;
                        importId = await scannedDocumentsDB.InsertImportLog(importLog);
                        foreach (var item in document_MetaDataList)
                        {
                            Documents documents = new Documents();
                            documents.ActiveCode = "A";
                            documents.RegistrantTypeId = 2;
                            documents.DocTypeId = item.code;
                            documents.ImportId = importId;
                            documents.NisNumber = (int)Document_Employer.employerNo;
                            documents.PdfData = await ApiRequest.GetDocument_Data(item.documentImageGuid, item.fileType);
                            documents.ScannedBy = importLog.ImportedBy;
                            documents.ScanDatetime = DateTime.Now;
                            documents.ModifiedDatetime = DateTime.Now;
                            await scannedDocumentsDB.InsertDocumentforRegistration(documents);
                            cant++;
                        }
                        return cant;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Response> AddNISMapping(SupportRequest Request, Document_Employer Document_Employer)
        {
            try
            {
                NisMapping nisMapping = new NisMapping();
                nisMapping.nisNumberTypeId = 1;
                nisMapping.nisNumber = Document_Employer.employerNo.ToString();
                nisMapping.userAccountId = Request.ownerId;
                return await AddNisMapping(nisMapping);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<bool> AddEmployerRole(SupportRequest Request)
        {
            try
            {
                AssignRoleToUserAccount roleToUserAccount = new AssignRoleToUserAccount();
                roleToUserAccount.userAccountId = Request.ownerId;
                roleToUserAccount.roleId = 6;
                roleToUserAccount.createdBy = 7083;
                return await AssignRoleToUserAccount(roleToUserAccount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
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
        private static async Task<Response> AddNisMapping(NisMapping nisMapping)
        {
            try
            {
                Response response = await ApiServices.PostAsync("Account/AddNisMapping", nisMapping);
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static async Task<bool> AssignRoleToUserAccount(AssignRoleToUserAccount roleToUserAccount)
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
                throw ex;
            }
        }
        public static async Task<Response> UpdateWorkFlowStateEmployee(int userId, int requestId, int actionId)
        {
            try
            {
                Response response = await ApiRequest.UpdateWorkFlowState(userId, requestId, actionId);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
