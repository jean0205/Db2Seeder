using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.NIS.SQL.DocumentsTest
{
    public partial class Documents
    {
        public int FileId { get; set; }
        public string ActiveCode { get; set; }
        public int RegistrantTypeId { get; set; }
        public string DocTypeId { get; set; }
        public int ImportId { get; set; }
        public int NisNumber { get; set; }
        public int? ClaimNumber { get; set; }
        public byte[] PdfData { get; set; }
        public string ScannedBy { get; set; }
        public DateTime ScanDatetime { get; set; }
        public int? SubNumber { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDatetime { get; set; }
        public string ProFlag { get; set; }
        public string FlOpn { get; set; }
        public string ClaimComment { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string GovClaim { get; set; }
    }
}
