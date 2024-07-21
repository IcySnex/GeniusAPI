using System.Text.Json.Serialization;

namespace GeniusAPI.Models;

/// <summary>
/// Represents an artist on Genius.
/// </summary>
public class GeniusArtist
{
    /// <summary>
    /// The ÁPI path to the artist.
    /// </summary>
    [JsonPropertyName("api_path")]
    public string ApiPath { get; set; } = default!;

    /// <summary>
    /// The url of the header image of the artist.
    /// </summary>
    [JsonPropertyName("header_image_url")]
    public string HeaderImageUrl { get; set; } = default!;

    /// <summary>
    /// The id of the artist.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; } = default!;

    /// <summary>
    /// The url of the image of the artist.
    /// </summary>
    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; } = default!;

    /// <summary>
    /// Weither the artist is meme verified or not.
    /// </summary>
    [JsonPropertyName("is_meme_verified")]
    public bool IsMemeVerified { get; set; } = default!;

    /// <summary>
    /// Weither the artist is verified or not.
    /// </summary>
    [JsonPropertyName("is_verified")]
    public bool IsVerified { get; set; } = default!;

    /// <summary>
    /// The name of the artist.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    /// <summary>
    /// The url of the artist to Genius.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = default!;

    /// <summary>
    /// The iq of the artist.
    /// </summary>
    [JsonPropertyName("iq")]
    public int Iq { get; set; } = default!;
}