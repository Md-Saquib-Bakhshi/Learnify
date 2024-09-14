using LMSApi.Models.CourseDTO;
using LMSApi.Models.UpdateCourseDTO;
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

        // POST: api/Courses
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] CreateCourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid course data.");

            var result = await _courseService.AddCourse(courseDto);

            if (result.Status == "Success")
                return Ok(result);
            return BadRequest(result);
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var result = await _courseService.GetAllCourse();

            if (result.Status == "Success")
                return Ok(result);
            return NotFound(result);
        }

        // GET: api/Courses/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var result = await _courseService.GetCourse(id);

            if (result.Status == "Success")
                return Ok(result);
            return NotFound(result);
        }

        // PUT: api/Courses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid course data.");

            var result = await _courseService.UpdateCourse(id, courseDto);

            if (result.Status == "Success")
                return Ok(result);
            return BadRequest(result);
        }

        // DELETE: api/Courses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _courseService.DeleteCourse(id);

            if (result.Status == "Success")
                return Ok(result);
            return BadRequest(result);
        }
    }
}
