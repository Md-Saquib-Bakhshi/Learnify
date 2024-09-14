namespace AuthenticationAPI.Models.RegistrationDTO.UpdateUserDTO
{
    public class UpdateStudentDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string? Domain { get; set; }
        public string? Phone { get; set; }
    }
}
