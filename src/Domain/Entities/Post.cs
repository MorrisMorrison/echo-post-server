namespace EchoPost.Domain.Entities;

public class Post : BaseAuditableEntity
{

    public string? Title { get; set; }

    public string? Content { get; set; }

}
