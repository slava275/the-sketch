using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheSketch.Application.DTOs;
using TheSketch.Application.Interfaces.Services;
using TheSketch.Domain.Entities;

namespace TheSketch.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _articleService;

    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    // GET: api/articles?page=1&pageSize=10
    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<ArticleDto>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _articleService.GetAllAsync(page, pageSize);
        return Ok(result);
    }

    // GET: api/articles/slug/naczionalna-identychnist
    [HttpGet("slug/{slug}")]
    public async Task<ActionResult<ArticleDetailsDto>> GetBySlug(string slug)
    {
        var article = await _articleService.GetBySlugAsync(slug);

        if (article == null)
            return NotFound(new { message = $"Статтю зі слагом '{slug}' не знайдено." });

        return Ok(article);
    }

    // GET: api/articles/category/Ukraine?page=1&pageSize=6
    [HttpGet("category/{category}")]
    public async Task<ActionResult<PagedResponseDto<ArticleDto>>> GetByCategory(
        ArticleCategory category,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _articleService.GetByCategoryAsync(category, page, pageSize);
        return Ok(result);
    }

    // GET: api/articles/tag/мистецтво?page=1&pageSize=10
    [HttpGet("tag/{tag}")]
    public async Task<ActionResult<PagedResponseDto<ArticleDto>>> GetByTag(
        string tag,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _articleService.GetByTagAsync(tag, page, pageSize);
        return Ok(result);
    }

    // GET: api/articles/search?searchTerm=виставка
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<ArticleDto>>> Search([FromQuery] string searchTerm)
    {
        var result = await _articleService.SearchAsync(searchTerm);
        return Ok(result);
    }

    // GET: api/articles/d3b07384.../related?count=3
    [HttpGet("{id:guid}/related")]
    public async Task<ActionResult<IEnumerable<ArticleDto>>> GetRelated(Guid id, [FromQuery] int count = 3)
    {
        var result = await _articleService.GetRelatedArticlesAsync(id, count);
        return Ok(result);
    }

    // POST: api/articles
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ArticleDto>> Create([FromBody] CreateArticleDto dto)
    {
        var createdArticle = await _articleService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetBySlug), new { slug = createdArticle.Slug }, createdArticle);
    }

    // PUT: api/articles/d3b07384-e29b-433c-a9b0-4578550e8400
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ArticleDto>> Update(Guid id, [FromBody] UpdateArticleDto dto)
    {
        var updatedArticle = await _articleService.UpdateAsync(id, dto);
        return Ok(updatedArticle);
    }

    // DELETE: api/articles/d3b07384-e29b-433c-a9b0-4578550e8400
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _articleService.DeleteAsync(id);
        return NoContent();
    }
}