using ToDo.Web.Models;
using ToDo.Web.Service.IService;

namespace ToDo.Web.Service
{
    public class BaseService : IBaseService
    {
        public Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            throw new NotImplementedException();
        }
    }
}
