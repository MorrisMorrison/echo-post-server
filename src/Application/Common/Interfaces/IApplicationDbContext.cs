using EchoPost.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EchoPost.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Post> Posts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
