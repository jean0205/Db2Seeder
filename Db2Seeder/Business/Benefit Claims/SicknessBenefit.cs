using Db2Seeder.API.Helpers;
using Db2Seeder.API.Models;
using Db2Seeder.API.Request;
using ShareModels.Models;
using ShareModels.Models.Sickness_Claim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db2Seeder.Business.Benefit_Claims
{
    public class SicknessBenefit
    {
        //SupportRequest/GetByState/Type/13/State/42
        public static async Task<List<SupportRequest>> GetClaimsCompleted()
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
        public static async Task<Document_Sickness> ClaimDetail(SupportRequest Request)
        {
            try
            {
                DocumentGuid guid = new DocumentGuid();
                guid = await ApiRequest.GetRequestGUID(Request.supportRequestId);
                if (guid.message != Guid.Empty)
                {
                    Document_Sickness Document_Sickness = new Document_Sickness();
                    List<RequestHistory> requestHistory = new List<RequestHistory>();
                    requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", Request.supportRequestId);
                    Document_Sickness = await GetDetails(guid);

                    Document_Sickness.CompletedBy = requestHistory.Last().UserName;
                    Document_Sickness.CompletedTime = requestHistory.Last().dateModified;
                    Document_Sickness.SupportRequestId = Request.supportRequestId;
                    return Document_Sickness;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_Sickness> GetDetails(DocumentGuid guid)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByGuid<Document_Sickness>("Document/Get?id", guid.message);

                if (!response.IsSuccess)
                {
                    return null;
                }
                return (Document_Sickness)response.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Response> UpdateWorkFlowStateSicknessBenefit(int userId, int requestId, int actionId)
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
