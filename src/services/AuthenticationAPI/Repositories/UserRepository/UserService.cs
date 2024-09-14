using AuthenticationAPI.Models.RegistrationDTO.GetUserDTO;
using AuthenticationAPI.Models.RegistrationDTO.UpdateUserDTO;
using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Repositories.UserRepository
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _adminManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<ApplicationUser> adminManager, RoleManager<IdentityRole> roleManager)
        {
            _adminManager = adminManager;
            _roleManager = roleManager;
        }

        public async Task<ResponseWithData<IList<GetStudentDto>>> GetAllUser(string role)
        {
            if (await _roleManager.RoleExistsAsync(role))
            {
                var users = await _adminManager.GetUsersInRoleAsync(role);

                var getUsers = users.Select(user => new GetStudentDto
                {
                    Name = user.Name,
                    Email = user.Email,
                    Gender = user.Gender,
                    Phone = user.PhoneNumber,
                    Domain = user.Domain
                }).ToList();

                return new ResponseWithData<IList<GetStudentDto>>
                {
                    Status = "Success",
                    Message = "Users retrieved successfully.",
                    Data = getUsers
                };
            }
            return new ResponseWithData<IList<GetStudentDto>>
            {
                Status = "Error",
                Message = "No users found.",
                Data = null
            };
        }

        public async Task<ResponseWithData<GetStudentDto>> GetUserById(string role, string email)
        {
            var user = await _adminManager.FindByEmailAsync(email);
            if (user != null && await _adminManager.IsInRoleAsync(user, role))
            {
                var getUser = new GetStudentDto
                {
                    Name = user.Name,
                    Email = user.Email,
                    Gender = user.Gender,
                    Phone = user.PhoneNumber,
                    Domain = user.Domain
                };

                return new ResponseWithData<GetStudentDto>
                {
                    Status = "Success",
                    Message = "User found successfully.",
                    Data = getUser
                };
            }
            return new ResponseWithData<GetStudentDto>
            {
                Status = "Error",
                Message = "User not found.",
                Data = null
            };
        }

        public async Task<Response> UpdateAdmin(UpdateAdminDto updateAdminDto, string role, string email)
        {
            if (await _roleManager.RoleExistsAsync(role))
            {
                var user = await _adminManager.FindByEmailAsync(email);
                if (user != null && await _adminManager.IsInRoleAsync(user, role))
                {
                    user.Name = updateAdminDto.Name;
                    user.Gender = updateAdminDto.Gender;
                    user.PhoneNumber = updateAdminDto.Phone;

                    var result = await _adminManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return new Response
                        {
                            Status = "Success",
                            Message = "Admin updated successfully."
                        };
                    }
                }
            }
            return new Response
            {
                Status = "Error",
                Message = "Admin update failed."
            };
        }

        public async Task<Response> UpdateStudent(UpdateStudentDto updateStudentDto, string role, string email)
        {
            if (await _roleManager.RoleExistsAsync(role))
            {
                var user = await _adminManager.FindByEmailAsync(email);
                if (user != null && await _adminManager.IsInRoleAsync(user, role))
                {
                    user.Name = updateStudentDto.Name;
                    user.Gender = updateStudentDto.Gender;
                    user.Domain = updateStudentDto.Domain;
                    user.PhoneNumber = updateStudentDto.Phone;

                    var result = await _adminManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return new Response
                        {
                            Status = "Success",
                            Message = "Student updated successfully."
                        };
                    }
                }
            }
            return new Response
            {
                Status = "Error",
                Message = "Student update failed."
            };
        }

        public async Task<Response> DeleteUser(string role, string email)
        {
            if (await _roleManager.RoleExistsAsync(role))
            {
                var user = await _adminManager.FindByEmailAsync(email);
                if (user != null && await _adminManager.IsInRoleAsync(user, role))
                {
                    var resultRole = await _adminManager.RemoveFromRoleAsync(user, role);
                    var resultUser = await _adminManager.DeleteAsync(user);

                    if (resultRole.Succeeded && resultUser.Succeeded)
                    {
                        return new Response
                        {
                            Status = "Success",
                            Message = "User deleted successfully."
                        };
                    }
                }
            }
            return new Response
            {
                Status = "Error",
                Message = "User deletion failed."
            };
        }
    }
}
