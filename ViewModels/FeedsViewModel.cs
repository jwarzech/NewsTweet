using System.Collections.ObjectModel;
using NewsTweet.Models;

namespace NewsTweet.ViewModels
{
    /// <summary>
    /// A single feed viewmodel
    /// </summary>
    public class FeedViewModel : ViewModelBase
    {
        #region Private Class Members

        private Feed _feed;

        #endregion // Private Class Members

        #region Access Properties

        /// <summary>
        /// The feed's name
        /// </summary>
        public string Name
        {
            get { return _feed.Name; }
        }

        /// <summary>
        /// The feed's URI
        /// </summary>
        public string URI
        {
            get { return _feed.URI; }
        }

        #endregion // Access Properties

        #region Constructors

        public FeedViewModel(Feed feed)
        {
            // Set inner feed
            _feed = feed;
        }

        #endregion // Constructors
    }
}
