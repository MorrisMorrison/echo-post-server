using EchoPost.Application.Common.Models;
using EchoPost.Application.Posts.Commands.CreatePost;
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

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreatePostCommand command)
    {
        return await Mediator.Send(command);
    }
}