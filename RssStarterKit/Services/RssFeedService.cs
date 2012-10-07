using Microsoft.WindowsAzure.MobileServices;
using RssStarterKit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace RssStarterKit.Services
{
    public class RssFeedService : IRssFeedService
    {

        public static MobileServiceClient MobileService = new MobileServiceClient(
            "https://rssstarterkit.azure-mobile.net/",
            "odgGOtpMegpdQAiQrjSYfMCIdxafQn83");

        private IMobileServiceTable<RssFeed> feedTable = MobileService.GetTable<RssFeed>();

        private async Task<List<RssItem>> FetchFeedItemsForUrl(string url)
        {
            var items = new List<RssItem>();
            var client = new SyndicationClient();
            var uri = new Uri(url);
            var syndicationFeed = await client.RetrieveFeedAsync(uri);
            foreach (var syndicationItem in syndicationFeed.Items)
            {
                var feedItem = new RssItem()
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

        public async Task<RssFeed> InsertFeed(string Title, string Link)
        {
            try
            {
                var feed = new RssFeed()
                {
                    Title = Title,
                    Link = Link,
                };
                feed.Items = await FetchFeedItemsForUrl(Link);
                await feedTable.InsertAsync(feed);
                return feed;
            }
            catch (Exception ex)
            {
                // audit error
                return null;
            }
        }

        public async Task<bool> DeleteFeed(RssFeed feed)
        {
            try
            {
                await feedTable.DeleteAsync(feed);
                return true;
            }
            catch (Exception ex)
            {
                // audit exception
                return false;
            }
        }

        public ObservableCollection<RssFeed> GetFeeds()
        {
            return null;
        }

    }
}
