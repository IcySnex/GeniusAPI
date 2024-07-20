using System.Text.Json.Serialization;

namespace GeniusAPI.Internal.Models;

internal class LyricsRequestResult
{
    [JsonPropertyName("meta")]
    public LyricsRequestMeta Meta { get; set; } = default!;

    [JsonPropertyName("response")]
    public LyricsRequestResponse Response { get; set; } = default!;
}
