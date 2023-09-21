using System.Text.Json;
using RedditLogger.Interfaces;
using RedditLogger.Models;

namespace RedditLogger.Common
{
    public class JsonParser : IJsonParser
    {
        public List<JsonElement> Parse(string json)
        {
            return JsonDocument
                .Parse(json)
                .RootElement
                .GetProperty("data")
                .GetProperty("children")
                .EnumerateArray()
                .ToList();
        }
    }
}
