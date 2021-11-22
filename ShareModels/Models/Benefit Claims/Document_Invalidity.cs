using Newtonsoft.Json;
using ShareModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models.Benefit_Claims
{
    public class Document_Invalidity
    {
        [JsonProperty("invalidityBenefitFormId")]
        public int InvalidityBenefitFormId { get; set; }

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
        public int Gender { get; set; }

        [JsonProperty("maidenName")]
        public string MaidenName { get; set; }

        [JsonProperty("occupation")]
        public object Occupation { get; set; }

        [JsonProperty("aliases")]
        public string Aliases { get; set; }

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("maritalStatus")]
        public int MaritalStatus { get; set; }

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
        public int AccountType { get; set; }

        [JsonProperty("chkbxAge")]
        public int ChkbxAge { get; set; }

        [JsonProperty("chkbxInvalidity")]
        public object ChkbxInvalidity { get; set; }

        [JsonProperty("chkbxSurvivors")]
        public object ChkbxSurvivors { get; set; }

        [JsonProperty("chkbxSickness")]
        public object ChkbxSickness { get; set; }

        [JsonProperty("chkbxEmploymentInjury")]
        public int ChkbxEmploymentInjury { get; set; }

        [JsonProperty("chkbxDisablement")]
        public object ChkbxDisablement { get; set; }

        [JsonProperty("chkbxNone")]
        public object ChkbxNone { get; set; }

        [JsonProperty("providentFund")]
        public int ProvidentFund { get; set; }

        [JsonProperty("receivedInvalidityBenefit")]
        public int ReceivedInvalidityBenefit { get; set; }

        [JsonProperty("providentFundList")]
        public string ProvidentFundList { get; set; }

        [JsonProperty("workHistoryList")]
        public string WorkHistoryList { get; set; }

        [JsonProperty("workOtherCountries")]
        public int WorkOtherCountries { get; set; }

        [JsonProperty("workOtherCountryList")]
        public string WorkOtherCountryList { get; set; }

        [JsonProperty("periodStart")]
        public object PeriodStart { get; set; }

        [JsonProperty("periodEnd")]
        public object PeriodEnd { get; set; }

        [JsonProperty("findings")]
        public object Findings { get; set; }

        [JsonProperty("medicalSurname")]
        public object MedicalSurname { get; set; }

        [JsonProperty("medicalName")]
        public object MedicalName { get; set; }

        [JsonProperty("medicalPhone")]
        public object MedicalPhone { get; set; }

        [JsonProperty("medicalRegistrationNo")]
        public object MedicalRegistrationNo { get; set; }

        [JsonProperty("medicalAddress")]
        public object MedicalAddress { get; set; }

        [JsonProperty("medicalPracticeId")]
        public object MedicalPracticeId { get; set; }

        [JsonProperty("doctorId")]
        public object DoctorId { get; set; }

        [JsonProperty("workHistoryEntity")]
        public List<WorkHistoryEntity2> WorkHistoryEntity { get; set; }

        [JsonProperty("providentFundEntity")]
        public List<ProvidentFundEntity2> ProvidentFundEntity { get; set; }

        [JsonProperty("workOtherCountryEntity")]
        public List<WorkOtherCountryEntity2> WorkOtherCountryEntity { get; set; }

        [JsonProperty("documentId")]
        public int DocumentId { get; set; }

        [JsonProperty("documentGuid")]
        public string DocumentGuid { get; set; }

        [JsonProperty("documentTypeId")]
        public int DocumentTypeId { get; set; }

        [JsonProperty("documentCode")]
        public string DocumentCode { get; set; }

        [JsonProperty("name")]
        public object Name { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("createdBy")]
        public int CreatedBy { get; set; }

        [JsonProperty("updatedBy")]
        public int UpdatedBy { get; set; }

        [JsonProperty("checkInById")]
        public object CheckInById { get; set; }

        [JsonProperty("checkInAtTime")]
        public DateTime CheckInAtTime { get; set; }

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
        public int Version { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("updatedOn")]
        public DateTime UpdatedOn { get; set; }
        public string CompletedBy { get; set; }
        public DateTime? CompletedTime { get; set; }
        public int SupportRequestId { get; set; }
        public string WebPortalLink => Settings.GetPortalUrl() + SupportRequestId;
        public int? ClaimNumber { get; set; }
    }
    public class WorkHistoryEntity2
    {
        [JsonProperty("nameEmployer")]
        public string NameEmployer { get; set; }

        [JsonProperty("periodWorked")]
        public string PeriodWorked { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }
    }

    public class ProvidentFundEntity2
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("periodWorked")]
        public string PeriodWorked { get; set; }

        [JsonProperty("supervisor")]
        public string Supervisor { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }
    }

    public class WorkOtherCountryEntity2
    {
        [JsonProperty("nameCountry")]
        public string NameCountry { get; set; }

        [JsonProperty("othernis")]
        public string Othernis { get; set; }

        [JsonProperty("periodWorked")]
        public string PeriodWorked { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }
    }

}
