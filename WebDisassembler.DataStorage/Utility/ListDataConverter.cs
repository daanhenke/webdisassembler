using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WebDisassembler.DataStorage.Utility;

public class ListDataConverter<T> : ValueConverter<ICollection<T>, string> where T : notnull
{
    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
	{
		Converters =
		{
			new JsonStringEnumConverter()
		}
	};
    
    public ListDataConverter() : base(
        value => value == null
            ? "[]"
            : JsonSerializer.Serialize(value, _jsonOptions),
        value => string.IsNullOrWhiteSpace(value)
            ? new List<T>()
            : JsonSerializer.Deserialize<ICollection<T>>(value, _jsonOptions) ?? new List<T>()
    ) {}
}
