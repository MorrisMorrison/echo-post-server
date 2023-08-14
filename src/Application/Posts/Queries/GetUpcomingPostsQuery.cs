using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EchoPost.Application.Common.Interfaces;
using EchoPost.Application.Common.Mappings;
using EchoPost.Application.Common.Models;
using EchoPost.Application.Posts.Services;
using MediatR;

namespace EchoPost.Application.Posts.Queries;

public record GetUpcomingPostQuery : IRequest<IList<PostDto>>;

public class GetUpcomingPostsQueryHandler : IRequestHandler<GetPostsQuery, IList<PostDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUpcomingPostsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<PostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Posts
        .Where(x => x.State == Domain.Enums.PostState.SCHEDULED)
            .OrderBy(x => x.Title)
            .ProjectToListAsync<PostDto>(_mapper.ConfigurationProvider);
    }
}
