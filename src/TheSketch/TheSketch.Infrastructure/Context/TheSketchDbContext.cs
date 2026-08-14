using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheSketch.Domain.Entities;

namespace TheSketch.Infrastructure.Context;

public class TheSketchDbContext : DbContext
{
    public TheSketchDbContext(DbContextOptions<TheSketchDbContext> options)
        : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserArticleBookmark> UserArticleBookmarks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TheSketchDbContext).Assembly);

        modelBuilder.Entity<User>()
            .HasData(
                new User
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Email = "admin@admin",
                    Role = "Admin",
                    PasswordHash = "$2a$11$TFHodhhcTrqDwycX/B53EekseJJm8silFkIigyYYD6MylgZFbszg2"
                });
    }
}
