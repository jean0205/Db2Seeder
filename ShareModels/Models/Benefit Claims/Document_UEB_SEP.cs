using ShareModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models.Benefit_Claims
{
    public class Document_UEB_SEP
    {
        public int unemploymentSelfEmployedClaimFormId { get; set; }
        public string surname { get; set; }
        public string nisNo { get; set; }
        public string firstName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string otherName { get; set; }
        public int gender { get; set; }
        public string maidenName { get; set; }
        public string occupation { get; set; }
        public string emailAddress { get; set; }
        public string tAddress { get; set; }
        public string home { get; set; }
        public string work { get; set; }
        public string mobile { get; set; }
        public string postalAddress { get; set; }
        public object businessList { get; set; }
        public object businessNisNo { get; set; }
        public string businessName { get; set; }
        public string businessAddress { get; set; }
        public string businessNature { get; set; }
        public string bank { get; set; }
        public string accountNo { get; set; }
        public string accountName { get; set; }
        public int? accountType { get; set; }
        public int accountSameName { get; set; }
        public int? chkbxSickness { get; set; }
        public int? chkbxInvalidity { get; set; }
        public int? chkbxMaternity { get; set; }
        public int? chkbxInjury { get; set; }
        public int? chkbxNone { get; set; }
        public int contributedAsEmployedPerson { get; set; }
        public int stillSelfEmployed { get; set; }
        public int selfEmployedType { get; set; }
        public int engagedtherEmployment { get; set; }
        public string workHistoryList { get; set; }
        public DateTime? ceasedSelfEmploymentDate { get; set; }
        public string reasonCeased { get; set; }
        public string expectedPeriodLength { get; set; }
        public int soldBusinessShare { get; set; }
        public int willingFormalEmployment { get; set; }
        public string lateReason { get; set; }
        public List<object> workHistoryEntity { get; set; }
        public int documentId { get; set; }
        public string documentGuid { get; set; }
        public int documentTypeId { get; set; }
        public string documentCode { get; set; }
        public object name { get; set; }
        public object description { get; set; }
        public int? createdBy { get; set; }
        public int? updatedBy { get; set; }
        public int? checkInById { get; set; }
        public DateTime? checkInAtTime { get; set; }
        public string checkInMessage { get; set; }
        public string checkOutById { get; set; }
        public string checkOutTime { get; set; }
        public string checkOutMessage { get; set; }
        public string checkOutExpiration { get; set; }
        public int? version { get; set; }
        public DateTime? createdOn { get; set; }
        public DateTime? updatedOn { get; set; }

        ///////
        public string CompletedBy { get; set; }
        public DateTime CompletedTime { get; set; }
        public int SupportRequestId { get; set; }
        public string WebPortalLink => Settings.GetPortalUrl() + SupportRequestId;
        public int? ClaimNumber { get; set; }
        public DateTime readyTime { get; set; }
    }
}
