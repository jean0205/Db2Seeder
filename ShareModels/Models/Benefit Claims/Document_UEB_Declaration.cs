using ShareModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models.Benefit_Claims
{
    public class Document_UEB_Declaration
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
       
            public int unemploymentRegularEmployeedeclarationFormId { get; set; }
            public int? unemploymentRegularEmployeeClaimFormId { get; set; }
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
            public DateTime? dateOfOriginalClaim { get; set; }
            public int haveNotWorkedSinceClaim { get; set; }
            public string nameOfEmployer { get; set; }
            public DateTime? dateOfReemployment { get; set; }
            public string addressEmployer { get; set; }
            public string nisNoEmployer { get; set; }
            public object employerList { get; set; }
            public int willingButUnableToFindWork { get; set; }
            public int physicallyAble { get; set; }
            public int? falseStatementAcknowledge { get; set; }
            public int? failureToSubmitAcknowledge { get; set; }
            public int? notOutOfIslandSinceClaim { get; set; }
            public DateTime? outIslandFrom { get; set; }
            public DateTime? outislandTo { get; set; }
            public string outIslandReason { get; set; }
            public int? consentAgree { get; set; }
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
