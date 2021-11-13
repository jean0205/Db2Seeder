namespace ShareModels.Models
{
    public class SupportRequestType
    {
        public int SupportRequestTypeId { get; set; }
        public string Description { get; set; }
        public int? WorkflowId { get; set; }
    }
}
