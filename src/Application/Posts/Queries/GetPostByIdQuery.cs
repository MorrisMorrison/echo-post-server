using AutoMapper;
using AutoMapper.QueryableExtensions;
using EchoPost.Application.Common.Exceptions;
using EchoPost.Application.Common.Interfaces;
using EchoPost.Application.Common.Mappings;
using EchoPost.Application.Common.Models;
using EchoPost.Application.Posts.Services;
using EchoPost.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace EchoPost.Application.Posts.Queries;

public record GetPostByIdQuery : IRequest<PostDto>
{
    public int Id { get; init; }
}

public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPostByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {   
        Post? post = await _context.Posts
            .FirstOrDefaultAsync(post => post.Id == request.Id, cancellationToken: cancellationToken);
        
        if (post == null) throw new NotFoundException(nameof(Post), request.Id);

        return _mapper.Map<PostDto>(post);
    }
}
