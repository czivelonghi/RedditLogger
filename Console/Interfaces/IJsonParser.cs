using System.Text.Json;

namespace RedditLogger.Interfaces
{
    public interface IJsonParser
    {
        List<JsonElement> Parse(string json);
    }
}
