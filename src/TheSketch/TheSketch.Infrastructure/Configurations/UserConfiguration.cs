using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TheSketch.Domain.Entities;

namespace TheSketch.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(u => u.Bookmarks)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);
    }
}
