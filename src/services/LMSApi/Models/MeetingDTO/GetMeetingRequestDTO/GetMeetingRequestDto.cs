namespace LMSApi.Models.MeetingDTO.GetMeetingRequestDTO
{
    public class GetMeetingRequestDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string RequestMessage { get; set; }
        public string RequestStatus { get; set; }
        public string MeetingLink { get; set; }
        public string Time { get; set; }
    }
}
