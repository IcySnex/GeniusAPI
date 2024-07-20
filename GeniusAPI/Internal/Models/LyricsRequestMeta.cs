﻿using System.Text.Json.Serialization;

namespace GeniusAPI.Internal.Models;

internal class LyricsRequestMeta
{
    [JsonPropertyName("status")]
    public int StatusCode { get; set; } = default!;

    [JsonPropertyName("message")]
    public string? Message { get; set; } = null;
}