global using MeDirectTest.Data;
global using Microsoft.EntityFrameworkCore;
using MeDirectTest.Controllers;
using MeDirectTest.Data.Repository.Rates;
using MeDirectTest.Data.Repository.User;
using MeDirectTest.Middleware.RateLimiting;
using MeDirectTest.Service.Rates;
using MeDirectTest.Service.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRateService, RateService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRateRepository, RateRepository>();

//Logging
builder.Services.AddScoped<ILogger, Logger<UserController>>();
builder.Services.AddScoped<ILogger, Logger<UserService>>();
builder.Services.AddScoped<ILogger, Logger<UserRepository>>();

builder.Services.AddScoped<ILogger, Logger<RateController>>();
builder.Services.AddScoped<ILogger, Logger<RateService>>();
builder.Services.AddScoped<ILogger, Logger<RateRepository>>();

//var loggerFactory = LoggerFactory.Create(builder =>
//{
//    builder
//        .AddFilter("Microsoft", LogLevel.Information)
//        .AddFilter("System", LogLevel.Information)
//        .AddFilter("TestRx.Program", LogLevel.Information)
//        .AddConsole();
//});

//ILogger logger = loggerFactory.CreateLogger<Program>();


//Rate Limiting
builder.Services.AddRateLimiting(builder.Configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRateLimiting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
