using Db2Seeder.API.Helpers;
using Db2Seeder.API.Models;
using Db2Seeder.API.Request;
using Db2Seeder.NIS.SQL.Documents.DataAccess;
using Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments;
using ShareModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db2Seeder.Business
{
    public class EmployeeRegistration
    {
        public static async Task<List<SupportRequest>> GetEmployeeRegistrationSupportRequestCompleted()
        {
            try
            {
                List<SupportRequest> RequestList = new List<SupportRequest>();
                return RequestList = await ApiRequest.GetSupportRequestTypeByState(3, 8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_Employee> EmployeeRegistrationRequestDetail(SupportRequest Request)
        {
            try
            {
                DocumentGuid guid = new DocumentGuid();
                guid = await ApiRequest.GetRequestGUID(Request.supportRequestId);
                if (guid.message != Guid.Empty)
                {
                    List<RequestHistory> requestHistory = new List<RequestHistory>();
                    requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", Request.supportRequestId);

                    Document_Employee Document_Employee = new Document_Employee();
                    Document_Employee = await GetRequestDetailsEmployee(guid);
                    validatePhone(Document_Employee);
                    
                    Document_Employee.CompletedBy = requestHistory.Last().UserName;
                    Document_Employee.CompletedTime = requestHistory.Last().dateModifiedToLocalTime;
                    Document_Employee.SupportRequestId = Request.supportRequestId;
                    return Document_Employee;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<int> RequestAttachmentToScannedDocuments(SupportRequest Request, Document_Employee Document_Employee)
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
                            documents.RegistrantTypeId = Document_Employee.registrationType == 1 ? 1 : 3;
                            documents.DocTypeId = item.code;
                            documents.ImportId = importId;
                            documents.NisNumber = (int)Document_Employee.nisNo;
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

        public static async Task<Response> AddNISMapping(SupportRequest Request, Document_Employee Document_Employee)
        {
            try
            {
                NisMapping nisMapping = new NisMapping();
                nisMapping.nisNumberTypeId = 2;
                nisMapping.nisNumber = Document_Employee.nisNo.ToString();
                nisMapping.userAccountId = Request.ownerId;
                return await AddNisMapping(nisMapping);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<bool> AddEmployeeRole(SupportRequest Request)
        {
            try
            {
                AssignRoleToUserAccount roleToUserAccount = new AssignRoleToUserAccount();
                roleToUserAccount.userAccountId = Request.ownerId;
                roleToUserAccount.roleId = 7;
                roleToUserAccount.createdBy = 7083;
                return await AssignRoleToUserAccount(roleToUserAccount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_Employee> GetRequestDetailsEmployee(DocumentGuid guid)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByGuid<Document_Employee>("Document/Get?id", guid.message);

                if (!response.IsSuccess)
                {
                    return null;
                }
                return (Document_Employee)response.Result;
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
                Response response = await ApiRequest.UpdateWorkFlowState( userId, requestId,actionId);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void validatePhone(Document_Employee Document_Employee)
        {
            if (Document_Employee.homePhoneNumber != null)
            {
                Document_Employee.homePhoneNumber = Int64.Parse(Document_Employee.homePhoneNumber).ToString();
                Document_Employee.homePhoneNumber = Document_Employee.homePhoneNumber.Length > 10 ? Document_Employee.homePhoneNumber.Substring(Document_Employee.homePhoneNumber.Length - 10) : Document_Employee.homePhoneNumber;
            }
            if (Document_Employee.businessPhoneNumber != null)
            {
                Document_Employee.businessPhoneNumber = Int64.Parse(Document_Employee.businessPhoneNumber).ToString();
                Document_Employee.businessPhoneNumber = Document_Employee.businessPhoneNumber.Length > 10 ? Document_Employee.businessPhoneNumber.Substring(Document_Employee.businessPhoneNumber.Length - 10) : Document_Employee.businessPhoneNumber;
            }
            if (Document_Employee.businessMobile != null)
            {
                Document_Employee.businessMobile = Int64.Parse(Document_Employee.businessMobile).ToString();
                Document_Employee.businessMobile = Document_Employee.businessMobile.Length > 10 ? Document_Employee.businessMobile.Substring(Document_Employee.businessMobile.Length - 10) : Document_Employee.businessMobile;
            }
            if (Document_Employee.primaryMobileNumber != null) 
            {
                Document_Employee.primaryMobileNumber = Int64.Parse(Document_Employee.primaryMobileNumber).ToString();
                Document_Employee.primaryMobileNumber = Document_Employee.primaryMobileNumber.Length > 10 ? Document_Employee.primaryMobileNumber.Substring(Document_Employee.primaryMobileNumber.Length - 10) : Document_Employee.primaryMobileNumber;
            }  
        }
    }
}
