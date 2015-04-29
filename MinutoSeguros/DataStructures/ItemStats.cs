using MinutoSeguros.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Syndication;

namespace MinutoSeguros.DataStructures
{
    /// <summary>
    /// Contains stats related to a given Item
    /// </summary>
    class ItemStats
    {
        public SyndicationItem Item { get; private set; }
        public int WordCount { get; private set; }
        public ReadOnlyCollection<WordStats> Words { get; private set; }

        public ItemStats(SyndicationItem item)
        {
            this.Item = item;

            var text = CreatePlainText(item);

            Words = CreateWordCounts(text);
            WordCount = Words.Sum(x => x.Ocurrences);
        }

        private string CreatePlainText(SyndicationItem item)
        {
            // get content and title
            var content = item.ElementExtensions
                .ReadElementExtensions<string>("encoded", "http://purl.org/rss/1.0/modules/content/")
                .FirstOrDefault();

            var title = item.Title.Text;

            // get only data from HTML tags, we don't want to count word from html tags
            content = HtmlToText.GetText(content);
            title = HtmlToText.GetText(title);

            // merge title and content
            var text = title + " " + content;

            // Scape non alphabetic character, we only want to count valida words
            var plainText = StringUtil.ScapeNonAlphabeticCharaters(text);

            return plainText;
        }

        private static string[] Prepositions = new string[] { "a", "ante", "após", "até", "com", "contra", "de", "desde", "em", "entre", "para", "per", "perante", "por", "sem", "sob", "sobre", "trás", "ao", "à", "aos", "às", "do", "da", "dos", "das", "dum", "duma", "duns", "dumas", "no", "na", "nos", "nas", "num", "numa", "nuns", "numas", "pelo", "pela", "pelos", "pelas", "e", "the", "on" };
        private static string[] Articles = new string[] { "o", "a", "os", "as", "um", "uma", "uns", "umas" };
        private static string[] InvalidWords = Prepositions.Union(Articles).ToArray();

        private static ReadOnlyCollection<WordStats> CreateWordCounts(string text)
        {
            // Split the words and process to avoid errors
            var separators = new char[] { ' ' };
            var words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToLowerInvariant())
                .Except(InvalidWords);

            // Group and count ocurrences
            var wordCounts = words.GroupBy(x => x)
                .Select(x => new WordStats(x.Key, x.Count()))
                .ToList();

            return new ReadOnlyCollection<WordStats>(wordCounts);
        }
    }
}
