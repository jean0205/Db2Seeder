using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.Models_OnlineFoms
{
    public partial class ClaimEmpe
    {
        public int Id { get; set; }
        public int ImportedId { get; set; }
        public int EmployerId { get; set; }
        public int NisNumber { get; set; }
        public string Name { get; set; }
        public string FileUrl { get; set; }
        public DateTime AppDate { get; set; }
        public DateTime DateUpload { get; set; }
        public string UserUpload { get; set; }
        public string AppState { get; set; }
        public string Notes { get; set; }
        public string UserName { get; set; }
        public DateTime? DateProc { get; set; }
        public int? ClaimNumber { get; set; }
        public string NisChanged { get; set; }
    }
}
