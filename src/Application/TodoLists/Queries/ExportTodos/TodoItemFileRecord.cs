using EchoPost.Application.Common.Mappings;
using EchoPost.Domain.Entities;

namespace EchoPost.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; init; }

    public bool Done { get; init; }
}
