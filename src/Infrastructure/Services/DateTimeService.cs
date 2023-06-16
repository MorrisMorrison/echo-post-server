using EchoPost.Application.Common.Interfaces;

namespace EchoPost.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
