
using ShareModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models.Benefit_Claims
{
    public class Document_DeathBenefit
    {
        public int deathBenefitFormId { get; set; }
        public string nameDeceased { get; set; }
        public int nisNo { get; set; }
        public string lastAddressDeceased { get; set; }
        public string childrenUnder { get; set; }
        public DateTime dateOfBirthDeceased { get; set; }
        public DateTime dateOfDeath { get; set; }
        public string placeOfBirthDeceased { get; set; }
        public string mothersNameDeceased { get; set; }
        public string fathersNameDeceased { get; set; }
        public string fullNameWidow { get; set; }
        public string addressWidow { get; set; }
        public DateTime dateOfBirthWidow { get; set; }
        public DateTime dateOfMarriageWidow { get; set; }
        public int livingTogether { get; set; }
        public int marriedToSomeone { get; set; }
        public int survivingPerson { get; set; }
        public string howLongTogetherYrs { get; set; }
        public string howLongTogetherMths { get; set; }
        public string namePersonMakingClaim { get; set; }
        public string addressMakingClaim { get; set; }
        public DateTime dateOfBirthMakingClaim { get; set; }
        public string relationMakingClaim { get; set; }
        public int residingMakingClaim { get; set; }
        public int maintainMakingClaim { get; set; }
        public string amountMakingClaim { get; set; }
        public string childrenParticular { get; set; }
        public int deceasedUnder { get; set; }
        public List<ChildrenUnderEntity> childrenUnderEntity { get; set; }
        public List<ChildrenParticularEntity> childrenParticularEntity { get; set; }
        public int documentId { get; set; }
        public string documentGuid { get; set; }
        public int documentTypeId { get; set; }
        public string documentCode { get; set; }
        public object name { get; set; }
        public object description { get; set; }
        public int createdBy { get; set; }
        public int updatedBy { get; set; }
        public int? checkInById { get; set; }
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
        public string WebPortalLink => Settings.GetPortalUrl() + SupportRequestId;
        public int? ClaimNumber { get; set; }
    }
    public class ChildrenUnderEntity
    {
        public string nameChild { get; set; }
        public string address { get; set; }
        public bool status { get; set; }
    }

    public class ChildrenParticularEntity
    {
        public string name { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string father { get; set; }
        public string mother { get; set; }
        public string childresiding { get; set; }
        public string childmaintenance { get; set; }
        public string maintenance { get; set; }
        public bool status { get; set; }
    }
}
