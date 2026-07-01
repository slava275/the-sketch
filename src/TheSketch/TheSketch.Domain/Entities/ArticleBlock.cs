using System;
using System.Collections.Generic;
using System.Text;

namespace TheSketch.Domain.Entities;

public abstract class ArticleBlock
{
    public string Type { get; set; } = null!;
}

public class ParagraphBlock : ArticleBlock
{
    public ParagraphBlock() => Type = "paragraph";

    public string Text { get; set; } = null!;
}

public class ImageBlock : ArticleBlock
{
    public ImageBlock() => Type = "image-wide";
    public string Url { get; set; } = null!;
    public string? Caption { get; set; }
}

public class TitleBlock : ArticleBlock
{
    public TitleBlock() => Type = "title";
    public string Text { get; set; } = null!;
}

public class QuoteBlock : ArticleBlock
{
    public QuoteBlock() => Type = "quote";
    public string Text { get; set; } = null!;
    public string? Author { get; set; }
}

public class SubtitleBlock : ArticleBlock
{
    public SubtitleBlock() => Type = "subtitle";
    public string Text { get; set; } = null!;
}
