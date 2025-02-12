using System.Net.Http.Json;
using System.Text.Json;

namespace Client.Library;

public static class Api
{
    private static readonly HttpClient Http = new();
    private static readonly string StringArrayUrl = "https://cis-230-sp25.azurewebsites.net/api/StringArray";
    private static readonly string IntegerArrayUrl = "https://cis-230-sp25.azurewebsites.net/api/IntegerArray";

    public static async Task<string[]> GetStringArrayAsync()
    {
        return await Http.GetFromJsonAsync<string[]>(StringArrayUrl) 
            ?? throw new Exception("Failed to deserialize string array.");
    }

    public static async Task<int[]> GetIntegerArrayAsync()
    {
        var response = await Http.GetAsync(IntegerArrayUrl);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var integerArray = JsonSerializer.Deserialize<int[]>(json);
        return integerArray ?? throw new Exception("Failed to deserialize integer array.");
    }
}
