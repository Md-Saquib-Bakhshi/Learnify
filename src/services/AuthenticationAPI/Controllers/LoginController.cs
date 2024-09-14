using AuthenticationAPI.Models;
using AuthenticationAPI.Models.LoginDTO;
using AuthenticationAPI.Models.TokenDTO;
using AuthenticationAPI.Repositories.LoginRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest(new Response
                {
                    Status = "Error",
                    Message = "All fields are required."
                });
            }

            var response = await _loginService.LoginUserAsync(loginDto);

            if (response.Status == "Error")
            {
                return Unauthorized(new Response
                {
                    Status = "Error",
                    Message = response.Message
                });
            }

            return Ok(new ResponseWithData<TokenDataDto>
            {
                Status = response.Status,
                Message = response.Message,
                Data = response.Data
            });
        }
    }
}
