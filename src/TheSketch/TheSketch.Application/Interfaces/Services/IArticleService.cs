using TheSketch.Application.DTOs;
using TheSketch.Domain.Entities;

namespace TheSketch.Application.Interfaces.Services;

public interface IArticleService
{
    Task<ArticleDetailsDto?> GetBySlugAsync(string slug);
    Task<PagedResponseDto<ArticleDto>> GetAllAsync(int page, int pageSize);
    Task<PagedResponseDto<ArticleDto>> GetByCategoryAsync(ArticleCategory category, int page, int pageSize);
    Task<PagedResponseDto<ArticleDto>> GetByTagAsync(string tag, int page, int pageSize);
    Task<PagedResponseDto<ArticleDto>> SearchAsync(string searchTerm, int page, int pageSize);
    Task<IEnumerable<ArticleDto>> GetRelatedArticlesAsync(Guid articleId, int count = 3);

    Task<ArticleDto> CreateAsync(CreateArticleDto dto);
    Task<ArticleDto> UpdateAsync(Guid id, UpdateArticleDto dto);
    Task DeleteAsync(Guid id);
}