using ToDo.Services.AuthAPI.Models.Dto;
using ToDo.Services.AuthAPI.Service.IService;

namespace ToDo.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
