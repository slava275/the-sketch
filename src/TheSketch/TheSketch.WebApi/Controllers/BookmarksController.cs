using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TheSketch.Application.Interfaces.Services;

namespace TheSketch.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BookmarksController : ControllerBase
{
    private readonly IBookmarkService _bookmarkService;

    public BookmarksController(IBookmarkService bookmarkService)
    {
        _bookmarkService = bookmarkService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyBookmarks(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1 || pageSize > 50) pageSize = 10;

        var userId = GetCurrentUserId();
        var result = await _bookmarkService.GetUserBookmarksAsync(userId, pageNumber, pageSize);

        return Ok(result);
    }

    [HttpPost("{articleId:guid}")]
    public async Task<IActionResult> Add(Guid articleId)
    {
        var userId = GetCurrentUserId();
        await _bookmarkService.AddToBookmarksAsync(userId, articleId);

        return Ok();
    }

    [HttpDelete("{articleId:guid}")]
    public async Task<IActionResult> Remove(Guid articleId)
    {
        var userId = GetCurrentUserId();
        await _bookmarkService.RemoveFromBookmarksAsync(userId, articleId);

        return NoContent();
    }

    private Guid GetCurrentUserId()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
        {
            throw new UnauthorizedAccessException("User not authenticated or invalid token.");
        }

        return Guid.Parse(userIdString);
    }
}