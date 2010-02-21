using System.Collections.ObjectModel;
using NewsTweet.Models;

namespace NewsTweet.ViewModels
{
    /// <summary>
    /// A single headline viewmodel
    /// </summary>
    public class HeadlineViewModel : ViewModelBase
    {
        #region Private Class Members

        private string _title;
        private ObservableCollection<TweetViewModel> _tweets;

        #endregion // Private Class Members

        #region Access Properties

        /// <summary>
        /// The headline's title
        /// </summary>
        public string Title
        {
            get { return _title; }
        }

        /// <summary>
        /// A list of related tweets
        /// </summary>
        public ObservableCollection<TweetViewModel> Tweets
        {
            get { return _tweets; }
            private set
            {
                if (_tweets == value)
                    return;

                _tweets = value;
                OnPropertyChanged("TweetList");
            }
        }

        #endregion // Access Properties

        #region Constructors

        public HeadlineViewModel(string headline)
        {
            // Set title property
            _title = headline;

            // Populate associated tweet list
            _tweets = new ObservableCollection<TweetViewModel>();
            var tweets = FeedRepository.FetchTweets(headline);
            tweets.ForEach(i => _tweets.Add(new TweetViewModel(i)));
        }

        #endregion // Constructors
    }
}
