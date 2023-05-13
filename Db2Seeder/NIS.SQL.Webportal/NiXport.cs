using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.NIS.SQL.Webportal
{
    public partial class NiXport
    {
        public string Nisnum { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Sex { get; set; }
        public string Dob { get; set; }
        public string Natl { get; set; }
        public string Plob { get; set; }
        public string Issued { get; set; }
        public short Cardprint { get; set; }
        public string Lastmod { get; set; }
        public string Photo { get; set; }
        public string Signature { get; set; }
    }
}
