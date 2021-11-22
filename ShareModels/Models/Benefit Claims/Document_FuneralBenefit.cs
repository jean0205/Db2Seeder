using Newtonsoft.Json;
using ShareModels.Helpers;
using System;
using System.Collections.Generic;

namespace ShareModels.Models.Benefit_Claims
{
    public class Document_FuneralBenefit
    {
        [JsonProperty("funeralGrantBenefitFormId")]
        public int FuneralGrantBenefitFormId;

        [JsonProperty("surname")]
        public string Surname;

        [JsonProperty("nisNo")]
        public int NisNo;

        [JsonProperty("firstName")]
        public string FirstName;

        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth;

        [JsonProperty("otherName")]
        public string OtherName;

        [JsonProperty("gender")]
        public int Gender;

        [JsonProperty("maidenName")]
        public string MaidenName;

        [JsonProperty("occupation")]
        public string Occupation;

        [JsonProperty("aliases")]
        public string Aliases;

        [JsonProperty("emailAddress")]
        public string EmailAddress;

        [JsonProperty("maritalStatus")]
        public int MaritalStatus;

        [JsonProperty("tAddress")]
        public string TAddress;

        [JsonProperty("employersWorkedWith")]
        public string EmployersWorkedWith;

        [JsonProperty("workHistory")]
        public string WorkHistory;

        [JsonProperty("claimantSurname")]
        public string ClaimantSurname;

        [JsonProperty("claimantNisNo")]
        public string ClaimantNisNo;

        [JsonProperty("claimantFirstName")]
        public string ClaimantFirstName;

        [JsonProperty("claimantDateOfBirth")]
        public DateTime ClaimantDateOfBirth;

        [JsonProperty("claimantOtherName")]
        public string ClaimantOtherName;

        [JsonProperty("relationshipToDeceased")]
        public string RelationshipToDeceased;

        [JsonProperty("claimantEmailAddress")]
        public string ClaimantEmailAddress;

        [JsonProperty("claimantTAddress")]
        public string ClaimantTAddress;

        [JsonProperty("mobile")]
        public string Mobile;

        [JsonProperty("home")]
        public string Home;

        [JsonProperty("work")]
        public string Work;

        [JsonProperty("postalAddress")]
        public string PostalAddress;

        [JsonProperty("bank")]
        public string Bank;

        [JsonProperty("accountNo")]
        public string AccountNo;

        [JsonProperty("accountName")]
        public string AccountName;

        [JsonProperty("accountType")]
        public int AccountType;

        [JsonProperty("workHistoryEntity")]
        public List<WorkHistoryEntity1> WorkHistoryEntity;

        [JsonProperty("providentFundEntity")]
        public List<ProvidentFundEntity1> ProvidentFundEntity;

        [JsonProperty("documentId")]
        public int DocumentId;

        [JsonProperty("documentGuid")]
        public string DocumentGuid;

        [JsonProperty("documentTypeId")]
        public int DocumentTypeId;

        [JsonProperty("documentCode")]
        public string DocumentCode;

        [JsonProperty("name")]
        public object Name;

        [JsonProperty("description")]
        public object Description;

        [JsonProperty("createdBy")]
        public int CreatedBy;

        [JsonProperty("updatedBy")]
        public int UpdatedBy;

        [JsonProperty("checkInById")]
        public object CheckInById;

        [JsonProperty("checkInAtTime")]
        public DateTime CheckInAtTime;

        [JsonProperty("checkInMessage")]
        public object CheckInMessage;

        [JsonProperty("checkOutById")]
        public object CheckOutById;

        [JsonProperty("checkOutTime")]
        public object CheckOutTime;

        [JsonProperty("checkOutMessage")]
        public object CheckOutMessage;

        [JsonProperty("checkOutExpiration")]
        public object CheckOutExpiration;

        [JsonProperty("version")]
        public int Version;

        [JsonProperty("createdOn")]
        public DateTime CreatedOn;

        [JsonProperty("updatedOn")]
        public DateTime UpdatedOn;
        public string CompletedBy { get; set; }
        public DateTime? CompletedTime { get; set; }
        public int SupportRequestId { get; set; }
        public string WebPortalLink => Settings.GetPortalUrl() + SupportRequestId;
        public int? ClaimNumber { get; set; }
    }
    public class WorkHistoryEntity1
    {
        [JsonProperty("nameEmployer")]
        public string NameEmployer;

        [JsonProperty("periodWorked")]
        public string PeriodWorked;

        [JsonProperty("status")]
        public bool Status;
    }

    public class ProvidentFundEntity1
    {
        [JsonProperty("state")]
        public string State;

        [JsonProperty("address")]
        public string Address;

        [JsonProperty("periodWorked")]
        public string PeriodWorked;

        [JsonProperty("supervisor")]
        public string Supervisor;

        [JsonProperty("status")]
        public bool Status;
    }
}
