using System;
using APIDemo.DTOs;
using APIDemo.Entities;
using APIDemo.Interfaces;

namespace APIDemo.Extensions;

// AppUser的扩展方法
public static class AppUserExtensions
{
    public static UserDto ToDto(this AppUser user, ITokenService tokenService)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            ImageUrl = user.ImageUrl,
            Token = tokenService.CreateToken(user)
        };
    }
}
