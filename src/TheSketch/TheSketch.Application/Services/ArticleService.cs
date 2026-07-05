using AnyAscii;
using TheSketch.Application.DTOs;
using TheSketch.Application.Interfaces.Repositories;
using TheSketch.Application.Interfaces.Services;
using TheSketch.Application.Mappings;
using TheSketch.Domain.Entities;
using TheSketch.Domain.Exceptions;

namespace TheSketch.Application.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<ArticleDto> CreateAsync(CreateArticleDto dto)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(dto));

        var article = dto.ToEntity();

        article.Slug = GenerateSlug(article.Title);
        article.CalculateTimeToRead();

        await _articleRepository.AddAsync(article);
        await _articleRepository.SaveChangesAsync();

        return article.ToDto();
    }

    public async Task DeleteAsync(Guid id)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        if (article == null)
            throw new EntityNotFoundException($"Article with ID {id} not found");

        _articleRepository.Delete(article);
        await _articleRepository.SaveChangesAsync();
    }

    public async Task<PagedResponseDto<ArticleDto>> GetAllAsync(int page, int pageSize)
    {
        int totalItems = await _articleRepository.CountAsync();

        var articles = await _articleRepository.GetAllAsync(page, pageSize);

        var articleDtos = articles.Select(a => a.ToDto());

        return new PagedResponseDto<ArticleDto>(articleDtos, totalItems, page, pageSize);
    }

    public async Task<PagedResponseDto<ArticleDto>> GetByCategoryAsync(ArticleCategory category, int page, int pageSize)
    {
        int totalItems = await _articleRepository.CountAsync();

        var articles = await _articleRepository.GetByCategoryAsync(category, page, pageSize);

        var articleDtos = articles.Select(a => a.ToDto());

        return new PagedResponseDto<ArticleDto>(articleDtos, totalItems, page, pageSize);
    }

    public async Task<ArticleDetailsDto?> GetBySlugAsync(string slug)
    {
        var article = await _articleRepository.GetBySlugAsync(slug);
        if (article == null)
            throw new EntityNotFoundException($"Article with slug {slug} not found");

        return article.ToDetailsDto();
    }

    public async Task<PagedResponseDto<ArticleDto>> GetByTagAsync(string tag, int page, int pageSize)
    {
        int totalItems = await _articleRepository.CountAsync();

        var articles = await _articleRepository.GetByTagAsync(tag, page, pageSize);

        var articleDtos = articles.Select(a => a.ToDto());

        return new PagedResponseDto<ArticleDto>(articleDtos, totalItems, page, pageSize);
    }

    public async Task<IEnumerable<ArticleDto>> GetRelatedArticlesAsync(Guid articleId, int count = 3)
    {
        var currentArticle = await _articleRepository.GetByIdAsync(articleId);
        if (currentArticle == null)
            return Enumerable.Empty<ArticleDto>();

        var relatedArticles = await _articleRepository.GetRelatedAsync(
            articleId,
            currentArticle.Category,
            currentArticle.Tags,
            count
        );

        return relatedArticles.Select(a => a.ToDto());
    }

    public async Task<PagedResponseDto<ArticleDto>> SearchAsync(string searchTerm, int page, int pageSize)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new PagedResponseDto<ArticleDto>(Enumerable.Empty<ArticleDto>(), 0, page, pageSize);

        var articles = await _articleRepository.SearchByTitleAsync(searchTerm.Trim(), page, pageSize);

        return new PagedResponseDto<ArticleDto>(articles.Select(a => a.ToDto()), articles.Count(), page, pageSize);
    }

    public async Task<ArticleDto> UpdateAsync(Guid id, UpdateArticleDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        var article = await _articleRepository.GetByIdAsync(id);
        if (article == null)
            throw new EntityNotFoundException($"Article with ID {id} not found");

        if (!string.Equals(article.Title, dto.Title, StringComparison.OrdinalIgnoreCase))
        {
            article.Slug = GenerateSlug(dto.Title);
        }

        article.UpdateFromDto(dto);

        article.CalculateTimeToRead();

        _articleRepository.Update(article);
        await _articleRepository.SaveChangesAsync();

        return article.ToDto();
    }

    private string GenerateSlug(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Guid.NewGuid().ToString()[..8];

        string slug = title.Transliterate();

        slug = slug.ToLower().Trim();

        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\s-]", "");

        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s+", "-").Trim('-');

        return $"{slug}-{Guid.NewGuid().ToString()[..5]}";
    }
}
