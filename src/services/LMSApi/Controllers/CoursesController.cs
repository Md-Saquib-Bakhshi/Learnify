using LMSApi.Models.CourseDTO.CreateCourseDTO;
using LMSApi.Models.CourseDTO.UpdateCourseDTO;
using LMSApi.Repositories.CourseRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] CreateCourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Status = "Error", Message = "Invalid course data." });

            var result = await _courseService.AddCourse(courseDto);

            if (result.Status == "Success")
                return Ok(result);

            return BadRequest(new { Status = result.Status, Message = result.Message });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var result = await _courseService.GetAllCourse();

            if (result.Status == "Success")
                return Ok(result);

            return NotFound(new { Status = result.Status, Message = result.Message });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var result = await _courseService.GetCourse(id);

            if (result.Status == "Success")
                return Ok(result);

            return NotFound(new { Status = result.Status, Message = result.Message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Status = "Error", Message = "Invalid course data." });

            if (id != courseDto.Id)
                return BadRequest(new { Status = "Error", Message = "Course ID mismatch." });

            var result = await _courseService.UpdateCourse(id, courseDto);

            if (result.Status == "Success")
                return Ok(result);

            return BadRequest(new { Status = result.Status, Message = result.Message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _courseService.DeleteCourse(id);

            if (result.Status == "Success")
                return Ok(result);

            return BadRequest(new { Status = result.Status, Message = result.Message });
        }
    }
}
