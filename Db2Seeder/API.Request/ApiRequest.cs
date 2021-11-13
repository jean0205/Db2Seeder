using Db2Seeder.API.Models;
using ShareModels.Models;
using Microsoft.AppCenter.Crashes;
using System.Collections.Generic;
using System.Threading.Tasks;
using Db2Seeder.API.Helpers;
using System;
using System.Linq;

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
        public static async Task<DocumentGuid> GetGUIDDocument(string controler,int id)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByID<DocumentGuid>(controler, id);

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
        public static async Task<List<DocumentGuid>> GetListGUIDDocument(string controler, int id)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByID<List<DocumentGuid>>(controler, id);

                if (!response.IsSuccess)
                {

                    return new List<DocumentGuid>();
                }
                return (List<DocumentGuid>)response.Result;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return new List<DocumentGuid>();
        }

        //Document/Get?id=b5a1b323-3adf-4917-b77a-c6fda5f1be5f
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
        //attachment metadatas list
        public static async Task<List<Document_MetaData>> GetSupportRequestAttachments(List<DocumentGuid> guids)
        {
            try
            {
                List<Guid> attachmentGuids = new List<Guid>();
                attachmentGuids = guids.Select(x=>x.message).ToList();
                Response response = await ApiServices.PostAsync<List<Document_MetaData>>("Document/ListMetaByDocuments", attachmentGuids);

                if (!response.IsSuccess)
                {

                    return null;
                }
                return (List<Document_MetaData>)response.Result;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return null;
        }

        //get pdf data
        public static async Task<object> GetSupportRequestAttachmentsData(Guid guid)
        {
            try
            {   
              
                Response response = await ApiServices.FindAsyncByGuid<object>("Document/DownloadImage?documentImageGuid", guid);

                if (!response.IsSuccess)
                {

                    return null;
                }
                return (object)response.Result;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return null;
        }

    }
}
