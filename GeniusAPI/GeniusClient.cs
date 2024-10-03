using GeniusAPI.Internal;
using GeniusAPI.Internal.Models;
using GeniusAPI.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace GeniusAPI;

/// <summary>
/// Allows to search and fetch track lyrics/genres on Genius.
/// </summary>
public class GeniusClient
{
    readonly ILogger? logger = null;

    readonly TrackInfoParser infoParser;
    readonly HttpClient client;

    /// <summary>
    /// Creates a new GeniusClient
    /// </summary>
    /// <param name="accessToken">The access token for the Genius API. Get one for free here: https://genius.com/api-clients</param>
    public GeniusClient(
        string accessToken)
    {
        this.client = new();
        this.infoParser = new();

        this.AccessToken = accessToken;
    }

    /// <summary>
    /// Creates a new GeniusClient
    /// </summary>
    /// <param name="accessToken">The access token for the Genius API. Get one for free here: https://genius.com/api-clients.</param>
    /// <param name="logger">The logger used to log.</param>
    public GeniusClient(
        string accessToken,
        ILogger logger)
    {
        this.client = new();
        this.infoParser = new(logger);

        this.AccessToken = accessToken;
        this.logger = logger;

        logger.LogInformation("[LyricsClient-.ctor] LyricsClient has been initialized.");
    }


    string accessToken = default!;

    /// <summary>
    /// The access token for the Genius API.
    /// Get one for free here: https://genius.com/api-clients.
    /// </summary>
    public string AccessToken
    {
        get => accessToken;
        set
        {
            accessToken = value;
            client.DefaultRequestHeaders.Authorization = new("Bearer", value);
        }
    }


    /// <summary>
    /// Searches for tracks on Genius.
    /// </summary>
    /// <param name="query">The query to search for.</param>
    /// <param name="cancellationToken">The token to cancel this action.</param>
    /// <returns>An enumerable containing all found tracks.</returns>
    public async Task<IEnumerable<GeniusTrack>> SearchTracksAsync(
        string query,
        CancellationToken cancellationToken = default)
    {
        logger?.LogInformation("[LyricsClient-SearchTracksAsync] Searching for tracks on Genius...");
        HttpResponseMessage response = await client.GetAsync($"https://api.genius.com/search?q={WebUtility.UrlEncode(query)}", cancellationToken);
        response.EnsureSuccessStatusCode();

        string body = await response.Content.ReadAsStringAsync(cancellationToken);

        GeniusRequestResult? result = JsonSerializer.Deserialize<GeniusRequestResult>(body);
        if (result is null)
        {
            logger?.LogError(ex, "[LyricsClient-SearchTracksAsync] Failed to search for tracks on Genius: Parsed search result is null");
            return [];
        }
        if (result.MetaData.StatusCode != 200)
        {
            logger?.LogError(ex, "[LyricsClient-SearchTracksAsync] Failed to search for lyrics: {statusCode}, {message}", result.MetaData.StatusCode, result.MetaData.Message);
            return [];
        }

        return result.Response.Hits
            .Where(hit => hit.Type == "song")
            .Select(hit => hit.Track);
    }


    async Task<HtmlNode> GetDocumentNodeAsync(
        string trackUrl,
        CancellationToken cancellationToken = default)
    {
        logger?.LogInformation("[LyricsClient-GetDocumentNodeAsync] Getting track html content...");
        HttpResponseMessage response = await client.GetAsync(trackUrl, cancellationToken);
        response.EnsureSuccessStatusCode();

        HtmlDocument html = new();
        html.Load(await response.Content.ReadAsStreamAsync(cancellationToken));

        return html.DocumentNode;
    }

    /// <summary>
    /// Fetches the lyrics of a track on Genius.
    /// </summary>
    /// <param name="trackUrl">The url of the track to fetch the lyrics for.</param>
    /// <param name="cancellationToken">The token to cancel this action.</param>
    /// <returns>A string representing the lyrics. May be null if lyrics couldn't be found.</returns>
    public async Task<string?> FetchLyricsAsync(
        string trackUrl,
        CancellationToken cancellationToken = default)
    {
        HtmlNode documentNode = await GetDocumentNodeAsync(trackUrl, cancellationToken);
        return infoParser.GetLyrics(documentNode);
    }

    /// <summary>
    /// Fetches the genres of a track on Genius.
    /// </summary>
    /// <param name="trackUrl">The url of the track to fetch the genres for.</param>
    /// <param name="cancellationToken">The token to cancel this action.</param>
    /// <returns>An enumerable containing all genres. May be null if genres couldn't be found.</returns>
    public async Task<IEnumerable<string>?> FetchGenresAsync(
        string trackUrl,
        CancellationToken cancellationToken = default)
    {
        HtmlNode documentNode = await GetDocumentNodeAsync(trackUrl, cancellationToken);
        return infoParser.GetGenres(documentNode);
    }


    /// <summary>
    /// Searches for the track on Genius and additionally fetches the lyrics and genres.
    /// </summary>
    /// <param name="title">The title of the track to search for.</param>
    /// <param name="artist">The primary artist of the track to search for.</param>
    /// <param name="cancellationToken">The token to cancel this action.</param>
    /// <returns>A new LyricsTrackInfo. May be null if track info couldn't be found.</returns>
    public async Task<GeniusTrackInfo?> GetTrackInfoAsync(
        string title,
        string artist,
        CancellationToken cancellationToken = default)
    {
        IEnumerable<GeniusTrack> searchResults = await SearchTracksAsync($"{title} {artist}", cancellationToken);

        GeniusTrack? track = searchResults.FirstOrDefault(searchResult => searchResult.FullTitle.Contains(title, StringComparison.InvariantCultureIgnoreCase) && searchResult.ArtistNames.Contains(artist, StringComparison.InvariantCultureIgnoreCase));
        if (track is null)
        {
            logger?.LogError(ex, "[LyricsClient-GetTrackInfoAsync] Failed to get track info: search results don't contain required track.");
            return null;
        }

        HtmlNode documentNode = await GetDocumentNodeAsync(track.Url, cancellationToken);
        string? lyrics = infoParser.GetLyrics(documentNode);
        IEnumerable<string>? genres = infoParser.GetGenres(documentNode);
        
        return new(track, lyrics, genres);
    }
}