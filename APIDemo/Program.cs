using System.Text;
using APIDemo.Data;
using APIDemo.Interfaces;
using APIDemo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnetion"));
});

builder.Services.AddCors();

// 服务设置接口
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var tokenKey = builder.Configuration["TokenKey"]
        ?? throw new Exception("Token key not found - Program.cs");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));
}


app.UseAuthentication();// 你是谁
app.UseAuthorization();// 你是谁之后，判断你是否有权限做某事情。

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
