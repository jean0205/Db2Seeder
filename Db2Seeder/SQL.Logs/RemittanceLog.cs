using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.SQL.Logs
{
    public partial class RemittanceLog
    {
        public int Id { get; set; }
        public int SupportRequestId { get; set; }
        public long EmployerNo { get; set; }
        public int EmployerSub { get; set; }
        public int Period { get; set; }
        public decimal TotalInsuranceEarning { get; set; }
        public decimal TotalContribution { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime PostedOn { get; set; }
    }
}
