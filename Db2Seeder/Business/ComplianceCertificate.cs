using Db2Seeder.API.Helpers;
using Db2Seeder.API.Models;
using Db2Seeder.API.Request;
using Db2Seeder.Models_OnlineFoms;
using Db2Seeder.Models_OnlineFoms.DataAccess;
using Microsoft.AppCenter.Crashes;
using ShareModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db2Seeder.Business
{
    public class ComplianceCertificate
    {
        List<SupportRequest> RequestList;
        Document_ComplianceCert Document_ComplianceCert;

        //SupportRequest/GetByState/Type/2/State/2
        public async Task GetComplianceCrtCompleted()
        {
            try
            {
                RequestList = new List<SupportRequest>();
                RequestList = await ApiRequest.GetSupportRequestTypeByState(2, 2);
                if (RequestList.Any())
                {
                    foreach (var request in RequestList)
                    {
                        DocumentGuid guid = new DocumentGuid();
                        guid = await ApiRequest.GetRequestGUID(request.supportRequestId);

                        if (guid.message != Guid.Empty)
                        {
                            Document_ComplianceCert = new Document_ComplianceCert();
                            Document_ComplianceCert = await GetRequestDetailsComplianceCertificate(guid);
                            if (Document_ComplianceCert != null)
                            {
                                ComplianceCert certificate = new ComplianceCert();
                                OnlineFormsDB onlineFormsDB = new OnlineFormsDB();
                                //TODO                                
                                certificate.ImportedId = 100000 + Document_ComplianceCert.documentId;
                                certificate.EmployerNo = Document_ComplianceCert.employerNumber.Split('-')[0];
                                certificate.EmployerSub = Document_ComplianceCert.employerNumber.Split('-')[1];
                                certificate.BusinessName= Document_ComplianceCert.businessName.ToUpper();
                                certificate.BusinessAddress= Document_ComplianceCert.businessAddress.ToUpper();
                                certificate.Telephone = Document_ComplianceCert.phoneNumber;
                                certificate.Email = Document_ComplianceCert.emailAddress.ToUpper();
                                certificate.Reason = Document_ComplianceCert.certificateReason.ToUpper();
                                certificate.Title=Document_ComplianceCert.title.ToUpper();
                                certificate.AppDate = Document_ComplianceCert.createdOn.ToLocalTime();
                                List<RequestHistory> requestHistory = new List<RequestHistory>();
                                requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", request.supportRequestId);
                                certificate.UserUpload = requestHistory.Last().modifiedBy.ToUpper();
                                certificate.DateUpload = DateTime.Now;
                                certificate.AppState = "PENDING";
                                certificate.Notes = string.Empty;
                                certificate.Form = $@"\\nisgrenada.org\Work_Files\incoming\Online_forms\ComplianceCertificate\{certificate.ImportedId}.pdf";
                                onlineFormsDB.CreatePDFDocument(certificate);
                                await onlineFormsDB.InsertComplianceCertificate(certificate);
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
        public static async Task<Document_ComplianceCert> GetRequestDetailsComplianceCertificate(DocumentGuid guid)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByGuid<Document_ComplianceCert>("Document/Get?id", guid.message);

                if (!response.IsSuccess)
                {
                    return null;
                }
                return (Document_ComplianceCert)response.Result;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
        }
    }

}
