using RssStarterKit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RssStarterKit.Services
{
    public class MockRssFeedService : IRssFeedService
    {

        public MockRssFeedService()
        {
        }

        public ObservableCollection<RssFeed> GetFeeds()
        {
            var results = new ObservableCollection<RssFeed>();

            results.Add(new RssFeed()
            {
                Title = "ChrisKoenig.net",
                Link = "http://feeds.feedburner.com/chriskoenig",
                LastUpdated = DateTimeOffset.Now,
                Items = new List<RssItem>()
                    {
                        new RssItem() 
                        { 
                            Id = 11,
                            Link = "http://chriskoenig.net",
                            PubDate = DateTimeOffset.Now,
                            Summary = "This is a summary",
                            Title = "Title",
                            Unread = false,
                        },
                        new RssItem() 
                        { 
                            Id = 12,
                            Link = "http://chriskoenig.net",
                            PubDate = DateTimeOffset.Now,
                            Summary = "This is a summary",
                            Title = "Title",
                            Unread = false,
                        },
                        new RssItem() 
                        { 
                            Id = 13,
                            Link = "http://chriskoenig.net",
                            PubDate = DateTimeOffset.Now,
                            Summary = "This is a summary",
                            Title = "Title",
                            Unread = false,
                        },
                        new RssItem() 
                        { 
                            Id = 14,
                            Link = "http://chriskoenig.net",
                            PubDate = DateTimeOffset.Now,
                            Summary = "This is a summary",
                            Title = "Title",
                            Unread = false,
                        },
                    },
            });

            results.Add(new RssFeed()
            {
                Title = "GiveCamp.org",
                Link = "http://feeds.feedburner.com/givecamp",
                LastUpdated = DateTimeOffset.Now,
                Items = new List<RssItem>()
                    {
                        new RssItem() 
                        { 
                            Id = 21,
                            Link = "http://givecamp.org",
                            PubDate = DateTimeOffset.Now,
                            Summary = "This is givecamp summary 1",
                            Title = "Title",
                            Unread = false,
                        },
                        new RssItem() 
                        { 
                            Id = 22,
                            Link = "http://givecamp.org",
                            PubDate = DateTimeOffset.Now,
                            Summary = "This is givecamp summary 1",
                            Title = "Title",
                            Unread = false,
                        },
                    },
            });

            return results;
        }

        public async Task<RssFeed> InsertFeed(string Title, string Link)
        {
            var feed = new RssFeed()
            {
                Title = Title,
                Link = Link,
                Id = 99,
                Items = new List<RssItem>() 
                {
                    new RssItem() 
                    { 
                        Id = 31,
                        Title = "New RssItem 1",
                        Summary = "New RssItem 1 Summary",
                        Unread = true,
                        PubDate = DateTimeOffset.Now,
                    },
                    new RssItem() 
                    { 
                        Id = 32,
                        Title = "New RssItem 2",
                        Summary = "New RssItem 2 Summary",
                        Unread = true,
                        PubDate = DateTimeOffset.Now,
                    },
                },
            };
            return feed;
        }

        public async Task<bool> DeleteFeed(RssFeed feed)
        {
            return true;
        }

    }
}
