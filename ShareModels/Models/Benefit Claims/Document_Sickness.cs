using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models.Sickness_Claim
{
    public class Document_Sickness
    {
        public int sicknessBenefitFormId { get; set; }
        public string surname { get; set; }
        public int nisNo { get; set; }
        public string firstName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string otherName { get; set; }
        public int gender { get; set; }
        public string maidenName { get; set; }
        public string occupation { get; set; }
        public string aliases { get; set; }
        public string emailAddress { get; set; }
        public int maritalStatus { get; set; }
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
        public object paidInFull { get; set; }
        public object requiredReimburse { get; set; }
        public object employerBank { get; set; }
        public object employerAccountNo { get; set; }
        public object employerAccountName { get; set; }
        public object employerAccountType { get; set; }
        public int accountType { get; set; }
        public object natureInjury { get; set; }
        public object incapableDateFrom { get; set; }
        public object incapableDateTo { get; set; }
        public object specifySickness { get; set; }
        public object icdCode { get; set; }
        public object medicalPractitionerName { get; set; }
        public object medicalRegistrationNo { get; set; }
        public string lateReason { get; set; }
        public int formCompleteEmployee { get; set; }
        public object formCompleteDoctor { get; set; }
        public object formCompleteEmployer { get; set; }
        public string employerNis { get; set; }
        public int medicalPracticeId { get; set; }
        public int doctorId { get; set; }
        public string employerData { get; set; }
        public List<object> workHistoryEntity { get; set; }
        public List<EmployerEntity> employerEntity { get; set; }
        public int documentId { get; set; }
        public string documentGuid { get; set; }
        public int documentTypeId { get; set; }
        public string documentCode { get; set; }
        public object name { get; set; }
        public object description { get; set; }
        public object createdBy { get; set; }
        public object updatedBy { get; set; }
        public object checkInById { get; set; }
        public DateTime checkInAtTime { get; set; }
        public object checkInMessage { get; set; }
        public object checkOutById { get; set; }
        public object checkOutTime { get; set; }
        public object checkOutMessage { get; set; }
        public object checkOutExpiration { get; set; }
        public int version { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }
        public string CompletedBy { get; set; }
        public DateTime? CompletedTime { get; set; }
        public int SupportRequestId { get; set; }
        public string WebPortalLink => $"http://my-nis-uat.loteklabs.com/SupportRequest/Detail/" + SupportRequestId;
        public int? ClaimNumber { get; set; }
    }
    public class EmployerEntity
    {
        public string employerNis { get; set; }
        public object employmentDateStart { get; set; }
        public object employerNumber { get; set; }
        public object lastWorkedDate { get; set; }
        public object paidInFull { get; set; }
        public object requiredReimburse { get; set; }
        public object employerBank { get; set; }
        public object employerAccountNo { get; set; }
        public object employerAccountName { get; set; }
        public object employerAccountType { get; set; }
        public object employerName { get; set; }
        public string registrationNo { get; set; }
    }
}
