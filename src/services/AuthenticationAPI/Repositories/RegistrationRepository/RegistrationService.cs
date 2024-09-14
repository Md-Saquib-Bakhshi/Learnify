using AuthenticationAPI.Models;
using AuthenticationAPI.Models.RegistrationDTO.CreateUserDTO;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Repositories.RegistrationRepository
{
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegistrationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Response> RegisterAdminAsync(CreateAdminDto createAdminDto)
        {
            var userExists = await _userManager.FindByEmailAsync(createAdminDto.Email);

            if (userExists != null)
                return new Response { Status = "Error", Message = "User already exists!" };

            ApplicationUser admin = new()
            {
                Email = createAdminDto.Email,
                UserName = createAdminDto.Email,
                Name = createAdminDto.Name,
                Gender = createAdminDto.Gender,
                PhoneNumber = createAdminDto.Phone,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(admin, createAdminDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return new Response { Status = "Error", Message = "User creation failed! " + string.Join(", ", errors) };
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));


            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(admin, UserRoles.Admin);

            return new Response { Status = "Success", Message = "Admin created successfully!" };

        }

        public async Task<Response> RegisterStudentAsync(CreateStudentDto createStudentDto)
        {
            var userExists = await _userManager.FindByEmailAsync(createStudentDto.Email);

            if (userExists != null)
                return new Response { Status = "Error", Message = "User already exists!" };

            ApplicationUser student = new()
            {
                Email = createStudentDto.Email,
                UserName = createStudentDto.Email,
                Name = createStudentDto.Name,
                Domain = createStudentDto.Domain,
                Gender = createStudentDto.Gender,
                PhoneNumber = createStudentDto.Phone,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(student, createStudentDto.Password);

            if (!result.Succeeded)
                return new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." };

            else
            {
                if (!await _roleManager.RoleExistsAsync(UserRoles.Student))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Student));

                if (await _roleManager.RoleExistsAsync(UserRoles.Student))
                    await _userManager.AddToRoleAsync(student, UserRoles.Student);
            }
            return new Response { Status = "Success", Message = "User created successfully!" };

        }
    }
}
