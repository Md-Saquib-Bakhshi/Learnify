using AuthenticationAPI.Models.LoginDTO;
using AuthenticationAPI.Models;
using AuthenticationAPI.Models.TokenDTO;

namespace AuthenticationAPI.Repositories.LoginRepository
{
    public interface ILoginService
    {
        Task<ResponseWithData<TokenDataDto>> LoginUserAsync(LoginDto loginDto);
    }
}
