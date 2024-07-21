using GeniusAPI.Internal;
using System.Text.Json.Serialization;

namespace GeniusAPI.Models;

/// <summary>
/// Represents a track on Genius.
/// </summary>
public class GeniusTrack
{
    /// <summary>
    /// The count of annotations of the track.
    /// </summary>
    [JsonPropertyName("annotation_count")]
    public int AnnotationCount { get; set; } = default!;

    /// <summary>
    /// The API path of the track.
    /// </summary>
    [JsonPropertyName("api_path")]
    public string ApiPath { get; set; } = default!;

    /// <summary>
    /// The names of all artists of the track.
    /// </summary>
    [JsonPropertyName("artist_names")]
    public string ArtistNames { get; set; } = default!;

    /// <summary>
    /// The full title of the track.
    /// </summary>
    [JsonPropertyName("full_title")]
    public string FullTitle { get; set; } = default!;

    /// <summary>
    /// The url to the header thumbnail image of the track.
    /// </summary>
    [JsonPropertyName("header_image_thumbnail_url")]
    public string HeaderImageThumbnailUrl { get; set; } = default!;

    /// <summary>
    /// The url to the header image of the track.
    /// </summary>
    [JsonPropertyName("header_image_url")]
    public string HeaderImageUrl { get; set; } = default!;

    /// <summary>
    /// The id of the track.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; } = default!;

    /// <summary>
    /// The id of the lyrics owner of the track.
    /// </summary>
    [JsonPropertyName("lyrics_owner_id")]
    public int LyricsOwnerId { get; set; } = default!;

    /// <summary>
    /// The state of the lyrics of track.
    /// </summary>
    [JsonPropertyName("lyrics_state")]
    public string LyricsState { get; set; } = default!;

    /// <summary>
    /// The path of the track.
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; set; } = default!;

    /// <summary>
    /// The count of pyongs of the track.
    /// </summary>
    [JsonPropertyName("pyongs_count")]
    public int? PyongsCount { get; set; } = null;

    /// <summary>
    /// The url to the relationship index of the track.
    /// </summary>
    [JsonPropertyName("relationships_index_url")]
    public string RelationshipsIndexUrl { get; set; } = default!;

    /// <summary>
    /// The date and time when the track was released.
    /// </summary>
    [JsonPropertyName("release_date_components")]
    [JsonConverter(typeof(DateComponentsConverter))]
    public DateTime ReleasedAt { get; set; } = DateTime.MinValue;

    /// <summary>
    /// The url of the artwork thumbnail of the track.
    /// </summary>
    [JsonPropertyName("song_art_image_thumbnail_url")]
    public string ArtworkThumbnailUrl { get; set; } = default!;

    /// <summary>
    /// The url of the artwork of the track.
    /// </summary>
    [JsonPropertyName("song_art_image_url")]
    public string ArtworklUrl { get; set; } = default!;

    /// <summary>
    /// The stats of the track.
    /// </summary>
    [JsonPropertyName("stats")]
    public GeniusTrackStats Stats { get; set; } = default!;

    /// <summary>
    /// The title of the track.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = default!;

    /// <summary>
    /// The title of the track containing the featured information.
    /// </summary>
    [JsonPropertyName("title_with_featured")]
    public string TitleWithFeatured { get; set; } = default!;

    /// <summary>
    /// The url of the track.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = default!;

    /// <summary>
    /// The featured artists of the track.
    /// </summary>
    [JsonPropertyName("featured_artists")]
    public GeniusArtist[] FeaturedArtists { get; set; } = [];

    /// <summary>
    /// The primary artist of the track.
    /// </summary>
    [JsonPropertyName("primary_artist")]
    public GeniusArtist PrimaryArtist { get; set; } = default!;
}
