using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.NIS.SQL.DocumentsTest
{
    public partial class FailedImports
    {
        public int FailedFileId { get; set; }
        public string FailedFileName { get; set; }
        public int FailCode { get; set; }
        public int ImportId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDatetime { get; set; }
    }
}
