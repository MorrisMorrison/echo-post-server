using AutoMapper;
using EchoPost.Application.Common.Mappings;
using EchoPost.Domain.Entities;
using EchoPost.Domain.Enums;


namespace EchoPost.Application.Posts.Services;

public record PostDto : IMapFrom<Post>
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public string? Content { get; init; }

    public Channel Channel { get; init; }

}
