using System.Text.Json.Serialization;

namespace GeniusAPI.Models;

/// <summary>
/// Represents the stats of a Genius track.
/// </summary>
public class LyricTrackStats
{
    /// <summary>
    /// The count of unreviewed annotations of the track.
    /// </summary>
    [JsonPropertyName("unreviewed_annotations")]
    public int UnreviewedAnnotationsCount { get; set; } = default!;

    /// <summary>
    /// The concurrents of the track.
    /// </summary>
    [JsonPropertyName("concurrents")]
    public int? Concurrents { get; set; } = null;

    /// <summary>
    /// Weither the track is hot or not.
    /// </summary>
    [JsonPropertyName("hot")]
    public bool IsHot { get; set; } = default!;

    /// <summary>
    /// The page views of the track.
    /// </summary>
    [JsonPropertyName("pageviews")]
    public int PageViews { get; set; } = default!;
}