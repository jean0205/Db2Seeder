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
        List<SupportRequest> RequestList;
        Document_AgeBenefit Document_AgeBenefit;
        //SupportRequest/GetByState/Type/6/State/16
        public async Task GetRemittanceAgeBenefitCompleted()
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
