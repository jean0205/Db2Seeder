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
        //SupportRequest/GetByState/Type/3/State/8
        public static async Task<List<SupportRequest>> GetSupportRequestTypeByState( int type, int state)
        {
            try
            {
                Response response = await ApiServices.GetListAsyncByTypeandState<SupportRequest>("SupportRequest/GetByState",type, state);

                if (!response.IsSuccess)
                {
                   
                    return new List<SupportRequest>();
                }
                return (List<SupportRequest>)response.Result;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return new List<SupportRequest>();
        }

        //SupportRequest/FormGuid? id = 580
        public static async Task<DocumentGuid> GetSupportRequestGUIDDocument(int id)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByID<DocumentGuid>("SupportRequest/FormGuid?id", id);

                if (!response.IsSuccess)
                {

                    return new DocumentGuid();
                }
                return (DocumentGuid)response.Result;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return  new DocumentGuid();
        }
        public static async Task<Document_Employee> GetEmployeeRequest(DocumentGuid guid)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByGuid<Document_Employee>("Document/Get?id", guid.message);

                if (!response.IsSuccess)
                {

                    return null;
                }
                return (Document_Employee)response.Result;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return null;
        }

    }
}
