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
using System.Linq;
using System.Threading.Tasks;

namespace Db2Seeder.Business.Benefit_Claims
{
    public class CovidBenefit
    {
        //SupportRequest/GetByState/Type/14/State/18
        public static async Task<List<SupportRequest>> GetClaimsCompleted()
        {
            try
            {
                List<SupportRequest> RequestList = new List<SupportRequest>();
                return RequestList = await ApiRequest.GetSupportRequestTypeByState(14, 18);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_Covid19> ClaimDetail(SupportRequest Request)
        {
            try
            {                
                DocumentGuid guid = new DocumentGuid();
                guid = await ApiRequest.GetRequestGUID(Request.supportRequestId);
                if (guid.message != Guid.Empty)
                {
                    Document_Covid19 Document_Covid19 = new Document_Covid19();
                    List<RequestHistory> requestHistory = new List<RequestHistory>();
                    requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", Request.supportRequestId);
                    Document_Covid19 = await GetDetails(guid);

                    Document_Covid19.CompletedBy = requestHistory.Last().UserName;
                    Document_Covid19.CompletedTime = requestHistory.Last().dateModifiedToLocalTime;
                    Document_Covid19.SupportRequestId = Request.supportRequestId;
                    return Document_Covid19;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_Covid19> GetDetails(DocumentGuid guid)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByGuid<Document_Covid19>("Document/Get?id", guid.message);

                if (!response.IsSuccess)
                {
                    return null;
                }
                return (Document_Covid19)response.Result;
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
        public static async Task<int> RequestAttachmentToScannedDocuments(SupportRequest Request, Document_Covid19 Document_Covid19)
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
                            documents.NisNumber = Document_Covid19.NisNo;
                            documents.ClaimNumber = Document_Covid19.ClaimNumber;
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
    }
}
