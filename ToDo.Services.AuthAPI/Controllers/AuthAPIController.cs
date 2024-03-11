using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Services.AuthAPI.Models.Dto;
using ToDo.Services.AuthAPI.Service.IService;

namespace ToDo.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if(loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password incorrect!";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;

            return Ok(_response);
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequestDto model)
        {
            var result = await _authService.Register(model);
            if(!string.IsNullOrEmpty(result))
            {
                _response.IsSuccess = false;
                _response.Message = result;
            }

            return Ok(_response);
        }
    }
}
