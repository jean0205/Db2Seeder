using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.Models_OnlineFoms
{
    public partial class EmployeeRegist
    {
        public int Id { get; set; }
        public string ImportedId { get; set; }
        public int EmployerId { get; set; }
        public int NisNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddelName { get; set; }
        public string LastName { get; set; }
        public DateTime EmployedDate { get; set; }

        public virtual EmployerRegist Employer { get; set; }
    }
}
