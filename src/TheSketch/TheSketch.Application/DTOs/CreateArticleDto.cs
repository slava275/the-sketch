using TheSketch.Domain.Entities;

namespace TheSketch.Application.DTOs;

public class CreateArticleDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string CoverImageUrl { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public ArticleCategory Category { get; set; }

    public List<string> Tags { get; set; } = new();

    public List<ArticleBlock> Blocks { get; set; } = new();
}