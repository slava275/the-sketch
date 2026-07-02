using System;
using System.Collections.Generic;
using System.Text;

namespace TheSketch.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Role { get; set; } = "User";

    public List<UserArticleBookmark> Bookmarks { get; set; } = new();
}
