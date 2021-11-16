using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.Models_OnlineFoms
{
    public partial class UnempGoverment
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string EmployerNo { get; set; }
        public string EmployerSub { get; set; }
        public string EmployerName { get; set; }
        public string UserName { get; set; }
        public DateTime DateProc { get; set; }
    }
}
