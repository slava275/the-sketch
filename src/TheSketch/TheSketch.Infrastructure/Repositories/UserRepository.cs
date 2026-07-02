using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TheSketch.Application.Interfaces.Repositories;
using TheSketch.Domain.Entities;
using TheSketch.Infrastructure.Context;

namespace TheSketch.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TheSketchDbContext _context;

    public UserRepository(TheSketchDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower(CultureInfo.CurrentCulture) == email.ToLower(CultureInfo.CurrentCulture).Trim());
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<IEnumerable<UserArticleBookmark>> GetBookmarksAsync(Guid userId, int pageNumber, int pageSize)
    {
        return await _context.UserArticleBookmarks
            .AsNoTracking()
            .Include(ub => ub.Article)
            .Where(ub => ub.UserId == userId)
            .OrderByDescending(ub => ub.BookmarkedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<UserArticleBookmark?> GetBookmarkAsync(Guid userId, Guid articleId)
    {
        return await _context.UserArticleBookmarks
            .AsNoTracking()
            .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.ArticleId == articleId);
    }

    public async Task<bool> IsBookmarkedAsync(Guid userId, Guid articleId)
    {
        return await _context.UserArticleBookmarks
            .AnyAsync(ub => ub.UserId == userId && ub.ArticleId == articleId);
    }

    public async Task AddBookmarkAsync(UserArticleBookmark bookmark)
    {
        await _context.UserArticleBookmarks.AddAsync(bookmark);
    }

    public void RemoveBookmark(UserArticleBookmark bookmark)
    {
        _context.UserArticleBookmarks.Remove(bookmark);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
