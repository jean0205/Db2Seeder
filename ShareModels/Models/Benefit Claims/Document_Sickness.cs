using ShareModels.Helpers;
using System;
using System.Collections.Generic;

namespace ShareModels.Models.Sickness_Claim
{
    public class Document_Sickness
    {
        public int sicknessBenefitFormId { get; set; }
        public string surname { get; set; }
        public int nisNo { get; set; }
        public string firstName { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public string otherName { get; set; }
        public int? gender { get; set; }
        public string maidenName { get; set; }
        public string occupation { get; set; }
        public string aliases { get; set; }
        public string emailAddress { get; set; }
        public int? maritalStatus { get; set; }
        public string tAddress { get; set; }
        public string workHistory { get; set; }
        public string mobile { get; set; }
        public string home { get; set; }
        public string work { get; set; }
        public string postalAddress { get; set; }
        public string bank { get; set; }
        public string accountNo { get; set; }
        public string accountName { get; set; }
        public string employerName { get; set; }
        public string registrationNo { get; set; }
        public DateTime? employmentDateStart { get; set; }
        public string employerNumber { get; set; }
        public DateTime? lastWorkedDate { get; set; }
        public int? paidInFull { get; set; }
        public int? requiredReimburse { get; set; }
        public string employerBank { get; set; }
        public string employerAccountNo { get; set; }
        public string employerAccountName { get; set; }
        public int? employerAccountType { get; set; }
        public int? accountType { get; set; }
        public int? natureInjury { get; set; }
        public DateTime? incapableDateFrom { get; set; }
        public DateTime? incapableDateTo { get; set; }
        public string specifySickness { get; set; }
        public string icdCode { get; set; }
        public string medicalPractitionerName { get; set; }
        public string medicalRegistrationNo { get; set; }
        public string lateReason { get; set; }
        public int? formCompleteEmployee { get; set; }
        public int? formCompleteDoctor { get; set; }
        public int? formCompleteEmployer { get; set; }
        public string employerNis { get; set; }
        public int? medicalPracticeId { get; set; }
        public int? doctorId { get; set; }
        public string employerData { get; set; }
        public DateTime? claimDate { get; set; }
        public string claimVariationReason { get; set; }
        public int? consent { get; set; }
        public int? consentClaim { get; set; }
        public string gapReason { get; set; }
        public List<object> workHistoryEntity { get; set; }
        public List<EmployerEntity> employerEntity { get; set; }
        public int? documentId { get; set; }
        public string documentGuid { get; set; }
        public int? documentTypeId { get; set; }
        public string documentCode { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? createdBy { get; set; }
        public int? updatedBy { get; set; }
        public int? checkInById { get; set; }
        public DateTime? checkInAtTime { get; set; }
        public string checkInMessage { get; set; }
        public int? checkOutById { get; set; }
        public DateTime? checkOutTime { get; set; }
        public string checkOutMessage { get; set; }
        public object checkOutExpiration { get; set; }
        public int? version { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime? updatedOn { get; set; }
        public string CompletedBy { get; set; }
        public DateTime CompletedTime { get; set; }
        public int SupportRequestId { get; set; }
        public string WebPortalLink => Settings.GetPortalUrl() + SupportRequestId;
        public int? ClaimNumber { get; set; }
    }
    public class EmployerEntity
    {
        public string employerNis { get; set; }
        public DateTime? employmentDateStart { get; set; }
        public string employerNumber { get; set; }
        public DateTime? lastWorkedDate { get; set; }
        public int? paidInFull { get; set; }
        public int? requiredReimburse { get; set; }
        public string employerBank { get; set; }
        public string employerAccountNo { get; set; }
        public string employerAccountName { get; set; }
        public int? employerAccountType { get; set; }
        public string employerName { get; set; }
        public string registrationNo { get; set; }
    }
}
