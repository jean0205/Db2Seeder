using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.ModelsTest
{
    public partial class RejectionCodes
    {
        public int Id { get; set; }
        public int? Code { get; set; }
        public string Reasson { get; set; }
        public bool? Claim { get; set; }
        public bool? Declaration { get; set; }
        public bool? RejectAllPeriods { get; set; }
    }
}
