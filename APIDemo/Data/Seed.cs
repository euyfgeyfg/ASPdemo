using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using APIDemo.DTOs;
using APIDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIDemo.Data;

public class Seed
{
    public static async Task SeedUsers(AppDbContext context)
    {
        // 如果能从数据库中拿到任意数据，代表数据库不为空，就直接返回
        if (await context.Users.AnyAsync()) return;

        var memberData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        var members = JsonSerializer.Deserialize<List<SeedUserDto>>(memberData);

        if (members == null)
        {
            Console.WriteLine("No members in seed data");
            return;
        }

        foreach (var member in members)
        {
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                Id = member.Id,
                Email = member.Email,
                Name = member.DisplayName,
                ImageUrl = member.ImageUrl,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")),
                PasswordSalt = hmac.Key,
                Member = new Member
                {
                    Id = member.Id,
                    DispalyName = member.DisplayName,
                    Description = member.Description,
                    DateOfBirth = member.DateOfBirth,
                    ImageUrl = member.ImageUrl,
                    Gender = member.Gender,
                    City = member.City,
                    Country = member.Country,
                    LastActive = member.LastActive,
                    Created = member.Created
                }
            };

            user.Member.Photos.Add(new Photo
            {
                Url = member.ImageUrl!,
                MemberId = member.Id
            });

            // 将修改都存入到内存中
            context.Users.Add(user);
        }

        // 异步执行SQL操作，将数据存入到数据库（SQLServer）中
        await context.SaveChangesAsync();

    }
}
