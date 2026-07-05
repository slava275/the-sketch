using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TheSketch.Domain.Entities;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(ParagraphBlock), "paragraph")]
[JsonDerivedType(typeof(ImageBlock), "image-wide")]
[JsonDerivedType(typeof(TitleBlock), "title")]
[JsonDerivedType(typeof(QuoteBlock), "quote")]
[JsonDerivedType(typeof(SubtitleBlock), "subtitle")]
public abstract class ArticleBlock
{
    [JsonIgnore]
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
