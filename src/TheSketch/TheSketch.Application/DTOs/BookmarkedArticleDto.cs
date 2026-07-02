using System;
using System.Collections.Generic;
using System.Text;

namespace TheSketch.Application.DTOs;

public record BookmarkedArticleDto(
    Guid ArticleId,
    string Title,
    string Slug,
    string? Description,
    DateTime BookmarkedAt
);
