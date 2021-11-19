using System;

namespace ShareModels.Models
{
    public class RequestHistory
    {
        public string description { get; set; }
        public string modifiedBy { get; set; }
        public DateTime dateModified { get; set; }
        public string email { get; set; }
        public string UserName => $"{email}".Split('@')[0].ToUpper();
    }
}
