using FreeCourse.Services.Discount.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<DiscountDto>>> GetAll();
        Task<Response<DiscountDto>> GetById(int id);
        Task<Response<NoContent>> Save(DiscountDto dto);
        Task<Response<NoContent>> DeleteById(int id);
        Task<Response<NoContent>> Update(DiscountDto dto);
        Task<Response<DiscountDto>> GetByCodeAndUserId(string code, string userId);
    }
}
