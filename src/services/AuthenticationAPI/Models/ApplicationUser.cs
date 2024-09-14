using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string? Domain { get; set; }
        public string Gender { get; set; }
    }
}
