using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder.API.Models
{
    public class SupportRequestComment
    {
        public int supportRequestId { get; set; }
        public int supportRequestCommentId { get; set; }
        public int userAccountId { get; set; }
        public string comment { get; set; }
    }
}
