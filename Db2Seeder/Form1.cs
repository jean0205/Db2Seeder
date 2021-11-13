using As400DataAccess;
using Db2Seeder.API.Request;
using Microsoft.AppCenter.Crashes;
using ShareModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Db2Seeder
{
    public partial class Form1 : Form
    {       
        List<SupportRequestType> RequestTypeList;
        List<SupportRequest> RequestList;
        Document_Employee Document_Employee;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            await GetEmployeesCompleted();
        }

        async void GetRequestType()
        {
            try
            {
                RequestTypeList = new List<SupportRequestType>();
                RequestTypeList = await ApiRequest.GetSupportRequestTypes();

                if (RequestTypeList.Any())
                {

                }

            }
            catch (Exception ex)
            {

                Crashes.TrackError(ex);
            }

        }

        //SupportRequest/GetByState/Type/3/State/8
        async Task GetEmployeesCompleted()
        {
            try
            {
                RequestList = new List<SupportRequest>();
                RequestList = await ApiRequest.GetSupportRequestTypeByState(3, 8);
                if (RequestList.Any())
                {
                    foreach (var request in RequestList)
                    {
                        DocumentGuid guid= new DocumentGuid();
                         guid = await GetRequestGUID(request.supportRequestId);

                        if (guid.message!= Guid.Empty)
                        {
                            
                             Document_Employee= await ApiRequest.GetEmployeeRequest(guid);
                            //TODO
                            //pasar el objeto a as400 para que se registre el employee y recibir el NIS

                            //el nis recibido despues de insertar
                            Document_Employee.nisNo = 99999;

                            //TODO
                            //si se registra satisfactoriamente(recibo un nis de palacio), leer la lista de attachement para insertarla en sql server

                            List<DocumentGuid> attachmentsGuid= await GetAttachmentsGuid(request.supportRequestId);
                            if (attachmentsGuid.Any())
                            {
                                foreach(var attachment in attachmentsGuid)
                                {
                                    Document_MetaData document_MetaData = new Document_MetaData();
                                    document_MetaData = await GetDocument_MetaData(attachment);
                                    
                                }  
                               

                            }


                        }
                    }
                }

            }
            catch (Exception ex)
            {

                Crashes.TrackError(ex);
            }

        }

        //SupportRequest/FormGuid? id = 580
        async Task<DocumentGuid> GetRequestGUID(int id)
        {
            DocumentGuid guid = new DocumentGuid();
            try
            {
                guid = await ApiRequest.GetGUIDDocument("SupportRequest/FormGuid?id",id);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return guid;
        }

        //get attachements Guids
        //SupportRequest/AttachmentGuids?id=580

        async Task<List<DocumentGuid>> GetAttachmentsGuid(int id)
        {
            List<DocumentGuid> guids = new List<DocumentGuid>();
            try
            {
                guids = await ApiRequest.GetListGUIDDocument("SupportRequest/AttachmentGuids?id", id);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return guids;
        }

        //get metadata for documents lis
        async Task<Document_MetaData> GetDocument_MetaData(DocumentGuid id)
        {
            Document_MetaData document_Meta = new Document_MetaData();
            try
            {

                document_Meta = await ApiRequest.GetSupportRequestAttachments(id);

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return document_Meta;
        }
    }
}
