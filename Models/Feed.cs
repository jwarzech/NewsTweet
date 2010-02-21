using System;

namespace NewsTweet.Models
{
    /// <summary>
    /// Representation of a saved rss feed
    /// </summary>
    public class Feed
    {
        public string Name { get; set; }
        public string URI { get; set; }

        public Feed() { }
    }
}
