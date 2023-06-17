using EchoPost.Application.Common.Models;
using EchoPost.Application.Posts.Commands.CreatePost;
using EchoPost.Application.Posts.Queries;
using EchoPost.Application.Posts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoPost.WebUI.Controllers;

[Authorize]
public class PostsController : ApiControllerBase
{
    // [HttpGet]
    // public async Task<ActionResult<PaginatedList<PostDto>>> GetPostsWithPagination([FromQuery] GetPostsWithPaginationQuery query)
    // {
    //     return await Mediator.Send(query);
    // }

    [HttpGet]
    public async Task<IList<PostDto>> GetPosts()
    {
        return await Mediator.Send(new GetPostsQuery());
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreatePostCommand command)
    {
        return await Mediator.Send(command);
    }
}