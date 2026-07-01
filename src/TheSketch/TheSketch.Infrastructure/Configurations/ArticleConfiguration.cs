using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheSketch.Domain.Entities;

namespace TheSketch.Infrastructure.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(a => a.Slug)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasIndex(a => a.Slug)
            .IsUnique();

        builder.Property(a => a.Description)
            .HasMaxLength(500);

        builder.Property(a => a.Category)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(a => a.Tags)
            .IsRequired();

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        builder.Property(a => a.Blocks)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, jsonOptions),
                v => ConvertJsonToBlocks(v, jsonOptions)
            )
            .Metadata.SetValueComparer(new ValueComparer<List<ArticleBlock>>(
                (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
            ));
    }

    private static List<ArticleBlock> ConvertJsonToBlocks(string json, JsonSerializerOptions options)
    {
        if (string.IsNullOrEmpty(json))
            return new List<ArticleBlock>();

        using var doc = JsonDocument.Parse(json);
        var list = new List<ArticleBlock>();

        foreach (var element in doc.RootElement.EnumerateArray())
        {
            if (!element.TryGetProperty("type", out var typeProp))
                continue;

            var type = typeProp.GetString();

            ArticleBlock? block = type switch
            {
                "title" => JsonSerializer.Deserialize<TitleBlock>(element.GetRawText(), options),
                "subtitle" => JsonSerializer.Deserialize<SubtitleBlock>(element.GetRawText(), options),
                "paragraph" => JsonSerializer.Deserialize<ParagraphBlock>(element.GetRawText(), options),
                "image-wide" => JsonSerializer.Deserialize<ImageBlock>(element.GetRawText(), options),
                "quote" => JsonSerializer.Deserialize<QuoteBlock>(element.GetRawText(), options),
                _ => null
            };

            if (block != null)
                list.Add(block);
        }

        return list;
    }
}
