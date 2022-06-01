using Newtonsoft.Json;
using ShareModels.Helpers;
using System;
using System.Collections.Generic;

namespace ShareModels.Models.Benefit_Claims
{
    public class Document_Maternity
    {
        [JsonProperty("maternityBenefitFormId")]
        public int MaternityBenefitFormId { get; set; }

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

        [JsonProperty("home")]
        public string Home { get; set; }

        [JsonProperty("work")]
        public string Work { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("postalAddress")]
        public string PostalAddress { get; set; }

        [JsonProperty("employersWorkedWith")]
        public string EmployersWorkedWith { get; set; }

        [JsonProperty("bank")]
        public string Bank { get; set; }

        [JsonProperty("accountNo")]
        public string AccountNo { get; set; }

        [JsonProperty("accountName")]
        public string AccountName { get; set; }

        [JsonProperty("accountType")]
        public int? AccountType { get; set; }

        [JsonProperty("benefitApply")]
        public string BenefitApply { get; set; }

        [JsonProperty("maternityDateFrom")]
        public DateTime? MaternityDateFrom { get; set; }

        [JsonProperty("maternityDateTo")]
        public DateTime? MaternityDateTo { get; set; }

        [JsonProperty("dateOfEmployment")]
        public DateTime? DateOfEmployment { get; set; }

        [JsonProperty("dateLastWorked")]
        public DateTime DateLastWorked { get; set; }

        [JsonProperty("employersBusinessNameSecTwo")]
        public string EmployersBusinessNameSecTwo { get; set; }

        [JsonProperty("employerNis")]
        public string EmployerNis { get; set; }

        [JsonProperty("businessAddressSecTwo")]
        public string BusinessAddressSecTwo { get; set; }

        [JsonProperty("expectDeliver")]
        public DateTime? ExpectDeliver { get; set; }

        [JsonProperty("expectDelivered")]
        public DateTime? ExpectDelivered { get; set; }

        [JsonProperty("doctorMidwifeName")]
        public string DoctorMidwifeName { get; set; }

        [JsonProperty("registrationNo")]
        public string RegistrationNo { get; set; }

        [JsonProperty("medicalPracticeId")]
        public int? MedicalPracticeId { get; set; }

        [JsonProperty("doctorId")]
        public int? DoctorId { get; set; }
        public string lateReason { get; set; }

        [JsonProperty("workHistoryEntity")]
        public List<WorkHistoryEntityM> WorkHistoryEntity { get; set; }

        [JsonProperty("documentId")]
        public int DocumentId { get; set; }

        [JsonProperty("documentGuid")]
        public string DocumentGuid { get; set; }

        [JsonProperty("documentTypeId")]
        public int DocumentTypeId { get; set; }

        [JsonProperty("documentCode")]
        public string DocumentCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("createdBy")]
        public int? CreatedBy { get; set; }

        [JsonProperty("updatedBy")]
        public int? UpdatedBy { get; set; }

        [JsonProperty("checkInById")]
        public int? CheckInById { get; set; }

        [JsonProperty("checkInAtTime")]
        public DateTime? CheckInAtTime { get; set; }

        [JsonProperty("checkInMessage")]
        public string CheckInMessage { get; set; }

        [JsonProperty("checkOutById")]
        public int? CheckOutById { get; set; }

        [JsonProperty("checkOutTime")]
        public DateTime? CheckOutTime { get; set; }

        [JsonProperty("checkOutMessage")]
        public string CheckOutMessage { get; set; }

        [JsonProperty("checkOutExpiration")]
        public object CheckOutExpiration { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("updatedOn")]
        public DateTime? UpdatedOn { get; set; }
        public string CompletedBy { get; set; }
        public DateTime? CompletedTime { get; set; }
        public int SupportRequestId { get; set; }
        public string WebPortalLink => Settings.GetPortalUrl() + SupportRequestId;
        public int? ClaimNumber { get; set; }


    }
    public class WorkHistoryEntityM
    {
        [JsonProperty("nameEmployer")]
        public string NameEmployer { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }
    }
}
