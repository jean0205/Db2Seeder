using Db2Seeder.API.Helpers;
using Db2Seeder.API.Models;
using Db2Seeder.API.Request;
using Db2Seeder.NIS.SQL.Documents.DataAccess;
using Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments;
using Db2Seeder.NIS.SQL.Unemployment.ModelsUnemployment.DataAccess;
using Db2Seeder.NIS.SQL.Unemployment.ModelsUnemployment;
using Newtonsoft.Json;
using ShareModels.Models;
using ShareModels.Models.Benefit_Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Db2Seeder.Business
{
    public class UEB_Declaration_SEP
    {
        public static async Task<List<SupportRequest>> GetClaimsCompleted()
        {
            try
            {
                List<SupportRequest> RequestList = new List<SupportRequest>();
                return RequestList = await ApiRequest.GetSupportRequestTypeByState(21, 260);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_UEB_Declaration> ClaimDetail(SupportRequest Request)
        {
            try
            {
                DocumentGuid guid = new DocumentGuid();
                guid = await ApiRequest.GetRequestGUID(Request.supportRequestId);
                if (guid.message != Guid.Empty)
                {
                    Document_UEB_Declaration declaration = new Document_UEB_Declaration();
                    List<RequestHistory> requestHistory = new List<RequestHistory>();
                    requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", Request.supportRequestId);
                    declaration = await GetDetails(guid);

                    if (declaration == null)
                    {
                        return null;
                    }
                    if (requestHistory.Any())
                    {

                        declaration.CompletedBy = requestHistory.Last().UserName;
                        declaration.CompletedTime = requestHistory.Last().dateModifiedToLocalTime;
                        declaration.SupportRequestId = Request.supportRequestId;

                        if (requestHistory.Any(x => x.description.Contains("Pending Processing")))
                        {
                            declaration.createdOn = requestHistory.OrderBy(x => x.dateModified).Where(x => x.description.Contains("Pending Processing")).Last().dateModified.ToLocalTime();
                        }

                            //actualizar la fecha de creada a cuando esta lista 
                           
                    }

                     

                    return declaration;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_UEB_Declaration> GetDetails(DocumentGuid guid)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByGuid<Document_UEB_Declaration>("Document/Get?id", guid.message);

                if (!response.IsSuccess)
                {
                    return null;
                }
                return (Document_UEB_Declaration)response.Result;
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

        public static async Task<int> RequestAttachmentToScannedDocuments(SupportRequest Request, Document_UEB_Declaration declaration)
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
                            documents.NisNumber = int.Parse(declaration.nisNo);
                            documents.ClaimNumber = declaration.ClaimNumber;
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
        public static async Task<bool> SaveJsoninNISDataBase(SupportRequest Request, Document_UEB_Declaration declaration, long claimSupportRequestId)
        {
            try
            {
                UnemploymentDB unemploymentDB = new UnemploymentDB();
                UnempDeclaration decl = new UnempDeclaration();

                decl.NisNumber = long.Parse(declaration.nisNo);
                decl.ClaimNumber = declaration.ClaimNumber;
                decl.ClaimSupportRequestId = claimSupportRequestId;
                decl.DeclarationJson = JsonConvert.SerializeObject(declaration);
                decl.SavedTime = DateTime.Now;
                decl.SupportrequestId = Request.supportRequestId;
                decl.Status = "PENDING";
                await unemploymentDB.InsertUnemploymentDeclaration(decl);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static async Task<RequestClaimMapping> GetClaimRequestMappingByRequestId(long claimSupportRequestId)
        {
            try
            {
                UnemploymentDB unemploymentDB = new UnemploymentDB();
                return await unemploymentDB.GetClaimRequestMappingByRequestId(claimSupportRequestId);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
