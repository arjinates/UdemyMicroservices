using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using FreeCourse.Services.Discount.Dtos;
using FreeCourse.Services.Discount.Models;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;

        public DiscountService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
            _mapper = mapper;   
        }

        public Task<Response<NoContent>> DeleteById(int id)
        {
            throw new NotImplementedException(); //TODO
        }

        public async Task<Response<List<DiscountDto>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("Select * from discount");
            return Response<List<DiscountDto>>.Success(_mapper.Map<List<DiscountDto>>(discounts), 200);
            
        }

        public Task<Response<DiscountDto>> GetByCodeAndUserId(string code, string userId)
        { 
            throw new NotImplementedException(); //TODO
        }

        public async Task<Response<DiscountDto>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync("Select * from discount where id=@Id", new { id })).SingleOrDefault();
            if (discount == null) 
            {
                return Response<DiscountDto>.Fail("Discount could not found", 404);
            }
            return Response<DiscountDto>.Success(_mapper.Map<DiscountDto>(discount), 200);
        }

        public Task<Response<NoContent>> Save(DiscountDto dto)
        {
            throw new NotImplementedException(); //TODO
        }

        public Task<Response<NoContent>> Update(DiscountDto dto)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
