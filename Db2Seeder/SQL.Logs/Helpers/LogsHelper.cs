﻿using Db2Seeder.API.Helpers;
using Db2Seeder.SQL.Logs.DataAccess;
using Newtonsoft.Json;
using ShareModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Db2Seeder.SQL.Logs.Helpers

{
    public static class LogsHelper
    {
        public static async Task SaveErrorLOG(string errorMessage, SupportRequest request, int? formId, DateTime? completedOn)
        {
            Log log = new Log
            {
                RequestType = request.supportRequestType,
                RequestId = request.supportRequestId,
                FormId = formId,
                ErrorMessage = errorMessage,
                CreatedOn = request.createdOnToLocalTime,
                CompletedOn = completedOn,
            };
            LogsDB logsDB = new LogsDB();
            await logsDB.InsertErrorLog(log);
            //await UtilRecurrent.SendMail("jcsoto@nisgrenada.org", "DB2 Seeder Error", $"<h1>Error Message</h1>" +
            //     $"Please see error message hereunder:</br></br>" +
            //     $"{errorMessage}");
            //await UtilRecurrent.SendMail("spalacio@nisgrenada.org", "DB2 Seeder Error", $"<h1>Error Message</h1>" +
            //    $"Please see error message hereunder:</br></br>" +
            //    $"{errorMessage}");
        }
        public static async Task SaveEmployeeLOG(SupportRequest request, Document_Employee employee)
        {
            EmployeeRequestLog log = new EmployeeRequestLog
            {
                SupportRequestId = request.supportRequestId,
                Nisnumber = (int)employee.nisNo,
                FirstName = employee.firstName,
                MiddleName = employee.middleName,
                LastName = employee.lastName,
                DateOfBirth = (DateTime)employee.dateOfBirth,
                CreatedOn = (DateTime)request.createdOnToLocalTime,
                CompletedOn = (DateTime)employee.CompletedTime,
                CompletedBy = employee.CompletedBy,
                PostedOn = DateTime.Now
            };
            LogsDB logsDB = new LogsDB();
            await logsDB.InsertEmployeeRequestLog(log);
            //await UtilRecurrent.SendMail("jcsoto@nisgrenada.org", "DB2 Seeder Employee Posted", $"<h1>New Employee Posted</h1>" +
            //     $"Please see Information below:</br></br>" +
            //     $"Employee Number: {employee.nisNo}" +
            //     "</br ></br> " +
            //      $"Employee Name: {employee.firstName} {employee.lastName}" +
            //     "</br ></br> " +
            //      $" Date Of Birth: {employee.dateOfBirth}" +
            //     "</br ></br> ");
            //await UtilRecurrent.SendMail("spalacio@nisgrenada.org", "DB2 Seeder Employee Posted", $"<h1>New Employee Posted</h1>" +
            //    $"Please see Information below:</br></br>" +
            //    $"Employee Number: {employee.nisNo}" +
            //    "</br ></br> " +
            //     $"Employee Name: {employee.firstName} {employee.lastName}" +
            //    "</br ></br> " +
            //     $" Date Of Birth: {employee.dateOfBirth}" +
            //    "</br ></br> ");

        }
        public static async Task SaveSelfEmployeeLOG(SupportRequest request, Document_Employee employee)
        {
            EmployerRequestLog log = new EmployerRequestLog
            {
                SupportRequestId = request.supportRequestId,
                SelfEmployed = true,
                Voluntary = false,
                EmployerNo = (int)employee.nisNo,
                EmloyerName = $"{employee.firstName} {employee.lastName}",
                CreatedOn = (DateTime)request.createdOnToLocalTime,
                CompletedOn = (DateTime)employee.CompletedTime,
                CompletedBy = employee.CompletedBy,
                PostedOn = DateTime.Now
            };
            LogsDB logsDB = new LogsDB();
            await logsDB.InsertEmployerRequestLog(log);
            //await UtilRecurrent.SendMail("jcsoto@nisgrenada.org", "DB2 Seeder Employer(Self-Employee) Posted", $"<h1>New Employer Posted</h1>" +
            //     $"Please see Information below:</br></br>" +
            //     $"Employer Number {employee.nisNo}" +
            //     "</br ></br> " +
            //      $" Employer Name: {employee.firstName} {employee.lastName}" +
            //     "</br ></br> ");
            //await UtilRecurrent.SendMail("spalacio@nisgrenada.org", "DB2 Seeder Employer(Self-Employee) Posted", $"<h1>New Employer Posted</h1>" +
            //    $"Please see Information below:</br></br>" +
            //    $"Employer Number {employee.nisNo}" +
            //    "</br ></br> " +
            //     $" Employer Name: {employee.firstName} {employee.lastName}" +
            //    "</br ></br> ");

        }
        public static async Task SaveVoluntaryLOG(SupportRequest request, Document_Employee employee)
        {
            EmployerRequestLog log = new EmployerRequestLog
            {
                SupportRequestId = request.supportRequestId,
                SelfEmployed = false,
                Voluntary = true,
                EmployerNo = (int)employee.nisNo,
                EmloyerName = $"{employee.firstName} {employee.lastName}",
                CreatedOn = (DateTime)request.createdOnToLocalTime,
                CompletedOn = (DateTime)employee.CompletedTime,
                CompletedBy = employee.CompletedBy,
                PostedOn = DateTime.Now
            };
            LogsDB logsDB = new LogsDB();
            await logsDB.InsertEmployerRequestLog(log);
            //await UtilRecurrent.SendMail("jcsoto@nisgrenada.org", "DB2 Seeder Employer(Voluntary) Posted", $"<h1>New Employer Posted</h1>" +
            //     $"Please see Information below:</br></br>" +
            //     $"Employer Number {employee.nisNo}" +
            //     "</br ></br> " +
            //      $" Employer Name: {employee.firstName} {employee.lastName}" +
            //     "</br ></br> ");
            //await UtilRecurrent.SendMail("spalacio@nisgrenada.org", "DB2 Seeder Employer(Voluntary) Posted", $"<h1>New Employer Posted</h1>" +
            //    $"Please see Information below:</br></br>" +
            //    $"Employer Number {employee.nisNo}" +
            //    "</br ></br> " +
            //     $" Employer Name: {employee.firstName} {employee.lastName}" +
            //    "</br ></br> ");

        }
        public static async Task SaveEmployerLOG(SupportRequest request, Document_Employer employer)
        {
            EmployerRequestLog log = new EmployerRequestLog
            {
                SupportRequestId = request.supportRequestId,
                SelfEmployed = false,
                Voluntary = false,
                EmployerNo = (long)employer.employerNo,
                EmployerSub = 0,
                EmloyerName = employer.employerName,
                CreatedOn = (DateTime)request.createdOnToLocalTime,
                CompletedOn = (DateTime)employer.CompletedTime,
                CompletedBy = employer.CompletedBy,
                PostedOn = DateTime.Now
            };
            LogsDB logsDB = new LogsDB();
            await logsDB.InsertEmployerRequestLog(log);
            //await UtilRecurrent.SendMail("jcsoto@nisgrenada.org", "DB2 Seeder Employer Posted", $"<h1>New Employer Posted</h1>" +
            //     $"Please see Information below:</br></br>" +
            //     $"Employer Number: {employer.employerNo}" +
            //     "</br ></br> " +
            //      $"Employer Name: {employer.employerName}" +
            //     "</br ></br> " +
            //      $"Self-Employee:0" +
            //     "</br ></br> " +
            //      $"Voluntary:0" +
            //     "</br ></br> ");
            //await UtilRecurrent.SendMail("spalacio@nisgrenada.org", "DB2 Seeder Employer Posted", $"<h1>New Employer Posted</h1>" +
            //    $"Please see Information below:</br></br>" +
            //    $"Employer Number: {employer.employerNo}" +
            //    "</br ></br> " +
            //     $"Employer Name: {employer.employerName}" +
            //    "</br ></br> " +
            //     $"Self-Employee:0" +
            //    "</br ></br> " +
            //     $"Voluntary:0" +
            //    "</br ></br> ");
        }
        public static async Task SaveComplianceLOG(SupportRequest request, Document_ComplianceCert compliance)
        {
            ComplianceCertRequestLog log = new ComplianceCertRequestLog
            {
                SupportRequestId = request.supportRequestId,
                EmployerNo = long.Parse(compliance.employerNumber.Split('-')[0]),
                EmployerSub = compliance.employerNumber.Contains("-")?int.Parse(compliance.employerNumber.Split('-')[1]):0,
                BusinessName = compliance.businessName,
                EmailAddress = compliance.emailAddress,
                PhoneNumber = compliance.phoneNumber,
                CreatedOn = (DateTime)request.createdOnToLocalTime,
                CompletedOn = (DateTime)compliance.CompletedTime,
                CompletedBy = compliance.CompletedBy,
                PostedOn = DateTime.Now
            };
            LogsDB logsDB = new LogsDB();
            await logsDB.InsertComplianceCertificateLog(log);
            //await UtilRecurrent.SendMail("jcsoto@nisgrenada.org", "DB2 Seeder Compliance Certificate", $"<h1>New Compliance Certificate Posted</h1>" +
            //     $"Please see Information below:</br></br>" +
            //     $"Employer Number: {compliance.employerNumber}" +
            //     "</br ></br> " +
            //      $"Business Name: {compliance.businessName}" +
            //     "</br ></br>");
            //await UtilRecurrent.SendMail("spalacio@nisgrenada.org", "DB2 Seeder Compliance Certificate", $"<h1>New Compliance Certificate Posted</h1>" +
            //    $"Please see Information below:</br></br>" +
            //    $"Employer Number: {compliance.employerNumber}" +
            //    "</br ></br> " +
            //     $"Business Name: {compliance.businessName}" +
            //    "</br ></br>");
        }
        public static async Task SaveRemittanceLOG(SupportRequest request, Document_Remittance remittance, int period, decimal insEarnings, decimal contribution)
        {
            RemittanceLog log = new RemittanceLog
            {
                SupportRequestId = request.supportRequestId,
                EmployerNo = long.Parse(remittance.employerNumber.Split('-')[0]),
                EmployerSub = int.Parse(remittance.employerNumber.Split('-')[1]),
                Period = period,
                TotalInsuranceEarning = insEarnings,
                TotalContribution = contribution,
                CreatedOn = (DateTime)request.createdOnToLocalTime,
                PostedOn = DateTime.Now
            };
            LogsDB logsDB = new LogsDB();
            await logsDB.InsertRemittanceLog(log);
            //await UtilRecurrent.SendMail("jcsoto@nisgrenada.org", "DB2 Seeder Remittance", $"<h1>New Remittance Posted</h1>" +
            //     $"Please see Information below:</br></br>" +
            //     $"Employer Number: {remittance.employerNumber}" +
            //     "</br ></br> " +
            //      $" Period: {period}" +
            //       "</br ></br> " +
            //      $" Total Insurance Earnings: {insEarnings}" +
            //       "</br ></br> " +
            //        $" Total Controbutions: {contribution}" +
            //     "</br ></br>");
            //await UtilRecurrent.SendMail("spalacio@nisgrenada.org", "DB2 Seeder Remittance", $"<h1>New Remittance Posted</h1>" +
            //     $"Please see Information below:</br></br>" +
            //     $"Employer Number: {remittance.employerNumber}" +
            //     "</br ></br> " +
            //      $" Period: {period}" +
            //       "</br ></br> " +
            //      $" Total Insurance Earnings: {insEarnings}" +
            //       "</br ></br> " +
            //        $" Total Controbutions: {contribution}" +
            //     "</br ></br>");
        }

        public static async Task SaveClaimLOG(SupportRequest request, long claimNumber,string benType,string employerNumber,long employeeNumber,string employeeName,DateTime approvedOn,string approvedBy)
        {
            string emprNo = employerNumber;
            //if (employerNumber !="0")
            //{
            //    Match match = Regex.Match(employerNumber, @"^[0-9-]*$");
            //    emprNo = match.Value;               
            //}           
            ClaimRequestLog log = new ClaimRequestLog
            {
                SupportRequestId = request.supportRequestId,
                ClaimNumber = claimNumber,
                BenefitType = benType,
                EmployerNumber=emprNo,
                EmployeeNumber=employeeNumber,
                EmployeeName=employeeName,
                CreatedOn = (DateTime)request.createdOnToLocalTime,
                ApprovedOn = approvedOn,
                ApprovedBy = approvedBy,
                PostedOn = DateTime.Now
            };
            LogsDB logsDB = new LogsDB();
            await logsDB.InsertClaimLog(log);
            ////await UtilRecurrent.SendMail("jcsoto@nisgrenada.org", "DB2 Seeder Claim", $"<h1>New Claim Posted</h1>" +
            //     $"Please see Information below:</br></br>" +
            //     $"Employee Number: {log.EmployeeNumber}" +
            //     "</br ></br> " +
            //      $"Employee Name: {log.EmployeeName}" +
            //     "</br ></br>");            
        }
       
    }
}
