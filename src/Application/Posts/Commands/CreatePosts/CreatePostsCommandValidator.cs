using FluentValidation;

namespace EchoPost.Application.Posts.Commands.CreatePost;

public class CreatePostsCommandValidator : AbstractValidator<CreatePostsCommand>
{
    public CreatePostsCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}
