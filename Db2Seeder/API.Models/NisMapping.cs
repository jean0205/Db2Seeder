using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder.API.Models
{ 
    public class NisMapping
    {
        public int userAccountId { get; set; }
        public string nisNumber { get; set; }
        public int nisNumberTypeId { get; set; }
    }
}
