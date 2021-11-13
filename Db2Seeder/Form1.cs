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
        //AS400DATACCESS
        EmployeeDB2 as400Empe;


        List<SupportRequestType> RequestTypeList;
        List<SupportRequest> RequestList;
        Document_Employee Document_Employee;

        bool finish = true;
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
                finish = false;
                RequestList = new List<SupportRequest>();
                RequestList = await ApiRequest.GetSupportRequestTypeByState(3, 8);
                if (RequestList.Any())
                {
                    foreach (var request in RequestList)
                    {
                        DocumentGuid guid = new DocumentGuid();
                        guid = await GetRequestGUID(request.supportRequestId);

                        if (guid.message != Guid.Empty)
                        {
                            Document_Employee = new Document_Employee();
                            Document_Employee = await ApiRequest.GetEmployeeRequest(guid);
                            //TODO
                            //pasar el objeto a as400 para que se registre el employee y recibir el NIS
                            as400Empe = new EmployeeDB2();
                            Document_Employee.dateOfBirth = DateTime.Today;
                            Document_Employee.dateOfMarriage = DateTime.Today;
                            Document_Employee.middleName = String.Empty;
                            Document_Employee.accountNumber = "55555";
                            Document_Employee.nisNo = await as400Empe.InsertEmployees(Document_Employee);
                            if (Document_Employee.nisNo == 0)
                            {
                                return;
                            }
                            //el nis recibido despues de insertar


                            //TODO
                            //si se registra satisfactoriamente(recibo un nis de palacio), leer la lista de attachement para insertarla en sql server

                            List<DocumentGuid> attachmentsGuid = await GetAttachmentsGuid(request.supportRequestId);
                            if (attachmentsGuid.Any())
                            {
                                List<Document_MetaData> document_MetaDataList = new List<Document_MetaData>();
                                document_MetaDataList = await GetDocument_MetaData(attachmentsGuid);
                                if (document_MetaDataList.Any())
                                {
                                    //buscar el pdf Data
                                    foreach (var item in document_MetaDataList)
                                    {
                                        //TODO
                                        //BUSCAR QUE FORMATO DEVUELVE E API PARA LOS DOCUMENTOS, NO ES UN JSON
                                        if (await GetDocument_Data(item.documentImageGuid))
                                        {

                                        }

                                    }

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
            finish = true;
        }

        //SupportRequest/FormGuid? id = 580
        async Task<DocumentGuid> GetRequestGUID(int id)
        {
            DocumentGuid guid = new DocumentGuid();
            try
            {
                guid = await ApiRequest.GetGUIDDocument("SupportRequest/FormGuid?id", id);
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
        async Task<List<Document_MetaData>> GetDocument_MetaData(List<DocumentGuid> ids)
        {
            List<Document_MetaData> document_Meta = new List<Document_MetaData>();
            try
            {
                document_Meta = await ApiRequest.GetSupportRequestAttachments(ids);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return document_Meta;
        }

        //get PDF Data
        async Task<bool> GetDocument_Data(Guid guid)
        {
            try
            {
                var result = await ApiRequest.GetSupportRequestAttachmentsData(guid);
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return false;
        }

        private async void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            await GetEmployeesCompleted();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (finish)
            {
                await GetEmployeesCompleted();
            }
        }
    }
}
