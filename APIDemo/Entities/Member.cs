using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIDemo.Entities;

public class Member
{
    public string Id { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string? ImageUrl { get; set; }
    public required string DispalyName { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public required string Gender { get; set; }
    public string? Description { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }

    // 导航属性
    [JsonIgnore]
    public List<Photo> Photos { get; set; } = [];

    // 指向AppUser的属性
    [ForeignKey(nameof(Id))] //这个ID是AppUser的主键，也是Memeber的外键加主键
    [JsonIgnore]
    public AppUser User { get; set; } = null!;
}
