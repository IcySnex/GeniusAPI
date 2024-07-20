using System.Text.Json;

namespace GeniusAPI.Tests;

internal class TestData
{
    public static JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true
    };


    public const string AccessToken = "u_s2DsG-ewN4YDxgLZxzpo01mZaWSePOilc5rkBcylAYZ29cl93UzA7OEuPxWOCr";

    public const string Query = "Pashanim";

    public const string Url = "https://genius.com/Pashanim-shababs-botten-lyrics";
}