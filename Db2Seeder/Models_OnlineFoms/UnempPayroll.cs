using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.Models_OnlineFoms
{
    public partial class UnempPayroll
    {
        public int Id { get; set; }
        public bool? Active { get; set; }
        public string NisNumber { get; set; }
        public string SurName { get; set; }
        public string GivenNames { get; set; }
        public string ContactNo1 { get; set; }
        public string ContactNo2 { get; set; }
        public string ContactNo3 { get; set; }
        public string Email { get; set; }
        public string Bank { get; set; }
        public string BankAccount { get; set; }
        public string AccountName { get; set; }
        public string Address { get; set; }
        public string VehicleReg { get; set; }
        public string UserName { get; set; }
        public DateTime? DateInserted { get; set; }
    }
}
