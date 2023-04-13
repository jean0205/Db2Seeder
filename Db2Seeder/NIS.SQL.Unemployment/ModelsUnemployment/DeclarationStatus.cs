using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.NIS.SQL.Unemployment.ModelsUnemployment
{
    public partial class DeclarationStatus
    {
        public int DeclarationsId { get; set; }
        public int StatussesId { get; set; }

        public virtual UnempDeclaration Declarations { get; set; }
        public virtual Status Statusses { get; set; }
    }
}
