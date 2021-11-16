using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models
{
    public class RequestHistory
    {
        public string description { get; set; }
        public string modifiedBy { get; set; }
        public DateTime dateModified { get; set; }
    }
}
