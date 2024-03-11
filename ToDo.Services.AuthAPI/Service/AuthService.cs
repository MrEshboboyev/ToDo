using Microsoft.AspNetCore.Identity;
using ToDo.Services.AuthAPI.Data;
using ToDo.Services.AuthAPI.Model;
using ToDo.Services.AuthAPI.Models.Dto;
using ToDo.Services.AuthAPI.Service.IService;

namespace ToDo.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(AppDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                Email = registrationRequestDto.Email,
                PhoneNumber = registrationRequestDto.PhoneNumber,
                Name = registrationRequestDto.Name,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Nickname = registrationRequestDto.Nickname
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    return "";
                }
                else
                {
                    return result.Errors.First().Description;
                }
            }
            catch (Exception ex)
            {
                
            }

            return "Error Encountered";
        }
    }
}
