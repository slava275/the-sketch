using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TheSketch.Domain.Entities;

namespace TheSketch.Infrastructure.Configurations;

public class UserArticleBookmarkConfiguration : IEntityTypeConfiguration<UserArticleBookmark>
{
    public void Configure(EntityTypeBuilder<UserArticleBookmark> builder)
    {
        builder.HasKey(b => new { b.UserId, b.ArticleId });

        builder.HasOne(b => b.User)
            .WithMany(u => u.Bookmarks)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.Article)
            .WithMany(a => a.BookmarkedByUsers)
            .HasForeignKey(b => b.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
