global using MeDirectTest.Data;
global using Microsoft.EntityFrameworkCore;
using AspNetCoreRateLimit;
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

//Rate Limiting
builder.Services.AddRateLimiting(builder.Configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use Rate Limiting
app.UseRateLimiting();
//app.UseClientRateLimiting();


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
