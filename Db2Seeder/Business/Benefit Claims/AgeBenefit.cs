using As400DataAccess;
using Db2Seeder.API.Helpers;
using Db2Seeder.API.Models;
using Db2Seeder.API.Request;
using Db2Seeder.NIS.SQL.Documents.DataAccess;
using Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments;
using Microsoft.AppCenter.Crashes;
using ShareModels.Models;
using ShareModels.Models.Benefit_Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder.Business.Benefit_Claims
{
    public class AgeBenefit
    {//SupportRequest/GetByState/Type/6/State/16
        public static async Task<List<SupportRequest>> GetAgeBenefitClaimCompleted()
        {
            try
            {
                List<SupportRequest> RequestList = new List<SupportRequest>();
                return RequestList = await ApiRequest.GetSupportRequestTypeByState(6, 16);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_AgeBenefit> AgeBenefitClaimDetail(SupportRequest Request)
        {
            try
            {
                DocumentGuid guid = new DocumentGuid();
                guid = await ApiRequest.GetRequestGUID(Request.supportRequestId);
                if (guid.message != Guid.Empty)
                {
                    Document_AgeBenefit Document_AgeBenefit = new Document_AgeBenefit();
                    List<RequestHistory> requestHistory = new List<RequestHistory>();
                    requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", Request.supportRequestId);
                    Document_AgeBenefit = await GetAgeBanefitDetails(guid);
                    Document_AgeBenefit.CompletedBy = requestHistory.Last().UserName;
                    Document_AgeBenefit.CompletedTime = requestHistory.Last().dateModified;
                    Document_AgeBenefit.SupportRequestId=Request.supportRequestId;
                    return Document_AgeBenefit;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_AgeBenefit> GetAgeBanefitDetails(DocumentGuid guid)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByGuid<Document_AgeBenefit>("Document/Get?id", guid.message);

                if (!response.IsSuccess)
                {
                    return null;
                }
                return (Document_AgeBenefit)response.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<int> RequestAttachmentToScannedDocuments(SupportRequest Request, Document_AgeBenefit Document_AgeBenefit)
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
                            documents.NisNumber = Document_AgeBenefit.nisNo;
                            documents.PdfData = await ApiRequest.GetDocument_Data(item.documentImageGuid);
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
        public static async Task<Response> UpdateWorkFlowStateAgeBenefit(int userId, int requestId, int actionId)
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




       
        //Document/Get?id=b5a1b323-3adf-4917-b77a-c6fda5f1be5f
        private static async Task<Document_AgeBenefit> GetRequestDetailsAgeBenefit(DocumentGuid guid)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByGuid<Document_AgeBenefit>("Document/Get?id", guid.message);

                if (!response.IsSuccess)
                {

                    return null;
                }
                return (Document_AgeBenefit)response.Result;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
        }
    }
}
