using System;

namespace ShareModels.Models
{
    public class Document_Employee
    {
        public int employeeRegistrationFormId { get; set; }
        public int registrationType { get; set; }
        public int? nisNo { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; } = String.Empty;
        public string lastName { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public int sex { get; set; }
        public int maritalStatus { get; set; }
        public DateTime? dateOfMarriage { get; set; }
        public string address { get; set; }
        public string town { get; set; }
        public string parish { get; set; }
        public string nationality { get; set; }
        public string emailAddress { get; set; }
        public string primaryMobileNumber { get; set; }
        public string secondaryMobileNumber { get; set; }
        public string homePhoneNumber { get; set; }
        public string businessAddress { get; set; }
        public string businessTown { get; set; }
        public string businessParish { get; set; }
        public string businessMailingAddress { get; set; }
        public string businessMobile { get; set; }
        public string businessPhoneNumber { get; set; }
        public string businessFax { get; set; }
        public string businessEmailAddress { get; set; }
        public DateTime? businessStartDate { get; set; }
        public string businessNature { get; set; }
        public decimal? monthlyIncome { get; set; }
        public decimal? previousMonthlyIncome { get; set; }
        public string lastEmployer { get; set; }
        public DateTime? dateEmployedEnded { get; set; }
        public string bank { get; set; }
        public string accountNumber { get; set; }
        public string accountType { get; set; }
        public int documentId { get; set; }
        public Guid documentGuid { get; set; }
        public int documentTypeId { get; set; }
        public string documentCode { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public object checkInById { get; set; }
        public DateTime? checkInAtTime { get; set; }
        public object checkInMessage { get; set; }
        public object checkOutById { get; set; }
        public DateTime? checkOutTime { get; set; }
        public object checkOutMessage { get; set; }
        public object checkOutExpiration { get; set; }
        public int version { get; set; }
        public DateTime? createdOn { get; set; }
        public DateTime? updatedOn { get; set; }


    }
}
