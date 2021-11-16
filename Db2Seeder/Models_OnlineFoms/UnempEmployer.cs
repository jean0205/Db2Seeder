using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.Models_OnlineFoms
{
    public partial class UnempEmployer
    {
        public UnempEmployer()
        {
            UnempEmployee = new HashSet<UnempEmployee>();
        }

        public int Id { get; set; }
        public int ImportedId { get; set; }
        public string EmployerNo { get; set; }
        public string EmployerSub { get; set; }
        public string EmployerName { get; set; }
        public bool PayrollSupport { get; set; }
        public string Officer { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string FormFile { get; set; }
        public DateTime? DateUpload { get; set; }
        public string UserUpload { get; set; }
        public string AppState { get; set; }
        public string Notes { get; set; }
        public string UserName { get; set; }
        public DateTime? Dateproc { get; set; }
        public DateTime? DateClosed { get; set; }
        public int? GovermentChecked { get; set; }
        public string GovertmentUser { get; set; }
        public string EmployerNumberChanged { get; set; }

        public virtual ICollection<UnempEmployee> UnempEmployee { get; set; }
    }
}
