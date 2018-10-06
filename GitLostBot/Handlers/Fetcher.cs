using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace Gitlost_bot.Handlers
{
    public static class Fetcher
    {
        public static List<string[]> FetchTweets(int limit = 30)
        {
            List<string[]> contentBuffer = new List<string[]>();
            HtmlDocument homePage = new HtmlDocument();
            HtmlWeb web = new HtmlWeb();
            homePage = web.Load("https://twitter.com/gitlost");

            if (homePage != null)
            {
                string html = homePage.ParsedText;
                HtmlNode docNode = homePage.DocumentNode;

                int i = 0;

                if (docNode != null)
                {
                    IEnumerable<HtmlNode> allContainers = docNode.Descendants("table");

                    foreach (HtmlNode container in allContainers)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(container.Attributes["class"].Value))
                            {
                                if (container.Attributes["class"].Value == "tweet  ")
                                {
                                    HtmlNode t = container.ChildNodes[1];
                                    string hoursAgo = container.ChildNodes[1].ChildNodes[5].InnerText.Replace("\n", "").Trim().TrimStart(new char[] { ' ' });
                                    string content = container.ChildNodes[3].InnerText.Replace("\n", "").Trim().TrimStart(new char[] { ' ' });
                                    string likes = container.ChildNodes[3].InnerText.Replace("\n", "").Trim().TrimStart(new char[] { ' ' });
                                    contentBuffer.Add(new string[] { content, hoursAgo, likes });

                                    i++;
                                    if (i == limit)
                                        break;
                                }
                            }
                        }
                        catch (NullReferenceException)
                        {

                        }
                    }
                }
            }
            return contentBuffer;
        }
    }
}
