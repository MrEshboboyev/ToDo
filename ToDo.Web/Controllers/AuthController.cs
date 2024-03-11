using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ToDo.Web.Models;
using ToDo.Web.Service.IService;
using ToDo.Web.Utility;

namespace ToDo.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            ResponseDto response = await _authService.LoginAsync(obj);
            if(response != null && response.IsSuccess)
            {
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>
                    (Convert.ToString(response.Result));

                TempData["success"] = "Login successfully!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", response.Message);
                return View(obj);
            }
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            List<SelectListItem> roleList = new()
            {
                new SelectListItem { Text = SD.RoleAdmin, Value = SD.RoleAdmin },
                new SelectListItem { Text = SD.RoleCustomer, Value = SD.RoleCustomer}
            };

            ViewBag.RoleList = roleList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            ResponseDto response = await _authService.RegisterAsync(obj);
            ResponseDto assignRole;
            if(response != null && response.IsSuccess)
            {
                // if role is not selected
                if(string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RoleCustomer;
                }

                assignRole = await _authService.AssignRoleAsync(obj);
                if(assignRole != null && assignRole.IsSuccess)
                {
                    TempData["success"] = "Registration Successfully";
                    return RedirectToAction(nameof(Login));
                }
            }

            List<SelectListItem> roleList = new()
            {
                new SelectListItem { Text = SD.RoleAdmin, Value = SD.RoleAdmin },
                new SelectListItem { Text = SD.RoleCustomer, Value = SD.RoleCustomer}
            };

            ViewBag.RoleList = roleList;

            return View(obj);  
        }
        
        [HttpGet]
        public IActionResult Logout()
        {
            return View();  
        }


    }
}
