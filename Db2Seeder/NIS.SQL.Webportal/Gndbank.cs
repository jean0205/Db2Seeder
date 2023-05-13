using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.NIS.SQL.Webportal
{
    public partial class Gndbank
    {
        public int Id { get; set; }
        public string FinancialInstitution { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Parish { get; set; }
        public string VendorN { get; set; }
    }
}
