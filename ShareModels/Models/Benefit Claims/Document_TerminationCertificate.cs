using ShareModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models.Benefit_Claims
{
    public class Document_TerminationCertificate
    {
        public int certificateTerminationLayoffFormId { get; set; }
        public string surname { get; set; }
        public string nisNo { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string emailAddress { get; set; }
        public string home { get; set; }
        public string mobile { get; set; }
        public string postalAddress { get; set; }
        public string parish { get; set; }
        public string employerName { get; set; }
        public string employerNisNo { get; set; }
        public string employerAddress { get; set; }
        public string employerParish { get; set; }
        public string employerPostalCode { get; set; }
        public string employerTelNo { get; set; }
        public string employerFaxNo { get; set; }
        public string occupation { get; set; }
        public string positionOrg { get; set; }
        public DateTime? commencedDate { get; set; }
        public DateTime? lastDateWorked { get; set; }
        public DateTime? terminationDate { get; set; }
        public DateTime? lastPaidDate { get; set; }
        public string terminationReason { get; set; }
        public int? payInLieuWeekPaid { get; set; }
        public int? severanceWeekPaid { get; set; }
        public int? vacationWeekPaid { get; set; }
        public int? dismissedDueToMisconduct { get; set; }
        public string misconductReason { get; set; }
        public int? voluntariyLeave { get; set; }
        public string declarationName { get; set; }
        public string declarationPosition { get; set; }
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
//22-264