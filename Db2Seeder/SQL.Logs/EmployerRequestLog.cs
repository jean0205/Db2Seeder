using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.SQL.Logs
{
    public partial class EmployerRequestLog
    {
        public int Id { get; set; }
        public int SupportRequestId { get; set; }
        public bool? SelfEmployed { get; set; }
        public bool? Voluntary { get; set; }
        public long EmployerNo { get; set; }
        public string EmloyerName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime CompletedOn { get; set; }
        public string CompletedBy { get; set; }
        public DateTime PostedOn { get; set; }
    }
}
