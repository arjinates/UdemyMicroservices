using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public Task<UserViewModel> GetUser()
        {
            throw new NotImplementedException();
        }
    }
}
