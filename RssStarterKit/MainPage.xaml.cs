using Microsoft.WindowsAzure.MobileServices;
using RssStarterKit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Syndication;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RssStarterKit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IMobileServiceTable<Feed> feedTable = App.MobileService.GetTable<Feed>();
        private ObservableCollection<Feed> feeds;

        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RefreshFeeds();
        }


        private void RefreshFeeds()
        {
            // This code refreshes the entries in the list view be querying the TodoItems table.
            // The query excludes completed TodoItems
            //feeds = feedTable.ToCollectionView();
            feeds = GetTestFeeds();
            FeedList.ItemsSource = feeds;
        }

        private ObservableCollection<Feed> GetTestFeeds()
        {
            var results = new ObservableCollection<Feed>();

            results.Add(new Feed() { Title = "ChrisKoenig.net", Link = "http://feeds.feedburner.com/chriskoenig" });
            results.Add(new Feed() { Title = "GiveCamp.org", Link = "http://feeds.feedburner.com/givecamp" });

            return results;
        }

        private async void AddFeed(Feed feed)
        {
            await feedTable.InsertAsync(feed);
            feeds.Add(feed);
        }

        private async void DeleteFeed(Feed feed)
        {
            await feedTable.DeleteAsync(feed);
            feeds.Remove(feed);
        }

        private async Task<List<FeedItem>> LoadItemsForFeed(string url)
        {
            var items = new List<FeedItem>();
            var client = new SyndicationClient();
            var uri = new Uri(url);
            var syndicationFeed = await client.RetrieveFeedAsync(uri);
            foreach (var syndicationItem in syndicationFeed.Items)
            {
                var feedItem = new FeedItem()
                {
                    Title = syndicationItem.Title.Text,
                    Link = syndicationItem.Links[0].Uri.ToString(),
                    Summary = syndicationItem.Summary.Text,
                    PubDate = syndicationItem.PublishedDate,
                };
                items.Add(feedItem);
            }
            return items;
        }

        private async void FeedList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var feed = e.AddedItems[0] as Feed;
            if (feed == null) return;
            feed.Items = await LoadItemsForFeed(feed.Link);
            FeedItems.ItemsSource = feed.Items;
        }

        private void FeedItems_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var feedItem = e.AddedItems[0] as FeedItem;
            if (feedItem == null) return;
            //FeedItemWebView.Navigate(new Uri(feedItem.Link));
            FeedItemWebView.NavigateToString(feedItem.Summary);
        }

    }
}
