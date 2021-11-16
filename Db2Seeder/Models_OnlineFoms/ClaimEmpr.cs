using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.Models_OnlineFoms
{
    public partial class ClaimEmpr
    {
        public int Id { get; set; }
        public int ImportedId { get; set; }
        public int EmployerNo { get; set; }
        public int EmployerSub { get; set; }
        public string EmployerName { get; set; }
        public string Email { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime? DateUpload { get; set; }
        public string UserUpload { get; set; }
        public string AppState { get; set; }
        public string Notes { get; set; }
        public string UserName { get; set; }
        public DateTime? Dateproc { get; set; }
        public string NumberChanged { get; set; }
    }
}
