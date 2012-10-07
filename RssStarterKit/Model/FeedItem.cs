using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace RssStarterKit.Model
{

    public class RssItem
    {

        public int Id { get; set; }

        [DataMember(Name="title")]
        public string Title { get; set; }
        
        [DataMember(Name = "summary")]
        public string Summary { get; set; }
        
        [DataMember(Name = "pubDate")]
        public DateTimeOffset PubDate { get; set; }
        
        [DataMember(Name = "link")]
        public string Link { get; set; }
        
        [DataMember(Name = "unread")]
        public bool Unread { get; set; }

    }
}
