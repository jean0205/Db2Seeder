using System;
using System.Collections.Generic;

namespace ShareModels.Models
{

    public class Document_Remittance
    {
        public int remittanceFormId { get; set; }
        public string employerNumber { get; set; }
        public string employerName { get; set; }
        public List<EmployeeContributionRecord> employeeContributionRecords { get; set; }
        public int documentId { get; set; }
        public string documentGuid { get; set; }
        public int documentTypeId { get; set; }
        public string documentCode { get; set; }
        public object name { get; set; }
        public object description { get; set; }
        public object createdBy { get; set; }
        public object updatedBy { get; set; }
        public object checkInById { get; set; }
        public object checkInAtTime { get; set; }
        public object checkInMessage { get; set; }
        public object checkOutById { get; set; }
        public object checkOutTime { get; set; }
        public object checkOutMessage { get; set; }
        public object checkOutExpiration { get; set; }
        public object version { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }
    }
    public class EmployeeContributionRecord
    {
        public int rowNumber { get; set; }
        public string employeeNumber { get; set; }
        public string employeeName { get; set; }
        public int contributionPeriodMonth { get; set; }
        public int contributionPeriodYear { get; set; }
        public string frequency { get; set; }
        public int weeksWorked { get; set; }
        public double insurableEarnings { get; set; }
        public Contributions contributions { get; set; }
        public double expectedContributionPercentage { get; set; }
        public Week1 week1 { get; set; }
        public Week2 week2 { get; set; }
        public Week3 week3 { get; set; }
        public Week4 week4 { get; set; }
        public Week5 week5 { get; set; }
    }
    public class Week1
    {
        public bool? hasWorked { get; set; }
        public double? amount { get; set; }
    }

    public class Week2
    {
        public bool? hasWorked { get; set; }
        public double? amount { get; set; }
    }

    public class Week3
    {
        public bool? hasWorked { get; set; }
        public double? amount { get; set; }
    }

    public class Week4
    {
        public bool? hasWorked { get; set; }
        public double? amount { get; set; }
    }

    public class Week5
    {
        public bool? hasWorked { get; set; }
        public double? amount { get; set; }
    }
    public class Contributions
    {
        public double employerPortion { get; set; }
        public double employeePortion { get; set; }
        public double total { get; set; }
    }
}

