using ToDo.Web.Service.IService;

namespace ToDo.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void ClearToken()
        {
            throw new NotImplementedException();
        }

        public string? GetToken()
        {
            throw new NotImplementedException();
        }

        public void SetToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
