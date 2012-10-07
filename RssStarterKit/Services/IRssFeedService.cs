using RssStarterKit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RssStarterKit.Services
{
    public interface IRssFeedService
    {
        ObservableCollection<RssFeed> GetFeeds();
        Task<RssFeed> InsertFeed(string Title, string Link);
        Task<bool> DeleteFeed(RssFeed feed);
    }
}
