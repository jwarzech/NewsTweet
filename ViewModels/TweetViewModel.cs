using NewsTweet.Models;

namespace NewsTweet.ViewModels
{
    /// <summary>
    /// A single tweet
    /// </summary>
    public class TweetViewModel : ViewModelBase
    {
        #region Private Class Members

        private Tweet _tweet;

        #endregion // Private Class Members

        #region Access Properties

        /// <summary>
        /// The content of the tweet
        /// </summary>
        public string Title
        {
            get { return _tweet.Title; }
            set
            {
                if (_tweet.Title == value)
                    return;

                _tweet.Title = value;
                OnPropertyChanged("Title");
            }
        }

        /// <summary>
        /// The author's profile image
        /// </summary>
        public string Image
        {
            get { return _tweet.Image; }
            set
            {
                if (_tweet.Image == value)
                    return;

                _tweet.Image = value;
                OnPropertyChanged("Image");
            }
        }

        #endregion // Access Properties

        #region Constructors

        public TweetViewModel(Tweet tweet)
        {
            _tweet = tweet;
        }

        #endregion // Constructors
    }
}
