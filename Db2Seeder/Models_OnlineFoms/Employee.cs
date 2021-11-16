using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.Models_OnlineFoms
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string ImportedId { get; set; }
        public string ApplicationType { get; set; }
        public string Nisnumber { get; set; }
        public string FirstName { get; set; }
        public string MaidenName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime? DateMarriage { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string Parish { get; set; }
        public string Nationality { get; set; }
        public string EmailAddress { get; set; }
        public string MobilePhone { get; set; }
        public string Mobile2Phone { get; set; }
        public string HomePhone { get; set; }
        public string Bank { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinnessTown { get; set; }
        public string BusinessParish { get; set; }
        public string MailingAddress { get; set; }
        public string BusinessMobile { get; set; }
        public string BusinessPhone { get; set; }
        public string BusinessFax { get; set; }
        public string BusinessEmail { get; set; }
        public DateTime? BusinessCommenced { get; set; }
        public string NatureOfBusiness { get; set; }
        public decimal? MonthlyIncome { get; set; }
        public string BusinessBank { get; set; }
        public string BusinessAccountNumber { get; set; }
        public string BusinessAccountType { get; set; }
        public decimal? PreviusIncome { get; set; }
        public string LastEmployer { get; set; }
        public DateTime? DateEmploymentEnd { get; set; }
        public string FormFile { get; set; }
        public string Photo { get; set; }
        public string BirthCertificate { get; set; }
        public string PictureId { get; set; }
        public string MarriageCertificate { get; set; }
        public DateTime? DateProcessed { get; set; }
        public string UserName { get; set; }
        public string ApplicationState { get; set; }
        public string Note { get; set; }
    }
}
