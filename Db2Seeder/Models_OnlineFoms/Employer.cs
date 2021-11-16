using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.Models_OnlineFoms
{
    public partial class Employer
    {
        public int Id { get; set; }
        public int EmployerId { get; set; }
        public string EmployerNumber { get; set; }
        public string EmployerName { get; set; }
        public string BusinesName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime DateProc { get; set; }
    }
}
