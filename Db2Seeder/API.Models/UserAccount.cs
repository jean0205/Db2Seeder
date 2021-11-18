using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder.API.Models
{
    public class UserAccount
    {
        public object password { get; set; }
        public int userAccountId { get; set; }
        public int userAccountStatusId { get; set; }
        public string mobilePhone { get; set; }
        public string email { get; set; }
        public object lockoutEndDateUtc { get; set; }
        public DateTime lastSuccessLogin { get; set; }
        public bool emailVerified { get; set; }
        public bool phoneVerified { get; set; }
        public string accountName { get; set; }
        public bool useEmailForNotification { get; set; }
        public bool useMobileForNotification { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }
    }
}
