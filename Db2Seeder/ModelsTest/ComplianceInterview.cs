using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.ModelsTest
{
    public partial class ComplianceInterview
    {
        public int Id { get; set; }
        public bool? Sep { get; set; }
        public long Nisnumber { get; set; }
        public long ClaimNumber { get; set; }
        public string RequestedBy { get; set; }
        public DateTime RequestedTime { get; set; }
        public string RequesterComments { get; set; }
        public string Status { get; set; }
        public string Interviewer { get; set; }
        public DateTime? InterviewTime { get; set; }
        public string InterviewJson { get; set; }
        public bool? PayRecommended { get; set; }
        public string LockedBy { get; set; }
    }
}
