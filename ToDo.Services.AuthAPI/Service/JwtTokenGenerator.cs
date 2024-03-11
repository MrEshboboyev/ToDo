using Microsoft.Extensions.Options;
using ToDo.Services.AuthAPI.Model;
using ToDo.Services.AuthAPI.Models;
using ToDo.Services.AuthAPI.Service.IService;

namespace ToDo.Services.AuthAPI.Service
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;

        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateJwtToken()
        {
            throw new NotImplementedException();
        }
    }
}
