using GeniusAPI.Models;
using System.Text.Json.Serialization;

namespace GeniusAPI.Internal.Models;

internal class LyricsHit
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("result")]
    public LyricsTrack Track { get; set; } = default!;
}