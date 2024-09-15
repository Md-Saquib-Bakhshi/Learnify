namespace LMSApi.Models
{
    public class MeetingRequest
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string RequestMessage { get; set; }
        public string RequestStatus { get; set; } = "Pending";
        public string? MeetingLink { get; set; }
        public string? Time { get; set; }
    }
}
