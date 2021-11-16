using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.Models_OnlineFoms
{
    public partial class EmployerRegist
    {
        public EmployerRegist()
        {
            EmployeeRegist = new HashSet<EmployeeRegist>();
        }

        public int Id { get; set; }
        public int ImportedId { get; set; }
        public string BusinessType { get; set; }
        public string BusinessName { get; set; }
        public string EmployerName { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessTown { get; set; }
        public string BusinessParish { get; set; }
        public string MailingAddress { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string EmailAddress { get; set; }
        public string NatureObusiness { get; set; }
        public DateTime BusinessDate { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
        public string AccountType { get; set; }
        public int MaleEmployees { get; set; }
        public int FemaleEmployees { get; set; }
        public string CertRegistration { get; set; }
        public string CertIncorporation { get; set; }
        public string NoticeOdirectors { get; set; }
        public string NoticeRegisteredOffice { get; set; }
        public DateTime AppDate { get; set; }
        public DateTime DateUpload { get; set; }
        public string UserUpload { get; set; }
        public string AppState { get; set; }
        public string Comments { get; set; }
        public DateTime? DateProcess { get; set; }
        public string UserProcess { get; set; }
        public string Form { get; set; }

        public virtual ICollection<EmployeeRegist> EmployeeRegist { get; set; }
    }
}
