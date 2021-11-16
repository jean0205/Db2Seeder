using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Db2Seeder.Models_OnlineFoms
{
    public partial class EmailNotification
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public bool Employer { get; set; }
        public bool Employee { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateSent { get; set; }
        public string UserSent { get; set; }
    }
}
