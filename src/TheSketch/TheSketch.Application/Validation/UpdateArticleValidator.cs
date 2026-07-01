using FluentValidation;
using TheSketch.Application.DTOs;

namespace TheSketch.Application.Validation;

public class UpdateArticleValidator : AbstractValidator<UpdateArticleDto>
{
    public UpdateArticleValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(256).WithMessage("Title cannot be longer than 256 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");

        RuleFor(x => x.CoverImageUrl)
            .NotEmpty().WithMessage("Cover image URL is required.")
            .MaximumLength(2048).WithMessage("Cover image URL cannot be longer than 2048 characters.");

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Invalid category selected.");

        RuleFor(x => x.Blocks)
            .NotEmpty().WithMessage("Article must contain at least one content block.");
    }
}
