using AutoMapper;
using EchoPost.Application.Common.Extensions;
using EchoPost.Application.Posts.Services;
using EchoPost.Domain.Entities;
using EchoPost.Domain.Enums;
using EchoPost.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;

namespace EchoPost.Application.Posts.EventHandlers;

public class PostCreatedEventHandler : INotificationHandler<PostCreatedEvent>
{
    private readonly IPostingService _postingService;
    private readonly IMapper _mapper;
    private readonly ILogger<PostCreatedEventHandler> _logger;

    public PostCreatedEventHandler(IPostingService postingService, IMapper mapper, ILogger<PostCreatedEventHandler> logger)
    {
        _postingService = postingService;
        _mapper = mapper;
        _logger = logger;
    }

    public Task Handle(PostCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("EchoPost Domain Event: {DomainEvent}", notification.GetType().Name);

        PostDto postDto = _mapper.Map<PostDto>(notification.Item);
        _postingService.Publish(postDto);

        return Task.CompletedTask;
    }
}
