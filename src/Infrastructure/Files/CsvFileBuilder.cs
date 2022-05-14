using System.Globalization;
using CleanArchitectureSolution.Application.Common.Interfaces;
using CleanArchitectureSolution.Application.TodoLists.Queries.ExportTodos;
using CleanArchitectureSolution.Infrastructure.Files.Maps;
using CsvHelper;

namespace CleanArchitectureSolution.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
