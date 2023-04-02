using Db2Seeder.API.Helpers;
using Db2Seeder.API.Models;
using Db2Seeder.API.Request;
using Db2Seeder.NIS.SQL.Documents.DataAccess;
using Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments;
using Db2Seeder.NIS.SQL.Unemployment.ModelsUnemployment;
using Db2Seeder.NIS.SQL.Unemployment.ModelsUnemployment.DataAccess;
using Newtonsoft.Json;
using ShareModels.Models;
using ShareModels.Models.Benefit_Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db2Seeder.Business.Benefit_Claims
{
    public class UEB_TerminationCert
    {
        public static async Task<List<SupportRequest>> GetClaimsCompleted()
        {
            try
            {
                List<SupportRequest> RequestList = new List<SupportRequest>();
                return RequestList = await ApiRequest.GetSupportRequestTypeByState(22, 264);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_TerminationCertificate> ClaimDetail(SupportRequest Request)
        {
            try
            {
                DocumentGuid guid = new DocumentGuid();
                guid = await ApiRequest.GetRequestGUID(Request.supportRequestId);
                if (guid.message != Guid.Empty)
                {
                    Document_TerminationCertificate certificate = new Document_TerminationCertificate();
                    List<RequestHistory> requestHistory = new List<RequestHistory>();
                    requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", Request.supportRequestId);
                    certificate = await GetDetails(guid);

                    if (certificate == null)
                    {
                        return null;
                    }
                    certificate.CompletedBy = requestHistory.Last().UserName;
                    certificate.CompletedTime = requestHistory.Last().dateModifiedToLocalTime;
                    certificate.SupportRequestId = Request.supportRequestId;

                    //actualizar la fecha de creada a cuando esta lista 
                    certificate.createdOn = requestHistory.OrderBy(x => x.dateModified).Where(x => x.description.Contains("Pending Processing")).Last().dateModified.ToLocalTime();

                    return certificate;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_TerminationCertificate> GetDetails(DocumentGuid guid)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByGuid<Document_TerminationCertificate>("Document/Get?id", guid.message);

                if (!response.IsSuccess)
                {
                    return null;
                }
                return (Document_TerminationCertificate)response.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //buscar o crear action Id para cambiar el ststus to processing 
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
        public static async Task<bool> SaveJsoninNISDataBase(SupportRequest Request,Document_TerminationCertificate certificate)
        {
            try
            {
                UnemploymentDB unemploymentDB = new UnemploymentDB();
                TerminationCertificate cert = new TerminationCertificate();
                cert.NisNumber = long.Parse(certificate.nisNo);
                cert.EmployerNumber = long.Parse(certificate.employerNisNo);
                cert.EmployerSub = 0;//TODO update when the portal issue with employernis be fixed
                cert.CertificateJson = JsonConvert.SerializeObject(certificate);               
                cert.SavedTime = DateTime.Now;
                cert.SupportrequestId = Request.supportRequestId;
                await unemploymentDB.InsertTerminationCertificate(cert);
                return true;
                
            }
            catch (Exception ex)
            {
                throw ex;                
            }
            
        }
        public static async Task<int> RequestAttachmentToScannedDocuments(SupportRequest Request, Document_TerminationCertificate Document_UEB)
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
                            documents.NisNumber = int.Parse(Document_UEB.nisNo);
                            //documents.ClaimNumber = Document_UEB.ClaimNumber;
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
