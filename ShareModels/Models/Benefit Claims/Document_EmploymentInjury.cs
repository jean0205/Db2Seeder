using Newtonsoft.Json;
using ShareModels.Helpers;
using System;
using System.Collections.Generic;

namespace ShareModels.Models.Benefit_Claims
{
    public class Document_EmploymentInjury
    {
        [JsonProperty("injuryBenefitFormId")]
        public int? InjuryBenefitFormId { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("nisNo")]
        public int NisNo { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("dateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

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

        [JsonProperty("bank")]
        public string Bank { get; set; }

        [JsonProperty("accountNo")]
        public string AccountNo { get; set; }

        [JsonProperty("accountName")]
        public string AccountName { get; set; }

        [JsonProperty("accountType")]
        public int? AccountType { get; set; }

        [JsonProperty("workHistoryList")]
        public string WorkHistoryList { get; set; }

        [JsonProperty("incapableDateFrom")]
        public DateTime? IncapableDateFrom { get; set; }

        [JsonProperty("incapableDateTo")]
        public DateTime? IncapableDateTo { get; set; }

        [JsonProperty("specifySickness")]
        public string SpecifySickness { get; set; }

        [JsonProperty("icdCode")]
        public string IcdCode { get; set; }

        [JsonProperty("medicalPractitionerName")]
        public string MedicalPractitionerName { get; set; }

        [JsonProperty("medicalRegistrationNo")]
        public string MedicalRegistrationNo { get; set; }

        [JsonProperty("employerName")]
        public string EmployerName { get; set; }

        [JsonProperty("registrationNo")]
        public string RegistrationNo { get; set; }

        [JsonProperty("employmentDateStart")]
        public DateTime? EmploymentDateStart { get; set; }

        [JsonProperty("employerNumber")]
        public string EmployerNumber { get; set; }

        [JsonProperty("lastWorkedDate")]
        public DateTime? LastWorkedDate { get; set; }

        [JsonProperty("paidInFull")]
        public int? PaidInFull { get; set; }

        [JsonProperty("formCompleteEmployee")]
        public int? FormCompleteEmployee { get; set; }

        [JsonProperty("formCompleteDoctor")]
        public int? FormCompleteDoctor { get; set; }

        [JsonProperty("formCompleteEmployer")]
        public int? FormCompleteEmployer { get; set; }

        [JsonProperty("employerNis")]
        public string EmployerNis { get; set; }

        [JsonProperty("medicalPracticeId")]
        public int? MedicalPracticeId { get; set; }

        [JsonProperty("doctorId")]
        public int? DoctorId { get; set; }

        
        public string lateReason { get; set; }
        public int? consent { get; set; }
        public DateTime? accidentDateTime { get; set; }
        public DateTime? reportDateTime { get; set; }
        public string location { get; set; }
        public string injurySustained { get; set; }
        public string witness { get; set; }
        public string incidentDetail { get; set; }
        public int? claimantRequiredInPlaceWork { get; set; }
        public string claimantNormalHours { get; set; }
        public string claimantNatureOfJob { get; set; }
        public int? reportedToSupervisorTimeOccured { get; set; }

        [JsonProperty("workHistoryEntity")]
        public List<WorkHistoryEntityE> WorkHistoryEntity { get; set; }

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
        public string Description { get; set; }

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
        public DateTime? CreatedOn { get; set; }

        [JsonProperty("updatedOn")]
        public DateTime? UpdatedOn { get; set; }
        public string CompletedBy { get; set; }
        public DateTime? CompletedTime { get; set; }
        public int SupportRequestId { get; set; }
        public string WebPortalLink => Settings.GetPortalUrl() + SupportRequestId;
        public int? ClaimNumber { get; set; }
    }
    public class WorkHistoryEntityE
    {
        [JsonProperty("nameEmployer")]
        public string NameEmployer { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("status")]
        public bool? Status { get; set; }
    }
}
