#pragma warning disable IDE1006 // Naming Styles

using GeniusAPI.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GeniusAPI.Tests;

internal class UnitTests
{
    ILogger logger;
    GeniusClient client;

    [SetUp]
    public void Setup()
    {
        ILoggerFactory factory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        logger = factory.CreateLogger<UnitTests>();
        client = new(TestData.AccessToken, logger);
    }


    [Test]
    public void search_for_tracks()
    {
        IEnumerable<GeniusTrack>? tracks = null;

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
    public void fetch_lyrics()
    {
        string? lyrics = null;

        Assert.DoesNotThrowAsync(async () =>
        {
            lyrics = await client.FetchLyricsAsync(TestData.Url);
        });
        Assert.That(lyrics, Is.Not.Null);
        Assert.That(lyrics, Is.Not.Empty);

        // Output
        logger.LogInformation("\nLyrics: {lyrics} ", lyrics);
    }

    [Test]
    public void fetch_genres()
    {
        IEnumerable<string>? genres = null;

        Assert.DoesNotThrowAsync(async () =>
        {
            genres = await client.FetchGenresAsync(TestData.Url);
        });
        Assert.That(genres, Is.Not.Null);
        Assert.That(genres, Is.Not.Empty);

        // Output
        logger.LogInformation("\nGenres: {genres} ", genres);
    }


    [Test]
    public void get_track_info()
    {
        GeniusTrackInfo? trackInfo = null;

        Assert.DoesNotThrowAsync(async () =>
        {
            trackInfo = await client.GetTrackInfoAsync(TestData.Title, TestData.Artist);
        });
        Assert.That(trackInfo, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(trackInfo.Track, Is.Not.Null);
            Assert.That(trackInfo.Lyrics, Is.Not.Null);
            Assert.That(trackInfo.Lyrics, Is.Not.Empty);
            Assert.That(trackInfo.Genres, Is.Not.Null);
            Assert.That(trackInfo.Genres, Is.Not.Empty);
        });

        // Output
        logger.LogInformation("\nTrack Info: {trackInfo} ", JsonSerializer.Serialize(trackInfo, TestData.SerializerOptions));
    }
}