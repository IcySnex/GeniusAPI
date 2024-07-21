using System.Text.Json.Serialization;

namespace GeniusAPI.Internal.Models;

internal class GeniusRequestResponse
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("hits")]
    public GeniusHit[] Hits { get; set; } = [];
}