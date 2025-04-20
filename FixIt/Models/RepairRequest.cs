namespace FixIt.Models
{
    public enum RequestStatus
    {
        Waiting,
        Assigned,
        Completed
    }

    public class RepairRequest
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Phone { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string? ImagePath { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
