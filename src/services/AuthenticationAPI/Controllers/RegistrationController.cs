using AuthenticationAPI.Models;
using AuthenticationAPI.Models.RegistrationDTO.CreateUserDTO;
using AuthenticationAPI.Repositories.RegistrationRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] CreateStudentDto createStudentDto)
        {
            if (createStudentDto == null)
            {
                return BadRequest(new Response
                {
                    Status = "Error",
                    Message = "All fields are required."
                });
            }

            var response = await _registrationService.RegisterStudentAsync(createStudentDto);

            if (response.Status == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] CreateAdminDto createAdminDto)
        {
            if (createAdminDto == null)
            {
                return BadRequest(new Response
                {
                    Status = "Error",
                    Message = "All fields are required."
                });
            }

            var response = await _registrationService.RegisterAdminAsync(createAdminDto);

            if (response.Status == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            return Ok(response);
        }
    }
}
