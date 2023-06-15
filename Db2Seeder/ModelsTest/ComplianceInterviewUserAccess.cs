using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.ModelsTest
{
    public partial class ComplianceInterviewUserAccess
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool? ComplianceManager { get; set; }
        public bool? Compliance { get; set; }
        public bool? Supervisor { get; set; }
        public bool? SecondApprover { get; set; }
    }
}
