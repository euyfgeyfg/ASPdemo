using System;

namespace APIDemo.DTOs;

public class UserDto
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
    public string? ImageUrl { get; set; }
    public required string Token { get; set; }
}
