using LMSApi.Models;
using LMSApi.Models.CourseDTO.CreateCourseDTO;
using LMSApi.Models.CourseDTO.GetCourseDTO;
using LMSApi.Models.CourseDTO.UpdateCourseDTO;

namespace LMSApi.Repositories.CourseRepository
{
    public interface ICourseService
    {
        Task<ResponseWithData<bool>> AddCourse(CreateCourseDto courseDto);
        Task<ResponseWithData<IEnumerable<GetCourseDto>>> GetAllCourse();
        Task<ResponseWithData<GetCourseDto>> GetCourse(int id);
        Task<ResponseWithData<bool>> UpdateCourse(int id, UpdateCourseDto courseDto);
        Task<ResponseWithData<bool>> DeleteCourse(int id);
    }
}

