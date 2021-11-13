using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models
{
    public class Document_MetaData
    {        
        public Guid documentImageGuid { get; set; }
        public string fileName { get; set; }
        public string fileType { get; set; }
        public int fileSize { get; set; }
        public Guid documentGuid { get; set; }
        public string documentType { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public DateTime createdOn { get; set; }
    }
}
