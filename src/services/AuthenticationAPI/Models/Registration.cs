namespace AuthenticationAPI.Models
{
    public class Registration
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string? Domain { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; }

        public Registration()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
