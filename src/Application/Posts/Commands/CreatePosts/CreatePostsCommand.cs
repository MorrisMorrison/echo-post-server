using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Channels;
using System.Linq;
using EchoPost.Application.Common.Extensions;
using EchoPost.Application.Common.Interfaces;
using EchoPost.Domain.Entities;
using EchoPost.Domain.Enums;
using EchoPost.Domain.Events;
using MediatR;
using System.Runtime.InteropServices;


namespace EchoPost.Application.Posts.Commands.CreatePost;

public record CreatePostsCommand : IRequest<Unit>
{

    public string? Title { get; init; }
    public string? Content { get; init; }
    public IList<ChannelType> Channels { get; init; } = new List<ChannelType>();
}

public class CreatePostsCommandHandler : IRequestHandler<CreatePostsCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public CreatePostsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreatePostsCommand request, CancellationToken cancellationToken)
    {   
        if (request.Channels == null ||request.Channels.Count == 0) return Unit.Value;

        request.Channels.ForEach(channel =>{
            var entity = new Post{
                Title = request.Title,
                Content = request.Content,
                Channel = channel
            };

            entity.AddDomainEvent(new PostCreatedEvent(entity));
            _context.Posts.Add(entity);
        });

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
