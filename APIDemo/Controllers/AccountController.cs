using System.Security.Cryptography;
using System.Text;
using APIDemo.Data;
using APIDemo.DTOs;
using APIDemo.Entities;
using APIDemo.Extensions;
using APIDemo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIDemo.Controllers;

public class AccountController(AppDbContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
    {
        if (await EmailExists(registerDto.Email))
        {
            return BadRequest("Email taken");
        }

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user.ToDto(tokenService);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
    {
        var user = await context.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Email);
        if (user == null)
        {
            return Unauthorized("Invalid email address");
        }

        using var hamc = new HMACSHA512(user.PasswordSalt);
        // 通过hamc的哈希，指定salt（盐值）哈希，然后对两个密码进行比对。
        var computedHash = hamc.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (var i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Invalid Password");
            }
        }

        return user.ToDto(tokenService);
    }


    private async Task<bool> EmailExists(string email)
    {
        return await context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
    }

}
