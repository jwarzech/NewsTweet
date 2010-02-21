using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using NewsTweet.Commands;
using NewsTweet.Models;

namespace NewsTweet.ViewModels
{
    /// <summary>
    /// The main screen view model
    /// </summary>s
    public class MainViewModel : ViewModelBase
    {
        #region Private Class Members

        private string _uri;
        private ObservableCollection<FeedViewModel> _feeds;
        private ObservableCollection<HeadlineViewModel> _headlines;

        // Commands
        private DelegateCommand _refreshCommand;

        #endregion // Private Class Members

        #region Access Properties

        /// <summary>
        /// The URI to fetch headlines from
        /// </summary>
        public string URI
        {
            get { return _uri; }
            set
            {
                if (_uri == value)
                    return;

                _uri = value;
                OnPropertyChanged("URI");
            }
        }

        /// <summary>
        /// The list of the fetched feeds
        /// </summary>
        public ObservableCollection<FeedViewModel> Feeds
        {
            get { return _feeds; }
            private set
            {
                if (_feeds == value)
                    return;

                _feeds = value;
                OnPropertyChanged("Feeds");
            }
        }

        /// <summary>
        /// The list of the fetched headlines
        /// </summary>
        public ObservableCollection<HeadlineViewModel> Headlines
        {
            get { return _headlines; }
            private set
            {
                if (_headlines == value)
                    return;

                _headlines = value;
                OnPropertyChanged("Headlines");
            }
        }

        #endregion // Access Properties

        #region Constructors

        public MainViewModel() 
        {
            _feeds = new ObservableCollection<FeedViewModel>();
            var feeds = FeedRepository.FetchFeeds();
            feeds.ForEach(i => _feeds.Add(new FeedViewModel(i)));

            _headlines = new ObservableCollection<HeadlineViewModel>();
        }

        #endregion // Constructors

        #region Commands

        public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                    _refreshCommand = new DelegateCommand(Refresh);
                return _refreshCommand;
            }
        }

        public bool CanRefresh()
        {
            return !(String.IsNullOrEmpty(_uri)); 
        }

        public void Refresh()
        {
            var headlines = FeedRepository.FetchHeadlines(_uri);
            headlines.ForEach(i => _headlines.Add(new HeadlineViewModel(i)));
            OnPropertyChanged("Headlines");
        }

        #endregion // Commands
    }
}
