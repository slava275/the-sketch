using TheSketch.Domain.Entities;

namespace TheSketch.Application.DTOs;

public class ArticleDetailsDto : ArticleDto
{
    public List<ArticleBlock> Blocks { get; set; } = new();
}