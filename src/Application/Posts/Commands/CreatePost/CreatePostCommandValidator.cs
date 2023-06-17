using EchoPost.Domain.Enums;
using FluentValidation;

namespace EchoPost.Application.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Content)
            .MinimumLength(10)
            .MaximumLength(5000)
            .NotEmpty();

        RuleFor(v => v.Channel)
            .IsInEnum()
            .NotNull();
    }
}
