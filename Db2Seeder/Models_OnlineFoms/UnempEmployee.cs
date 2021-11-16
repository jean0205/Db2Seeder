using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.Models_OnlineFoms
{
    public partial class UnempEmployee
    {
        public int Id { get; set; }
        public int ImportedId { get; set; }
        public int EmployerId { get; set; }
        public string NisNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public DateTime MonthApplied { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool DueToCovid { get; set; }
        public bool PayRevived { get; set; }
        public string Benefits { get; set; }
        public string Bank { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public string FormFile { get; set; }
        public DateTime? DateUpload { get; set; }
        public string UserUpload { get; set; }
        public string AppState { get; set; }
        public string ClaimNumber { get; set; }
        public string Notes { get; set; }
        public string UserName { get; set; }
        public DateTime? DateProc { get; set; }
        public string NisChanged { get; set; }

        public virtual UnempEmployer Employer { get; set; }
    }
}
