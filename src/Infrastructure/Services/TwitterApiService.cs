using EchoPost.Application.Common.Interfaces;
using EchoPost.Application.Posts.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;


namespace EchoPost.Infrastructure.Services;

public interface ITwitterApiService 
{
    void Publish(PostDto post);
}

public class TwitterApiService : ITwitterApiService
{
    public void Publish(PostDto postDto){
        Console.WriteLine("Publish TWITTER post");
        Console.WriteLine(postDto);
    }
}
