using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EchoPost.Domain.Entities;

public class Post : BaseAuditableEntity
{
    public string? UserId {get; set;} 
    
    public string? Title { get; set; }

    public string? Content { get; set; }

    public Channel Channel{ get; set; }
    public PostState State {get; set;}
    public DateTime? ToBePublishedAt{get;set;}
    public DateTime? PublishedAt{get; set;}
}
