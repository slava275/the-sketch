using System.ComponentModel.DataAnnotations;

namespace TheSketch.Application.DTOs;

public class ArticleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Category { get; set; } = null!;
    public List<string> Tags { get; set; } = new();
    public byte TimeToRead { get; set; }
}
