using System;

namespace NewsTweet.Models
{
    /// <summary>
    /// Representation of a tweet
    /// </summary>
    public class Tweet
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public DateTime PublishedDate { get; set; }
        
        public Tweet() { }
    }
}
