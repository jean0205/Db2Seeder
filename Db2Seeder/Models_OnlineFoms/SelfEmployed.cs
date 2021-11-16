using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.Models_OnlineFoms
{
    public partial class SelfEmployed
    {
        public int Id { get; set; }
        public int EmployeId { get; set; }
        public string NisNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime DateProc { get; set; }
    }
}
