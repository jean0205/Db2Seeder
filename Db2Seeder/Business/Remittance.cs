using Db2Seeder.API.Helpers;
using Db2Seeder.API.Models;
using Db2Seeder.API.Request;
using Microsoft.AppCenter.Crashes;
using ShareModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder.Business
{
    public class Remittance
    {
        List<SupportRequest> RequestList;
        Document_Remittance Document_Remittance;
        //SupportRequest/GetByState/Type/15/State/9
        public async Task GetRemittancePendingReview()
        {
            try
            {
                RequestList = new List<SupportRequest>();
                RequestList = await ApiRequest.GetSupportRequestTypeByState(15, 9);
                if (RequestList.Any())
                {
                    foreach (var request in RequestList)
                    {
                        DocumentGuid guid = new DocumentGuid();
                        guid = await ApiRequest.GetRequestGUID(request.supportRequestId);

                        if (guid.message != Guid.Empty)
                        {
                            Document_Remittance = new Document_Remittance();
                            Document_Remittance = await GetRequestDetailsRemittance(guid);
                            //TODO
                            //pasar el objeto a as400 para que se registre el employee y recibir el NIS

                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        //Document/Get?id=b5a1b323-3adf-4917-b77a-c6fda5f1be5f
        public static async Task<Document_Remittance> GetRequestDetailsRemittance(DocumentGuid guid)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByGuid<Document_Remittance>("Document/Get?id", guid.message);

                if (!response.IsSuccess)
                {

                    return null;
                }
                return (Document_Remittance)response.Result;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return null;
        }
    }

}
