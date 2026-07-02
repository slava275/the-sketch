using TheSketch.Application.DTOs;
using TheSketch.Application.Interfaces.Repositories;
using TheSketch.Application.Interfaces.Services;
using TheSketch.Domain.Entities;
using TheSketch.Domain.Exceptions;

namespace TheSketch.Application.Services;

public class BookmarkService : IBookmarkService
{
    private readonly IUserRepository _userRepository;
    private readonly IArticleRepository _articleRepository;

    public BookmarkService(IUserRepository userRepository, IArticleRepository articleRepository)
    {
        _userRepository = userRepository;
        _articleRepository = articleRepository;
    }

    public async Task AddToBookmarksAsync(Guid userId, Guid articleId)
    {
        var articleExists = await _articleRepository.ExistsAsync(articleId);
        if (!articleExists)
        {
            throw new EntityNotFoundException($"Article with ID {articleId} not found.");
        }

        var isAlreadyBookmarked = await _userRepository.IsBookmarkedAsync(userId, articleId);
        if (isAlreadyBookmarked)
        {
            return; // Already bookmarked, no action needed
        }

        var bookmark = new UserArticleBookmark
        {
            UserId = userId,
            ArticleId = articleId,
            BookmarkedAt = DateTime.UtcNow
        };

        await _userRepository.AddBookmarkAsync(bookmark);
        await _userRepository.SaveChangesAsync();
    }

    public async Task RemoveFromBookmarksAsync(Guid userId, Guid articleId)
    {
        var bookmarkToRemove = await _userRepository.GetBookmarkAsync(userId, articleId);

        if (bookmarkToRemove == null)
        {
            throw new EntityNotFoundException("Article is not found in bookmarks.");
        }

        _userRepository.RemoveBookmark(bookmarkToRemove);
        await _userRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<BookmarkedArticleDto>> GetUserBookmarksAsync(Guid userId, int pageNumber, int pageSize)
    {
        var bookmarks = await _userRepository.GetBookmarksAsync(userId, pageNumber, pageSize);

        return bookmarks.Select(b => new BookmarkedArticleDto(
            b.ArticleId,
            b.Article.Title,
            b.Article.Slug,
            b.Article.Description,
            b.BookmarkedAt
        ));
    }
}