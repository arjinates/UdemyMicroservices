using FreeCourse.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
