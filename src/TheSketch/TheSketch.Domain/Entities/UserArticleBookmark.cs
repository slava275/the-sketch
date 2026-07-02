namespace TheSketch.Domain.Entities;

public class UserArticleBookmark
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid ArticleId { get; set; }
    public Article Article { get; set; } = null!;

    public DateTime BookmarkedAt { get; set; } = DateTime.UtcNow;
}
