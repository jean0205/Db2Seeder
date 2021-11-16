using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.Models_OnlineFoms
{
    public partial class ComplianceCert
    {
        public int Id { get; set; }
        public int ImportedId { get; set; }
        public string EmployerNo { get; set; }
        public string EmployerSub { get; set; }
        public string BusinessName { get; set; }
        public string BusinessAddress { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Reason { get; set; }
        public string Title { get; set; }
        public DateTime AppDate { get; set; }
        public string UserUpload { get; set; }
        public DateTime DateUpload { get; set; }
        public string AppState { get; set; }
        public string Notes { get; set; }
        public string UserProcess { get; set; }
        public DateTime? DateProcess { get; set; }
        public string Form { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string CertificatePath { get; set; }
    }
}
