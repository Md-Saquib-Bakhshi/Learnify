namespace AuthenticationAPI.Models.TokenDTO
{
    public class TokenDataDto
    {
        public string Token { get; set; }
        public IList<string> Roles { get; set; }
    }
}
