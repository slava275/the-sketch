using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheSketch.Application.Interfaces.Repositories;
using TheSketch.Domain.Entities;
using TheSketch.Infrastructure.Context;

namespace TheSketch.Infrastructure.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly TheSketchDbContext context;

    public ArticleRepository(TheSketchDbContext context)
    {
        this.context = context;
    }

    public async Task AddAsync(Article article)
    {
        await context.Articles.AddAsync(article);
    }

    public void Delete(Article article)
    {
        context.Articles.Remove(article);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await context.Articles
            .AnyAsync(x => x.Id == id);
    }

    public async Task<int> CountAsync()
    {
        return await context.Articles.CountAsync();
    }

    public async Task<IEnumerable<Article>> GetRelatedAsync(Guid currentArticleId, ArticleCategory category, List<string> tags, int count)
    {
        return await context.Articles
            .AsNoTracking()
            .Where(x => x.Id != currentArticleId &&
                       (x.Category == category || x.Tags.Any(t => tags.Contains(t))))
            .OrderByDescending(x => x.CreatedAt)
            .Take(count)
            .ToListAsync();
    }

    public async Task<IEnumerable<Article>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        return await context.Articles
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Article>> GetByCategoryAsync(ArticleCategory category, int page = 1, int pageSize = 10)
    {
        return await context.Articles
            .AsNoTracking()
            .Where(x => x.Category == category)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Article>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await context.Articles
            .AsNoTracking()
            .Where(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate)
            .ToListAsync();
    }

    public async Task<Article?> GetByIdAsync(Guid id)
    {
        return await context.Articles.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Article?> GetBySlugAsync(string slug)
    {
        return await context.Articles.FirstOrDefaultAsync(x => x.Slug == slug);
    }

    public async Task<IEnumerable<Article>> GetByTagAsync(string tag, int page = 1, int pageSize = 10)
    {
        return await context.Articles
            .AsNoTracking()
            .Where(x => x.Tags.Any(t => t == tag))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Article>> SearchByTitleAsync(string searchTerm)
    {
        return await context.Articles
            .AsNoTracking()
            .Where(x => EF.Functions.ILike(x.Title, $"%{searchTerm}%"))
            .ToListAsync();
    }

    public void Update(Article article)
    {
        context.Articles.Update(article);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
