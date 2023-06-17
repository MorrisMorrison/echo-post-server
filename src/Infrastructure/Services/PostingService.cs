using EchoPost.Application.Common.Interfaces;
using EchoPost.Application.Posts.Services;
using EchoPost.Application.Common.Extensions;

namespace EchoPost.Infrastructure.Services;

public class PostingService : IPostingService
{
    private ITwitterApiService _twitterApiService;

    public PostingService(ITwitterApiService twitterApiService) {
        _twitterApiService = twitterApiService;
    }


    public void Publish(PostDto postDto) {
        Console.WriteLine($"Publish Post! {postDto}");
        _twitterApiService.Publish(postDto);
    }
}
