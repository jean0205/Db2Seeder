using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.NIS.SQL.Unemployment.ModelsUnemployment
{
    public partial class Status
    {
        public Status()
        {
            DeclarationStatus = new HashSet<DeclarationStatus>();
        }

        public int Id { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public virtual RejectionCodes IdNavigation { get; set; }
        public virtual ICollection<DeclarationStatus> DeclarationStatus { get; set; }
    }
}
