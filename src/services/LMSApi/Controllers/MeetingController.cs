using LMSApi.Models;
using LMSApi.Models.MeetingDTO;
using LMSApi.Models.MeetingDTO.CreateMeetingRequestDTO;
using LMSApi.Models.MeetingDTO.MeetingResponseDTO;
using LMSApi.Repositories.MeetingRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LMSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        // Create a new meeting request
        [HttpPost("request")]
        public async Task<IActionResult> CreateMeetingRequest([FromBody] CreateMeetingRequestDto meetingRequestDto)
        {
            var response = await _meetingService.CreateMeetingRequest(meetingRequestDto);

            if (response.Status == "Success")
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Get all meeting requests
        [HttpGet("requests")]
        public async Task<IActionResult> GetAllRequests()
        {
            var response = await _meetingService.GetAllRequests();

            if (response.Status == "Success")
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        // Get meeting request by student email
        [HttpGet("request/{email}")]
        public async Task<IActionResult> GetByStudentEmail(string email)
        {
            var response = await _meetingService.GetByStudentEmail(email);

            if (response.Status == "Success")
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        // Admin makes a response to a meeting request
        [HttpPost("response")]
        public async Task<IActionResult> MakeMeetingResponse([FromBody] MeetingResponseDto meetingResponseDto)
        {
            var response = await _meetingService.MakeMeetingResponse(meetingResponseDto);

            if (response.Status == "Success")
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Check if a student has a pending meeting request
        [HttpGet("hasPendingRequest/{email}")]
        public async Task<IActionResult> HasPendingRequest(string email)
        {
            var response = await _meetingService.HasPendingRequest(email);

            if (response.Status == "Success")
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
