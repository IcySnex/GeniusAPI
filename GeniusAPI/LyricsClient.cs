using GeniusAPI.Internal.Models;
using GeniusAPI.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Text.Json;

namespace GeniusAPI;

/// <summary>
/// Allows to search and fetch track lyrics on Genius.
/// </summary>
public class LyricsClient
{
    readonly ILogger<LyricsClient>? logger = null;

    readonly HttpClient client = new();

    /// <summary>
    /// Creates a new LyricsClient
    /// </summary>
    /// <param name="accessToken">The access token for the Genius API. Get one for free here: https://genius.com/api-clients</param>
    public LyricsClient(
        string accessToken)
    {
        client.DefaultRequestHeaders.Authorization = new("Bearer", accessToken);
    }

    /// <summary>
    /// Creates a new LyricsClient
    /// </summary>
    /// <param name="accessToken">The access token for the Genius API. Get one for free here: https://genius.com/api-clients</param>
    /// <param name="logger">The logger used to log.</param>
    public LyricsClient(
        string accessToken,
        ILogger<LyricsClient> logger)
    {
        this.logger = logger;

        client.DefaultRequestHeaders.Authorization = new("Bearer", accessToken);
    }


    /// <summary>
    /// Searches for tracks on Genius.
    /// </summary>
    /// <param name="query">The query to search for.</param>
    /// <param name="cancellationToken">The token to cancel this action.</param>
    /// <returns>An array containing all found tracks.</returns>
    public async Task<IEnumerable<LyricsTrack>> SearchTracksAsync(
        string query,
        CancellationToken cancellationToken = default)
    {
        logger?.LogInformation("[LyricsClient-SearchTracksAsync] Searching for tracks on Genius...");
        HttpResponseMessage response = await client.GetAsync($"https://api.genius.com/search?q={WebUtility.UrlEncode(query)}", cancellationToken);
        response.EnsureSuccessStatusCode();

        logger?.LogInformation("[LyricsClient-SearchTracksAsync] Parsing search results...");
        string body = await response.Content.ReadAsStringAsync(cancellationToken);

        LyricsRequestResult? result = JsonSerializer.Deserialize<LyricsRequestResult>(body);
        if (result is null)
        {
            logger?.LogError("[LyricsClient-SearchTracksAsync] Failed to search for tracks on Genius: Parsed search result is null");
            throw new NullReferenceException("Failed to search for tracks on Genius.");
        }
        if (result.Meta.StatusCode != 200)
        {
            logger?.LogError("[LyricsClient-SearchTracksAsync] Failed to search for lyrics: {statusCode}, {message}", result.Meta.StatusCode, result.Meta.Message);
            throw new Exception($"Lyrics request did not return successful status code: {result.Meta.StatusCode}.", new(result.Meta.Message ?? "Unknown message."));
        }

        return result.Response.Hits
            .Where(hit => hit.Type == "song")
            .Select(hit => hit.Track);
    }


    static void ExtractText(
        HtmlNode node,
        StringBuilder builder)
    {
        foreach (HtmlNode childNode in node.ChildNodes)
            switch (childNode.NodeType)
            {
                case HtmlNodeType.Text:
                    builder.Append(childNode.InnerText);
                    break;
                case HtmlNodeType.Element:
                    if (childNode.Name == "br")
                        builder.AppendLine();
                    else
                        ExtractText(childNode, builder);
                    break;
            }
    }

    /// <summary>
    /// Fetches the lyrics of a track on Genius.
    /// </summary>
    /// <param name="url">The url of the track to fetch the lyrics for.</param>
    /// <param name="cancellationToken">The token to cancel this action.</param>
    /// <returns>A string representing the lyrics.</returns>
    public async Task<string> FetchLyricsAsync(
        string url,
        CancellationToken cancellationToken = default)
    {
        logger?.LogInformation("[LyricsClient-FetchLyricsAsync] Fetching track lyrics...");
        HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        logger?.LogInformation("[LyricsClient-FetchLyricsAsync] Parsing track lyrics...");
        HtmlDocument html = new();
        html.Load(await response.Content.ReadAsStreamAsync(cancellationToken));

        HtmlNodeCollection nodes = html.DocumentNode.SelectNodes("//div[@data-lyrics-container]");
        if (nodes is null || nodes.Count == 0)
        {
            logger?.LogError("[LyricsClient-FetchLyricsAsync] Failed to fetch track lyrics: Parsed HTML nodes is null or empty");
            throw new NullReferenceException("Failed to fetch track lyrics.");
        }

        StringBuilder builder = new();
        foreach (HtmlNode node in nodes)
        {
            ExtractText(node, builder);
            builder.AppendLine();
        }

        return WebUtility.HtmlDecode(builder.ToString().TrimEnd());
    }
}