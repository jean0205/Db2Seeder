using As400DataAccess;
using Db2Seeder.API.Helpers;
using Db2Seeder.API.Models;
using Db2Seeder.API.Request;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using ShareModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db2Seeder.Business
{
    public class Remittance
    {
          ElectRemittanceDB2 as400Remittance= new ElectRemittanceDB2();
        List<SupportRequest> RequestList;
        Document_Remittance Document_Remittance;
        //SupportRequest/GetByState/Type/15/State/9

        public static async Task<List<SupportRequest>> GetRemittancePending()
        {
            try
            {
                List<SupportRequest> RequestList = new List<SupportRequest>();
                return RequestList = await ApiRequest.GetSupportRequestTypeByState(15, 9);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_Remittance> RemittanceDetailsDetail(SupportRequest Request)
        {
            try
            {
                DocumentGuid guid = new DocumentGuid();
                guid = await ApiRequest.GetRequestGUID(Request.supportRequestId);
                if (guid.message != Guid.Empty)
                {
                    Document_Remittance Document_Remittance = new Document_Remittance();
                    List<RequestHistory> requestHistory = new List<RequestHistory>();
                    requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", Request.supportRequestId);
                    Document_Remittance = await GetRequestDetailsRemittance(guid);
                    
                   
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task PostRemittanceToAs400(SupportRequest Request,Document_Remittance Document_Remittance)
        {
            try
            {
                string json = JsonConvert.SerializeObject(Document_Remittance);
                var RemittanceCopy = JsonConvert.DeserializeObject<Document_Remittance>(json);
                var periods = Document_Remittance.employeeContributionRecords.Select
                    (x => new { x.contributionPeriodYear, x.contributionPeriodMonth }).Distinct().ToList();

                List<RequestHistory> requestHistory = new List<RequestHistory>();
                requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", Request.supportRequestId);

                foreach (var period in periods)
                {
                    //pasando un solo periodo a la vez
                    RemittanceCopy.employeeContributionRecords.Clear();
                    RemittanceCopy.employeeContributionRecords = Document_Remittance.employeeContributionRecords.Where(x => x.contributionPeriodMonth == period.contributionPeriodMonth && x.contributionPeriodYear == period.contributionPeriodYear).ToList();

                    //copiando los earning para la ultima semana trabajada
                    foreach (var employee in RemittanceCopy.employeeContributionRecords.Where(x => x.frequency == "M"))
                    {
                        if (employee.week5.hasWorked == true)
                        {
                            employee.week5.amount = employee.insurableEarnings;
                            goto jmp;
                        }
                        if (employee.week4.hasWorked == true)
                        {
                            employee.week4.amount = employee.insurableEarnings;
                            goto jmp;
                        }
                        if (employee.week3.hasWorked == true)
                        {
                            employee.week3.amount = employee.insurableEarnings;
                            goto jmp;
                        }
                        if (employee.week2.hasWorked == true)
                        {
                            employee.week2.amount = employee.insurableEarnings;
                            goto jmp;
                        }
                        employee.week1.amount = employee.insurableEarnings;
                    jmp:;
                    }
                    //toma palacio
                    
                    //await as400Remittance.PostRemittances(RemittanceCopy);
                    //await PostAS400
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task PostAS400(Document_Remittance Document_Remittance)
        {
            await as400Remittance.PostRemittances(Document_Remittance);
        }



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

                            string json = JsonConvert.SerializeObject(Document_Remittance);
                            var RemittanceCopy = JsonConvert.DeserializeObject<Document_Remittance>(json);
                            var periods = Document_Remittance.employeeContributionRecords.Select
                                (x => new { x.contributionPeriodYear, x.contributionPeriodMonth }).Distinct().ToList();

                            List<RequestHistory> requestHistory = new List<RequestHistory>();
                            requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", request.supportRequestId);

                            foreach (var period in periods)
                            {
                                //pasando un solo periodo a la vez
                                RemittanceCopy.employeeContributionRecords.Clear();
                                RemittanceCopy.employeeContributionRecords = Document_Remittance.employeeContributionRecords.Where(x => x.contributionPeriodMonth == period.contributionPeriodMonth && x.contributionPeriodYear == period.contributionPeriodYear).ToList();

                                //copiando los earning para la ultima semana trabajada
                                foreach (var employee in RemittanceCopy.employeeContributionRecords.Where(x=>x.frequency=="M"))
                                {
                                    if (employee.week5.hasWorked == true)
                                    {
                                        employee.week5.amount = employee.insurableEarnings;
                                        goto jmp;
                                    }
                                    if (employee.week4.hasWorked == true)
                                    {
                                        employee.week4.amount = employee.insurableEarnings;
                                        goto jmp;
                                    }
                                    if (employee.week3.hasWorked == true)
                                    {
                                        employee.week3.amount = employee.insurableEarnings;
                                        goto jmp;
                                    }
                                    if (employee.week2.hasWorked == true)
                                    {
                                        employee.week2.amount = employee.insurableEarnings;
                                        goto jmp;
                                    }
                                    employee.week1.amount = employee.insurableEarnings;
                                jmp:;
                                }                            
                                //toma palacio
                                as400Remittance = new ElectRemittanceDB2();
                                await as400Remittance.PostRemittances(RemittanceCopy);
                            }
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
                throw ex;
            }
        }
    }

}
