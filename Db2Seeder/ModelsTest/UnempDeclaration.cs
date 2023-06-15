using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.ModelsTest
{
    public partial class UnempDeclaration
    {
        public int Id { get; set; }
        public long SupportrequestId { get; set; }
        public long NisNumber { get; set; }
        public long? ClaimNumber { get; set; }
        public string DeclarationJson { get; set; }
        public DateTime SavedTime { get; set; }
        public long? ClaimSupportRequestId { get; set; }
        public string Status { get; set; }
        public string ProcessedBy { get; set; }
        public DateTime? ProcessTime { get; set; }
        public string ReassonForRejection { get; set; }
        public string RejectionComment { get; set; }
        public string LockedBy { get; set; }
        public string PaymentPeriodsJson { get; set; }
        public string PaymentPeriodsJsonAfter { get; set; }
    }
}
