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
                throw ex;
            }
           
        }
        #endregion

        //SupportRequest/FormGuid? id = 580
        public static async Task<DocumentGuid> GetRequestGUID(int id)
        {
            DocumentGuid guid = new DocumentGuid();
            try
            {
                guid = await GetGUIDDocument("SupportRequest/FormGuid?id", id);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
            return guid;
        }

        //get attachements Guids
        //SupportRequest/AttachmentGuids?id=580

        public static async Task<List<DocumentGuid>> GetAttachmentsGuid(int id)
        {
            List<DocumentGuid> guids = new List<DocumentGuid>();
            try
            {
                guids = await GetListGUIDDocument("SupportRequest/AttachmentGuids?id", id);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
            return guids;
        }

        //get metadata for documents lis
        public static async Task<List<Document_MetaData>> GetDocument_MetaData(List<DocumentGuid> ids)
        {
            List<Document_MetaData> document_Meta = new List<Document_MetaData>();
            try
            {
                document_Meta = await GetSupportRequestAttachments(ids);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
            return document_Meta;
        }

        //get PDF Data
        public static async Task<byte[]> GetDocument_Data(Guid guid)
        {
            try
            {
                return await GetSupportRequestAttachmentsData(guid);

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
        }
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
                throw ex;
            }
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
                throw ex;
            }
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
                throw ex;
            }
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
        public static async Task<byte[]> GetSupportRequestAttachmentsData(Guid guid)
        {
            try
            {
                Response response = await ApiServices.GetAttachmentAsync<byte[]>("Document/DownloadImage?documentImageGuid", guid);

                if (!response.IsSuccess)
                {
                    return null;
                }
                return (byte[])response.Result;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
        }

        public static async Task<List<RequestHistory>> GetRequestHistory(string controler, int id)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByID<List<RequestHistory>>(controler, id);

                if (!response.IsSuccess)
                {

                    return new List<RequestHistory>();
                }
                return (List<RequestHistory>)response.Result;
            }
             catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
        }



    }
}
