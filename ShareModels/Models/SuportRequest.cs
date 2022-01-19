using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models
{
    public class SupportRequest
    {
        public int currentWorkflowStateId { get; set; }
        public int supportRequestId { get; set; }
        public int supportRequestTypeId { get; set; }
        public string supportRequestType { get; set; }
        public string supportRequestTypeDisplay { get; set; }
        public string currentWorkflowState { get; set; }
        public int ownerId { get; set; }
        public string owner { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime createdOnToLocalTime =>createdOn.ToLocalTime();       
        public DateTime? updatedOn { get; set; }
        
    }
}
