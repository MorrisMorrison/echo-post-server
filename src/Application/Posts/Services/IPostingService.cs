using EchoPost.Application.Common.Interfaces;
using EchoPost.Domain.Entities;


namespace EchoPost.Application.Posts.Services;

public interface IPostingService
{
    void Publish(PostDto post);
}
