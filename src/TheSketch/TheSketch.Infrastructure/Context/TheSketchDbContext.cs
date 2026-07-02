using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheSketch.Domain.Entities;

namespace TheSketch.Infrastructure.Context;

public class TheSketchDbContext: DbContext
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
    }
}
