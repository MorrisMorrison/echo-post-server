using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Channels;
using System.Linq;
using EchoPost.Application.Common.Extensions;
using EchoPost.Application.Common.Interfaces;
using EchoPost.Domain.Entities;
using EchoPost.Domain.Enums;
using EchoPost.Domain.Events;
using MediatR;

namespace EchoPost.Application.Posts.Commands.CreatePost;

public record CreatePostCommand : IRequest<string>
{

    public string? Title { get; init; }
    public string? Content { get; init; }
    public Domain.Enums.Channel Channel { get; init; }
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, string>
{
    private readonly IApplicationDbContext _context;

    public CreatePostCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var entity = new Post
        {
            Title = request.Title,
            Content = request.Content,
            Channel = request.Channel
        };

        entity.AddDomainEvent(new PostCreatedEvent(entity));

        _context.Posts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id.ToString();
    }
}
