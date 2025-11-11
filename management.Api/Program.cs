using System.Text;
using management.Application.DTOs;
using management.Application.Interfaces;
using management.Application.Services;
using management.Domain.Entities;
using management.Domain.Interfaces;
using management.Infrastructure.Data;
using management.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("Default");
if (string.IsNullOrEmpty(connection))
{
    // Para desarrollo local
    connection = "Server=localhost;Database=management_db;Uid=root;Pwd=;";
}
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

builder.Services.AddAutoMapper(typeof(MapProfile));

// Dependency Injection
builder.Services.AddScoped<IRepository<Product>, ProductsRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();

var jwtKey = builder.Configuration["Jwt:Key"] ?? "DefaultKeyForDevelopmentChangeInProduction12345";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "management-api";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "management-users";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();