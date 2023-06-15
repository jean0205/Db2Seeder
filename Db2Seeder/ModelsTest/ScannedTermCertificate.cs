using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.ModelsTest
{
    public partial class ScannedTermCertificate
    {
        public int Id { get; set; }
        public long ClaimNumber { get; set; }
        public long NisNumber { get; set; }
        public DateTime? DateOfTermination { get; set; }
        public DateTime? LastDayatWork { get; set; }
        public DateTime? LastDayPaid { get; set; }
        public decimal LieuWeeks { get; set; }
        public decimal SeveranceWeeks { get; set; }
        public int VacationDays { get; set; }
        public string LinkedBy { get; set; }
        public DateTime LinkedTime { get; set; }
    }
}
