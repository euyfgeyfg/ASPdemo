using System.Text.Json.Serialization;

namespace APIDemo.Entities;

public class Photo
{
    public int Id { get; set; }
    public required string Url { get; set; }
    public string? PublicId { get; set; }

    // 导航属性
    [JsonIgnore]
    public Member Member { get; set; } = null!;

    public string MemberId { get; set; } = null!;
}
