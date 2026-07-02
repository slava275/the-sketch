using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheSketch.Domain.Entities;

namespace TheSketch.Application.Interfaces.Repositories;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task<IEnumerable<UserArticleBookmark>> GetBookmarksAsync(Guid userId, int pageNumber, int pageSize);
    Task<UserArticleBookmark?> GetBookmarkAsync(Guid userId, Guid articleId);
    Task<bool> IsBookmarkedAsync(Guid userId, Guid articleId);
    Task AddBookmarkAsync(UserArticleBookmark bookmark);
    void RemoveBookmark(UserArticleBookmark bookmark);
    Task<bool> SaveChangesAsync();
}
