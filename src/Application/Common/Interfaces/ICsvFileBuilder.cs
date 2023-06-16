using EchoPost.Application.TodoLists.Queries.ExportTodos;

namespace EchoPost.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
