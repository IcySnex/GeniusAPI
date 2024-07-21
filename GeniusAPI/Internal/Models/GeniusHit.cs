using GeniusAPI.Models;
using System.Text.Json.Serialization;

namespace GeniusAPI.Internal.Models;

internal class GeniusHit
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("result")]
    public GeniusTrack Track { get; set; } = default!;
}