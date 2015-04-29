using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace MinutoSeguros.Providers
{
    /// <summary>
    /// Provides functionality to obtain Topics
    /// </summary>
    class FeedProvider
    {
        /// <summary>
        /// Fetches Items from the given URL Feed
        /// </summary>
        /// <param name="url">Address to look for Topic</param>
        /// <param name="count">Number of topic to fecth(ordered by publication date)</param>
        /// <returns>A list of Topics with raw data</returns>
        public IEnumerable<SyndicationItem> FetchItems(string url, int count)
        {
            // get xml feed
            var feed = FetchFeed(url);

            return feed.Items
                .OrderByDescending(x => x.PublishDate)
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// Fetch a feed from the given URL
        /// </summary>
        /// <param name="url">URL to fetch from</param>
        /// <returns>Feed from the given URL</returns>
        private SyndicationFeed FetchFeed(string url)
        {
            SyndicationFeed feed;

            using (var xml = XmlReader.Create(url))
            {
                feed = SyndicationFeed.Load(xml);
            }

            return feed;
        }
    }
}
