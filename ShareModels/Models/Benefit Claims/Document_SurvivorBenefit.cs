using Newtonsoft.Json;
using ShareModels.Helpers;
using System;
using System.Collections.Generic;

namespace ShareModels.Models.Benefit_Claims
{
    public class Document_SurvivorBenefit
    {
        [JsonProperty("survivorBenefitFormId")]
        public int? SurvivorBenefitFormId { get; set; }

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

        [JsonProperty("bank")]
        public string Bank { get; set; }

        [JsonProperty("accountNo")]
        public string AccountNo { get; set; }

        [JsonProperty("accountName")]
        public string AccountName { get; set; }

        [JsonProperty("accountType")]
        public int? AccountType { get; set; }

        [JsonProperty("chkbxSurvivor")]
        public int? ChkbxSurvivor { get; set; }

        [JsonProperty("chkbxSurvivors")]
        public int? ChkbxSurvivors { get; set; }

        [JsonProperty("chkbxSickness")]
        public int? ChkbxSickness { get; set; }

        [JsonProperty("chkbxNone")]
        public int? ChkbxNone { get; set; }

        [JsonProperty("providentFund")]
        public int? ProvidentFund { get; set; }

        [JsonProperty("providentFundList")]
        public string ProvidentFundList { get; set; }

        [JsonProperty("workHistoryList")]
        public string WorkHistoryList { get; set; }

        [JsonProperty("workOtherCountries")]
        public int? WorkOtherCountries { get; set; }

        [JsonProperty("workOtherCountryList")]
        public string WorkOtherCountryList { get; set; }

        [JsonProperty("residingMakingClaim")]
        public int? ResidingMakingClaim { get; set; }

        [JsonProperty("widowerMarried")]
        public int? WidowerMarried { get; set; }

        [JsonProperty("widowerMarriedAnyone")]
        public int? WidowerMarriedAnyone { get; set; }

        [JsonProperty("widowerRemarried")]
        public int? WidowerRemarried { get; set; }

        [JsonProperty("widowerChkbxAge")]
        public int? WidowerChkbxAge { get; set; }

        [JsonProperty("widowerChkbxInvalidity")]
        public int? WidowerChkbxInvalidity { get; set; }

        [JsonProperty("widowerChkbxSurvivors")]
        public int? WidowerChkbxSurvivors { get; set; }

        [JsonProperty("widowerChkbxNone")]
        public int? WidowerChkbxNone { get; set; }

        [JsonProperty("claimChildList")]
        public string ClaimChildList { get; set; }

        [JsonProperty("deathResidingMakingClaim")]
        public int? DeathResidingMakingClaim { get; set; }

        [JsonProperty("dependingLivingDeceasedYrs")]
        public int? DependingLivingDeceasedYrs { get; set; }

        [JsonProperty("dependingLivingDeceasedMonths")]
        public int? DependingLivingDeceasedMonths { get; set; }

        [JsonProperty("dependingMaintained")]
        public int? DependingMaintained { get; set; }

        [JsonProperty("dependingRemarried")]
        public int? DependingRemarried { get; set; }

        [JsonProperty("chkbxDependingAge")]
        public int? ChkbxDependingAge { get; set; }

        [JsonProperty("chkbxDependingInvalidity")]
        public int? ChkbxDependingInvalidity { get; set; }

        [JsonProperty("chkbxDependingSurvivors")]
        public int? ChkbxDependingSurvivors { get; set; }

        [JsonProperty("chkbxDependingNone")]
        public int? ChkbxDependingNone { get; set; }

        [JsonProperty("receivedInvalidityBenefit")]
        public int? ReceivedInvalidityBenefit { get; set; }

        [JsonProperty("chkbxAge")]
        public int? ChkbxAge { get; set; }

        [JsonProperty("chkbxEmploymentInjury")]
        public int? ChkbxEmploymentInjury { get; set; }

        [JsonProperty("chkbxDisablement")]
        public int? ChkbxDisablement { get; set; }

        [JsonProperty("dependentSurname")]
        public string DependentSurname { get; set; }

        [JsonProperty("dependentNisNo")]
        public int? DependentNisNo { get; set; }

        [JsonProperty("dependentFirstName")]
        public string DependentFirstName { get; set; }

        [JsonProperty("dependentDateOfBirth")]
        public DateTime? DependentDateOfBirth { get; set; }

        [JsonProperty("dependentOtherName")]
        public string DependentOtherName { get; set; }

        [JsonProperty("dependentGender")]
        public int? DependentGender { get; set; }

        [JsonProperty("dependentMaidenName")]
        public string DependentMaidenName { get; set; }

