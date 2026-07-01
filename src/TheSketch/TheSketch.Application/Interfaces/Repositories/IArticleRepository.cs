using System;
using System.Collections.Generic;
using System.Text;
using TheSketch.Domain.Entities;

namespace TheSketch.Application.Interfaces.Repositories;

public interface IArticleRepository
{
    Task AddAsync(Article article);
    void Update(Article article);
    void Delete(Article article);

    Task<Article?> GetByIdAsync(Guid id);
    Task<Article?> GetBySlugAsync(string slug);
    Task<IEnumerable<Article>> GetAllAsync(int page = 1, int pageSize = 10);
    Task<IEnumerable<Article>> GetByCategoryAsync(ArticleCategory category, int page = 1, int pageSize = 10);
    Task<IEnumerable<Article>> GetByTagAsync(string tag, int page = 1, int pageSize = 10);
    Task<IEnumerable<Article>> GetRelatedAsync(Guid currentArticleId, ArticleCategory category, List<string> tags, int count);

    Task<IEnumerable<Article>> SearchByTitleAsync(string searchTerm);
    Task<IEnumerable<Article>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

    Task<bool> ExistsAsync(Guid id);
    Task<int> CountAsync();

    Task SaveChangesAsync();
}
