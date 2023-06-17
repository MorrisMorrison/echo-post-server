using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Channels;

namespace EchoPost.Domain.Entities;

public class Post : BaseAuditableEntity
{

    public string? Title { get; set; }

    public string? Content { get; set; }

    [NotMapped]
    public IList<ChannelType> ChannelTypes{ get; set; } = new List<ChannelType>();

}