        [JsonProperty("dependentOccupation")]
        public string DependentOccupation { get; set; }

        [JsonProperty("dependentAliases")]
        public string DependentAliases { get; set; }

        [JsonProperty("dependentEmailAddress")]
        public string DependentEmailAddress { get; set; }

        [JsonProperty("dependentMaritalStatus")]
        public string DependentMaritalStatus { get; set; }

        [JsonProperty("dependentTAddress")]
        public string DependentTAddress { get; set; }

        [JsonProperty("dependentMobile")]
        public string DependentMobile { get; set; }

        [JsonProperty("dependentHome")]
        public string DependentHome { get; set; }

        [JsonProperty("dependentWork")]
        public string DependentWork { get; set; }

        [JsonProperty("dependentPostalAddress")]
        public string DependentPostalAddress { get; set; }

        [JsonProperty("dependentResidingMakingClaim")]
        public string DependentResidingMakingClaim { get; set; }

        [JsonProperty("dependentWidowerMarried")]
        public string DependentWidowerMarried { get; set; }

        [JsonProperty("dependingWidowLivingDeceasedYrs")]
        public int? DependingWidowLivingDeceasedYrs { get; set; }

        [JsonProperty("dependingWidowLivingDeceasedMonths")]
        public int? DependingWidowLivingDeceasedMonths { get; set; }

        [JsonProperty("dependentWidowerMarriedAnyone")]
        public int? DependentWidowerMarriedAnyone { get; set; }

        [JsonProperty("dependentWidowerRemarried")]
        public int? DependentWidowerRemarried { get; set; }

        [JsonProperty("dependentParentSurname")]
        public string DependentParentSurname { get; set; }

        [JsonProperty("dependentParentNisNo")]
        public int? DependentParentNisNo { get; set; }

        [JsonProperty("dependentParentFirstName")]
        public string DependentParentFirstName { get; set; }

        [JsonProperty("dependentParentDateOfBirth")]
        public DateTime? DependentParentDateOfBirth { get; set; }

        [JsonProperty("dependentParentOtherName")]
        public string DependentParentOtherName { get; set; }

        [JsonProperty("dependentParentGender")]
        public int? DependentParentGender { get; set; }

        [JsonProperty("dependentParentMaidenName")]
        public string DependentParentMaidenName { get; set; }

        [JsonProperty("dependentParentOccupation")]
        public string DependentParentOccupation { get; set; }

        [JsonProperty("dependentParentAliases")]
        public string DependentParentAliases { get; set; }

        [JsonProperty("dependentParentEmailAddress")]
        public string DependentParentEmailAddress { get; set; }

        [JsonProperty("dependentParentMaritalStatus")]
        public int? DependentParentMaritalStatus { get; set; }

        [JsonProperty("dependentParentTAddress")]
        public string DependentParentTAddress { get; set; }

        [JsonProperty("dependentParentMobile")]
        public string DependentParentMobile { get; set; }

        [JsonProperty("dependentParentHome")]
        public string DependentParentHome { get; set; }

        [JsonProperty("dependentParentWork")]
        public string DependentParentWork { get; set; }

        [JsonProperty("dependentParentPostalAddress")]
        public string DependentParentPostalAddress { get; set; }

        [JsonProperty("applicableSurname")]
        public string ApplicableSurname { get; set; }

        [JsonProperty("applicableNisNo")]
        public string ApplicableNisNo { get; set; }

        [JsonProperty("applicableFirstName")]
        public string ApplicableFirstName { get; set; }

        [JsonProperty("applicableDateOfBirth")]
        public DateTime? ApplicableDateOfBirth { get; set; }

        [JsonProperty("applicableOtherName")]
        public string ApplicableOtherName { get; set; }

        [JsonProperty("applicableGender")]
        public int? ApplicableGender { get; set; }

        [JsonProperty("applicableMaidenName")]
        public string ApplicableMaidenName { get; set; }

        [JsonProperty("applicableOccupation")]
        public string ApplicableOccupation { get; set; }

        [JsonProperty("applicableAliases")]
        public string ApplicableAliases { get; set; }

        [JsonProperty("applicableEmailAddress")]
        public string ApplicableEmailAddress { get; set; }

        [JsonProperty("applicableMaritalStatus")]
        public string ApplicableMaritalStatus { get; set; }

        [JsonProperty("applicableTAddress")]
        public string ApplicableTAddress { get; set; }

        [JsonProperty("applicableMobile")]
        public string ApplicableMobile { get; set; }

        [JsonProperty("applicableHome")]
        public string ApplicableHome { get; set; }

        [JsonProperty("applicableWork")]
        public string ApplicableWork { get; set; }

