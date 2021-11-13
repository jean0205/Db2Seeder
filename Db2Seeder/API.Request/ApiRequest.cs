using Db2Seeder.API.Models;
using ShareModels.Models;
using Microsoft.AppCenter.Crashes;
using System.Collections.Generic;
using System.Threading.Tasks;
using Db2Seeder.API.Helpers;
using System;

namespace Db2Seeder.API.Request
{
    static class ApiRequest
    {      



        #region SupportRequestType
        public static  async Task<List<SupportRequestType>> GetSupportRequestTypes()
        {
            try
            {
                Response response = await ApiServices.GetListAsync<SupportRequestType>("SupportRequest","GetSupportRequestType");

                if (!response.IsSuccess)
                {
                    UtilRecurrent.ErrorMessage(response.Message);
                    return new List<SupportRequestType>();
                }
                return (List<SupportRequestType>)response.Result;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return new List<SupportRequestType>();
        }
        #endregion

    }
}
