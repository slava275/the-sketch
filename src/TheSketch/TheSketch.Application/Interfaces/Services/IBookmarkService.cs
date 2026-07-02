using System;
using System.Collections.Generic;
using System.Text;
using TheSketch.Application.DTOs;

namespace TheSketch.Application.Interfaces.Services;

public interface IBookmarkService
{
    Task AddToBookmarksAsync(Guid userId, Guid articleId);
    Task RemoveFromBookmarksAsync(Guid userId, Guid articleId);
    Task<IEnumerable<BookmarkedArticleDto>> GetUserBookmarksAsync(Guid userId, int pageNumber, int pageSize);
}
