using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.NIS.SQL.Unemployment.ModelsUnemployment
{
    public partial class TerminationCertificate
    {
        public int Id { get; set; }
        public long? SupportrequestId { get; set; }
        public long NisNumber { get; set; }
        public long EmployerNumber { get; set; }
        public int? EmployerSub { get; set; }
        public long? ClaimNumber { get; set; }
        public string CertificateJson { get; set; }
        public byte[] CertificatePdf { get; set; }
        public DateTime SavedTime { get; set; }
        public string LinkedBy { get; set; }
        public DateTime? LinkedTime { get; set; }
        public long? ClaimSupportRequestId { get; set; }
    }
}
