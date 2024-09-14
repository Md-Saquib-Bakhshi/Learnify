using AuthenticationAPI.Models;
using AuthenticationAPI.Models.RegistrationDTO.CreateUserDTO;

namespace AuthenticationAPI.Repositories.RegistrationRepository
{
    public interface IRegistrationService
    {
        Task<Response> RegisterAdminAsync(CreateAdminDto createAdminDto);
        Task<Response> RegisterStudentAsync(CreateStudentDto createStudentDto);
    }
}
