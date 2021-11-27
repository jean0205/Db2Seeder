using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.SQL.Logs
{
    public partial class Log
    {
        public int Id { get; set; }
        public string RequestType { get; set; }
        public int? RequestId { get; set; }
        public int? FormId { get; set; }
        public bool? Error { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public DateTime? PostedOn { get; set; }
    }
}
