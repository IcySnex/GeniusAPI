namespace GeniusAPI.Models;

/// <summary>
/// Represents a track on Genius with additional information.  
/// </summary>
/// <remarks>
/// Creates a new GeniusTrackInfo.
/// </remarks>
/// <param name="track">The track the info was fetched from.</param>
/// <param name="lyrics">The lyrics of the track. May be null if lyrics couldn't be found.</param>
/// <param name="genres">The genres of the track. May be null if genres couldn't be found.</param>
public class GeniusTrackInfo(
    GeniusTrack track,
    string? lyrics,
    IEnumerable<string>? genres)
{
    /// <summary>
    /// The track the info was fetched from.
    /// </summary>
    public GeniusTrack Track { get; } = track;

    /// <summary>
    /// The lyrics of the track.
    /// <br/>
    /// May be null if lyrics couldn't be found.
    /// </summary>
    public string? Lyrics { get; } = lyrics;

    /// <summary>
    /// The genres of the track.
    /// <br/>
    /// May be null if genres couldn't be found.
    /// </summary>
    public IEnumerable<string>? Genres { get; } = genres;
}