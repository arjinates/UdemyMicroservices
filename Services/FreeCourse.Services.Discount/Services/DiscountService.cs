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

        public async Task<Response<NoContent>> DeleteById(int id)
        {
FixedArgumentsEncoder:            if ((await _dbConnection.QueryAsync("Select * from discount where id=@Id", new { Id=id })).FirstOrDefault() == null)
            {
                return Response<NoContent>.Fail("Discount could not found!", 404);
            }
            var status = await _dbConnection.ExecuteAsync("DELETE FROM discount WHERE id=@Id", new { Id=id });

            return status>0 ? Response<NoContent>.Success(204)  : Response<NoContent>.Fail("An ERROR occured while deleting.", 500);
        }

        public async Task<Response<List<DiscountDto>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("Select * from discount");
            return Response<List<DiscountDto>>.Success(_mapper.Map<List<DiscountDto>>(discounts), 200);
        }

        public async Task<Response<DiscountDto>> GetByCodeAndUserId(string code, string userId)
        {
            if (code == null || userId == null)
            {
                return Response<DiscountDto>.Fail("Code and userId can not be empty.", 400);
            }
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount WHERE code=@Code and userid=@UserId",
                new { Code = code, UserId = userId })).SingleOrDefault();

            return discount == null ? Response<DiscountDto>.Fail("Discount could not found.", 404) : 
                Response<DiscountDto>.Success(_mapper.Map<DiscountDto>(discount), 200);
        }

        public async Task<Response<DiscountDto>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("Select * from discount where id=@Id", new { Id=id })).SingleOrDefault();
            return discount==null ? Response<DiscountDto>.Fail("Discount could not found", 404) :
                Response<DiscountDto>.Success(_mapper.Map<DiscountDto>(discount), 200);
        }

        public async Task<Response<NoContent>> Save(DiscountDto dto)
        {
            var status = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)", _mapper.Map<Models.Discount>(dto));
            return status>0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("An ERROR occured while adding",500);
        }

        public async Task<Response<NoContent>> Update(DiscountDto dto)
        {
            if ((await _dbConnection.QueryAsync("Select * from discount where id=@Id", new { Id=dto.Id })).FirstOrDefault()==null)
            {
                return Response<NoContent>.Fail("Discount could not found!", 404);
            }
            var status = await _dbConnection.ExecuteAsync("update discount set userid=@UserId, code=@Code, rate=@Rate where id=@Id",
                new { Id=dto.Id, UserId=dto.UserId, Rate=dto.Rate, Code=dto.Code }); //tek tek de verebilirim

            return status>0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("An ERROR occured while updating", 500);
        }
    }
}
