using AuthenticationAPI.Models.RegistrationDTO.GetUserDTO;
using AuthenticationAPI.Models.RegistrationDTO.UpdateUserDTO;
using AuthenticationAPI.Models;

namespace AuthenticationAPI.Repositories.UserRepository
{
    public interface IUserService
    {
        Task<ResponseWithData<IList<GetStudentDto>>> GetAllUser(string role);
        Task<ResponseWithData<GetStudentDto>> GetUserById(string role, string email);
        Task<Response> UpdateAdmin(UpdateAdminDto updateAdminDto, string role, string email);
        Task<Response> UpdateStudent(UpdateStudentDto updateStudentDto, string role, string email);
        Task<Response> DeleteUser(string role, string email);
    }
}
