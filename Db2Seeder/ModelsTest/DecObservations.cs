using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.ModelsTest
{
    public partial class DecObservations
    {
        public int Id { get; set; }
        public int DeclarationId { get; set; }
        public int ObservationCode { get; set; }
        public string Message { get; set; }
        public string UserW { get; set; }
        public DateTime TimeW { get; set; }
    }
}
