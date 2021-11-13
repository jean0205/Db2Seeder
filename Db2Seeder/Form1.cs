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
                guid = await ApiRequest.GetSupportRequestGUIDDocument(id);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return guid;
        }

       
    }
}
