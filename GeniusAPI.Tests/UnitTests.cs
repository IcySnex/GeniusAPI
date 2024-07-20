#pragma warning disable IDE1006 // Naming Styles

using GeniusAPI.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GeniusAPI.Tests;

internal class UnitTests
{
    ILogger<LyricsClient> logger;
    LyricsClient client;

    [SetUp]
    public void Setup()
    {
        ILoggerFactory factory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        logger = factory.CreateLogger<LyricsClient>();
        client = new(TestData.AccessToken, logger);
    }


    [Test]
    public void search_for_tracks()
    {
        IEnumerable<LyricsTrack>? tracks = null;

        Assert.DoesNotThrowAsync(async () =>
        {
            tracks = await client.SearchTracksAsync(TestData.Query);
        });
        Assert.That(tracks, Is.Not.Null);
        Assert.That(tracks, Is.Not.Empty);

        // Output
        logger.LogInformation("\nTracks: {tracks} ", JsonSerializer.Serialize(tracks, TestData.SerializerOptions));
    }


    [Test]
    public void fetch_track_lyrics()
    {
        string? trackLyrics = null;

        Assert.DoesNotThrowAsync(async () =>
        {
            trackLyrics = await client.FetchLyricsAsync(TestData.Url);
        });
        Assert.That(trackLyrics, Is.Not.Null);
        Assert.That(trackLyrics, Is.Not.Empty);

        // Output
        logger.LogInformation("\nTrack Lyrics: {trackLyrics} ", trackLyrics);
    }
}