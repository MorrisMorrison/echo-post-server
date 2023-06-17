using EchoPost.Application.Common.Interfaces;
using EchoPost.Domain.Entities;
using EchoPost.Domain.Events;
using MediatR;

namespace EchoPost.Application.Posts.Commands.CreatePost;

public record CreatePostCommand : IRequest<int>
{

    public string? Title { get; init; } 
    public string? Content { get; init; }
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreatePostCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var entity = new Post
        {
            Title = request.Title,
            Content = request.Content
        };

        entity.AddDomainEvent(new PostCreatedEvent(entity));

        _context.Posts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
