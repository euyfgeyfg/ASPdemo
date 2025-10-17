using System;
using System.ComponentModel.DataAnnotations;

namespace APIDemo.DTOs;

public class RegisterDto
{
    [Required]
    public string Name { get; set; } = "";
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";
    [Required]
    [MinLength(4)]
    public string Password { get; set; } = "";

}
