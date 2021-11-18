using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder.API.Models
{
    public class AssignRoleToUserAccount
    {
        public int userAccountId { get; set; }
        public int roleId { get; set; }
        public int createdBy { get; set; }
    }
}
