using System.Text.Json.Serialization;

namespace GeniusAPI.Internal.Models;

internal class GeniusRequestResult
{
    [JsonPropertyName("meta")]
    public GeniusRequestMetaData MetaData { get; set; } = default!;

    [JsonPropertyName("response")]
    public GeniusRequestResponse Response { get; set; } = default!;
}
