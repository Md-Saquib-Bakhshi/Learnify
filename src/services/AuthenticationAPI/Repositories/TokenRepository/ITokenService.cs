using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthenticationAPI.Repositories.TokenRepository
{
    public interface ITokenService
    {
        JwtSecurityToken GetToken(List<Claim> authClaims);
    }
}
