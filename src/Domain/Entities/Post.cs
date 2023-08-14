using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Channels;

namespace EchoPost.Domain.Entities;

public class Post : BaseAuditableEntity
{

    public string? Title { get; set; }

    public string? Content { get; set; }

    public ChannelType Channel{ get; set; }

    public PostState State {get; set;}

}
