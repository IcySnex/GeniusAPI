﻿using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;

namespace GeniusAPI.Internal;

internal class TrackInfoParser
{
    static void GetHtmlText(
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
                        GetHtmlText(childNode, builder);
                    break;
            }
    }


    readonly ILogger? logger = null;

    public TrackInfoParser()
    {
    }

    public TrackInfoParser(
        ILogger logger)
    {
        this.logger = logger;

        logger.LogInformation("[TrackInfoParser-.ctor] TrackInfoParser has been initialized.");
    }


    public string GetLyrics(
        HtmlNode documentNode)
    {
        logger?.LogInformation("[TrackInfoParser-GetLyrics] Parsing track lyrics...");
        HtmlNodeCollection nodes = documentNode.SelectNodes("//div[@data-lyrics-container]");
        if (nodes is null || nodes.Count == 0)
        {
            logger?.LogError("[TrackInfoParser-GetLyrics] Failed to parse track lyrics: Parsed HTML nodes is null or empty.");
            throw new NullReferenceException("Failed to parse track lyrics: Parsed HTML nodes is null or empty.");
        }

        StringBuilder builder = new();
        foreach (HtmlNode node in nodes)
        {
            GetHtmlText(node, builder);
            builder.AppendLine();
        }

        return WebUtility.HtmlDecode(builder.ToString().TrimEnd());
    }

    public IEnumerable<string> GetGenres(
        HtmlNode documentNode)
    {
        logger?.LogInformation("[TrackInfoParser-GetLyrics] Parsing track genres...");
        HtmlNodeCollection nodes = documentNode.SelectNodes("//a[contains(@class, 'SongTags__Tag')]");
        if (nodes is null)
        {
            logger?.LogError("[LyricsClient-GetGenres] Failed to parse track genres: Parsed HTML nodes is null.");
            throw new NullReferenceException("Failed to parse track genres: Parsed HTML nodes is null.");
        }

        return nodes.Select(node => node.InnerText);
    }
}