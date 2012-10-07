using Microsoft.WindowsAzure.MobileServices;
using RssStarterKit.Model;
using RssStarterKit.Services;
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
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<RssFeed> feeds;
        private IRssFeedService feedService = new MockRssFeedService();

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RefreshFeeds();
        }

        #region Infrastructure

        private void RefreshFeeds()
        {
            feeds = feedService.GetFeeds();
            FeedList.ItemsSource = feeds;
        }

        private async void ShowItemsForFeed(RssFeed feed)
        {
            if (feed == null) return;
            FeedItems.ItemsSource = feed.Items;
        }

        private void ShowContentForFeedItem(RssItem feedItem)
        {
            if (feedItem == null) return;
            FeedItemWebView.NavigateToString(feedItem.Summary);
        }
        #endregion

        #region Crud

        private async void AddFeed(string Title, string Link)
        {
            var feed = await feedService.InsertFeed(Title, Link);
            feeds.Add(feed);
        }

        private async void DeleteFeed(RssFeed feed)
        {
            var result = await feedService.DeleteFeed(feed);
            if (result)
            {
                feeds.Remove(feed);
            }
        }
        #endregion

        #region Event Handlers

        private async void FeedList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            var feed = e.AddedItems[0] as RssFeed;
            ShowItemsForFeed(feed);
        }

        private void FeedItems_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            var feedItem = e.AddedItems[0] as RssItem;
            ShowContentForFeedItem(feedItem);
        }

        #endregion

    }
}
