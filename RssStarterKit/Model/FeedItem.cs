using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RssStarterKit.Model
{
    public class FeedItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTimeOffset PubDate { get; set; }
        public string Link { get; set; }
    }
}
