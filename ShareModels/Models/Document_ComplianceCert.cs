using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models
{
    public class Document_ComplianceCert
    {
        public string employerNumber { get; set; }
        public string businessName { get; set; }
        public string businessAddress { get; set; }
        public string certificateReason { get; set; }
        public string emailAddress { get; set; }
        public string phoneNumber { get; set; }
        public string title { get; set; }
        public int documentId { get; set; }
        public string documentGuid { get; set; }
        public int documentTypeId { get; set; }
        public string documentCode { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public int? checkInById { get; set; }
        public DateTime? checkInAtTime { get; set; }
        public string checkInMessage { get; set; }
        public int? checkOutById { get; set; }
        public DateTime? checkOutTime { get; set; }
        public string checkOutMessage { get; set; }
        public DateTime? checkOutExpiration { get; set; }
        public int version { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }
    }
}
