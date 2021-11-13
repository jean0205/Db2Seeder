using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments
{
    public partial class ImportLog
    {
        public int ImportId { get; set; }
        public string ImportedBy { get; set; }
        public DateTime ImportDatetime { get; set; }
    }
}
