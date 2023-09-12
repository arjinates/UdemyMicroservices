using FreeCourse.Services.Basket.Services;
using FreeCourse.Services.Basket.Settings;
using FreeCourse.Shared.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//shared/services/sharedIdentityService.cs de inject ettigim context accessor burada calisabilsin diye ekliyorum
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService,SharedIdentityService>(); //service'i de DI container'e ekliyorum

builder.Services.AddScoped<IBasketService,BasketService>();
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));
builder.Services.AddSingleton(serviceProvider =>
{
    var redisSetttings = serviceProvider.GetRequiredService<IOptions<RedisSettings>>().Value;

    var redis = new RedisService(redisSetttings.Host, redisSetttings.Port);

    redis.Connect();

    return redis;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
