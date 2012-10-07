using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RssStarterKit.Model
{
    public class RssFeed
    {
        public int Id { get; set; }

        [DataMember(Name="title")]
        public string Title { get; set; }

        [DataMember(Name="link")]
        public string Link { get; set; }

        [DataMember(Name = "lastUpdated")]
        public DateTimeOffset LastUpdated { get; set; }

        [IgnoreDataMember]
        public List<RssItem> Items { get; set; }
    }
}
