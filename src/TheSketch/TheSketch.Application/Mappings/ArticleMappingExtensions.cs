using System;
using System.Collections.Generic;
using System.Linq;
using TheSketch.Application.DTOs;
using TheSketch.Domain.Entities;

namespace TheSketch.Application.Mappings;

public static class ArticleMappingExtensions
{
    public static ArticleDto ToDto(this Article article)
    {
        if (article == null) throw new ArgumentNullException(nameof(article));

        return new ArticleDto
        {
            Id = article.Id,
            Title = article.Title,
            Slug = article.Slug,
            Description = article.Description,
            CoverImageUrl = article.CoverImageUrl,
            CreatedAt = article.CreatedAt,
            Category = article.Category.ToString(),
            Tags = article.Tags ?? new List<string>(),
            TimeToRead = article.TimeToRead
        };
    }

    public static ArticleDetailsDto ToDetailsDto(this Article article)
    {
        if (article == null) throw new ArgumentNullException(nameof(article));

        return new ArticleDetailsDto
        {
            Id = article.Id,
            Title = article.Title,
            Slug = article.Slug,
            Description = article.Description,
            CoverImageUrl = article.CoverImageUrl,
            CreatedAt = article.CreatedAt,
            Category = article.Category.ToString(),
            Tags = article.Tags ?? new List<string>(),
            TimeToRead = article.TimeToRead,
            Blocks = article.Blocks ?? new List<ArticleBlock>()
        };
    }

    public static Article ToEntity(this CreateArticleDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        return new Article
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            CoverImageUrl = dto.CoverImageUrl,
            Category = dto.Category,
            Tags = dto.Tags ?? new List<string>(),
            Blocks = dto.Blocks ?? new List<ArticleBlock>(),
            CreatedAt = dto.CreatedAt ?? DateTime.UtcNow
        };
    }

    public static void UpdateFromDto(this Article article, UpdateArticleDto dto)
    {
        if (article == null) throw new ArgumentNullException(nameof(article));
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        article.Title = dto.Title;
        article.Description = dto.Description;
        article.CoverImageUrl = dto.CoverImageUrl;
        article.Category = dto.Category;
        article.Tags = dto.Tags ?? new List<string>();
        article.Blocks = dto.Blocks ?? new List<ArticleBlock>();
    }
}