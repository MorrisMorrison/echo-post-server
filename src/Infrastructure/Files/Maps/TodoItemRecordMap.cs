using System.Globalization;
using EchoPost.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;

namespace EchoPost.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).Convert(c => c.Value.Done ? "Yes" : "No");
    }
}
