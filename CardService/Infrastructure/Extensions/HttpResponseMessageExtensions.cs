using System.Text.Json;
using System.Text.Json.Serialization;

namespace CardService.Infrastructure.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<T> GetFromJsonAsync<T>(this HttpResponseMessage message)
    {
        var jsonString = await message.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        options.Converters.Add(new JsonStringEnumConverter());
        return JsonSerializer.Deserialize<T>(jsonString, options)!;
    }
}