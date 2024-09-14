namespace AuthenticationAPI.Models.RegistrationDTO.GetUserDTO
{
    public class GetStudentDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string? Domain { get; set; }
        public string? Phone { get; set; }
    }
}
