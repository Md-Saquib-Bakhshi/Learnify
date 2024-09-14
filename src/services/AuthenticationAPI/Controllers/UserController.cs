using AuthenticationAPI.Models;
using AuthenticationAPI.Models.RegistrationDTO.UpdateUserDTO;
using AuthenticationAPI.Repositories.UserRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _adminManager;
        private readonly IUserService _userService;

        public UserController(UserManager<ApplicationUser> adminManager, IUserService userService)
        {
            _adminManager = adminManager;
            _userService = userService;
        }

        [HttpGet("role/{role}")]
        public async Task<IActionResult> GetAllUser(string role)
        {
            var response = await _userService.GetAllUser(role);

            if (response.Status == "Error")
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }

        [HttpGet("role/{role}/email/{email}")]
        public async Task<IActionResult> GetUserById(string role, string email)
        {
            var response = await _userService.GetUserById(role, email);

            if (response.Status == "Error")
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }

        [HttpPut("role-admin/{role}/email/{email}")]
        public async Task<IActionResult> UpdateAdmin([FromBody] UpdateAdminDto updateAdminDto, string role, string email)
        {
            if (updateAdminDto == null)
            {
                return BadRequest(new Response
                {
                    Status = "Error",
                    Message = "All fields are required."
                });
            }

            var response = await _userService.UpdateAdmin(updateAdminDto, role, email);

            if (response.Status == "Error")
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }

        [HttpPut("role/{role}/email/{email}")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentDto updateStudentDto, string role, string email)
        {
            if (updateStudentDto == null)
            {
                return BadRequest(new Response
                {
                    Status = "Error",
                    Message = "All fields are required."
                });
            }

            var response = await _userService.UpdateStudent(updateStudentDto, role, email);

            if (response.Status == "Error")
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }

        [HttpDelete("role/{role}/email/{email}")]
        public async Task<IActionResult> DeleteUser(string role, string email)
        {
            var response = await _userService.DeleteUser(role, email);

            if (response.Status == "Error")
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }
    }
}
