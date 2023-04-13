using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.NIS.SQL.Unemployment.ModelsUnemployment
{
    public partial class RequestClaimMapping
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public long ClaimNumber { get; set; }
    }
}
