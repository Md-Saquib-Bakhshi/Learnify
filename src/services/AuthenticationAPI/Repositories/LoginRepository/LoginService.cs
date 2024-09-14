using AuthenticationAPI.Models;
using AuthenticationAPI.Models.LoginDTO;
using AuthenticationAPI.Models.TokenDTO;
using AuthenticationAPI.Repositories.TokenRepository;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthenticationAPI.Repositories.LoginRepository
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public LoginService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<ResponseWithData<TokenDataDto>> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return new ResponseWithData<TokenDataDto>
                {
                    Status = "Error",
                    Message = "Invalid email or password.",
                    Data = null
                };
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GetToken(authClaims);

            var tokenData = new TokenDataDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Roles = userRoles.ToList()
            };

            return new ResponseWithData<TokenDataDto>
            {
                Status = "Success",
                Message = "Login successful.",
                Data = tokenData
            };
        }
    }
}
