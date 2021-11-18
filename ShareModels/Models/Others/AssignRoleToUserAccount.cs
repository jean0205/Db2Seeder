using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models.Others
{
    public class AssignRoleToUserAccount
    {
        public int userAccountId { get; set; }
        public int roleId { get; set; }
        public int createdBy { get; set; }
    }
}
