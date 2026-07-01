using System;
using System.Collections.Generic;
using System.Text;

namespace TheSketch.Domain.Entities;

public class Article
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public byte TimeToRead { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ArticleCategory Category { get; set; }
    public List<string> Tags { get; set; } = new ();
    public List<ArticleBlock> Blocks { get; set; } = new();

    public void CalculateTimeToRead()
    {
        if (Blocks == null || !Blocks.Any())
        {
            TimeToRead = 1;
            return;
        }

        int totalWords = 0;
        int imageCount = 0;

        foreach (var block in Blocks)
        {
            switch (block)
            {
                case TitleBlock titleBlock:
                    totalWords += CountWords(titleBlock.Text);
                    break;

                case SubtitleBlock subtitleBlock:
                    totalWords += CountWords(subtitleBlock.Text);
                    break;

                case ParagraphBlock paragraphBlock:
                    totalWords += CountWords(paragraphBlock.Text);
                    break;

                case QuoteBlock quoteBlock:
                    totalWords += CountWords(quoteBlock.Text);
                    if (!string.IsNullOrWhiteSpace(quoteBlock.Author))
                    {
                        totalWords += CountWords(quoteBlock.Author);
                    }
                    break;

                case ImageBlock _:
                    imageCount++;
                    break;
            }
        }

        double wordsTimeInMinutes = (double)totalWords / 200;


        double imagesTimeInMinutes = (imageCount * 8) / 60.0;

        double totalTimeInMinutes = wordsTimeInMinutes + imagesTimeInMinutes;

        int finalMinutes = (int)Math.Ceiling(totalTimeInMinutes);

        TimeToRead = (byte)Math.Clamp(finalMinutes, 1, 255);
    }

    private int CountWords(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;

        return text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }
}

public enum ArticleCategory
{
    World,
    Ukraine
}
