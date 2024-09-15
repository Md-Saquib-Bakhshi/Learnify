using LMSApi.Data;
using LMSApi.Models;
using LMSApi.Models.CourseDTO.CreateCourseDTO;
using LMSApi.Models.CourseDTO.GetCourseDTO;
using LMSApi.Models.CourseDTO.UpdateCourseDTO;
using Microsoft.EntityFrameworkCore;

namespace LMSApi.Repositories.CourseRepository
{
    public class CourseService : ICourseService
    {
        private readonly LMSDbContext _lmsDbContext;

        public CourseService(LMSDbContext lmsDbContext)
        {
            _lmsDbContext = lmsDbContext;
        }

        // Add Course
        public async Task<ResponseWithData<bool>> AddCourse(CreateCourseDto courseDto)
        {
            var response = new ResponseWithData<bool>();
            try
            {
                // Check if the Playlist exists
                var playlist = await _lmsDbContext.Playlists.FindAsync(courseDto.PlaylistId);
                if (playlist == null)
                {
                    response.Data = false;
                    response.Status = "Error";
                    response.Message = "Playlist not found.";
                    return response;
                }

                // Map CourseDto to the actual Course entity
                var course = new Course
                {
                    Title = courseDto.Title,
                    Link = courseDto.Link,
                    PlaylistId = courseDto.PlaylistId 
                };

                await _lmsDbContext.Courses.AddAsync(course);
                await _lmsDbContext.SaveChangesAsync();

                response.Data = true;
                response.Status = "Success";
                response.Message = "Course added successfully.";
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Status = "Error";
                response.Message = ex.Message;
            }

            return response;
        }

        // Get All Courses
        public async Task<ResponseWithData<IEnumerable<GetCourseDto>>> GetAllCourse()
        {
            var response = new ResponseWithData<IEnumerable<GetCourseDto>>();
            try
            {
                var courses = await _lmsDbContext.Courses
                    .Select(c => new GetCourseDto
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Link = c.Link,
                        PlaylistId = c.PlaylistId 
                    })
                    .ToListAsync();

                response.Data = courses;
                response.Status = "Success";
                response.Message = "Courses retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Status = "Error";
                response.Message = ex.Message;
            }

            return response;
        }

        // Get Course by ID
        public async Task<ResponseWithData<GetCourseDto>> GetCourse(int id)
        {
            var response = new ResponseWithData<GetCourseDto>();
            try
            {
                var course = await _lmsDbContext.Courses
                    .Where(c => c.Id == id)
                    .Select(c => new GetCourseDto
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Link = c.Link,
                        PlaylistId = c.PlaylistId 
                    })
                    .FirstOrDefaultAsync();

                if (course == null)
                {
                    response.Data = null;
                    response.Status = "Error";
                    response.Message = "Course not found.";
                }
                else
                {
                    response.Data = course;
                    response.Status = "Success";
                    response.Message = "Course retrieved successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Status = "Error";
                response.Message = ex.Message;
            }

            return response;
        }

        // Update Course
        public async Task<ResponseWithData<bool>> UpdateCourse(int id, UpdateCourseDto courseDto)
        {
            var response = new ResponseWithData<bool>();
            try
            {
                var course = await _lmsDbContext.Courses.FindAsync(id);
                if (course == null)
                {
                    response.Data = false;
                    response.Status = "Error";
                    response.Message = "Course not found.";
                    return response;
                }

                // Check if the Playlist exists
                var playlist = await _lmsDbContext.Playlists.FindAsync(courseDto.PlaylistId);
                if (playlist == null)
                {
                    response.Data = false;
                    response.Status = "Error";
                    response.Message = "Playlist not found.";
                    return response;
                }

                // Update course details
                course.Title = courseDto.Title;
                course.Link = courseDto.Link;
                course.PlaylistId = courseDto.PlaylistId; 

                _lmsDbContext.Courses.Update(course);
                await _lmsDbContext.SaveChangesAsync();

                response.Data = true;
                response.Status = "Success";
                response.Message = "Course updated successfully.";
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Status = "Error";
                response.Message = ex.Message;
            }

            return response;
        }

        // Delete Course
        public async Task<ResponseWithData<bool>> DeleteCourse(int id)
        {
            var response = new ResponseWithData<bool>();
            try
            {
                var course = await _lmsDbContext.Courses.FindAsync(id);
                if (course == null)
                {
                    response.Data = false;
                    response.Status = "Error";
                    response.Message = "Course not found.";
                    return response;
                }

                _lmsDbContext.Courses.Remove(course);
                await _lmsDbContext.SaveChangesAsync();

                response.Data = true;
                response.Status = "Success";
                response.Message = "Course deleted successfully.";
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Status = "Error";
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