        [JsonProperty("applicablePostalAddress")]
        public string ApplicablePostalAddress { get; set; }

        [JsonProperty("lateReason")]
        public string LateReason { get; set; }

        [JsonProperty("injuryRelatedDeath")]
        public int? InjuryRelatedDeath { get; set; }

        [JsonProperty("injuryRelatedDeathDateOfAccident")]
        public DateTime? InjuryRelatedDeathDateOfAccident { get; set; }

        [JsonProperty("injuryRelatedDeathTimeOfAccident")]
        public string InjuryRelatedDeathTimeOfAccident { get; set; }

        [JsonProperty("injuryRelatedDeathLocation")]
        public string InjuryRelatedDeathLocation { get; set; }

        [JsonProperty("injuryRelatedDeathWitness")]
        public string InjuryRelatedDeathWitness { get; set; }

        [JsonProperty("injuryRelatedDeathDetailsOfAccident")]
        public string InjuryRelatedDeathDetailsOfAccident { get; set; }

        [JsonProperty("onBehalfOfSomeone")]
        public int? OnBehalfOfSomeone { get; set; }

        [JsonProperty("onBehalfOfSomeoneFullName")]
        public string OnBehalfOfSomeoneFullName { get; set; }

        [JsonProperty("onBehalfOfSomeonePhoneNumber")]
        public string OnBehalfOfSomeonePhoneNumber { get; set; }

        [JsonProperty("onBehalfOfSomeoneEmailAddress")]
        public string OnBehalfOfSomeoneEmailAddress { get; set; }

        [JsonProperty("onBehalfOfSomeoneRelationToClaimant")]
        public string OnBehalfOfSomeoneRelationToClaimant { get; set; }

        [JsonProperty("childInvalidMentally")]
        public int? ChildInvalidMentally { get; set; }

        [JsonProperty("fullOrphan")]
        public int? FullOrphan { get; set; }

        [JsonProperty("fullOrphanOtherDeceasedParentName")]
        public string FullOrphanOtherDeceasedParentName { get; set; }

        [JsonProperty("fullOrphanOtherDeceasedParentNisNo")]
        public string FullOrphanOtherDeceasedParentNisNo { get; set; }

        [JsonProperty("madeForChild")]
        public int? MadeForChild { get; set; }

        [JsonProperty("madeForChildFullName")]
        public string MadeForChildFullName { get; set; }

        [JsonProperty("madeForChildDateOfBirth")]
        public DateTime MadeForChildDateOfBirth { get; set; }

        [JsonProperty("madeForChildSchoolAttending")]
        public string MadeForChildSchoolAttending { get; set; }

        [JsonProperty("madeForChildOtherEligibleChildren")]
        public string MadeForChildOtherEligibleChildren { get; set; }

        [JsonProperty("workHistoryEntity")]
        public List<WorkHistoryEntityS> WorkHistoryEntity { get; set; }

        [JsonProperty("providentFundEntity")]
        public List<ProvidentFundEntityS> ProvidentFundEntity { get; set; }

        [JsonProperty("workOtherCountryEntity")]
        public List<WorkOtherCountryEntity> WorkOtherCountryEntity { get; set; }

        [JsonProperty("claimChildEntity")]
        public List<ClaimChildEntity> ClaimChildEntity { get; set; }

        [JsonProperty("documentId")]
        public int? DocumentId { get; set; }

        [JsonProperty("documentGuid")]
        public string DocumentGuid { get; set; }

        [JsonProperty("documentTypeId")]
        public int? DocumentTypeId { get; set; }

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
        public string CheckInById { get; set; }

        [JsonProperty("checkInAtTime")]
        public DateTime? CheckInAtTime { get; set; }

        [JsonProperty("checkInMessage")]
        public string CheckInMessage { get; set; }

        [JsonProperty("checkOutById")]
        public string CheckOutById { get; set; }

        [JsonProperty("checkOutTime")]
        public string CheckOutTime { get; set; }

        [JsonProperty("checkOutMessage")]
        public string CheckOutMessage { get; set; }

        [JsonProperty("checkOutExpiration")]
        public string CheckOutExpiration { get; set; }

        [JsonProperty("version")]
        public int? Version { get; set; }

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
    public class WorkHistoryEntityS
    {
        [JsonProperty("nameEmployer")]
        public string NameEmployer { get; set; }

        [JsonProperty("periodWorked")]
        public string PeriodWorked { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }
    }

    public class ProvidentFundEntityS
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

    public class ClaimChildEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("dob")]
        public string Dob { get; set; }

        [JsonProperty("school")]
        public string School { get; set; }
    }

}
