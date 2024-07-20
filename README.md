# GeniusAPI
This Genius API wrapper allows you to easily search for tracks on Genius. It also provides lyrics fetching using web scraping.

---

## How to use?
Search for tracks on Genius:
```cs
using GeniusAPI;
using GeniusAPI.Models;

LyricsClient client = new("<GENIUS ACCESS TOKEN>");
IEnumerable<LyricsTrack> tracks = await client.SearchTracksAsync("<SEARCH QUERRY>");
```

Fetch lyrics of a track:
```cs
using GeniusAPI;
using GeniusAPI.Models;

LyricsClient client = new("<YOUR GENIUS ACCESS TOKEN>");
string lyrics = await client.FetchLyricsAsync("<GENIUS TRACK URL>");
```

---

## How to get an acces token?
You can learn how to get a free access token on the offical Genius API documentation: https://genius.com/api-clients
