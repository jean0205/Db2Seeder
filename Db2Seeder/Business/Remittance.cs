using As400DataAccess;
using Db2Seeder.API.Helpers;
using Db2Seeder.API.Models;
using Db2Seeder.API.Request;

using Db2Seeder.SQL.Logs.Helpers;
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
        ElectRemittanceDB2 as400Remittance = new ElectRemittanceDB2();
        List<SupportRequest> RequestList;
        Document_Remittance Document_Remittance;
        //SupportRequest/GetByState/Type/15/State/9

        public static async Task<List<SupportRequest>> GetRemittancePending()
        {
            try
            {
                List<SupportRequest> RequestList = new List<SupportRequest>();
                return RequestList = await ApiRequest.GetSupportRequestTypeByState(15, 231);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<List<SupportRequest>> GetRemittancePendingSEP()
        {
            try
            {
                List<SupportRequest> RequestList = new List<SupportRequest>();
                return RequestList = await ApiRequest.GetSupportRequestTypeByState(15, 55001);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Document_Remittance> RemittanceDetail(SupportRequest Request)
        {
            try
            {
                DocumentGuid guid = new DocumentGuid();
                guid = await ApiRequest.GetRequestGUID(Request.supportRequestId);
                if (guid.message != Guid.Empty)
                {
                    List<RequestHistory> requestHistory = new List<RequestHistory>();
                    requestHistory = await ApiRequest.GetRequestHistory("SupportRequest/History?id", Request.supportRequestId);
                    return await GetRequestDetailsRemittance(guid);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task PostRemittanceToAs400(SupportRequest Request, Document_Remittance Document_Remittance)
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
                            employee.week4.amount = 0.00;
                            employee.week3.amount = 0.00;
                            employee.week2.amount = 0.00;
                            employee.week1.amount = 0.00;
                            goto jmp;
                        }
                        else employee.week5.amount = 0.00;
                        if (employee.week4.hasWorked == true)
                        {
                            employee.week4.amount = employee.insurableEarnings;
                            employee.week3.amount = 0.00;
                            employee.week2.amount = 0.00;
                            employee.week1.amount = 0.00;
                            goto jmp;
                        }
                        else employee.week4.amount = 0.00;
                        if (employee.week3.hasWorked == true)
                        {
                            employee.week3.amount = employee.insurableEarnings;
                            employee.week2.amount = 0.00;
                            employee.week1.amount = 0.00;
                            goto jmp;
                        }
                        else employee.week3.amount = 0.00;
                        if (employee.week2.hasWorked == true)
                        {
                            employee.week2.amount = employee.insurableEarnings;
                            employee.week1.amount = 0.00;
                            goto jmp;
                        }
                        else employee.week2.amount = 0.00;
                        employee.week1.amount = employee.insurableEarnings;
                    jmp:;

                    }
                    //toma palacio
                    Remittance rt = new Remittance();
                    await rt.PostAS400(RemittanceCopy);
                    int yyyyMM = period.contributionPeriodYear * 100 + period.contributionPeriodMonth;
                    decimal totalIE = (decimal)Math.Round(RemittanceCopy.employeeContributionRecords.Sum(x => x.insurableEarnings), 2);
                    decimal totalCont = (decimal)Math.Round(RemittanceCopy.employeeContributionRecords.Sum(x => x.contributions.total), 2);

                    try
                    {
                        await LogsHelper.SaveRemittanceLOG(Request, Document_Remittance, yyyyMM, totalIE, totalCont);
                    }
                    catch (Exception)
                    {
                       
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task PostAS400(Document_Remittance Document_Remittance)
        {
            //as400Remittance.As400_lib = "NI";
            //await as400Remittance.PostRemittances(Document_Remittance);
            as400Remittance.As400_lib = "TT";
            await as400Remittance.PostRemittances(Document_Remittance);
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
