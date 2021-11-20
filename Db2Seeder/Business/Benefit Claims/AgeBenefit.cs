using As400DataAccess;
using Db2Seeder.API.Helpers;
using Db2Seeder.API.Models;
using Db2Seeder.API.Request;
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
    {
        readonly AgePensionDB2 as400AgePension=new AgePensionDB2();

        List<SupportRequest> RequestList;
        Document_AgeBenefit Document_AgeBenefit;
        //SupportRequest/GetByState/Type/6/State/16

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




        public async Task GetAgeBenefitCompleted()
        {
            try
            {
                RequestList = new List<SupportRequest>();
                RequestList = await ApiRequest.GetSupportRequestTypeByState(6, 16);
                if (RequestList.Any())
                {
                    foreach (var request in RequestList)
                    {
                        DocumentGuid guid = new DocumentGuid();
                        guid = await ApiRequest.GetRequestGUID(request.supportRequestId);

                        if (guid.message != Guid.Empty)
                        {
                            Document_AgeBenefit = new Document_AgeBenefit();
                            Document_AgeBenefit = await GetRequestDetailsAgeBenefit(guid);                            

                            List<RequestHistory> requestHistory = new List<RequestHistory>();
                            requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", request.supportRequestId);
                           
                           // Document_AgeBenefit.
                           var claim= await as400AgePension.InsertAgePension(Document_AgeBenefit);
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
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
