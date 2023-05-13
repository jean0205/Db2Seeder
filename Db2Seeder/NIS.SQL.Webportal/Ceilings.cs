using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.NIS.SQL.Webportal
{
    public partial class Ceilings
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal MinWeekly { get; set; }
        public decimal MaxWeekly { get; set; }
        public decimal MinMonthly { get; set; }
        public decimal MaxMonthly { get; set; }
        public decimal MinFortnightly { get; set; }
        public decimal MaxFortnightly { get; set; }
    }
}
