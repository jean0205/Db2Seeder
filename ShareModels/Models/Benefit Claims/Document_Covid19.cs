using Newtonsoft.Json;
using ShareModels.Helpers;
using ShareModels.Models.Sickness_Claim;
using System;
using System.Collections.Generic;

namespace ShareModels.Models.Benefit_Claims
{
    public class Document_Covid19
    {
        [JsonProperty("covidBenefitFormId")]
        public int? CovidBenefitFormId { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("nisNo")]
        public int NisNo { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("otherName")]
        public string OtherName { get; set; }

        [JsonProperty("gender")]
        public int? Gender { get; set; }

        [JsonProperty("maidenName")]
        public string MaidenName { get; set; }

        [JsonProperty("occupation")]
        public string Occupation { get; set; }

        [JsonProperty("aliases")]
        public string Aliases { get; set; }

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("maritalStatus")]
        public int? MaritalStatus { get; set; }

        [JsonProperty("tAddress")]
        public string TAddress { get; set; }

        [JsonProperty("workHistory")]
        public string WorkHistory { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("home")]
        public string Home { get; set; }

        [JsonProperty("work")]
        public string Work { get; set; }

        [JsonProperty("postalAddress")]
        public string PostalAddress { get; set; }

        [JsonProperty("bank")]
        public string Bank { get; set; }

        [JsonProperty("accountNo")]
        public string AccountNo { get; set; }

        [JsonProperty("accountName")]
        public string AccountName { get; set; }

        [JsonProperty("accountType")]
        public int? AccountType { get; set; }

        [JsonProperty("employerBusinessName")]
        public string EmployerBusinessName { get; set; }

        [JsonProperty("registrationNo")]
        public string RegistrationNo { get; set; }

        [JsonProperty("commenceEmployment")]
        public DateTime? CommenceEmployment { get; set; }

        [JsonProperty("lastWorkedDate")]
        public DateTime? LastWorkedDate { get; set; }

        [JsonProperty("employerPhone")]
        public string EmployerPhone { get; set; }

        [JsonProperty("employeePaidFull")]
        public int? EmployeePaidFull { get; set; }

        [JsonProperty("requiredReimburse")]
        public int? RequiredReimburse { get; set; }

        [JsonProperty("employerBank")]
        public string EmployerBank { get; set; }

        [JsonProperty("employerAccountNo")]
        public string EmployerAccountNo { get; set; }

        [JsonProperty("employerAccountName")]
        public string EmployerAccountName { get; set; }

        [JsonProperty("employerAccountType")]
        public int? EmployerAccountType { get; set; }

        [JsonProperty("chkQuarentine")]
        public int? ChkQuarentine { get; set; }

        [JsonProperty("chkIsolation")]
        public int? ChkIsolation { get; set; }

        [JsonProperty("chkCovid")]
        public int? ChkCovid { get; set; }

        [JsonProperty("lateReason")]
        public string LateReason { get; set; }

        [JsonProperty("employerNis")]
        public string EmployerNis { get; set; }

        [JsonProperty("medicalPracticeId")]
        public string MedicalPracticeId { get; set; }

        [JsonProperty("doctorId")]
        public string DoctorId { get; set; }

        [JsonProperty("employerData")]
        public string EmployerData { get; set; }

        [JsonProperty("workHistoryEntity")]
        public List<WorkHistoryEntityC> WorkHistoryEntity { get; set; }

        [JsonProperty("employerEntity")]
        public List<EmployerEntity> EmployerEntity { get; set; }

        [JsonProperty("documentId")]
        public int? DocumentId { get; set; }

        [JsonProperty("documentGuid")]
        public string DocumentGuid { get; set; }

        [JsonProperty("documentTypeId")]
        public int? DocumentTypeId { get; set; }

        [JsonProperty("documentCode")]
        public string DocumentCode { get; set; }

        [JsonProperty("name")]
        public object Name { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("createdBy")]
        public int? CreatedBy { get; set; }

        [JsonProperty("updatedBy")]
        public int? UpdatedBy { get; set; }

        [JsonProperty("checkInById")]
        public object CheckInById { get; set; }

        [JsonProperty("checkInAtTime")]
        public DateTime? CheckInAtTime { get; set; }

        [JsonProperty("checkInMessage")]
        public object CheckInMessage { get; set; }

        [JsonProperty("checkOutById")]
        public object CheckOutById { get; set; }

        [JsonProperty("checkOutTime")]
        public object CheckOutTime { get; set; }

        [JsonProperty("checkOutMessage")]
        public object CheckOutMessage { get; set; }

        [JsonProperty("checkOutExpiration")]
        public object CheckOutExpiration { get; set; }

        [JsonProperty("version")]
        public int? Version { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("updatedOn")]
        public DateTime UpdatedOn { get; set; }
        public string CompletedBy { get; set; }
        public DateTime CompletedTime { get; set; }
        public int SupportRequestId { get; set; }
        public string WebPortalLink => Settings.GetPortalUrl() + SupportRequestId;
        public int? ClaimNumber { get; set; }
    }
    public class WorkHistoryEntityC
    {
        [JsonProperty("nameEmployer")]
        public string NameEmployer { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("status")]
        public bool? Status { get; set; }
    }
}
