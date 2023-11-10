using Db2Seeder.API.Helpers;
using Db2Seeder.API.Models;
using Db2Seeder.API.Request;
using Db2Seeder.NIS.SQL.Documents.DataAccess;
using Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments;
using ShareModels.Models;
using ShareModels.Models.Benefit_Claims;
using ShareModels.Models.Sickness_Claim;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Db2Seeder.Business.Benefit_Claims
{
    public class EmployInjuryBenefit
    {
        //SupportRequest/GetByState/Type/16/State/40
        public static async Task<List<SupportRequest>> GetClaimsCompleted()
        {
            try
            {
                List<SupportRequest> RequestList = new List<SupportRequest>();
                return RequestList = await ApiRequest.GetSupportRequestTypeByState(16, 227);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_EmploymentInjury> ClaimDetail(SupportRequest Request)
        {
            try
            {                
                DocumentGuid guid = new DocumentGuid();
                guid = await ApiRequest.GetRequestGUID(Request.supportRequestId);
                if (guid.message != Guid.Empty)
                {
                    Document_EmploymentInjury Document_EmploymentInjury = new Document_EmploymentInjury();
                    List<RequestHistory> requestHistory = new List<RequestHistory>();
                    requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", Request.supportRequestId);
                    Document_EmploymentInjury = await GetDetails(guid);
                    Document_EmploymentInjury.CompletedBy = requestHistory.Last().UserName;
                    Document_EmploymentInjury.CompletedTime = requestHistory.Last().dateModifiedToLocalTime;
                    Document_EmploymentInjury.SupportRequestId = Request.supportRequestId;

                    //actualizar la fecha de creada a cuando esta lista 
                    Document_EmploymentInjury.CreatedOn = requestHistory.OrderBy(x => x.dateModified).Where(x => x.description.Contains("Pending Processing")).Last().dateModified.ToLocalTime();

                    return Document_EmploymentInjury;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_EmploymentInjury> GetDetails(DocumentGuid guid)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByGuid<Document_EmploymentInjury>("Document/Get?id", guid.message);

                if (!response.IsSuccess)
                {
                    return null;
                }
                return (Document_EmploymentInjury)response.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Response> UpdateWorkFlowState(int userId, int requestId, int actionId)
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
        public static async Task<int> RequestAttachmentToScannedDocuments(SupportRequest Request, Document_EmploymentInjury Document_EmploymentInjury)
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
                            documents.RegistrantTypeId = 1;
                            documents.DocTypeId = item.code;
                            documents.ImportId = importId;
                            documents.NisNumber = Document_EmploymentInjury.NisNo;
                            documents.ClaimNumber = Document_EmploymentInjury.ClaimNumber;
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

        #region SEP 

        public static async Task<List<SupportRequest>> GetClaimsCompletedSEP()
        {
            try
            {
                List<SupportRequest> RequestList = new List<SupportRequest>();
                return RequestList = await ApiRequest.GetSupportRequestTypeByState(31, 33800);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_EmploymentInjury> ClaimDetailSEP(SupportRequest Request)
        {
            try
            {
                DocumentGuid guid = new DocumentGuid();
                guid = await ApiRequest.GetRequestGUID(Request.supportRequestId);
                if (guid.message != Guid.Empty)
                {
                    Document_EmploymentInjury Document_EmploymentInjury = new Document_EmploymentInjury();
                    List<RequestHistory> requestHistory = new List<RequestHistory>();
                    requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", Request.supportRequestId);
                    Document_EmploymentInjury = await GetDetails(guid);
                    Document_EmploymentInjury.CompletedBy = requestHistory.Last().UserName;
                    Document_EmploymentInjury.CompletedTime = requestHistory.Last().dateModifiedToLocalTime;
                    Document_EmploymentInjury.SupportRequestId = Request.supportRequestId;

                    //actualizar la fecha de creada a cuando esta lista 
                    Document_EmploymentInjury.CreatedOn = requestHistory.OrderBy(x => x.dateModified).Where(x => x.description.Contains("Pending Processing")).Last().dateModified.ToLocalTime();

                    return Document_EmploymentInjury;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Response> UpdateWorkFlowStateSEP(int userId, int requestId, int actionId)
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

        #endregion
    }
}
