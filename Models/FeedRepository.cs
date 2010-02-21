using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NewsTweet.Models
{
    /// <summary>
    /// Repository for retrieving tweets
    /// </summary>
    public static class FeedRepository
    {
        #region Constants

        private const string SAVED_FEEDS = "feeds.xml";
        // Twitter search api english
        private const string SEARCH_URL = "http://search.twitter.com/search.atom?lang=en&q={0}";
        private static XNamespace ATOM_NAMESPACE = "http://www.w3.org/2005/Atom";

        #endregion // Constants

        #region Private Methods

        /// <summary>
        /// Constructs the query that is passed to Twitter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string BuildQuery(string value)
        {
            short wordCount = 0;

            var query = new StringBuilder();

            // Remove 'blacklisted' words
            foreach (string token in value.ToLower().Split(' '))
            {
                if (Blacklist.Values.Contains(token))
                    continue;

                string format = (++wordCount % 2 == 0) ? " {0}\" OR " : "\"{0}";
                query.Append(string.Format(format, token));
            }

            // TODO: Make sure results hit at list a tuple to display

            if (query[query.Length - 1] != '"')
                query.Append('"');
            else query.Remove(query.Length - 5, 4);

            return query.Append(" -rt -http").ToString();
        }

        #endregion // Private Methods

        #region Public Methods

        public static List<Feed> FetchFeeds()
        {
            var document = XDocument.Load(SAVED_FEEDS);

            return (from item in document.Descendants("feed")
                        select new Feed
                        {
                            Name = (string)item.Element("name"),
                            URI = (string)item.Element("uri")
                        }).ToList<Feed>();
        }

        /// <summary>
        /// Retrieves a list of headlines
        /// </summary>
        /// <param name="uri">The location of the headline rss feed</param>
        /// <returns>A list of headlines from the specified location</returns>
        public static List<string> FetchHeadlines(string uri)
        {
            var document = XDocument.Load(uri);

            return (from item in document.Descendants("item")
                    select (string)item.Element("title")).ToList<string>();
        }

        /// <summary>
        /// Retrieves a list of tweets
        /// </summary>
        /// <param name="headline">The headline that is the basis for the search criteria</param>
        /// <returns>A list of tweets that are similar to the passed healine</returns>
        public static List<Tweet> FetchTweets(string headline)
        {
            var query = BuildQuery(headline);
            var document = XDocument.Load(string.Format(SEARCH_URL, query));

            var list = (from entry in document.Descendants(ATOM_NAMESPACE + "entry")
                        select new Tweet
                        {
                            ID = (string)entry.Element(ATOM_NAMESPACE + "id"),
                            Title = (string)entry.Element(ATOM_NAMESPACE + "title"),
                            Image = (string)entry.Element(ATOM_NAMESPACE + "link").Attribute(ATOM_NAMESPACE + "href")
                            //Author = (string)entry.Descendants("author").Element("name")
                        });

            return list.ToList<Tweet>();
        }

        

        #endregion // Public Methods
    }
}