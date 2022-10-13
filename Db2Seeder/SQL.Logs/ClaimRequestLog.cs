using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.SQL.Logs
{
    public partial class ClaimRequestLog
    {
        public int Id { get; set; }
        public int SupportRequestId { get; set; }
        public long ClaimNumber { get; set; }
        public string BenefitType { get; set; }
        public string EmployerNumber { get; set; }
        public long EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? PostedOn { get; set; }
    }
}
