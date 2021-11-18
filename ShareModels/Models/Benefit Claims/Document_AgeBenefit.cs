using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models.Benefit_Claims
{
    public class Document_AgeBenefit
    {
        public int ageBenefitFormId { get; set; }
        public string surname { get; set; }
        public string nisNo { get; set; }
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
        public string home { get; set; }
        public string work { get; set; }
        public string mobile { get; set; }
        public string postalAddress { get; set; }
        public string bank { get; set; }
        public string accountNo { get; set; }
        public string accountName { get; set; }
        public int accountType { get; set; }
        public int chkbxInvalidity { get; set; }
        public int chkbxSurvivors { get; set; }
        public int chkbxSickness { get; set; }
        public object chkbxNone { get; set; }
        public int providentFund { get; set; }
        public string providentFundList { get; set; }
        public string workHistoryList { get; set; }
        public int workOtherCountries { get; set; }
        public string workOtherCountryList { get; set; }
        public List<WorkHistoryEntity> workHistoryEntity { get; set; }
        public List<ProvidentFundEntity> providentFundEntity { get; set; }
        public List<WorkOtherCountryEntity> workOtherCountryEntity { get; set; }
        public int documentId { get; set; }
        public string documentGuid { get; set; }
        public int documentTypeId { get; set; }
        public string documentCode { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
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
    }
}
