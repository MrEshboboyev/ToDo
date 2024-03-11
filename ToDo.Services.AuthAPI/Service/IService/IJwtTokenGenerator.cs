using ToDo.Services.AuthAPI.Model;

namespace ToDo.Services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(ApplicationUser applicationUser);
    }
}
