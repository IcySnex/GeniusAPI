using System.Text.Json.Serialization;

namespace GeniusAPI.Internal.Models;

internal class LyricsRequestResponse
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("hits")]
    public LyricsHit[] Hits { get; set; } = [];
}