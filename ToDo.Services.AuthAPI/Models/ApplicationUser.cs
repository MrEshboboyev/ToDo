using Microsoft.AspNetCore.Identity;

namespace ToDo.Services.AuthAPI.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Nickname { get; set; }
    }
}
