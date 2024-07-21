# GeniusAPI
This Genius API wrapper allows you to easily search for tracks on Genius. It also provides lyrics/genres fetching using web scraping.

---

## How to use?
Search for tracks on Genius:
```cs
using GeniusAPI;
using GeniusAPI.Models;

GeniusClient client = new("<GENIUS ACCESS TOKEN>");
IEnumerable<GeniusTrack> tracks = await client.SearchTracksAsync("<SEARCH QUERRY>");
```

Fetch lyrics of a track:
```cs
using GeniusAPI;
using GeniusAPI.Models;

GeniusClient client = new("<YOUR GENIUS ACCESS TOKEN>");
string lyrics = await client.FetchLyricsAsync("<GENIUS TRACK URL>");
```

Fetch genres of a track:
```cs
using GeniusAPI;
using GeniusAPI.Models;

GeniusClient client = new("<YOUR GENIUS ACCESS TOKEN>");
IEnumerable<string> genres = await client.FetchGenresAsync("<GENIUS TRACK URL>");
```

Get track info (search for track & additionally fetch the lyrics/genres):
```cs
using GeniusAPI;
using GeniusAPI.Models;

GeniusClient client = new("<YOUR GENIUS ACCESS TOKEN>");
GeniusTrackInfo trackInfo = await client.GetTrackInfoAsync("<TRACK TITLE>", "<TRACK ARTIST>");

GeniusTrack track = trackInfo.Track;
string lyrics = trackInfo.Lyrics;
IEnumerable<string> genres = trackInfo.Genres;
```

---

## How to get an acces token?
You can learn how to get a free access token on the offical Genius API documentation: https://genius.com/api-clients
