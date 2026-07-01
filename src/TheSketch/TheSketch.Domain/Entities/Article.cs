using System;
using System.Collections.Generic;
using System.Text;

namespace TheSketch.Domain.Entities;

public class Article
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public byte TimeToRead { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ArticleCategory Category { get; set; }
    public List<string> Tags { get; set; } = new ();
    public List<ArticleBlock> Blocks { get; set; } = new();
}

public enum ArticleCategory
{
    World,
    Ukraine
}
