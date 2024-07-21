namespace GeniusAPI.Models;

/// <summary>
/// Represents a track on Genius with additional information.  
/// </summary>
/// <remarks>
/// Creates a new GeniusTrackInfo.
/// </remarks>
/// <param name="track">The track the info was fetched from.</param>
/// <param name="lyrics">The lyrics of the track.</param>
/// <param name="genres">The genres of the track.</param>
public class GeniusTrackInfo(
    GeniusTrack track,
    string lyrics,
    IEnumerable<string> genres)
{
    /// <summary>
    /// The track the info was fetched from.
    /// </summary>
    public GeniusTrack Track { get; } = track;

    /// <summary>
    /// The lyrics of the track.
    /// </summary>
    public string Lyrics { get; } = lyrics;

    /// <summary>
    /// The genres of the track.
    /// </summary>
    public IEnumerable<string> Genres { get; } = genres;
}