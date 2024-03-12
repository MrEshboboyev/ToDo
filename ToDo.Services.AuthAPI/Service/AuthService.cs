using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
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
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                // if role not found, create a new role
                if(!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole { Name = roleName }).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }

            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {

            ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.Username.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (user == null || !isValid)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };
            }

            UserDto userDto = new()
            {
                Email = user.Email,
                Name = user.Name,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber
            };

            // if user was found, generated JWT token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateJwtToken(user, roles);

            LoginResponseDto loginResponseDto = new()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                Email = registrationRequestDto.Email,
                UserName = registrationRequestDto.Email,
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
