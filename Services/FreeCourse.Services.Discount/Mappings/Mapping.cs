using AutoMapper;
using FreeCourse.Services.Discount.Dtos;
using FreeCourse.Services.Discount.Models;

namespace FreeCourse.Services.Discount.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<DiscountDto, Models.Discount>().ReverseMap();
        }
    }
}
